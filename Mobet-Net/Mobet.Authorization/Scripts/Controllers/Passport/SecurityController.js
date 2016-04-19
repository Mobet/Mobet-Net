/**
 *  SecurityController
 *
 *  摘要：账号安全
 *
 */

(function () {
    angular.module('passport').controller('SecurityController', function ($scope, $dialogs) {

        $scope.setPassword = function () {
            dialog = $dialogs.create('/Passport/PasswordDetail', 'PasswordDetailController', null, { key: false, back: 'static' });
        };
        $scope.setEmail = function () {
            dialog = $dialogs.create('/Passport/EmailDetail', 'EmailDetailController', null, { key: false, back: 'static' });
        };
        $scope.setPhone = function () {
            dialog = $dialogs.create('/Passport/PasswordDetail', 'PasswordDetailController', null, { key: false, back: 'static' });
        };
        $scope.setQuestion = function () {
            dialog = $dialogs.create('/Passport/PasswordDetail', 'PasswordDetailController', null, { key: false, back: 'static' });
        };
        $scope._save = function () {

        }
        $scope._init = function () {

        };
    }).controller('PasswordDetailController', function ($scope, $modalInstance, data, $http, $timeout) {
        $scope.model = {};

        $scope.save = function () {
            if ($scope.model.OldPassword == "" || $scope.model.OldPassword == undefined) {
                $('.err_tip').text("请输入原密码").show();
                return false;
            }
            if ($scope.model.Password == undefined || $scope.model.Password == "" || !/^[\@@A-Za-z0-9\!\#\$\%\^\&\*\.\~]{6,22}$/.test($scope.model.Password)) {
                $('.err_tip').text("密码长度8~16位，数字、字母、字符至少包含两种").show();
                return false;
            }
            if ($scope.model.ConfirmPassword == "" || $scope.model.ConfirmPassword == undefined) {
                $('.err_tip').text("请输入确认密码").show();
                return false;
            }
            if ($scope.model.Password != $scope.model.ConfirmPassword) {
                $('.err_tip').text("两次输入的密码不一致").show();
                return false;
            }
            if ($scope.model.Captcha == "" || $scope.model.Captcha == undefined) {
                $('.err_tip').text("请输入验证码").show();
                return false;
            }
            $http.post('/Passport/SetPassword', $scope.model).success(function (data) {
                if (data.Result) {
                    $modalInstance.close($scope.model);
                } else {
                    $('.err_tip').text(data.Message).show();
                }
            });
        };
        $scope.close = function () {
            $modalInstance.dismiss('canceled');
        };

        $timeout(function () {
            $('#icode_image').click(function () {
                $(this).attr('src', $(this).attr('src') + '&timespan=' + new Date().getTime());
            });
        });
    }).controller('EmailDetailController', function ($scope, $modalInstance, data, $http, $timeout) {
        $scope.model = {};
        $scope.close = function () {
            $modalInstance.dismiss('canceled');
        };
        var timer;
        //重新发送短信验证码定时器
        function _emailCaptchaTimerStart() {
            clearInterval(timer);
            if (!$('.btn-resms').hasClass('disabled')) {
                $('.btn-resms').addClass('disabled');
            }
            var second = 60;
            timer = setInterval(function () {
                $('#wait-timer').text('(' + second-- + ')');
                if (second <= 0) {
                    $('#wait-timer').text('');
                    $('.btn-resms').removeClass('disabled');
                    clearInterval(timer);
                }
            }, 1 * 1000)
        }
        function _reSendEmail() {
            $http.post('/Passport/EmailCaptchaSendAsync', { email: $scope.model.Email }).success(function (data) {
                if (data.Result) {
                    $('.err_tip').text("我们已经向您的邮箱中重新发送了邮件，请注意查收").show();
                } else {
                    $('.err_tip').text(data.Message).show();
                }
            });
        }
        function _sendEmailAndGoNextStep() {
            $http.post('/Passport/EmailCaptchaSendAsync', { email: $scope.model.Email }).success(function (data) {
                if (data.Result) {
                    $('.step-active').hide().removeClass('step-active').next().addClass('step-active').show();
                    $('.mailtab.now').removeClass('now').next().addClass('now');
                    $('.tabline i.now').removeClass('now').next().addClass('now');

                    _emailCaptchaTimerStart();
                }
            });
        }
        function _validateEmailCaptchaAndGoNextStep() {
            $http.post('/Passport/ValidateEmailCaptchaAsync', { email: $scope.model.Email, captcha: $scope.model.EmailCaptcha }).success(function (data) {
                if (data.Result) {
                    $('.step-active').hide().removeClass('step-active').next().addClass('step-active').show();
                    $('.mailtab.now').removeClass('now').next().addClass('now');
                    $('.tabline i.now').removeClass('now').next().addClass('now');

                    if ($('.step-active').hasClass('success')) {
                        $('#btn-next-step').addClass('success').text('完成');
                    }

                } else {
                    $('.err_tip').text(data.Message).show();
                }
            });
        }
        $timeout(function () {
            $('#icode_image').click(function () {
                $(this).attr('src', $(this).attr('src') + '&timespan=' + new Date().getTime());
            });

            $('.btn-resms').click(function () {
                if ($(this).hasClass('disabled')) {
                    return false;
                }
                _reSendEmail();
                _emailCaptchaTimerStart();
            });
            $('#btn-next-step').click(function () {

                if ($('.step1').hasClass('step-active')) {

                    if ($scope.model.Email == "" || $scope.model.Email == undefined) {
                        $('.err_tip').text("请输入新的邮件地址").show();
                        return false;
                    }
                    if (!/\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/.test($scope.model.Email)) {
                        $('.err_tip').text("请输入正确的邮件地址").show();
                        return false;
                    }
                    if ($scope.model.Captcha == "" || $scope.model.Captcha == undefined) {
                        $('.err_tip').text("请输入验证码").show();
                        return false;
                    }

                    //发送邮件
                    _sendEmailAndGoNextStep();
                }
                if ($('.step2').hasClass('step-active')) {

                    if ($scope.model.EmailCaptcha == "" || $scope.model.EmailCaptcha == undefined) {
                        $('.err_tip').text("请输入邮件中的验证码").show();
                        return false;
                    }
                    //验证邮件验证码
                    _validateEmailCaptchaAndGoNextStep();
                }

                if ($(this).hasClass('success')) {
                    $modalInstance.dismiss('canceled');
                    window.location.reload();
                }
                $('.err_tip').text('').hide();
            });
        });
    });
})();


