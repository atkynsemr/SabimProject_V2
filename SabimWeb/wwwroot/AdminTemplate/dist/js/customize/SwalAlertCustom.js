function showSwalAlert(
    title = 'Bilgi',
    text = 'İşleminiz tamamlandı.',
    icon = 'info',
    timer = 5000,
    showConfirmButton = true,
    reloadOnConfirm = true, // OK'a basınca yenile
    autoReload = true // Süre dolunca yenile
) {
    // Otomatik yenileme zamanlayıcısını tanımlıyoruz
    let autoReloadTimeout;

    Swal.fire({
        title: title,
        text: text,
        icon: icon,
        timer: timer,
        showConfirmButton: showConfirmButton,
        allowOutsideClick: false // Kullanıcı dışarı tıklarsa modal kapanmasın
    }).then((result) => {
        if (result.isConfirmed && reloadOnConfirm) {
            // Eğer kullanıcı "Tamam" butonuna basarsa
            clearTimeout(autoReloadTimeout); // Otomatik yenileme zamanlayıcısını iptal et
            window.location.reload(); // Sayfayı yenile
        }
    });

    if (autoReload) {
        // Otomatik yenileme zamanlayıcısını başlatıyoruz
        autoReloadTimeout = setTimeout(() => {
            if (Swal.isVisible()) {
                Swal.close(); // SweetAlert modalını kapat
            }
            window.location.reload(); // Sayfayı yenile
        }, timer); // Kullanıcının belirttiği süre dolunca çalışır
    }
}
