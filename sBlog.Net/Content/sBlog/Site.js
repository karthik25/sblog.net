$(document).ready(function () {
    $('.comment').hover(function () {
        $(this).addClass('comment-hover').removeClass('comment');
    }, function () {
        $(this).addClass('comment').removeClass('comment-hover');
    });
});

// more pages
// 'siteRoot' - Root of the site injected in to the pages
// Required coz. the root may vary based on whether the application is 
// a 'website' or 'application'
$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: siteRoot + 'viewpage/pageslist',
        dataType: 'html',
        success: function (data) {
            initializeList(data);
        },
        error: function (req, status, err) {
            // one possible error condition is when the user 
            // clicks on another link before this request completes
            // in that case, this request, which is in process may fail
            // so suppressing any error messages
        }
    });
});

// admin shortcuts
$(document).ready(function () {
    $('#qShortcuts').click(function () {
        var reference = this;
        expandCollapseAdminMenu(reference);
    });

    $(document).keypress(function (e) {
        var reference = $('#qShortcuts');
        if (reference.length == 1) {
            var code = e.charCode || e.keyCode;
            if (e.target.tagName.toLowerCase() != 'input' && e.target.tagName.toLowerCase() != 'textarea' && (code == 83 || code == 115)) {
                expandCollapseAdminMenu(reference);
            }
        }
    });
});

function expandCollapseAdminMenu(referenceElement) {
    var reference = referenceElement;
    if ($(reference).hasClass('shortcuts-expanded')) {
        $(reference).removeClass('shortcuts-expanded');
        $('#qShortcutsContent').animate({
            height: '21px'
        });
    }
    else {
        $(reference).addClass('shortcuts-expanded');
        $('#qShortcutsContent').animate({
            height: '220px'
        });
    }
}

// If the data returned contains at least one page,
// there will be 'anchor' tags. So, just look for 1
// and setup qtips if there is at least 1 page
function initializeList(data) {
    var linksLength = $(data).find('a').length;

    if (linksLength > 0) {
        var anchor = $('#menu li:last').find('a');
        var offset = $('#menu li:last').width();
        var left = $('#menu li:last').offset().left + offset;
        var top = $(anchor).offset().top + ($(anchor).height() / 4);
        $('img#more-pages').css({ 'position': 'absolute', 'left': left, 'top': top });

        $('img#more-pages').qtip({
            content: data,
            show: 'mouseover',
            hide: { when: 'click', fixed: true }
        }).show();
    }
}

// Position to comment errors, if there are any
$(document).ready(function () {
    var error = $('.comment-errors');
    if (error.length > 0) {
        $("html, body").animate({ scrollTop: $('.post-comment').offset().top }, 1000);
    }
});

// Client side verification for anonymous comments
$(document).ready(function () {
    $('.anonymous-comment').click(function (e) {
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

        var flag = true;
        var firstEmpty = null;
        $('.required-box').each(function () {
            if ($(this).val().trim() == '') {
                $(this).addClass('required-box-empty');
                $(this).parent().prev().find('.required-field-info').show();
                flag = false;
                if (firstEmpty == null) {
                    firstEmpty = this;
                }
            }
            else {
                $(this).removeClass('required-box-empty');
                $(this).parent().prev().find('.required-field-info').hide();
            }
        });

        if ($('.misc-field-lbl').length == 1 && $('#IsHuman').is(':checked') == false) {
            alert('Please click the checkbox to confirm that you are NOT a spammer');
            return false;
        }

        if (flag)
            $('#userComment').submit();
        else {
            $(firstEmpty).focus();
        }
    });
});

$(function () {
    var miscField = $('.misc-field-lbl').length;
    if (miscField == 1) {
        var ckbox = document.createElement('input');
        $(ckbox).attr('type', 'checkbox');
        $(ckbox).attr('title', 'click to confirm that you are NOT a spammer!');
        $(ckbox).attr('id', 'IsHuman');
        $(ckbox).attr('name', 'IsHuman');

        var lbl = document.createElement('label');
        $(lbl).html('Click to confirm that you are NOT a spammer');

        $('.misc-field-lbl').find('span').append(lbl);
        $('.misc-field').append(ckbox);

        $('#user-tests').hide();
    }
});
