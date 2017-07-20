<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GeneralLedger.aspx.vb" Inherits="LMS.GeneralLedger" %>

<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ledger</title>
  <link href="../css/style.css" type="text/css" rel="stylesheet" media="screen" />
  <script type="text/javascript">

      var flag;
      function OnGetRowValues(values) {
          if (flag == 1) {
              BECoaFrom.SetValue(values[1]);
              document.getElementById("TxtNamaAkunFrom").value = values[2];
              document.getElementById("hfCoaFrom").value = values[1];

          }

          if (flag == 2) {
              BECoaTo.SetValue(values[1]);
              document.getElementById("TxtNamaAkunTo").value = values[2];
              document.getElementById("hfCoaTo").value = values[1];
          }



          Account_PopUp.Hide();
      }

      function viewReports() {
          var pilihan = document.getElementById('DDLPilih');
          var opt = pilihan.value;

          var datefrom = tbStartDate.GetDate();
  	        var dateto = tbEndDate.GetDate();
  	        var firstdayofyear = '01/01/' + datefrom.getFullYear();
  	        var coastart = document.getElementById('hfCoaFrom');
  	        var coaend = document.getElementById('hfCoaTo');

		var strs;

		var strdatefrom = (datefrom.getMonth()+1) + '/' + datefrom.getDate() + '/' + datefrom.getFullYear();
		var strdateto = (dateto.getMonth()+1) + '/' + dateto.getDate() + '/' + dateto.getFullYear();
  	var str;
  	
  		str = "&transdatefrom=" + strdatefrom + "&transdateto=" + strdateto;
  		str = str + "&accountnostart=" + coastart.value;
  		str = str + "&accountnoend=" + coaend.value;
  		str = str + "&firstdayofyear=" + firstdayofyear;
  		strs = "[Trans Date: " + strdatefrom + "-" + strdateto + "]" +
  						"[" + coastart.value + "]" +
  						"[" + coaend.value + "]";
  		
  		str = str + "&str=" + strs;
  		
  		var windowprops = 'toolbar=0,location=0,directories=0,status=0, ' +
												'menubar=0,scrollbars=1,resizable=1,width=500,height=200';
  		
  		window.open('ReportAcc.aspx?rpt=ledger' + str + "&tipe=" + opt, 'ledger', windowprops);
  	}
  </script>
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
   <div class="formtitle">
        <strong>Buku Besar</strong>    
    </div>
	<br />
    <div class="div_input">
        <div class="div_umum">
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="DDLPilih" runat="server">
                    <asp:ListItem Value="Semua">General Ledger</asp:ListItem>
                    <asp:ListItem Value="Pjk">General Ledger Pjk</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
            <table style = " border: 2px dotted #000000;">
			    <tr>
				    <td>COA :</td>
				    <td colspan="4">
				    <table>
				        <tr>
				            <td>
				                <dxe:ASPxButtonEdit ID="BECoaFrom" ClientInstanceName="BECoaFrom" runat="server" 
									 Width="189px">
										<Buttons>
										<dxe:EditButton>
										</dxe:EditButton>
									</Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
										e.processOnServer = false; 
										Account_PopUp.ShowAtElement(s.GetMainElement());
										flag=1;
										}" />
                            </dxe:ASPxButtonEdit>        
				            </td>
				            <td>
				                <asp:TextBox ID="TxtNamaAkunFrom" runat="server" ReadOnly ="true" Width="376px"></asp:TextBox>
				            </td>
				        </tr>
				    </table>
				    
				    </td>
			    </tr>
			    <tr>
				    <td></td>
				    <td colspan="4">To</td>
			    </tr>
			    <tr>
				    <td></td>
				    <td colspan="3">
				    <table>
				        <tr>
				            <td>
				                <dxe:ASPxButtonEdit ID="BECoaTo" ClientInstanceName="BECoaTo" runat="server" 
									 Width="189px">
										<Buttons>
										<dxe:EditButton>
										</dxe:EditButton>
									</Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
										e.processOnServer = false; 
										Account_PopUp.ShowAtElement(s.GetMainElement());
										flag=2;
										}" />
                            </dxe:ASPxButtonEdit>
				           
				            </td>
				            <td>
				                <asp:TextBox ID="TxtNamaAkunTo" runat="server" ReadOnly ="true" Width="376px"></asp:TextBox>
				            </td>
				        </tr>
				    </table>
				    </td>
			    </tr>
			    <tr>
				    <td>Date :</td>
				    <td>
				    <table>
				        <tr>
				            <td>
				                <dxe:ASPxDateEdit ID="tbStartDate" ClientInstanceName="tbStartDate" runat="server" Width="150px" EditFormat="Custom" EditFormatString="dd MMMM yyyy"
						    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
						    CssPostfix="Office2003_Olive" ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
						    <ButtonStyle Cursor="pointer" Width="13px">
						    </ButtonStyle>
					    </dxe:ASPxDateEdit>    
				            </td>
				            <td>-</td>
				            <td>
					    <dxe:ASPxDateEdit ID="tbEndDate" ClientInstanceName="tbEndDate" runat="server" Width="150px" EditFormat="Custom" EditFormatString="dd MMMM yyyy"
						    CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
						    CssPostfix="Office2003_Olive" ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
						    <ButtonStyle Cursor="pointer" Width="13px">
						    </ButtonStyle>
					    </dxe:ASPxDateEdit>
				    </td>        
				        </tr>
				    </table>
					    
				    </td>
				    
				    
				    <td width="300px">
					    <input id="Button1" type="button" value="Submit" onclick="javascript:viewReports();" />
				    </td>
			    </tr>
		    </table>
		    <asp:SqlDataSource ID="SqlDataSourceAccount" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
			 SelectCommand="SELECT ID, [Types], Code, Name, Levels FROM ChartOfAccount WHERE Parent <> 'TOP' and status = 1 and Levels <> 1 ORDER BY Code" >
		    </asp:SqlDataSource>
		    <asp:SqlDataSource ID="SqlDataSourceAccountPangkalPinang" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
			 SelectCommand="SELECT ID, [Types], Code, Name, Levels FROM ChartOfAccount WHERE Parent <> 'TOP' and  status = 1 and Levels <> 1 and LEFT(Parent, 8) = '0001.111' and Lokasi = '2' ORDER BY Code" >
		    </asp:SqlDataSource>
		    <asp:SqlDataSource ID="SqlDataSourceTanjungPandan" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
			 SelectCommand="SELECT ID, [Types], Code, Name, Levels FROM ChartOfAccount WHERE Parent <> 'TOP' and  status = 1 and Levels <> 1 and LEFT(Parent, 8) = '0001.111' and Lokasi = '3' ORDER BY Code" >
		    </asp:SqlDataSource>
		 
		<dxpc:ASPxPopupControl ID="Account_PopUp" ClientInstanceName="Account_PopUp" 
					runat="server" HeaderText="Account List" EnableClientSideAPI="True" 
					AllowResize="True">
				<Controls>
					<dxwgv:ASPxGridView ID="grid_Account" ClientInstanceName="grid_Account" 
						runat="server" AutoGenerateColumns="False"
						KeyFieldName="ID">
						<Columns>
							<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Types" VisibleIndex="1" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Code" VisibleIndex="2" >
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Name" VisibleIndex="3">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Levels" VisibleIndex="4">
							</dxwgv:GridViewDataColumn>
						</Columns>
						<ClientSideEvents RowDblClick="function(s, e) {
	grid_Account.GetRowValues(grid_Account.GetFocusedRowIndex(), 'ID;Code;Name;Types', OnGetRowValues)
}" />
						<SettingsBehavior AllowFocusedRow="True"  />
						<Settings ShowFilterRow="True" />
					</dxwgv:ASPxGridView>
				</Controls>
			</dxpc:ASPxPopupControl>
			
    <asp:HiddenField ID="hfCoaFrom" runat="server" />
    <asp:HiddenField ID="hfCoaTo" runat="server" />
			
    </div>
    </div>
    </form>
</body>
</html>
