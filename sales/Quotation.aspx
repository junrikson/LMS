<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Sales_Quotation" Codebehind="Quotation.aspx.vb" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quotation</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <script language="javascript" type ="text/javascript" src="../script/main.js" ></script>
    <script language="javascript" src="../script/NumberFormat.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function changenumberformat(control) {
            var conval = document.getElementById(control).value;
            if (conval.lastIndexOf(",") != conval.length - 1) {
                document.getElementById(control).value = FormatNumberBy3(conval.replace(/[.]/g, ""),",",".");
            }
            else {
                document.getElementById(control).value = conval;
            }
        }
    
        function getCustomer() {
            returnValue = ShowDialog2('MasterCustomer', 'Arg', '610', '450');
            if (returnValue) {
                if (document.getElementById("hfClose").value == "") {
                    var comp = new Array();
                    comp = returnValue.split(";");
                    var cust = document.getElementById("TxtCustCode");
                    cust.value = comp[0];
                    var cust2 = document.getElementById("hfCodeCustomer");
                    cust2.value = comp[0];
                    var Name = document.getElementById("TxtCustName");
                    Name.value = comp[1];
                    var Name2 = document.getElementById("hfNamaCustomer");
                    Name2.value = comp[1];
                    var ID = document.getElementById("hfCID")
                    ID.value = comp[0]
                } else {
                    alert("Anda tidak merubah nama pelanggan ketika memakai ulang quotation");
                }
                
            }
        }

        function getTujuan() {
        
            returnValue = ShowDialog2('Tujuan', 'Arg', '610', '450');
            if (returnValue) {
                var comp = new Array();
                comp = returnValue.split(";");
                var Name = document.getElementById("TxtTujuan");
                Name.value = comp[1];
            }
        }


        function getSatuan() {
            returnValue = ShowDialog2('Satuan', 'Arg', '610', '450');
            if (returnValue) {
                var comp = new Array();
                comp = returnValue.split(";");
                var Name = document.getElementById("TxtOthers");
                Name.value = comp[1];
                var ID = document.getElementById("hfOID")
                ID.value = comp[0]
            }
        }

        function getSales() {
            returnValue = ShowDialog2('Sales', 'Arg', '610', '450');
            if (returnValue) {
                var comp = new Array();
                comp = returnValue.split(";");
                var Sales = document.getElementById("TxtSales");
                Sales.value = comp[1];
            }
        }
        function getItem() {
            returnValue = ShowDialog7('MasterItem', 'Arg', '1000', '500');
            if (returnValue) {
                var comp = new Array();
                comp = returnValue.split(";");
                var ID = document.getElementById("hfIID");
                ID.value = comp[0];
                var Name = document.getElementById("TxtItem");
                Name.value = comp[1];
//                var Kode = document.getElementById("TxtOthers");
//                Kode.value = comp[2];
                var Berat = document.getElementById("lblBerat");
                Berat.value = comp[3];
                var Panjang = document.getElementById("lblPanjang");
                Panjang.value = comp[4];
                var Lebar = document.getElementById("lblLebar");
                Lebar.value = comp[5];
                var Tinggi = document.getElementById("lblTinggi");
                Tinggi.value = comp[6];
                var Unit = document.getElementById("lblUnit");
                Unit.value = comp[7];
            }
        }

        function getItemQuotation() {

            returnValue = ShowItem('MasterItem', 'Arg', '1000', '500');
            if (returnValue) {
            }
        }


        function viewReports() {

            

            var windowprops = 'toolbar=0,location=0,directories=0,status=0, ' +
												'menubar=0,scrollbars=1,resizable=1,width=500,height=200';
            window.open('ReportQuotation.aspx?Jenis=QuotattionAll', 'RptQuotation', windowprops);
    
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100px
        }
        .style2
        {
            width: 2px;
        }
        .style3
        {
            width: 244px;
            text-align :left;
        }
                .style4
        {
            width: 170px;
            text-align :left ;
        }
        .style5
        {
            width: 70px
        }
        .style6
        {
            width: 244px;
        }
        </style>
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
    <div class ="Divutama"> 
         <div class="formtitle">
             <b>Quotation</b>  
        </div>
        <br />
         <div class="div_input">
            <div class="div_umum">
            <table class = "borderdot">
            <tr>
                <td >
                <table bgcolor="White" cellpadding="4" cellspacing="0" style="text-align:left; ">
                    <tr>
                        <td>
                        Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td class="style2"> :</td>
                        <td class="style3">
                            <dxe:ASPxDateEdit ID="tb_tgl" runat="server" Height="19px" EditFormat="Custom" 
                                EditFormatString="dd MMMM yyyy" Width="200px" >
                            </dxe:ASPxDateEdit>
                        </td>
                        <td class ="style1"></td>
                    </tr>
                    <tr>
                        <td>Nomor Quotation</td>
                        <td>:</td>
                        <td class="style6">
                        <asp:Label ID="lblQoNo" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Pengirim</td>
                        <td>:</td>
                        <td class="style6"><asp:TextBox ID="TxtCustName" runat="server" Width="200px" ReadOnly="true"  ></asp:TextBox>
                        <img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
                        </td>
                    </tr>
                    <tr>
                        <td>Kode Konsumen</td>
                        <td>:</td>
                        <td class="style6"><asp:TextBox ID="TxtCustCode" runat="server" Width="200px" ReadOnly = "true" ></asp:TextBox></td>
                    </tr>
                    <%--<tr>
                        <td>Kota Tujuan</td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID = "TxtTujuan" runat="server" ></asp:TextBox>
                            <img alt="Browse" src="../images/search.png" onclick="javascript:getTujuan();" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td>Kota Tujuan</td>
                        <td>:</td>
                        <td class="style6">
                            <asp:DropDownList ID="DDLKotaTujuan" runat="server">
                            </asp:DropDownList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>Penerima</td>
                        <td>:</td>
                        <td class="style6"><asp:TextBox ID="TxtPenerima" runat="server" Width="200px" ></asp:TextBox></td>
                    </tr>
                    <%--<tr>
                        <td>Pembayaran Oleh</td>
                        <td>:</td>
                        <td><asp:TextBox ID="TxtPembayaranOleh" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Kota Pembayar</td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="DDLKotaPembayar" runat="server">
                            </asp:DropDownList></td>
                    </tr>--%>
                   <tr>
                        <td>U.P</td>
                        <td>:</td>
                        <td class="style6"><asp:TextBox ID="TxtUp" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Jabatan</td>
                        <td>:</td>
                        <td class="style6"><asp:TextBox ID="TxtJabatan" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                  
                </table>
                </td>
                <td>
                <table bgcolor="White" cellpadding="4" cellspacing="0" style= "text-align:left; " >
                    <tr>
                        <td>Satuan</td>
                        <td class = "style2">:</td>
                        <td class = "style4"><asp:DropDownList ID="DDLSatuan" runat="server"  AutoPostBack="false"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Jenis Barang &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td class = "style2">:</td>
                        <td class = "style4"><asp:TextBox ID="TxtItem" runat="server" Width="200px" ></asp:TextBox>
                                                
                        </td>                   
                        
                    </tr>
                    <tr>
                        <td>harga</td>
                        <td>:</td>
                        <td><asp:TextBox ID="TxtHargaItem" runat="server" Width="200px"></asp:TextBox></td>
                        <td class ="style5"> 
                            &nbsp;</td>
                    </tr>
                    <tr>
                    <td>
                        Keterangan
                    </td>
                    <td>
                        :
                    </td>
                    <td>
                        <asp:TextBox ID="TxtKeterangan" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                        <td colspan="3" align ="center">      
                            <table bgcolor="White" cellpadding="0" cellspacing="0" 
                            style="width: 54%" align = "center" >    
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_add" runat="server" Text="Tambah" >
                                       <Image Url="../images/add.png" /> 
                                    </dxe:ASPxButton>
                                </td>           
                                <td>
                                    <dxe:ASPxButton ID="btn_reset" runat="server" Text="Reset">
                                    <Image Url="../images/undo.png" />
                                    </dxe:ASPxButton>
                                </td>
                                </tr>
                            </tr>
                            </table>
                        </td>    
                    </table>
              </td> 
            </tr>
          </table>
          <br />
          <div align = "center">
            <asp:Label ID="lblinfo" runat="server" CssClass = "error" Visible="false" ></asp:Label> 
            <asp:Label ID="LblinfoS" runat="server" CssClass = "berhasil" Visible="false" ></asp:Label>
          </div>
                                   
            <br />  
            <dxwgv:ASPxGridView ID="Grid_SO" runat="server" 
                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDS" 
                Width="700px" ClientInstanceName = "grid_item">
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
                <SettingsPager PageSize="5">
                </SettingsPager>
				<Settings ShowFilterRow="True"  />
                <SettingsBehavior AllowFocusedRow="True" />
				<Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="IDS" Name="IDS" 
                        Visible="true" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nomor Quotation" FieldName="Quotation_NoS" 
                        Name="Quotation_NoS"  Visible="true" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="NamaBarangS" 
                        Name="NamaBarangS"  Visible="true" VisibleIndex="2" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SatuanID" FieldName="SatuanIDS" 
                        Name="SatuanIDS" visible = "false" VisibleIndex="3" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Satuan" FieldName="NamaHargaS" 
                        Name="NamaHargaS" VisibleIndex="4" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Harga" FieldName="HargaS" Name="HargaS" 
                        VisibleIndex="5" Width="100px">
                        <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="KeteranganS" 
                        Name="KeteranganS" VisibleIndex="6" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="11" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                ToolTip="Delete Item" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="12" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                ToolTip="Edit Item">Edit</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView> 
            <br />
            <div align = "center" style="width: 100%">
                <dxe:ASPxButton ID="btSave" runat="server" Text="Simpan" >
                <Image Url="../images/save-alt.png" />
                </dxe:ASPxButton>
            </div>
            <br />
            <asp:DropDownList ID="DDLQuotation" runat="server" Height="21px" Width="166px" AutoPostBack="true">
                <asp:ListItem Value="0">Active</asp:ListItem>
                <asp:ListItem Value="1">Unactive</asp:ListItem>
            </asp:DropDownList>
    
                <br />
    
            <br />
            <asp:Panel ID="PanelApprove" runat="server">
             <dxwgv:ASPxGridView ID="Grid_SO_Parent" runat="server" 
                AutoGenerateColumns="True" Font-Size="9pt" KeyFieldName="Quotation_No" 
                Width="100%" ClientInstanceName = "grid_parent" > 
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
                 <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
				<Settings ShowFilterRow="True" ShowFooter="false"  />
                <SettingsBehavior AllowFocusedRow="True"  ColumnResizeMode="Control" />
				<Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                        Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nomor Quotation" FieldName="Quotation_No" 
                        Name="Quotation_No"  Visible="true" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Tanggal" FieldName="Tanggal" 
                        Name="Tanggal"  Visible="true" VisibleIndex="2" Width="140px" >
                        <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                    </dxwgv:GridViewDataDateColumn>
                   <%-- <dxwgv:GridViewDataDateColumn Caption="Tanggal" FieldName="Tanggal" Name="Tanggal" 
                    VisibleIndex="2" Width="140px" >
                    <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Customer ID" FieldName="Cust_ID" 
                        Name="Cust_ID" VisibleIndex="3" Visible="false" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pengirim" FieldName="Cust_Name" 
                        Name="Cust_Name" VisibleIndex="4" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Kode Pengirim" FieldName="Cust_Code" 
                        Name="Cust_Code" VisibleIndex="5" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Tujuan" FieldName="Tujuan" 
                        Name="Tujuan" VisibleIndex="6" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Penerima" FieldName="Penerima" 
                        Name="Penerima" VisibleIndex="7" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
<%--                    <dxwgv:GridViewDataTextColumn Caption="Pembayar" FieldName="Pembayar" 
                        Name="Pembayar" VisibleIndex="8" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Kota Pembayar" FieldName="KotaPembayar" 
                        Name="KotaPembayar" VisibleIndex="8" Width="100px">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="U.P" FieldName="UP" 
                        Name="UP" VisibleIndex="8" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Jabatan" FieldName="JabatanUP" 
                        Name="JabatanUP" VisibleIndex="9" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Yang Input" Name="YgInput" VisibleIndex="10" 
									FieldName="YgInput" Width="150px">
								</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Close" FieldName="Close" 
                        Name="Close" VisibleIndex="10" Width="100px" Visible = "false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="QuotationClose_No" FieldName="QuotationClose_No" 
                        Name="QuotationClose_No" VisibleIndex="10" Width="100px" Visible = "false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="10" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                ToolTip="Delete Item" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="11" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                ToolTip="Edit Item">Revisi</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Update" VisibleIndex="11" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbUpdate" runat="server" CommandName="Update" 
                                ToolTip="Update Item">Update Quotation</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Print" VisibleIndex="12" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="Print" runat="server" CommandName="Print" 
                                ToolTip="Print">Print</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    
                </Columns>
                <Templates>
                    <DetailRow>
                        <dxwgv:ASPxGridView ID="Grid_SO_Child" runat="server" 
                            AutoGenerateColumns="True" Font-Size="9pt" KeyFieldName="Quotation_No" 
                            Width="569px" ClientInstanceName = "grid_child" onbeforeperformdataselect="Grid_SO_Child_DataSelect">
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
                                <dxwgv:GridViewDataTextColumn Caption="Nomor Quotation" FieldName="Quotation_No" 
                                    Name="Quotation_No"  Visible="true" VisibleIndex="1" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="Nama_Barang" 
                                    Name="Nama_Barang"  Visible="true" VisibleIndex="2" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="SatuanID" FieldName="SatuanID" 
                                    Name="SatuanID" visible = "false" VisibleIndex="3" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="NamaHarga" FieldName="NamaHarga" 
                                    Name="Satuan" VisibleIndex="4" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Harga" FieldName="Harga" Name="Harga" 
                                    VisibleIndex="10" Width="100px">
                                    <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="Keterangan" 
                                    Name="Keterangan" VisibleIndex="11" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:ASPxGridView> 
                    </DetailRow>
                </Templates>
            </dxwgv:ASPxGridView> 
            </asp:Panel>
            <br />
            
            <asp:Panel ID="Panel_Close" runat="server" Width="1045px">
             <dxwgv:ASPxGridView ID="Grid_Parent_Close" runat="server" 
                AutoGenerateColumns="True" Font-Size="9pt" KeyFieldName="Quotation_No" 
                Width="100%" ClientInstanceName = "grid_parent">
                <SettingsPager PageSize="15">
                    </SettingsPager>
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
                 <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
				<Settings ShowFilterRow="True" ShowFooter="false"  />
                <SettingsBehavior AllowFocusedRow="True"
                     ColumnResizeMode="Control" />
				<Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                        Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nomor Quotation" FieldName="Quotation_No" 
                        Name="Quotation_No"  Visible="true" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Tanggal" FieldName="Tanggal" 
                        Name="Tanggal"  Visible="true" VisibleIndex="2" Width="140px" >
                        <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer ID" FieldName="Cust_ID" 
                        Name="Cust_ID" VisibleIndex="3" Visible="false" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nama Konsumen" FieldName="Cust_Name" 
                        Name="Cust_Name" VisibleIndex="4" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Kode Konsumen" FieldName="Cust_Code" 
                        Name="Cust_Code" VisibleIndex="5" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Tujuan" FieldName="Tujuan" 
                        Name="Tujuan" VisibleIndex="6" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Penerima" FieldName="Penerima" 
                        Name="Penerima" VisibleIndex="7" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="U.P" FieldName="UP" 
                        Name="UP" VisibleIndex="8" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Jabatan" FieldName="JabatanUP" 
                        Name="JabatanUP" VisibleIndex="9" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                   <%-- <dxwgv:GridViewDataColumn Caption="#" Name="Use" VisibleIndex="10" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbUse" runat="server" CommandName="Use" 
                                ToolTip="Use Item">Use</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Use and rev" VisibleIndex="11" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbUseRev" runat="server" CommandName="UseRev" 
                                ToolTip="Use Item Rev">UseRev</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>--%>
                  </Columns>
                <Templates>
                    <DetailRow>
                        <dxwgv:ASPxGridView ID="Grid_Child_Close" runat="server" 
                            AutoGenerateColumns="True" Font-Size="9pt" KeyFieldName="Quotation_No" 
                            Width="569px" ClientInstanceName = "grid_child" onbeforeperformdataselect="Grid_Child_Close_DataSelect">
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
                                <dxwgv:GridViewDataTextColumn Caption="Nomor Quotation" FieldName="Quotation_No" 
                                    Name="Quotation_No"  Visible="true" VisibleIndex="1" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="Nama_Barang" 
                                    Name="Nama_Barang"  Visible="true" VisibleIndex="2" Width="140px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="SatuanID" FieldName="SatuanID" 
                                    Name="SatuanID" visible = "false" VisibleIndex="3" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Satuan" FieldName="NamaHarga" 
                                    Name="NamaHarga" VisibleIndex="4" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Harga" FieldName="Harga" Name="Harga" 
                                    VisibleIndex="10" Width="100px">
                                    <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="Keterangan" 
                                    Name="Keterangan" VisibleIndex="11" Width="100px">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:ASPxGridView> 
                    </DetailRow>
                </Templates>
            </dxwgv:ASPxGridView> 
            </asp:Panel>
            
            </div>
        </div>  
        <div align = "left" style="width: 100%">
        <asp:Panel ID="pnlbutton" runat="server">
                <input id="Button1" type="button" value="Print All" onclick="javascript:viewReports();" />
            </asp:Panel>
            </div>
         <asp:HiddenField id="hfQuotationClose" runat="server"></asp:HiddenField>
         <asp:HiddenField id="HfSo" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfModeEdit" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfClose" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfStatusClose" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfIDC" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfOID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfCID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfDel" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfItem" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfIID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="HFIDS" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfNamaCustomer" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfCodeCustomer" runat="server"></asp:HiddenField>
    </div>
    </form>
</body>
</html>
