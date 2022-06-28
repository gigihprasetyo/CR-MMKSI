(function () {
    'use strict';

    angular.module('DNet.services.clientRolePermission', [])
        .service('ClientRolePermissionService', ClientRolePermissionService);

    ClientRolePermissionService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function ClientRolePermissionService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.clientRolePermission;
        service.GetClientOptions = _getClientOptions;
        service.GetSelectedPermissionIdsByClientRoleId = _geSelectedPermissionIdsByClientRoleId;
        service.GetRolesByClientId = _getRolesByClientId;
        service.GetAllPermissionsByClientId = _getAllPermissionsByClientId;
        service.UpdateClientRole = _updateClientRole
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _getClientOptions() {
            var url = this.endpoint.getClientOptions;
            return RequestHandler.get(url);
        }

        function _geSelectedPermissionIdsByClientRoleId(clientRoleId) {
            var url = this.endpoint.getSelectedPermissionIdsByClientRoleId + clientRoleId;
            return RequestHandler.get(url);
        }

        function _getRolesByClientId(clientId) {
            var url = this.endpoint.getRolesByClientId + clientId;
            return RequestHandler.get(url)
        }

        function _getAllPermissionsByClientId(clientId) {
            var url = this.endpoint.getAllPermissionsByClientId + clientId;
            return RequestHandler.get(url)
        }

        function _updateClientRole($entity) {
            var url = this.endpoint.updateClientRole;
            return RequestHandler.post(url, $entity);
        }
    }

})();
