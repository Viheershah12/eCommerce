$(function () {
    var l = abp.localization.getResource('eCommerce');

    var dataTable = $('#customerGroupTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[3, "asc"]],
            scrollX: true,
            searching: true,
            processing: true,
            ajax: abp.libs.datatables.createAjax(customer.controllers.customerGroup.getList),
            columnDefs:
                [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        visible: abp.auth.isGranted('Customer.CustomerGroup.Edit') || abp.auth.isGranted('Customer.CustomerGroup.View'),
                                        action: function (data) {
                                            var link = abp.appPath + 'CustomerGroup/Edit?id=' + data.record.id
                                            window.location.href = link
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        visible: abp.auth.isGranted('Customer.CustomerGroup.Delete'),
                                        confirmMessage: function (data) {
                                            return l(
                                                'CustomerGroupDeletionConfirmationMessage',
                                                data.record.name
                                            );
                                        },
                                        action: function (data) {
                                            customer.controllers.customerGroup
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