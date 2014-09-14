/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */
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
