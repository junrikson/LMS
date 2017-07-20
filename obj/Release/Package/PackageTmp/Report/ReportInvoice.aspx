<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportInvoice.aspx.vb" Inherits="LMS.ReportInvoice" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Invoice Report</title>
     <style type="text/css" media="screen">
			.NoPrint { FONT-SIZE: 9pt; FONT-FAMILY: Arial }
		 </style>
		<style type="text/css" media="print">
			.NoPrint, .btn, .kembali { DISPLAY: none }
		</style>		
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <link rel="stylesheet" type="text/css" href="../css/print.css"  />
    </head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama">
    <asp:Panel ID = "Panel_Input" runat = "server">
        <div class="formtitle">Cetak Invoice</div>
        <br />
        <div class = "div_input">
        <div class = "div_umum">
                <table >
                    <tr>
                        <td style = " width : 250px ;   "  >
                            Jenis Invoice
                        </td>
                        <td >:</td>
                        <td>
                             <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="-Pilih-">-Pilih-</asp:ListItem>
                                        <asp:ListItem Value="InvoiceDP" >DP</asp:ListItem>
                                        <asp:ListItem Value="InvoiceNDP">Non DP</asp:ListItem>
                                    </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style = " width : 250px ;  "    >
                            Header
                        </td>
                        <td >:</td>
                        <td>
                            <asp:DropDownList ID="ddlHeader" runat = "server" ></asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td style = " width : 250px ;  "  >
                            Kota Tujuan
                        </td>
                        <td >:</td>
                        <td>
                            <asp:TextBox ID="TxtKotaTujuan" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="ChkViewRekening" runat = "server" Text="Tampilkan No Rekening" AutoPostBack="false"/>
                        </td>
                         <td >:</td>
                         
                        <td>
                        <table>
                            <tr>
                                <td>
                                    Nama Bank
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtNamaBank" runat="server" ></asp:TextBox>
                                </td>
                                <td>
                                    Atas Nama
                                </td>
                                <td>
                                     <asp:TextBox ID="txtAtasNama" runat="server" ></asp:TextBox>
                                </td>
                                <td>
                                    No Rekening
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtRekening" runat="server" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                            
                        </td>
                        
                    </tr>
                     <tr>
                        <td>
                            <asp:CheckBox ID="Chkberatcontainer" runat = "server" Text="Tampilkan berat container" Visible = "false"  />
                        </td>
                    </tr>
                      <tr>
                        <td>
                            <asp:CheckBox ID="ChkTotalGabung" runat = "server" Text="Total+Asuransi" Visible ="false"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:TextBox ID="TxtMinCharge" runat="server" Visible = "false" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td>
                        <asp:Label ID= "lblError" runat ="server" CssClass = "error" ></asp:Label>                    
                    </td>
                    </tr>
      
                </table>
                </div>
                </div>
            </asp:Panel>
        </div>
            <asp:Panel ID ="Panel_Grid" runat ="server" >
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
                             VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Di Tujukan" FieldName="Ditujukan" Name="Ditujukan" 
                             VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="KapalID" FieldName="KapalID" 
                            Name="KapalID"  Visible="false" VisibleIndex="6" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Kapal" FieldName="Kapal" 
                            Name="Kapal"  Visible="true" VisibleIndex="7" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Total" FieldName="Total" 
                            Name="Total"  Visible="true" VisibleIndex="8" Width="140px">
                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="DP" FieldName="DP" 
                            Name="DP"  Visible="true" VisibleIndex="8" Width="20px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Paid(%)" FieldName="Paid" 
                            Name="Paid"  Visible="true" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="Keterangan" 
                            Name="Keterangan"  Visible="true" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Status Pembayaran" FieldName="StatusPembayaran" 
                            Name="StatusPembayaran"  Visible="true" VisibleIndex="8" >
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="KeteranganOngkosAngkut" FieldName="KeteranganOngkosAngkut" 
                            Name="KeteranganOngkosAngkut"  Visible="true" VisibleIndex="8" >
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="KeteranganDS" FieldName="KeteranganDS" 
                            Name="KeteranganDS"  Visible="false" VisibleIndex="8" >
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Yang Input" Name="YgInput" VisibleIndex="9" 
							    FieldName="YgInput">
						    </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="PrintInvoice" VisibleIndex="11" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbPrintInvoice" runat="server" CommandName="PrintInvoice" 
                                    ToolTip="Cetak Invoice">Cetak Invoice</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Tampil" VisibleIndex="11" Width="1%" Visible = "false">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbShow" runat="server" CommandName="TampilkanButton" 
                                    ToolTip="Tampilkan Button">Ulang</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        
                    </Columns>
                    <Templates>
                        <DetailRow>
                                
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
                        </table>
                        </DetailRow>
                        
                    </Templates>
                </dxwgv:ASPxGridView>         
                
        </asp:Panel>
        <div align ="center" style ="display : block">
            <asp:Panel ID = "Panel_Report" runat = "server" >
           <asp:Label  ID="lblReport" runat="server" ></asp:Label>
            <table style="width: 100%" align="center" border='0'>
                <tr align ="center">
                    <td align ="center">
                    <br />
                    <table align ="center" >
                        <tr align ="center">
                            <td > 
                                <input type="button" onclick="window.print()" value="Print" class="btn" />
                            </td>
                            <td>
                                <asp:Button CssClass="btn" ID="btKembaliDevPeriod" runat="server" 
                        Text="Kembali" Width="89px" /> 
                            </td>
                        </tr>
                    </table>
                        
                    </td>
                </tr>
            </table>
            </asp:Panel>
            </div>
            <div>
        </div>
        
    <asp:HiddenField ID="hfBLID" runat="server" />
    <asp:HiddenField ID="hfMRID" runat="server" />
    <asp:HiddenField ID="hfMode" runat="server" />
    <asp:HiddenField ID="hfPC" runat="server" />
    <asp:HiddenField ID="hfCID" runat="server" />
    
    </form>
</body>
</html>
