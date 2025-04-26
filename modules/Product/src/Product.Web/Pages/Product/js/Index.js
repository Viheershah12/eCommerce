$(function () {
    var l = abp.localization.getResource('eCommerce');

    var dataTable = $('#productTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            scrollX: true,
            searching: true,
            processing: true,
            ajax: abp.libs.datatables.createAjax(product.controllers.product.getList),
            columnDefs:
                [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        visible: abp.auth.isGranted('Product.Product.Edit') || abp.auth.isGranted('Product.Product.View'),
                                        action: function (data) {
                                            var link = abp.appPath + 'Product/Edit?id=' + data.record.id
                                            window.location.href = link
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        visible: abp.auth.isGranted('Product.Product.Delete'),
                                        confirmMessage: function (data) {
                                            return l(
                                                'ProductDeletionConfirmationMessage',
                                                data.record.name
                                            );
                                        },
                                        action: function (data) {
                                            product.controllers.product
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
                        title: l('SKU'),
                        data: 'sku'
                    },
                    {
                        title: l('Description'),
                        data: 'description'
                    },
                    {
                        title: l('IsPublished'),
                        data: 'isPublished',
                        render: function (data, type, row) {
                            return data ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                        }
                    },
                    {
                        title: l('IsNew'),
                        data: 'isNew',
                        render: function (data, type, row) {
                            return data ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                        }
                    }
                ]
        })
    );
})