(function (window, document) {
    angular.module('angular.radio', []).directive('inputRadio', function ($timeout) {
		return {
			restrict: 'A',
			require: '?ngModel',
			link: function ($scope, $element, $attrs, $ngModel) {
				$ngModel = $ngModel || {
					"$setViewValue": angular.noop
				}
				$ngModel.$render = function () {
				    if ($ngModel.$modelValue == undefined) {
				        $ngModel.$setViewValue($($element).val());
				    } else {
				        $ngModel.$setViewValue($ngModel.$modelValue);
				    }

				    if ($ngModel.$modelValue == $($element).val()) {
                        $($element).parent(".iradio_square-green").addClass("checked");
					} else {
				        $($element).parent(".iradio_square-green").removeClass("checked");
					}
				}
				$($element).iCheck({
					labelHover: false,
					cursor: true,
					checkboxClass: 'icheckbox_square-green',
					radioClass: 'iradio_square-green',
					increaseArea: '20%'
				}).on('ifClicked', function (event) {
				    $ngModel.$setViewValue($($element).val());
				});
			}
		};
	});
})(window, document);