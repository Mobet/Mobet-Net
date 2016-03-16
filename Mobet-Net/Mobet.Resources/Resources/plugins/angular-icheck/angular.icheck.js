(function (window, document) {
	angular.module('angular.icheck', []).directive('inputIcheck', function ($timeout) {
		return {
			restrict: 'A',
			require: '?ngModel',
			link: function ($scope, $element, $attrs, $ngModel) {
				$ngModel = $ngModel || {
					"$setViewValue": angular.noop
				}
				$ngModel.$render = function () {
					if ($ngModel.$modelValue == undefined) {
						$ngModel.$setViewValue(false);
					} else {
						$ngModel.$setViewValue($ngModel.$modelValue);
					}
					if ($ngModel.$modelValue == true) {
						$($element).find(".icheckbox_square-green").addClass("checked");
					} else {
						$($element).find(".icheckbox_square-green").removeClass("checked");
					}
				}
				$($element).iCheck({
					labelHover: false,
					cursor: true,
					checkboxClass: 'icheckbox_square-green',
					radioClass: 'iradio_square-green',
					increaseArea: '20%'
				}).on('ifClicked', function (event) {
					$ngModel.$setViewValue(!$ngModel.$modelValue);
					$timeout(function () {
						if (!$ngModel.$modelValue) {
							$($element).find(".icheckbox_square-green").addClass("checked");
						} else {
							$($element).find(".icheckbox_square-green").removeClass("checked");
						}
					});

				});
			}
		};
	});
})(window, document);