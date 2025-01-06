// Toast mesajını gösterecek fonksiyon
function showToast(message, title = 'Bilgi Mesajı', position = 'topRight', delay = 3000, className = 'bg-info', autohide = true) {
    $(document).Toasts('create', {
        class: className,   // Toast'un tipi (örneğin: bg-success, bg-danger)
        title: title,       // Toast başlığı
        autohide: autohide, // Toast otomatik kapanma
        position: position, // Pozisyon (topRight, topLeft, bottomRight, bottomLeft)
        delay: delay,       // Gösterim süresi (milisaniye cinsinden)
        body: message       // Mesaj içeriği
    });
}
