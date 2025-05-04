var l = abp.localization.getResource('eCommerce');

document.getElementById('btnIncrement').addEventListener('click', function () {
    var input = document.getElementById('quantityInput');
    input.value = parseInt(input.value) + 1;
});

document.getElementById('btnDecrement').addEventListener('click', function () {
    var input = document.getElementById('quantityInput');
    if (parseInt(input.value) > 1) {
        input.value = parseInt(input.value) - 1;
    }
});

$('#addToCart').click(function (e) {
    e.preventDefault();

    abp.message.confirm(l('AddToCartMessage')).then(function (confirmed) {
        if (!confirmed) return;

        var productId = $('#Product_Id').val();
        var quantity = parseInt($('#quantityInput').val(), 10);

        if (!productId || isNaN(quantity) || quantity < 1) {
            abp.message.warn(l('InvalidQuantity'));
            return;
        }

        abp.ajax({
            type: 'GET',
            url: `/Store/Product?handler=AddToCart&ProductId=${productId}&Quantity=${quantity}`
        }).then(function (result) {
            if (result.success) {
                // Update cart count badge
                $('.fa-shopping-cart')
                    .closest('.btn')
                    .find('.badge')
                    .removeClass('d-none')
                    .addClass('d-block')
                    .text(result.cartCount);

                // Update offcanvas content
                $('#shoppingCartPanel').html(result.cartItemsHtml);

                abp.message.success(l('AddedToCart'));
            } else {
                abp.message.error(result.message || l('AddToCartFailed'));
            }
        }).catch(function (error) {
            abp.message.error(error);
        });
    });
});

$('#addToWishlist').click(function (e) {
    e.preventDefault();

    abp.message.confirm(l('AddToWishlistMessage')).then(function (confirmed) {
        if (!confirmed) return;

        var productId = $('#Product_Id').val();

        abp.ajax({
            type: 'GET',
            url: `/Store/Product?handler=AddToWishlist&productId=${productId}`
        }).then(function (result) {
            if (result.success) {
                // Update cart count badge
                $('.fa-star')
                    .closest('.btn')
                    .find('.badge')
                    .removeClass('d-none')
                    .addClass('d-block')
                    .text(result.wishListCount);

                // Update offcanvas content
                $('#wishlistPanel').html(result.wishListItemsHtml);

                abp.message.success(l('AddedToWishlist'));
            } else {
                abp.message.error(result.message || l('AddToCartFailed'));
            }
        }).catch(function (error) {
            abp.message.error(error);
        });
    });
});