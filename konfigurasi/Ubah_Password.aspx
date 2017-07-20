<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Konfigurasi_Ubah_Password" Codebehind="Ubah_Password.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ubah Password</title>
    <link href="../css/main.css" type="text/css" rel="Stylesheet" />
	<link href="../css/RoundCorner.css" type="text/css" rel="Stylesheet" />
	<link href="../css/Style.css" type="text/css" rel="Stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 92px;
        }
        .style2
        {
            width: 95px;
        }
        .style3
        {
            width: 112px;
        }
        .style4
        {
            width: 117px;
        }
        .style5
        {
            width: 132px;
        }
    </style>
</head>
<body class="mainmenu">
<div class ="Divutama"> 
    <form id="form1" runat="server">
    
        
			<div class="formtitle">
			<b>Ubah Password</b></div>
			<br />
			<div class = "div_input">
			  <div class = "div_umum">
				<table>
				<tr>
						<td class="style5">User Name:</td><td class="style1" colspan="2">
                        <asp:Label ID="LblUserName" runat="server"></asp:Label>
                        </td>
					</tr>
					<tr>
						<td class="style5">Password :</td><td class="style1" colspan="2"><asp:TextBox ID="tbpass" 
										runat="server" CssClass="tb1" Width="100" MaxLength="10" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style5">Confirm Password :</td><td class="style1" colspan="2">
						<asp:TextBox ID="tbconfirm" runat="server" CssClass="tb1" Width="100" 
							MaxLength="10" TextMode="Password"></asp:TextBox></td>
					</tr>
					<tr>
					    <td colspan = "3" align = "center">
					        
					        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
					        <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
					    </td>
					    </tr>
					    <tr>
						<td class="style4">&nbsp;</td><td class="style2">
								<dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
									<Image Url="../images/save-alt.png" />
								</dxe:ASPxButton>
									</td>
					    <td class="style3">
							<dxe:ASPxButton ID="btKeluar" runat="server" Text="Keluar" Width="90px">
								<Image Url="../images/door.png" />
							</dxe:ASPxButton>
									</td>
					</tr>
				</table>
			</div>
			</div>
			</div>
		
    
    </form>
</div>
</body>
</html>
