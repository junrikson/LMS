﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LaporanPendapatan.aspx.vb" Inherits="LMS.LaporanPendapatan" %>


<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
	
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LIGITA - Laporan Pendapatan</title>
  <link href="../css/style.css" type="text/css" rel="stylesheet" media="screen" />
  <script type="text/javascript">

      function viewReports() {

          var statusbyr = document.getElementById('DDLStatus').value;
          var pilihan = document.getElementById('DDLPilih');
          var opt = pilihan.value;
  	var datefrom = tbStartDate.GetDate();
  	var dateto = tbEndDate.GetDate();
  	var firstdayofyear = '01/01/' + datefrom.getFullYear();
//		var coastart = document.getElementById('DDLCOAFrom');
//		var coaend = document.getElementById('DDLCOATo');

		var strs;

		var strdatefrom = (datefrom.getMonth()+1) + '/' + datefrom.getDate() + '/' + datefrom.getFullYear();
		var strdateto = (dateto.getMonth()+1) + '/' + dateto.getDate() + '/' + dateto.getFullYear();
  	var str;
  	
  		str = "&transdatefrom=" + strdatefrom + "&transdateto=" + strdateto;
//  		str = str + "&accountnostart=" + coastart.value;
//  		str = str + "&accountnoend=" + coaend.value;
  		str = str + "&firstdayofyear=" + firstdayofyear.value;
  		strs = "[Trans Date: " + strdatefrom + "-" + strdateto + "]";
//  						"[" + coastart.options[coastart.selectedIndex].text + "]" +
//  						"[" + coaend.options[coaend.selectedIndex].text + "]";
  		
  		str = str + "&str=" + strs;
  		
  		var windowprops = 'toolbar=0,location=0,directories=0,status=0, ' +
												'menubar=0,scrollbars=1,resizable=1,width=500,height=200';
  		window.open('ReportAcc.aspx?rpt=rptPendapatan' + str + "&tipe=" + opt + "&statbyr=" + statusbyr, 'rptPendapatan', windowprops);
  	}
  </script>
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
   <div class="formtitle">
        <strong>laporan Pendapatan</strong>    
    </div>
	<br />
    
    <div class="div_input">
        <div class="div_umum">
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="DDLPilih" runat="server">
                    <asp:ListItem Value="Semua">Pendapatan</asp:ListItem>
                    <asp:ListItem Value="Pjk">Pendapatan Pjk</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="DDLStatus" runat="server">
                    <asp:ListItem Value="Semua">Semua</asp:ListItem>
                    <asp:ListItem Value="Lunas">Lunas</asp:ListItem>
                    <asp:ListItem Value="BelumLunas">Belum Lunas</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
            <table style = " border: 2px dotted #000000;">
			    <tr>
				    <td>Date :</td>
				    <td>
					    <dxe:ASPxDateEdit ID="tbStartDate" ClientInstanceName="tbStartDate" runat="server" Width="150px" EditFormat="Custom" EditFormatString="dd MMMM yyyy"
						    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
						    CssPostfix="Office2003_Olive" ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
						    <ButtonStyle Cursor="pointer" Width="13px">
						    </ButtonStyle>
					    </dxe:ASPxDateEdit>
				    </td>
				    <td>-</td>
				    <td>
					    <dxe:ASPxDateEdit ID="tbEndDate" ClientInstanceName="tbEndDate" runat="server" Width="150px" EditFormat="Custom" EditFormatString="dd MMMM yyyy"
						    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
						    CssPostfix="Office2003_Olive" ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
						    <ButtonStyle Cursor="pointer" Width="13px">
						    </ButtonStyle>
					    </dxe:ASPxDateEdit>
				    </td>
				    <td width="300px">
					    <input id="Button1" type="button" value="Submit" onclick="javascript:viewReports();" />
				    </td>
			    </tr>
		    </table>
    </div>
    </div>
    </form>

</body>
</html>
