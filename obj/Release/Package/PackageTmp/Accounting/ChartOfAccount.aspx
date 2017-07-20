<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChartOfAccount.aspx.vb" Inherits="LMS.ChartOfAccount" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Chart Of Account</title>
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
    <div class="formtitle">
            <b>Chart Of Account</b>  
        </div><br />
           <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
            <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
        <asp:Panel ID="PanelInput" runat ="server">
            <div class="div_input">
                <div class="div_umum">
                    <div align="center">
                     <table style="width:100%;" class = "borderdot" >
                       <tr>
                            <td class="style1">
                              <asp:Label ID="lbltipeakun" runat = "server" Text ="Tipe Akun " ></asp:Label> </td>
                            <td class="style2">
                               <asp:Label ID="lbl0" runat = "server" Text =":" ></asp:Label> </td>
                            <td class="style3">
                                <asp:Label ID="lbltipeakunview" runat = "server" ></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td class="style1">
                              Pilih level </td>
                            <td class="style2">
                              :</td>
                            <td class="style3">
                                <asp:DropDownList ID = "ddlLevel" runat ="server" AutoPostBack="True" >
                                    <asp:ListItem Value="-Pilih Level-">-Pilih-</asp:ListItem>
                                    <asp:ListItem Value="1">Level 1</asp:ListItem>
                                    <asp:ListItem Value="2">Level 2</asp:ListItem>
                                    <asp:ListItem Value="3">Level 3</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                              <asp:Label ID="lbllevel1" runat = "server" Text ="level 2 " ></asp:Label> </td>
                            <td class="style2">
                               <asp:Label ID="lbl1" runat = "server" Text =":" ></asp:Label> </td>
                            <td class="style3">
                                <asp:DropDownList ID = "ddlLevel1" runat ="server" AutoPostBack="True" ></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="style1">
                              <asp:Label ID="lbllevel2" runat = "server" Text ="level 3 " ></asp:Label> </td>
                            <td class="style2">
                               <asp:Label ID="lbl2" runat = "server" Text =":" ></asp:Label> </td>
                            <td class="style3">
                                <asp:DropDownList ID = "ddlLevel2" runat ="server" AutoPostBack="True" ></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="style1">
                                Nomor Akun </td>
                            <td class="style2">
                                :</td>
                            <td class="style3">
                                <asp:Label ID="lblNoAkun" runat = "server" ></asp:Label><asp:Label ID="lblNoAKun2" runat = "server" ></asp:Label>
                                <asp:TextBox ID="TxtNoAkun" runat="server" MaxLength="4"></asp:TextBox>
                            </td>
                         </tr>
                         <tr>
                            <td class="style1">
                                Nama Akun </td>
                            <td class="style2">
                                :</td>
                            <td class="style3">
                                <asp:TextBox ID="TxtNamaAkun" runat="server" Width="208px"></asp:TextBox>
                               </td>
                         </tr>
                         <tr>
                            <td class="style1">
                                Lokasi
                            </td>
                            <td class="style2">
                                :
                            </td>
                            <td class="style3">
                                <asp:DropDownList ID = "DDLLokasi" runat ="server">
                                    <asp:ListItem Value="1" Text="Jakarta" />
                                    <asp:ListItem Value="2" Text="Pangkal Pinang" />
                                    <asp:ListItem Value="3" Text="Tanjung Pandan" />
                                    <asp:ListItem Value="4" Text="Gabungan" />
                                </asp:DropDownList>
                                
                            </td>
                         </tr>
                        <tr>
                            <td colspan = "3" align = "left" >
                             
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
                                            <dxe:ASPxButton ID="btBatal" runat="server" Text="Back" Width="90px">
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
                    </div>
                </div>
            </div>
        </asp:Panel>
        <br />
        <asp:Panel ID = "PanelView" runat ="server">
        <div class ="div_input">
            <div class = "div_umum ">
          
        <table style="width:100%;" class = "borderdot" >
           <tr>
                <td class="style1">
                  Tipe Akun</td>
                <td class="style2">
                   :</td>
                <td class="style3">
                   <asp:DropDownList ID="ddltipeakun" runat="server" AutoPostBack ="true"></asp:DropDownList><asp:HiddenField id="hfTipeAkun" runat="server"></asp:HiddenField>
                </td>
            </tr>
            <tr>
                <td align = "right">
                    <dxe:ASPxButton ID="btnAdd" runat="server" Text="Tambah" Width="90px">
                        <Image Url="../images/save-alt.png" />
                    </dxe:ASPxButton>
                </td>
                <td> &nbsp; </td>
                <td>&nbsp; </td>
            </tr>
        </table>
          </div>
        </div>
        <br />
              <dxwgv:ASPxGridView ID="Grid_COA" runat="server" 
                AutoGenerateColumns="False" Font-Size="9pt" KeyFieldName="ID" 
                Width="100%" ClientInstanceName = "grid">
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
				<Settings ShowFilterRow="True"  />
				<SettingsPager Mode="ShowPager" PageSize = "20"></SettingsPager>
                <SettingsBehavior AllowFocusedRow="True"  />
				<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                        Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Kode Akun" FieldName="Types" 
                        Name="Types"  Visible="false" VisibleIndex="1" Width="150px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Kode" FieldName="Code" 
                        Name="Code" VisibleIndex="2" Visible="true" Width="130px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nama Akun" FieldName="AccName" 
                        Name="AccName" VisibleIndex="3" Width="800px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Kode Parent " FieldName="Parent" 
                        Name="Parent" VisibleIndex="4" Visible="false" Width="120px">
                    </dxwgv:GridViewDataTextColumn>
                     
                    <dxwgv:GridViewDataTextColumn Caption="Nama Akun" FieldName="Name" 
                        Name="Name" VisibleIndex="5" Visible="false" Width="120px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Level" FieldName="Levels" 
                        Name="Levels" VisibleIndex="6" Width="70px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lokasi" FieldName="Lokasi" 
                        Name="Lokasi" VisibleIndex="7" Width="70px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="LokasiCode" FieldName="LokasiCode" 
                        Name="LokasiCode" VisibleIndex="8" Width="70px" Visible="false">
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
        </asp:Panel>
        <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>
        <asp:HiddenField id="hfLevel" runat="server"></asp:HiddenField>
        <asp:HiddenField id="hfCode" runat="server"></asp:HiddenField>
        <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
        <asp:HiddenField id="hfParent" runat="server"></asp:HiddenField>
        
    </div>
    </form>
</body>
</html>
