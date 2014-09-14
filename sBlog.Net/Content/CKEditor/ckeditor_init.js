$(document).ready(function() {
    // Initialize CKEditor, if present
    jQuery('.adminRichText').ckeditor();

    $('#fullScreen').click(function () {
        var editor = CKEDITOR.instances["Post_PostContent"];
        editor.execCommand('maximize');
    });

    $('#contentPreview').click(function () {
        var editor = CKEDITOR.instances["Post_PostContent"];
        editor.execCommand('preview');
    });
});
