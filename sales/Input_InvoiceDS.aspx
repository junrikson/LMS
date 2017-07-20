<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Input_InvoiceDS.aspx.vb" Inherits="LMS.Input_InvoiceDS" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Input Invoice DS</title>
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

            returnValue = ShowDialog2('CustomerInvoiceDS', 'Arg', '610', '450');
            if (returnValue) {

                var comp = new Array();
                comp = returnValue.split(";");
                var cust = document.getElementById("TxtNamaPengirim");
                cust.value = comp[1];
                var cust3 = document.getElementById("HFNmCust");
                cust3.value = comp[1];
                var cust2 = document.getElementById("HFCodeCust");
                cust2.value = comp[0];
                var cust4 = document.getElementById("TxtDaerahDitujukan");
                cust4.value = comp[2];
                var cust5 = document.getElementById("HFDaerahDitujukan");
                cust5.value = comp[2];
            }
        }
   
    
</script>    
    
    <style type="text/css">
        .style4
        {
            width: 350px;
        }
        .style5
        {
            width: 155px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Input Invoice DS</div>
		<br />
		<div  class="div_input" >
		    <div class="div_umum">
            <table >
            <tr>
                <td>
                    <table style="width: 800px" class = "borderdot">
                       <tr>
                                <td class="style5">
                                    Tanggal Kapal
                                </td>
                                <td>:</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="DtInvoice" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="147px">
                        </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    Jenis Pembayaran
                                </td>
                                <td>
                                       <asp:DropDownList ID="DDLSatuan" runat="server" AutoPostBack="true">
                        </asp:DropDownList>   
                           </td>
                            </tr>  
            <tr>
                    <td class="style5">
                        Nama Kapal</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="DDLKapal" runat="server">
                        </asp:DropDownList>   
                    </td>
                    <td>
                        Harga 
                    </td>
                    <td>
                        <asp:TextBox ID = "TxtHargaByr" runat = "server"
                              ></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td class="style5">
                        Ditujukan</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID = "TxtNamaPengirim" runat = "server" Enabled="False" CssClass = "textbox_1"
                              ></asp:TextBox>
                                        <img alt="Browse" onclick="javascript:getCustomer();" src="../images/search.png" /></td>
                                        <td>
                                            Quantity
                                        </td>
                                        <td>
                                            <asp:TextBox ID = "TxtQty" runat = "server"></asp:TextBox>
                                        </td>
                </tr>
                <tr>
                    <td class="style5">
                        Kota</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID = "TxtDaerahDitujukan" runat = "server" Enabled="False" CssClass = "textbox_1" ></asp:TextBox>
                                        </td>
                    <td>
                                    Nama Barang
                                </td>
                                <td>
                                    <asp:TextBox ID = "TxtNamaBarang" runat = "server" ></asp:TextBox>
                                        </td>
                                </td>
                            </tr>           
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    
                    </td>
                    <td>
                    
                    </td>
                    <td>
                                            Ukuran
                                        </td>
                                        <td>
                                            <asp:TextBox ID = "TxtUkuran" runat = "server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="Lblukuran" runat="server" Text=""></asp:Label>
                    
                                       
                           </td>
                </tr>
                <tr>
                    <td class="style5">
                    
                    </td>
                    <td>
                    
                    </td>
                    <td>
                    
                    </td>
                    <td>
                    Satuan
                                </td>
                                <td>
                                       <asp:DropDownList ID="DDLSatuanbrg" runat="server">
                        </asp:DropDownList>
                                        </td>
                </tr>
                
                <tr>
                    <td colspan="3">
                    </td>
                    <td class="style5">
                        <table>
					        <tr>
		                        <td align="right" >
			                        <dxe:ASPxButton ID="btAdd" runat="server" Text="Tambah" Width="90px" 
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
                    <td class="style5">
                        
                        
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
                    <TR>
                    <td class="style4">
                        <asp:CheckBox ID = "chkAsuransi" runat="server" Text = "Asuransi" AutoPostBack="True" Visible = "false" /> </td>
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
				 
                    <dxwgv:ASPxGridView ID="Grid_Invoice_DS_Detail" runat="server" AutoGenerateColumns = "false" ClientInstanceName = "Grid_Invoice_DS_Detail"
                 KeyFieldName = "No" Font-Size = "9pt" Width = "700px">
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
                    
                   
                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="No" Name="No" 
                        Visible="true" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="IDJenisPembayaran" FieldName="IDJenisPembayaran" Name="IDJenisPembayaran" 
                        Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                                        
                    <dxwgv:GridViewDataTextColumn Caption="Jenis Pembayaran" FieldName="JenisPembayaran" 
                        Name="JenisPembayaran"  Visible="True" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Harga" FieldName="Harga" 
                        Name="Harga"  Visible="true" VisibleIndex="1" Width="200px">
                         <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Quantity" FieldName="Quantity" 
                        Name="Quantity"  Visible="true" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                   
                    <dxwgv:GridViewDataTextColumn Caption="NamaBarang" FieldName="NamaBarang" 
                        Name="NamaBarang"  Visible="true" VisibleIndex="3" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="IDSatuan" FieldName="IDSatuan" 
                        Name="IDSatuan"  Visible="false" VisibleIndex="4" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Satuan" FieldName="Satuan" 
                        Name="Satuan"  Visible="true" VisibleIndex="4" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ukuran" FieldName="Ukuran" 
                        Name="Ukuran"  Visible="true" VisibleIndex="9" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Total Bayar" FieldName="TotalByr" 
                        Name="TotalByr" VisibleIndex="9" Visible ="true">
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
                
                <dxwgv:ASPxGridView ID="Grid_Invoice_ParentDS" ClientInstanceName="Grid_Invoice_ParentDS" 
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
									<dxwgv:GridViewDataTextColumn FieldName="NamaKapal" Name="NamaKapal" 
                                        VisibleIndex="7" Caption="NamaKapal" >
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="IDKapal" Name="IDKapal" 
                                        VisibleIndex="8" Caption="IDKapal" Visible = "false">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="CodeCust" Name="CodeCust" 
                                        VisibleIndex="8" Caption="CodeCust" Visible = "false">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Ditujukan" Name="Ditujukan" 
                                        VisibleIndex="9" Caption="Ditujukan">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="DaerahDitujukan" Name="DaerahDitujukan" 
                                        VisibleIndex="10" Caption="Daerah Ditujukan">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Paid" Name="Paid" Visible = "false" 
                                        VisibleIndex="11" Caption="Paid">
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
									    FieldName="YgInput">
								    </dxwgv:GridViewDataTextColumn>
									
									<dxwgv:GridViewDataTextColumn Name="Edit" Caption="#" VisibleIndex="16" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataTextColumn>
								    <dxwgv:GridViewDataTextColumn Name="Delete" Caption="#" VisibleIndex="17" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
								    </DataItemTemplate>
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
                                                    <dxwgv:GridViewDataTextColumn Caption="No_Invoice" FieldName="No_Invoice" Name="No_Invoice" 
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                   <dxwgv:GridViewDataTextColumn FieldName="Nama_Barang" Name="Nama_Barang" 
                                                                    VisibleIndex="2" Caption="Nama Barang" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Quantity" Name="Quantity" 
                                                                    VisibleIndex="2" Caption="Quantity" >
						                            </dxwgv:GridViewDataTextColumn>
						                             <dxwgv:GridViewDataTextColumn FieldName="TotalUkuran" Name="TotalUkuran" 
                                                                    VisibleIndex="2" Caption="TotalUkuran" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="NamaHarga" Name="NamaHarga" VisibleIndex="3" Caption="Jenis Pembayaran" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="HargaSatuan" Name="HargaSatuan" VisibleIndex="4" Caption="HargaSatuan" >
						                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="TotalHarga" Name="TotalHarga" VisibleIndex="5" Caption="Harga Total" >
						                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
								                </Columns>						                            
                                            </dxwgv:ASPxGridView>
								                </td>
								                
								            </tr>
				                            
                                            </table>
								        </DetailRow>
								    </Templates>
							    </dxwgv:ASPxGridView>
                
            
            </div>
        </div>
        </div>
        <asp:HiddenField ID="HFInvoiceHeaderID" runat="server" />
                        <asp:HiddenField ID = "HFID" runat="server" />
                        <asp:HiddenField ID = "HFMode" runat="server" />
                        <asp:HiddenField ID = "HFModeItem" runat="server" />
				        <asp:HiddenField ID="hfDel" runat="server" />
				        <asp:HiddenField ID="HFInvoiceDetailID" runat="server" />
				        <asp:HiddenField ID="HFTujuan" runat="server" />
				        <asp:HiddenField ID="HFSatuan" runat="server" />
				        <asp:HiddenField ID="HFIDDetail" runat="server" />
				        <asp:HiddenField ID="HFNamakapal" runat="server" />
				        <asp:HiddenField ID="HFPenerima" runat="server" />
				        <asp:HiddenField ID="HFNoInvoice" runat="server" />
				        <asp:HiddenField ID="HFPembayaran" runat="server" />
				        <asp:HiddenField ID="hfPaid" runat="server" />
				        <asp:HiddenField ID="HFCodeCust" runat="server" />	        
				        <asp:HiddenField ID="HFNmCust" runat="server" />
				        <asp:HiddenField ID="HFDaerahDitujukan" runat="server" />
    </form>
    
</body>
</html>
