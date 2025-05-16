$(function () {
    var l = abp.localization.getResource('eCommerce');

    var dataTable = $('#stockBalanceTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            scrollX: true,
            searching: true,
            processing: true,
            ajax: abp.libs.datatables.createAjax(inventory.controllers.stockBalance.getList),
            columnDefs:
                [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        //visible: abp.auth.isGranted('Product.Product.Edit') || abp.auth.isGranted('Product.Product.View'),
                                        action: function (data) {
                                            var link = abp.appPath + 'StockBalance/Edit?id=' + data.record.id
                                            window.location.href = link
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        //visible: abp.auth.isGranted('Product.Product.Delete'),
                                        confirmMessage: function (data) {
                                            return l(
                                                'StockBalanceDeletionConfirmationMessage',
                                                data.record.productName
                                            );
                                        },
                                        action: function (data) {
                                            inventory.controllers.stockBalance
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
                        title: l('ProductName'),
                        data: 'productName'
                    },
                    {
                        title: l('StockQuantity'),
                        data: 'stockQuantity'
                    },
                    {
                        title: l('ReservedQuantity'),
                        data: 'reservedQuantity'
                    },
                    {
                        title: l('AllowBackorder'),
                        data: 'allowBackorder',
                        render: function (data, type, row) {
                            return data ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                        }
                    }
                ]
        })
    );
})