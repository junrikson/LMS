<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Inventory_Approve_Item" Codebehind="Approve_Item.aspx.vb" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approve Item</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style1
        {
            width: 100px
        }
        .style2
        {
            width: 6px;
        }
        .style3
        {
            width: 278px;
        }
        </style>
<script language="javascript" src="../script/main.js" ></script>
<script type="text/javascript" language="javascript">
 function getCustomer() {
    returnValue = ShowDialog2('MasterCustomer', 'Arg', '610', '450');
    if (returnValue) {
        var comp = new Array();
        comp = returnValue.split(";");
        var ID = document.getElementById("TxtKodeCost");
        ID.value = comp[2];
        var Name = document.getElementById("TxtNameCost");
        Name.value = comp[1];
        var ID = document.getElementById("hfCID")
        ID.value = comp[0]
    }
}
</script>
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
    <div class ="Divutama"> 
    <asp:Panel ID="Panel1" runat ="server" Height="54px" style="margin-bottom: 244px">
        <div class="formtitle">
            Approve Item  
        </div>
        <br />
         <div class="div_input">
            <div class="div_umum">
                
                <table style="width:100%;" align="center" bgcolor="White">
                    <tr>
                        <td class="style1">
                            Customer Name </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtNameCost" runat="server"></asp:TextBox>
                            <img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
                        </td>
                    </tr>
                     <tr>
                        <td class="style1">
                            Customer Code </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtKodeCost" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Jenis Barang </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtBarang" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Kode Barang </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:Label ID="lblKodeItem" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Berat Barang </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtBerat" runat="server"></asp:TextBox>
                        </td>
                     </tr>
                     <tr>
                        <td class="style1">
                            Panjang Barang </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtPanjang" runat="server"></asp:TextBox>
                        </td>
                     </tr>
                     <tr>
                        <td class="style1">
                            Lebar Barang </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtLebar" runat="server"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td class="style1">
                            Tinggi Barang </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtTinggi" runat="server"></asp:TextBox>
                        </td>
                     </tr>
                     <tr>
                        <td class="style1">
                            Unit Barang </td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtUnit" runat="server"></asp:TextBox>
                        </td>
                     </tr>
                    <tr>
                        <td >
					        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			                <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					    </td>
                    </tr>
                     <tr>
                        <td >
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
         </div>
         </div>
         <div align =center> 
                 <asp:DropDownList ID="DDLStatusItem" runat="server" Height="21px" Width="166px" AutoPostBack="true">
                                <asp:ListItem Value="0">Approve</asp:ListItem>
                                <asp:ListItem Value="1">Pending</asp:ListItem>
                            </asp:DropDownList>
         </div>                   
        <br />
        
         <asp:Panel ID="PanelApprove" runat="server">
            <div class="div_input">
                <div class="div_umum">
                    <div align="center">
                 <br />
                    <dxwgv:ASPxGridView ID="GridItem_Approve" runat="server" 
                        AutoGenerateColumns="False" Font-Size="9pt" KeyFieldName="ID" 
                        Width="569px" ClientInstanceName = "grid">
                        <styles>
                            <header backcolor="#2c3848" font-bold="true" forecolor="#ffffff" 
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
						<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                                Visible="false" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Customer ID" FieldName="CustomerID" 
                                Name="CustomerID"  Visible="false" VisibleIndex="1" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Customer Name" FieldName="NamaKostumer" 
                                Name="NamaKostumer" VisibleIndex="2" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Customer Code" FieldName="KodeKostumer" 
                                Name="KodeKostumer" VisibleIndex="3" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                             <dxwgv:GridViewDataTextColumn Caption="Kode Barang" FieldName="KodeBarang" 
                                Name="KodeBarang" VisibleIndex="4" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Jenis Barang" FieldName="NamaBarang" 
                                Name="NamaBarang" VisibleIndex="5" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Berat Barang" FieldName="Berat" 
                                Name="Berat" VisibleIndex="6" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Panjang Barang" FieldName="Panjang" Name="Panjang" 
                                VisibleIndex="7" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Lebar Barang" FieldName="Lebar" Name="Lebar" 
                                VisibleIndex="8" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tinggi Barang" FieldName="Tinggi" Name="Tinggi" 
                                VisibleIndex="9" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unit Barang" FieldName="Unit" Name="Unit" 
                                VisibleIndex="10" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="11" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbedit" runat="server" CommandName="Edit" 
                                        ToolTip="Edit Item">Edit</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="12" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbdelete" runat="server" CommandName="Delete" 
                                        ToolTip="Delete Item">Delete</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                         </Columns>
                    </dxwgv:ASPxGridView>
			    <br />
			    </div>
        	  </div>
            </div>
            </asp:Panel>
            <asp:Panel ID="PanelPending" runat="server">
            <div class="div_input">
                <div class="div_umum">
                    <div align="center">
                 <br />
                    <dxwgv:ASPxGridView ID="GridItem_Pending" runat="server" 
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
						<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                                Visible="false" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Customer ID" FieldName="CustomerID" 
                                Name="CustomerID"  Visible="false" VisibleIndex="1" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Customer Name" FieldName="NamaKostumer" 
                                Name="NamaKostumer" VisibleIndex="2" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Customer Code" FieldName="KodeKostumer" 
                                Name="KodeKostumer" VisibleIndex="3" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                             <dxwgv:GridViewDataTextColumn Caption="Kode Barang" FieldName="KodeBarang" 
                                Name="KodeBarang" VisibleIndex="4" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Jenis Barang" FieldName="NamaBarang" 
                                Name="NamaBarang" VisibleIndex="5" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Berat Barang" FieldName="Berat" 
                                Name="Berat" VisibleIndex="6" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Panjang Barang" FieldName="Panjang" Name="Panjang" 
                                VisibleIndex="7" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Lebar Barang" FieldName="Lebar" Name="Lebar" 
                                VisibleIndex="8" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tinggi Barang" FieldName="Tinggi" Name="Tinggi" 
                                VisibleIndex="9" Width="150px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Unit Barang" FieldName="Unit" Name="Unit" 
                                VisibleIndex="10" Width="200px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="11" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbedit" runat="server" CommandName="Edit" 
                                        ToolTip="Edit Item">Edit</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="12" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbdelete" runat="server" CommandName="Delete" 
                                        ToolTip="Delete Item">Delete</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Approve" VisibleIndex="13" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbapprove" runat="server" CommandName="Approve" 
                                        ToolTip="Delete Item">Approve</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                         </Columns>
                    </dxwgv:ASPxGridView>
			    <br />
			    </div>
        	  </div>
            </div>
		 </asp:Panel>
        
         <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfCID" runat="server"></asp:HiddenField>
    </asp:Panel>
        </div>        
    </form>
</body>
</html>
