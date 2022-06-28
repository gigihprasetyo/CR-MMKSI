(function () {
  'use strict';

  angular.module('DNet.theme.components')
    .controller('baWizardCtrl', baWizardCtrl);

  /** @ngInject */
  baWizardCtrl.$inject = ['$scope'];
  function baWizardCtrl($scope) {
    var vm = this;
    vm.tabs = [];

    vm.tabNum = 0;
    vm.progressPrev = 0;
    vm.progress = 0;

    vm.addTab = function (tab) {
      tab.setPrev(vm.tabs[vm.tabs.length - 1]);
      vm.tabs.push(tab);
      vm.selectTab(0);
      calcProgress()
    };

    $scope.$watch(angular.bind(vm, function () { return vm.tabNum; }), calcProgress);

    vm.selectTab = function (tabNum) {
      vm.tabs[vm.tabNum].submit();
      if (vm.tabs[tabNum].isAvailiable()) {
        vm.tabNum = tabNum;
        vm.tabs.forEach(function (t, tIndex) {
          tIndex == vm.tabNum ? t.select(true) : t.select(false);
        });
      }
    };

    vm.isFirstTab = function () {
      return vm.tabNum == 0;
    };

    vm.hideNav = function () {
      return vm.tabs[vm.tabNum] === undefined ? true : vm.tabs[vm.tabNum].hideNav !== "";
    };

    vm.nextText = function () {
      return vm.tabs[vm.tabNum] === undefined ? "next" : vm.tabs[vm.tabNum].nextText !== undefined ? vm.tabs[vm.tabNum].nextText : 'next';
    };

    vm.isLastTab = function () {
      return vm.tabNum == vm.tabs.length - 1;
    };

    vm.nextTab = function () {
      vm.selectTab(vm.tabNum + 1)
    };

    vm.previousTab = function () {
      vm.selectTab(vm.tabNum - 1)
    };

    function calcProgress() {
      vm.progressPrev = ((vm.tabNum) / vm.tabs.length) * 100;
      vm.progress = ((vm.tabNum + 1) / vm.tabs.length) * 100;
    }
  }
})();

