'use strict';

angular.module('angular.laydate', []).directive('inputDate', ['$timeout', function ($timeout) {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function ($scope, $element, $attrs, $ngModel) {
            $ngModel = $ngModel || {
                "$setViewValue": angular.noop
            }
            $ngModel.$render = function () {
                $ngModel.$setViewValue($ngModel.$modelValue);
                $timeout(function () {
                    $($element).parent().find("input").val($ngModel.$modelValue);
                });
            }
            $timeout(function () {
                var id = 'laydate' + new Date().getTime() + Math.floor(Math.random() * 16.0).toString(16) + $($element).attr('data-id');
                $($element).attr('id', id);
                //外部js调用
                laydate({
                    elem: '#' + id, //目标元素。由于laydate.js封装了一个轻量级的选择器引擎，因此elem还允许你传入class、tag但必须按照这种方式 '#id .class'
                    format: $attrs.format,
                    max: $attrs.max,
                    min: $attrs.min,
                    event: 'focus' //响应事件。如果没有传入event，则按照默认的click
                });
                $timeout(function () {
                    $('#' + id).on('focus', function () {
                        $('#laydate_table td').unbind('click').on('click',function () {
                            $ngModel.$setViewValue($('#' + id).val());
                        });
                        $('#laydate_clear,#laydate_today,#laydate_ok').unbind('click').on('click', function () {
                            $ngModel.$setViewValue($('#' + id).val());
                        });
                    });
                    
                });
               
            });
        }
    }
}]);