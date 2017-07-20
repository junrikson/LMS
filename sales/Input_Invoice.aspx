<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Input_Invoice.aspx.vb" Inherits="LMS.Input_Invoice" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Input Invoice</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <script language="javascript" type="text/javascript" src="../script/main.js" ></script>
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

        function getCustomer() {

            returnValue = ShowDialog2('MasterCustomer', 'Arg', '610', '450');
            if (returnValue) {

                var comp = new Array();
                comp = returnValue.split(";");
                var cust = document.getElementById("TxtNmLain");
                cust.value = comp[1];
                var cust3 = document.getElementById("HFNmCust");
                cust3.value = comp[1];
                var cust2 = document.getElementById("HFCodeCustLain");
                cust2.value = comp[0];
            }
        }
    
    function getnama() {

        returnValue = DialogInvoice('Nama', 'Arg', '610', '450');
        
        
        if (returnValue) {
            if (document.getElementById("HFmuatBarangID").value == "") {
                var comp = new Array();

                comp = returnValue.split(";");

                var hfmuatbarangid = document.getElementById("HFmuatBarangID");
                hfmuatbarangid.value = comp[0];

                var mbno = document.getElementById("HFMBNO");
                mbno.value = comp[1];
                
                var nama_customer = document.getElementById("HFNamaPengirim");
                nama_customer.value = comp[2];
                var nama_customer2 = document.getElementById("TxtNamaPengirim");
                nama_customer2.value = comp[2];

                var penerima = document.getElementById("HFPenerima");
                penerima.value = comp[3];
                var penerima2 = document.getElementById("TxtPenerima");
                penerima2.value = comp[3];

                var tujuan = document.getElementById("HFTujuan");
                tujuan.value = comp[4];
                var tujuan2 = document.getElementById("TxtTujuan");
                tujuan2.value = comp[4];

                var idkapal = document.getElementById("HFIDKapal");
                idkapal.value = comp[5];

                var namakapal = document.getElementById("TxtNamaKapal");
                namakapal.value = comp[6];
                var namakapal2 = document.getElementById("HFNamakapal");
                namakapal2.value = comp[6];
                var paid = document.getElementById("hfPaid");
                paid.value = comp[7];
                var CodeCust = document.getElementById("HFCodeCust");
                CodeCust.value = comp[8];
            }
            else {
                alert("Tekan reset untuk memasukan nama baru"); 
            }
        }
    }
   
    
</script>    
    
    <style type="text/css">
        .style4
        {
            width: 350px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Input Invoice</div>
		<br />
		<div  class="div_input" >
		    <div class="div_umum">
            <table >
            <tr>
                <td>
                    <table style="width: 600px" class = "borderdot">
                         
            <tr>
                    <td class="style4">
                        Nama Kapal</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="TxtNamaKapal" runat="server" CssClass = "textbox_1" 
                            Enabled="False" ></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td class="style4">
                        Nama Pengirim</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID = "TxtNamaPengirim" runat = "server" Enabled="False" CssClass = "textbox_1"
                              ></asp:TextBox>
                                        <img alt="Browse" onclick="javascript:getnama();" src="../images/search.png" /></td>
                </tr>
                <tr>
                    <td class="style4">
                        Tujuan</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID = "TxtTujuan" runat = "server" Enabled="False" CssClass = "textbox_1" ></asp:TextBox>
                                        </td>
                </tr>
                
                <tr>
                    <td class="style4">
                        Penerima</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID = "TxtPenerima" runat = "server" Enabled="False" CssClass = "textbox_1" ></asp:TextBox>
                                        </td>
                </tr>
                <tr>
                    <td class="style4">
                        <table>
					        <tr>
		                        <td align="right" >
			                        <dxe:ASPxButton ID="btAdd" runat="server" Text="OK" Width="90px" 
                                        style="height: 25px">
				                        <Image Url="../images/save-alt.png" />
				                        
			                        </dxe:ASPxButton>
		                        </td>
		                        <td align="left" >
        	                        <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
				                        <Image Url="../images/undo.png" />
			                        </dxe:ASPxButton>
		                        </td>        
		                    </tr>
					    </table>
					</td>
                </tr>
                <tr>
                    <td class="style4">
                        
                        
                    </td>
                </tr>
                
                    </table>
                </td>
            </tr>
            <br />
           </table>
           <br />
                <div align = "center" style="width:100%">
			        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                    <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
			    </div>
			    
                        <asp:HiddenField ID="HFInvoiceHeaderID" runat="server" />
                        <asp:HiddenField ID="HFMBNO" runat="server" />
                        <asp:HiddenField ID = "HFMuatBarangDetailID" runat="server" />
                        <asp:HiddenField ID = "HFID" runat="server" />
                        <asp:HiddenField ID = "HFMode" runat="server" />
                        <asp:HiddenField ID = "HFModeItem" runat="server" />
				        <asp:HiddenField ID="hfDel" runat="server" />
				        <asp:HiddenField ID="HFmuatBarangID" runat="server" />  
				        <asp:HiddenField ID="HFInvoiceDetailID" runat="server" />
				        <asp:HiddenField ID="HFTujuan" runat="server" />
				        <asp:HiddenField ID="HFNamaPengirim" runat="server" />
				        <asp:HiddenField ID="HFNmBarang" runat="server" />
				        <asp:HiddenField ID="HFSatuan" runat="server" />
				        <asp:HiddenField ID="HFIDKapal" runat="server" />
				        <asp:HiddenField ID="HFNamakapal" runat="server" />
				        <asp:HiddenField ID="HFPenerima" runat="server" />
				        <asp:HiddenField ID="HFNoInvoice" runat="server" />
				        <asp:HiddenField ID="HFPembayaran" runat="server" />
				        <asp:HiddenField ID="hfPaid" runat="server" />
				        <asp:HiddenField ID="HFTanggal" runat="server" />
				        <asp:HiddenField ID="HFPembayar" runat="server" />
				        <asp:HiddenField ID="HFCodeCust" runat="server" />
				        <asp:HiddenField ID="HFCodeCustLain" runat="server" />
				        <asp:HiddenField ID="HFNmCust" runat="server" />
				        <asp:HiddenField ID="HFTglBerangkat" runat="server" />
				<div align = "left">
				<table>
				<tr>
                    <td >
                        <table>
                            <tr>
                                <td>
                                    Tanggal Invoice
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="DtInvoice" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="147px">
                        </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
				</table>
				    <table>
				    
				        <tr>
				            <td>
				                Ditunjukan       
				            </td>
				            <td>
				                <asp:DropDownList ID="ddlDitunjukan" runat="server">
                        </asp:DropDownList>       
				            </td>
				            <td>
				            </td>
				            <td>
				                <asp:CheckBox ID = "ChkLainLain" runat="server" Text = "Lain-Lain" />
				            </td>
				            <td>
				                :
				            </td>
				            <td>
				                Nama :
				            </td>
				            <td>
				                <asp:TextBox ID="TxtNmLain" runat="server" Width="150px" Enabled="false"></asp:TextBox><img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
				            </td>
				            <td>
				                Kota :
				            </td>
				            <td>
				                <asp:TextBox ID="TxtKotaLain" runat="server" AutoPostBack = "false" Width="150px"></asp:TextBox>
				            </td>
				        </tr>
				        <tr>
				            <td>
				                Nama Barang :
				            </td>
				            <td colspan="8">
				                <asp:TextBox ID="TxtNmBrg" runat="server" AutoPostBack = "false" Width="500px"></asp:TextBox>
				            </td>
				        </tr>
				    </table>
				     
				</div>
				<br />
				<table>
				<tr>
                <td class="style4">
                        </td>
                    <td></td>
                    <td> <asp:TextBox ID="TxtNoInvoice" runat="server" AutoPostBack = "true" Width="256px" Visible = "false"></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                    <td>
                        Indikator </td>
                    <td></td>
                    <td> <asp:TextBox ID="TxtIndikator" runat="server" AutoPostBack = "False" 
                            Width="20" MaxLength="1"></asp:TextBox>
                    </td>
                    </tr>
                    
                    <tr>
                    <td class="style4">
                        <asp:CheckBox ID = "chkAsuransi" runat="server" Text = "Asuransi" AutoPostBack="True" Visible="false" /> </td>
                    <td></td>
                    
                </tr>
                
                </table> 
                <asp:Panel runat="server" ID="PanelAsuransi" Visible ="false">
				<table>
				    <tr>
				        <td>
				            No Asuransi
				        </td>
				        <td>
				            <asp:TextBox ID="TxtAsuransi" runat="server" Width="256px"></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
				        <td>
                            Harga
				        </td>
				        <td>
				            <asp:TextBox ID="TxtHargaAsuransi" runat="server"></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
				        <td>
				            Premi
				        </td>
				        <td>
                            <asp:TextBox ID="TxtPremi" runat="server"></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
				        <td>
				            Biaya Polis/Materai
				        </td>
				        <td>
                            <asp:TextBox ID="TxtPolis" runat="server"></asp:TextBox>
				        </td>
				    </tr>
				</table>
				</asp:Panel>  
				<table>
				    <tr>
				        <td>
                            <asp:CheckBox ID="ChkMiniByr" runat="server" Text = "Batas Minimum Pembayaran" /> 
				        </td>
				        <td>
                            <asp:TextBox ID="txtMinByr" runat="server" Width="172px"></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
                        <td>
                            <asp:CheckBox ID="ChkOngkos" runat="server" Text = "Ongkos Angkut" /> 
				        </td>
				        <td>
                            <asp:TextBox ID="TxtOngkos" runat="server" Width="172px"></asp:TextBox>
				        </td>
				        <td>
				            Jumlah
				        </td>
				        <td>
				            <asp:TextBox ID="TxtJumlahOngkosAngkutan" runat="server" Width="172px"></asp:TextBox>
				        </td>				    
				    </tr>
				    <tr>
				        <td>
				        
				        </td>
				    </tr>
				</table>
				 
                    <dxwgv:ASPxGridView ID="Grid_Pilih_invoice" runat="server" AutoGenerateColumns = "false" ClientInstanceName = "grid"
                 KeyFieldName = "NoI" Font-Size = "9pt" Width = "700px">
                <SettingsBehavior AllowFocusedRow="True" />
                
                <styles>
                    <header backcolor="#2c3848" font-bold="true" forecolor="#FFFFFF" 
                        horizontalalign="Center" hoverstyle-border-bordercolor="#515763">
                        <hoverstyle>
                            <border bordercolor="#515763"></border>
                        </hoverstyle>
                    </header>
                    <FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
                    
                    
                </styles>
                    <SettingsPager AlwaysShowPager="True" PageSize="5"></SettingsPager>
                    <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
                <Columns>
                <%--<dxwgv:GridViewCommandColumn Name="Checkbox" ShowSelectCheckbox="true" VisibleIndex="0" Width="1px" >
										<HeaderTemplate>
												<input id = "cbx" type="checkbox" onclick="grid.SelectAllRowsOnPage(this.checked);" style="vertical-align:middle;" title="Select/Unselect all rows on the page"></input>
										 </HeaderTemplate>
										<HeaderStyle HorizontalAlign="Center"/>
									</dxwgv:GridViewCommandColumn>--%>
                    
                   
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="NoI" Name="NoI" 
                        Visible="true" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="IDQuotationDetailI" FieldName="IDQuotationDetailI" Name="IDQuotationDetailI" 
                        Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                                        
                    <dxwgv:GridViewDataTextColumn Caption="Mb_No" FieldName="Mb_NoI" 
                        Name="Mb_NoI"  Visible="false" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Jenis Barang" FieldName="Nama_BarangI" 
                        Name="Nama_BarangI"  Visible="true" VisibleIndex="1" Width="200px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Jenis Pembayaran" FieldName="JenisPembayaranI" 
                        Name="JenisPembayaranI"  Visible="true" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                   
                    <dxwgv:GridViewDataTextColumn Caption="Harga" FieldName="HargaI" 
                        Name="HargaI"  Visible="true" VisibleIndex="3" Width="140px">
                        <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Total Quantity" FieldName="TotalQtyI" 
                        Name="TotalQtyI"  Visible="true" VisibleIndex="4" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume Total(M3)" FieldName="VolumeTotalI" 
                        Name="VolumeTotalI"  Visible="true" VisibleIndex="9" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                   <dxwgv:GridViewDataTextColumn Caption="Berat Total(kg)" FieldName="BeratTotalI" 
                        Name="BeratTotalI"  Visible="true" VisibleIndex="9" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Total Bayar" FieldName="TotalByrI" 
                        Name="TotalByrI" VisibleIndex="9" Visible ="true">
                        <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Paid" FieldName="PaidI" 
                        Name="PaidI" VisibleIndex="9" Visible ="true">
                    </dxwgv:GridViewDataTextColumn>

                    
                </Columns>
               

            </dxwgv:ASPxGridView>
            
                        <tr>
                    <td >
                         Total yang Harus Dibayar :<asp:Label ID="LblBayar" runat="server" 
                    Font-Bold="True" Font-Size="Larger"></asp:Label>
                    </td>
                </tr><br />
                    </td>
                </tr>
                
                
                <tr>
                    <td colspan = "3" align = "center">
                       <%-- <asp:Button ID="btnHitung" runat="server" Text="Hitung Total" CssClass= "btn" />--%>
                        
                        <asp:Panel runat = "server" ID = "PanelHitung">
                        <br />
                        <table style="border : solid 2px black;">
                            
                            <tr>
                                <td>
                                    Harga Satuan
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtHarga" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Total Bayar
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtTotalBayar" runat="server" Enabled="False"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    <table width= "100%">
                <tr>
                    <td align ="center">
                        <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
							                <Image Url="../images/save-alt.png" />
							                
						                </dxe:ASPxButton>
                        <br />
                    </td>
                </tr>
            </table>
                      
						                
                        <br />
                    </td>
                </tr>
                
                <dxwgv:ASPxGridView ID="Grid_Invoice_Parent" ClientInstanceName="grid_parent" 
                                runat="server" KeyFieldName="ID" 
								 AutoGenerateColumns="False" Width = "100%">			
								<Styles>
									<Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#ffffff" Font-Bold="true">
                                        <HoverStyle>
                                        <Border BorderColor="#515763"></Border>
                                        </HoverStyle>
                                    </Header>
									<FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
									<AlternatingRow Enabled="True" BackColor="#f4f4e9"></AlternatingRow>
									<Row BackColor="#ffffff"></Row>
									<Cell Paddings-PaddingLeft="3" Paddings-PaddingRight="3" Paddings-PaddingBottom="1" Paddings-PaddingTop="1">
                                        <Paddings PaddingLeft="3px" PaddingTop="1px" PaddingRight="3px" PaddingBottom="1px"></Paddings>
                                    </Cell>
								</Styles>
								<SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
								<Settings ShowFilterRow="True" />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
								<Columns>
									<dxwgv:GridViewDataTextColumn FieldName="ID" Name="ID" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataTextColumn>
									
									<dxwgv:GridViewDataTextColumn FieldName="No_Invoice" Name="No_Invoice" 
                                        VisibleIndex="2" Caption="No Invoice" >
									</dxwgv:GridViewDataTextColumn>
									
									<dxwgv:GridViewDataTextColumn FieldName="MuatBarangID" Name="MuatBarangID" 
                                        VisibleIndex="3" Caption="MuatBarangID" Visible = "false">
									</dxwgv:GridViewDataTextColumn>									
									<dxwgv:GridViewDataTextColumn FieldName="Pengirim" Name="pengirim" 
                                        VisibleIndex="4" Caption="Pengirim" Visible = "true">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Penerima" Name="Penerima" 
                                        VisibleIndex="5" Caption="Penerima" Visible = "true" >
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="NamaKapal" Name="NamaKapal" 
                                        VisibleIndex="7" Caption="NamaKapal" >
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="IDKapal" Name="IDKapal" 
                                        VisibleIndex="8" Caption="IDKapal" Visible = "false">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Ditujukan" Name="Ditujukan" 
                                        VisibleIndex="9" Caption="Ditujukan">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="DaerahDitujukan" Name="DaerahDitujukan" 
                                        VisibleIndex="10" Caption="Daerah Ditujukan">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Tujuan" Name="Tujuan" 
                                        VisibleIndex="11" Caption="Tujuan">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Paid" Name="Paid" 
                                        VisibleIndex="11" Caption="Paid">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="NamaBarang" Name="NamaBarang" 
                                        VisibleIndex="11" Caption="Nama Barang">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Total" Name="Total" 
                                        VisibleIndex="12" Caption="Total Harga">
                                       <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
									</dxwgv:GridViewDataTextColumn>
									
									<dxwgv:GridViewDataDateColumn FieldName="TglInvoice" Name="TglInvoice" 
                                        VisibleIndex="13" Caption="Tanggal Invoice">
									<PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
									</dxwgv:GridViewDataDateColumn>
									<dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="Keterangan" 
                                        Name="Keterangan"  Visible="true" VisibleIndex="14">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="KeteranganOngkosAngkut" FieldName="KeteranganOngkosAngkut" 
                                        Name="KeteranganOngkosAngkut"  Visible="true" VisibleIndex="15">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Yang Input" Name="YgInput" VisibleIndex="16" 
									    FieldName="YgInput" Width="150px">
								    </dxwgv:GridViewDataTextColumn>
									
									<dxwgv:GridViewDataTextColumn Name="Edit" Caption="#" VisibleIndex="16" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataTextColumn>
								    <%--<dxwgv:GridViewDataTextColumn Name="Delete" Caption="#" VisibleIndex="17" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataTextColumn>		--%>			    
								    <%--<dxwgv:GridViewDataTextColumn Name="Close" Caption="#" VisibleIndex="16" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbClose" ToolTip="Close Invoice" CommandName="Close" runat="server" OnClientClick="return confirm('Are You Sure Want to Close Invoice?');" >Close</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataTextColumn>	--%>		
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
								        </DetailRow>
								    </Templates>
							    </dxwgv:ASPxGridView>
                
            
            </div>
        </div>
        </div>
    </form>
    
</body>
</html>
