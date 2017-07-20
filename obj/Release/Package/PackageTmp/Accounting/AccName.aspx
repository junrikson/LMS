<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccName.aspx.vb" Inherits="LMS.AccName" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Account Name </title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style1
        {
            width: 115px
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
   <script type= "text/javascript" language="javascript" src="../script/main.js" ></script>
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
    <div class ="Divutama"> 
    <asp:Panel ID="Panel1" runat ="server" Height="54px" style="margin-bottom: 244px">
        <div class="formtitle">
            <b>Account Name</b>  
        </div><br />
            <div class="div_input">
                <div class="div_umum">
                    <table style="width: 516px" >
                    <tr>
                        <td>
                            <table style="width:95%;" class = "borderdot">
                    <tr>
                        <td class="style1">
                            Nomor Akun</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtAccCode" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td class="style1">
                            Nama Akun</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtAccName" runat="server" Width="300px" ></asp:TextBox>
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
                                        <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px" 
                                            style="height: 27px">
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
                
                </div>
            </div>
            <div class="div_input">
                <div class="div_umum">
                    <div align="center">
                 <br />
                    <dxwgv:ASPxGridView ID="Grid_Account" runat="server" 
                        AutoGenerateColumns="False" Font-Size="9pt" KeyFieldName="ID" 
                        Width="569px" ClientInstanceName = "grid">
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
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
						<Settings ShowFilterRow="True"  />
                        <SettingsBehavior AllowFocusedRow="True" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                                Visible="false" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption=" Nomor Akun" FieldName="AccCode" 
                                Name="AccCode" VisibleIndex="1" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Nama Akun" FieldName="AccName" 
                                Name="AccName" VisibleIndex="2" Width="120px">
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
        	  </div>
            </div>
        
         <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfAID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="HFNamaCustomer" runat="server"></asp:HiddenField>
         <asp:HiddenField id="HFkodeakun" runat="server"></asp:HiddenField>
    </asp:Panel>
        </div>        
    </form>
</body>

</html>
