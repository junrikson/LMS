<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Konfigurasi_KonfigurasiUser" Codebehind="KonfigurasiUser.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Konfigurasi User</title>
     <link href="../css/main.css" type="text/css" rel="Stylesheet" />
	<link href="../css/RoundCorner.css" type="text/css" rel="Stylesheet" />
	<link href="../css/Style.css" type="text/css" rel="Stylesheet" />
	
	<script type="text/javascript" language="javascript">
	
//	function OnGetRowValues(values) {
//	
//		if (values[0] == null) {
//			values[0]='';
//			document.getElementById("hfMode").value = 'Insert';
//		} else {
//			document.getElementById("Txtpassword").disabled = true;
//			document.getElementById("TxtConfirmPassword").disabled = true;
//			document.getElementById("hfMode").value = 'Update';
//		}
//		if (values[1] == null) values[1]='';
//		if (values[2] == null) values[2]='';
//		var kode = document.getElementById("TxtKode");
//		kode.value = values[1];
//		var nama = document.getElementById("TxtNama");
//		nama.value = values[2];
//		var id = document.getElementById("hfID");
//		id.value = values[0];
//		
//	}

</script>
	
    <style type="text/css">
        .style1
        {
            width: 250px;
        }
    </style>
	
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
    <div class ="Divutama">
    
			<div class="formtitle" align=center><b>Konfigurasi User</b></div>
			<br />
			<div class="div_input">
			<div class="div_umum">
				<table >
				<tr>
				    <td>
				        <table class = "borderdot">
				            <tr>
						<td class="label">Role :</td><td class="input"><asp:DropDownList ID="DDLGolongan" runat="server" AutoPostBack="true"></asp:DropDownList></td>
					</tr>
					<tr>
						<td class="label">Nama :</td><td class="input"><asp:TextBox ID="TxtNama" runat="server" CssClass="tb1" Width="200" MaxLength="50"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="label">User Login :</td><td class="input">
                        <asp:TextBox ID="TxtKode" 
                            runat="server" CssClass="tb1" Width="200" MaxLength="10" TabIndex="1"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="label">Password :</td><td class="input">
                        <asp:TextBox ID="Txtpassword" 
										runat="server" CssClass="tb1" Width="200" MaxLength="10" TextMode="Password" TabIndex="2"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="label">Confirm Password :</td><td class="input">
						<asp:TextBox ID="TxtConfirmPassword" runat="server" CssClass="tb1" Width="200" 
							MaxLength="10" TextMode="Password" TabIndex="3"></asp:TextBox></td>
					</tr>
					<tr>
					<td colspan = "2" align = "center">
						<asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
					        <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
						</td> 
					
					</tr>
					<tr>
					    <td colspan = "2" align = "center">
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
					                <dxe:ASPxButton ID="btKeluar" runat="server" Text="Keluar" Width="90px">
								<Image Url="../images/door.png" />
							</dxe:ASPxButton>
					            </td>
					        </tr>
				        </table>
				    </td>
				</tr>
					
					    
					    </table>
					        
								
								
					    </td>
					</tr>
				</table>
				<asp:HiddenField ID="hfMode" runat="server" />
			
			
			
				<br />
							<dxwgv:ASPxGridView ID="grid_Master_User" ClientInstanceName="grid" 
                                runat="server" KeyFieldName="id" 
								Width="50%" AutoGenerateColumns="false">	
								<SettingsPager PageSize="10">
                            </SettingsPager>
                            <settings showfilterrow="True" />		
								<Styles>
									<Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#ffffff" Font-Bold="true" HorizontalAlign= "Center">
                                        <HoverStyle>
                                        <Border BorderColor="#515763"></Border>
                                        </HoverStyle>
                                    </Header>
									<FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
									<Row BackColor="#ffffff"></Row>
							
								</Styles>
								<Settings ShowFilterRow="True"  />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
								<Columns>
									<dxwgv:GridViewDataColumn FieldName="id" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="userid" Caption="User" VisibleIndex="1">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="namauser" Caption = "Nama" VisibleIndex="2" >
									</dxwgv:GridViewDataColumn>
	                                <dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="4" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>
    								
								    <dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="5" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>	
								    <dxwgv:GridViewDataColumn Name="Reset Password" Caption="#" VisibleIndex="6" Width="50px">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbReset" ToolTip="Reset Password" CommandName="ResetPassword" runat="server" OnClientClick="return confirm('Are You Sure Want to Reset password ?');" >Reset Password</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>								
								    </Columns>
							    </dxwgv:ASPxGridView>			
					
				</div>
			</div>
			<p>
                <asp:HiddenField ID="hiddenid" runat="server" />
            </p>
			</div>
			

    </form>
</body>
</html>
