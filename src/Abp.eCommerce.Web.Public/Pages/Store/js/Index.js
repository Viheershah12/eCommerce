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

            $('.category-item').removeClass('active');
            $(`#${categoryId}`).addClass('active');
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        },
        complete: function () {
            abp.ui.unblock();
        }
    });
}