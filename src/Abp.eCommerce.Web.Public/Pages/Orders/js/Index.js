var l = abp.localization.getResource('eCommerce');

$(document).on('click', '.status-item', function () {
    const status = $(this).data('status');
    toggleStatus(status);
});

function toggleStatus(status) {
    $('#SelectedStatus').val(status);
    var startDate = $('#StartDate').val();
    var endDate = $('#EndDate').val();

    $.ajax({
        url: '/Orders/Index?handler=ToggleStatus&status=' + status + "&StartDate=" + startDate + "&EndDate=" + endDate,
        type: 'GET',
        beforeSend: function () {
            abp.ui.block({
                busy: true
            });
        },
        success: function (response) {
            $('#orderTable').html(response);
            const tabs = document.querySelectorAll('.status-item');

            tabs.forEach(tab => {
                const tabId = tab.id;
                if (tabId === status) {
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

$(document).on('click', '.cancelOrder', function (e) {
    var orderId = $(this).data('id');

    abp.message.confirm(l('OrderCancelConfirmationMessage'))
        .then(function (confirmed) {
            if (confirmed) {
                $.ajax({
                    url: '/Orders/Index?handler=CancelOrder&orderId=' + orderId,
                    type: 'GET',
                    beforeSend: function () {
                        abp.ui.block({
                            busy: true
                        });
                    },
                    success: function (response) {
                        window.location.href = '/Orders/';
                    },
                    error: function (xhr, status, error) {
                        console.error(xhr.responseText);
                    },
                    complete: function () {
                        abp.ui.unblock();
                    }
                });
            }
        });
});