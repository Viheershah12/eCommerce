$(document).on('change', '#StockMovement_InventoryId', function () {
    var dropdown = document.getElementById('StockMovement_InventoryId');
    var selectedOption = dropdown.options[dropdown.selectedIndex];
    $('#StockMovement_ProductName').val(selectedOption.innerHTML);

    $.ajax({
        type: 'GET',
        url: '/api/inventory/stockBalance/get?id=' + selectedOption.value
    })
    .done(function (result) {
        $('#StockMovement_QuantityBeforeChange').val(result.stockQuantity);
    });
});

$(document).on('change', '#StockMovement_QuantityChanged', function () {
    var movementType = $('#StockMovement_MovementType').val();
    var change = parseFloat($('#StockMovement_QuantityChanged').val()) || 0;
    var oldQuantity = parseFloat($('#StockMovement_QuantityBeforeChange').val()) || 0;
    var total;

    if (movementType === '1' || movementType === '4' || movementType === '7') {
        total = oldQuantity + change;
    }
    else if (['2', '3', '5', '6'].includes(movementType)) {
        total = oldQuantity - change;
    }

    $('#StockMovement_QuantityAfterChange').val(total);
});

$(document).on('change', '#StockMovement_MovementType', function () {
    toggleQuantityChanged();
});

$(document).ready(function () {
    toggleQuantityChanged();
});

function toggleQuantityChanged() {
    var movementType = $('#StockMovement_MovementType').val();
    var quantityInput = $('#StockMovement_QuantityChanged');

    if (movementType) {
        quantityInput.prop('disabled', false);
    } else {
        quantityInput.prop('disabled', true);
        quantityInput.val('0.0'); // optional: clear input
    }
}