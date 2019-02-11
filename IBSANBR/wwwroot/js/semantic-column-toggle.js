(function ($) {
    $.fn.columntoggle = function (options) {
        var $this = $(this);
        var settings = $.extend({
            hiddenColumns: [],
            showMenu: true
        }, options);

        return this.each(function () {
            if (settings.hiddenColumns) {
                window.$columnCount = $this.find("thead tr th:not(:last-child)").length;

                $.each(settings.hiddenColumns,
                    function (i, e) {
                        if (e > window.$columnCount)
                            console.log("Aviso: A coluna " + e + " está além do número de colunas da tabela");
                        else if (!$this.find("thead tr th:nth-child(" + e + ")").is(":last-child")) {
                            $this.find("thead th:nth-child(" + e + ")").toggle();
                            $this.find("tbody td:nth-child(" + e + ")").toggle();
                        }
                    });
            }

            if (settings.showMenu) {
                window.$colunas = [];

                $.each($this.find("thead tr th:not(:last-child)"),
                    function (i, e) {
                        window.$colunas.push({
                            columnIndex: i + 1,
                            columnName: e.innerText,
                            visible: $(e).is(":visible")
                        });
                    });

                $this.find("thead tr th:last-child").append(
                    '<div class="ui icon top left pointing dropdown mini button colshIconButton">' +
                    '<i class="columns icon"></i>' +
                    '<div class="ui menu colsh">' +
                    '<div class="center aligned header">Escolha as colunas</div>' +
                    "</div>" +
                    "</div>");

                $.each(window.$colunas,
                    function (i, e) {
                        $(".colsh").append(
                            '<div class="item">' +
                            '<div class="ui checkbox colshCheckbox">' +
                            '<input type="checkbox" name="col_' +
                            e.columnIndex +
                            '"' +
                            (e.visible ? "checked" : "") +
                            "/>" +
                            "<label>" +
                            e.columnName +
                            "</label>" +
                            "</div>");
                    });

                $(".item input[type=checkbox]").change(function () {
                    var i = parseInt($(this).attr("name").substring(4));
                    $this.find("thead tr th:nth-child(" + i + ")").toggle();
                    $this.find("tbody tr").each(function () {
                        $(this).find("td:nth-child(" + i + ")").toggle();
                    });
                });

                $(".colshIconButton").popup({
                    lastResort: "bottom left",
                    on: "click"
                }).dropdown({
                    action: "nothing"
                });

                $(".colshCheckbox").checkbox();
            }
        });
    };
}(jQuery));