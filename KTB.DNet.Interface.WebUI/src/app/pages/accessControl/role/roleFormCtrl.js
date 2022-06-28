
(function () {
  'use strict';

  angular.module('DNet.pages.accessControl')
    .controller('roleFormCtrl', roleFormCtrl)

  roleFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'RoleService'];
  function roleFormCtrl($scope, $state, $stateParams, RoleService) {
    var vm = $scope
    vm.role = {}
    if ($stateParams.id) {
      RoleService.GetById($stateParams.id)
        .then(function (response) {
          vm.role = response.Data;
        }, function (error) {
        });
    }

    vm.Submit = function () {
      if (vm.role.Id == null) {
        RoleService.Create(vm.role)
          .then(function (response) {
            $state.go('accessControl.role.list')
          }, function (error) {
          });
      } else {
        RoleService.Update(vm.role)
          .then(function (response) {
              $state.go('accessControl.role.list')
          }, function (error) {
          });
      }
      vm.editmode = 0;
    }
  }

})();