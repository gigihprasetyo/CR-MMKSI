(function () {
  'use strict';

  angular.module('DNet.theme.components')
    .provider('baSidebarService', baSidebarServiceProvider);

  /** @ngInject */
  function baSidebarServiceProvider() {
    var staticMenuItems = [];

    this.addStaticItem = function () {
      staticMenuItems.push.apply(staticMenuItems, arguments);
    };

    /** @ngInject */
    this.$get = ['$q','$timeout','$state', '$rootScope', 'layoutSizes', function ($q, $timeout, $state, $rootScope, layoutSizes) {
      return new _factory();

      function _factory() {
        var isMenuCollapsed = shouldMenuBeCollapsed();

        this.getMenuItems = function () {
          var defer = $q.defer()
          
          var states = defineMenuItemStates();
            var menuItems = states.filter(function (item) {
              var authorized = isAuthorized(item.permissionCodes);
              return item.level == 0 && authorized;
            });
  
            menuItems.forEach(function (item) {
              var children = states.filter(function (child) {
                var authorized = isAuthorized(child.permissionCodes);
                return child.level == 1 && child.name.indexOf(item.name) === 0 && authorized;
              });
              item.subMenu = children.length ? children : null;
            });
            defer.resolve(menuItems.concat(staticMenuItems));

          return defer.promise;
        };

        this.shouldMenuBeCollapsed = shouldMenuBeCollapsed;
        this.canSidebarBeHidden = canSidebarBeHidden;

        this.setMenuCollapsed = function (isCollapsed) {
          isMenuCollapsed = isCollapsed;
        };

        this.isMenuCollapsed = function () {
          return isMenuCollapsed;
        };

        this.toggleMenuCollapsed = function () {
          isMenuCollapsed = !isMenuCollapsed;
        };

        this.getAllStateRefsRecursive = function (item) {
          var result = [];
          _iterateSubItems(item);
          return result;

          function _iterateSubItems(currentItem) {
            currentItem.subMenu && currentItem.subMenu.forEach(function (subItem) {
              subItem.stateRef && result.push(subItem.stateRef);
              _iterateSubItems(subItem);
            });
          }
        };

        function isAuthorized(permissionCodes) {
          if ($rootScope.userPermissions) {
            
            if (permissionCodes) {
              for (var i in permissionCodes) {
                if ($rootScope.userPermissions.includes(permissionCodes[i])) {
                  return true;
                }
              }
              return false;
            }
          }
          return true;
        }
        function defineMenuItemStates() {
          return $state.get()
            .filter(function (s) {
              return s.sidebarMeta;
            })
            .map(function (s) {
              var meta = s.sidebarMeta;
              return {
                name: s.name,
                title: s.title,
                level: (s.name.match(/\./g) || []).length,
                order: meta.order,
                icon: meta.icon,
                stateRef: s.name,
                redirect: s.redirect,
                permissionCodes: s.permissionCodes
              };
            })
            .sort(function (a, b) {
              return (a.level - b.level) * 100 + a.order - b.order;
            });
        }

        function shouldMenuBeCollapsed() {
          return window.innerWidth <= layoutSizes.resWidthCollapseSidebar;
        }

        function canSidebarBeHidden() {
          return window.innerWidth <= layoutSizes.resWidthHideSidebar;
        }
      }

    }];

  }
})();
