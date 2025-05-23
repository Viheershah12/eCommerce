(function ($) {
    $(function () {
        var l = abp.localization.getResource("eCommerce");

        $('#BillingAddressForm').submit(function (e) {
            e.preventDefault();

            if (!$('#BillingAddressForm').valid()) {
                return false;
            }

            var input = $('#BillingAddressForm').serializeFormToObject(false);

            customer.controllers.customer.updateBillingAddress(input).then(function (result) {
                abp.notify.success(l('BillingAddressSaved'));
            });
        });
    });
})(jQuery);