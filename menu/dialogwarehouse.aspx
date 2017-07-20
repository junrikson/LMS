<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dialogwarehouse.aspx.vb" Inherits="LMS.dialogwarehouse" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
	<link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <title>Dialog</title>
    <script type="text/javascript">
        function OnGridFocusedRowChanged() 
        {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'Nama_Customer;Quotation_No;Nama_Barang;Satuan;Berat;Kubik;Penerima', OnGetRowValues);
        }

        function OnGetRowValues(values) 
        {
            if (values[0] == null) values[0] = '';
            if (values[1] == null) values[1] = '';
            if (values[2] == null) values[2] = '';
            if (values[3] == null) values[3] = '';
            if (values[4] == null) values[4] = '';
            if (values[5] == null) values[5] = '';
            if (values[6] == null) values[6] = '';
            document.getElementById("Nama_Customer").value = values[0];
            document.getElementById("Quotation_No").value = values[1];
            document.getElementById("Quotation_No_S").value = values[1];
            document.getElementById("Nama_Barang").value = values[2];
            document.getElementById("Satuan").value = values[3];
            document.getElementById("Berat").value = values[4];
            document.getElementById("Pengirim").value = values[5];
            document.getElementById("Penerima").value = values[6];
            
            
        }

        function passValue()
        {
            var quotation_number = document.getElementById("Quotation_No");
            var nama_barang = document.getElementById("Nama_Barang");
            var pengirim = document.getElementById("Nama_Customer");
            var satuan = document.getElementById("Satuan");
            var berat = document.getElementById("Berat");
            var quantity = document.getElementById("Pengirim");
            var penerima = document.getElementById("Penerima");
            var val = quotation_number.value + ";" + nama_barang.value + ";" + pengirim.value + ";" + satuan.value + ";" + berat.value + ";" + quantity.value + ";" + penerima.value;

            window.returnValue = val;

            if (window.returnValue != ";") {
                window.close();
            }
        }

        function cls() {
            window.close();
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxwgv:ASPxGridView ID="Grid_item" ClientInstanceName="grid" 
            runat="server" KeyFieldName="Quotation_No" 
								Width="100%" AutoGenerateColumns="False" >			
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
								<SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
								<Settings ShowFilterRow="True" />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" RowDblClick="function(s, e) { passValue(); }" />
								<Columns>
								   
									<dxwgv:GridViewDataColumn FieldName="Quotation_No" Name="Quotation_No" VisibleIndex="0" Visible = "true" Caption="No Quotation" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Nama_Customer" Name="Nama_Customer" VisibleIndex="1" Caption = "Nama Customer">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Penerima" Name="Penerima" VisibleIndex="2" >
									</dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn FieldName="Nama_Barang" Name="namabarang" 
                                        VisibleIndex="3" Caption="Nama Barang" >
									</dxwgv:GridViewDataColumn>
							        <dxwgv:GridViewDataColumn FieldName="Berat" Name="berat" VisibleIndex="4" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Kubik" Name="kubik" VisibleIndex="5" >
									</dxwgv:GridViewDataColumn>
									
									<dxwgv:GridViewDataColumn FieldName="Unit" Name="unit" VisibleIndex="8" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Others" Name="others" VisibleIndex="9" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Satuan" Name="Satuan" VisibleIndex="10" >
									</dxwgv:GridViewDataColumn>						    				
								    </Columns>
							    </dxwgv:ASPxGridView>
    </div>
    <asp:HiddenField id="Quotation_No_S" runat="server"></asp:HiddenField>    
    <input type="hidden" id="Quotation_No" />
    <input type="hidden" id="Nama_Barang" />
    <input type="hidden" id="Satuan" />
    <input type="hidden" id="Nama_Customer" />
    <input type="hidden" id="Berat" />
    <input type="hidden" id="Pengirim" />
    <input type="hidden" id="Penerima" />
    </form>
</body>
</html>
