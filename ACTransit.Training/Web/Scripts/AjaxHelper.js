
var Ajax = {
    ajax: function (url, options, waitBox) {
        var wb = waitBox;
        var prom = $.Deferred();
        if (wb != null)
            wb.show();
        $.ajax(url, options).success(function (data) {
            try {
                prom.resolve(data);
            } catch (e) {
                alert(e.message);
            }
        }).fail(function (e, eType, message) {
            
            var m = '';
            if (e && typeof (e.responseJSON) !== 'undefined' && typeof (e.responseJSON.error) !== 'undefined' && e.responseJSON.error)
                m = e.responseJSON.message;
            else if (e && e.state && e.state() == 'rejected')
                m = 'Could not connect to the server. Please try again later.';
            else
                m = 'Some error happend while connecting to the server. please try again later.';
            var errorBox = modal.show("Failed", m, { showRefresh: false });
            prom.reject(e, eType, message,errorBox);
        }).always(function () {
            if (wb != null) wb.hide();
        });
        return prom;
    },
}
