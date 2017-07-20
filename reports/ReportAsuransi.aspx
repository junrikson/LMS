<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportAsuransi.aspx.vb" Inherits="LMS.ReportAsuransi1" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
        <div class="formtitle">Cetak Asuransi</div>
        <br />
        <div class = "div_input">
        <div class = "div_umum">
                <table >
                    <tr>
                        <td style = " width : 250px ;   "  >
                             Jenis Asuransi
                        </td>
                        <td >:</td>
                        <td>
                             <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="-Pilih-">-Pilih-</asp:ListItem>
                                        <asp:ListItem Value="Freight" >Freight</asp:ListItem>
                                        <asp:ListItem Value="NonFreight">Non Freight</asp:ListItem>
                                    </asp:DropDownList>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style = " width : 250px ;   "  >
                            <asp:Label ID="lbljenis" runat="server" Text ="Jenis Invoice" ></asp:Label>
                        </td>
                            <asp:Label ID="lbltitikdua" runat="server" Text =":" ></asp:Label>
                        <td ></td>
                        <td>
                             <asp:DropDownList ID="ddltypeinvoice" runat="server" AutoPostBack="True">
                                        <asp:ListItem Value="-Pilih-">-Pilih-</asp:ListItem>
                                        <asp:ListItem Value="InvoiceDP" >DP</asp:ListItem>
                                        <asp:ListItem Value="InvoiceNonDP">Non DP</asp:ListItem>
                                    </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style = " width : 250px ;  "  >
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
                            <asp:CheckBox ID="ChkViewRekening" runat = "server" Text="Tampilkan No Rekening" AutoPostBack="true"/>
                        </td>
                         <td >:</td>
                        <td>
                            <asp:TextBox ID="TxtRekening" runat="server" ></asp:TextBox>
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
                               <dxwgv:ASPxGridView ID="Grid_Asuransi_Parent" runat="server" 
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
                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
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
                        <dxwgv:GridViewDataColumn Caption="Tanggal" FieldName="InvoiceDate" 
                            Name="InvoiceDate"  Visible="true" VisibleIndex="3" Width="140px">
                        </dxwgv:GridViewDataColumn>
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
                        </dxwgv:GridViewDataTextColumn>
                          <dxwgv:GridViewDataTextColumn Caption="DP" FieldName="DP" 
                            Name="DP"  Visible="true" VisibleIndex="9" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        
                          <dxwgv:GridViewDataTextColumn Caption="Nomor Asuransi" FieldName="NoAsuransi" 
                            Name="NoAsuransi"  Visible="true" VisibleIndex="10" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        
                          <dxwgv:GridViewDataTextColumn Caption="Harga Asuransi" FieldName="HargaAsuransi" 
                            Name="HargaAsuransi"  Visible="true" VisibleIndex="11" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        
                          <dxwgv:GridViewDataTextColumn Caption="Polis/Materai" FieldName="Polis" 
                            Name="Polis"  Visible="true" VisibleIndex="12" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        
                          <dxwgv:GridViewDataTextColumn Caption="Premi" FieldName="Premi" 
                            Name="Premi"  Visible="true" VisibleIndex="13" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        
                          <dxwgv:GridViewDataTextColumn Caption="Total Asuransi" FieldName="TotalAsuransi" 
                            Name="TotalAsuransi"  Visible="true" VisibleIndex="14" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        
                        <dxwgv:GridViewDataColumn Caption="#" Name="PrintBL" VisibleIndex="15" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbPrintInvoice" runat="server" CommandName="PrintInvoice" 
                                    ToolTip="Print Invoice">Cetak Asuransi</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        
                    </Columns>
                  <%--  <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="Grid_Invoice_Child" runat="server" 
                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                                Width="569px" ClientInstanceName = "Grid_Invoice_Child" onbeforeperformdataselect="Grid_Invoice_Child_DataSelect">
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
                                    <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="Nama_Barang" 
                                        Name="Nama_Barang"  Visible="true" VisibleIndex="2" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Harga" FieldName="Hargatotal" 
                                        Name="Hargatotal"  Visible="true" VisibleIndex="3" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>        
                                </dxwgv:ASPxGridView>
                        </DetailRow>
                  </Templates>  --%>
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
    <asp:HiddenField ID="hfInvoiceType" runat="server" />
    <asp:HiddenField ID="hfMode" runat="server" />
    <asp:HiddenField ID="hfPC" runat="server" />
    <asp:HiddenField ID="hfCID" runat="server" />
    
    </form>
</body>
</html>
