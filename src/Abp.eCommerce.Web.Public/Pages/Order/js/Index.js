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

//$(function () {
//    var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44321/signalr-hubs/messaging").build();

//    connection.on("ReceiveMessage", function (message) {
//        $('#MessageList').append('<li><strong><i class="fas fa-long-arrow-alt-right"></i> ' + message + '</strong></li>');
//    });

//    connection.start().then(function () {
//        $('#SendMessageButton').click(function (e) {
//            e.preventDefault();

//            var targetUserName = "Admin";
//            var message = "Test";

//            connection.invoke("SendMessage", targetUserName, message)
//                .then(function () {
//                    $('#MessageList')
//                        .html('<li><i class="fas fa-long-arrow-alt-left"></i> ' + abp.currentUser.userName + ': ' + message + '</li>');
//                })
//                .catch(function (err) {
//                    return console.error(err.toString());
//                });
//        });
//    }).catch(function (err) {
//        return console.error(err.toString());
//    });
//});