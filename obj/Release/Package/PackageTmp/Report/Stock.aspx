<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Stock.aspx.vb" Inherits="LMS.Stock" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   <title>Report Stock</title>
  <link href="../css/style.css" type="text/css" rel="stylesheet" media="screen" />
  <script type="text/javascript">

      function viewReports() {
          var Tujuan = document.getElementById('DDLTujuan');
          var Tujuancst = Tujuan.value;
          var Gudang = document.getElementById('DDLGudang');
          var Gudangcst = Gudang.value;
          var datefrom = tbTanggalMulai.GetDate();
          var dateto = tbTanggalSelesai.GetDate();
          var firstdayofyear = '01/01/' + datefrom.getFullYear();
          var strs;
          var strdatefrom = (datefrom.getMonth() + 1) + '/' + datefrom.getDate() + '/' + datefrom.getFullYear();
          var strdateto = (dateto.getMonth() + 1) + '/' + dateto.getDate() + '/' + dateto.getFullYear();
          var str;

          str = "&transdatefrom=" + strdatefrom + "&transdateto=" + strdateto;
          str = str + "&firstdayofyear=" + firstdayofyear.value;
          strs = "Tanggal:" + strdatefrom + "-" + strdateto;
          str = str + "&str=" + strs;
  		var windowprops = 'toolbar=0,location=0,directories=0,status=0, ' +
												'menubar=0,scrollbars=1,resizable=1,width=500,height=500';
  		window.open('ReportAcc.aspx?rpt=rptStock' + str + '&tujuan=' + Tujuancst + '&Gudang=' + Gudangcst, 'rptStock', windowprops);
}
  </script>
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
     <div class="formtitle">
        <strong>Laporan Stock Gudang</strong>    
    </div>
	<br />
    <div class="div_input">
        <div class="div_umum">
            <table style = " border: 2px dotted #000000;">
            <tr>
				    <td>Date :</td>
				    <td>
					    <dxe:ASPxDateEdit ID="tbStartDate" ClientInstanceName="tbTanggalMulai" runat="server" Width="150px" EditFormat="Custom" EditFormatString="dd MMMM yyyy"
						    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
						    CssPostfix="Office2003_Olive" ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
						    <ButtonStyle Cursor="pointer" Width="13px">
						    </ButtonStyle>
					    </dxe:ASPxDateEdit>
				    </td>
				    <td>-</td>
				    <td>
					    <dxe:ASPxDateEdit ID="tbEndDate" ClientInstanceName="tbTanggalSelesai" runat="server" Width="150px" EditFormat="Custom" EditFormatString="dd MMMM yyyy"
						    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
						    CssPostfix="Office2003_Olive" ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
						    <ButtonStyle Cursor="pointer" Width="13px">
						    </ButtonStyle>
					    </dxe:ASPxDateEdit>
				    </td>
			    </tr>
			    <tr>
				    <td>Tujuan :</td>
				    <td>
                        <asp:DropDownList ID="DDLTujuan" runat="server">
                        </asp:DropDownList>
				    </td>
				</tr>
				<tr>
				    <td>
				        Gudang :
				    </td>
				    <td>
				        <asp:DropDownList ID="DDLGudang" runat="server">
                        </asp:DropDownList>
				    </td>
				</tr>
				<tr>    
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
