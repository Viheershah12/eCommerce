$(function () {
    var l = abp.localization.getResource('eCommerce');

    var dataTable = $('#orderTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            scrollX: true,
            searching: true,
            processing: true,
            ajax: abp.libs.datatables.createAjax(order.controllers.orderTransaction.getList),
            columnDefs:
                [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('View'),
                                        //visible: abp.auth.isGranted('Management.ContentManagement.Edit') || abp.auth.isGranted('Management.ContentManagement.View'),
                                        action: function (data) {
                                            var link = abp.appPath + 'Order/View?id=' + data.record.id
                                            window.location.href = link
                                        }
                                    }
                                    //{
                                    //    text: l('Delete'),
                                    //    visible: abp.auth.isGranted('Management.ContentManagement.Delete'),
                                    //    confirmMessage: function (data) {
                                    //        return l(
                                    //            'ContentDeletionConfirmationMessage',
                                    //            data.record.title
                                    //        );
                                    //    },
                                    //    action: function (data) {
                                    //        management.controllers.content
                                    //            .delete(data.record.id)
                                    //            .then(function () {
                                    //                abp.notify.info(l('SuccessfullyDeleted'));
                                    //                dataTable.ajax.reload();
                                    //            })
                                    //    }
                                    //}
                                ]
                        }
                    },
                    {
                        title: l('CreatedOn'),
                        data: 'creationTime'
                    },
                    {
                        title: l('CustomerName'),
                        data: 'customerName'
                    },
                    {
                        title: l('PhoneNumber'),
                        data: 'phoneNumber'
                    },
                    {
                        title: l('TotalAmount'),
                        data: 'totalAmount'
                    },
                    {
                        title: l('OrderStatus'),
                        data: 'status',
                        render: function (data) {
                            return l(data);
                        }
                    },
                    {
                        title: l('PaymentStatus'),
                        data: 'paymentStatus',
                        render: function (data) {
                            return l(data);
                        }
                    },
                    {
                        title: l('PaymentMethod'),
                        data: 'paymentMethodSystemName',
                        render: function (data) {
                            return l(data);
                        }
                    }
                ]
        })
    );
})