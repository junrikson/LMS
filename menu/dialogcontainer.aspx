<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="dialogcontainer.aspx.vb" Inherits="LMS.dialogcontainer" %>

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
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'ID;ContainerCode;QuotationNo', OnGetRowValues);
        }

        function OnGetRowValues(values) 
        {
            if (values[0] == null) values[0] = '';
            if (values[1] == null) values[1] = '';
            if (values[2] == null) values[2] = '';

            document.getElementById("IDContainer").value = values[0];
            document.getElementById("CodeContainer").value = values[1];
            document.getElementById("QDID").value = values[2];
        }

        function passValue()
        {
            var container_id = document.getElementById("IDContainer");
            var container_code = document.getElementById("CodeContainer");
            var QDID = document.getElementById("QDID");

            var val = container_id.value + ";" + container_code.value + ";" + QDID.value;

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
        <dxwgv:ASPxGridView ID="Grid_Dialog" ClientInstanceName="grid" 
            runat="server" KeyFieldName="ID" 
								Width="100%" AutoGenerateColumns="False" SettingsDetail-AllowOnlyOneMasterRowExpanded="True" SettingsDetail-ShowDetailRow="True">			
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
                        <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

                                <SettingsPager NumericButtonCount="20">
                                </SettingsPager>
								<Settings ShowFilterRow="True" />
								<SettingsBehavior AllowFocusedRow="True"  />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" RowDblClick="function(s, e) { passValue(); }" />
								<Columns>
								   
									<dxwgv:GridViewDataColumn FieldName="ID" Name="ID" VisibleIndex="0" Visible = "false">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="ContainerCode" Name="ContainerCode" VisibleIndex="1" Caption="No Container">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="QuotationNo" Name="QuotationNo" VisibleIndex="2" >
									</dxwgv:GridViewDataColumn>
							        <dxwgv:GridViewDataColumn FieldName="Nama_Customer" Name="Nama_Customer" VisibleIndex="3" >
									</dxwgv:GridViewDataColumn>						    				
								    </Columns>
								     <Templates>
								        <DetailRow>
				                            <dxwgv:ASPxGridView ID="Grid_Dialog_Child" runat="server" 
                                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                                                Width="569px" ClientInstanceName = "grid_child" onbeforeperformdataselect = "Grid_Dialog_Child_DataSelect" >
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
                                                <SettingsBehavior AllowFocusedRow="True"  />
			                                    <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="ID" FieldName="ID" Name="ID" 
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="ContainerHeaderID" Name="ContainerHeaderID" 
                                                        visible="false" VisibleIndex="2" Caption="ContainerHeaderID" >
						                            </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="NamaBarang" Name="NamaBarang" 
                                                        Visible="true" VisibleIndex= "3" Caption="NamaBarang" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Qty" Name="Qty" 
                                                        Visible="false" VisibleIndex= "4" Caption="Quantity" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Nama_Satuan" Name="Nama_Satuan" 
                                                        VisibleIndex="5" Caption="Satuan" >
						                            </dxwgv:GridViewDataColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView> 
								        </DetailRow>
								    </Templates>
							    </dxwgv:ASPxGridView>
    </div>
    <asp:HiddenField id="Quotation_No_S" runat="server"></asp:HiddenField>    
    <input type="hidden" id="QDID" />
    <input type="hidden" id="IDContainer" />
    <input type="hidden" id="CodeContainer" />
    <input type="hidden" id="Satuan" />
    <input type="hidden" id="Pengirim" />
    </form>
</body>
</html>
