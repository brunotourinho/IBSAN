// Cannot have another modal on the same screen
// Remove {id?} from route to work with single parameter (usually id)
// app.UseMvc(routes =>
// {
//    routes.MapRoute(
//        name: "default",
//         //template: "{controller=Home}/{action=Index}/{id?}");
//         template: "{controller=Home}/{action=Index}/");
// });

(function ($) {
    var re = /([^&=]+)=?([^&]*)/g;
    var decodeRe = /\+/g; // Regex for replacing addition symbol with a space
    var decode = function (str) { return decodeURIComponent(str.replace(decodeRe, " ")); };
    $.parseParams = function (query) {
        var params = {}, e;
        while (e === re.exec(query)) {
            var k = decode(e[1]), v = decode(e[2]);
            if (k.substring(k.length - 2) === "[]") {
                k = k.substring(0, k.length - 2);
                (params[k] || (params[k] = [])).push(v);
            } else params[k] = v;
        }
        return params;
    };
})(jQuery);

(function ($) {
    $.fn.isConfirmButton = function (options) {
        var defaults = {
            header: "",
            text: "",
            inverted: false
        };

        var settings = $.extend({}, defaults, options);
        $(this).on("click",
            function (e) {
                e.preventDefault();
                var current = $(this);

                if ($("#flyModal").length > 0) {
                    $("#flyModal").remove();
                }

                $(current).closest(".ui.container").parent().append(
                    '<div id="flyModal" class="ui small modal"><div class="header">' +
                    settings.header +
                    '</div><div class="content"><p>' +
                    settings.text +
                    '</p></div><div class="actions"><div class="ui ok button">Ok</div><div class="ui cancel button">Cancelar</div></div></div>');

                $(".ui.small.modal")
                    .modal({
                        inverted: settings.inverted,
                        closable: true,
                        onDeny: function () {
                            return true;
                        },
                        onApprove: function () {
                            window.location.href = $(current).attr("href");
                        }
                    }).modal("show");
            });
    };
})(jQuery);