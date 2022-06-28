(function () {
    'use strict';

    angular.module('DNet.services.errorLog', [])
        .service('ErrorLogService', ErrorLogService);

    ErrorLogService.$inject = ['Endpoint', '$http', '$q', '$state', 'UIHelper', 'toastr', 'baNotification', 'PendingRequest', 'BaseHTTPService', 'RequestHandler'];
    function ErrorLogService(Endpoint, $http, $q, $state, UIHelper, toastr, baNotification, PendingRequest, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.errorLog;
        service.Search = _searchData;
        service = angular.extend(angular.copy(BaseHTTPService), service);
        service.GetErrorDetail = _getErrorDetail;
        service.GetErrorLogSummaryPerApplication = _getErrorLogSummaryPerApplication;
        service.GetErrorLogMainInfo = _getErrorLogMainInfo;
        service.Delete = _delete;
        service.GetApplicationLogList = _getApplicationLogList;
        service.GetLatestErrorLog = _getLatestErrorLog;
        return service;

        /* public methods */
        function _searchData($params, appname) {
            var url = this.endpoint.search + "?appName=" + appname;
            return RequestHandler.post(url, $params, false);
        }

        function _delete(data) {
            var url = this.endpoint.delete;
            return RequestHandler.post(url, data);
        }

        function _getErrorLogSummaryPerApplication(app) {
            var url = this.endpoint.getErrorLogSummaryPerApplication;
            return RequestHandler.get(url + '?application=' + app);
        }

        function _getLatestErrorLog(take, appName, severity) {
            var url = this.endpoint.getLatestErrorLog + '?totalTake=' + take + '&appName=' + appName + '&severity=' + severity;
            return RequestHandler.get(url);
        }

        function _getErrorLogMainInfo() {
            var url = this.endpoint.getErrorLogMainInfo;
            return RequestHandler.get(url);
        }

        function _getErrorDetail(id, app) {
            var url = this.endpoint.getErrorDetail + "?id=" + id + "&app=" + app;
            var defer = $q.defer();
            var _headers = {
                'Content-Type': 'application/json'
            }

            PendingRequest.add({
                url: url,
                state: $state.current.name,
                canceller: defer
            }, 'GET');
            $http({
                method: 'GET',
                url: url,
                headers: _headers,
                timeout: defer.promise
            }).then(function success(xhr) {
                if (xhr.status == 200) {
                    defer.resolve(xhr.data);
                } else {
                    if (xhr.status == 403) {
                        $state.go('auth.unauthorized');
                    }
                    else {
                        toastr.error('', '', UIHelper.toastrCustom.error);
                        defer.reject(xhr.data);
                    }
                    baNotification.addNotification('', false);
                }
            }, function error(xhr) {
                switch (xhr.status) {
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
                        break;
                    case 404:
                    default:
                        toastr.error('', '', UIHelper.toastrCustom.error);
                        baNotification.addNotification('', false);
                        break;
                }
                defer.reject(xhr)
            }).finally(function () {
                PendingRequest.remove(url, 'GET');
            });
            return defer.promise;
        }

        function _getApplicationLogList() {
            var url = this.endpoint.getApplicationLogList;
            return RequestHandler.get(url);
        }
    }

})();
