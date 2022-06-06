$(document).ready(function () {
    $('.view__eye').click(function () {
        let input = $(this).parent().children('input');
        input.attr("type", input.attr("type") == "password" ? "text" : "password");
    });
});

function AddToLiked(id) {
    alert(id)
}

function DeleteFromLiked(id, event) {
    if (confirm(`Вы действительно хотите убрать из избранных объявление под номером ${id}?`)) {
        $(event.target).parents('.liked__content__item').remove()
    }
    // alert(id);
}

function DeleteLikedList() {
    if (confirm(`Вы действительно хотите очистить избранные?`)) {
        // ajax
        console.log(true)
    }
}

function SignOut() {
    $.ajax({
        url: "../Account/Logout",
        type: 'POST',
        cache: false,
        contentType: false,
        processData: false,
        data: null,
        success: function (response) {
            $('.header__profile__list__content').html(`<ul>
            <li>
            <a href="../Account/Login" class="header__list__ul__item _unselectable">
                <h5>Войти</h5>
            </a>
            </li>
            <li>
            <a href="../Account/Register" class="header__list__ul__item _unselectable">
                <h5>Регистрация</h5>
            </a>
            </li>
                </ul>`)
        }
    });
}