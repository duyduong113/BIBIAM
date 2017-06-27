
var chat;

$(document).ready(function () {
    GetCountMessage("addWork");
    GetCountMessage("addAppointment");
    GetCountMessage("Annoucement");
    GetCountMessage("Emulation");
    chat = $.connection.chatHub;
    $.connection.hub.start();
    chat.client.broadcastMessage = function (data) {
        console.log(data);
        if (data.type) {
            $.gritter.add({
                title: '',
                text: data.message,
                class_name: 'gritter-success'
            });
            GetCountMessage(data.type);
        }
        
    };
    //setInterval(function () {
    //    GetCountMessage();
    //}, 10000);
});
function GetCountMessage(type) {
    $.ajax({
        url: r + "/Ajax/GetCountNotificationType",
        type: 'POST',
        data: {type:type},
        success: function (data) {
            totalmessage = data.data;

            if (totalmessage > 0) {
                switch (type) {
                    case "addWork":
                        if ($("#GridViewWork").length > 0) {
                            $("#GridViewWork").data("kendoListView").dataSource.read();
                        }
                        if ($("#GridWorks").length > 0) {
                            $("#GridWorks").data("kendoGrid").dataSource.read();
                        }
                        showNotification($("#tabWork"), type);
                        break;
                    case "addAppointment": 
                        if ($("#GridAppointment").length > 0) {
                            $("#GridAppointment").data("kendoGrid").dataSource.read();
                        }
                        if ($("#GridViewAppointment").length > 0) {
                            $("#GridViewAppointment").data("kendoListView").dataSource.read();
                        }
                        showNotification($("#tabAppoint"), type);
                        break;
                    case "Annoucement":
                        console.log(type);
                        if ($("#GridViewAnnoucement").length > 0) {
                            $("#GridViewAnnoucement").data("kendoListView").dataSource.read();
                        }
                        showNotification($("#tabNotice"), type);
                        break;
                    case "Emulation":
                        console.log(type);

                        if ($("#GridViewEmulation").length > 0) {
                            $("#GridViewEmulation").data("kendoListView").dataSource.read();
                        }
                        showNotification($("#tabEmulat"), type);
                        break;
                   
                }
            }
        }
    });
}
function UpdateIsRead(e) {
    console.log($(e).attr("data-notif"));
    $.ajax({
        url: r + "/Ajax/UpdateNotificationRead",
        type: 'POST',
        data: { type: $(e).attr("data-notif") },
        success: function (data) {
            if (data.success) {
                $(e).removeClass("is_active");
                $(e).find(".number").hide();
            }
        }
    });
}
function showNotification(element,type) {
    if ($(element).length > 0 && $(element).attr("class") == "") {
        $(element).find("a").addClass("is_active");
        $(element).find(".number").show().text(totalmessage);
    } else if ($(element).attr("class") == "active") {
        $.ajax({
            url: r + "/Ajax/UpdateNotificationRead",
            type: 'POST',
            data: { type: type },
            //success: function (data) {
            //}
        });
    }
}

function onclickprofile() {
    $('#divpopover').show();
    if (IsRoleAccountant == "True") {
        $('#TabCountMessage a').click();
    }
    else {
        $("#number_notify").show().text('');
        $('#Portal_Message_tab').click();
    }
}
$("body").click(function (e) {
    var popup = $("#divpopover");
    if (!$('#profile').is(e.target) && $('#profile').has(e.target).length == 0 && !popup.is(e.target) && popup.has(e.target).length == 0) {
        $('#divpopover').hide();
    }
});
//function popover profile
function onActiveTab(obj) {
    $(".popover-item").parent().removeClass('active');
    $(obj).parent().addClass('active');
    if (obj.title == "Thông tin") {
        $.ajax({
            url: r + "/SalesEmployerTouch/_PartialTreeProfiles",
            type: 'GET',
            success: function (partialView) {
                $(".popover-content-profile").hide().html(partialView).fadeIn("fast");
            }
        });
    }
    else if (obj.title == 'Nhắc nhở') {
        $.ajax({
            url: r + "/PortalMessage/GetCountForAccountant",
            type: 'POST',
            success: function (data) {
                $(".popover-content-profile").html($('#CountForAccountantTemplate').html());
                $('.itemright').html('0');
                $.each(data.Data, function (index, d) {
                    if (d.Type == 'Giảm giá ngoài chính sách') {
                        $('#CountForAccountant1').html(d.Total);
                    }
                    else if (d.Type == 'Phiếu đăng ký') {
                        $('#CountForAccountant2').html(d.Total);
                    }
                    else if (d.Type == 'Duyệt kích tin') {
                        $('#CountForAccountant3').html(d.Total);
                    }
                    else if (d.Type == 'Duyệt yêu cầu') {
                        $('#CountForAccountant4').html(d.Total);
                    }
                    else if (d.Type == 'Cài đặt bảng giá tháng mới') {
                        $('#CountForAccountant5').html(d.Total);
                    }
                });
                if (data.Data.length > 0) {
                    $('#LastReadAccountant').html('Cập nhật lúc: ' + kendo.toString(kendo.parseDate(data.Data[0].RowLastUpdatedAt), 'HH:mm dd-MM-yyyy'));
                }
            }
        });
    }
    else {
        $.ajax({
            url: r + "/PortalMessage/GetMessage",
            type: 'POST',
            success: function (data) {
                var html = '<div class="row"><div class="col-sm-12" style="margin-bottom: 10px;text-align:right;max-height: 400px;overflow-y: scroll;">';
                $.each(data.Data, function (index, d) {
                    html += '<a data-link="' + d.Link + '" href="#" onclick="ClickMessage(this)" data-id="' + d.RowID + '"  class="list-group-item ';
                    if (d.IsRead == 2) {
                        html += 'disabled';
                    }
                    else {
                        html += 'active';
                    }
                    html += '"><h6 class="list-group-item-heading" style="font-size: 12px;">' + d.Description + '</h6>';
                    html += ' <p class="text-right"><small><span class="glyphicon glyphicon-time"></span>' + kendo.toString(kendo.parseDate(d.RowCreatedAt), "HH:mm dd/MM/yyyy") + '</small>';
                    html += ' Từ ' + d.RowCreatedBy + '</p>';
                    html += ' </a>';
                });
                html += '</div><br/><a href="' + r + '/PortalMessage" style="display:block;text-align:center"> Xem thêm</a></div>';
                $(".popover-content-profile").html(html).fadeIn("fast");
            }
        });
    }
}
function ClickMessage(e) {
    $.post(r + "/PortalMessage/UpdateClick?id=" + $(e).data('id'));
    window.location = r + $(e).data('link');
}