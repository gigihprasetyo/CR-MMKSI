(function () {
    'use strict';

    angular.module('DNet.pages.directives')
        .directive('multiSelectJquery', function () {
            var selectCtrl = ['$rootScope', '$scope', function ($rootScope, $scope) {
                $rootScope.$watch('hasPendingRequest', function (newValue, oldValue) {
                    $scope.hasPendingRequest = $rootScope.hasPendingRequest;
                });
                $scope.query1 = '';
                $scope.query2 = '';
                $scope.optionChaged = true;

                $scope.$watch('options', function (newValue, oldValue) {
                    if ($scope.element) {
                        $scope.optionChaged = true;
                        setTimeout(function () {
                            $('#' + $scope.element[0].id).multiSelect('refresh');
                        }, 0)

                    }
                });

                $scope.$watch('ngModel', function (newValue, oldValue) {
                    if ($scope.element && $scope.optionChaged) {
                        $scope.optionChaged = false;
                        setTimeout(function () {
                            $('#' + $scope.element[0].id).multiSelect('refresh');
                        }, 0)
                    }
                });

                $scope.selectAll = function () {
                    if ($scope.element) {
                        setTimeout(function () {
                            $('#' + $scope.element[0].id).multiSelect('select_all');
                        }, 0)

                    }
                }

                $scope.deselectAll = function () {
                    if ($scope.element) {
                        setTimeout(function () {
                            $('#' + $scope.element[0].id).multiSelect('deselect_all');
                        }, 0)

                    }
                }

                $scope.element = undefined;
                $scope.msOptions2 = {
                    selectableHeader: "<input id='selectableHeader-" + $scope.name + "' type='text' class='form-control search-input' autocomplete='off' placeholder='Search available options...'>",
                    selectionHeader: "<input id='selectionHeader-" + $scope.name + "' type='text' class='form-control search-input' autocomplete='off' placeholder='Search selected options...'>",
                    afterInit: function (ms) {
                        var that = this,
                            $selectableSearch = that.$selectableUl.prev(),
                            $selectionSearch = that.$selectionUl.prev(),
                            selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                            selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

                        that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                            .on('keydown', function (e) {
                                if (e.which === 40) {
                                    that.$selectableUl.focus();
                                    return false;
                                }
                            }).on('keyup', function (e) {
                                $scope.query1 = $(this).val()
                                $scope.$apply()
                            });

                        that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                            .on('keydown', function (e) {
                                if (e.which == 40) {
                                    that.$selectionUl.focus();
                                    return false;
                                }
                            }).on('keyup', function (e) {
                                $scope.query2 = $(this).val()
                                $scope.$apply()
                            })
                        that.$element.data('multiSelect', that);
                        $scope.element = that.$element;
                    },
                    afterSelect: function () {
                        $('#' + this.qs1[0].id).val($scope.query1)
                        $('#' + this.qs2[0].id).val($scope.query2)
                        $scope.form[$scope.name].$setTouched()
                        $scope.$apply()

                        this.qs1.cache();
                        this.qs2.cache();
                    },
                    afterDeselect: function () {
                        $('#' + this.qs1[0].id).val($scope.query1)
                        $('#' + this.qs2[0].id).val($scope.query2)
                        $scope.form[$scope.name].$setTouched()
                        $scope.$apply()

                        this.qs1.cache();
                        this.qs2.cache();
                    }
                };
            }];
            return {
                require: '^form',
                restrict: 'E',
                templateUrl: 'src/app/pages/directives/forms/multiSelectJquery/multiSelect.jquery.html',
                controller: selectCtrl,
                scope: {
                    ngModel: "=",
                    required: "@",
                    ngDisabled: "@",
                    allButton: "@",
                    label: "@",
                    name: "@",
                    options: "=options",
                    value: "@",
                    text: "@",
                },
                // replace: true,
                link: function (scope, elem, attrs, form) {
                    scope.form = form;
                }
            };
        })
})();