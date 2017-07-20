<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NeracaLajurb.aspx.vb" Inherits="LMS.NeracaLajurb" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LIGITA - Neraca Lajur</title>
     <link href="../css/style.css" type="text/css" rel="stylesheet" media="screen" />
  <script language="JavaScript" src="../script/main.js" type ="text/javascript"></script>	
  <script type="text/javascript">
      function viewReports() {
          var date = tbDate.GetDate();

          var strs;

          //		var strlaststartdate = date.getMonth() + '/01/' + date.getFullYear();
          //		var strlastenddate = date.getMonth() + '/' + getLastDays(date.getMonth(),date.getFullYear(),"total") + '/' + date.getFullYear();
          var strstartdate = (date.getMonth() + 1) + '/01/' + date.getFullYear();
          var strenddate = (date.getMonth() + 1) + '/' + getLastDays(date.getMonth() + 1, date.getFullYear(), "total") + '/' + date.getFullYear();
          var strfirstdayofyear = '01/01/' + date.getFullYear();
          var strlastmonth = getLastDays(date.getMonth(), date.getFullYear(), "last");
          var strcurrentmonth = getLastDays(date.getMonth() + 1, date.getFullYear(), "last");
          var strlastdayofyear = '12/31/' + date.getFullYear();
          var str;
          str = "&startdate=" + strstartdate + "&enddate=" + strenddate;
          str = str + "&firstdayofyear=" + strfirstdayofyear;
          //  		str = str + "&laststartdate=" + strlaststartdate + "&lastenddate=" + strlastenddate;
          str = str + "&lastmonth=" + strlastmonth;
          str = str + "&currentmonth=" + strcurrentmonth;
          str = str + "&lastdayofyear=" + strlastdayofyear;
          strs = "";

          str = str + "&str=" + strs;

          var windowprops = 'toolbar=0,location=0,directories=0,status=0, ' +
												'menubar=0,scrollbars=1,resizable=1,width=500,height=200';
          window.open('ReportAcc.aspx?rpt=NeracaLajur' + str, 'ReportNeraca', windowprops);
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
  </script>
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
    <div class="formtitle">
        <strong>Neraca Akhir Tahun Tutup</strong> 
    </div>
		<br />
  <div class="div_input" style = "width:20%;">
            <div class="div_umum" style = "width:100%;">
		<table  style = " border: 2px dotted #000000;" width = "90%">
			<tr>
				<td>Date :</td>
				<td>
					<dxe:ASPxDateEdit ID="tbDate" ClientInstanceName="tbDate" runat="server" Width="100px" EditFormat="Custom" EditFormatString="yyyy"
						CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
						CssPostfix="Office2003_Olive" ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
						<ButtonStyle Cursor="pointer" Width="13px">
						</ButtonStyle>
					</dxe:ASPxDateEdit>
				</td>
			</tr>
			<tr>
				<td></td>
				<td><input id="Button1" type="button" value="Submit" onclick="javascript:viewReports();" /></td>
			</tr>
		</table>
	</div>
  </div>
    </form>

</body>
</html>
