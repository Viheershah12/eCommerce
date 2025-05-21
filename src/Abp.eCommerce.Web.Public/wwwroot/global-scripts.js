/* Your Global Scripts */
function paginateTable(pageSize, pageNumber, page, pageHandler, partialId, filterUrl) {
    var url = '/' + page + '?handler=' + pageHandler + '&PageSize=' + pageSize + '&PageNumber=' + pageNumber + filterUrl;

    $.ajax({
        url: url,
        type: 'GET',
        beforeSend: function () {
            abp.ui.block({
                busy: true
            });
        },
        success: function (response) {
            $(partialId).html(response);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        },
        complete: function () {
            abp.ui.unblock();
        }
    });
}

function changePageSize(pagesize, pageNumber, page, pageHandler, partialId, filterUrl) {
    var selectedOptionIndex = pagesize.selectedIndex;
    var selectedOptionValue = pagesize.options[selectedOptionIndex].value;

    var url = "/" + page + "?handler=" + pageHandler + "&PageSize=" + parseInt(selectedOptionValue) + "&PageNumber=" + pageNumber + filterUrl;

    $.ajax({
        url: url,
        type: 'GET',
        beforeSend: function () {
            abp.ui.block({
                busy: true
            });
        },
        success: function (response) {
            $(partialId).html(response);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        },
        complete: function () {
            abp.ui.unblock();
        }
    });
}

$(function () {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalr-hubs/transaction")
        .withAutomaticReconnect()
        .build();

    connection.on("ReceiveNotification", function (data) {
        console.info("Notification received:", data);
        showToastWithProgress(data.message, data.type, data.redirectUrl);
    });

    connection.start().then(function () {
        
    })
    .catch(function (err) {
        console.error("SignalR connection failed:", err.toString());
    });

    function showToastWithProgress(message, type = 'info', redirectUrl = null) {
        const $toast = toastr[type](message, '', {
            closeButton: true,
            timeOut: 10000,
            extendedTimeOut: 5000,
            progressBar: true
        });

        if (redirectUrl) {
            $toast.on('click', function () {
                window.location.href = redirectUrl;
            });
        }
    }
});