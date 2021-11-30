
// toggle dark mode
const btn = $(".dark-mode-switch");
const label = $("#dark-mode-label");
const theme = $("#theme-link");

btn.on("click", () => {
    btn.prop("checked") ? theme.attr("href", "/css/light_mode.css") :
        theme.attr("href", "/css/dark_mode.css");

    let text = label.text();
    label.text(
        text == "Switch to Light Mode" ? "Switch to Dark Mode" : "Switch to Light Mode");
})
