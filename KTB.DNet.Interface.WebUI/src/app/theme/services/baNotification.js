/**
 * @author irfan s
 * created on 27.06.2016
 */
(function () {
    'use strict';

    angular.module('DNet.theme')
        .factory('baNotification', baNotification);

    /** @ngInject */
    baNotification.$inject = ['$rootScope'];
    function baNotification($rootScope) {
        var lsName = window.location.hostname + '.mmksi.webui.notifications';
        var notif = getSavedNotification(lsName)
        return {
            addNotification: function (message, success) {
                notif = getSavedNotification(lsName)
                notif.unshift({
                    read: $rootScope.windowFocused,
                    success: success,
                    message: message,
                    time: new Date().toISOString()
                });
                localStorage.setItem(lsName, JSON.stringify(notif));
            },
            getNotification: function () {
                return notif;
            },
            clearAll: function () {
                notif = [];
                localStorage.setItem(lsName, JSON.stringify([]));
            }
        };
    }

    function getSavedNotification(lsName){
         var dnetnotifs = localStorage.getItem(lsName);
         var result = dnetnotifs !== null ? JSON.parse(dnetnotifs) : [];
        return result;
    }

})();