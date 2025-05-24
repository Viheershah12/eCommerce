var l = abp.localization.getResource("eCommerce");

async function initMap() {
    const latVal = $('#Customer_DeliveryAddress_Latitude').val();
    const lngVal = $('#Customer_DeliveryAddress_Longitude').val();

    const latitude = parseFloat(latVal);
    const longitude = parseFloat(lngVal);

    console.log("Latitude:", latitude, "Longitude:", longitude);

    const [{ Map }, { AdvancedMarkerElement }] = await Promise.all([
        google.maps.importLibrary("maps"),
        google.maps.importLibrary("marker"),
    ]);

    // Validate coordinates before using them
    const isValidCoords = !isNaN(latitude) && !isNaN(longitude);
    const mapCenter = isValidCoords
        ? { lat: latitude, lng: longitude }
        : { lat: -1.286389, lng: 36.817223 }; // fallback to Nairobi

    const map = new Map(document.getElementById("map"), {
        zoom: 15,
        center: mapCenter,
        mapId: "DEMO_MAP_ID", // optional
    });

    if (isValidCoords) {
        new AdvancedMarkerElement({
            map: map,
            position: { lat: latitude, lng: longitude },
            title: "Delivery Location",
        });
    }
}

$('#PaymentTransactionNote').submit(function (e) {
    e.preventDefault();

    if (!$('#PaymentTransactionNote').valid()) {
        return false;
    }

    var input = $('#PaymentTransactionNote').serializeFormToObject(false);

    paymentTransactions.controllers.paymentTransaction.updatePaymentTransactionNote(input).then(function (result) {
        abp.notify.success(l('UpdatedPaymentTransactionNote'));
    });
});

$(function () {
    function orderNoteTable() {
        var list = []
        var orderNoteList = $('#OrderNotes').val()

        if (orderNoteList != null && orderNoteList != '') {
            list = JSON.parse(orderNoteList)
        }
        return list
    }

    var createOrderNoteModal = new abp.ModalManager(abp.appPath + 'Order/AddOrderNoteModal');
    var editOrderNoteModal = new abp.ModalManager(abp.appPath + 'Order/EditOrderNoteModal');
    var orderId = $('#Order_Id').val();

    $('#addOrderNote').click(function (e) {
        e.preventDefault()
        createOrderNoteModal.open({ orderId: orderId })
    });

    createOrderNoteModal.onResult(function () {
        location.reload()
    });

    editOrderNoteModal.onResult(function () {
        location.reload()
    });

    $('#orderNoteTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: false,
            paging: false,
            order: [[1, "asc"]],
            scrollX: true,
            searching: false,
            processing: false,
            data: orderNoteTable(),
            columnDefs:
                [
                    {
                        title: l('Actions'),
                        rowAction: {
                            items:
                                [
                                    {
                                        text: l('Edit'),
                                        //visible: abp.auth.isGranted('Management.ContentManagement.Edit') || abp.auth.isGranted('Management.ContentManagement.View'),
                                        action: function (data) {
                                            editOrderNoteModal.open({ id: data.record.Id, orderId: orderId })
                                        }
                                    },
                                    {
                                        text: l('Delete'),
                                        //visible: abp.auth.isGranted('Management.ContentManagement.Delete'),
                                        confirmMessage: function (data) {
                                            return l(
                                                'OrderNoteDeletionConfirmationMessage',
                                                data.record.CreatorName
                                            );
                                        },
                                        action: function (data) {
                                            order.controllers.orderTransaction
                                                .deleteOrderNote({ id: data.record.Id, orderId: orderId })
                                                .then(function () {
                                                    abp.notify.info(l('SuccessfullyDeleted'));
                                                    location.reload();
                                                })
                                        }
                                    }
                                ]
                        }
                    },
                    {
                        title: l('OrderNoteType'),
                        data: 'OrderNoteTypeName',
                        render: function (data) {
                            return l(data);
                        }
                    },
                    {
                        title: l('Note'),
                        data: 'Note'
                    },
                    {
                        title: l('CreatedOn'),
                        data: 'CreationTime',
                        render: function (data, type, row) {
                            if (!data) return '';

                            const date = new Date(data);

                            const day = String(date.getDate()).padStart(2, '0');
                            const month = String(date.getMonth() + 1).padStart(2, '0'); // 0-based month
                            const year = date.getFullYear();

                            const hours = String(date.getHours()).padStart(2, '0');
                            const minutes = String(date.getMinutes()).padStart(2, '0');

                            return `${day}/${month}/${year} ${hours}:${minutes}`;
                        }
                    },
                    {
                        title: l('CreatorName'),
                        data: 'CreatorName'
                    }
                ]
        })
    );
});