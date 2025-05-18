//const transactionId = $('#TransactionId').val();
//const intervalId = setInterval(async () => {
//    const response = await fetch(`/api/paymenttransactions/paymenttransaction/status?transactionId=${transactionId}`);
//    const data = await response.json();

//    if (data === 15) {
//        clearInterval(intervalId); // Stop the interval
//        abp.message.success('Payment successful!', 'Success').then(() => {
//            window.location.href = '/Order/';
//        });
//    }
//    else if (data === 20 || data === 25) {
//        clearInterval(intervalId); // Stop the interval
//        abp.message.warn('Payment failed or was cancelled.', 'Warning').then(() => {
//            window.location.href = '/Order/';
//        });
//    }
//}, 3000);


$(function () {
    const transactionId = $('#TransactionId').val();
    var transactionReceived = false;

    var connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalr-hubs/transaction")
        .withAutomaticReconnect()
        .build();

    connection.on("ReceiveTransactionStatus", function (data) {
        transactionReceived = true;
        console.info("Status is:" + data);

        if (data === '15') {
            abp.message.success('Payment successful!', 'Success').then(() => {
                window.location.href = '/Order/';
            });
        }
        else if (data === '20' || data === '25') {
            abp.message.warn('Payment failed or was cancelled.', 'Warning').then(() => {
                window.location.href = '/Order/';
            });
        }
    });

    function invokeCheckTransaction() {
        connection.invoke("CheckMpesaTransactionStatus", transactionId)
            .then(function () {
                console.info("Triggered CheckMpesaTransactionStatus");
            })
            .catch(function (err) {
                console.error("Invoke error:", err.toString());
            });
    }

    connection.start().then(function () {
        //invokeCheckTransaction();

        // Wait 30 seconds, then re-invoke if no response was received
        setTimeout(function () {
            if (!transactionReceived) {
                console.warn("No transaction status received in 30 seconds. Retrying...");
                invokeCheckTransaction();
            }
        }, 30000); // 30,000 ms = 30 seconds
    })
    .catch(function (err) {
        console.error("SignalR connection failed:", err.toString());
    });
});