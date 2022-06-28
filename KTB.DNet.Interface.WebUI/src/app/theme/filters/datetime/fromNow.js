/**
 * @author a.demeshko
 * created on 23.12.2015
 */
(function () {
    'use strict';

    angular.module('DNet.theme')
        .filter('fromNow', function () {
            function fromNow(value) {
                if (value == null) {
                    return 'Never';
                }
                else {
                    return moment(value).fromNow();
                }
            }
            fromNow.$stateful = true;
            return fromNow;
        });
})();