function updateTotal() {
    let total = 0;
    let anyChecked = false;
    const selectedItemsContainer = $('#selectedItems');
    selectedItemsContainer.empty(); // Clear previous items

    $('.quantity-group').each(function () {
        const $group = $(this);
        const itemId = $group.data('id');
        const price = parseFloat($group.data('price'));
        const quantity = parseInt($group.find('.quantity-input').val());
        const isChecked = $(`.cart-item-checkbox[data-id='${itemId}']`).is(':checked');

        if (isChecked) {
            anyChecked = true;
            total += quantity * price;

            // Get item name
            const itemName = $(`.cart-item-checkbox[data-id='${itemId}']`).closest('.border-top').find('h6').text().trim();

            // Format price
            const formattedPrice = new Intl.NumberFormat('en-KE', {
                style: 'currency',
                currency: 'KES'
            }).format(price * quantity);

            // Append to summary
            selectedItemsContainer.append(
                `<div class="d-flex justify-content-between small">
                    <span>${itemName} x${quantity}</span>
                    <span>${formattedPrice}</span>
                </div>`
            );
        }
    });

    // Update total price
    $('.checkoutTotal').text(
        new Intl.NumberFormat('en-KE', {
            style: 'currency',
            currency: 'KES'
        }).format(total)
    );

    // Enable/disable checkout
    $('#checkoutButton').prop('disabled', !anyChecked);
}

$('.cart-item-checkbox').on('change', function () {
    const itemId = $(this).data('id');
    const enabled = $(this).is(':checked');
    const $buttons = $(`.quantity-group[data-id='${itemId}']`).find('button');

    $buttons.prop('disabled', !enabled);
    updateTotal();
});

$('.btn-increase').on('click', function () {
    const $group = $(this).closest('.quantity-group');
    const $input = $group.find('.quantity-input');
    let quantity = parseInt($input.val());
    quantity++;
    $input.val(quantity);
    updateTotal();
});

$('.btn-decrease').on('click', function () {
    const $group = $(this).closest('.quantity-group');
    const $input = $group.find('.quantity-input');
    let quantity = parseInt($input.val());
    if (quantity > 1) {
        quantity--;
        $input.val(quantity);
        updateTotal();
    }
});

// Trigger initial state
$('.cart-item-checkbox').trigger('change');