using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// Handles all Json database inquiries and operations
    /// </summary>
    public class JsonFileProductService
    {
        /// <summary>
        /// Initiated JsonFileProductService
        /// </summary>
        /// <param name="webHostEnvironment">environment</param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Retrieves web host environment
        /// </summary>
        /// <returns>IWebHostEnvironment</returns>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Returns file path of product json database
        /// </summary>
        /// <returns>the name of the json product file</returns>
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        /// <summary>
        /// Retrieves products
        /// </summary>
        /// <returns>iterable list of products</returns>
        public IEnumerable<ProductModel> GetProducts()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);//Use simple 'using' statement (IDE0063)
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Adds a rating to the specified product ID
        /// </summary>
        /// <param name = "productId">ID of specific product</param>
        /// <param name = "rating">Rating to be added</param>
        /// <returns>true if the addition of a rating is successful, false otherwise</returns>
        public bool AddRating(string productId, int rating)
        {
            // If the ProductID is invalid, return false
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            //Get products method and return the producus lists
            var products = GetProducts();

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (data == null)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add( rating );
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveData(products);

            return true;
        }

        /// <summary>
        /// Adds a comment to the specified product ID
        /// </summary>
        /// <param name = "productId">ID of specific product</param>
        /// <param name = "comment">Comment to be added</param>
        public void AddComment(string productId, string comment)
        {
            //Get products method and return the products lists
            var products = GetProducts();

            //Chech the id product whether null, if null, create a new list of comments , if not null, adds comments to existing comments lists
            if (products.First(x => x.Id == productId).Comments == null)
            {
                //creates new list of comments
                products.First(x => x.Id == productId).Comments = new string[] { comment };
            }
            if (products.First(x => x.Id == productId).Comments != null)
            {
                //adds comment to existing comments list
                var comments = products.First(x => x.Id == productId).Comments.ToList();
                comments.Add(comment);
                products.First(x => x.Id == productId).Comments = comments.ToArray();
            }

            //Update to storage
            SaveData(products);
        }

        /// <summary>
        /// Save All products data to storage
        /// </summary>
        private void SaveData(IEnumerable<ProductModel> products)
        {
            //Use simple 'using' statement (IDE0063)
            using var outputStream = File.Create(JsonFileName); 
            {
                JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    {
                        SkipValidation = true,
                        Indented = true
                    }),
                    products
                );
            }
        }

        /// <summary>
        /// Find the data record
        /// Update the fields
        /// Save to the data store
        /// </summary>
        /// <param name = "data"></param>
        /// <returns>The updated product data</returns>
        public ProductModel UpdateData(ProductModel data)
        {
            //Get products lists
            var products = GetProducts();

            //find the id data 
            var productData = products.FirstOrDefault(x => x.Id.Equals(data.Id));

            // data is null , return null
            if (productData == null)
            {
                return null;
            }

            // Update the data information
            productData.Artist = data.Artist;
            productData.Title = data.Title;            
            productData.Description = data.Description;
            productData.Image = data.Image;

            //Update to storage
            SaveData(products);

            //return updated data
            return productData;
        }

        /// <summary>
        /// Create a new product using default values
        /// After create the user can update to set values
        /// </summary>
        /// <returns>An empty product</returns>
        public ProductModel CreateData()
        {
            //create a data model and fill with information
            var data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Artist = "",
                Title = "",
                Description = "",
                Image = "",
            };

            // Get the current set, and append the new record to it becuase IEnumerable does not have Add
            var dataSet = GetProducts();

            //append the data into dataset
            dataSet = dataSet.Append(data);

            //Update to storage
            SaveData(dataSet);

            //return updated data
            return data;
        }

        /// <summary>
        /// Remove the item from the system
        /// </summary>
        /// <returns>The data that was deleted</returns>
        public ProductModel DeleteData(string id)
        {
            // Get the current set, and append the new record to it
            var dataSet = GetProducts();

            //find the id data 
            var data = dataSet.FirstOrDefault(m => m.Id.Equals(id));

            //create a new data set and delete the id data
            var newDataSet = GetProducts().Where(m => m.Id.Equals(id) == false);

            //Update to storage
            SaveData(newDataSet);

            //return updated data
            return data;
        }

        /// <summary>
        /// Finds the average rating of a product
        /// </summary>
        /// <param name = "product">ProductModel of the product to find the average ratings for</param>
        /// <returns>The average rating for the passed in product</returns>
        public int GetAverageRating(ProductModel product)
        {
            //initial the parameters
            int currentRating = 0;
            int voteCount = 0;

            //Checks if there are ratings
            if (product.Ratings == null) // product with no ratings
            {
                currentRating = 0;
            }
            if (product.Ratings != null) // product with ratings
            {
                //retrieves number of votes
                voteCount = product.Ratings.Count(); 
                
                //calculates average of all votes
                currentRating = product.Ratings.Sum() / voteCount; 
            }

            //return the result
            return currentRating;
        }

        /// <summary>
        /// Finds the 3 highest rated artworks of all the artworks
        /// </summary>
        /// <returns>Tuple of (productID, rating) top 3 highest rated artworks</returns>
        public List<ProductModel> GetHighestRatedArtwork()
        {
            //Initiate List
            var listOfID = new List<(string, int)>();

            //Gets Product List
            var dataset = GetProducts();

            //finds the average rating of each artwork and adds it and product ID to list
            foreach (var product in dataset)
            {
                listOfID.Add((product.Id, GetAverageRating(product)));
            }

            //sort list in decending order by item2 of tuple (the rating)
            //so that the highest rated will be at beginnning of list
            listOfID = listOfID.OrderByDescending(x => x.Item2).ToList();

            List<ProductModel> topThree = new List<ProductModel>();

            //Add product with corresponding ID of top three products to topThree
            foreach (var item in listOfID.GetRange(0, 3))
            {
                foreach (var art in dataset)
                {
                    if (item.Item1 == art.Id)
                    {
                        topThree.Add(art);
                    }
                }
            }

            //return first 3 entries of list (top 3 highest rated products)
            return topThree;
        }

        /// <summary>
        /// Method to sort ProductModel IEnumerable from highest rated to lowest rated.
        /// </summary>
        /// <returns>IEnumerable<ProductModel> sorted by descending rating</returns>
        public IEnumerable<ProductModel> GetProductSortedByDescRating()
        {
            //Initiate List
            var listOfID = new List<(string, int)>();

            //Gets Product List
            var dataset = GetProducts();

            //finds the average rating of each artwork and adds it and product ID to list
            foreach (var product in dataset)
            {
                listOfID.Add((product.Id, GetAverageRating(product)));
            }

            //sort list in decending order by item2 of tuple (the rating)
            // the highest rated will be at beginnning of list
            listOfID = listOfID.OrderByDescending(x => x.Item2).ToList();

            //get the product lists first
            List<ProductModel> result = new List<ProductModel>();

            //sort list in decending order by item2 of tuple (the rating)
            foreach (var item in listOfID)
            {
                foreach (var art in dataset)
                {
                    if (item.Item1 == art.Id)
                    {
                        result.Add(art);
                    }
                }
            }

            //return the result 
            return result.AsEnumerable();
        }

        /// <summary>
        /// Method to sort ProductModel IEnumerable from lowest to highest rating
        /// </summary>
        /// <returns>IEnumerable<ProductModel> sorted by ascending rating</returns>
        public IEnumerable<ProductModel> GetProductSortedByAscRating()
        {
            //Initiate List
            var listOfID = new List<(string, int)>();

            //Gets Product List
            var dataset = GetProducts();

            //finds the average rating of each artwork and adds it and product ID to list
            foreach (var product in dataset)
            {
                listOfID.Add((product.Id, GetAverageRating(product)));
            }

            //sort list in ascending order by item2 of tuple (the rating)
            //so that the highest rated will be at beginnning of list
            listOfID = listOfID.OrderBy(x => x.Item2).ToList();

            //get the product lists first
            List<ProductModel> result = new List<ProductModel>();

            //sort list in ascending order by item2 of tuple (the rating)
            foreach (var item in listOfID)
            {
                foreach (var art in dataset)
                {
                    if (item.Item1 == art.Id)
                    {
                        result.Add(art);
                    }
                }
            }

            return result.AsEnumerable();
        }

        /// <summary>
        /// Method to sort ProductModel IEnumerable by artist name.
        /// </summary>
        /// <returns>IEnumerable<ProductModel> sorted by artist name from A-Z</returns>
        public IEnumerable<ProductModel> GetProductSortedByAscArtist()
        {
            return GetProducts().OrderBy(x => x.Artist).ToList();
        }

        /// <summary>
        /// Method to sort ProductModel IEnumerable by artist name.
        /// </summary>
        /// <returns>IEnumerable<ProductModel> sorted by artist name from Z-A</returns>
        public IEnumerable<ProductModel> GetProductSortedByDescArtist()
        {
            return GetProducts().OrderByDescending(x => x.Artist).ToList();
        }

        /// <summary>
        /// Method to sort ProductModel IEnumerate by Title.
        /// </summary>
        /// <returns>IEnumerable<ProductModel> sorted by Title A-Z</returns>
        public IEnumerable<ProductModel> GetProductSortedByAscTitle()
        {
            return GetProducts().OrderBy( x => x.Title).ToList();
        }

        /// <summary>
        /// Method to sort ProductModel IEnumerate by Title.
        /// </summary>
        /// <returns>IEnumerable<ProductModel> sorted by Title Z-A</returns>
        public IEnumerable<ProductModel> GetProductSortedByDescTitle()
        {
            return GetProducts().OrderByDescending(x => x.Title).ToList();
        }
    }
}