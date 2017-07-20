<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="KonfigurasiMenu.aspx.vb" Inherits="LMS.KonfigurasiMenu" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Konfigurasi Menu</title>
     <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style1
        {
            width: 74px
        }
        .style2
        {
            width: 6px;
        }
        .style3
        {
            width: 217px;
        }
        </style>


</head>
<body class="mainmenu">
    <form id="form1" runat="server">
    <div class ="Divutama"> 
        <div class="formtitle">
            <b>Konfigurasi Menu</b>  
        </div><br />
         <div class="div_input">
            <div class="div_umum">
                <table width="500px">
                    <tr>
                        <td>
                            <table class = "borderdot">

                     <tr>
                        <td class="style1">
                            Parent </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:DropDownList ID="DDLParent" runat="server">
                            <asp:ListItem Text = "-Pilih Parent-"></asp:ListItem>
                            <asp:ListItem Text = "Accounting" Value = "Accounting"></asp:ListItem>
                            <asp:ListItem Text = "Inventory" Value = "Inventory"></asp:ListItem>
                            <asp:ListItem Text = "Konfigurasi" Value = "Konfigurasi"></asp:ListItem>
                            <asp:ListItem Text = "Master" Value = "Master"></asp:ListItem>
                            <asp:ListItem Text = "Report" Value = "Report"></asp:ListItem>
                            <asp:ListItem Text = "Sales" Value = "Sales"></asp:ListItem>
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Child </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtChild" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            URL </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtUrl" runat="server" Width="100%"></asp:TextBox>

                        </td>
                     </tr>
                       
                     
                    <tr>
                        <td colspan = "3" align = "left" >
					        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			                <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					    </td>
                    </tr>
                     <tr>
                        <td colspan = "3" align = "left" >
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
                        </td>
                    </tr>
                </table>
               
         
       <br />       
                 <br />
                    <dxwgv:ASPxGridView ID="GridKonfigurasiMenu" runat="server" 
                        AutoGenerateColumns="False" Font-Size="9pt" KeyFieldName="ID" 
                        Width="80%" ClientInstanceName = "grid">
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
                        <SettingsPager PageSize="15">
                        </SettingsPager>
						<Settings ShowFilterRow="True"  />
                        <SettingsBehavior AllowFocusedRow="True" />
						<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                                Visible="false" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Parent" FieldName="Parent" 
                                Name="Parent"  VisibleIndex="1" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Child" FieldName="Child" 
                                Name="Child" VisibleIndex="2" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="URL" FieldName="URL" 
                                Name="URL" VisibleIndex="3" Width="50%">
                            </dxwgv:GridViewDataTextColumn>
                            
                            <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="11" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbedit" runat="server" CommandName="Edit" 
                                        ToolTip="Edit Item">Edit</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="12" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbdelete" runat="server" CommandName="Delete" 
                                        ToolTip="Delete Item" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            
                         </Columns>
                    </dxwgv:ASPxGridView>
			    <br />
			   
            </div>

        <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>


        </div>        
    </form>
</body>
</html>
