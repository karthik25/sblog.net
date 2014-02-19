$(function () {
    var api = "http://api.bootswatch.com/3/";
    $.ajax({
        type: 'GET',
        url: api,
        dataType: 'json',
        success: function (data) {
            populateDropdown(data.themes);
        }
    });

    $(document).on('change', '#bootswatch', function () {
        var val = $('#bootswatch').val();
        $('#bwLink').attr('href', val);
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
