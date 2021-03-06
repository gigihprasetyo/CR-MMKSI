/**
 * Created by k.danovsky on 13.05.2016.
 */

(function () {
  'use strict';

  angular.module('DNet.theme')
    .config(config);

  /** @ngInject */
  config.$inject = ['baConfigProvider', 'colorHelper', '$provide'];
  function config(baConfigProvider, colorHelper, $provide) {
    $provide.decorator('$uiViewScroll', uiViewScrollDecorator);
    //baConfigProvider.changeTheme({blur: true});
    
    //baConfigProvider.changeColors({
    //  default: 'rgba(#000000, 0.2)',
    //  defaultText: '#ffffff',
    //  dashboard: {
    //    white: '#ffffff',
    //  },
    //});
  }

  /** @ngInject */
  uiViewScrollDecorator.$inject = ['$delegate', '$anchorScroll', 'baUtil'];
  function uiViewScrollDecorator($delegate, $anchorScroll, baUtil) {
    return function (uiViewElement) {
      if (baUtil.hasAttr(uiViewElement, "autoscroll-body-top")) {
        $anchorScroll();
      } else {
        $delegate(uiViewElement);
      }
    };
  }
})();
