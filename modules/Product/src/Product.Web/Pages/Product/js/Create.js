let uploadedImages = [];
var l = abp.localization.getResource('eCommerce');

function previewImages(input) {
    const previewInner = document.getElementById('image-preview-inner');
    const indicators = document.getElementById('thumbnail-indicators');
    const thumbnailWrapper = document.getElementById('thumbnail-wrapper');

    // Show carousel
    thumbnailWrapper.style.display = "block";

    if (input.files && input.files.length > 0) {
        let filesProcessed = 0;

        Array.from(input.files).forEach((file, index) => {
            const reader = new FileReader();
            reader.onload = function (e) {
                const newImage = e.target.result;
                uploadedImages.push({ downloadBinary: newImage, contentType: file.type, id: generateRandomId(), isInDb: false });
                const newIndex = uploadedImages.length - 1;

                const div = document.createElement('div');
                div.className = `carousel-item`;
                div.innerHTML = `
                    <div style="position: relative;">
                        <img src="${newImage}" class="d-block w-100 img-fluid rounded" 
                             alt="Uploaded Image" style="cursor:pointer; max-height: 400px; object-fit: contain;" 
                             onclick="openImageModal(${newIndex})" />
                        <button onclick="deleteImage(${newIndex}, false)" type="button"
                                style="position: absolute; top: 10px; right: 10px; z-index: 10; background: rgba(255,0,0,0.7); border: none; color: white; padding: 5px 10px; border-radius: 5px; cursor: pointer;">
                            Delete
                        </button>
                    </div>
                `;
                previewInner.appendChild(div);

                const button = document.createElement('button');
                button.type = 'button';
                button.setAttribute('data-bs-target', '#image-preview');
                button.setAttribute('data-bs-slide-to', uploadedImages.length - 1);
                indicators.appendChild(button);

                // Remove previous active
                previewInner.querySelectorAll('.carousel-item').forEach(item => item.classList.remove('active'));
                indicators.querySelectorAll('button').forEach(btn => btn.classList.remove('active'));
                indicators.querySelectorAll('button').forEach(btn => btn.removeAttribute('aria-current'));

                // Set new active
                div.classList.add('active');
                button.classList.add('active');
                button.setAttribute('aria-current', 'true');

                updateThumbnailCounter(newIndex + 1, uploadedImages.length);

                filesProcessed++;
                if (filesProcessed === input.files.length) {
                    setTimeout(() => {
                        setupThumbnailCounter();
                    }, 500);
                }
            }
            reader.readAsDataURL(file);
        });

        setTimeout(() => {
            setupThumbnailCounter();
        }, 500);
    }
}
function generateRandomId() {
    return 'img_' + Math.random().toString(36).substr(2, 9);
}

function deleteImage(index, isInDb = true, isInModal = false) {
    abp.message.confirm(l('ImageDeletionConfirmationMessage'))
        .then(function (confirmed) {
            if (confirmed) {
                const imageId = uploadedImages[index].id;
                const productId = $('#Product_Id').val();

                if (isInDb) {
                    abp.ajax({
                        type: 'DELETE',
                        url: `/api/product/product/deleteProductMedia?id=${productId}&fileId=${imageId}`
                    }).then(function () {
                        uploadedImages.splice(index, 1);
                        refreshThumbnailCarousel();
                        toggleThumbnailWrapper();

                        if (isInModal) {
                            const modalElement = document.getElementById('imageModal');
                            const modal = bootstrap.Modal.getInstance(modalElement);
                            modal.hide();
                        }
                    });
                }
                else {
                    uploadedImages.splice(index, 1);
                    removeFileFromInput(index);
                    refreshThumbnailCarousel();
                    toggleThumbnailWrapper();
                }
            }
            else {
                return;
            }
        });
}

function removeFileFromInput(index) {
    const fileInput = document.getElementById('Product_Media');

    if (!fileInput.files.length) {
        return;
    }

    const dt = new DataTransfer(); // Create a new file list

    Array.from(fileInput.files)
        .forEach((file, i) => {
            if (i !== index) {
                dt.items.add(file); // Keep all files except the one to remove
            }
        });

    fileInput.files = dt.files; // Set the updated file list
}

function getActiveSlideIndex(carouselId) {
    const carousel = document.getElementById(carouselId);
    const activeItem = carousel.querySelector('.carousel-item.active');
    const allItems = Array.from(carousel.querySelectorAll('.carousel-item'));
    return allItems.indexOf(activeItem);
}

function refreshThumbnailCarousel() {
    const previewInner = document.getElementById('image-preview-inner');
    const indicators = document.getElementById('thumbnail-indicators');

    previewInner.innerHTML = '';
    indicators.innerHTML = '';

    uploadedImages.forEach((imgSrc, index) => {
        const div = document.createElement('div');
        div.className = `carousel-item ${index === 0 ? 'active' : ''}`;
        div.innerHTML = `
            <div style="position: relative;">
                <img src="${imgSrc.downloadBinary}" class="d-block w-100 img-fluid rounded" 
                     alt="Uploaded Image" style="cursor:pointer; max-height: 400px; object-fit: contain;" 
                     onclick="openImageModal(${index})" />
                <button onclick="deleteImage(${index}, ${imgSrc.isInDb})" type="button" 
                        style="position: absolute; top: 10px; right: 10px; z-index: 10; background: rgba(255,0,0,0.7); border: none; color: white; padding: 5px 10px; border-radius: 5px; cursor: pointer;">
                    Delete
                </button>
            </div>
        `;
        previewInner.appendChild(div);

        const button = document.createElement('button');
        button.type = 'button';
        button.setAttribute('data-bs-target', '#image-preview');
        button.setAttribute('data-bs-slide-to', index);
        button.className = index === 0 ? 'active' : '';
        button.setAttribute('aria-current', index === 0 ? 'true' : 'false');
        indicators.appendChild(button);
    });

    updateThumbnailCounter(uploadedImages.length > 0 ? 1 : 0, uploadedImages.length);
}

function toggleThumbnailWrapper() {
    const wrapper = document.getElementById('thumbnail-wrapper');
    if (uploadedImages.length === 0) {
        wrapper.style.display = 'none';
    } else {
        wrapper.style.display = 'block';
    }
}

function openImageModal(activeIndex) {
    const carouselInner = document.getElementById('carousel-inner');
    const indicators = document.getElementById('modal-indicators');
    carouselInner.innerHTML = "";
    indicators.innerHTML = "";

    uploadedImages.forEach((imgSrc, index) => {
        const newIndex = uploadedImages.length - 1;

        const div = document.createElement('div');
        div.className = `carousel-item ${index === activeIndex ? 'active' : ''}`;
        div.innerHTML = `
            <div style="position: relative;">
                <img src="${imgSrc.downloadBinary}" class="d-block w-100 img-fluid rounded" 
                        alt="Uploaded Image" style="cursor:pointer; max-height: 400px; object-fit: contain;" 
                        onclick="openImageModal(${newIndex})" />
                <button onclick="deleteImage(${newIndex}, ${imgSrc.isInDb}, true)" type="button"
                        style="position: absolute; top: 10px; right: 10px; z-index: 10; background: rgba(255,0,0,0.7); border: none; color: white; padding: 5px 10px; border-radius: 5px; cursor: pointer;">
                    Delete
                </button>
            </div>
        `;
        carouselInner.appendChild(div);

        const button = document.createElement('button');
        button.type = 'button';
        button.setAttribute('data-bs-target', '#imageCarousel');
        button.setAttribute('data-bs-slide-to', index);
        button.className = index === activeIndex ? 'active' : '';
        button.setAttribute('aria-current', index === activeIndex ? 'true' : 'false');
        indicators.appendChild(button);
    });

    updateModalCounter(activeIndex + 1, uploadedImages.length);

    const myModal = new bootstrap.Modal(document.getElementById('imageModal'));
    myModal.show();

    setTimeout(() => {
        setupModalCounter();
    }, 500);
}

function setupThumbnailCounter() {
    const previewCarousel = document.getElementById('image-preview');
    previewCarousel.addEventListener('slid.bs.carousel', function (e) {
        updateThumbnailCounter(e.to + 1, uploadedImages.length);
    });
}

function setupModalCounter() {
    const modalCarousel = document.getElementById('imageCarousel');
    modalCarousel.addEventListener('slid.bs.carousel', function (e) {
        updateModalCounter(e.to + 1, uploadedImages.length);
    });
}

function updateThumbnailCounter(current, total) {
    document.getElementById('thumbnail-counter').textContent = `${current} / ${total}`;
}

function updateModalCounter(current, total) {
    document.getElementById('modal-counter').textContent = `${current} / ${total}`;
}