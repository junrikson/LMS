<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EditInvoice.aspx.vb" Inherits="LMS.EditInvoice" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Invoice</title>
    
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <script language="javascript" type="text/javascript" src="../script/main.js" ></script>
    <script language="javascript" src="../script/NumberFormat.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">

        function getCustomer() {

            returnValue = ShowDialog2('MasterCustomer', 'Arg', '610', '450');
            if (returnValue) {

                var comp = new Array();
                comp = returnValue.split(";");
                var cust = document.getElementById("TxtNamaCustomer");
                cust.value = comp[1];
                var cust3 = document.getElementById("HFNamaCust");
                cust3.value = comp[1];
                var cust2 = document.getElementById("HFCodeCust");
                cust2.value = comp[0];
                var cust4 = document.getElementById("TxtCodeCustomer");
                cust4.value = comp[0];
            }
        }

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
    
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Edit Invoice</div>
		<br />
		<div  class="div_input" >
		    <div class="div_umum">
                         
			    
                        <asp:HiddenField ID="HFInvoiceHeaderID" runat="server" />
                        <asp:HiddenField ID="HFMBNO" runat="server" />
                        <asp:HiddenField ID = "HFMuatBarangDetailID" runat="server" />
                        <asp:HiddenField ID = "HFID" runat="server" />
                        <asp:HiddenField ID = "HFMode" runat="server" />
				        <asp:HiddenField ID="hfModeItem" runat="server" />
				        <asp:HiddenField ID="HFmuatBarangID" runat="server" />  
				        <asp:HiddenField ID="HFInvoiceDetailID" runat="server" />
				        <asp:HiddenField ID="HFTujuan" runat="server" />
				        <asp:HiddenField ID="HFIDDetailInvoice" runat="server" />
				        <asp:HiddenField ID="HFNamakapal" runat="server" />
				        <asp:HiddenField ID="HFNoInvoice" runat="server" />
				        <asp:HiddenField ID="HFPembayaran" runat="server" />
				        <asp:HiddenField ID="hfPaid" runat="server" />
				        <asp:HiddenField ID="HFTanggal" runat="server" />
				        <asp:HiddenField ID="HFCodeCust" runat="server" />
				        <asp:HiddenField ID="HFNamaCust" runat="server" />
				<div align = "left" style="width:100%">
				     
				</div>
				<br />
				<table>
				
                    <tr>
                <td>
                        No Invoice </td>
                    <td> <asp:TextBox ID="TxtNoInvoice" runat="server" Width="200px" Enabled="true" ReadOnly = "true"></asp:TextBox></td>
                    <td></td>
                    <td>
                        Jenis Barang
                    </td>
                    <td>
                        <asp:TextBox ID="TxtJenisBarang" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                        
                    
               </tr>
               <tr>
                    <td>
                        Total Harga Invoice
                    </td>
                    <td>
                        <asp:TextBox ID="TxtHargaInvoice" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        Jenis Pembayaran
                    </td>
                    <td>
                        <asp:TextBox ID="TxtJenisPembayaran" runat="server" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
               </tr>
               
               <tr>
                    <td>
                        Tanggal
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="DtInvoice" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="147px">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td></td>
                    <td>
                        HargaPersatuan
                    </td>
                    <td>
                        <asp:TextBox ID="TxtHargaSatuan" runat="server" Width="200px" Enabled = "false"></asp:TextBox>
                    </td>
               </tr>
               <tr>
                    <td>
                        Nama Kapal
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLKapal" runat="server">
                        </asp:DropDownList>  
                    </td>
                    <td>
                    </td>
                    <td>
                        Total UKuran
                    </td>
                    <td>
                        <asp:TextBox ID="TxtTotalUkuran" runat="server" Width="200px"></asp:TextBox>
                    </td>
               </tr>
               
               <tr>
                <td>
				                Nama Customer
				            </td>
				            <td>
				                <asp:TextBox ID="TxtNamaCustomer" runat="server" Width="200px"></asp:TextBox><img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
				            </td>
               </tr>
               <tr>
				            <td>
				                Code Customer
				            </td>
				            <td>
				                 <asp:TextBox ID="TxtCodeCustomer" runat="server" Width="200px" ReadOnly="false"></asp:TextBox>
				            </td>
				        </tr>
				        <tr>
				            <td>
				                Daerah Ditujukan
				            </td>
				            <td>
				                <asp:TextBox ID="TxtDaerahDitujukan" runat="server" Width="200px"></asp:TextBox>
				            </td>
				        </tr>
               
               
               <tr>
                    <td colspan = "3">
                    
                    </td>
                    <td colspan = "2">
                        <dxe:ASPxButton ID="BtnTambah" runat="server" Text="Tambah" Width="90px">
							                <Image Url="../images/add.png" />
							                
						                </dxe:ASPxButton>
                    </td>
               </tr>
                
                </table> 
                
                   
                        <br />
                <table>
                    <tr>
                        <td>
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
                                                            
                                       <dxwgv:GridViewDataTextColumn Caption="Jenis Barang" FieldName="Nama_BarangI" 
                                            Name="Nama_BarangI"  Visible="true" VisibleIndex="1" Width="200px">
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Jenis Pembayaran" FieldName="JenisPembayaranI" 
                                            Name="JenisPembayaranI"  Visible="true" VisibleIndex="1" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                       
                                        <dxwgv:GridViewDataTextColumn Caption="Harga Per Satuan" FieldName="HargaI" 
                                            Name="HargaI"  Visible="true" VisibleIndex="3" Width="140px">
                                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                       
                                        <dxwgv:GridViewDataTextColumn Caption="TotalUkuran" FieldName="TotalUkuranI" 
                                            Name="TotalUkuranI"  Visible="true" VisibleIndex="9" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                                                               <dxwgv:GridViewDataTextColumn Caption="Total Bayar" FieldName="TotalByrI" 
                                            Name="TotalByrI" VisibleIndex="9" Visible ="true">
                                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                                        </dxwgv:GridViewDataTextColumn>
                                        <dxwgv:GridViewDataTextColumn Caption="Paid" FieldName="PaidI" 
                                            Name="PaidI" VisibleIndex="9" Visible ="true">
                                        </dxwgv:GridViewDataTextColumn>
                                        
                                        <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="12" Width="1%">
                                            <dataitemtemplate>
                                                <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                                    ToolTip="Edit Item">Edit</asp:LinkButton>
                                            </dataitemtemplate>
                                        </dxwgv:GridViewDataColumn>
                                        
                                    </Columns>
                                   

                                </dxwgv:ASPxGridView>
                        
                        </td>
                    </tr>
                </table>

                <div align = "center" style="width:100%">
			        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                    <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
			    </div>
                

                        
                        <table width= "100%">
                <tr>
                    <td align ="right">
                        <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
							                <Image Url="../images/save-alt.png" />
							                
						                </dxe:ASPxButton>
                        <br />
                    </td>
                    <td align="left" valign="top" >
        	                        <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
				                        <Image Url="../images/undo.png" />
			                        </dxe:ASPxButton>
		                        </td> 
                </tr>
            </table>
                      
						                
                        <br />
                
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
									<dxwgv:GridViewDataDateColumn FieldName="TglInvoice" Name="TglInvoice" 
                                        VisibleIndex="3" Caption="Tanggal Invoice">
									<PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
									</dxwgv:GridViewDataDateColumn>
									<dxwgv:GridViewDataTextColumn FieldName="MuatBarangID" Name="MuatBarangID" 
                                        VisibleIndex="3" Caption="MuatBarangID" Visible = "false">
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
									<dxwgv:GridViewDataTextColumn FieldName="CodeCust" Name="CodeCust" 
                                        VisibleIndex="10" Caption="Kode Customer">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="DaerahDitujukan" Name="DaerahDitujukan" 
                                        VisibleIndex="10" Caption="Daerah Ditujukan">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Total" Name="Total" 
                                        VisibleIndex="12" Caption="Total Harga">
                                       <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="UpdatedBy" Name="UpdatedBy" 
                                        VisibleIndex="10" Caption="Updated by">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn Name="Edit" Caption="#" VisibleIndex="14" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
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
								                
								            </tr>
				                            
                                            </table>
								        </DetailRow>
								    </Templates>
							    </dxwgv:ASPxGridView>
                
            
            </div>
        </div>
        </div>
    </form>
</body>
</html>
