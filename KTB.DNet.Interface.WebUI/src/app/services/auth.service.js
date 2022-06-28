/**
 * @author v.lugovsky
 * created on 16.12.2015
 */
(function () {
    'use strict';

    angular.module('DNet.services.auth', [])
        .service('AuthenticationService', AuthenticationService);

    AuthenticationService.$inject = ['$state', '$http', '$q', 'config', 'Endpoint', '$cookies', '$rootScope', 'RequestHandler', 'ResponseHandler', 'ModalHelper', 'PendingRequest'];
    function AuthenticationService($state, $http, $q, config, Endpoint, $cookies, $rootScope, RequestHandler, ResponseHandler, ModalHelper, PendingRequest) {
        var _headers = {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
        var cookiesKey = 'dnetuic' + '-' + config.rootUrl;

        var service = {};
        service.endpoint = Endpoint.auth;
        service.Login = Login;
        service.Token = Token;
        service.SetCredentials = SetCredentials;
        service.ClearCredentials = ClearCredentials;
        service.GetUserPermissions = GetUserPermissions;
        service.GetDNETUICFromCookies = GetDNETUICFromCookies;

        return service;

        function Login(username, password) {
            var $entity = { username: username, password: password };
            return RequestHandler.post(this.endpoint.login, $entity)
        }

        function GetUserPermissions(userId, clientId) {
            return RequestHandler.post(this.endpoint.userPermission, { UserId: userId, ClientId: clientId });
        }

        function Token(entity) {
            var defer = $q.defer()
            var url = config.tokenIssuer;
            PendingRequest.add({
                url: url,
                state: $state.current.name,
                canceller: defer,
                loader: true
            }, 'POST');

            ModalHelper.showProgressBar();
            $http({
                method: 'POST',
                url: url,
                data: $.param(entity),
                headers: _headers
            }).then(function (response) {
                if (response.status == 200) {
                    defer.resolve(response);
                } else {
                    defer.reject(response);
                }
            }, ResponseHandler.error)
                .finally(function () {
                    PendingRequest.remove(url, 'POST');
                    ModalHelper.hideProgressBar();
                });
            return defer.promise;
        }

        function SetCredentials(entity, token) {
            $rootScope.dnetuic = {
                currentUser: {
                    userId: entity.userId,
                    username: entity.username,
                    clientId: entity.clientId,
                    token: token,
                    dealerCode: entity.dealerCode,
                    isDMSAdmin: entity.isDMSAdmin
                }
            };

            // set default auth header for http requests
            $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;

            // store user details in dnetuic cookie that keeps user logged in for 1 week (or until they logout)
            var cookieExp = new Date();
            cookieExp.setDate(cookieExp.getDate() + 1);

            $cookies.putObject(cookiesKey, $rootScope.dnetuic, { expires: cookieExp });
        }


        function ClearCredentials() {
            $rootScope.dnetuic = undefined;
            $rootScope.userPermissions = undefined;
            $cookies.remove(cookiesKey);
            $http.defaults.headers.common.Authorization = 'Bearer';
        }

        function GetDNETUICFromCookies()
        {
            return $cookies.get(cookiesKey) || undefined;
        }
    }

})();