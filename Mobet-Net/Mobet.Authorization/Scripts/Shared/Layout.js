(function () {
    'use strict';

    angular.module('app').directive('layoutHeader', function () {
        return {
            templateUrl: "/Layout/Header",
            link: function (scope, element, attr) {

                // Minimalize menu when screen is less than 768px
                $(window).bind("resize", function () {
                    if ($(this).width() < 769) {
                        $('body').addClass('body-small')
                    } else {
                        $('body').removeClass('body-small')
                    }
                });

                // Minimalize menu
                $('.navbar-menu-button').click(function () {
                    MinimalizeMenu();
                });
            }
        };
    });

    angular.module('app').directive('layoutFooter', function () {
        return {
            templateUrl: "/Layout/Footer",
            link: function (scope, element, attr) {
                //element.addClass("dhxlayout_sep");
            }
        };
    });

    angular.module('app').directive('layoutMain', function () {
        return {
            templateUrl: "/Layout/Main",
            link: function (scope, element, attrs) {
                //scope.creator.createLayout();
            }
        };
    });

    angular.module('app').directive('layoutSetting', function () {
        return {
            templateUrl: "/Layout/Settings",
            link: function (scope, el, attr) {
                //app.handleBootstrapSwitch(el);
                //app.handleTemplateSetting();
            }
        };
    });

    angular.module('app').directive('layoutIntercom', function () {
        return {
            templateUrl:  "/Layout/Intercom",
            link: function (scope, el, attr) {
                $('.intercom-sheet-header-close-button,.intercom-launcher-button').click(function () {
                    $('.intercom-messenger').toggle();
                });
            }
        };
    });

    angular.module('app').directive('layoutNavbar', function () {
        return {
            templateUrl: "/Layout/Navbar",
            link: function (scope, el, attr) {
                //app.handleBootstrapSwitch(el);
                //app.handleFormChat(scope);

                $('#side-menu').metisMenu();
            }
        };
    });

    var MinimalizeMenu = function () {
        $('.navbar-menu').toggleClass('navbar-menu-mini');
        $("body").toggleClass("mini-navbar");

        if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
            // Hide menu in order to smoothly turn on when maximize menu
            $('#side-menu').hide();
            // For smoothly turn on menu
            setTimeout(
                function () {
                    $('#side-menu').fadeIn(500);
                }, 100);
        } else if ($('body').hasClass('fixed-sidebar')) {
            $('#side-menu').hide();
            setTimeout(
                function () {
                    $('#side-menu').fadeIn(500);
                }, 300);
        } else {
            // Remove all inline style from jquery fadeIn function to reset menu state
            $('#side-menu').removeAttr('style');
        }
    }
})();