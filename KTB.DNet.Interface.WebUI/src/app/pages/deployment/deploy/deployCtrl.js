(function () {
    'use strict';

    angular.module('DNet.pages.deployment')
        .controller('deployCtrl', DeployCtrl);

    /** @ngInject */
    DeployCtrl.$inject = ['$scope', '$uibModal', 'DeploymentService', 'ModalHelper', 'toastr', 'baNotification'];
    function DeployCtrl($scope, $uibModal, DeploymentService, ModalHelper, toastr, baNotification) {
        var vm = $scope;

        (function initController() {
            _loadData()
        })();

        vm.tableDefinitions = {};
        vm.RestartJenkins = _restartJenkins;

        vm.tableDefinitionTemplate = {
            name: "Deploy",
            key: "Id",
            identifier: "Name",
            customButtons: [
                {
                    action: vm.RestartJenkins,
                    text: "Restart Jenkins",
                    class: "btn btn-primary"
                }
            ],
            action: {
                customActions: [
                    { text: "Deploy", type: "primary", action: _deploy }
                ]
            },
            columns: [
                { name: "Name", label: "Job Name" },
                { name: "LastSuccessfulBuildTimestamp", label: "Last Success On", datatype: "fromnow" },
                { name: "LastSuccessfulBuildNumber", label: "Last Success #", datatype: "action", onClick: _viewOuput },
                { name: "LastFailedBuildTimestamp", label: "Last Failure On", datatype: "fromnow" },
                { name: "LastFailedBuildNumber", label: "Last Failure #", datatype: "action", onClick: _viewOuput },
                { name: "Actions", label: "Actions", action: true }
            ],
            searchable: false,
            tablePageSize: 100 // rows
        };

        function _loadData() {
            DeploymentService.GetJenkinsJobs()
                .then(function (response) {
                    angular.forEach(response.Data, function (value, key) {
                        vm.tableDefinitions[value.Name] = angular.copy(vm.tableDefinitionTemplate, vm.tableDefinitions[value.Name]);
                        vm.tableDefinitions[value.Name].dataSource = value.JenkinsJobs;
                    });
                    if(!vm.categorizedJobs) vm.categorizedJobs = response.Data;
                }, function (error) { });
        }


        function _deploy(row) {
            ModalHelper
              .showMessage(
                "Are you sure you want to deploy " + row.Name,
                "confirm")
              .then(
                function () {
                    DeploymentService.Deploy({ Name: row.Name, Parameter: {} })
                      .then(function (response) {
                          var msg = row.Name + " has been " + response.Message.toLowerCase() + ".";
                          ModalHelper.showMessage(msg, "success");
                          _loadData()
                      }, function (error) { });
                }, function () {
                    // canceled.
                }
              );
        }

        function _viewOuput(row, data) {
            DeploymentService.Output({ Name: row.Name, BuildNumber: data })
              .then(function (response) {
                  var output = response.Output;
                  // ModalHelper.showMessage(output, "info", 'lg');
                  showOuput(row.Name, data, output);
              }, function (error) { });
        }

        var ModalCtrl = ['$scope', 'output', function ($scope, output) {
            $scope.output = output;
        }];

        function showOuput(jobName, buildNo, message) {

            return $uibModal.open({
                animation: true,
                templateUrl: "src/app/pages/deployment/deploy/outputModal.html",
                size: 'lg',
                controller: ModalCtrl,
                resolve: {
                    output: function () {
                        return {
                            jobName: jobName,
                            buildNo: buildNo,
                            message: message
                        };
                    }
                }
            }).result;
        };

        function _restartJenkins() {
            DeploymentService.RestartJenkins()
                   .then(function (response) {
                       
                       if (response.Message != null || response.Message != undefined) {
                           if (response.Message != "") {
                               toastr.success(response.Message);
                               baNotification.addNotification(response.Message, true);
                           }
                       }
                       
                   }, function (error) { });
        }
    }
})();

