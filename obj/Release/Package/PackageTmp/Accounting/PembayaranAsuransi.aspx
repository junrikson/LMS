<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PembayaranAsuransi.aspx.vb" Inherits="LMS.PembayaranAsuransi" %>

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
<head id="Head1" runat="server">
    <title>Pembayaran Asuransi</title>
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


            function OnGetRowValuesAsuransi(values) {

                document.getElementById("hfNoAsuransi").value = values[1];
			    TbNoAsuransi.SetValue(values[1]);
			    document.getElementById("TxtNmCustomer").value = values[2];
			    document.getElementById("hfNamaCustomer").value = values[2];
			    document.getElementById("TxtTotalHarga").value = values[3];
			    document.getElementById("hfTotalHarga").value = values[3];
			    changenumberformat("TxtTotalHarga")
			    PopUpAsuransi.Hide();
			    setTimeout("CBSisa.callBack(document.getElementById('hfNoAsuransi').value);", 100);
			}

			function CBSisa_handeler(result) {
			    document.getElementById("TxtSisaHrsByr").value = result;
			    document.getElementById("hfSisaBelumBayar").value = result;
			}

			function OnGetRowValuesCOA(values) {

			    document.getElementById("hfAccountNo").value = values[1];
			    tbPembayaran.SetValue(values[1]);
			    document.getElementById("lAccountNo").value = values[2];
			    PopUpPembayaran.Hide();

			}
   
  </script>
        
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Pembayaran Asuransi</div>
		<dxpc:ASPxPopupControl ID="PopUpAsuransi" runat="server" ClientInstanceName="PopUpAsuransi" HeaderText="List Asuransi" >
		    <Controls>
		        <dxwgv:ASPxGridView ID="GridAsuransi" runat="server" DataSourceID="SqlDataSourceAccount" 
                    AutoGenerateColumns = "False" ClientInstanceName = "GridAsuransi"
                 KeyFieldName = "ID" Font-Size = "9pt" Width = "500px">
                <SettingsBehavior AllowFocusedRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="ID" Name="ID" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataTextColumn>
									
									<dxwgv:GridViewDataTextColumn FieldName="No_Asuransi" Name="No_Asuransi" 
                                        VisibleIndex="2" Caption="No Asuransi" >
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataDateColumn FieldName="TglAsuransi" Name="TglAsuransi" 
                                        VisibleIndex="3" Caption="Tanggal Asuransi">
									<PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
									</dxwgv:GridViewDataDateColumn>									
									<dxwgv:GridViewDataTextColumn FieldName="NamaKapal" Name="NamaKapal" 
                                        VisibleIndex="7" Caption="NamaKapal" >
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="IDKapal" Name="IDKapal" 
                                        VisibleIndex="8" Caption="IDKapal" Visible = "false">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Ditujukan" Name="Ditujukan" 
                                        VisibleIndex="9" Caption="Ditujukan">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="CodeCust" Name="CodeCust" 
                                        VisibleIndex="10" Caption="Kode Customer">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Harga" Name="Harga" 
                                        VisibleIndex="11" Caption="Harga">
                                       <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Keterangan" Name="Keterangan" 
                                        VisibleIndex="12" Caption="Keterangan">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="StatusPembayaran" Name="StatusPembayaran" 
                                        VisibleIndex="13" Caption="Status" Visible="false">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Type" Name="Type" 
                                        VisibleIndex="13" Caption="Type" Visible="false">
									</dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <ClientSideEvents RowDblClick="function(s, e) {
               	    GridAsuransi.GetRowValues(GridAsuransi.GetFocusedRowIndex(), 'ID;No_Asuransi;Ditujukan;Harga', OnGetRowValuesAsuransi)}" />
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
                                <asp:Label ID="lblnoNota" runat="server" Text=""></asp:Label>
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
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" ReadOnly = "false">
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
		                </tr>
		                <tr>
		                    <td>
		                        No Asuransi</td>
		                    <td>
                                <dxe:ASPxButtonEdit ID="TbNoAsuransi" runat="server" ReadOnly="True">
                                <Buttons>
                                    <dxe:EditButton>
                                    </dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
									e.processOnServer = false; 
									PopUpAsuransi.ShowAtElement(s.GetMainElement());
									}" />
                            </dxe:ASPxButtonEdit>
		                    </td>
                            <td>
		                                    Total Bayar
		                    </td>
		                    <td>	                                    
                                            <asp:TextBox ID="TxtTotalHarga" runat="server" ReadOnly = "true"></asp:TextBox>
		                    </td>
		                    
		                </tr>
		                <tr>
		                   
		                </tr>
		                <tr>
		                    <td>
		                        Nama Customer
		                    </td>
		                    <td>
                                <asp:TextBox ID="TxtNmCustomer" runat="server" ReadOnly = "true"></asp:TextBox>
		                    </td>
		                    <td>
		                        Sisa yg Dibayar
		                    </td>
		                    <td>
		                        <asp:TextBox ID="TxtSisaHrsByr" runat="server" ReadOnly = "true"></asp:TextBox>
		                    </td>
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
                                Discount
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDiscount" runat="server" CssClass = "TextBoxOK" style="text-align:right;" AutoPostBack="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                        <tr>
                            <td>
                                Sisa Bayar
                            </td>
                            <td>
                                <asp:TextBox ID="TxtSisa" runat="server" CssClass = "TextBoxOK" 
                                    style="text-align:right;" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Kelebihan
                            </td>
                            <td>
                                <asp:TextBox ID="TxtLebih" runat="server" CssClass = "TextBoxOK" 
                                    style="text-align:right;" ReadOnly="True"></asp:TextBox>
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
		            
		           <dxwgv:ASPxGridView ID="GridPelunasanAsuransi" runat="server" AutoGenerateColumns = "false" ClientInstanceName = "GridPelunasanAsuransi"
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
                                <PropertiesTextEdit DisplayFormatString = "{0:###,###,###}"></PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="No Cek/Giro" FieldName="NoCekOrGiro" 
                                Name="NoCekOrGiro"  Visible="true" VisibleIndex="5" Width="10%" >
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="7" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbEdit" runat="server" CommandName="Delete" 
                                        ToolTip="edit" >Edit</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>

                            <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="7" Width="1%">
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
                                                <SettingsBehavior AllowFocusedRow="True" />
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
						                            <dxwgv:GridViewDataTextColumn FieldName="IDDetail" Name="IDDetail" VisibleIndex="4" Caption="IDDetail" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Type" Name="Type" VisibleIndex="5" Caption="Type" >
						                            </dxwgv:GridViewDataTextColumn>
						                            
						                            <dxwgv:GridViewDataTextColumn FieldName="Amount" Name="Amount" VisibleIndex="6" Caption="Amount" >
						                            <PropertiesTextEdit DisplayFormatString = "{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            
				                                    <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" VisibleIndex="7" Caption="Description" Width="250px">
				                                    
						                            </dxwgv:GridViewDataTextColumn>
						                            
						                            </Columns>
						                            
                                            </dxwgv:ASPxGridView> 
                            </DetailRow>
                        </Templates>
                    </dxwgv:ASPxGridView>
		            
		            <asp:SqlDataSource ID="SqlDataSourceAccount" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
                         SelectCommand="select A.ID, A.No_Asuransi, A.KapalID, A.Harga, A.TglAsuransi, A.CodeCust,A.Keterangan,A.StatusPembayaran,K.Nama_Kapal as NamaKapal, MC.Nama_Customer as Ditujukan, A.Type FROM Asuransi A JOIN MasterCustomer MC ON A.CodeCust = MC.Kode_Customer JOIN Kapal K ON A.KapalID = K.IDDetail WHERE A.[status] <> 0 AND MC.status = 1 AND K.status = 1 AND A.Type='Freight' AND A.statuspembayaran<>'Lunas' UNION ALL select A.ID, A.No_Asuransi, '0' as KapalID, A.Harga, A.TglAsuransi, A.CodeCust,A.Keterangan,A.StatusPembayaran,'' as NamaKapal, MC.Nama_Customer as Ditujukan, A.Type FROM Asuransi A JOIN MasterCustomer MC ON A.CodeCust = MC.Kode_Customer WHERE A.[status] <> 0 AND MC.status = 1 AND A.Type = 'Non-Freight' AND A.statuspembayaran <> 'Lunas'  ">
                    </asp:SqlDataSource>
                    
                    
		            <asp:HiddenField ID="hfNamaCustomer" runat="server" />
					<asp:HiddenField ID="hfNoAsuransi" runat="server" />
					<asp:HiddenField ID="hfMode" runat="server" />
					<asp:HiddenField ID="hfTotalHarga" runat="server" />
					
					<asp:HiddenField ID="hfHargaAsuransi" runat="server" />
					<asp:HiddenField ID="hfID" runat="server" />
					<asp:HiddenField ID="hfSisa" runat="server" />
					<asp:HiddenField ID="hfLebih" runat="server" />
					<asp:HiddenField ID="hfNoGiro" runat="server" />
					<asp:HiddenField ID="hfSisaBelumBayar" runat="server" />
					    <dxcb:ASPxCallback ID="ASPxCallbackSisa" runat="server" ClientInstanceName="CBSisa" >
                            <ClientSideEvents CallbackComplete="function(s, e) {CBSisa_handeler(e.result);}" />
                        </dxcb:ASPxCallback>
		         </div>
		    </div>
    </div>
    </form>
</body>
</html>
