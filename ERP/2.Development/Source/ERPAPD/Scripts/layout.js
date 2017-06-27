//$(function () {
//    // Reference the auto-generated proxy for the hub.  
//    var userActivity = $.connection.userActivityHub;

//    // Create a function that the hub can call back to display messages.
//    userActivity.client.updateUsersOnline = function () {

//    };

//    $.connection.hub.start();
//});
//$(function () {
//    toastr.options = {
//        "debug": false,
//        "positionClass": "toast-top-right",
//        "onclick": null,
//        "fadeIn": 300,
//        "fadeOut": 5000,
//        "timeOut": 5000,
//        "extendedTimeOut": 1000
//    };

//    var connection = $.connection(r + '/echos');
//    connection.received(function (data) {
//        toastr.success(data, 'Annoucement');
//        //$('#messages').append('<li>' + data + '</li>');
//    });

//    connection.start().done(function () {
//        $("#broadcast").click(function () {
//            var msg = $('#msg').val();
//            if (msg) {
//                connection.send($('#msg').val());
//            }
//        });
//    });

//})


var pathname = window.location.pathname.toLowerCase();
$('.sub a').each(function () {
    if ($(this).attr("href").toLowerCase() == pathname) {
        var parent = $(this).parent("li");
        parent.addClass("current");
        parent.parent("ul").addClass("show");
        if (parent.parent("ul").parent("li").length > 0) {
            parent.parent("ul").parent("li").addClass("current");
        }
    }
});

var pathname = window.location.pathname.toLowerCase();
$('.nav-list a').each(function () {
    if ($(this).attr("href").toLowerCase() == pathname) {
        var parent = $(this).parent("li");
        parent.addClass("active");
        if (parent.parent("ul").parent("li").length > 0) {
            parent.parent("ul").parent("li").addClass("active open");
            if (parent.parent("ul").parent("li").parent("ul").parent("li").length > 0) {
                parent.parent("ul").parent("li").parent("ul").parent("li").addClass("active open");
            }
        }
    }
});
//var userremind = $.connection.chatHub;
//$.connection.hub.start();




//userremind.client.addNewMessageToPage = function (user, message) {
//    if (user == currentuserid) {
//        data = message.split(';');
//        var myticket = parseInt(data[0]);
//        var newticket = parseInt(data[1]);
//        var queueticket = parseInt(data[2]);
//        var createdticket = parseInt(data[3]);
//        var resolvedticket = parseInt(data[4]);
//        var totalticket = parseInt(myticket) + parseInt(newticket) + parseInt(queueticket) + parseInt(createdticket) + parseInt(resolvedticket);
//        if (totalticket > 0) {
//            $("#totalticket").html(totalticket);
//            if (myticket > 0) {
//                $("#contentmyticket").html(myticket);
//            }
//            else {
//                $("#contentmyticket").html("");
//            }
//            if (newticket > 0) {
//                $("#contentnewticket").html(newticket);
//            }
//            else {
//                $("#contentnewticket").html("");
//            }
//            if (queueticket > 0) {
//                $("#contentqueueticket").html(queueticket);
//            }
//            else {
//                $("#contentqueueticket").html("");
//            }
//            if (createdticket > 0) {
//                $("#contentcreatedticket").html(createdticket);
//            }
//            else {
//                $("#contentcreatedticket").html("");
//            }
//            if (resolvedticket > 0) {
//                $("#contentresolvedticket").html(resolvedticket);
//            }
//            else {
//                $("#contentresolvedticket").html("");
//            }
//            $("#alertdiv").css("display", "inline-block");
//            $("#alertdiv").find('.purple').removeClass('open');
//        }
//        else {
//            $("#alertdiv").css("display", "none");
//        }
//        allowgetremind = 0;
//    }
//};