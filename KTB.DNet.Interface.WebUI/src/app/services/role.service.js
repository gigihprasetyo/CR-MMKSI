(function () {
    'use strict';

    angular.module('DNet.services.role', [])
        .service('RoleService', RoleService);

    RoleService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function RoleService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.role;
        service.GetRolesByClientId = _getRolesByClientId;

        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _getRolesByClientId(clientId) {
            var url = this.endpoint.getRolesByClientId + clientId;
            return RequestHandler.get(url);
        }

    }

})();
