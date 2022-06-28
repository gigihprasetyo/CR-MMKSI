(function () {
    'use strict';

    angular.module('DNet.services.endpointSchedule', [])
        .service('EndpointScheduleService', EndpointScheduleService);

    EndpointScheduleService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function EndpointScheduleService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.endpointSchedule;
        service.Search = _search;
        service.GetUnassignedSchedule = _getUnassignedSchedule;
        service.AddBulkEndpointSchedule = _addBulkEndpointSchedule;
        service.GetOptions = _getOptions;
        service.SearchScheduled = _searchScheduled;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        /* public methods */
        function _search($params, endpointId) {
            var url = this.endpoint.search + endpointId;
            return RequestHandler.post(url, $params, false);
        }

        function _getUnassignedSchedule(endpointId) {
            var url = this.endpoint.getUnassignedSchedule + endpointId;
            return RequestHandler.get(url, true);
        }

        function _addBulkEndpointSchedule($params) {
            var url = this.endpoint.addBulkEndpointSchedule;
            return RequestHandler.post(url, $params);
        }

        function _getOptions() {
            var url = this.endpoint.getOptions;
            return RequestHandler.get(url, true);
        }

        function _searchScheduled($params) {
            var url = this.endpoint.searchScheduled;
            return RequestHandler.post(url, $params, false);
        }

    }

})();
