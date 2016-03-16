(function (window, document) {
    angular.module('angular.switchery', []).directive('inputSwitchery', function ($timeout) {
        return {
            restrict: 'A',
            require: '?ngModel',
            link: function ($scope, $element, $attrs, $ngModel) {
                $ngModel = $ngModel || {
                    "$setViewValue": angular.noop
                }
                $ngModel.$render = function () {
                    if ($ngModel.$viewValue === false || $ngModel.$viewValue === 0) {
                        $($element).find(".switchery").remove();
                        $($element).html("<input type='checkbox' checked class='js-switch_2'/>");
                        $timeout(function () {
                            Switchery($($element).find('input')[0], {
                                color: '#ED5565'
                            });
                        });

                        if ($ngModel.$viewValue === false) {
                            $ngModel.$setViewValue(false);
                        }
                        if ($ngModel.$viewValue === 0) {
                            $ngModel.$setViewValue(0);
                        }
                    } else {
                        $($element).find(".switchery").remove();
                        $($element).html("<input type='checkbox' class='js-switch_2'/>");
                        $timeout(function () {
                            Switchery($($element).find('input')[0], {
                                color: '#ED5565'
                            });
                        });
                        if ($ngModel.$viewValue === true) {
                            $ngModel.$setViewValue(true);
                        }
                        if ($ngModel.$viewValue === 1) {
                            $ngModel.$setViewValue(1);
                        }
                    }
                }

                $($element).append("<input type='checkbox' class='js-switch_2'/>");

                $timeout(function () {
                    $($element).bind("click", function () {

                        if ($ngModel.$viewValue === true) {
                            $ngModel.$setViewValue(false);
                        } else if ($ngModel.$viewValue === false) {
                            $ngModel.$setViewValue(true);
                        }

                        if ($ngModel.$viewValue === 1) {
                            $ngModel.$setViewValue(0);
                        }else if ($ngModel.$viewValue === 0) {
                            $ngModel.$setViewValue(1);
                        }

                    });
                });
            }
        };
    });
})(window, document);