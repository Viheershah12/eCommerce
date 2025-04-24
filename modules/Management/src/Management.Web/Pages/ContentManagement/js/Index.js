$(function () {
    var l = abp.localization.getResource('eCommerce');

    var dataTable = $('#contentManagementTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            scrollX: true,
            searching: true,
            processing: true,
            ajax: abp.libs.datatables.createAjax(management.controllers.content.getList),
            columnDefs:
                [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        //visible: abp.auth.isGranted('Setup.TaskSetup.Edit') || abp.auth.isGranted('Setup.TaskSetup.View'),
                                        action: function (data) {
                                            var link = abp.appPath + 'ContentManagement/Edit?id=' + data.record.id
                                            window.location.href = link
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        //visible: abp.auth.isGranted('Setup.TaskSetup.Delete'),
                                        confirmMessage: function (data) {
                                            return l(
                                                'ContentDeletionConfirmationMessage',
                                                data.record.name
                                            );
                                        },
                                        action: function (data) {
                                            management.controllers.content
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
                        title: l('Title'),
                        data: 'title'
                    },
                    {
                        title: l('ContentType'),
                        data: 'contentType',
                        render: function (data) {
                            return l(data);
                        }
                    },
                    {
                        title: l('IsPublished'),
                        data: 'isPublished',
                        render: function (data, type, row) {
                            return data ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                        }
                    }
                ]
        })
    );
})