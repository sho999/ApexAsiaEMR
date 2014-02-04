
$(document).ready(function () {

    $("#btnSave").click(function () {
        var userObj = {
            UserName: $("#txtUsername").val(),
            FullName: $("#txtFullName").val(),
            Password: $("#txtConfirmPassword").val(),
            PasswordQuestion:$("#txtPasswordQuestion").val(),
            PasswordAnswer:$("#txtPasswordAnswer").val(),
            Email:$("#txtEmail").val(),
            Comment:$("#txtComment").val()
           
        };
        $.ajax({
            url: '/service/AddUser',
            type: 'POST',
            data: JSON.stringify(userObj),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("AjaxError = " + textStatus + " Reason = " + errorThrown);
            },
            success: function (result) {
                alert(result);
            },
            async: true
        });
    
    });

});