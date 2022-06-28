(function () {
    'use strict';

    angular.module('DNet.services.baseHttp', [])
        .service('BaseHTTPService', BaseHTTPService);

    BaseHTTPService.$inject = ['RequestHandler'];
    function BaseHTTPService(RequestHandler) {
        var service = {};

        service.GetById = _getById
        service.GetAll = _getAll
        service.Create = _create
        service.Update = _update
        service.Delete = _delete
        service.Search = _search

        return service;

        function _getById(id) {
            var url = this.endpoint.getById + '/' + id;
            return RequestHandler.get(url, true);
        }

        function _getAll() {
            var url = this.endpoint.getAll;
            return RequestHandler.get(url);
        }

        function _create($entity) {
            var url = this.endpoint.create;
            return RequestHandler.post(url, $entity);
        }

        function _update($entity) {
            var url = this.endpoint.update;
            return RequestHandler.post(url, $entity);
        }

        function _delete(id) {
            var url = this.endpoint.delete + '/' + id;
            return RequestHandler.delete(url);
        }

        function _search($params) {
            var url = this.endpoint.search;
            return RequestHandler.post(url, $params, false);
        }
    }

})();
