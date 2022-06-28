(function () {
    'use strict';

    angular.module('DNet.services.deployment', [])
        .service('DeploymentService', DeploymentService);

    DeploymentService.$inject = ['Endpoint', 'RequestHandler'];
    function DeploymentService(Endpoint, RequestHandler) {
        var service = {};
        service.endpoint = Endpoint.deployment;
        service.GetJenkinsJobs = _getJenkinsJobs;
        service.Deploy = _deploy;
        service.Output = _output;
        service.RestartJenkins = _restartJenkins;
        return service;

        /* public methods */
        function _getJenkinsJobs() {
            var url = this.endpoint.getJenkinsJobs;
            return RequestHandler.get(url, true);
        }
        function _deploy($model) {
            var url = this.endpoint.deploy;
            return RequestHandler.post(url, $model);
        }
        function _output($model) {
            var url = this.endpoint.output;
            return RequestHandler.post(url, $model, false);
        }

        function _restartJenkins() {
            var url = this.endpoint.restartJenkins;
            return RequestHandler.get(url, true);
        }

    }

})();
