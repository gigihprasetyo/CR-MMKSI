(function () {
    'use strict';

    angular.module('DNet.services.userActivity', [])
        .service('UserActivityService', UserActivityService);

    UserActivityService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function UserActivityService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.userActivity;
        service.GetOptions = _getOptions;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        function _getOptions() {
            var url = this.endpoint.getOptions;
            return RequestHandler.get(url);
        }
    }

})();
