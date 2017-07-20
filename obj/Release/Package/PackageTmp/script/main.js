// New JScript For Application on YUKI
// Mike
// Yuki
// November 2009

function ShowDialog(is_Mode, is_Arg, is_Width, is_Height) {
	
	return window.showModalDialog("../Menu/dialog.aspx?&mode=" + is_Mode,
																is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");

}

function ShowDialog2(is_Mode, is_Arg, is_Width, is_Height) {

    return window.showModalDialog("../Menu/dialog2.aspx?&mode=" + is_Mode,
																is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");

}
function DialogInvoice(is_Mode, is_Arg, is_Width, is_Height) {

    return window.showModalDialog("../Menu/DialogInvoice.aspx?&mode=" + is_Mode,
																is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");

}



function ShowDialog3(is_Mode, is_Arg, is_Width, is_Height) {

    return window.showModalDialog("../Menu/dialog3.aspx?&mode=" + is_Mode,
																is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");

}
function ShowDialog7(is_Mode, is_Arg, is_Width, is_Height) {
    
    return window.showModalDialog("../Menu/dialog7.aspx?&mode=" + is_Mode,
																is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");

}

function ShowDialog4(is_Mode, is_Arg, is_Width, is_Height) {

    return window.showModalDialog("../Menu/dialog4.aspx?&mode=" + is_Mode,
																is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");

}

																
function ShowItem(is_Mode, is_Arg, is_Width, is_Height) {
    
    return window.showModalDialog("../Menu/show_item.aspx?&mode=" + is_Mode,
																is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");
}


function Showdialogwarehouse(is_Mode,is_Arg, is_Width, is_Height) {

    return window.showModalDialog("../Menu/dialogwarehouse.aspx?&mode=" + is_Mode,
                                                                is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");


}
function Showdialogwarehouseitem(is_Mode, is_Quotation,is_Arg, is_Width, is_Height) {

    return window.showModalDialog("../Menu/dialogwarehouse.aspx?&mode=" + is_Mode + "&Quotation=" + is_Quotation,
                                                                is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");
}

function Showdialogcontainer(is_Mode, is_Arg, is_Width, is_Height) {

    return window.showModalDialog("../Menu/dialogcontainer.aspx?&mode=" + is_Mode ,
                                                                is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");
}

function ShowDialog2(is_Mode, is_cust,is_Arg, is_Width, is_Height) {

    return window.showModalDialog("../Menu/dialog2.aspx?&mode=" + is_Mode + "&customer=" + is_cust,
																is_Arg,
																"dialogWidth:" + is_Width + "; " +
																"dialogHeight:" + is_Height + "; " +
																"status:no; resizable:yes; help:no;");

}

function TextboxOnFocus(id) {
    document.getElementById(id).style.backgroundColor = "#d8e9c7";
    document.getElementById(id).style.color = "#000000";
}

function TextboxOnBlur(id) {
    document.getElementById(id).style.backgroundColor = "#FFFFFF";
    document.getElementById(id).style.color = "#000000"
}

function getLastDays(month, year, mode) {
    var m;
    var d;
    switch (month) {
        case 1:
            m = "January";
            d = 31;
            break;
        case 2:
            m = "February";
            if ((year % 4) == 0) {
                d = 29;
            } else {
                d = 28;
            }
            break;
        case 3:
            m = "March";
            d = 31;
            break;
        case 4:
            m = "April";
            d = 30;
            break;
        case 5:
            m = "May";
            d = 31;
            break;
        case 6:
            m = "June";
            d = 30;
            break;
        case 7:
            m = "July";
            d = 31;
            break;
        case 8:
            m = "August";
            d = 31;
            break;
        case 9:
            m = "September";
            d = 30;
            break;
        case 10:
            m = "October";
            d = 31;
            break;
        case 11:
            m = "November";
            d = 30;
            break;
        case 12:
            m = "December";
            d = 31;
            break;
    }

    if (mode == "last") {
        return d + " " + m + " " + year;
    } else {
        return d;
    }

}