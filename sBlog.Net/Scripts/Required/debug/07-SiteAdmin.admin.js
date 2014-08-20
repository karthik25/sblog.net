/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */
$(document).ready(function () {
    // Initialize CKEditor, if present
    jQuery('.adminRichText').ckeditor();

    updatePublishBtn();

    $('.privateChkBox').change(function () {
        updatePublishBtn();
    });

    $('#fullScreen').click(function () {
        var editor = CKEDITOR.instances["Post_PostContent"];
        editor.execCommand('maximize');
    });

    $('#contentPreview').click(function () {
        var editor = CKEDITOR.instances["Post_PostContent"];
        editor.execCommand('preview');
    });

    $('.postUrl,.postUrlPrefix,#btnUpdate,#btnCancel').hide();

    // If the post/page is being edited, this
    // event does not do anything
    $('.postTitle').change(function () {
        var postId = parseInt($('#Post_PostID').val());

        if (postId == 0) {
            var text = $(this).val();
            if (text.trim() != '') {
                getVerifiedUrl(text);
            } else {
                clearUrlFields();
            }
        }
    });

    if ($('.postUrlValue').val() != null && $('.postUrlValue').val() != '') {
        var prefixText = '';
        if ($('#Post_PostID').val() != '0' && $('#Post_EntryType').val() == '1') {
            prefixText = getDatePrefix() + '/';
        }
        else {
            prefixText = 'pages' + '/';
        }
        $('.postUrlLabel').text(prefixText + $('.postUrlValue').val());
        $('.postUrlLabel').attr('title', 'click to change the url').bind('click', function () {
            $('.postUrl').show();
            $('.postUrlPrefix').show();
            $('.postUrlPrefix').text(prefixText);
            $('.postUrlLabel').hide();
            $('#btnUpdate').show();
            $('#btnCancel').show();
        });
    }

    $('#btnUpdate').click(function () {
        var text = $('.postUrl').val();
        if (text.trim() == '') {
            text = $('.postTitle').val();
        }
        $('.postUrl,.postUrlPrefix').hide();
        $('.postUrlLabel').show();
        $('#btnUpdate').hide();
        $('#btnCancel').hide();
        if (text.trim() != '') {
            getVerifiedUrl(text);
        }
        else {
            clearUrlFields();
        }
    });

    $('#btnCancel').click(function () {
        $('.postUrl,.postUrlPrefix').hide();
        $('.postUrlLabel').show();
        $('#btnUpdate').hide();
        $('#btnCancel').hide();
    });

    $(document).on('focus', '#bitlyLink', function () {
        $(this).one('mouseup', function (event) {
            event.preventDefault();
        }).select();
    });
});

function clearUrlFields() {
    $('.postUrlLabel').text('---').unbind('click');
    $('.postUrlPrefix').text('');
    $('.postUrlValue').val('');
    $('.postUrl').val('');
}

function getDatePrefix() {
    var postDate = $('#Post_PostAddedDate').val();
    var prefix = '', month = '';
    if (postDate == '1/1/0001 12:00:00 AM') {
        var date = new Date;
        month = date.getMonth() + 1;
        prefix = date.getFullYear() + '/' + (month >= 10 ? month : ("0" + month));
    } else {
        var parsedDate = new Date(Date.parse(postDate));
        month = parsedDate.getMonth() + 1;
        prefix = parsedDate.getFullYear() + '/' + (month >= 10 ? month : ("0" + month));
    }
    return prefix;
}

function getVerifiedUrl(postTitle) {
    var entryType = $('#Post_EntryType').val();
    var postId = $('#Post_PostID').val();
    $.ajax({
        type: 'GET',
        url: siteRoot + 'admin/post/verifyurlurlexists',
        data: { 'postTitle': postTitle, 'entryType': entryType, 'postId': postId },
        dataType: 'json',
        success: function (data) {
            finalizeUrl(data);
        }
    });
}

function finalizeUrl(text) {
    $('.postUrl').val(text);
    var entryType = $('#Post_EntryType').val();
    var prefix = '';
    if (entryType == "1") {
        prefix = getDatePrefix();
        text = prefix + '/' + text;
    }
    else {
        prefix = 'pages';
        text = 'pages/' + text;
    }
    $('.postUrlPrefix').text(prefix + '/');
    $('.postUrlValue').val(text);
    $('.postUrlLabel').text(text);
    
    $('.postUrlLabel').attr('title', 'click to change the url').bind('click', function () {
        $('.postUrl').show();
        $('.postUrlPrefix').show();
        $('.postUrlLabel').hide();
        $('#btnUpdate').show();
        $('#btnCancel').show();
    });
}

$(document).ready(function () {
    $('#btnAddTags').click(function (e) {
        e.preventDefault();
        showTagHelp();
        if ($('#txtTag').val().trim() != '') {
            var array = getCleanedTagList($('#txtTag').val().trim());
            createTags(array);
        }
        setHiddenVal();
        $('#txtTag').val('');
        $('#txtTag').focus();
    });

    $('.tag').live('click', function () {
        $(this).next().remove();
        $(this).remove();
        setHiddenVal();
    });

    if ($('#hdnAddedTags').val() != undefined && $('#hdnAddedTags').val() != '') {
        var elements = $('#hdnAddedTags').val().split(',');
        createTags(elements);
    }
});

function showTagHelp() {
    $('#tag-info').show();
    setTimeout(function () {
        $('#tag-info').hide('fast');
    }, 1000);
}

function getCleanedTagList(tagList) {
    var array = tagList.split(',');
    var regex = /\<[\S* ]*\>/;
    var alphaRegex = /[a-zA-Z]{1,}/;
    
    var cleanedArray = [];
    var invalidTags = false;
    $.each(array, function () {
        var trimmedText = this.trim();
        if (trimmedText != '') {
            if (trimmedText.length <= 50 && trimmedText.match(regex) == null && trimmedText.match(alphaRegex) != null) {
                cleanedArray.push(trimmedText);
            }
            else {
                invalidTags = true;
            }
        }
    });
    if (invalidTags) {
        alert('Tags with length greater than 50, tags with html, tags without any alphabets were removed.');
    }
    return cleanedArray;
}

function notPresent(text) {
    var result = true;
    var elements = $('#addedTags').find('span.tag');
    $(elements).each(function () {
        if ($(this).html().toLowerCase() == text.toLowerCase()) {
            result = false;
        }
    });
    return result;
}

function setHiddenVal() {
    var hdnStr = '';
    $('#addedTags').find('span.tag').each(function () {
        hdnStr += $(this).text() + ',';
    });
    hdnStr = hdnStr.substring(0, hdnStr.length - 1);
    $('#hdnAddedTags').val(hdnStr);
}

function createTags(array) {
    $(array).each(function () {
        if (notPresent(this)) {
            var element = document.createElement('span');
            $(element).attr('class', 'tag');
            $(element).attr('title', 'click to remove');
            element.innerHTML = this;
            $('#addedTags').append(element);
            var selement = document.createElement('span');
            selement.innerHTML = '&nbsp;&nbsp;&nbsp;';
            $('#addedTags').append(selement);
        }
    });
}

function updatePublishBtn() {
    var text = $('.privateChkBox').is(':checked') ? "Save" : "Publish";
    $('.publishBtn').attr('value', text);
}

$(document).ready(function () {
    $('#showHelp').click(function (e) {
        e.preventDefault();
        $("#dialog").dialog({ width: 600, height: 400 });
    });

    $('.possibilityHeader').click(function () {
        var content = $(this).next('.possibilityContent');
        $('.possibilityContent').hide();
        $(content).slideToggle('slow');
    });

    $('.brush-not-selected').hide();
    $('.possibilityContent').hide();

    $('#chk-show-all').change(function () {
        if ($(this).is(':checked')) {
            $('.brush-not-selected').show();
            $('.possibilityContent').hide();
            $('.selected-brushes-title').html('All brushes:');
        }
        else {
            $('.brush-not-selected').hide();
            $('.possibilityContent').hide();
            $('.selected-brushes-title').html('Selected brushes:');
        }
    });
});

$(document).ready(function () {
    var li = $('li.current');
    var submnu;
    if ($(li).parent().attr('class') != 'no-display sub-menu') {
        submnu = $(li).find('ul.sub-menu');
        $(submnu).removeClass('no-display').addClass('display');
    }
    else {
        li = $('li.current').parent().parent();
        submnu = $(li).find('ul.sub-menu');
        $(submnu).removeClass('no-display').addClass('display');
        $(li).addClass('current');
    }

    $('li.collapse').click(function () {
        $('#content').removeClass('content-actual').addClass('content-collapsed');
        $('#menu').hide();
        $('#quickLinks').hide();
        $('#restore').show();
    });

    $('a#restoreMenu').click(function (e) {
        e.preventDefault();
        $('#content').removeClass('content-collapsed').addClass('content-actual');
        $('#menu').show();
        $('#quickLinks').show();
        $('#restore').hide();
    });

    $('.imgSharing').click(function () {
        var radio = $(this).parent().prev();
        $(radio).click();
    });
});

$(document).ready(function () {
    $('.deleteFile').click(function () {
        var prevCell = $(this).parent();
        var fileName = $(this).attr('data-file-name');

        var choice = confirm("Are you sure you want to delete " + fileName + "? It cannot be recovered once it is deleted. Click \"Ok\" to delete.");

        if (choice) {
            $.ajax({
                type: 'GET',
                url: siteRoot + 'Admin/Uploads/DeleteUploadedFile',
                data: { 'fileName': fileName, 'token': $('#OneTimeCode').val() },
                dataType: 'json',
                success: function (data) {
                    if (data.FileStatus == "File Deleted") {
                        var zeroRowHtml = '<tr><td>there are no items in this section at this point!</td><td></td></tr>';
                        var row = $(prevCell).parent();
                        $(row).remove();
                        if ($('#manage-table > tbody').find('tr').length == 0) {
                            var tbody = $('#manage-table > tbody');
                            $(tbody).append(zeroRowHtml);
                        }
                    }
                },
                error: function (req, status, err) {
                    alert('an error occurred while trying to delete the selected file, please try again.');
                }
            });
        }
    });
});

$(document).ready(function () {
    $('.trashComment').click(function (e) {
        e.preventDefault();
        var anchor = this;
        var commentId = $(anchor).next('.trashCommentID').val();

        $.ajax({
            type: 'GET',
            url: siteRoot + 'Admin/CommentAdmin/TrashComment',
            data: { 'commentId': parseInt(commentId), 'token': $('#OneTimeCode').val() },
            dataType: 'json',
            success: function (data) {
                if (data.DeleteStatusString == "Delete succeeded") {
                    var row = $(anchor).parent().parent().parent();
                    $(row).remove();
                }
            },
            error: function (req, status, err) {
                alert('an error occurred while trying to delete the selected comment, please try again.');
            }
        });
    });
});

$(document).ready(function () {
    $('#btnCreateTags').click(function () {
        var text = $('#txtTag').val().trim();

        var regex = /\<[\S* ]*\>/;
        var alphaRegex = /[a-zA-Z]{1,}/;

        if (text.match(regex) != null) {
            alert('Tag name entered is invalid');
            $('#txtTag').val('').focus();
            return;
        }

        if (text.match(alphaRegex) == null) {
            alert('Tag entered is invalid, should have at least 1 alphabet');
            $('#txtTag').val('').focus();
            return;            
        }

        if (text != '' && text.indexOf(',') < 0) {
            ajaxAddTagPartial(text);
        }
        else if (text != '') {
            $('.add-msg').html('invalid tag, cannot contain a \',\'').show('slow');
            setTimeout(function () {
                $('.add-msg').html('');
                $('#txtTag').val('').focus();
            }, 2000);
        }
    });

    $('.deletePost, .deletePage, .deleteCategory, .deleteTag, .deleteAuthor').live('click', function (e) {
        e.preventDefault();
        var element = this;
        var url = $(element).attr('href') + "&token=" + $('#OneTimeCode').val();

        var choice = confirm("Are you sure you want to delete the selected item?");

        if (choice) {
            $.ajax({
                type: 'GET',
                url: url,
                dataType: 'json',
                success: function (data) {
                    deleteRow(element);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert('unable to delete the chosen item');
                }
            });
        }
    });
});

function deleteRow(element) {
    var zeroRowsHtml = '<tr class=\"manage-row\"><td class=\"manage-cell\">there are no items in this section at this point!</td><td class=\"manage-cell\"></td><td class=\"manage-cell\"></td></tr>';
    var row = $(element).parents('tr');
    $(row).remove();
    if ($('#manage-table > tbody').find('tr').length == 0) {
        var tbody = $('#manage-table > tbody');
        $(tbody).append(zeroRowsHtml);
    }
}

function ajaxAddTagPartial(text) {
    $.ajax({
        type: 'GET',
        url: siteRoot + 'Admin/TagAdmin/AddTagPartial',
        data: { 'tagName': text, 'token': $('#OneTimeCode').val() },
        dataType: 'html',
        success: function (data) {
            if (data.trim() != '') {
                var zeroRow = $('tr.manage-row');
                $('#manage-table tbody tr:first').before(data);
                $('.add-msg').html('');
                if (zeroRow.length == 1) {
                    $(zeroRow).remove();
                }
            }
            else {
                $('.add-msg').html('tag already exists').show('slow');
                setTimeout(function () {
                    $('.add-msg').html('');
                }, 2000);
            }
            $('#txtTag').val('');
        },
        error: function (req, status, err) {
            alert('an error occurred while trying to add the tag, please try again.');
        }
    });
}

$(document).ready(function () {
    $('#AkismetEnabled').change(function () {
        $('.akismetAdvancedOptions').toggle();
    });

    if ($('#AkismetEnabled').is(':checked')) {
        $('.akismetAdvancedOptions').show();
    }
});

$(document).ready(function () {
    $('#forgottenPswd').click(function (e) {
        e.preventDefault();
        $('.login-errors').html('');
        $('.client-login-errors').html('');
        $('#username').val('username');
        $('#fake-password').show().val('password');
        $('#passwd').hide();
        $('#slick-login').hide();
        $('#slick-forgotten').show();
    });

    $('#loginForm').click(function (e) {
        e.preventDefault();
        $('#results').html('');
        $('#emailAddress').val('your email address');
        $('#slick-login').show();
        $('#slick-forgotten').hide();
    });

    $('#btnLogin').click(function () {
        if ($('#username').val() != 'username' && $('#passwd').val() != 'password') {
            $('.client-login-errors').html('');
            return true;
        }
        $('.login-errors').html('');
        $('.client-login-errors').html('Invalid username/password');
        return false;
    });

    $('#btnForgottenPswd').click(function () {
        if ($.trim($('#emailAddress').val()) != '' && $.trim($('#emailAddress').val()) != 'your email address') {
            return true;
        }
        $('#emailAddress').val('your email address');
        return false;
    });

    $('#btnClose').click(function () {
        $('#dialog').dialog('close');
    });
});

$(document).ready(function () {
    $('.col-no-display').each(function () {
        var index = $(this).index();
        if (index > 0) {
            $('#manage-table tr').each(function () {
                $(this).find('td:eq(' + index + ')').hide();
            });
            $('#manage-table tfoot tr').find('th:eq(' + index + ')').hide();
            $('.col-no-display').hide();
        }
    });
});

$(document).ready(function () {
    $('#btnCreateCategory').click(function () {
        var text = $('#txtCat').val().trim();
        
        var regex = /\<[\S* ]*\>/;

        var alphaRegex = /[a-zA-Z]{1,}/;

        if (text.match(regex) != null) {
            showMessage('Category name entered is invalid');
            return;
        }

        if (text.match(alphaRegex) == null) {
            showMessage('Category name entered is invalid, should have at least 1 alphabet');
            return;
        }

        if (text != '' && text.length <= 50) {
            ajaxAddPartial(text);
        }
        else {
            showMessage('Category name cannot be more than 50 characters');
        }
    });
});

function ajaxAddPartial(text) {
    $.ajax({
        type: 'GET',
        url: '/Admin/CategoryAdmin/AddCategoryPartial',
        data: { 'categoryName': text, 'token': $('#OneTimeCode').val() },
        dataType: 'html',
        success: function (data) {
            if (data.trim() != '') {
                $('#manage-table tbody tr:first').before(data);
                $('.add-msg').html('');
                var zeroRow = $('tr.manage-row');
                if (zeroRow.length == 1) {
                    $(zeroRow).remove();
                }
            }
            else {
                $('.add-msg').html('category already exists').show('slow');
                setTimeout(function () {
                    $('.add-msg').html('');
                }, 2000);
            }
            $('#txtCat').val('');
        },
        error: function (req, status, err) {
            alert('an error occurred while trying to add the category entered, please try again.');
        }
    });
}

$('.deactivateAuthor').live('click', function (e) {
    e.preventDefault();
    var element = this;
    var url = $(element).attr('href') + "&token=" + $('#OneTimeCode').val();
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'json',
        success: function (data) {
            if (data == true) {
                var newUrl = $(element).attr('href');
                var statusElement = $(element).parent().parent().find('td:eq(3)');
                if (newUrl.indexOf('currentStatus=1') >= 0) {
                    $(element).html('activate');
                    newUrl = newUrl.replace("currentStatus=1", "currentStatus=0");
                    $(element).attr('href', newUrl);
                    $(statusElement).html('Inactive');
                }
                else {
                    $(element).html('deactivate');
                    newUrl = newUrl.replace("currentStatus=0", "currentStatus=1");
                    $(element).attr('href', newUrl);
                    $(statusElement).html('Active');
                }
            }
        },
        error: function (req, status, err) {
            alert('an error occurred while trying activate/deactivate the selected author, please try again.');
        }
    });
});

$(function () {
    $('#username').focus(function () {
        $(this).val('');
    }).focusout(function () {
        if ($(this).val() == '') {
            $(this).val('username');
        }
    });

    $('#emailAddress').focus(function () {
        $(this).val('');
    }).focusout(function () {
        if ($(this).val() == '') {
            $(this).val('your email address');
        }
    });

    $('#fake-password').focus(function () {
        $(this).hide();
        $('#passwd').val('').show().focus();
    });

    $('#passwd').focusout(function () {
        if ($(this).val() == '') {
            $(this).val('password').hide();
            $('#fake-password').val('password').show();
        }
    });

    $('input:text,input:password').focus(function () {
        $(this).parent().next('.field-info').show();
    }).blur(function () {
        $(this).parent().next('.field-info').hide();
    });

    $('#showOptions').click(function () {
        $('.remember-me').toggle('slow');
    });

    $('#requestAccount').click(function () {
        $('#request-account').hide();
        $('#requestResults').html('');
        $('.request-form-content input:text,textarea').val('');
        resetValidation();
        $('.request-form').show();
        $('.request-form-content input:text:eq(0)').focus();
    });

    $('#sendRequest').click(function (e) {
        e.preventDefault();

        // If a field contains any html, return
        var regex = /\<[\S* ]*\>/;
        var validateErrors = null;
        $('.validate-html').each(function () {
            if (validateErrors == null && $(this).val().trim() != '' && $(this).val().match(regex) != null) {
                validateErrors = this;
            }
        });

        if (validateErrors != null) {
            $(validateErrors).focus();
            alert('Field contains invalid content (like html tags)');
            return;
        }

        $('#requestAccountFrm').submit();
    });

    $('.closeWindow,#closeForm').click(function () {
        showRequestAccountLink();
    });

    $(document).keyup(function (e) {
        var requestForm = $('.request-form');
        if (requestForm.length == 1 && e.keyCode == 27) {
            if ($(requestForm).is(':visible')) {
                showRequestAccountLink();
            }
        }
    });
});

function showRequestAccountLink() {
    $('.request-form').hide();
    $('#request-account').show();
}

function resetValidation() {
    //Removes validation from input-fields
    $('.request-form-content .input-validation-error').addClass('input-validation-valid');
    $('.request-form-content .input-validation-error').removeClass('input-validation-error');
    //Removes validation message after input-fields
    $('.request-form-content .field-validation-error').addClass('field-validation-valid');
    $('.request-form-content .field-validation-error').removeClass('field-validation-error');
}

function savePostOrPage() {
    $('.privateChkBox').trigger('click');
    var rUrl = ($('#AjaxSaved').val().toLowerCase() == 'true') ? 'edit' : 'add';
    var formData = $('form').serializeArray();
    $.ajax({
        type: 'POST',
        url: siteRoot + 'admin/post/' + rUrl,
        data: formData,
        dataType: 'json',
        success: function (data) {
            if (data.IsValid) {
                $('#AjaxSaved').val(true);
                $('#Post_PostID').val(data.PostId);
                var msg = 'Saved a draft of your post at <b>' + getCurrentDateTime() + '</b>';
                $('#updateAjaxStatus').html(msg).show();
            }
        }
    });
}

function getCurrentDateTime() {
    var now = new Date();
    var year = "" + now.getFullYear();
    var month = "" + (now.getMonth() + 1); if (month.length == 1) { month = "0" + month; }
    var day = "" + now.getDate(); if (day.length == 1) { day = "0" + day; }
    var hour = "" + now.getHours(); if (hour.length == 1) { hour = "0" + hour; }
    var minute = "" + now.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
    var second = "" + now.getSeconds(); if (second.length == 1) { second = "0" + second; }
    return month + "/" + day + "/" + year + " " + hour + ":" + minute + ":" + second;
}
