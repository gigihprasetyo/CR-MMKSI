(function () {
    'use strict';

    angular.module('DNet.pages.deployment')
      .controller('rollbackCtrl', RollbackCtrl);

    /** @ngInject */
    RollbackCtrl.$inject = ['$scope', 'ModalHelper', 'RollbackService'];
    function RollbackCtrl($scope, ModalHelper, RollbackService) {
        var vm = $scope;
        vm.rollbackData = {}
        vm.selectDir = _selectDir
        vm.deleteSelected = _deleteSelected
        vm.run = _run
        vm.selectedRollback = []
        vm.parameter = {};

        _loadData()

        function _loadData() {
            RollbackService.GetRollbackData()
              .then(function (response) {
                  vm.rollbackData = response.Data;
              }, function (error) { });
        }

        function _selectDir(env, app, dir) {
            console.log(env, app, dir);
            var param = app.toUpperCase() + '_BACKUPPATH';
            vm.parameter[env] = vm.parameter[env] === undefined ? {} : vm.parameter[env];
            vm.parameter[env][param] = { dir: dir, app: app };
        }

        function _deleteSelected(env, key) {
            delete vm.parameter[env][key];
        }

        function _run(env) {
           ModalHelper
          .showMessage(
            "Are you sure you want to rollback " + env + "?",
            "confirm")
          .then(
          function () {
              var param = {}
              angular.forEach(vm.parameter[env], function (value, key) {
                  param[key] = value.dir;
              });
              var runModel = {
                  name: env,
                  Parameter: param
              }
              RollbackService.Run(runModel)
                .then(function (response) {

                }, function (error) { });
          });
        }
    }
})();

