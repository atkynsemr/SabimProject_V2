// Sayfa yüklendiğinde çalışacak olan kod
$(function () {
    // Login formu için doğrulama kurallarını ayarlıyoruz
    $('#LoginForm').validate({
        rules: {
            UserName: {
                required: true
            },
            Password: {
                required: true
            }
        },
        messages: {
            UserName: {
                required: "Lütfen kullanıcı adınızı giriniz"
            },
            Password: {
                required: "Lütfen şifrenizi giriniz"
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
});

