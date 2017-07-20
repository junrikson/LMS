<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Input_Kontainer.aspx.vb" Inherits="LMS.Input_Kontainer" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Input Container</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
	<link href="../css/RoundCorner.css" type="text/css" rel="Stylesheet" />
<script language="javascript" type ="text/javascript" src="../script/main.js" ></script>
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

    function getnama() {
    returnValue = Showdialogwarehouse('Container', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var quotation_No = document.getElementById("hfquotationno");
            quotation_No.value = comp[0];

            var pengirim = document.getElementById("TxtNamaCustomer");
            pengirim.value = comp[2];
            var nm = document.getElementById("hfnama");
            nm.value = comp[2];
            
            var idcustomer = document.getElementById("HFIDcustomer");
            idcustomer.value = comp[1];

            var quodetailID = document.getElementById("HFIDQuotationDetail");
            quodetailID.value = comp[4];

            var penerima = document.getElementById("TxtPenerima");
            penerima.value = comp[6];
            
            var type = document.getElementById("hfType");
            type.value = "Container";

            var berat = document.getElementById("TxtBerat");
            berat.value = "20.000";

            var qty = document.getElementById("TxtQuantity");
            qty.value = "1";
        }
   }

    
    </script>
    <style type="text/css">
        .style3
        {
            text-align: left;
            padding-left: 20px;
            width:300px;
        }
        .style4
        {
            width: 25%;
        }
        .style5
        {
            text-align: left;
            width: 113px;
        }
        .style6
        {
            width: 5px;
        }
        </style>
</head>
<body class= "mainmenu">
    <form id="form1" runat="server">
        <div  class = "Divutama" >
            
			<div class="formtitle"><b>Input Barang Container</b></div>
			<br />
			
			
			<div  class="div_input">
			    <div class="div_umum">
			
			
				<table>
					<tr>
					    <td class = "style4">
					        <table style="width: 1000px" class = "borderdot">
					            <tr>    
		                            <td class="style5" align="center"> Type Input </td> 
                                    <td class="style6">:</td>
                                    <td class="style3">
                                    <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="True">

                                        <asp:ListItem Value="Container" >Kontainer</asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                                   
		                        </tr>
		                        <tr>    
		                            <td class="style5" align="center"> Kode Kontainer</td> 
                                    <td class="style6">:</td>
                                    <td class="style3">
                                        <asp:Label ID="lblKontainer" runat ="server" ></asp:Label></td>
                                        
                                   
		                        </tr>
		                        <tr>    
		                            <td class="style5" align="center"> Nama Konsumen</td> 
                                    <td class="style6">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtNamaCustomer" runat="server" MaxLength="100" 
                                             Enabled="True" Width="250px" ReadOnly="true"></asp:TextBox>
                                        <img alt="Browse" onclick="javascript:getnama();" src="../images/search.png" /></td>
                                  
		                        </tr>
		                        <tr>
		                            <td>
		                                Penerima
		                            </td>
		                            <td >
		                                :
		                            </td >
		                            <td class="style3">
		                                <asp:TextBox ID="TxtPenerima" runat="server" MaxLength="100" 
                                             Enabled="True" Width="250px" ReadOnly="true"></asp:TextBox>
		                            </td>
		                        </tr>
		                        <tr>
					                <td class="style5" align="center"> <asp:Label ID="lblJenis" runat ="server" Text = "No Kontainer " ></asp:Label></td>
					                <td class="style6">:</td>
			                        <td class="style3">
			                            <asp:Panel ID="PanelNoCont" runat="server">
				                    <table>
				                        <tr>
				                            <td>
				                                <asp:TextBox ID="TxtNoCont" runat="server" Width="36px" MaxLength="4"></asp:TextBox>
				                            </td>
				                            <td>
				                                <asp:TextBox ID="TxtNoCont2" runat="server" Width="61px" MaxLength="6" ></asp:TextBox>
				                            </td>
				                            <td>
				                                -
				                            </td>
				                            <td>
				                                <asp:TextBox ID="TxtNoCont3" runat="server" Width="31px" MaxLength="1" ></asp:TextBox>
				                            </td>
				                        </tr>
				                    </table>
				                </asp:Panel>
                                        <%--<asp:TextBox ID="TxtNoKontainer" runat="server" Width="250px"></asp:TextBox>--%>
					                </td> 
					               
					            </tr>
					            <tr>
					                <td class="style5" align="center"> <asp:Label ID="Label1" runat ="server" Text = "No Seal " ></asp:Label></td>
					                <td class="style6">:</td>
			                        <td class="style3">
			                            <asp:Panel ID="Panel1" runat="server">
				                    <table>
				                        <tr>
				                            <td>
				                                <asp:TextBox ID="TxtNoSeal1" runat="server" Width="36px" MaxLength="3"></asp:TextBox>
				                            </td>
				                            <td>
				                                <asp:TextBox ID="TxtNoSeal2" runat="server" Width="61px" MaxLength="6" ></asp:TextBox>
				                            </td>
				                            
				                            
				                        </tr>
				                    </table>
				                </asp:Panel>
                                        <%--<asp:TextBox ID="TxtNoKontainer" runat="server" Width="250px"></asp:TextBox>--%>
					                </td> 
					            </tr>
					            <tr>
					                <td class="style5" align="center"> <asp:Label ID="lblTotalBerat" runat="server" Text ="Total Berat" ></asp:Label></td>
					                <td class="style6">:</td>
			                        <td class="style3">
                                        <asp:TextBox ID="TxtBerat" runat="server" Width="250px"></asp:TextBox> <asp:Label ID ="lblSatuanBerat" runat="server" Text="kg"></asp:Label>
					                </td> 
					            </tr>
					            <tr>
		                            <td class="style5" align="center"> No Tally</td>
		                            <td class="style6">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtNoTally" runat="server" Width="250" MaxLength="50"></asp:TextBox>
                                        </td>
		                        </tr>
		                        <tr>
		                            <td class="style5" align="center"> No Surat Jalan</td>
		                            <td class="style6">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtNoSuratJalan" runat="server" Width="250" MaxLength="50"></asp:TextBox>
                                        </td>
		                        </tr>
		                        <tr>
		                            <td class="style5" align="center">Pilih Gudang</td>
		                            <td class="style6">:</td>
                                    <td class="style3">
                                        <asp:DropDownList 
                                            ID="DdlNamaGudang" runat="server" Width="157">
                                        </asp:DropDownList>
                                        </td>
		                        </tr>
		                        <tr>
		                            <td class="style5" align="center"> Tgl Msk Barang</td>
		                            <td class="style6">:</td>
                                    <td class="style3">
                                        <dxe:ASPxDateEdit ID="tb_tgl" runat="server" EditFormat="Custom" 
                                            EditFormatString="dd MMMM yyyy" Width="128px">
                                        </dxe:ASPxDateEdit></td>
                                      <td class="style5" align="center"> Nama Barang</td>
		                            <td class="style6">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtNamaBarang" runat="server" Width="250"></asp:TextBox>
                                        </td>
		                        </tr>
		                        <tr>
		                            <td class="style5" align="center">Nama Pengirim</td>
		                            <td class="style6">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtPengirimBarang" runat="server" Width="250"></asp:TextBox>
                                        </td>
                                        <td class="style5" align="center">Satuan</td>
		                            <td class="style6">:</td>
                                    <td class="style3">
                                       
                                        <asp:DropDownList ID="DDLSatuan" runat="server">
                                        </asp:DropDownList>
                                    </td>
		                        </tr>
		                        <tr>
		                            <td class="style5" align="center"> Keterangan</td>
		                            <td class="style6">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtKeterangan" runat="server" Width="250px"
                                            Height="113px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        			                        <td class="style5" align="center">Quantity</td>
			                        <td class="style6">:</td>
			                        <td class="style3"><asp:TextBox ID="TxtQuantity" runat="server" Width="250" MaxLength="50"></asp:TextBox></td>
		                        </tr>
		                        
					            
					  
					            
		                        <tr>
					                <td colspan = "3" align = "center">
					                  
					                    <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton ID="btAdd" runat="server" Text="Tambah" Width="90px">
                                                        <Image Url="../images/add.png" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="btreset" runat="server" Text="Reset" Width="90px">
                                                        <Image Url="../images/undo.png" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
					                </td>
					            </tr>
						
		    		           
					            				            
					        </table>
					    </td>
					    
					</tr>
					
					
					
				</table>
				<asp:HiddenField ID="hfType" runat="server" />
				<asp:HiddenField ID="HFIDDetailcontainer" runat="server" />
				<asp:HiddenField ID="HFIDcustomer" runat="server" />
				<asp:HiddenField ID="hfID" runat="server" />
				<asp:HiddenField ID="hfModeItem" runat="server" />
				<asp:HiddenField ID="hfMode" runat="server" />
				<asp:HiddenField ID="hfDel" runat="server" />
				<asp:HiddenField ID="hfquotationno" runat="server" />
				<asp:HiddenField ID="hfnamabarang" runat="server" />
				<asp:HiddenField ID="hfContainerCode" runat="server" />
				<asp:HiddenField ID="HFIDQuotationDetail" runat="server" />
				<asp:HiddenField ID="hfnama" runat="server" />
				<div align = "center" style="width:100%">
			        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                    <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
			    </div>
			
            <br />
            <div align = "left">
                <dxwgv:ASPxGridView ID="Grid_Item" runat="server" 
                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDC" 
                    Width="100%" ClientInstanceName = "grid_item" >
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
			        <SettingsPager AlwaysShowPager="True">
                    </SettingsPager>
			        <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True"  />
			        <Columns>
			        <dxwgv:GridViewDataColumn FieldName="IDC" Name="IDC" 
                                        VisibleIndex="3" Caption="IDC" Visible="false">
						</dxwgv:GridViewDataColumn>
                        
                        <dxwgv:GridViewDataColumn FieldName="ContainerHeaderIDC" Name="ContainerHeaderIDC" 
                                        VisibleIndex="3" Caption="ContainerHeaderIDC" Visible="false"  >
						</dxwgv:GridViewDataColumn>
						
				        <dxwgv:GridViewDataColumn FieldName="Nama_BarangC" Name="Nama_BarangC" VisibleIndex="5" Caption="Nama Barang" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="SatuanC" Name="SatuanC" VisibleIndex="6" Caption="Satuan" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="IDSatuanC" Name="IDSatuanC" VisibleIndex="7" Caption="IDSatuan" Visible="false" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="QtyC" Name="QtyC" VisibleIndex="8" Caption="Quantity" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="ContainerCodeC" Name="ContainerCodeC" VisibleIndex="8" Caption="Nomor Kontainer" Visible ="false" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="13" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                    ToolTip="Delete Item" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="14" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                    ToolTip="Edit Item">Edit</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                    </Columns>
                </dxwgv:ASPxGridView> 
               </div> 
            <br />
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
            
            
			<div class = "divtengah">
				<table width="100%" cellpadding="0" cellspacing="0">
					<tr valign="top">
						<td rowspan="2" width="100%" align = "left">
							<dxwgv:ASPxGridView ID="Grid_Container_Parent" ClientInstanceName="grid_parent" 
                                runat="server" KeyFieldName="ID" 
								 AutoGenerateColumns="False" Width = "100%">			
								<Styles>
									<Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#ffffff" Font-Bold="true" HorizontalAlign=Center>
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
								<SettingsPager PageSize="15">
                                </SettingsPager>
								<Settings ShowFilterRow="True" />
								<SettingsBehavior AllowFocusedRow="True"  />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
								<Columns>
									<dxwgv:GridViewDataColumn FieldName="ID" Name="ID" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataColumn>
									
									<dxwgv:GridViewDataColumn FieldName="Quotation_No" Name="Quotation_No" 
                                        VisibleIndex="2" Caption="Quotation Number" >
									</dxwgv:GridViewDataColumn>
									
									<dxwgv:GridViewDataColumn FieldName="Nama_Customer" Name="Nama_Customer" 
                                        VisibleIndex="3" Caption="Pengirim(Quotation)">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="CustomerID" Name="CustomerID" 
                                        VisibleIndex="4" Caption="CustomerID" Visible = "false">
									</dxwgv:GridViewDataColumn>
									
									<dxwgv:GridViewDataColumn FieldName="IDQuotationDetail" Name="IDQuotationDetail" 
                                        VisibleIndex="5" Caption="IDQuotationDetail" Visible = "false" >
									</dxwgv:GridViewDataColumn>
									
									<dxwgv:GridViewDataColumn FieldName="NamaContainer" Name="NamaContainer" 
                                        VisibleIndex="6" Caption="NamaContainer" >
									</dxwgv:GridViewDataColumn>
									
									<dxwgv:GridViewDataColumn FieldName="ContainerCode" Name="ContainerCode" 
                                        VisibleIndex="7" Caption="Kode Input Cont" Visible ="true">
									</dxwgv:GridViewDataColumn>
									
									<dxwgv:GridViewDataColumn FieldName="totalberat" Name="Total Berat" 
                                        VisibleIndex="8" Caption="Total Berat">
									</dxwgv:GridViewDataColumn>
									
									
									<dxwgv:GridViewDataColumn FieldName="NoKontainer" Name="NoKontainer" 
                                        VisibleIndex="9" Caption="Nomor Kontainer" Visible ="true">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="NoSeal" Name="NoSeal" 
                                        VisibleIndex="9" Caption="No Seal" Visible ="True">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Type" Name="Type" 
                                        VisibleIndex="9" Caption="Nomor Type" Visible ="false">
									</dxwgv:GridViewDataColumn>
									
									<dxwgv:GridViewDataTextColumn Caption="Penginput Data" FieldName="PenginputData" 
                                     Name="PenginputData" VisibleIndex="10"  Visible="true"></dxwgv:GridViewDataTextColumn>
									
									<dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="10" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>
								    <dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="11" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>				
								    </Columns>
								    <Templates>
								        <DetailRow>
				                            <dxwgv:ASPxGridView ID="Grid_Container_Child" runat="server" 
                                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                                                Width="569px" ClientInstanceName = "grid_child" onbeforeperformdataselect = "Grid_Container_Child_DataSelect" >
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
                                                    <dxwgv:GridViewDataColumn Caption="ID" FieldName="ID" Name="ID" 
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ContainerHeaderID" Name="ContainerHeaderID" 
                                                        visible="false" VisibleIndex="2" Caption="ContainerHeaderID" >
						                            </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="NamaBarang" Name="NamaBarang" 
                                                                    VisibleIndex="3" Caption="Nama Barang" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Qty" Name="Qty" VisibleIndex="4" Caption="Quantity" >
						                            </dxwgv:GridViewDataColumn>
				                                    <dxwgv:GridViewDataColumn FieldName="SatuanID" Name="SatuanID" VisibleIndex="5" Caption="SatuanID" Visible="false" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Nama_Satuan" Name="Nama_Satuan" VisibleIndex="6" Caption="Nama Satuan" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="ContainerCode" Name="ContainerCode" VisibleIndex="6" Caption="Nomor Kontainer"  Visible ="false">
						                            </dxwgv:GridViewDataColumn>
						                            
						                            </Columns>
                                            </dxwgv:ASPxGridView> 
								        </DetailRow>
								    </Templates>
							    </dxwgv:ASPxGridView>
                            <br />
                        </td>
						
					</tr>
					<tr>
						<td valign="bottom" align="right">
							&nbsp;</td>
					</tr>
				</table>
			</div>
        </div>
        </div>
        </div>
    </form>
</body>
</html>
