<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TutupBuku.aspx.vb" Inherits="LMS.TutupBuku" %>


<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
	
	
  
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LIGITA - Tutup Buku </title>
     <link href="../css/style.css" type="text/css" rel="stylesheet" media="screen" />
  <script language="JavaScript" src="../script/main.js" type ="text/javascript"></script>	

</head>
<body class="mainmenu">
    <form id="form1" runat="server">
    <div class="formtitle">
        <strong>Tutup Buku</strong> 
    </div>
		<br />
  <div class="div_input" style = "width:20%;">
            <div class="div_umum" style = "width:100%;">
            <asp:Label ID="LabelInfo" runat = "server" ForeColor ="Red" ></asp:Label>
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
				<td> <asp:Button ID="BtnButton" runat="server" Text = "Submit" /> </td>
			</tr>
			<tr>
			    <td colspan="2"><asp:Label ID="Lblberhasil" runat = "server" ForeColor ="Green" Visible="False"></asp:Label></td>
			</tr>
		</table>
	</div>
  </div>
    </form>

</body>
</html>
