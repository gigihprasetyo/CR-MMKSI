(function () {
    'use strict';

    angular.module('DNet.services.schedule', [])
        .service('ScheduleService', ScheduleService);

    ScheduleService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler'];
    function ScheduleService(Endpoint, BaseHTTPService, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.schedule;
        service.GetOptions = _getOptions;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;

        function _getOptions() {
            var url = this.endpoint.getOptions;
            return RequestHandler.get(url);
        }
    }

})();
