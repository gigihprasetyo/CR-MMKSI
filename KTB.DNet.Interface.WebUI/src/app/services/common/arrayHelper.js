(function () {
    'use strict';

    angular.module('DNet.services.arrayHelper', [])
        .service('ArrayHelper', ArrayHelper);

    function ArrayHelper() {

        // get item on array of object based on its property
        this.getItemByProperty = function (prop, propVal, arr) {

            if (arr != null && arr != undefined && arr.length > 0) {
                if (!arr[0].hasOwnProperty(prop)) { return null; }

                for (var i in arr) {
                    if (arr[i][prop] == propVal) {
                        return arr[i];
                    }
                }
            }

            return null;
        }

        // remove item on array of object based on its property
        this.removeItemByProperty = function (prop, propVal, arr) {
            if (arr.length > 0) {
                if (!arr[0].hasOwnProperty(prop)) { return; }
            }

            for (var i in arr) {
                if (arr[i][prop] == propVal) {
                    arr.splice(i, 1);
                }
            }

        }

        // remove subset from array of object
        this.removeSubset = function (arr, subset, arrayKey, subsetKey) {
            var result = [];

            if (subsetKey == null || subsetKey == undefined) {
                subsetKey = arrayKey;
            }

            for (var i in arr) {
                var existItem = this.getItemByProperty(subsetKey, arr[i][arrayKey], subset);
                if (!(existItem != null && existItem != undefined)) {
                    result.push(arr[i]);
                }
            }
            return result;
        }

        // get property value
        this.getPropertyValue = function(obj, propertyName){
            var listOfName = propertyName.split(".");

            if (listOfName.length > 1) {
                if (obj[listOfName[0]]) {
                    return this.getPropertyValue(obj[listOfName[0]], propertyName.replace(listOfName[0] + ".", ""));
                }

                return null;
            }
            else {
                return obj[propertyName];
            }
        }

        // get array of property's object from array of object
        this.getArrayOfProperty = function (arr, prop) {
            var result = [];

            for (var i in arr) {
                var value = this.getPropertyValue(arr[i], prop);
                if (value != null && value != undefined) {
                    result.push(value);
                }
            }
            return result;
        }
    }
})();
