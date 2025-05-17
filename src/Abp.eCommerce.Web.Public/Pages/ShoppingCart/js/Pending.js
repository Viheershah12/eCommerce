const transactionId = $('#TransactionId').val();
const intervalId = setInterval(async () => {
    const response = await fetch(`/api/paymenttransactions/paymenttransaction/status?transactionId=${transactionId}`);
    const data = await response.json();

    if (data === 15) {
        clearInterval(intervalId); // Stop the interval
        abp.message.success('Payment successful!', 'Success').then(() => {
            window.location.href = '/Order/';
        });
    }
    else if (data === 20 || data === 25) {
        clearInterval(intervalId); // Stop the interval
        abp.message.warn('Payment failed or was cancelled.', 'Warning').then(() => {
            window.location.href = '/Order/';
        });
    }
}, 3000);