$(document).on('click', '.status-item', function () {
    const status = $(this).data('status');
    toggleStatus(status);
});

function toggleStatus(status) {
    $.ajax({
        url: '/Order/Index?handler=ToggleStatus&status=' + status,
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