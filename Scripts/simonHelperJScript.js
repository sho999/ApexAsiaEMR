
function showChromeToaster() {
    var havePermission = window.webkitNotifications.checkPermission();
    if (havePermission == 0) {
        // 0 is PERMISSION_ALLOWED
        var notification = window.webkitNotifications.createNotification(
                    'http://i.stack.imgur.com/dmHl0.png',
                    'You Inactive on TS for more than 20 Minutes!',
                    'Please click on close button or anywhere in  this box to start working on TS again.'
                    );
        notification.onclick = function () {
            //updateUserStates($("#hiddenLoginName").val(), "WakeUp", "Clicked on Notification Toster.");
            alert("Clicked on Notification Toster.");
            notification.close();
        }
        notification.onclose = function () {
            //updateUserStates($("#hiddenLoginName").val(), "WakeUp", "Closed Notification Toster.");
            alert("Closed Notification Toster.");
            notification.close();
        }

        notification.show();
    } else {
        $("#enableChromeNotification").show();
    }
}

function populateDaysDropdownList(dropdownListName) {
    for (var i = 1; i < 32; i++) {
        $("#" + dropdownListName).append("<option>" + i + "</option>");
    }
}

function populateYearsDropdownList(dropdownListName) {
    for (var i = 1901; i < 2014; i++) {
        $("#" + dropdownListName).append("<option>" + i + "</option>");
    }
}


// When using prototype the function is added to the object and every object that is instantiated has that function.
// So I could call it directly on an object, like myStringVariable.isNullOrEmpty():
String.prototype.isNullOrEmpty = function () {
    var _isNullOrEmpty = true;
    if (this)
        if (this.length > 0)
            _isNullOrEmpty = false;
    return _isNullOrEmpty;
}




function parseASMXdateTime(microsoftDateTime) {
    var dateString = microsoftDateTime;
    var date = new Date(parseInt(dateString.replace(/\/Date\((\d+)\)\//, '$1')));

    var datePart = date.getDate();
    var monthPart = date.getMonth() + 1;
    var fullYear = date.getFullYear();

    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();

    return fixTime(monthPart) + "/" + fixTime(datePart) + "/" + fullYear + " " + fixTime(hours) + ":" + fixTime(minutes) + ":" + fixTime(seconds);
}

function toDDMMMYYYYdate(microsoftDateTime) {
    console.log("microsoftDateTime=" + microsoftDateTime);
    if (isEmpty(microsoftDateTime) || isBlank(microsoftDateTime)) { 
        return "";
    }
    else {
    var dateString = microsoftDateTime;
    var date = new Date(parseInt(dateString.replace(/\/Date\((\d+)\)\//, '$1')));
    console.log("parse Date " + date);
    var datePart = date.getDate();
    var monthPart = date.getMonth() + 1;
    var fullYear = date.getFullYear();

    var arr = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    return fixTime(datePart) + " " + arr[monthPart - 1] + " " + fullYear;
    }

}

function parseJsonDate(jsonDate) {
    var offset = new Date().getTimezoneOffset() * 60000;
    var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);

    if (parts[2] == undefined)
        parts[2] = 0;

    if (parts[3] == undefined)
        parts[3] = 0;


    var myDate = new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
    return myDate.toDateString();
};

function parseJsonDateTime(jsonDate) {
    var offset = new Date().getTimezoneOffset() * 60000;
    var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);

    if (parts[2] == undefined)
        parts[2] = 0;

    if (parts[3] == undefined)
        parts[3] = 0;


    var myDate = new Date(+parts[1] + parts[2] * 3600000 + parts[3] * 60000);
    return myDate;
};


//For checking if a string is empty, null or undefined:
function isEmpty(str) {
    return (!str || 0 === str.length);
}

//For checking if a string is blank, null or undefined:
function isBlank(str) {
    return (!str || /^\s*$/.test(str));
}

function validateEmail(inputValue) {
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    return emailPattern.test(inputValue);
}

function getEmptyStringIfEmptyOrBlank(inputString) {
    if (isEmpty(inputString) || isBlank(inputString)) {
        return "";
    }
    else {
        return inputString;
    }
}

function fixTime(t) {
    if (t < 10) {
        t = "0" + t;
    }
    return t;
}


