
(function () {
    'use strict';

    angular.module('DNet.pages.scheduler', [])
        .config(routeConfig);

    /** @ngInject */
    routeConfig.$inject = ['$stateProvider'];
    function routeConfig($stateProvider) {
        $stateProvider
            .state('scheduler', {
                url: '/scheduler',
                permissionCodes: ["WebUI_Schedule_Read", "WebUI_EndpointSchedule_Read"],
                abstract: true,
                module: true,
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                title: 'Scheduler',
                sidebarMeta: {
                    icon: 'ion-android-list',
                    order: 50,
                },
            })
            .state('scheduler.schedule', {
                url: '/schedule',
                permissionCodes: ["WebUI_Schedule_Read"],
                abstract: true,
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                title: 'Schedule',
                controller: 'scheduleCtrl',
                sidebarMeta: {
                    order: 501,
                }
            })

            .state('scheduler.schedule.list', {
                url: '',
                templateUrl: 'src/app/pages/scheduler/schedule/schedule.html',
                title: 'Schedules',
                controller: 'scheduleCtrl',
                breadchrumb: {
                    text: 'List'
                }
            })

            .state('scheduler.schedule.create', {
                url: '/create',
                templateUrl: 'src/app/pages/scheduler/schedule/scheduleForm.html',
                title: 'Create Schedule',
                controller: 'scheduleFormCtrl',
                breadchrumb: {
                    text: 'Create'
                }
            })

            .state('scheduler.schedule.update', {
                url: '/:id',
                templateUrl: 'src/app/pages/scheduler/schedule/scheduleForm.html',
                title: 'Update Schedule',
                controller: 'scheduleFormCtrl',
                breadchrumb: {
                    text: 'Update'
                }

            })
            .state('scheduler.endpointSchedule', {
                url: '/endpointSchedule',
                permissionCodes: ["WebUI_EndpointSchedule_Read"],
                templateUrl: 'src/app/pages/scheduler/endpointSchedules/endpointSchedule.html',
                title: 'Endpoint Schedule',
                controller: 'endpointScheduleCtrl',
                sidebarMeta: {
                    order: 502,
                }
            })
            ;
    }

})();
