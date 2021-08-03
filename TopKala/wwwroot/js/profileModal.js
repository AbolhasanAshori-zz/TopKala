let modalBody = document.querySelector(".modal-body"); modalBody.remove();
const swalQueue = Swal.mixin({
    title: 'تغییر نمایه کاربری شما',
    showCloseButton: true,
});

let profile = document.querySelector("#profile");
profile.addEventListener("click", () => {
    swalQueue.fire({
        html: modalBody.outerHTML,
        width: '600px',
        input: 'file',
        inputLabel: 'آپلود عکس',
        inputAttributes: {
            'accept': 'image/*'
        },
        customClass: {
            input: 'd-none',
            inputLabel: 'btn btn-lg btn-secondary',
            confirmButton: 'd-none'
        },
        // showConfirmButton: false,
        showLoaderOnConfirm: true,
        preConfirm: async () => {
            let selectedImg = Swal.getHtmlContainer().querySelector(".profile-avatars-item.selected");
            if (selectedImg != null){
                let imagePath = selectedImg.getAttribute("src");
                return await uploadProfile(imagePath).then();
            }
        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => showMessageModal(result.value));

    let images = Swal.getHtmlContainer().querySelectorAll(".profile-avatars-item");
    confirmOnClick(images);

    let uploadInput = Swal.getInput();
    uploadInput.addEventListener("change", () => {
        if (uploadInput.files.length != 0) {
            const reader = new FileReader();
            var image;
            reader.onload = (e) => {
                image = e.target.result;
                showUploadModal(image);
            }
            reader.readAsDataURL(uploadInput.files[0]);
        }
    });
});

function showUploadModal(image) {
    var croppie;

    swalQueue.fire({
        html: '<div></div>',
        confirmButtonText: 'برش'
    }).then((result) => {
        if (result.isConfirmed) {
            // TODO: Check Photo Size
            croppie.result('base64').then(
                (cropImage) => showCropModal(cropImage)
            );
        }
    });

    setTimeout(() => {
        var el = Swal.getHtmlContainer().firstChild;
        croppie = new Croppie(el,{
            viewport: {
                width: 200,
                height: 200
            },
            boundary: {
                height: 400
            }
        });
        croppie.bind({
            url: image
        });
    }, 250); 
}

function showCropModal(image){
    var imgTag = document.createElement('img');
    imgTag.src = image;
    swalQueue.fire({
        html: imgTag.outerHTML,
        confirmButtonText: 'ارسال',
        showLoaderOnConfirm: true,
        preConfirm: async () => await uploadProfile(image).then(),
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => showMessageModal(result.value));
}

function showMessageModal(response){
    if (response.ok) {
        Swal.fire({
            icon: 'success',
            title: 'عملیات موفق آمیز بود',
            text: 'عکس پروفایل شما با موفقیت بروز رسانی شد',
            confirmButtonText: 'بستن'
        }).then(() => location.reload())
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'خطا در سرور',
            text: 'مشکلی در بروز رسانی عکس پروفایل شما پیش آمد',
            confirmButtonText: 'بستن'
        });
    }
}

async function uploadProfile(image){
    const response = await fetch('/Profile/ChangeProfile', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(image)
    });
    return response;
}

function confirmOnClick(images) {
    for (let i = 0; i < images.length; i++){
        images[i].addEventListener("click", function(){
            images[i].classList.add("selected");
            Swal.clickConfirm();
        });
    }
}