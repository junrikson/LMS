<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Konfigurasi_SettingRoles" Codebehind="SettingRoles.aspx.vb" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Setting Roles</title>
    <link href="../css/main.css" type="text/css" rel="Stylesheet" />
	<link href="../css/RoundCorner.css" type="text/css" rel="Stylesheet" />
	<link href="../css/Style.css" type="text/css" rel="Stylesheet" />
	<script type="text/javascript">
        function reloadframe() {
            top.menu.location.reload(true);
        }
    </script>
	
    <style type="text/css">
        .style1
        {
            text-align: left;
            padding-left: 20px;
            width: 148px;
        }
    </style>
</head>
<body class="mainmenu" id=body1 runat="server">
    <form id="form1" runat="server">
        <div class ="Divutama">
			
			<div class="formtitle">Setting Roles</div>
			<br />
			<div class="div_input">
			<div class="div_umum">
					
			
				<table style="border:dotted 2px black;">
					<tr>
						<td class="label">Role :</td><td class="style1"><asp:DropDownList ID="DDLRoles" runat="server" AutoPostBack="true"></asp:DropDownList></td>
					</tr>
					<tr>
					    <td colspan = "2">
					        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
				            <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
					    </td>
					</tr>
					<tr>
					    <td colspan = "2">
					        <table>
					            <tr>
					                <td>
					                    <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
									<Image Url="../images/save-alt.png" />
								</dxe:ASPxButton>
					    </td>
					    <td>
					        <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
									<Image Url="../images/undo.png" />
								</dxe:ASPxButton>
					    </td>
					    <td>
					        &nbsp;</td>
					            </tr>
					        </table>
					        
					    </td>
					</tr>
				</table>
				<asp:HiddenField ID="hfID" runat="server" />
				<asp:HiddenField ID="hfMode" runat="server" />
			
			<br />
			
				<table cellpadding="0" cellspacing="0" style="width: 100%">
					<tr valign="top">
						<td rowspan="2" style="width:100%;">
						    <asp:Repeater ID="rptMenu" runat="server">
  	                            <HeaderTemplate>
  	                            <table width="90%">
  	                            </HeaderTemplate>
  	                            <ItemTemplate>
  	                            <tr>
  	                                <td>
  	                                    <strong><asp:Label ID="lblHeader" runat="server"></asp:Label> : </strong> 
  	                                    
  	                                </td>
  	                            </tr>
  	                            <tr>
                                    <td>  	                
  	                                    <asp:CheckBoxList ID="cblChild" runat="server" RepeatColumns="4" Width="100%"></asp:CheckBoxList>
  	                                </td>
  	                            </tr>
  	                            </ItemTemplate>
  	                            <AlternatingItemTemplate>
  	                            <tr>
  	                                <td>
  	                                    <strong><asp:Label ID="lblHeader" runat="server"></asp:Label> : </strong> 
  	                                    
  	                                </td>
  	                            </tr>
  	                            <tr>
                                    <td>  	                
  	                                    <asp:CheckBoxList ID="cblChild" runat="server" RepeatColumns="4" Width="800px"></asp:CheckBoxList>
  	                                </td>
  	                            </tr>
  	                            </AlternatingItemTemplate>
  	                            <SeparatorTemplate>
  	                            <tr>
  	                                <td>&nbsp;</td>
  	                            </tr>
  	                            </SeparatorTemplate>
  	                            <FooterTemplate>
  	                            </table>
  	                            </FooterTemplate>
  	                        </asp:Repeater>  
						    
							<%--<table><tr><td>
							<strong>Accounting :</strong>
							</td></tr>
							<tr><td>
							<asp:CheckBoxList ID="cblmenuaccounting" runat="server" RepeatColumns="4">
							</asp:CheckBoxList>
							<br />
							</td></tr>
							<tr><td>
							<strong>Inventory :</strong>
							<asp:CheckBoxList ID="cblMenuInventory" runat="server" RepeatColumns="4">
							</asp:CheckBoxList>
							<br />
							</td></tr>
							<tr><td>
                            <strong>Konfigurasi :</strong><br />
                            <asp:CheckBoxList ID="cblMenuKonfigurasi" runat="server" RepeatColumns="4">
                            </asp:CheckBoxList>
							<br />
							</td></tr>
							<tr><td>
							<strong>Report :</strong>
							<asp:CheckBoxList ID="cblMenuReport" runat="server" RepeatColumns="4">
							</asp:CheckBoxList >
							<br />
							</td></tr>
							<tr><td>
							<strong>
							Sales :</strong>
							<asp:CheckBoxList ID="cblMenuSales" runat="server" RepeatColumns="4">
							</asp:CheckBoxList >
							<br />
							</td></tr>
							<tr><td>
							<strong>Master</strong>
							<asp:CheckBoxList ID="cblMenuMaster" runat="server" RepeatColumns="4">
							</asp:CheckBoxList >
						    <br />
						    </td></tr></table>--%>
						</td>
						
					</tr>
					
				</table>
				
			</div>
			</div>
		
    </div>
    </form>
</body>
</html>
