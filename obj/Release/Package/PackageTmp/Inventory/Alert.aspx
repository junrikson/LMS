<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Alert.aspx.vb" Inherits="LMS.Alert" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Konfigurasi Alert</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style1
        {
            width: 80px;
        }
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama">
        
        <div class="formtitle"><b>Alert</b></div>
        <br />
        <div class="div_input">
        <table cellpadding="5">
            <tr>
                <td>
                    <table cellpadding="5" class = "borderdot">
                        <tr>
                            <td class = "style1" >
                                Mulai Alert
                            </td>
                            <td>:&nbsp;<asp:TextBox ID = "TxtAlert" runat = "server" Width = "50px"></asp:TextBox></td>
                            <td><asp:Label ID = "LblAlert" runat = "server" Text = "Hari" ></asp:Label></td>
                        </tr>
                        <tr>
                            <td align ="right" >                    
                            <dxe:ASPxButton ID="btSave" runat="server" Text="Simpan" >
                            <Image Url="../images/save-alt.png" />
                            </dxe:ASPxButton>
                            </td>
                            <td align ="left" >                    
                            <dxe:ASPxButton ID="btReset" runat="server" Text="Reset" >
                            <Image Url="../images/undo.png" />
                            </dxe:ASPxButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblError" runat="server" CssClass = "error" Visible="false" ></asp:Label>                        
        			            <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>    
                </td>
                <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        
                <td>
                    <dxwgv:ASPxGridView ID="Grid_Alert" runat="server" 
                        AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDK" 
                        Width="200px" ClientInstanceName = "grid_kapal">
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
                        <SettingsPager AlwaysShowPager="True" PageSize="5" Mode="ShowAllRecords"></SettingsPager>
                        <Settings ShowFilterRow="True"  />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="IDK" Name="IDK" 
                                Visible="false" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Lama Alert" FieldName="Alert_Time" 
                                Name="Alert_Time"  VisibleIndex="1" Width="140px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="2" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are You Sure Want to Delete ?');" 
                                        ToolTip="Delete Barang">Delete</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                           <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="3" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                        ToolTip="Edit Barang">Edit
                                        </asp:LinkButton>
                                        
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                        </Columns>
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
            
        </div>
    </div>
    	<asp:HiddenField ID="hfMode" runat="server" />    
    	<asp:HiddenField ID="hfID" runat="server" />    
    </form>
</body>
</html>
