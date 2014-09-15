$(function () {
    $('#ckeditor_toolbar').hide();

    $("textarea.mdd_editor").MarkdownDeep({
        help_location: "/Scripts/mdd_help.htm",
        ExtraMode: true
    }).attr("rows", "10");
})
