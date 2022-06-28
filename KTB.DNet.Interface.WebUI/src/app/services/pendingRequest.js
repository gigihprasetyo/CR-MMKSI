(function () {
    'use strict';

    angular.module('DNet.services.pendingRequest', [])
        .service('PendingRequest', PendingRequest);

    PendingRequest.$inject = ['$rootScope', '$filter'];
    function PendingRequest($rootScope, $filter) {
        var service = {};
        var pending = {};

        pending['GET'] = [];
        pending['POST'] = [];
        pending['PUT'] = [];
        pending['DELETE'] = [];

        service.get = _get
        service.add = _add
        service.remove = _remove
        service.cancelAll = _cancelAll
        service.isFinished = _isFinished
        service.isLoaderFinished = _isLoaderFinished

        return service;

        function _get(method) {
            return pending[method];
        };

        function _add(request, method) {
            var cancelReq = angular.copy(pending[method].filter(function (p) {
                return p.url === request.url;
            }), cancelReq);

            angular.forEach(cancelReq, function (c) {
                if (c.canceller) c.canceller.resolve({});
                pending[method] = angular.copy(pending[method].filter(function (p) {
                    return p.url !== c.url;
                }), pending[method]);
            });

            // add latest request
            pending[method].push(request);
            $rootScope.hasPendingRequest = true;
        };

        function _remove(request, method) {
            pending[method] = angular.copy(pending[method].filter(function (p) {
                return p.url !== request;
            }), pending[method]);
            $rootScope.hasPendingRequest = !_isFinished();
        };

        function _cancelAll(state, method) {
            var cancelReq = pending[method].filter(function (p) {
                return p.state !== state;
            });

            angular.forEach(cancelReq, function (p) {
                if (p.canceller) p.canceller.resolve({});
            });

            pending[method] = angular.copy(pending[method].filter(function (p) {
                return p.state == state;
            }), pending[method]);
            $rootScope.hasPendingRequest = !_isFinished();
        };

        function _isFinished() {
            return pending['GET'].length == 0
                && pending['POST'].length == 0
                && pending['PUT'].length == 0
                && pending['DELETE'].length == 0;
        };

        function _isLoaderFinished() {
            var loaderPOST = angular.copy(pending['POST'].filter(function (p) {
                return p.loader == true;
            }), pending['POST']);

            var loaderGET = angular.copy(pending['GET'].filter(function (p) {
                return p.loader == true;
            }), pending['GET']);

            return loaderPOST.length == 0
                && loaderGET.length == 0;
        };
    }
})();
