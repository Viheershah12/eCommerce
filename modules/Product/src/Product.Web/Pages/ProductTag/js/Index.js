$(function () {
    var l = abp.localization.getResource('eCommerce');

    var dataTable = $('#productTagTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            scrollX: true,
            searching: true,
            processing: true,
            ajax: abp.libs.datatables.createAjax(product.controllers.productTag.getList),
            columnDefs:
                [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        visible: abp.auth.isGranted('Product.ProductTag.Edit') || abp.auth.isGranted('Product.ProductTag.View'),
                                        action: function (data) {
                                            var link = abp.appPath + 'ProductTag/Edit?id=' + data.record.id
                                            window.location.href = link
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        visible: abp.auth.isGranted('Product.ProductTag.Delete'),
                                        confirmMessage: function (data) {
                                            return l(
                                                'ProductTagDeletionConfirmationMessage',
                                                data.record.name
                                            );
                                        },
                                        action: function (data) {
                                            product.controllers.productTag
                                                .delete(data.record.id)
                                                .then(function () {
                                                    abp.notify.info(l('SuccessfullyDeleted'));
                                                    dataTable.ajax.reload();
                                                })
                                        }
                                    }
                                ]
                        }
                    },
                    {
                        title: l('Name'),
                        data: 'name'
                    },
                    {
                        title: l('Description'),
                        data: 'description'
                    },
                ]
        })
    );
})