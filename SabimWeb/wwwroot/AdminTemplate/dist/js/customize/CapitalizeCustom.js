$(function () {
    $('.capitalize-input').on('input', function () {
        let value = $(this).val();

        $(this).val(
            value
                .toLocaleLowerCase('tr') // Türkçe küçük harf dönüşümü
                .replace(/(^|\s)[a-zçğıöşü]/g, function (match) {
                    // İlk harfi Türkçe'ye göre büyütme (Başlangıç veya boşluktan sonra)
                    return match.toLocaleUpperCase('tr');
                })
        );
    });
});