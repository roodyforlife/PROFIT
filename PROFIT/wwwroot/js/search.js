let cityArray = [];
$(document).ready(function () {
    $('.content__search__category').hover(function (event) {
        if (event.type == "mouseenter") {
            $('.search__category__menu').addClass('_active__search__menu');
        } else if (event.type == "mouseleave") {
            $('.search__category__menu').removeClass('_active__search__menu');
        }
    });

    $('.content__search__sort').hover(function (event) {
        if (event.type == "mouseenter") {
            $('.search__sort__menu').addClass('_active__search__menu');
        } else if (event.type == "mouseleave") {
            $('.search__sort__menu').removeClass('_active__search__menu');
        }
    });

    $('.sort__menu__item').on('click', function () {
        let sortType = $(this).attr("sort");
        let sortText = $(this).attr("sortValue");
        $('.SortValue').text(sortText);
        $('.SortInput').val(sortType);
        $('.search__sort__menu').removeClass('_active__search__menu');
    });

    $('.CostFrom').on('input', function () {
        if (+$(this).val() < 0) {
            $(this).val(0)
        }
    });

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
        }, 500)
    });
});


// Click on city and set value of input
function ClickOnCity(city, area) {
    $('.city__input').val(`${city}, ${area} обл.`);
    $('.city__list').addClass('city__list__hide');
    $('.city__list').removeClass('city__list__active');
}