<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Pengiriman_Barang.aspx.vb" Inherits="LMS.Pengiriman_Barang" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pengiriman Barang</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <script language="javascript" src="../script/NumberFormat.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
    function changenumberformat(control) {
        var conval = document.getElementById(control).value;
        if (conval.lastIndexOf(",") != conval.length - 1) {
            document.getElementById(control).value = FormatNumberBy3(conval.replace(/[.]/g, ""), ",", ".");
        }
        else {
            document.getElementById(control).value = conval;
        }
    }
    </script>
    
    <style type="text/css">
        .style1
        {
            width: 80px;
        }
        .style2
        {
            height: 208px;
            width: 300px;
        }
        .style3
        {
            height: 208px;
            width: 50px;
        }
        .style4
        {
            height: 208px;
        }
     </style>
    
<script language="javascript" type="text/javascript" src="../script/main.js" ></script>
<script type="text/javascript" language="javascript">
    function getCustomer() {
        returnValue = ShowDialog3('MasterCustomerW', 'Arg', '610', '450');
        if (returnValue) {
            if (document.getElementById("hfWCID").value == "") {
                var comp = new Array();
                comp = returnValue.split(";");
                var Name = document.getElementById("TxtCustName");
                Name.value = comp[1];
                var ID = document.getElementById("hfWCID");
                ID.value = comp[0];
                var Penerima = document.getElementById("TxtPenerima");
                Penerima.value = comp[2];
                var CID = document.getElementById("hfCID");
                CID.value = comp[3];
                document.getElementById("DDLSource").selectedIndex = 0;
                var quotation = document.getElementById("hfQuotation");
                quotation.value = comp[4];
            }
            else {
                alert("You should input the item to ship one customer for 1 MuatBarang Number");
            }
        }
    }

    function getContainer() {
        returnValue = Showdialogcontainer('Kontainer', 'Arg', '1000', '1000');
        if (returnValue) {
            var comp = new Array
            comp = returnValue.split(";");
            var ContainerID = document.getElementById("hfPKCID");
            ContainerID.value = comp[0];
            var Container = document.getElementById("TxtNoCont");
            Container.value = comp[1];

        }
    }
    function getPenerima() {
        returnValue = ShowDialog2('Penerima', document.getElementById("hfCID").value,'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var Name = document.getElementById("TxtPenerima");
            Name.value = comp[1];


        }
    }
    function getKapal() {
        returnValue = ShowDialog2('Kapal', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var Name = document.getElementById("TxtKapal");
            Name.value = comp[1];
            var ID = document.getElementById("hfIDK");
            ID.value = comp[0];
        }
    }
</script>
</head>
<body>
    <form id="form1" runat="server">

    <div class="Divutama">
        <div class="formtitle"><b>Input Pengiriman Barang</b></div>
        <br />
        <div class="div_input">
            <table >
                <tr>
                    <td style="width:300px;">
                        <table cellpadding="5" class = "borderdot">
                            <tr>
                                <td class="style1"> <strong> Nama Konsumen</strong> </td>
                                <td class ="style1"><asp:TextBox ID="TxtCustName" runat="server" ></asp:TextBox>
                                    <img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
                                </td>
                            </tr>
                           <tr>
                            <td class="style1">
                                <strong>Gudang</strong>
                            </td>
                            <td class="style1">
                                <asp:DropDownList ID="DDLSource" runat="server" AutoPostBack ="true" Width="170" >
                                </asp:DropDownList>
                            </td>
                            </tr>
                            <tr>
                            <td class="style1"><strong>Tanggal Pengiriman</strong></td>
                            <td>
                                <dxe:ASPxDateEdit ID="tb_tgl" runat="server" EditFormat="Custom" EditFormatString="dd MMMM yyyy">
                                </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1"> <strong> Penerima </strong> </td>
                                <td class ="style1"><asp:TextBox ID="TxtPenerima" runat="server" ></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td class ="style1">
                                    <strong>Kapal</strong></td>
                                <td class="style1">
                                    <asp:TextBox ID="TxtKapal" runat = "server" ></asp:TextBox>
                                    <img alt="Browse" src="../images/search.png" onclick="javascript:getKapal();" />                                    
                                </td>                                            
                            </tr>               
                            <tr>
                                <td class ="style1">
                                    Mb No</td>
                                <td class="style1">
                                    <asp:Label ID="lblMbNo" runat = "server" ></asp:Label></td>      
                                                                          
                            </tr>               
                            <tr>
                                <td align ="left" >                    
                                    &nbsp;</td>
                            </tr>
                        </table> 
                    </td>
                    <td style= "width : 50px">                   
                    </td>
                    <td valign = "top">
                        <strong>Gudang</strong>
                        <dxwgv:ASPxGridView ID="Grid_Gudang" runat="server" 
                            AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDG" 
                            Width="630px" ClientInstanceName = "grid_gudang">
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
                            <SettingsPager AlwaysShowPager="True" PageSize="5"></SettingsPager>
                            <Settings ShowFilterRow="True"  />
                            <SettingsBehavior AllowFocusedRow="True" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="IDGdg" FieldName="IDG" Name="IDG" 
                                    Visible="true" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn Name="Status" Caption="Status" VisibleIndex="1" Width="1%">
									    <DataItemTemplate>
									        <asp:Image ID = "ImageGrid" runat = "server" />
									    </DataItemTemplate>
									</dxwgv:GridViewDataColumn>
								<dxwgv:GridViewDataTextColumn Caption="Container" FieldName="Container" 
                                    Name="Container"  Visible="true" VisibleIndex="2" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="Nama_BarangG" 
                                    Name="Nama_BarangG"  Visible="true" VisibleIndex="3" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Quantity" FieldName="QuantityG" 
                                    Name="QuantityG"  Visible="true" VisibleIndex="4" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="QtyMsk" FieldName="QtyMskG" 
                                    Name="QtyMskG"  Visible="true" VisibleIndex="4" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Tgl Msk Brg" FieldName="Time_ItemcomeG" 
                                    Name="Time_ItemcomeG"  Visible="true" VisibleIndex="4" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Pengirim" FieldName="PengirimG" 
                                    Name="PengirimG"  Visible="true" VisibleIndex="4" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="No Tally" FieldName="NoTallyG" 
                                    Name="NoTallyG"  Visible="true" VisibleIndex="4" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Pnjg" FieldName="PanjangG" 
                                    Name="PanjangG"  Visible="true" VisibleIndex="4" >
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Lebar" FieldName="LebarG" 
                                    Name="LebarG"  Visible="true" VisibleIndex="4" >
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Tinggi" FieldName="TinggiG" 
                                    Name="TinggiG"  Visible="true" VisibleIndex="4" >
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Nokont" FieldName="NoKontG" 
                                    Name="NoKontG"  Visible="false" VisibleIndex="4" >
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn Caption="#" Name="Kirim" VisibleIndex="5" Width="1%">
                                    <dataitemtemplate>
                                        <asp:LinkButton ID="tbKirim" runat="server" CommandName="Kirim" 
                                            ToolTip="Kirim Barang">Kirim</asp:LinkButton>
                                    </dataitemtemplate>
                                </dxwgv:GridViewDataColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>         
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="div_input">
        <table >
            <tr>
                <td>
                    <table style="width: 1001px">
                    	<tr>
				            <td valign="top" align="center" class="style2">
				            <table cellpadding="4" cellspacing="0" align="center" 
                                    style="text-align:left; border:dotted 2px black; width: 100%;">
				            <tr>
				            <td colspan="3" bgcolor="#515763" 
                                    
                                    style="border-bottom:#7EACB1 1px solid; background-color: #333333; color: #FFFFFF;"><b>Pilih Quantity Barang Yang Dikirim</b> - <asp:Label ID="lInfo" runat="server" ForeColor="red"></asp:Label></td>	
				            </tr>
				        <tr id="tr_item_name" runat="server">
				            <td>Item Name</td>
				            <td>:</td>
				            <td>
                                <asp:TextBox ID="tb_item_name" runat="server"></asp:TextBox></td>
				        </tr>
				        <tr>
				            <td>Quantity Dikirim</td>
				            <td>:</td>
				            <td>
                                <asp:TextBox ID="tb_qty" runat="server" ></asp:TextBox></td>
				        </tr>
				         <tr>
				            <td><asp:CheckBox ID ="packed" runat="server" Text="LCL" AutoPostBack="True" /></td>
				            <td>:</td>
				            <td>
				                <asp:Panel ID="PanelNoCont" runat="server">
				                    <table>
				                        <tr>
				                            <td>
				                                NO Cont
				                            </td>
				                            <td>
				                                <asp:TextBox ID="TxtNoCont" runat="server" Width="36px" MaxLength="4"></asp:TextBox>
				                            </td>
				                            <td>
				                                <asp:TextBox ID="TxtNoCont2" runat="server" Width="61px" MaxLength="6" ></asp:TextBox>
				                            </td>
				                            <td>
				                                -
				                            </td>
				                            <td>
				                                <asp:TextBox ID="TxtNoCont3" runat="server" Width="31px" MaxLength="1" ></asp:TextBox>
				                            </td>
				                        </tr>
				                    </table>
				                    <table>
				                        <tr>
				                            <td>
				                                No Seal
				                            </td>
				                            <td>
				                                <asp:TextBox ID="TxtNoSeal1" runat="server" Width="36px" MaxLength="3"></asp:TextBox>
				                            </td>
				                            <td>
				                                <asp:TextBox ID="TxtNoSeal2" runat="server" Width="61px" MaxLength="6" ></asp:TextBox>
				                            </td>
				                        </tr>
				                    
				                    </table>
				                </asp:Panel>
                                
                            </td>
				        </tr>
			            <tr>
			                <td>
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false" ></asp:Label>                        
			                </td>
				            <td colspan="3" bgcolor="#0000" align="center" style="border-top:#7EACB1 1px solid; padding:0px;">
		                        <table width="100%" cellpadding="4" cellspacing="0">
			                        <tr>
				                        <td align ="right">
                                            <dxe:ASPxButton ID="btn_add" runat="server" Text="Tambah">
                                            <Image Url="../images/add.png" />
                                            </dxe:ASPxButton></td>
                                        <td align ="left">
                                            <dxe:ASPxButton ID="btn_reset" runat="server" Text="Reset">
                                            <Image Url="../images/undo.png" />
                                            </dxe:ASPxButton></td>
			                        </tr>
				                    
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="style3">
                    
                </td>
                
                    
                            <td valign = "top" class="style4">
                                <dxwgv:ASPxGridView ID="Grid_Kapal" runat="server" 
                                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDK" 
                                    Width="650px" ClientInstanceName = "grid_kapal">
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
                                    <SettingsPager AlwaysShowPager="True" PageSize="5"></SettingsPager>
                                    <Settings ShowFilterRow="True"  />
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <Columns>
                                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="IDK" Name="IDK" 
                                            Visible="true" VisibleIndex="0">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="IDGdg" FieldName="Warehouse_IdK" 
                                            Name="Warehouse_IdK"  Visible="true" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="IDDetail" FieldName="IDDetailK" 
                                            Name="IDDetailK"  Visible="false" VisibleIndex="1">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="Nama_BarangK" 
                                            Name="Nama_BarangK"  Visible="true" VisibleIndex="2" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Quantity" FieldName="QuantityK" 
                                            Name="QuantityK"  Visible="true" VisibleIndex="3" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="PackedContIDK" FieldName="PackedContIDK" 
                                            Name="PackedContIDK"  Visible="false" VisibleIndex="4" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="No Packed Container" FieldName="NoContainerK" 
                                            Name="NoContainerK"  Visible="true" VisibleIndex="5" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="No Seal" FieldName="NoSealK" 
                                            Name="NoSealK"  Visible="true" VisibleIndex="5" Width="100px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Pnjg" FieldName="PanjangK" 
                                            Name="NoSealK"  Visible="true" VisibleIndex="5" >
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Lebar" FieldName="LebarK" 
                                            Name="NoSealK"  Visible="true" VisibleIndex="5" >
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Tinggi" FieldName="TinggiK" 
                                            Name="NoSealK"  Visible="true" VisibleIndex="5" >
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="No Cont" FieldName="NoKontK" 
                                            Name="NoKontK"  Visible="true" VisibleIndex="5" >
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="6" Width="1%">
                                            <dataitemtemplate>
                                                <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                                    ToolTip="Delete Barang" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                                            </dataitemtemplate>
                                        </dxwgv:GridViewDataColumn>
                                       <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="7" Width="1%">
                                            <dataitemtemplate>
                                                <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                                    ToolTip="Edit Barang">Edit</asp:LinkButton>
                                            </dataitemtemplate>
                                        </dxwgv:GridViewDataColumn>
                                    </Columns>
                                </dxwgv:ASPxGridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;      
                                <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
                                <asp:Label ID="lblinfo" runat="server" CssClass="error" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan ="4">
                                
                                <dxe:ASPxButton ID="btSave" runat="server" Text="Simpan" >
                                <Image Url="../images/save-alt.png" />
                                </dxe:ASPxButton>
                                
                            </td>
                        
                
            </tr>
        </table>
        <br />
        </td>
        </tr>
        
        <tr>
            <td>
                <asp:DropDownList ID="DDLHistory" runat="server" AutoPostBack="true" Visible="false">
                <asp:ListItem Value="Now">Now</asp:ListItem>
                <asp:ListItem Value="History">History</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
           
        </table>
        <asp:Panel runat="server" ID="PanelGridKapal">
        <table>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="Grid_Kapal_Parent" runat="server" 
                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="Mb_No" 
                    Width="100%" ClientInstanceName = "grid_kapal_parent">
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
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
                    <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                            Visible="false" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Kode Data Gudang" FieldName="WarehouseHeaderID" Name="WarehouseHeaderID" 
                            Visible="true" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer Id" FieldName="Customer_Id" Name="Customer_Id" 
                            Visible="false" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pengirim(Quotation)" FieldName="Nama_Customer" Name="Nama_Customer" 
                             VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Penerima" FieldName="Penerima" Name="Penerima" 
                             VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Muat Barang Number" FieldName="Mb_No" 
                            Name="Mb_No"  Visible="true" VisibleIndex="5" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Tanggal" FieldName="Tanggal" 
                            Name="Tanggal"  Visible="true" VisibleIndex="6" Width="140px">
                            <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        
                        <dxwgv:GridViewDataTextColumn Caption="KapalID" FieldName="KapalID" 
                            Name="KapalID"  Visible="false" VisibleIndex="7" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Kapal" FieldName="Kapal" 
                            Name="Kapal"  Visible="true" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Warehouse_Code" FieldName="Warehouse_Code" 
                            Name="Warehouse_Code"  Visible="false" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Penginput Data" FieldName="PenginputData" 
                            Name="PenginputData"  Visible="true" VisibleIndex="8">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="9" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                    ToolTip="Delete Barang" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="10" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                    ToolTip="Edit Barang">Edit</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        
                        
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="Grid_Kapal_Child" runat="server" 
                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="Mb_No" 
                                Width="569px" ClientInstanceName = "grid_kapal_child" onbeforeperformdataselect="Grid_Kapal_Child_DataSelect">
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
                                <SettingsBehavior AllowFocusedRow="True"  />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                                        Visible="false" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Muat Barang Number" FieldName="Mb_No" 
                                        Name="Mb_No"  Visible="true" VisibleIndex="1" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="Nama_Barang" 
                                        Name="Nama_Barang"  Visible="true" VisibleIndex="2" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Quantity" FieldName="Quantity" 
                                        Name="Quantity"  Visible="true" VisibleIndex="3" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="PackedContID" FieldName="PackedContID" 
                                            Name="PackedContID"  Visible="false" VisibleIndex="4" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="No Packed Container" FieldName="NoContainer" 
                                            Name="NoContainer"  Visible="true" VisibleIndex="5" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="No Seal" FieldName="NoSeal" 
                                            Name="NoSeal"  Visible="true" VisibleIndex="5" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                </Columns>        
                                </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dxwgv:ASPxGridView>         
            </td>
   </tr>
   </table>
   </asp:Panel>
   
   <asp:Panel runat="server" ID="PanelHistory">
   <table>
        <tr>
            <td>
                <dxwgv:ASPxGridView ID="Grid_Kapal_History_Parent" runat="server" 
                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="Mb_No" 
                    Width="100%" ClientInstanceName = "grid_kapal_history_parent">
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
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
                    <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                            Visible="false" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Warehouse header ID" FieldName="WarehouseHeaderID" Name="WarehouseHeaderID" 
                            Visible="True" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer Id" FieldName="Customer_Id" Name="Customer_Id" 
                            Visible="false" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pengirim(Quotation)" FieldName="Nama_Customer" Name="Nama_Customer" 
                             VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Penerima" FieldName="Penerima" Name="Penerima" 
                             VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Muat Barang Number" FieldName="Mb_No" 
                            Name="Mb_No"  Visible="true" VisibleIndex="5" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Tanggal" FieldName="Tanggal" 
                            Name="Tanggal"  Visible="true" VisibleIndex="6" Width="140px">
                            <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        
                        <dxwgv:GridViewDataTextColumn Caption="KapalID" FieldName="Kapal_ID" 
                            Name="Kapal_ID"  Visible="false" VisibleIndex="7" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Kapal" FieldName="Kapal" 
                            Name="Kapal"  Visible="true" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Warehouse_Code" FieldName="Warehouse_Code" 
                            Name="Warehouse_Code"  Visible="false" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Penginput Data" FieldName="PenginputData" 
                            Name="PenginputData"  Visible="true" VisibleIndex="8" >
                        </dxwgv:GridViewDataTextColumn>                    
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="Grid_Kapal_History_Child" runat="server" 
                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="Mb_No" 
                                Width="569px" ClientInstanceName = "grid_kapal_history_child" onbeforeperformdataselect="Grid_Kapal_History_Child_DataSelect">
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
                                <SettingsBehavior AllowFocusedRow="True"  />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                                        Visible="false" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Muat Barang Number" FieldName="Mb_No" 
                                        Name="Mb_No"  Visible="true" VisibleIndex="1" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="Nama_Barang" 
                                        Name="Nama_Barang"  Visible="true" VisibleIndex="2" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Quantity" FieldName="Quantity" 
                                        Name="Quantity"  Visible="true" VisibleIndex="3" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="PackedContID" FieldName="PackedContID" 
                                            Name="PackedContID"  Visible="false" VisibleIndex="4" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="No Packed Container" FieldName="NoContainer" 
                                            Name="NoContainer"  Visible="true" VisibleIndex="5" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="No Seal" FieldName="NoSeal" 
                                            Name="NoSeal"  Visible="true" VisibleIndex="5" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                </Columns>        
                                </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dxwgv:ASPxGridView>  
            </td>
        </tr>
   </table>
        </asp:Panel>
    </div>
         <asp:HiddenField id="hfWh" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfModeItem" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfQT" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfIDK" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfDel" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfWCID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfPKCID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfCID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfpanjang" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hflebar" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hftinggi" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfNoKontainer" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfQtyMsk" runat="server"></asp:HiddenField>
         <asp:HiddenField id="HFQtyKpl" runat="server"></asp:HiddenField>
    </div>
    </form>
</body>
</html>
