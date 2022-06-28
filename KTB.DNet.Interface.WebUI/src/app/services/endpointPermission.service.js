(function () {
    'use strict';

    angular.module('DNet.services.endpointPermission', [])
        .service('EndpointPermissionService', EndpointPermissionService);

    EndpointPermissionService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function EndpointPermissionService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.endpointPermission;
        service.getAll = _getAll;
        service.GetAllPermission = _getAllPermission;
        service.GetOptions = _getOptions;
        service.GetAllPermissionsByClientId = _getAllPermissionsByClientId;
        service.GetEndpointGroupOptions = _getEndpointGroupOptions;
        service.GetEndpointsByEndpointGroup = _getEndpointsByEndpointGroup;
        service.GetEndpointsByEndpointType = _getEndpointsByEndpointType;
        service.GetEndpointsByOperationType = _getEndpointsByOperationType;
        service.GetUnselectedEndpointsByEndpointGroup = _getUnselectedEndpointsByEndpointGroup;
        service.SaveEndpointPermissionGroup = _saveEndpointPermissionGroup;
        service.SaveEndpointType = _saveEndpointType;
        service.SaveOperationType = _saveOperationType;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _getOptions() {
            var url = this.endpoint.getOptions;
            return RequestHandler.get(url);
        }

        function _getAll() {
            var url = this.endpoint.getAll;
            return RequestHandler.get(url);
        }

        function _getAllPermission() {
            var url = this.endpoint.getAllPermission;
            return RequestHandler.get(url);
        }

        function _getAllPermissionsByClientId(clientId) {
            var url = this.endpoint.getAllPermissionsByClientId + clientId;
            return RequestHandler.get(url);
        }

        function _getEndpointOptions() {
            var url = this.endpoint.getEndpointOptions;
            return RequestHandler.get(url);
        }

        function _getEndpointGroupOptions() {
            var url = this.endpoint.getEndpointGroupOptions;
            return RequestHandler.get(url);
        }

        function _getEndpointsByEndpointGroup(id) {
            var url = this.endpoint.getEndpointsByEndpointGroup + id;
            return RequestHandler.get(url);
        }

        function _getEndpointsByEndpointType(id) {
            var url = this.endpoint.getEndpointsByEndpointType + id;
            return RequestHandler.get(url);
        }

        function _getEndpointsByOperationType(id) {
            var url = this.endpoint.getEndpointsByOperationType + id;
            return RequestHandler.get(url);
        }

        function _getUnselectedEndpointsByEndpointGroup(id) {
            var url = this.endpoint.getUnselectedEndpointsByEndpointGroup +id;
            return RequestHandler.get(url);
        }

        function _saveEndpointPermissionGroup($entity) {
            var url = this.endpoint.saveEndpointPermissionGroup;
            return RequestHandler.post(url, $entity);
        }

        function _saveEndpointType($entity) {
            var url = this.endpoint.saveEndpointType;
            return RequestHandler.post(url, $entity);
        }

        function _saveOperationType($entity) {
            var url = this.endpoint.saveOperationType;
            return RequestHandler.post(url, $entity);
        }
    }
 
})();
