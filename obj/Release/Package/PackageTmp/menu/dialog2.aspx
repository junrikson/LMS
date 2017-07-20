<%@ Page Language="VB" Strict="true" AutoEventWireup="false" Inherits="LMS.dialog2" Codebehind="dialog2.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link rel="stylesheet" type="text/css" href="../css/style.css" />
	<link rel="stylesheet" type="text/css" href="../css/main.css"  />
	<title>Dialog</title>
	<script type="text/javascript">

function OnGridFocusedRowChanged() {
	      
		grid.GetRowValues(grid.GetFocusedRowIndex(), 'CODE;NAME;QTY', OnGetRowValues);
}

function OnGetRowValues(values) {
	if (values[0] == null) values[0] = '';
	if (values[1] == null) values[1] = '';
	if (values[2] == null) values[2] = '';	
	document.getElementById("packet_code").value = values[0];
	document.getElementById("packet_name").value = values[1];
	document.getElementById("jenis").value = values[2];
}


function passValue() {
	var packet_code = document.getElementById("packet_code");
	var packet_name = document.getElementById("packet_name");
	var jenis = document.getElementById("jenis");
	var val = packet_code.value + ";" + packet_name.value + ";" + jenis.value;
//	if(val=="" || val==";") {
////		passValue();
//		//alert('too fast');
//		OnGridFocusedRowChanged();
//		passValue();
//	}else {
	window.returnValue = val;
//	alert(window.returnValue);
	if (window.returnValue != ";") {
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
				
				<Header BackColor="#2c3848" ForeColor="#ffffff" HorizontalAlign="Center" hoverstyle-border-bordercolor="#515763" Paddings-Padding="2px"></Header>
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
				<dxwgv:GridViewDataColumn FieldName="QTY" Caption="Quantity" VisibleIndex="2"></dxwgv:GridViewDataColumn>
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
  <input type="hidden" id="packet_code" />
  <input type="hidden" id="packet_name" />
  <input type="hidden" id="jenis" />
  	<asp:Label ID="lError" runat="server"></asp:Label>
  </form>
</body>
</html>
