$(function () {
    $.ajax({
        type: 'GET',
        url: api,
        dataType: 'json',
        success: function (data) {
            populateDropdown(data.themes);
            setupCookie();
            setOption($.cookie('sblog_user_theme'));
        }
    });

    $(document).on('change', '#bootswatch', function () {
        var val = $('#bootswatch').val();
        $('#bwLink').attr('href', val);
        updateCookie();
    });
});

function populateDropdown(themes) {
    $.each(themes, function () {
        var theme = this;
        $('#bootswatch').append($("<option></option>")
         .attr("value", theme.cssCdn)
         .text(theme.name));
    });
}

function setupCookie() {
    var userTheme = $.cookie('sblog_user_theme');
    if (userTheme === null || userTheme === undefined) {
        $.cookie('sblog_user_theme', $('#bootswatch').val(), { path: '/' });
    }
}

function updateCookie() {
    $.cookie('sblog_user_theme', $('#bootswatch').val(), { path: '/' });
}
