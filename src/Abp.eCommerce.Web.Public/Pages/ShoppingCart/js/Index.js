function updateTotal() {
    let total = 0;
    let anyChecked = false;

    $('.quantity-group').each(function () {
        const $group = $(this);
        const itemId = $group.data('id');
        const price = parseFloat($group.data('price'));
        const quantity = parseInt($group.find('.quantity-input').val());

        const isChecked = $(`.cart-item-checkbox[data-id='${itemId}']`).is(':checked');
        if (isChecked) {
            total += quantity * price;
            anyChecked = true;
        }
    });

    $('.checkoutTotal').text(
        new Intl.NumberFormat('en-KE', {
            style: 'currency',
            currency: 'KES'
        }).format(total)
    );

    // Enable/disable checkout button
    const checkoutButton = $('#checkoutButton');
    checkoutButton.prop('disabled', !anyChecked);
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