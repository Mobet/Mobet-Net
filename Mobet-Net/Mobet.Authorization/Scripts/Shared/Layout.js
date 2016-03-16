(function () {
    'use strict';

    angular.module('AuthorizationApp').directive('layoutHeader', function () {
        return {
            templateUrl: "/Layout/Header",
            link: function (scope, element, attr) {
                //app.handleToggle();
                //app.handleSlimScroll();
                //app.handlePulsate();
            }
        };
    });

    angular.module('AuthorizationApp').directive('layoutFooter', function () {
        return {
            templateUrl: "/Layout/Footer",
            link: function (scope, element, attr) {
                //element.addClass("dhxlayout_sep");
            }
        };
    });

    angular.module('AuthorizationApp').directive('layoutMain', function () {
        return {
            templateUrl: "/Layout/Main",
            link: function (scope, element, attrs) {
                //scope.creator.createLayout();
            }
        };
    });

    angular.module('AuthorizationApp').directive('layoutSetting', function () {
        return {
            templateUrl: "/Layout/Settings",
            link: function (scope, el, attr) {
                //app.handleBootstrapSwitch(el);
                //app.handleTemplateSetting();
            }
        };
    });

    angular.module('AuthorizationApp').directive('layoutChat', function () {
        return {
            templateUrl:  "/Layout/Chat",
            link: function (scope, el, attr) {
                //app.handleBootstrapSwitch(el);
                //app.handleFormChat(scope);
            }
        };
    });

    angular.module('AuthorizationApp').directive('layoutNavbar', function () {
        return {
            templateUrl: "/Layout/Navbar",
            link: function (scope, el, attr) {
                //app.handleBootstrapSwitch(el);
                //app.handleFormChat(scope);
            }
        };
    });
})();