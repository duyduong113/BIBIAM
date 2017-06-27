// Convert string to from fomat display dd/mm/yyyy --> yyyy/mm/dd with validate
// Author: BaoHV
function convertDate(str) {

    var ngay, thang, nam = "";
    if (str.length < 8 || str.length > 10) {
        return "";
    }
    var splitDate = str.split('/');
    if (splitDate.length != 3) {
        return "";
    }
    else {
        var day = splitDate[0];
        var month = splitDate[1];
        var year = splitDate[2];
        if (parseInt(day, 0) > 0 && parseInt(day, 0) <= 31) {
            ngay = day;
        }
        else {
            return "";
        }
        if (parseInt(month, 0) > 0 && parseInt(month, 0) <= 12) {
            thang = month;
        }
        else {
            return "";
        }
        if (parseInt(year, 0) > 1900 && parseInt(year, 0) <= 9999) {
            nam = year;
        }
        else {
            return "";
        }
    }
    return nam + "/" + thang + "/" + ngay;
}
//function convertDate(chuoi,daucach) {
//    var ngay, thang, nam = "";
//    if (chuoi.length < 8 || chuoi.length > 10) {
//        return "";
//    }
//    var splitDate = chuoi.split(daucach);
//    if (splitDate.length != 3) {
//        return "";
//    }
//    else {
//        var day = splitDate[0];
//        var month = splitDate[1];
//        var year = splitDate[2];
//        if (parseInt(day, 0) > 0 && parseInt(day, 0) <= 31) {
//            ngay = day;
//        }
//        else {
//            return "";
//        }
//        if (parseInt(month, 0) > 0 && parseInt(month, 0) <= 12) {
//            thang = month;
//        }
//        else {
//            return "";
//        }
//        if (parseInt(year, 0) > 1900 && parseInt(year, 0) <= 9999) {
//            nam = year;
//        }
//        else {
//            return "";
//        }
//    }
//    return nam + daucach + thang + daucach + ngay;
//}
//
function alertMessage(title, message, issuccess, timeout) {
    var time = 3000;
    if (timeout > 0 && timeout > time) {
        time = timeout;
    }
    if (issuccess) {
        $.gritter.add({
            title: title,
            text: message,
            class_name: 'gritter-success',
            time: time
        });
    }
    else {
        $.gritter.add({
            title: title,
            text: message,
            class_name: 'gritter-error',
            time: time
        });
    }
}
//Convert Currecy to format Number with ,
function currencyToNumber(currency) {
    if (currency == "") {
        return 0;
    }
    return parseFloat(currency.split(",").join(''));
}
// convert string number to format type float
function numberToCurrency(number) {
    return (parseFloat(number) + "").replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
// Lay khoang cach giữa 2 khoang thời gian
function dayNo(y, m, d) {
    return --m >= 0 && m < 12 && d > 0 && d < 29 + (
             4 * (y = y & 3 || !(y % 25) && y & 15 ? 0 : 1) + 15662003 >> m * 2 & 3
           ) && m * 31 - (m > 1 ? (1054267675 >> m * 3 - 6 & 7) - y : 0) + d;
}
function getDateRange(startDate, endDate) {

    var start = startDate.split("/");
    var end = endDate.split("/");
    var ngaydau = dayNo(+start[2], +start[1], +start[0]);
    var ngaycuoi = dayNo(+end[2], +end[1], +end[0]);
    var yearStart = new Date(convertDate(startDate)).getFullYear();
    var yearEnd = new Date(convertDate(endDate)).getFullYear();
    var songay = 0;
    if (yearStart == yearEnd) {
        songay = (ngaycuoi - ngaydau) + 1;
        if (ngaydau > ngaycuoi) {
            alertMessage("Có lỗi", "Ngày lên lớn hơn ngày xuống !", 0);
            return false;
        }
        return songay;
    }
    else {
        if (yearStart > yearEnd) {
            alertMessage("Có lỗi", "Ngày lên lớn hơn ngày xuống !", 0);
            return false;
        }
        else {
            var tongSoNgayYearStart = dayNo(+start[2], 12, 31);
            var tongSoNam = (parseInt(yearEnd) - yearStart);
            var tongSoNgayYearEnd = dayNo(+end[2], 12, 31);
            if (tongSoNam > 1) {
                // khi lớn hơn 2 năm
                var tongSoNgayLonHon2Nam = 0;
                for (i = 1; i < tongSoNam; i++) {
                    tongSoNgayLonHon2Nam = tongSoNgayLonHon2Nam + dayNo(+(yearStart + i), 12, 31);
                }
                songay = (tongSoNgayYearStart - (ngaydau - 1)) + tongSoNgayLonHon2Nam + ngaycuoi;
                return songay;
            }
            else {
                songay = (tongSoNgayYearStart - (ngaydau - 1)) + ngaycuoi;
                return songay;
            }

        }
    }

};