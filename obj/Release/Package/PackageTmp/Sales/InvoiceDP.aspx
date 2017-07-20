<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvoiceDP.aspx.vb" Inherits="LMS.InvoiceDP" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
	<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Input Invoice DP</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <script language="javascript" type="text/javascript" src="../script/main.js" ></script>
    <script type="text/javascript" language="javascript">

        function getnama() {

            returnValue = DialogInvoice('DP', 'Arg', '610', '450');


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
                    var CodeCust = document.getElementById("HFCodeCustomer");
                    CodeCust.value = comp[8];

                    var tujuan = document.getElementById("HFTujuan");
                    tujuan.value = comp[4];
                    var tujuan2 = document.getElementById("TxtTujuan");
                    tujuan2.value = comp[4];


                }
                else {
                    alert("You should input invoice one Muatbarang for 1 Invoice Number,Please press reset button for inputting a new MuatBarang ");
                }
            }
        }
        function getKapal() {
            returnValue = ShowDialog2('Kapal', 'Arg', '610', '450');
            if (returnValue) {
                if (document.getElementById("HFMode").value == "Insert") {
                    var comp = new Array();
                    comp = returnValue.split(";");
                    var Name = document.getElementById("TxtKapal");
                    var namakapal = document.getElementById("HFNamakapal");
                    Name.value = comp[1];
                    namakapal.value = comp[1];
                    var ID = document.getElementById("HFIDKapal");
                    ID.value = comp[0];
                }
                else {
                    alert("Anda tidak bisa mengubah nama kapal waktu revisi ");                
                }
               
            }
        }
</script>    
    
    <style type="text/css">
        .style2
        {
            height: 22px;
        }
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
                        Tanggal Invoice</td>
                    <td>:</td>
                    <td style = "width: 240px">
                        <dxe:ASPxDateEdit ID="DtInvoice" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="147px">
                        </dxe:ASPxDateEdit>
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
                                <td class ="style4">
                                    <strong>Kapal</strong></td>
                                    <td>:</td>
                                <td>
                                    <asp:TextBox ID="TxtKapal" runat = "server" ></asp:TextBox>
                                    <img alt="Browse" src="../images/search.png" onclick="javascript:getKapal();" />                                    
                                </td>                                            
                            </tr>            
                <tr>
                                <td class ="style4">
                                    <strong>DP Persen</strong></td>
                                    <td>:</td>
                                <td>
                                    <asp:TextBox ID="TxtPercen" runat = "server" ></asp:TextBox>
                                </td>                                            
                            </tr>            
                <tr>
                    <td class="style4">
                        <table>
					        <tr>
		                        <td align="right" >
			                        <dxe:ASPxButton ID="btAdd" runat="server" Text="Add" Width="90px" 
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
				        <asp:HiddenField ID="HFTanggal" runat="server" />
				        <asp:HiddenField ID="HFCodeCustomer" runat="server" />
				<div align = "left" style="width:100%">
				    Ditunjukan <asp:DropDownList ID="ddlDitunjukan" runat="server">
                        </asp:DropDownList>
				</div>
				<br />
				<table>

				<tr>
                    <td>
                        Indikator </td>
                    <td> <asp:TextBox ID="TxtIndikator" runat="server" AutoPostBack = "False" 
                            Width="20" MaxLength="1"></asp:TextBox>
                    </td>
                 </tr>
                 <tr>
                    <td> <asp:TextBox ID="TxtNoInvoice" runat="server" AutoPostBack = "false" Visible="false">
                    </asp:TextBox>
                    </td>             
                 </tr> 
                 <tr>
                    <td>
                        Nama Barang
                    </td>
                        <td colspan="3">
                            <asp:TextBox ID="TxtNamaBarang" runat="server" AutoPostBack = "false" Width="256px"></asp:TextBox>
                        </td>
                  </tr>
                  <tr>
				        <td>
                            <asp:CheckBox ID="ChkMiniByr" runat="server" Text = "Batas Minimum Pembayaran" /> 
				        </td>
				        <td>
                            <asp:TextBox ID="txtMinByr" runat="server" Width="172px"></asp:TextBox>
				        </td>
				    </tr> 
                </table> 
                <br />
                <table width="100%">
                    <tr> 
                        
		                              <td align="center" >
        	                        <dxe:ASPxButton ID="btSave" runat="server" Text="Simpan" Width="90px">
				                        <Image Url="../images/save-alt.png" />
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
									
									<dxwgv:GridViewDataTextColumn FieldName="MuatBarangID" Name="MuatBarangID" 
                                        VisibleIndex="3" Caption="MuatBarangID" Visible = "false">
									</dxwgv:GridViewDataTextColumn>									
									<dxwgv:GridViewDataTextColumn FieldName="Pengirim" Name="pengirim" 
                                        VisibleIndex="4" Caption="Pengirim" Visible = "true">
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Penerima" Name="Penerima" 
                                        VisibleIndex="5" Caption="Penerima" Visible = "true" >
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
									<dxwgv:GridViewDataTextColumn FieldName="NamaKapal" Name="NamaKapal" 
                                        VisibleIndex="7" Caption="NamaKapal" >
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn FieldName="Total" Name="Total" 
                                        VisibleIndex="12" Caption="Total Harga">
                                       <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
									</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="Keterangan" 
                                        Name="Keterangan"  Visible="true" VisibleIndex="13">
                                    </dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataDateColumn FieldName="TglInvoice" Name="TglInvoice" 
                                        VisibleIndex="14" Caption="Tanggal Invoice">
									<PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
									</dxwgv:GridViewDataDateColumn>
									<dxwgv:GridViewDataTextColumn Caption="Yang Input" Name="YgInput" VisibleIndex="14" 
									    FieldName="YgInput">
								    </dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn Name="Edit" Caption="#" VisibleIndex="14" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataTextColumn>
								    <dxwgv:GridViewDataTextColumn Name="Delete" Caption="#" VisibleIndex="15" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" Visible="false" >Delete</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataTextColumn>					    
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
