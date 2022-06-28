(function () {
    'use strict';

    angular.module('DNet.services.application', [])
        .service('ApplicationService', ApplicationService);

    ApplicationService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function ApplicationService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.application;
        service.GetPermissionOptions = _getPermissionOptions;
        service.GetJenkinsJobOptions = _getJenkinsJobOptions;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        function _getPermissionOptions() {
            var url = this.endpoint.getPermissions;
            return RequestHandler.get(url);
        }

        function _getJenkinsJobOptions() {
            var url = this.endpoint.getJenkinsJobOptions;
            return RequestHandler.get(url);
        }
    }

})();
