(function () {
    'use strict';

    angular.module('DNet.services.requestHandler', [])
        .service('RequestHandler', RequestHandler);

    RequestHandler.$inject = ['$rootScope', '$http', '$q', '$state', 'ResponseHandler', 'PendingRequest', 'ModalHelper'];
    function RequestHandler($rootScope, $http, $q, $state, ResponseHandler, PendingRequest, ModalHelper) {
        var _headers = {
            'Content-Type': 'application/json'
        }

        var service = {};

        service.get = _get
        service.getFile = _getFile
        service.post = _post
        service.put = _put
        service.delete = _delete

        return service;

        function _get(url, loader = false) {
            var defer = $q.defer();
            PendingRequest.add({
                url: url,
                state: $state.current.name,
                canceller: defer,
                loader: loader
            }, 'GET');
            if (loader) ModalHelper.showProgressBar();
            $http({
                method: 'GET',
                url: url,
                headers: _headers,
                timeout: defer.promise
            }).then(function success(response) {
                ResponseHandler.success(response, defer)
            }, function error(response) {
                ResponseHandler.error(response, defer)
            }).finally(function () {
                PendingRequest.remove(url, 'GET');
                if (loader) ModalHelper.hideProgressBar();
            });
            return defer.promise;
        }

        function _getFile(url, filename, loader = false) {
            var defer = $q.defer();
            PendingRequest.add({
                url: url,
                state: $state.current.name,
                canceller: defer,
                loader: loader
            }, 'GET');
            if (loader) ModalHelper.showProgressBar();            
            $http({
                method: 'GET',
                url: url,
                headers: _headers,
                responseType: "blob",
                timeout: defer.promise
            }).then(function success(response) {
                var url1 = []
                // window.open(url, '_blank', '');
                ResponseHandler.successFile(response, filename, defer)
            }, function error(response) {
                ResponseHandler.error(response, defer)
            }).finally(function () {
                PendingRequest.remove(url, 'GET');
                if (loader) ModalHelper.hideProgressBar();
            });
            return defer.promise;
        }

        // function _getFile(url, loader = false) {
        //     var deferred = $q.defer();
        //     $http.get(url).success(function (results) {
        //         window.open(url, '_self', '');
        //         deferred.resolve(results);
        //     }).error(function (data, status, headers, config) {
        //         deferred.reject('Failed generate pdf');
        //     });
    
        //     return deferred.promise;
        // };

        function _post(url, data, loader = true) {
            var defer = $q.defer();
            PendingRequest.add({
                url: url,
                state: $state.current.name,
                canceller: defer,
                loader: loader
            }, 'POST');
            if (loader) ModalHelper.showProgressBar();
            $http({
                method: 'POST',
                url: url,
                data: data,
                headers: _headers
            }).then(function success(response) {
                ResponseHandler.transSuccess(response, defer)
            }, function error(response) {
                ResponseHandler.error(response, defer)
            }).finally(function () {
                PendingRequest.remove(url, 'POST');
                if (loader) ModalHelper.hideProgressBar();
            });
            return defer.promise;
        }

        function _put(url, data) {
            var defer = $q.defer();
            $http({
                method: 'PUT',
                url: url,
                data: data,
                headers: _headers
            }).then(function success(response) {
                ResponseHandler.transSuccess(response, defer)
            }, function error(response) {
                ResponseHandler.error(response, defer)
            });
            return defer.promise;
        }

        function _delete(url) {
            var defer = $q.defer();
            ModalHelper.showProgressBar();
            $http({
                method: 'DELETE',
                url: url,
                headers: _headers
            }).then(function success(response) {
                ResponseHandler.transSuccess(response, defer)
            }, function error(response) {
                ResponseHandler.error(response, defer)
            }).finally(function () {
                ModalHelper.hideProgressBar();
            });;
            return defer.promise;
        }
    }

})();
