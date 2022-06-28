'use strict';

angular.lowercase = function (text) {
    return text !== undefined ? text.toLowerCase() : '';
}

var app = angular.module('DNet', [
  'ngAnimate',
  'ui.bootstrap',
  'ui.sortable',
  'ui.router',
  'ngTouch',
  'toastr',
  'smart-table',
  "xeditable",
  'ui.slimscroll',
  'ngJsTree',
  'angular-progress-button-styles',
  'ngFileUpload',
  'DNet.theme',
  'DNet.pages',
  'DNet.services',
  'DNet.endpoints'
])
