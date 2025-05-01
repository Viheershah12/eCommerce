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