
var scale = 1;
$(document).ready(function () {
    $('.view__photo__content').on('wheel', zoom)
    scroll();
    $('.burger__menu').on("click", function (e) {
        $(this).toggleClass('_active__burger');
        $('.chat__menu').toggleClass('_active__burger');
    });

    $('#MessageInput').on('input', function () {
        let messagePhotos = $('#messagePhotos')
        if ($(this).val().length > 0 || messagePhotos[0].files.length > 0) {
            $('.messages__input__submit').css('display', 'block')
        } else {
            $('.messages__input__submit').css('display', 'none')
        }
    });

    $('#messagePhotos').on('change', function () {
        LoadPhotos($(this))
    });

    $('.messages__uploaded__photo').click(function () {
        let photos = $('#messagePhotos')
        console.log(photos);
    });

    $('.tweet__photo__item, .view__photo').on('click', function () {
        scale = 1;
        $('.view__photo__content').css('transform', `scale(${scale})`)
        $('body').toggleClass('_lock_scroll')
        $('.view__photo').toggleClass('_active__view__photo')
        $('.view__photo__content img').attr('src', $(this).children('img').attr('src'))
    });
});

function DeleteUpload(id) {
    let photos = $('#messagePhotos')
    let fileArray = [...photos[0].files];
    fileArray.splice(id, 1)
    const propertyNames = new Object(...fileArray)
    let container = new DataTransfer();
    for (let i = 0; i < photos[0].files.length; i++) {
        if (i != id)
            container.items.add(photos[0].files[i]);
    }
    photos[0].files = container.files
    LoadPhotos($('#messagePhotos'));
}

function LoadPhotos(thisInput) {
    if (thisInput[0].files.length > 4) {
        thisInput.val(null);
        if ($('#MessageInput').val().length == 0)
            $('.messages__input__submit').css('display', 'none')
        $('.messages__uploaded__photos').css('display', 'none')
        $('.messages__uploaded__photos').empty();
        alert("Фотографий должно быть не более 4");
    } else if (thisInput[0].files.length == 0) {
        if ($('#MessageInput').val().length == 0)
            $('.messages__input__submit').css('display', 'none')
        $('.messages__uploaded__photos').css('display', 'none')
        $('.messages__uploaded__photos').empty();
    } else {
        $('.messages__input__submit').css('display', 'block')
        $('.messages__uploaded__photos').css('display', 'flex')
        $('.messages__uploaded__photos').empty();
        let i = 0;
        for (let item of thisInput[0].files) {
            var fr = new FileReader();
            fr.readAsDataURL(item)
            fr.onload = function (e) {
                $('.messages__uploaded__photos').html($('.messages__uploaded__photos').html() + `<div class="messages__uploaded__photo shimmer">
                    <img src="${e.currentTarget.result}" alt="">
                    <div class="uploaded__photo__cross" onclick="DeleteUpload(${i++})">&#10006;</div>
                </div>`);
            };
        }
    }
}

function scroll() {
    var scr = $('.chat__messages__container')[0].scrollHeight;
    $('.chat__messages__container').animate({ scrollTop: scr }, 1);
}
function zoom(event) {
    var translateX = ($('.view__photo__content').width() / 2) - event.pageX
    var translateY = ($('.view__photo__content').height() / 2) - event.pageY
    if (event.originalEvent.wheelDelta / 120 > 0) {
        if (scale < 4) {
            scale += 0.1;
            $('.view__photo__content').css('transform', `scale(${scale})`)
        }
    }
    else {
        if (scale > 0.5) {
            scale -= 0.1;
            $('.view__photo__content').css('transform', `scale(${scale})`)
        }
    }
    event.preventDefault();
}