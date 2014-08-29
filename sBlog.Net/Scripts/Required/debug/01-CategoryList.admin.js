/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */
$(document).ready(function () {
    $('.chkClickable').live('click', function () {
        $(this).next('.hdnStatus').val($(this).is(':checked'));
    });

    $('#btnAdd').click(function () {
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

        if (text != '' && !isPresent(text)) {
            if (text.length <= 50) {
                ajaxAdd(text);
            }
            else {
                showMessage('Category name cannot be more than 50 characters');
            }
        }
    });
});

function showMessage(message) {
    alert(message);
    $('#txtCat').val('').focus();
}

function isPresent(text) {
    var status = false;
    var elements = $("label[for^='chkBox_selectcategories_']");
    $(elements).each(function () {
        if ($(this).text() == text) {
            status = true;

            var currentTop = $(this).offset().top;
            var parentTop = $('.cpCheckBoxContent').position().top;
            $('.cpCheckBoxContent').scrollTop(currentTop - parentTop);

            $('#txtCat').val('');
        }
    });

    return status;
}

function ajaxAdd(text) {
    $.ajax({
        type: 'GET',
        url: '/Admin/CategoryAdmin/AddCategory',
        data: { 'categoryName': text },
        success: function (data) {
            if (data != null && data.CategoryID != 0) {
                addCategory(data.CategoryName, data.CategoryID);
                $(".cpCheckBoxContent").animate({ scrollTop: $(".cpCheckBoxContent").prop("scrollHeight") }, 1000);
            }
            $('#txtCat').val('');
        },
        error: function (req, status, err) {
            alert('an error occurred');
        }
    });
}

// create a new category
function addCategory(name, value) {
    var max = parseInt($("input[id^='valValue_selectcategories_']").last().attr('id').split('_')[2]) + 1;
    var element1 = document.createElement('input');
    $(element1).attr('id', 'valValue_selectcategories_' + max)
	           .attr('name', 'valValue_selectcategories_' + max)
	           .attr('type', 'hidden')
	           .attr('value', value);
    var element2 = document.createElement('input');
    $(element2).attr('id', 'chkBox_selectcategories_' + max)
	           .attr('name', 'chkBox_selectcategories_' + max)
	           .attr('type', 'checkbox')
	           .attr('class', 'chkClickable')
               .attr('checked', 'checked')
	           .attr('value', 'true');
    var element3 = document.createElement('input');
    $(element3).attr('id', 'hdnChk_selectcategories_' + max)
	           .attr('name', 'hdnChk_selectcategories_' + max)
	           .attr('type', 'hidden')
	           .attr('class', 'hdnStatus')
	           .attr('value', 'true');
    var element4 = document.createElement('label');
    $(element4).attr('for', 'chkBox_selectcategories_' + max);
    element4.innerHTML = "&nbsp;" + name;
    var element5 = document.createElement('input');
    $(element5).attr('id', 'lblLabel_selectcategories_' + max)
	           .attr('name', 'lblLabel_selectcategories_' + max)
	           .attr('type', 'hidden')
	           .attr('value', name);
    var element6 = document.createElement('br');
    $('.cpCheckBoxContent').append(element1)
	               .append(element2)
	               .append(element3)
                   .append(element4)
                   .append(element5)
                   .append(element6);
}
