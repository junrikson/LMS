<%@ Page Language="VB" Strict="true" AutoEventWireup="false" Inherits="LMS.dialog7" Codebehind="dialog7.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link rel="stylesheet" type="text/css" href="../css/style.css" />
	<link rel="stylesheet" type="text/css" href="../css/main.css"  />
	<title>Dialog</title>
	<script type="text/javascript">

function OnGridFocusedRowChanged() {
	     
		grid.GetRowValues(grid.GetFocusedRowIndex(), 'CODE;NAME;WAREHOUSE;TYPE;PRICE;DP;MEMO;UNIT', OnGetRowValues);
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
	document.getElementById("CODE").value = values[0];
	document.getElementById("NAME").value = values[1];
	document.getElementById("WAREHOUSE").value = values[2];
	document.getElementById("TYPE").value = values[3];
	document.getElementById("PRICE").value = values[4];
	document.getElementById("DP").value = values[5];
	document.getElementById("MEMO").value = values[6];
	document.getElementById("UNIT").value = values[7];
}


function passValue() {
	var item_code = document.getElementById("CODE");
	var item_name = document.getElementById("NAME");
	var warehouse = document.getElementById("WAREHOUSE");
	var type = document.getElementById("TYPE");
	var price = document.getElementById("PRICE");
	var dp = document.getElementById("DP");
	var memo = document.getElementById("MEMO");
	var unit = document.getElementById("UNIT");
	var val = item_code.value + ";" + item_name.value + ";" + warehouse.value + ";" + type.value + ";" + price.value + ";" + dp.value + ";" + memo.value + ";" + unit.value; 
//	if(val=="" || val==";") {
////		passValue();
//		//alert('too fast');
//		OnGridFocusedRowChanged();
//		passValue();
//	}else {
	window.returnValue = val;
//	alert(window.returnValue);
	if (window.returnValue != ";;;;;;;") {
		//alert(window.returnValue);
		window.close();
	}
//setTimeout('cls();',5000);
//		if(window.returnValue=="" || window.returnValue==";") {
//			alert('error in return a value');	
//			window.close();
//		}else {
//			window.close();
//		}
//	}
}

function cls() {
	window.close();
}


	</script>
</head>
<body>
  <form id="form1" runat="server">
  <div>
		<dxwgv:ASPxGridView ID="grid_Dialog" ClientInstanceName="grid" runat="server" Width="100%" Font-Names="Trebuchet MS" AutoGenerateColumns="False" KeyFieldName="CODE">
			<Styles>
				<Header BackColor="#2c3848" ForeColor="#FFFFFF" HorizontalAlign="Center" Paddings-Padding="2px"></Header>
				<Cell HorizontalAlign="Left" Font-Size="11px" VerticalAlign="Top" Paddings-Padding="2px"></Cell>
				<FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
			</Styles>
			<Settings ShowFilterRow="true" />
			<%--<Settings ShowFilterRow="true" ShowVerticalScrollBar="true" VerticalScrollableHeight="320" />--%>
			<SettingsBehavior AllowFocusedRow="true" />
			<SettingsPager AlwaysShowPager="true" ShowSeparators="true" NumericButtonCount="10"></SettingsPager>
			<%--<SettingsPager Mode="ShowAllRecords"></SettingsPager>--%>
			<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" RowDblClick="function(s, e) { passValue(); }" />
			<Columns>
				<dxwgv:GridViewDataColumn FieldName="CODE" Caption="Code" VisibleIndex="0" Visible = "false"></dxwgv:GridViewDataColumn>				
				<dxwgv:GridViewDataColumn FieldName="NAME" Caption="Name" VisibleIndex="1"></dxwgv:GridViewDataColumn>
				<dxwgv:GridViewDataColumn FieldName="WAREHOUSE" Caption="Warehouse" VisibleIndex="2"></dxwgv:GridViewDataColumn>
				<dxwgv:GridViewDataColumn FieldName="TYPE" Caption="Type" VisibleIndex="3"></dxwgv:GridViewDataColumn>
				<dxwgv:GridViewDataColumn FieldName="PRICE" Caption="Price" VisibleIndex="4"></dxwgv:GridViewDataColumn>
				<dxwgv:GridViewDataColumn FieldName="DP" Caption="DP" VisibleIndex="5"></dxwgv:GridViewDataColumn>
				<dxwgv:GridViewDataColumn FieldName="MEMO" Caption="Memo" VisibleIndex="6"></dxwgv:GridViewDataColumn>
				<dxwgv:GridViewDataColumn FieldName="UNIT" Caption="UNIT" VisibleIndex="7"></dxwgv:GridViewDataColumn>				
			</Columns>
		</dxwgv:ASPxGridView>
		
  </div>
  <div>
  <table>
  <tr>
  <td id="okitem" runat="server"><img src="../images/ok.png" onclick="javascript:passValue();" 
          style="width: 16px" /></td>
       
  <td><img src="~/images/undo.png" onclick="javascript:window.close();" id="img1" runat="server" /></td>
  </tr>
  </table>
  </div>
  <input type="hidden" id="CODE" />
  <input type="hidden" id="NAME" />
  <input type="hidden" id="WAREHOUSE" />
  <input type="hidden" id="TYPE" />
  <input type="hidden" id="PRICE" />
  <input type="hidden" id="DP" />
  <input type="hidden" id="MEMO" />
  <input type="hidden" id="UNIT" />
  	<asp:Label ID="lError" runat="server"></asp:Label>
  </form>
</body>
</html>
