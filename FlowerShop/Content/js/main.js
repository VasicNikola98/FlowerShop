
var hashedCookiesUserId = function () {
    let existingCookieData = document.cookie.match("cartItemHashedUserId");

    if (existingCookieData == undefined || existingCookieData == "" || existingCookieData == null) {
        existingCookieData = localStorage.getItem('cartHashUserId');
        document.cookie = "cartItemHashedUserId=" + existingCookieData + "; path=/";
    }
}

function getCartCounter() {
   
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        url: '/Korpa/BrojProizvodaUKorpi/',
        dataType: "json",
        success: function (result) {
            $('#cartProductsCount').html(result);
        }
    });
}

window.onload = function () {
    hashedCookiesUserId();
    getCartCounter();
}