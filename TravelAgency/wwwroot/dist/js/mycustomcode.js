$(document).ready(function () {

    $('[data-toggle="tooltip"]').tooltip();


$('.deleteWithSubmit').click(function () {
    //var formId = $("#" + $(this).data('form'));
    //if (!confirm($(this).data('confirm'))) {
    //    return false;
    //}

    //var token = $(':input:hidden[name*="RequestVerificationToken"]');
    //var data = {};
    //data[token.attr('name')] = token.val();
    //$.ajax({
    //    url: this.href,
    //    type: 'POST',
    //    data: data,
    //    success: function (result) {
    //        formId.submit();
    //    },
    //    error: function (xhr, ajaxOptions, thrownError) {
    //        alert(xhr.status);
    //        alert(thrownError);
    //    }
    //});

    //return false;

    var formId = $("#" + $(this).data('form'));
    var confirmMsg = $(this).data('confirm');
    var url = this.href;
    var table = $('#basicTable').DataTable();
    var btnDelete = $(this);
    $.confirm({
        title: "Delete confirmation",
        text: confirmMsg,
        confirm: function (button) {
            var token = $(':input:hidden[name*="RequestVerificationToken"]');
            var data = {};
            data[token.attr('name')] = token.val();



            $.ajax({
                url: url,
                type: 'POST',
                data: data,
                success: function (result) {
                    formId.submit();

                    table
                        .row($(btnDelete).parents('tr'))
                        .remove()
                        .draw();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        },
        cancel: function (button) {
            alert("You aborted the operation.");
        }
    });



    return false;
});
});


function isNumber(o) {
    return !isNaN(o - 0) && o !== null && o.replace(/^\s\s*/, '') !== "" && o !== false;
}

function reload() {
    location.reload();
}

function redirectToUrl(url) {
    window.location = url;
}



function displayErrorHtmlMessage(result) {
    var data = (typeof result == "string") ? jQuery.parseJSON(result) : result;

    var errors = "";

    if (data.ClientStatusCode === 2) {
        errors = data.ClientMessageContent;
    }
    if (data.ClientStatusCode === 3) {
        errors = data.ValidationResults;
    }



    if (errors.length > 1) {

        var divError = jQuery("<div/>");
        var ul = jQuery("<ul/>");
        jQuery.each(errors, function (i) {
            jQuery('<li/>').text(errors[i]).appendTo(ul);
        });
        ul.appendTo(divError);
        toastr.error(divError.html(), 'Error');
    }
    else {
        if (errors.length > 0) {
            toastr.error(errors[0], 'Error');
        }
    }



}

function showAlert(title, msg, type) {

    var icon = '';
    var dangerMode = false;
    switch (type) {
        case 1:
            icon = 'success';
            break;
        case 2:
            icon = 'error';
            dangerMode = true;
            break;
        case 3:
            icon = 'warning';
            dangerMode = true;
            break;
        case 4:
            icon = 'info';
            break;
        default:
            icon = 'info';
            break;
    }

    swal(title, msg,icon);
   
}


function notification(type, message) {

    if (type === 'Success') {
        toastr.success(message, '<i>Success</i>' );
    } else if (type === 'Error') {
        toastr.error(message, 'Error');
    } else if (type === 'Warning') {
        toastr.warning(message, 'Warning');
    } else {
        toastr.info(message, 'Information');
    }
}



function blockPageElement(elementId) {

    if (elementId) {
        $('#' + elementId).blockUI({ message: '<h2><img src="/be/images/loaders/loader10.gif" /> Processing...</h2>' });
    }
    else {
        $.blockUI({ message: '<h2><img src="/be/images/loaders/loader10.gif" /> Processing...</h2>' });
    }
}


function setMenuActive(pageName) {
    var pathname = window.location.pathname.toLowerCase();
    //  alert(pathname);
    if (pageName == null) {
        if (pathname !== '/index' && pathname !== '/admin') {
            var indexpath = pathname.substring(pathname.lastIndexOf('/') + 1);
            if (indexpath !== 'index') {
                pathname = pathname.substring(0, pathname.lastIndexOf('/') + 1) + 'index';
            }
        } else {
            pathname = null;
        }

    } else {
        pathname = pageName;
    }

    //  alert(pathname);

    if (pathname) {
        //   alert(pathname);
        var selectedLink = $(".leftMenu a[href*='" + pathname + "']");
        if (selectedLink) {
            $(selectedLink).closest('li').addClass("active");
            $(selectedLink).parents('li:last').addClass("active").css({ display: "block" });

            var parentUl = $(selectedLink).parents('ul');
            $.each(parentUl,
                function (index, item) {
                    $(item).css({ display: "block" });
                });
        }
    }
}


var msgCls =
    {
        "Saved": "Saved Successfully.",
        "Error": "Sorry there is an error in the process."
    };