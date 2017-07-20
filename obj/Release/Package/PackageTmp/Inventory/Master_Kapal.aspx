<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Master_Kapal.aspx.vb" Inherits="LMS.Master_Kapal" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ship Input</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style1
        {
            width: 40px;
        }
     </style>
<script language="javascript" type="text/javascript" src="../script/main.js" ></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle"><b>Input Kapal</b></div>
		<br />
		<div  class="div_input" >
		<div class="div_umum">
            <table >
                <tr>
                    <td>
                        <table class = "borderdot">
                            <tr>
                    <td style = " width : 80px" >
                        Nama Kapal
                    </td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID = "TxtNamaKapal" runat = "server" ></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td style = " width : 100px">
                        Nahkoda Kapal
                    </td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID = "TxtNahkoda" runat = "server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style = " width : 100px">
                        Singkatan Kapal
                    </td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID = "TxtSingkatanKapal" runat = "server" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Kapal Milik
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLMilik" runat="server">
                            <asp:ListItem Text="Sendiri" Value="Sendiri">Sendiri</asp:ListItem>
                            <asp:ListItem Text="PerusahaanLain" Value="PerusahaanLain">Perusahaan Lain</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan = "3">
                        <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
				        &nbsp;<asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
					        <tr>
		                        <td align="right" >
			                        <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
				                        <Image Url="../images/save-alt.png" />
				                        
			                        </dxe:ASPxButton>
		                        </td>
		                        <td align="left" >
        	                        <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
				                        <Image Url="../images/undo.png" />
			                        </dxe:ASPxButton>
		                        </td>        
		                    </tr>
					    </table>
					</td>
                </tr></table>
                        </table>
                    </td>
                </tr>
            
                
            </table>
         
        <br />
        
        
            <dxwgv:ASPxGridView ID="Grid_Kapal" runat="server" AutoGenerateColumns = "false" ClientInstanceName = "grid"
                 KeyFieldName = "ID" Font-Size = "9pt" Width = "500px">
                <SettingsBehavior AllowFocusedRow="True" />
                <styles>
                    <header backcolor="#2c3848" font-bold="true" forecolor="#FFFFFF" 
                        horizontalalign="Center" hoverstyle-border-bordercolor="#515763">
                        <hoverstyle>
                            <border bordercolor="#515763"></border>
                        </hoverstyle>
                    </header>
                    <FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
                    
                    <Row BackColor="#ffffff"></Row>
                </styles>
                    <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
                    <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                        Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nama Kapal" FieldName="Nama_Kapal" 
                        Name="Nama_Kapal"  Visible="true" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nahkoda Kapal" FieldName="Nahkoda_Kapal" 
                        Name="Nahkoda_Kapal"  Visible="true" VisibleIndex="2" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Singkatan Kapal" FieldName="Alias_Kapal" 
                        Name="Alias_Kapal"  Visible="true" VisibleIndex="3" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ket Kapal" FieldName="KeteranganKapal" 
                        Name="KeteranganKapal"  Visible="true" VisibleIndex="4" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="5" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                ToolTip="Edit Barang">Edit</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="6" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                ToolTip="Delete Barang" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            
            <br />
            </div>
        </div>
    </div>
	<asp:HiddenField ID="hfID" runat="server" />
	<asp:HiddenField ID="hfMode" runat="server" />
    </form>
</body>
</html>
