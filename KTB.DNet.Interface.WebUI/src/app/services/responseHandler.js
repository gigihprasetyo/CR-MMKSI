(function () {
    'use strict';

    angular.module('DNet.services.responseHandler', [])
        .service('ResponseHandler', ResponseHandler);

    ResponseHandler.$inject = ['$rootScope', '$state', 'UIHelper', 'toastr', 'baNotification', '$window'];
    function ResponseHandler($rootScope, $state, UIHelper, toastr, baNotification, $window) {
        var service = {};

        service.success = handleSuccess
        service.transSuccess = handleTransactionSuccess
        service.error = handleError
        service.successFile = handleSuccessFile

        return service;

        function handleSuccessFile(response, filename, defer) {
            if (response.data || (response.status == 200)) {
                var headers = response.headers();

                var contentType = headers['content-type'];

                var linkElement = document.createElement('a');
                try {
                    var blob = new Blob([response.data], { type: contentType });
                    var url = $window.URL.createObjectURL(blob);

                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": $window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                } catch (ex) {
                    console.log(ex);
                }
                defer.resolve(response.data);
            } else {
                if (response.status == 403) {
                    $state.go('auth.unauthorized');
                }
                else {
                    toastr.error(_populateErrorModelState(response), _limitText(response.data.Message), UIHelper.toastrCustom.error);
                    defer.reject(response.data);
                }
                baNotification.addNotification(response.data.Message, false);
            }
        }

        function handleSuccess(response, defer) {
            if (response.data.Success || (response.status == 200 && response.data.Records)) {
                defer.resolve(response.data);
            } else {
                if (response.status == 403) {
                    $state.go('auth.unauthorized');
                }
                else {
                    toastr.error(_populateErrorModelState(response), _limitText(response.data.Message), UIHelper.toastrCustom.error);
                    defer.reject(response.data);
                }
                baNotification.addNotification(response.data.Message, false);
            }
        }

        function handleTransactionSuccess(response, defer) {
            if (response.data.Success || (response.status == 200 && response.data.Records)) {
                defer.resolve(response.data);
                if (response.data.Message != null || response.data.Message != undefined) {
                    if (response.data.Message != "") {
                        toastr.success(response.data.Message);
                        baNotification.addNotification(response.data.Message, true);
                    }
                }
            } else {
                if (response.status == 403) {
                    $state.go('auth.unauthorized');
                }
                else {
                    toastr.error(_populateErrorModelState(response), _limitText(response.data.Message), UIHelper.toastrCustom.error);
                    defer.reject(response.data);
                }
                baNotification.addNotification(response.data.Message, false);
            }
        }

        function _limitText(str) {
            if (str != null) {
                var limit = 100;

                if (str !== undefined && str.length > 100)
                    return str.substring(0, 100) + "...";

            }

            return str;
        }

        function handleError(response, defer) {
            switch (response.status) {
                case -1:
                    break;
                case 401:
                    $rootScope.isLogin = true;
                    $state.go('auth.login');
                    $rootScope.tokenExpired = sessionStorage.getItem('visited');
                    break;
                case 403:
                case 405:
                    $state.go('auth.unauthorized');
                    //toastr.error(response.data.Message + '<br/>' + response.config.url, response.statusText, UIHelper.toastrCustom.error);
                    break;
                case 404:
                default:
                    toastr.error(response.data.Message, response.statusText, UIHelper.toastrCustom.error);
                    baNotification.addNotification(response.data.Message, false);
                    break;
            }
            defer.reject(response)
        }

        function _populateErrorModelState(response) {
            var msg = "<ol>";
            // if (response.data.Message) {
            //     msg += "<li>" + response.data.Message + "</li>";
            // }

            if (response.data.ModelState) {
                response.data.ModelState.forEach(function (item) {
                    if (item.errorMessage) msg += "<li>" + item.propertyName + ": " + _limitText(item.errorMessage) + "</li>";
                });
            }
            return msg + "<ol>";
        }
    }
})();
