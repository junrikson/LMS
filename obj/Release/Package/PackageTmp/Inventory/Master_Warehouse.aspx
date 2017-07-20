<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Inventory_Master_Warehouse" Codebehind="Master_Warehouse.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gudang</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
	<link href="../css/RoundCorner.css" type="text/css" rel="Stylesheet" />

</head>
<body class= "mainmenu">
    <form id="form1" runat="server">
        <div class="Divutama">
           
			<div class="formtitle"><b>Master Warehouse</b></div>
			<br />
			
			
			<div class="div_input">
            <div class="div_umum">
            <table >
                <tr>
                    <td>
                        <table style="width: 40%" class = "borderdot">
					<tr>
						<td class="label">Kode Gudang :</td>
						<td class="input"> 
						<asp:Label ID = "lblKode" runat="server" ></asp:Label></td>
					</tr>
					<tr>
						<td class="label" align="center">Nama Gudang:</td>
						<td class="input"><asp:TextBox ID="TxtNama" runat="server" CssClass="tb1" Width="300" MaxLength="50"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="label" align="center">Location:</td>
						<td class="input"><asp:TextBox ID="TxtLocation" runat="server" CssClass="tb1" Width="300" MaxLength="50"></asp:TextBox></td>
					</tr>
					<tr>
					    <td colspan = "2" align = "center">
					        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			                <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					    </td>
					</tr>
					<tr>
					    <td width="25%" align="right">
							    <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
								    <Image Url="../images/save-alt.png" />
								    <ClientSideEvents Click="function(s, e) { 
								                                                e.processOnServer = confirm('Anda Yakin ingin Menyimpan Data Ini?'); 
								                                            }" />
							    </dxe:ASPxButton>
					    </td>
					    <td width="25%" align="left">
				        	    <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
								    <Image Url="../images/undo.png" />
							    </dxe:ASPxButton>
					    </td>
					</tr>
				</table>
                    </td>
                </tr>
            </table>
				
				<asp:HiddenField ID="hfID" runat="server" />
				<asp:HiddenField ID="hfMode" runat="server" />
			
			
			
				
							<br />
			
				
							<dxwgv:ASPxGridView ID="Grid_Warehouse" ClientInstanceName="grid" runat="server" KeyFieldName="ID" 
								Width="50%" AutoGenerateColumns="false">			
								<Styles>
									<Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#ffffff" Font-Bold="true" HorizontalAlign=Center>
                                        <HoverStyle>
                                        <Border BorderColor="#515763"></Border>
                                        </HoverStyle>
                                    </Header>
									<FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
									<Row BackColor="#ffffff"></Row>
							
								</Styles>
								<SettingsPager AlwaysShowPager="True" PageSize="15">
                                </SettingsPager>
								<Settings ShowFilterRow="True"  />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
								<Columns>
									<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Warehouse_Code" caption = "Kode Gudang " VisibleIndex="1">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Warehouse_Name" caption = "Nama Gudang " VisibleIndex="2" >
									</dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn FieldName="Location" caption = "Lokasi" VisibleIndex="3" >
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
					
			
			</div>
			
        </div>
        </div>
    </form>
</body>

</html>
