function showCategory(categoryId) {
    $.ajax({
        url: '/Store/Index?handler=SwitchCategory&categoryId=' + categoryId,
        type: 'GET',
        beforeSend: function () {
            abp.ui.block({
                busy: true
            });
        },
        success: function (response) {
            $('#productsTable').html(response);
            const tabs = document.querySelectorAll('.category-item');

            tabs.forEach(tab => {
                const tabId = tab.id;
                if (tabId === categoryId) {
                    tab.classList.add('active');
                } else {
                    tab.classList.remove('active');
                }
            });
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        },
        complete: function () {
            abp.ui.unblock();
        }
    });
}