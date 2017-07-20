<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DaftarInvoice.aspx.vb" Inherits="LMS.DaftarInvoice" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Daftar Invoice</title>
    
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <link rel="stylesheet" type="text/css" href="../css/print.css"  />
    
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama">
        <div class="formtitle">Daftar Invoice</div>
         <br />
        
        <div class = "div_umum">
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="Semua" >Semua</asp:ListItem>
                                        <asp:ListItem Value="Bulanan">per Bulan</asp:ListItem>
                                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPilih" runat="server" Text="Pilih Bulan" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLBulan" runat="server" AutoPostBack="True" Visible ="false">
                                        
                                    </asp:DropDownList></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    
                        <dxwgv:ASPxGridView ID="Grid_Invoice_Parent" runat="server" 
                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                    Width="100%" ClientInstanceName = "Grid_Invoice_Parent">
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
                    <SettingsPager Mode="ShowPager" PageSize = "15"></SettingsPager>
                    <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                            Visible="false" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="MuatBarangID" FieldName="MuatBarangID" Name="MuatBarangID" 
                            Visible="false" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="No Invoice" FieldName="No_Invoice" 
                            Name="No_Invoice"  Visible="true" VisibleIndex="2" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Tanggal" FieldName="InvoiceDate" 
                            Name="InvoiceDate"  Visible="true" VisibleIndex="3" Width="140px">
                        <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption=" Pengirim " FieldName="Nama_Customer" Name="Nama_Customer" 
                             VisibleIndex="4" Visible = "false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Di Tujukan" FieldName="Ditujukan" Name="Ditujukan" 
                             VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="KapalID" FieldName="KapalID" 
                            Name="KapalID"  Visible="false" VisibleIndex="6" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Kapal" FieldName="Kapal" 
                            Name="Kapal"  Visible="true" VisibleIndex="7" Width="140px" Settings-AutoFilterCondition="Contains">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Total" FieldName="Total" 
                            Name="Total"  Visible="true" VisibleIndex="8" Width="140px">
                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="DP" FieldName="DP" 
                            Name="DP"  Visible="true" VisibleIndex="8" Width="20px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Paid(%)" FieldName="Paid" 
                            Name="Paid"  Visible="false" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="Keterangan" 
                            Name="Keterangan"  Visible="false" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status Pembayaran" FieldName="StatusPembayaran" 
                            Name="StatusPembayaran"  Visible="true" VisibleIndex="8" >
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Tgl Pelunasan" FieldName="TglPelunasan" 
                            Name="TglPelunasan"  Visible="true" VisibleIndex="9" Width="140px">
                        <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Yang Input" Name="YgInput" VisibleIndex="10" 
									    FieldName="YgInput">
						</dxwgv:GridViewDataTextColumn>
                        
                    </Columns>
                    <Templates>
                        <DetailRow>
                        <table>
                            <tr>
                                <td>
                                
                            <dxwgv:ASPxGridView ID="Grid_Invoice_Child" runat="server" 
                                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDDetail" 
                                                Width="569px" ClientInstanceName = "grid_child" onbeforeperformdataselect = "Grid_Invoice_Child_DataSelect" >
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
                                                    <dxwgv:GridViewDataTextColumn Caption="IDDetail" FieldName="IDDetail" Name="IDDetail" 
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                   <dxwgv:GridViewDataTextColumn FieldName="Nama_Barang" Name="Nama_Barang" 
                                                                    VisibleIndex="2" Caption="Nama Barang" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="JenisPembayaran" Name="JenisPembayaran" VisibleIndex="3" Caption="Jenis Pembayaran" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Harga" Name="Harga" VisibleIndex="4" Caption="Harga" >
						                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Hargatotal" Name="Hargatotal" VisibleIndex="5" Caption="Harga Total" >
						                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Paid" Name="Paid" VisibleIndex="6" Caption="Paid" >
						                            </dxwgv:GridViewDataTextColumn>
								                </Columns>						                            
                                            </dxwgv:ASPxGridView>
                                            </td>
                                            <td valign = "top">
								                   <%-- <dxwgv:ASPxGridView ID="Grid_Asuransi" runat="server" 
                                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="NoAsuransi" 
                                                Width="400px" ClientInstanceName = "grid_child" onbeforeperformdataselect = "Grid_Asuransi_DataSelect" >
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
                                                    <dxwgv:GridViewDataTextColumn Caption="No Asuransi" FieldName="NoAsuransi" Name="NoAsuransi" 
                                                        Visible="true" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="HargaAsuransi" Name="HargaAsuransi" 
                                                        visible="true" VisibleIndex="2" Caption="Harga" >
                                                        <PropertiesTextEdit DisplayFormatString = "{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Premi" Name="Premi" 
                                                                    VisibleIndex="3" Caption="Premi" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Polis" Name="Polis" VisibleIndex="4" Caption="Polis" >
						                            <PropertiesTextEdit DisplayFormatString = "{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            
						                            <dxwgv:GridViewDataTextColumn FieldName="TotalAsuransi" Name="TotalAsuransi" VisibleIndex="4" Caption="Total Asuransi" >
						                            <PropertiesTextEdit DisplayFormatString = "{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            
						                            </Columns>
                                            </dxwgv:ASPxGridView> --%>
								                </td>
                            </tr>
                        </table>
                        </DetailRow>
                        
                    </Templates>
                </dxwgv:ASPxGridView>         
                    
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
