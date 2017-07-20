<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Konfigurasi_KonfigurasiRole" Codebehind="KonfigurasiRole.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Konfigurasi Role</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
	<link href="../css/RoundCorner.css" type="text/css" rel="Stylesheet" />


<script type="text/javascript" language="javascript">

//    function OnGridFocusedRowChanged() {
//        grid.GetRowValues(grid.GetFocusedRowIndex(), 'ID;RoleID;Nama', OnGetRowValues);
//    }

//    function OnGetRowValues(values) {

//        if (values[0] == null) {
//            values[0] = '';
//            document.getElementById("hfMode").value = 'Insert';
//        } else {
//            document.getElementById("TxtKode").disabled = false;
//            document.getElementById("hfMode").value = 'Update';
//        }
//        if (values[1] == null) values[1] = '';
//        if (values[2] == null) values[2] = '';
//        var kode = document.getElementById("TxtKode");
//        kode.value = values[1];
//        var nama = document.getElementById("TxtNama");
//        nama.value = values[2];
//        var id = document.getElementById("hfID");
//        id.value = values[0];

//    }

</script>

</head>
<body class="mainmenu">
    <form id="form1" runat="server">
        <div class ="Divutama">
            
			<div class="formtitle">Konfigurasi Roles</div>
			<br />
			<div class="div_input">
			<div class="div_umum">
				<table >
				    <tr>
				        <td>
				            <table class = "borderdot">
				                <tr>
						<td class="label">Kode :</td><td class="input"> 
                        <asp:TextBox ID="TxtKode" 
                            runat="server" CssClass="tb1" Width="300" MaxLength="50" Enabled="False"></asp:TextBox> </td>
					</tr>
					<tr>
						<td class="label" align="center">Nama :</td><td class="input"><asp:TextBox ID="TxtNama" runat="server" CssClass="tb1" Width="300" MaxLength="50"></asp:TextBox></td>
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
					                    <dxe:ASPxButton ID="btnew" runat="server" Text="Baru" Width="90px">
									    <Image Url="../images/file-edit.png" />
								</dxe:ASPxButton>
					                </td>
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
					            </tr>
					        </table>
					    </td>
					</tr>
				            </table>
				        </td>
				    </tr>
					
				</table>
				<br />
				<asp:HiddenField ID="hfID" runat="server" />
				<asp:HiddenField ID="hfMode" runat="server" />
				<asp:HiddenField ID="Hfroleid" runat="server" />
			
				<table width="100%" cellpadding="0" cellspacing="0">
					<tr valign="top">
						<td rowspan="2" width="75%">
							<dxwgv:ASPxGridView ID="grid_MasterRole" ClientInstanceName="grid" runat="server" KeyFieldName="ID" 
								Width="75%" AutoGenerateColumns="false">	
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
									<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="RoleID" Caption="Role ID" VisibleIndex="1">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Nama" Caption = "Nama Role" VisibleIndex="2" >
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
								    </Columns>
							    </dxwgv:ASPxGridView>			
						</td>
						<td width="25%" align="right">
						<div style="margin-bottom: 5px;">
								
							</div>
							<div style="margin-bottom: 5px;">
								
							</div>
							<div style="margin-bottom: 5px;">
								
							</div>
							<div style="margin-bottom: 5px;">
							</div>
						</td>
					</tr>
					<tr>
						<td valign="bottom" align="right">
							&nbsp;</td>
					</tr>
				</table>
				
			</div>
			<p></p>
			
        </div>
    </form>
</body>
</html>
