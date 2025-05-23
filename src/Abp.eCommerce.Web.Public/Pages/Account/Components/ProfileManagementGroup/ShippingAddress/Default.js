(function ($) {
    $(function () {
        var l = abp.localization.getResource("eCommerce");

        $('#ShippingAddressForm').submit(function (e) {
            e.preventDefault();

            if (!$('#ShippingAddressForm').valid()) {
                return false;
            }

            var input = $('#ShippingAddressForm').serializeFormToObject(false);

            customer.controllers.customer.updateShippingAddress(input).then(function (result) {
                abp.notify.success(l('ShippingAddressSaved'));
            });
        });
    });
})(jQuery);