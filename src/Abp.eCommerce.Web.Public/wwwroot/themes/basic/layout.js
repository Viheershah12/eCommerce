$(function () {
    // Your existing code
    $('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
        $(this).next().toggleClass('show');

        if (!$(this).next().hasClass('show')) {
            $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
        }

        var $subMenu = $(this).next(".dropdown-menu");
        $subMenu.toggleClass('show');

        $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
            $('.dropdown-submenu .show').removeClass("show");
        });

        return false;
    });

    // NEW: Remove initial 'show' classes for clean hover behavior
    $('.dropdown-toggle.show').removeClass('show');
    $('.dropdown-menu.show').removeClass('show');
});

function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('mainContent');

    // Toggle sidebar and main content classes
    sidebar.classList.toggle('collapsed');
    sidebar.classList.toggle('expanded');

    mainContent.classList.toggle('ms-md-320');
    mainContent.classList.toggle('ms-md-60');
}

//document.addEventListener("click", function (event) {
//    var dropdown = document.getElementById("cartDropdownButton");
//    var menu = dropdown?.nextElementSibling;

//    if (!dropdown.contains(event.target) && !menu.contains(event.target)) {
//        var dropdownInstance = bootstrap.Dropdown.getInstance(dropdown);
//        if (dropdownInstance) dropdownInstance.hide();
//    }
//});