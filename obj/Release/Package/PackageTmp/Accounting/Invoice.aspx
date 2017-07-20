<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Invoice.aspx.vb" Inherits="LMS.Invoice" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Invoice</title>
    
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
                var cust = document.getElementById("TxtNmLain");
                cust.value = comp[1];
                var cust3 = document.getElementById("HFNmCust");
                cust3.value = comp[1];
                var cust2 = document.getElementById("HFCodeCust");
                cust2.value = comp[0];
                var cust3 = document.getElementById("TxtCodeCustomer");
                cust3.value = comp[0];
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
        <div class="formtitle">Input Invoice</div>
		<br />
		<div  class="div_input" >
		    <div class="div_umum">
            
           <br />
                
			    
                        <asp:HiddenField ID="HFInvoiceHeaderID" runat="server" />
                        <asp:HiddenField ID="HFMBNO" runat="server" />
                        <asp:HiddenField ID = "HFMuatBarangDetailID" runat="server" />
                        <asp:HiddenField ID = "HFID" runat="server" />
                        <asp:HiddenField ID = "HFMode" runat="server" />
				        <asp:HiddenField ID="hfDel" runat="server" />
				        <asp:HiddenField ID="HFmuatBarangID" runat="server" />  
				        <asp:HiddenField ID="HFInvoiceDetailID" runat="server" />
				        <asp:HiddenField ID="HFTujuan" runat="server" />
				        <asp:HiddenField ID="HFIDKapal" runat="server" />
				        <asp:HiddenField ID="HFNamakapal" runat="server" />
				        <asp:HiddenField ID="HFNoInvoice" runat="server" />
				        <asp:HiddenField ID="HFPembayaran" runat="server" />
				        <asp:HiddenField ID="hfPaid" runat="server" />
				        <asp:HiddenField ID="HFTanggal" runat="server" />
				        <asp:HiddenField ID="HFCodeCust" runat="server" />
				        <asp:HiddenField ID="HFNmCust" runat="server" />
				<div align = "left" style="width:100%">
				     
				</div>
				<br />
				<table>
				<tr>
				<td>
                        Tanggal Invoice</td>
                    <td style = "width: 240px">
                        <dxe:ASPxDateEdit ID="DtInvoice" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="160px">
                        </dxe:ASPxDateEdit>
                    </td>
                    </tr>
                    <tr>
                <td>
                        No Invoice </td>
                    <td> <asp:TextBox ID="TxtNoInvoice" runat="server" Width="200px"></asp:TextBox>
                    <td>
                    penulisan : noinvoice/jenis perusahaan/Singkatan Kapal/Bulan(angka romawi)/tahun</td>
                    </td>
                        
                    
               </tr>
               <tr>
                    <td>
                        Harga Invoice
                    </td>
                    <td>
                        <asp:TextBox ID="TxtHargaInvoice" runat="server" Width="200px"></asp:TextBox>
                    </td>
               </tr>
               <tr>
				            
				            
				            <td>
				                Nama Customer
				            </td>
				            <td>
				                <asp:TextBox ID="TxtNmLain" runat="server" Width="200px" ReadOnly="true"></asp:TextBox><img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
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
				                Kota :
				            </td>
				            <td>
				                <asp:TextBox ID="TxtKotaLain" runat="server" 
                                    Width="200px" ></asp:TextBox>
				            </td>
				        </tr>
               <TR>
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
				
				 
       
                        <br />

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
									
									<dxwgv:GridViewDataTextColumn Name="Edit" Caption="#" VisibleIndex="14" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataTextColumn>
								    <dxwgv:GridViewDataTextColumn Name="Delete" Caption="#" VisibleIndex="15" Width="1%">
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
								                    <dxwgv:ASPxGridView ID="Grid_Asuransi" runat="server" 
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
