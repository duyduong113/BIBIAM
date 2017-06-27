var glbOrder = '';
var listen = new Firebase(accountfb);
listen.on("child_changed", function (snapshot) {
	Fetchdata(snapshot.val());
});
document.addEventListener('DOMContentLoaded', function () {
	if (!Notification) {
		alert('Trình duyệt của bạn chưa hỗ trợ thông báo. Vui lòng chuyển sang Chorme.');
		return;
	}
	if (Notification.permission !== "granted")
		Notification.requestPermission();
});
function Fetchdata(order) {
	$.ajax({
		type: "POST",
		async: false,
		url: r + "/FireBaseIncoming/Order",
		data: { key: 'TEST', orderid: order },
		success: function (data) {
			if (data.success) {
				if (data.magh == magh) {
					if (data.data.length > 0) {
						for (var i = 0; i < data.data.length; i++) {
							if (data.data[i].ma_gian_hang == magh) {
								notifyOrder(data.data[i]);
							}
						}
					}
				} else {
					return false;
				}
			} else {
				return false;
			}
		}
	});
}
function notifyOrder(data) {
	if (Notification.permission !== "granted")
		Notification.requestPermission();
	else {
		var notification = new Notification('Thietbinhanh - Thông báo', {
			icon: r + '/Content/images/logo.png',
			body: "Khách hàng:"+data.hoten + " vừa đặt đơn hàng mã số:" + data.ma_don_hang,
		});
		notification.onclick = function () {
			window.open(r + "/Merchant_Order");
		};
	}
}

