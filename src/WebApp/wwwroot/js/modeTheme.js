function changeTheme() {
    const body = document.body;
    const html = document.querySelector('html');
    const icon = document.getElementById('theme-icon');

    body.classList.toggle("dark-theme");

    const isDarkMode = body.classList.contains("dark-theme");
    html.setAttribute("data-bs-theme", isDarkMode ? "dark" : "light");

    icon.className = isDarkMode ? "bi bi-moon-stars-fill" : "bi bi-sun-fill";
    
    // Save the user's preference to localStorage
    localStorage.setItem("darkMode", isDarkMode);
}
