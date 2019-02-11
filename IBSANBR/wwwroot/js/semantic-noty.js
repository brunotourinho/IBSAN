function showNoty(type, header, text, icon) {
    var m = '<div style="display:flex; flex-direction:row; align-items: center;">';
    if (icon) {
        m += '<div style="flex: 0 1 auto; padding-right: 1em;"><i class="' + icon + ' icon large"></i></div>';
    }
    m += '<div style="flex: 1 1 auto; flex-direction:column;"><div style="font-weight: bold;font-size: 1.1em;">' + header + '</div><div class="sncontent">' + text + '</div></div>';
    new window.Noty({
        text: m,
        type: type,
        theme: 'semanticui',
        timeout: 1500,
        progressBar: true,
        animation: {
            open: 'animated fadeIn',
            close: 'animated fadeOut'
        }
    }).show();
}