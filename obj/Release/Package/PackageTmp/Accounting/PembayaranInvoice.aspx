<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PembayaranInvoice.aspx.vb" Inherits="LMS.PembayaranInvoice" %>

<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxm" %>
    
<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>


<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
    
    <%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pembayaran Invoice Jakarta</title>
        <link rel="stylesheet" type="text/css" href="../css/style.css" />
        <link rel="stylesheet" type="text/css" href="../css/main.css"  />
        <script language="javascript" src="../script/NumberFormat.js" type="text/javascript"></script>
        <script type="text/javascript">

            function changenumberformat(control) {
                var conval = document.getElementById(control).value;
                if (conval.lastIndexOf(",") != conval.length - 1) {
                    document.getElementById(control).value = FormatNumberBy3(conval.replace(/[.]/g, ""), ",", ".");
                }
                else {
                    document.getElementById(control).value = conval;
                }
            }

            function OnGetRowValuesInvoice(values) {
               
                document.getElementById("hfNoInvoice").value = values[1];

                TbNoInvoice.SetValue(values[1]);

			    document.getElementById("TxtNmCustomer").value = values[4];
			    document.getElementById("hfpengirim").value = values[4];
			    document.getElementById("hfDitunjukan").value = values[4];

//			    alert("msk1");
//			    var NilaiTotal = values[2].toString();
//			    var nilai = NilaiTotal.replace(/[.]/g, ",")
//			    alert("msk2");
//			    alert(nilai);

			    document.getElementById("TxtTotalHarga").value = values[2];
			    document.getElementById("hfTotalHarga").value = values[2];
			    document.getElementById("hfkodecustomer").value = values[3];
			    document.getElementById("HFJenis").value = values[5];

			    var pisah = new Array();
			    var Nilai = values[2].toString();
			    var PnjgArray;
			    
			    pisah = Nilai.split(".");
			    PnjgArray = parseInt(pisah.length);
			    if (PnjgArray > 1) {
			        var txtTotalHarga = document.getElementById("TxtTotalHarga")
			        txtTotalHarga.value = pisah[0];
			        changenumberformat("TxtTotalHarga")
			        txtTotalHarga.value = txtTotalHarga.value + "," + pisah[1];
			    }
			    else {
			        changenumberformat("TxtTotalHarga")
			    }
			    
			    
			    PopUpInvoice.Hide();
			    setTimeout("CBSisa.callBack(document.getElementById('hfNoInvoice').value);", 100);
			}

			function CBSisa_handeler(result) {
			    var data = new Array();
			    data = result.split("|");
			    document.getElementById("hfSisaBelumBayar").value = data[0];
			    document.getElementById("TxtBlmBayar").value = data[0];
			}
			function reloadframe() {
			    top.menu.location.reload(true);
			}

  </script>
        
</head>
<body runat = "server">
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Pembayaran Invoice Jakarta</div>
		<dxpc:ASPxPopupControl ID="PopUpInvoice" runat="server" ClientInstanceName="PopUpInvoice" HeaderText="List Invoice" >
		    <Controls>
		        <dxwgv:ASPxGridView ID="GridInvoice" runat="server"  
                    AutoGenerateColumns = "False" ClientInstanceName = "GridInvoice" DataSourceID = "SqlDataSourceAccount"
                 KeyFieldName = "ID" Font-Size = "9pt" Width = "500px">
                <SettingsBehavior AllowFocusedRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                            VisibleIndex="0" Visible = "false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="No Invoice" FieldName="No_Invoice" Name="No_Invoice" 
                            VisibleIndex="1" Width="200px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Ditujukan" FieldName="Ditujukan" 
                            Name="Ditujukan" VisibleIndex="2" Width="200px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="KodeCustomer" FieldName="Kode_Customer" 
                            Name="Kode_Customer" VisibleIndex="2" Visible = "false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Jenis" FieldName="Jenis" 
                            Name="Jenis" VisibleIndex="2" Visible = "false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Total Harga" FieldName="Total" 
                            Name="Total" VisibleIndex="2" Width="140px">
                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###.00}"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Sisa" FieldName="Sisa" 
                            Name="Sisa" VisibleIndex="2" Width="100px">
                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###.00}"></PropertiesTextEdit>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <ClientSideEvents RowDblClick="function(s, e) {
               	    GridInvoice.GetRowValues(GridInvoice.GetFocusedRowIndex(), 'ID;No_Invoice;Total;Kode_Customer;Ditujukan;Jenis', OnGetRowValuesInvoice)}" />
                    <SettingsPager AlwaysShowPager="True" PageSize="5">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" />
                    <Styles>
                        <Header BackColor="#2C3848" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Center">
                            <HoverStyle>
                                <Border BorderColor="#515763" />
                            </HoverStyle>
                        </Header>
                        <FocusedRow BackColor="#D3D1D4" ForeColor="Black">
                        </FocusedRow>
                    </Styles>

            </dxwgv:ASPxGridView>
		    </Controls>
        </dxpc:ASPxPopupControl>
       
        
		<br />
            <div  class="div_input" >
		         <div class="div_umum">
		            <table>
		            
		                <tr>
		                    <td>
		                        No Nota
		                    </td>
		                    <td>
                                <asp:Label ID="lblnoNota" runat="server" Text="Label"></asp:Label>
		                    </td>
		                </tr>
		                <tr>
		                    <td>
		                        Jenis Pembayaran
		                    </td>
		                    <td>
                                <asp:DropDownList ID="DDLJenisPembayaran" runat="server">
                                </asp:DropDownList>
		                    </td>
		                </tr>
		                <tr>
		                    <td>
		                        Tanggal
		                    </td>
		                    <td>
                                <dxe:ASPxDateEdit ID="TbTanggal" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer">
                                </dxe:ASPxDateEdit>
		                    </td>
		                </tr>
		                <tr>
		                    <td>
		                        No Giro/Cek
		                    </td>
		                    <td>
                                <asp:TextBox ID="TxtNoGiro" runat="server"></asp:TextBox>
		                    </td>
		                    <td>
		                        Tanggal Jatuh Tempo
		                    </td>
		                    <td>
		                        <dxe:ASPxDateEdit ID="DtJatuhTempo" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer">
                                </dxe:ASPxDateEdit>
		                    </td>
		                </tr>
		                <tr>
		                    <td>
		                        No Invoice
		                    </td>
		                    <td>
                                <dxe:ASPxButtonEdit ID="TbNoInvoice" runat="server" ReadOnly="false">
                                <Buttons>
                                    <dxe:EditButton>
                                    </dxe:EditButton>
                                </Buttons>
                                <%--<ClientSideEvents ButtonClick="LoadGrid"  />--%>
                                <ClientSideEvents ButtonClick="function(s, e) {
									e.processOnServer = false; 
									PopUpInvoice.ShowAtElement(s.GetMainElement());
									}" />
                            </dxe:ASPxButtonEdit>
		                    </td>
		                    <td>
		                        Total Harga
		                    </td>
		                    <td>
                                <asp:TextBox ID="TxtTotalHarga" runat="server" ReadOnly = "false"></asp:TextBox>
		                    
		                    </td>
		                </tr>
		                <tr>
		                    <td>
		                        Pengirim
		                    </td>
		                    <td>
                                <asp:TextBox ID="TxtNmCustomer" runat="server" ReadOnly = "false"></asp:TextBox>
		                    </td>
		                    <td>
		                        Sisa Belum Bayar
		                    </td>
		                    <td>
		                        <asp:TextBox ID="TxtBlmBayar" runat="server" ReadOnly = "false"></asp:TextBox>
		                    </td>
		                </tr>
		                <tr>
		                    <td>
		                        
		                    </td>
		                    <td>
		                    </td>
		                    <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
		                </tr>
                        <tr>
                            <td>
                                Jumlah Bayar
                            </td>
                            <td>
                                <asp:TextBox ID="TxtJmlhBayar" runat="server" CssClass = "TextBoxOK" style="text-align:right;" AutoPostBack="false"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                Claim
                            </td>
                            <td>
                                <asp:TextBox ID="TxtClaim" runat="server" CssClass = "TextBoxOK" style="text-align:right;" AutoPostBack="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PPH</td>
                            <td>
                                <asp:TextBox ID="TxtPPH" runat="server" CssClass = "TextBoxOK" style="text-align:right;" AutoPostBack = "false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Discount
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDiscount" runat="server" CssClass = "TextBoxOK" style="text-align:right;" AutoPostBack="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan = "2">
                                <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
				        &nbsp;<asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan = "2">
                                <table>
                                    <tr>
                                        <td>
			                                <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px" 
                                                style="height: 25px">
				                                <Image Url="../images/save-alt.png" />
        				                        
			                                </dxe:ASPxButton>
		                                </td>
		                                <td>
        	                                <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
				                                <Image Url="../images/undo.png" />
			                                </dxe:ASPxButton>
		                                </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
		            </table>
		            <br />
		            <dxwgv:ASPxGridView ID="GridPelunasanInvoice" runat="server" AutoGenerateColumns = "false" ClientInstanceName = "GridPelunasanInvoice"
                         KeyFieldName = "ID" Font-Size = "9pt" Width = "80%">
                        <SettingsBehavior AllowFocusedRow="True" />
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

                            <dxwgv:GridViewDataTextColumn Caption="No Nota" FieldName="NoNota" Name="NoNota" 
                                Visible="true" VisibleIndex="1" Width="20px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ref No" FieldName="RefNo" 
                                Name="RefNo"  Visible="true" VisibleIndex="2" Width="20%">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataDateColumn Caption="Tanggal Transaksi" FieldName="TransDate" 
                                Name="TransDate"  Visible="true" VisibleIndex="3" Width="10%">
                                <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                            </dxwgv:GridViewDataDateColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Deskripsi" FieldName="Description" 
                                Name="Description"  Visible="true" VisibleIndex="4" >
                            </dxwgv:GridViewDataTextColumn>
                             <dxwgv:GridViewDataTextColumn Caption="Sisa" FieldName="Sisa" 
                                Name="Sisa"  Visible="true" VisibleIndex="4" Width="5%" >
                                <PropertiesTextEdit DisplayFormatString = "{0:###,###,###.00}"></PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="No Cek/Giro" FieldName="NoCekOrGiro" 
                                Name="NoCekOrGiro"  Visible="true" VisibleIndex="5" Width="10%" >
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tanggal Jatuh Tempo" FieldName="JatuhTempo" 
                                Name="JatuhTempo"  Visible="true" VisibleIndex="6" Width="10%" >
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Ket" FieldName="Ket" 
                                Name="Ket"  Visible="false" VisibleIndex="6" Width="10%" >
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Jenis" FieldName="Jenis" 
                                Name="Jenis"  Visible="false" VisibleIndex="6" Width="10%" >
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Yang Input Pembayaran" Name="YgInput" VisibleIndex="7" 
									    FieldName="YgInput">
						    </dxwgv:GridViewDataTextColumn>
						      <%--<dxwgv:GridViewDataTextColumn Caption="Yang Input Invoice" Name="YgInputInvoice" VisibleIndex="8" 
									    FieldName="YgInputInvoice">
						    </dxwgv:GridViewDataTextColumn>--%>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="9" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                        ToolTip="Delete Barang" >Edit</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="9" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                        ToolTip="Delete Barang" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                        </Columns>
                        <Templates>
                            <DetailRow>
                            
                                <dxwgv:ASPxGridView ID="GridPelunasanDetail" runat="server" 
                                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                                                Width="569px" ClientInstanceName = "GridPelunasanDetail" onbeforeperformdataselect = "GridPelunasanDetail_DataSelect" >
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
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="JournalSalesHeaderID" Name="JournalSalesHeaderID" 
                                                        visible="false" VisibleIndex="2" Caption="JournalSalesHeaderID" >
						                            </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="AccountNo" Name="AccountNo" 
                                                                    VisibleIndex="3" Caption="AccountNo" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="IDDetail" Name="IDDetail" VisibleIndex="4" Caption="IDDetail" Visible = "false" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Type" Name="Type" VisibleIndex="5" Caption="Type" >
						                            </dxwgv:GridViewDataTextColumn>
						                            
						                            <dxwgv:GridViewDataTextColumn FieldName="Amount" Name="Amount" VisibleIndex="6" Caption="Amount" >
						                                <PropertiesTextEdit DisplayFormatString="{0:###,###,###.00}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            
				                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" VisibleIndex="7" Caption="Description" Width="250px">
				                                    
						                            </dxwgv:GridViewDataTextColumn>
						                            
						                            </Columns>
						                            
                                            </dxwgv:ASPxGridView> 
                            </DetailRow>
                        </Templates>
                    </dxwgv:ASPxGridView>
		            
		            <asp:SqlDataSource ID="SqlDataSourceAccount" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
                        SelectCommand = "SELECT IH.ID, IH.No_Invoice, MC.Nama_Customer,IH.Ditujukan, IH.CodeCust as Kode_Customer,'Biasa' as Jenis, IH.Total,ISNULL(n.Sisa, 0) as Sisa FROM InvoiceHeader IH LEFT JOIN (select JH.RefNo, JH.Sisa FROM JournalSalesHeader JH JOIN (select MAX(runno) as runno, RefNo from JournalSalesHeader WHERE [status] <> 0 GROUP BY RefNo ) as M ON m.runno = JH.RunNo WHERE JH.status <> 0) as n ON IH.No_Invoice = n.RefNo LEFT JOIN MasterCustomer MC ON IH.CodeCust = MC.Kode_Customer WHERE IH.[status] <> 0 AND IH.[statuspembayaran] = 'Belum Bayar' AND MC.[status] = 1 UNION ALL SELECT A.ID, A.No_Asuransi as No_Invoice, MC.Nama_Customer,MC.Nama_Customer as Ditujukan, A.CodeCust as Kode_Customer,A.Jenis, A.Harga as Total,ISNULL(n.Sisa, 0) as Sisa FROM asuransi A LEFT JOIN (select JH.RefNo, JH.Sisa FROM JournalSalesHeader JH JOIN (select MAX(runno) as runno, RefNo from JournalSalesHeader WHERE [status] <> 0 GROUP BY RefNo ) as M ON m.runno = JH.RunNo WHERE JH.status <> 0) as n ON A.No_Asuransi = n.RefNo LEFT JOIN MasterCustomer MC ON A.CodeCust = MC.Kode_Customer WHERE A.[status] <> 0 AND A.[statuspembayaran] = 'Belum Bayar' AND MC.[status] = 1 ">

                    </asp:SqlDataSource>
                    
		            <asp:HiddenField ID="hfDitunjukan" runat="server" />
		            <asp:HiddenField ID="hfpengirim" runat="server" />
					<asp:HiddenField ID="hfNoInvoice" runat="server" />
					<asp:HiddenField ID="hfMode" runat="server" />
					<asp:HiddenField ID="hfAccountNo" runat="server" />
					<asp:HiddenField ID="hfNmAccount" runat="server" />
					<asp:HiddenField ID="hfTotalHarga" runat="server" />
					<asp:HiddenField ID="hfID" runat="server" />
					<asp:HiddenField ID="hfSisa" runat="server" />
					<asp:HiddenField ID="hfSisaBelumBayar" runat="server" />
					<asp:HiddenField ID="hfNoGiro" runat="server" />
					<asp:HiddenField ID="hfLebih" runat="server" />
					<asp:HiddenField ID="hfLebihSebelumnya" runat="server" />
					<asp:HiddenField ID="hfkodecustomer" runat="server" />
					<asp:HiddenField ID="HFJenis" runat="server" />
					<asp:HiddenField ID="HfKota" runat = "server" />
			
					    <dxcb:ASPxCallback ID="ASPxCallbackSisa" runat="server" ClientInstanceName="CBSisa" >
                            <ClientSideEvents CallbackComplete="function(s, e) {CBSisa_handeler(e.result);}" />
                        </dxcb:ASPxCallback>
		         </div>
		    </div>
    </div>
    </form>
</body>
</html>
