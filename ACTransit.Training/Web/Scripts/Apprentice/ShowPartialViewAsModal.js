var lastModalName;

rebind = function(){}

onAjaxSuccess = function (data, status, xhr) {
    if (data.error) {
        alert(data.message);
    }
    $("#" + lastModalName + "-container").modal('hide');
    location.reload();
}

showPartialViewAsModal = function (rootUrl, that) {
    var url = rootUrl + 'apprentice/' + that.data("action") + '/' + that.data("id");
    lastModalName = that.data("modal");
    $.ajax({
        url: url,
        cache: false,
        contentType: "text/html; charset=utf-8",
        dataType: "html"
    }).fail(function (jqXhr, textStatus, errorThrown) {
        alert(textStatus);
    }).done(function (data) {
        if (data == null) return;
        $("#" + lastModalName + "-placeholder").html(data);
        $("#" + lastModalName + "-container").modal('show');
        rebind();
    });
}

getPartialViewAsModal = function (rootUrl, that) {
    var url = rootUrl + 'apprentice/' + that.data("action") + '/' + that.data("id");
    lastModalName = that.data("modal");
    $.ajax({
        url: url,
        cache: false,
        contentType: "text/html; charset=utf-8",
        dataType: "html"
    }).fail(function (jqXhr, textStatus, errorThrown) {
        alert(textStatus);
    }).done(function (data) {
        return data;
    });
}
