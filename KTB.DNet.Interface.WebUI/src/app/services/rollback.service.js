(function () {
    'use strict';

    angular.module('DNet.services.rollback', [])
        .service('RollbackService', RollbackService);

    RollbackService.$inject = ['Endpoint', 'RequestHandler'];
    function RollbackService(Endpoint, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.rollback;
        service.GetRollbackData = _getRollbackData
        service.Run = _run;

        return service;

        /* public methods */
        function _getRollbackData() {
            var url = this.endpoint.getRollbackData;
            return RequestHandler.get(url, true);
        }

        function _run($model) {
            var url = this.endpoint.run;
            return RequestHandler.post(url, $model);
        }
    }

})();
