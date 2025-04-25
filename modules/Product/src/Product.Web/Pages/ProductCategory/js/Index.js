$(function () {
    var l = abp.localization.getResource('eCommerce');

    var dataTable = $('#productCategoryTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            scrollX: true,
            searching: true,
            processing: true,
            ajax: abp.libs.datatables.createAjax(product.controllers.productCategory.getList),
            columnDefs:
                [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        visible: abp.auth.isGranted('Product.ProductCategory.Edit') || abp.auth.isGranted('Product.ProductCategory.View'),
                                        action: function (data) {
                                            var link = abp.appPath + 'ProductCategory/Edit?id=' + data.record.id
                                            window.location.href = link
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        visible: abp.auth.isGranted('Product.ProductCategory.Delete'),
                                        confirmMessage: function (data) {
                                            return l(
                                                'ProductCategoryDeletionConfirmationMessage',
                                                data.record.name
                                            );
                                        },
                                        action: function (data) {
                                            product.controllers.productCategory
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
                    {
                        title: l('DisplayOrder'),
                        data: 'displayOrder'
                    },
                    {
                        title: l('IsActive'),
                        data: 'isActive',
                        render: function (data, type, row) {
                            return data ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                        }
                    }
                ]
        })
    );
})