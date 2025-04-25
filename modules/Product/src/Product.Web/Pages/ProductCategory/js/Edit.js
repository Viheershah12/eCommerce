function downloadFile(fileId, fileName) {
    abp.ui.block({ busy: true });

    $.ajax({
        url: `/ProductCategory/Edit?handler=DownloadFile&fileId=${fileId}&fileName=${fileName}`,
        type: 'GET',
        xhrFields: {
            responseType: 'blob'
        },
        success: function (blob, status, xhr) {
            abp.ui.unblock();

            // Extract filename from Content-Disposition header
            let disposition = xhr.getResponseHeader('Content-Disposition');
            let filename = ''; // Default filename

            if (disposition) {
                let match = disposition.match(/filename[^;=\n]*=(['"]?)([^;\n]+)\1/);
                if (match && match[2]) {
                    filename = match[2].trim();
                }
            }

            // Create download link and trigger click
            let url = window.URL.createObjectURL(blob);
            let a = document.createElement('a');
            a.href = url;
            a.download = filename.replace(/"/g, '');
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
            window.URL.revokeObjectURL(url);
        },
        error: function (xhr, status, error) {
            abp.ui.unblock();

            let errorMessage = "An unexpected error occurred.";
            if (xhr.responseJSON && xhr.responseJSON.message) {
                errorMessage = xhr.responseJSON.message;
            }

            abp.message.error(errorMessage);
        }
    });
}