(function () {
    var sw = window.swaggerUi;
    $(document).ready(function () {

        $('#busy').hide();
        $("#changeLogin").click(function (e) {
            e.preventDefault();
            sw.api.clientAuthorizations.remove('Authorization');
            showLoginForm();
        });
        $('#username').focus();
    });
    $('#api_selector').submit(function () {
        var username = $('#username').val();
        var password = $('#password').val();
        var dealercode = $('#dealercode').val();
        var tokenUrl = sw.api.securityDefinitions.oauth2.tokenUrl;
        var apiUrl = sw.api.scheme + "://" + sw.api.host + sw.api.basePath;
        //console.log(username);
        //console.log(password);
        //console.log(tokenUrl);
        //console.log(apiUrl);
        //console.log(dealercode);
        
        showBusyIndicator();
        $.ajax({
            url: tokenUrl,
            type: "post",
            contenttype: 'x-www-form-urlencoded',
            data: "DealerCode=" + dealercode + "&Username=" + username + "&Password=" + password,
            success: function (response) {
                var bearerToken = 'Bearer ' + response.access_token;
                sw.api.clientAuthorizations.add('Authorization', new window.SwaggerClient.ApiKeyAuthorization('Authorization', bearerToken, 'header'));
                sw.api.clientAuthorizations.remove('api_key');
                $("#readOnlyUserName").text(username.toLowerCase());
                $("#readOnlyDealerCode").text(dealercode);
                showWhoIsLoggedIn();
            },
            error: function (xhr, ajaxoptions, ex) {
                alert("Login failed! " + xhr.responseText);
                showLoginForm();
            }
        });
        return false;
    });
})();

function showLoginForm() {
    $('#username').val('');
    $('#password').val('');
    $("#loggedInUser").hide();
    $("#busy").hide();
    $("#api_selector").show();
}
function showWhoIsLoggedIn() {
    $("#api_selector").hide();
    $("#busy").hide();
    $("#loggedInUser").show();
}
function showBusyIndicator() {
    $("#api_selector").hide();
    $("#loggedInUser").hide();
    $("#busy").show();
}