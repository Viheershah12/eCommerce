$(function () {
    var l = abp.localization.getResource('eCommerce');

    var dataTable = $('#stockMovementTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            scrollX: true,
            searching: true,
            processing: true,
            ajax: abp.libs.datatables.createAjax(inventory.controllers.stockMovement.getList),
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
                                            var link = abp.appPath + 'StockMovement/Edit?id=' + data.record.id
                                            window.location.href = link
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        //visible: abp.auth.isGranted('Product.Product.Delete'),
                                        confirmMessage: function (data) {
                                            return l(
                                                'StockMovementDeletionConfirmationMessage',
                                                data.record.productName
                                            );
                                        },
                                        action: function (data) {
                                            inventory.controllers.stockMovement
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
                        title: l('MovementType'),
                        data: 'movementType',
                        render: function (data) {
                            return l(data);
                        }
                    },
                    {
                        title: l('QuantityChanged'),
                        data: 'quantityChanged'
                    },
                    {
                        title: l('QuantityBeforeChange'),
                        data: 'quantityBeforeChange'
                    },
                    {
                        title: l('QuantityAfterChange'),
                        data: 'quantityAfterChange'
                    }
                ]
        })
    );
})