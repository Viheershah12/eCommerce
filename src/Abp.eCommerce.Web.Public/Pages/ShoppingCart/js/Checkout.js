const form = document.querySelector('form');
const confirmMpesaBtn = document.getElementById('confirmMpesaBtn');
const mpesaModal = new bootstrap.Modal(document.getElementById('mpesaModal'));

form.addEventListener('submit', function (e) {
    const selectedPayment = document.querySelector('input[name="PaymentMethod"]:checked')?.value;

    if (selectedPayment === '2') {
        e.preventDefault(); // stop normal form submission
        mpesaModal.show();  // show modal
    }
    else {
        e.currentTarget.submit();
    }
});

confirmMpesaBtn.addEventListener('click', function () {
    const phoneInput = document.getElementById('mpesaInput').value.trim();

    if (!phoneInput || !/^2547\d{8}$/.test(phoneInput)) {
        alert('@L["InvalidPhoneNumber"]'); // simple validation
        return;
    }

    document.getElementById('PhoneNumber').value = phoneInput;
    mpesaModal.hide();

    setTimeout(() => {
        form.submit();
    }, 300); // wait for modal to close
});