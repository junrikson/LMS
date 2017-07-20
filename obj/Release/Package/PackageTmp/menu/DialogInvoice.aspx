<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DialogInvoice.aspx.vb" Inherits="LMS.DialogInvoice" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <link rel="stylesheet" type="text/css" href="../css/style.css" />
	<link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <title>Dialog</title>
    <script type="text/javascript">
        function OnGridFocusedRowChanged() {

            grid.GetRowValues(grid.GetFocusedRowIndex(), 'ID;MB_No;Nama_Customer;Penerima;Tujuan;IDkapal;NamaKapal;Paid;Kode_Customer', OnGetRowValues);
            
        }

        function OnGetRowValues(values) {
            
            if (values[0] == null) values[0] = '';
            if (values[1] == null) values[1] = '';
            if (values[2] == null) values[2] = '';
            if (values[3] == null) values[3] = '';
            if (values[4] == null) values[4] = '';
            if (values[5] == null) values[5] = '';
            if (values[6] == null) values[6] = '';
            if (values[7] == null) values[7] = '';
            if (values[8] == null) values[8] = '';

            document.getElementById("IDMuatBarangHeader").value = values[0];
            document.getElementById("MB_No").value = values[1];
            document.getElementById("Nama_Customer").value = values[2];
            document.getElementById("Penerima").value = values[3];
            document.getElementById("Tujuan").value = values[4];
            document.getElementById("IDkapal").value = values[5];
            document.getElementById("NamaKapal").value = values[6];
            document.getElementById("Paid").value = values[7];  
            document.getElementById("Kode_Customer").value = values[8];        
        }

        function passValue() {
            var idmuatbarangheader = document.getElementById("IDMuatBarangHeader");
            var mb_no = document.getElementById("MB_No");
            var nama_customer = document.getElementById("Nama_Customer");
            var penerima = document.getElementById("Penerima");
            var tujuan = document.getElementById("Tujuan");
            var idkapal = document.getElementById("IDkapal");
            var nama_kapal = document.getElementById("NamaKapal");
//            var pembayar = document.getElementById("Pembayar");
            var paid = document.getElementById("Paid");
            var Kode_Customer = document.getElementById("Kode_Customer");

            var val = idmuatbarangheader.value + ";" + mb_no.value + ";" + nama_customer.value + ";" + penerima.value + ";" + tujuan.value + ";" + idkapal.value + ";" + nama_kapal.value + ";" + paid.value + ";" + Kode_Customer.value;

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
            runat="server" KeyFieldName="ID" 
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
								<SettingsPager PageSize="15">
                                </SettingsPager>
								<Settings ShowFilterRow="True" />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" RowDblClick="function(s, e) { passValue(); }" />
								<Columns>
							        <dxwgv:GridViewDataColumn FieldName="ID" Name="ID" VisibleIndex="0" Visible = "false" >
								    </dxwgv:GridViewDataColumn >
									<dxwgv:GridViewDataColumn FieldName="MB_No" Name="MB_No" VisibleIndex="1" Visible = "false" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Nama_Customer" Name="Nama_Customer" VisibleIndex="2" Caption = "Pengirim">
									</dxwgv:GridViewDataColumn>
							        <dxwgv:GridViewDataColumn FieldName="Penerima" Name="Penerima" VisibleIndex="3" Caption="Penerima">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Tujuan" Name="Tujuan" VisibleIndex="4" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="IDkapal" Name="IDkapal" VisibleIndex="5" Visible = "false" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="NamaKapal" Name="NamaKapal" VisibleIndex="6" >
									</dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn FieldName="Kode_Customer" Name="Kode_Customer" VisibleIndex="7" Caption = "Kode Customer" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Paid" Name="Paid" VisibleIndex="7" Caption = "Paid(%)" >
									</dxwgv:GridViewDataColumn>	
									<dxwgv:GridViewDataDateColumn Caption="Tanggal Keberangkatan" FieldName="Tanggal" 
                                    Name="Tanggal"  Visible="true" VisibleIndex="8" >
                                    <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                                    </dxwgv:GridViewDataDateColumn>	
									
														    				
								    </Columns>
							    </dxwgv:ASPxGridView>
    </div>
    <asp:HiddenField id="IDMuatBarangHeader" runat="server"></asp:HiddenField> 
    <input type="hidden" id="MB_No" />   
    <input type="hidden" id="Nama_Customer" />
    <input type="hidden" id="Penerima" />
    <input type="hidden" id="Tujuan" />
    <input type="hidden" id="IDkapal" />
    <input type="hidden" id="NamaKapal" />
    <input type="hidden" id="Tanggal" />
    <input type="hidden" id="Paid" />
    <input type="hidden" id="Kode_Customer" />
    </form>
</body>
</html>
