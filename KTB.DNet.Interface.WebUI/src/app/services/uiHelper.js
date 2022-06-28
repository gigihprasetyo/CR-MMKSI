(function () {
    'use strict';

    angular.module('DNet.services.uiHelper', [])
        .service('UIHelper', UIHelper);

    function UIHelper() {
        var service = {};
        var vm = {}

        service.convertTableStateToSearchParams = _convertTableStateToSearchParams
        service.toastrCustom = _toastrCustom()

        return service;

        function _convertTableStateToSearchParams(tableState) {
            var dataTablePostModel = {
                Start: 0,
                Length: 10,
                Search: ""
            };

            dataTablePostModel.Start = tableState.pagination.start || 0;
            dataTablePostModel.Length = tableState.pagination.number || 10

            if (tableState.sort.predicate !== undefined) {
                dataTablePostModel.Order = {
                    "column": tableState.sort.predicate,
                    "dir": tableState.sort.reverse ? 'desc' : 'asc'
                }
            }
            if (tableState.search.predicateObject !== undefined) {
                dataTablePostModel.Search = tableState.search.predicateObject.$
            }

            return dataTablePostModel
        }

        function _toastrCustom() {
            return {
                error: {
                    closeButton: true,
                    timeOut: 5000,
                    autoDismiss: false,
                    allowHtml: true,
                    containerId: 'toast-container',
                    extendedTimeOut: 2000,
                    maxOpened: 0,
                    newestOnTop: true,
                    preventDuplicates: false,
                    preventOpenDuplicates: true,
                    target: 'body',
                    tapToDismiss: false,
                    progressBar: false,
                    // title: '<b>Something went wrong.</b>'
                }
            }
        }
    }
})();
