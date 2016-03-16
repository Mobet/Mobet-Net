$(function () {
    $('#qrcode-trigger,#qrcode-close').click(function () {
        $('#login-main,#login-qrcode').toggle();
    });
    $('#login-button').click(function () {
        $('.labelbox').removeClass('error_bg');
        if ($('#user_account').val() == "") {
            $('#label_account').addClass('error_bg');
            $('#error-outcon').html('<div class="dis_box"><em class="icon_error"></em><span class="error-con">请输入账号</span></div>').show();
            return false;
        }
        if ($('#password').val() == "") {
            $('#label_password').addClass('error_bg');
            $('#error-outcon').html('<div class="dis_box"><em class="icon_error"></em><span class="error-con">请输入密码</span></div>');
            return false;
        }
    });
});