<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="QuotationPotential.aspx.vb" Inherits="LMS.QuotationPotential" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Potential Quotation</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <script language="javascript" type ="text/javascript" src="../script/main.js" ></script>
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
            width: 120px;
            text-align :left ;
        }
                .style4
        {
            width: 170px;
            text-align :left ;
        }
        .style5
        {
            width: 100px
        }
                </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Divutama">
    
   <asp:Panel ID="PanelInput" runat ="server">
        <asp:Panel ID="PanelCustomer" runat ="server">
        <div class="formtitle">
            <b>Input Customer</b>  
        </div>
        <br />
        <div class="div_input">
            <div class="div_umum">
                <table>
                    <tr>
                        <td>
                            <table class = "borderdot">
                    <tr>
                        <td class="style1">
                            Nama</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtNamaCustomer" runat="server" Width="184px"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="style1">
                            Alamat</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtAlamat" runat="server" Height="99px" Width="247px" 
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="style1">
                            Kota</td>
                        <td class="style2">
                            : </td>
                        <td class="style3">
                            <asp:TextBox ID="TxtArea" runat="server" Width="181px"></asp:TextBox>
                        </td>
                       
                        
                    </tr>
                    <tr>
                        <td>
                            Kota Asal Barang
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLKotaASAlBarang" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            No telp</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtkodearea" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtNoHP" runat="server" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                            
                        </td>
                      
                    </tr>
                    <tr>
                        <td class="style1" colspan="3" align="center">
					        
					    </td>
                    </tr>
                </table>
                        </td>
                    </tr>
                </table>
                
                
            </div>
        </div>
            <asp:HiddenField id="HfSo" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfModeEdit" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfIDC" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfOID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfCID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfDel" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfIID" runat="server"></asp:HiddenField>
        </asp:Panel>
        <br />
        <asp:Panel ID="PanelQuotation" runat="server">
        <div class="formtitle">
            <b>Input Quotation</b>  
        </div>
        <br />
        <div class = "div_umum" style = "background-color:White;">
            <table class = "borderdot" bgcolor="White">
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
                                EditFormatString="dd MMMM yyyy" Width="204px" Enabled ="true" >
                            </dxe:ASPxDateEdit>
                        </td>
                        <td class ="style1"></td>
                    </tr>
                    <tr>
                        <td>Nomor Quotation</td>
                        <td>:</td>
                        <td>
                        <asp:Label ID="lblQoNo" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Tujuan</td>
                        <td>:</td>
                        <td>
                            <%--<asp:TextBox ID = "TxtTujuan" runat="server" ></asp:TextBox>
                            <img alt="Browse" src="../images/search.png" onclick="javascript:getTujuan();" />--%>
                            <asp:DropDownList ID="DDLKotaTujuan" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Penerima</td>
                        <td>:</td>
                        <td><asp:TextBox ID="TxtPenerima" runat="server" Width="200px" ></asp:TextBox></td>
                    </tr>
                   <tr>
                        <td>U.P</td>
                        <td>:</td>
                        <td><asp:TextBox ID="TxtUp" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Jabatan</td>
                        <td>:</td>
                        <td><asp:TextBox ID="TxtJabatan" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                    
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                </td>
                <td>
                 <table bgcolor="White" cellpadding="4" cellspacing="0" style= "text-align:left; " >
                    <tr>
                        <td>Satuan</td>
                        <td>:</td>
                        <td><asp:DropDownList ID="DDLSatuan" runat="server"  AutoPostBack="false"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>Jenis Barang &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td class = "style2">:</td>
                        <td class = "style4"><asp:TextBox ID="TxtItem" runat="server" Width="200px"></asp:TextBox>
                        </td>                   
                        
                    </tr>
                    <tr>
                        <td>harga</td>
                        <td>:</td>
                        <td><asp:TextBox ID="TxtHargaItem" runat="server" Width="200px"></asp:TextBox></td>
                        <%--<td class ="style5"> 
                            <input id="Button1" type="button" value="Cek Perbandingan" class="btn" style="width:150px;" onclick="javascript:getItemQuotation();" />
                        </td>--%>
                    </tr>
                    
                </table>
                </td>
            </tr>    
            <tr>
                <td colspan="2" align ="center">      
                &nbsp;              
              
                    
                        <table bgcolor="White" cellpadding="4" cellspacing="0" 
                        style="width: 151px" >    
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
                        </table>
                </td>
               
            </tr>
           
            </table>
        
        <br />
        <div align = "center" style="width:100%;">
            <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			                <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
        </div>
        
        </div>
        <br />
        </asp:Panel>
        <asp:Panel ID="PanelGrid" runat = "server">
        <div class="formtitle">
            <b>Daftar Barang</b>  
        </div>
        <br />
        <div class="div_input">
                <div class="div_umum" align="center">
            <dxwgv:ASPxGridView ID="Grid_SO" runat="server" 
                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDS" 
                Width="569px" ClientInstanceName = "grid_item">
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
                <SettingsBehavior AllowFocusedRow="True" />
				<Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="IDS" Name="IDS" 
                      VisibleIndex="0" Visible="false">
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
                        VisibleIndex="10" Width="100px">
                        <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
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
            <div align = "center" style="width:100%;">
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btSave" runat="server" Text="Simpan" >
                        <Image Url="../images/save-alt.png" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                         <dxe:ASPxButton ID="btback" runat="server" Text="Kembali" Width="90px">
                        <Image Url="../images/left.gif" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>


        </div>
            </div>
            </div>
        </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="PanelView" runat ="server">
        <div class="formtitle">
            <b>View Grid</b>  
        </div>
        <br />
        <div class="div_input">
                <div class="div_umum" align="center">
        <dxwgv:ASPxGridView ID="Grid_SO_Parent" runat="server" 
                AutoGenerateColumns="True" Font-Size="9pt" KeyFieldName="Quotation_No" 
                Width="100%" ClientInstanceName = "grid_parent">
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
                        Name="Tanggal"  Visible="true" VisibleIndex="2" Width="140px">
                        <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nama Konsumen" FieldName="Nama_Customer" 
                        Name="Nama_Customer" VisibleIndex="3" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="No telepon" FieldName="No_Telp" 
                        Name="No_Telp" VisibleIndex="4" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Alamat" FieldName="Alamat" 
                        Name="Alamat" VisibleIndex="5" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Kota" FieldName="Kota" 
                        Name="Kota" VisibleIndex="6" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Kota Asal Barang" FieldName="KotaAsalBrg" 
                        Name="KotaAsalBrg" VisibleIndex="6" Width="110px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Tujuan" FieldName="Tujuan" 
                        Name="Tujuan" VisibleIndex="7" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Penerima" FieldName="Penerima" 
                        Name="Penerima" VisibleIndex="8" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="U.P" FieldName="UP" 
                        Name="UP" VisibleIndex="9" Width="100px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Jabatan" FieldName="JabatanUP" 
                        Name="JabatanUP" VisibleIndex="10" Width="100px">
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
                                ToolTip="Edit">Edit</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Deal" VisibleIndex="13" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbDeal" runat="server" CommandName="Deal" 
                                ToolTip="Deal">Deal</asp:LinkButton>
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
                            </Columns>
                        </dxwgv:ASPxGridView> 
                    </DetailRow>
                </Templates>
            </dxwgv:ASPxGridView> 
            </div>
            </div>
            
            <div align="center">
                        <dxe:ASPxButton ID="btn_new" runat="server" Text="Tambah Baru" Font-Size ="Large" >
                        </dxe:ASPxButton>
            </div>
            
    </asp:Panel>
    </div>
    </form>
</body>
</html>
