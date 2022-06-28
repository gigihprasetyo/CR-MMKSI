/**
 * @author v.lugovsky
 * created on 16.12.2015
 */
(function () {
    'use strict';

    angular.module('DNet.services', [
      'ui.router',
      'ngCookies',
      'DNet.services.baseHttp',
      'DNet.services.uiHelper',
      'DNet.services.modalHelper',
      'DNet.services.arrayHelper',
      'DNet.services.requestHandler',
      'DNet.services.responseHandler',
      'DNet.services.pendingRequest',
      
      'DNet.services.auth',
      'DNet.services.client',
      'DNet.services.dashboard',
      'DNet.services.user',
      'DNet.services.clientUser',
      'DNet.services.role',
      'DNet.services.endpointPermission',
      'DNet.services.endpointSchedule',
      'DNet.services.throttler',
      'DNet.services.threadLog',
      'DNet.services.application',
      'DNet.services.appVersion',
      'DNet.services.systemConfig',
      'DNet.services.clientRolePermission',
      'DNet.services.schedule',
      'DNet.services.deployment',
      'DNet.services.rollback',
      'DNet.services.errorLog',
      'DNet.services.transaction',
      'DNet.services.userActivity',
      'DNet.services.standardCode',
      'DNet.services.standardCodeChar'
    ])

})();
