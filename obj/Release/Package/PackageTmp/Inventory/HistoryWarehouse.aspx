<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HistoryWarehouse.aspx.vb" Inherits="LMS.HistoryWarehouse" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>History Gudang</title>
    
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama">
        <div class="formtitle">History Gudang</div>
        <br />
        <div class = "div_input">
            <div class="div_umum">
                <table>
                <tr>
                    <td>
                        Dari Tanggal
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="tbFrom" runat="server" EditFormat="Custom" EditFormatString="dd MMMM yyyy">
                            
                            </dxe:ASPxDateEdit> 
                    </td>
                    <td>
                        -
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="tbEnd" runat="server" EditFormat="Custom" EditFormatString="dd MMMM yyyy">
                            </dxe:ASPxDateEdit> 
                    </td>
                </tr>
                    <tr>
                        <td>
                            Pilih Gudang
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLGudang" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="RdSisa" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" >Terdapat sisa</asp:ListItem>
                                <asp:ListItem Value="1" >tidak ada sisa</asp:ListItem>
                            </asp:RadioButtonList>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="BtnOk" runat="server" Text="OK">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="BtnReset" runat="server" Text="Reset">
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                
                <div align = "center" style="width:100%">
			        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                    <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
			    </div>
                
                <br />
                <dxwgv:ASPxGridView ID="Grid_Warehouse_Parent" ClientInstanceName="grid_parent" 
                                runat="server" KeyFieldName="ID" 
								 AutoGenerateColumns="False" Width="100%">			
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
								<SettingsPager PageSize="15">
                                </SettingsPager>
								<Settings ShowFilterRow="True" />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
								<Columns>
									<dxwgv:GridViewDataColumn FieldName="ID" Name="ID" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn Name="Status" Caption="Status" VisibleIndex="1" Width="1%" Visible = "false">
									    <DataItemTemplate>
									        <asp:Image ID = "ImageGrid" runat = "server" />
									    </DataItemTemplate>
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="WarehouseItem_Code" Name="WarehouseItem_Code" 
                                        VisibleIndex="2" Caption="Kode Data Gudang" Visible ="True">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Penerima" Name="Penerima" VisibleIndex="3" >
									</dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn FieldName="Quotation_No" Name="Quotation_No" 
                                        VisibleIndex="4" Caption="Quotation Number" visible = "false">
									</dxwgv:GridViewDataColumn>
                                    
                                    
									<dxwgv:GridViewDataColumn FieldName="Warehouse_Name" Name="Warehouse_Name" 
                                        VisibleIndex="8" Caption="Tempat" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Warehouse_Code" Name="Warehouse_Code" VisibleIndex="9" Visible="false" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="NamaSupplier" Name="NamaSupplier" VisibleIndex="9" Visible="false" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataTextColumn Caption="Penginput Data" FieldName="PenginputData" 
                                        Name="PenginputData"  Visible="true" VisibleIndex="10"></dxwgv:GridViewDataTextColumn>
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
				                            <dxwgv:ASPxGridView ID="Grid_Warehouse_Child" runat="server" 
                                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                                                Width="100%" ClientInstanceName = "grid_child" onbeforeperformdataselect = "Grid_Warehouse_Child_DataSelect" >
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
                                                    <dxwgv:GridViewDataColumn FieldName="Warehouseheader_ID" Name="Warehouseheader_ID" 
                                                        visible="false" VisibleIndex="2" Caption="Warehouseheader_ID" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="QDID" Name="QDID" 
                                                        Visible="false" VisibleIndex= "3" Caption="QDID" >
						                            </dxwgv:GridViewDataColumn>
						                             <dxwgv:GridViewDataColumn FieldName="Container" Name="Container" 
                                                        Visible="true" VisibleIndex= "4" Caption="Container" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="IDContainer" Name="IDContainer" 
                                                        Visible="false" VisibleIndex= "5" Caption="Container" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Nama_Barang" Name="Nama_Barang" 
                                                        VisibleIndex="6" Caption="Nama Barang" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="TallySheet_No" Name="TallySheet_No" 
                                                        VisibleIndex="7" Caption="No TallySheet" visible = "true">
									                </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="No_Surat_Jalan" Name="No_Surat_Jalan" 
                                                        VisibleIndex="8" Caption="No Surat Jalan" visible = "true">
									                </dxwgv:GridViewDataColumn>
									                <dxwgv:GridViewDataDateColumn FieldName="Time_Itemcome" Name="Time_Itemcome" 
                                                        VisibleIndex="9" Caption="Tgl Datang" >
                                                        <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesDateEdit>
									                </dxwgv:GridViewDataDateColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Merk" Name="Merk" VisibleIndex="10" Caption="Merk" >
						                            </dxwgv:GridViewDataColumn>
				                                    <dxwgv:GridViewDataColumn FieldName="Berat" Name="Berat" VisibleIndex="11" Caption="Berat" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Panjang" Name="Panjang" VisibleIndex="12" Caption="Panjang" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Lebar" Name="Lebar" VisibleIndex="13" Caption="Lebar" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Tinggi" Name="Tinggi" VisibleIndex="14" Caption="Tinggi" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Quantity" Name="Quantity" VisibleIndex="15" Caption="Quantity " >
						                            <PropertiesTextEdit DisplayFormatString="{0:###.###.###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="QuantityMsk" Name="QuantityMsk" VisibleIndex="16" Caption="Qty Muat" >
						                            <PropertiesTextEdit DisplayFormatString="{0:###.###.###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataColumn FieldName="OtherIDs" Name="OtherIDs" VisibleIndex="17" Caption= " OtherIDs " Visible="false" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Others" Name="Others" VisibleIndex="18" Caption= " Satuan " >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Keterangan" Name="Keterangan" VisibleIndex="19" Caption= " Keterangan ">
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="NamaSupplier" Name="NamaSupplier" VisibleIndex="20" Caption= " Pengirim ">
						                            </dxwgv:GridViewDataColumn>
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
