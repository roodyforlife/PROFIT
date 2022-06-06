let cityArray = [];
$(document).ready(function () {
    var settings = {
        apiKey: "a9160aa06bd246d834a84184413120d7",
        modelName: "Address",
        calledMethod: "getCities",
        methodProperties: {
        },
    }

    $.ajax({
        url: "https://api.novaposhta.ua/v2.0/json/",
        type: "POST",
        data: JSON.stringify(settings),
        success: function (response) {
            cityArray = response.data;
        }
    });

    // textarea count symbols
    $('.create__item__textarea textarea').on('change keyup paste', function () {
        $('.create__item___count span').html(this.value.length);
    });

    // make a list of cities
    $('.city__input').on("input", function () {
        if (this.value.length > 3) {
            $('.city__list').removeClass('city__list__hide')
            $('.city__list').addClass('city__list__active')
            $('.city__list ul').empty();
            cityArray.forEach(element => {
                let city = element.DescriptionRu;
                if (city.indexOf('(') - 1 > 0)
                    city = city.substring(0, city.indexOf('(') - 1)
                if (city.toLowerCase().includes(this.value.toLowerCase())) {
                    $('.city__list ul').html($('.city__list ul').html() + `<li onclick="ClickOnCity('${element.DescriptionRu}', '${element.AreaDescriptionRu}')">
                    <div class="city__list__title"><h3>${element.DescriptionRu}</h3></div>
                    <div class="city__list__text"><h6>${element.AreaDescriptionRu} обл.</h6></div>
                </li>`)
                }
            });
        }
        else {
            $('.city__list').addClass('city__list__hide')
            $('.city__list').removeClass('city__list__active')
        }
    });

    // check if input value equals name of city
    $('.city__input').on("change", function () {
        let thisInput = this, isExists = false;
        setTimeout(function () {
            $('.city__list').addClass('city__list__hide')
            $('.city__list').removeClass('city__list__active')
            cityArray.forEach(element => {
                let city = element.DescriptionRu;
                if (city.indexOf('(') - 1 > 0)
                    city = city.substring(0, city.indexOf('(') - 1)
                if (thisInput.value == `${element.DescriptionRu}, ${element.AreaDescriptionRu} обл.`) {
                    isExists = true;
                }
            });
            if (!isExists)
                thisInput.value = null;
        }, 1000)
    });
});

// load photo on site
function LoadPhotos(event) {
    $('.create__item__photos .shimmer').remove();
    let i = 0;
    for (let item of event.files) {
        var fr = new FileReader();
        fr.readAsDataURL(item)
        fr.onload = function (e) {
            $('.create__item__photos').html($('.create__item__photos').html() + `<div class="create__photos__item shimmer">
            <img src="${e.currentTarget.result}" alt="">
            <div class="_radio">
                <input class="_radio__content" type="radio" id="photo_${i}"
                    name="photo" value="${i}">
                <label for="photo_${i++}">
                    <div class="_radio__prompt">
                        <p>Это фото будет на обложке</p>
                    </div>
                </label>
            </div>
        </div>`);
        };
    }
}

// Click on city and set value of input
function ClickOnCity(city, area) {
    $('.city__input').val(`${city}, ${area} обл.`);
    $('.city__list').addClass('city__list__hide');
    $('.city__list').removeClass('city__list__active');
}

// validation for uploaded photo
function UploadPhotos(event) {
    var thisTarget = event.target;
    let errorLevel = false;
    if (thisTarget.files.length <= 5) {
        $('.photo__error').css("display", "none");
        $('label[for="uploadPhotos"]').css("box-shadow", "none");
        for (let item of thisTarget.files) {
            if (item.size >= 5242880) {
                $('label[for="uploadPhotos"]').css("box-shadow", "0 0 10px var(--red-color)");
                $('.photo__error').css("display", "block");
                $('.photo__error').html("Каждая фотография не должна привышать 5Мб.");
                errorLevel = true;
            }
        }
    }
    else {
        $('label[for="uploadPhotos"]').css("box-shadow", "0 0 10px var(--red-color)");
        $('.photo__error').css("display", "block");
        $('.photo__error').html("Число фотографий не должно быть более чем 5 ед.")
        errorLevel = true;
    }
    if (!errorLevel) {
        LoadPhotos(thisTarget);
        errorLevel = false;
    }
    else {
        test1.value = "";
    }
}