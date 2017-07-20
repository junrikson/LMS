<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BAST.aspx.vb" Inherits="LMS.BAST" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <style type="text/css" media="screen">
			.NoPrint { FONT-SIZE: 9pt; FONT-FAMILY: Arial }
		 .style1
         {
             width: 87px;
         }
		</style>
		<style type="text/css" media="print">
			.NoPrint, .btn, .kembali { DISPLAY: none }
		</style>		
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <link rel="stylesheet" type="text/css" href="../css/print.css"  />
    <script language="javascript" type ="text/javascript" src="../script/main.js" ></script>
     <script type="text/javascript" language="javascript">
   function getmb() {
        returnValue = ShowDialog2('Muat', document.getElementById("hfIDK").value,'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var ID = document.getElementById("hfMID");
            ID.value = comp[0]
            var Name = document.getElementById("TxtCustName");
            Name.value = comp[1];
            var per = document.getElementById("TxtPenerima");
            per.value = comp[2];
        }
    }
    function getTujuan() {
        returnValue = ShowDialog2('Tujuan', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var Name = document.getElementById("Ke");
            Name.value = comp[1];
        }
    }
    function getFrom() {
        returnValue = ShowDialog2('Tujuan', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var Name = document.getElementById("Dari");
            Name.value = comp[1];
        }
    }
    function getKapal() {
        returnValue = ShowDialog2('Kapal', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");            
            var ID = document.getElementById("hfIDK");
            ID.value = comp[0];
            var Name = document.getElementById("TxtKapal");
            Name.value = comp[1];
            var hfName = document.getElementById("hfnamakapal");
            hfName.value = comp[1];

        }
    }
    function getKapalhis() {
        returnValue = ShowDialog2('Kapal', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var ID = document.getElementById("hfIDKHIS");
            ID.value = comp[0];
            var Name = document.getElementById("TxtKapalhis");
            Name.value = comp[1];
            var hfName = document.getElementById("hfnamakapal");
            hfName.value = comp[1];
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div class = "Divutama">
        <div class = "div_input">
            <asp:Panel ID = "Panel_Input" runat = "server">
                <table>
                      <tr>
                        <td class="style1">
                            Kapal
                        </td>
                        <td>:</td>
                        <td style =" width:150px;">
                            <asp:TextBox ID = "TxtKapal" runat = "server" Enabled="False" ></asp:TextBox>
                            <img alt="Browse" onclick="javascript:getKapal();" src="../images/search.png" /></td>
                    </tr>
                    <tr>
                        <td  >
                            Tanggal Berangkat</td>
                        <td>:</td>
                        <td   >
                            <dxe:ASPxDateEdit ID="BASTDate" runat="server" EditFormat="Custom" EditFormatString="dd MMMM yyyy" >
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Header
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlHeader" runat = "server" ></asp:DropDownList>
                        </td>
                    </tr>
              
                          <tr>
                        <td style = " width : 250px ;  "  >
                            Kota Tujuan
                        </td>
                        <td >:</td>
                        <td>
                            <asp:TextBox ID="TxtKotaTujuan" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  >
                            Tanggal Bongkar</td>
                        <td>:</td>
                        <td   >
                            <dxe:ASPxDateEdit ID="DtBongkar" runat="server" EditFormat="Custom" EditFormatString="dd MMMM yyyy" >
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td  >
                            Yang Menyerahkan</td>
                        <td>:</td>
                        <td   >
                            <asp:TextBox ID="TxtMenyerahkan" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  >
                            Kota Bongkar</td>
                        <td>:</td>
                        <td   >
                            <asp:TextBox ID="TxtKotaBongkar" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="left">
                                        <dxe:ASPxButton ID="btAdd" runat="server" Text="Add">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btReset" runat="server" Text="Reset">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btViewHistory" runat="server" Text="View History">
                                        </dxe:ASPxButton>
                                    </td>
                             
                                </tr>
                            </table>
                        </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID= "lblError" runat ="server" CssClass = "error"></asp:Label>                    
                    </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div>
            <asp:Panel ID ="historyinput" runat ="server" >
            <div class="formtitle">
                    <b>Bast History </b>  
                </div>
                <br />
                <div class ="div_input">
                
                <table>
                    <tr>
                        <td>
                            From (Tanggal Bongkar)
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="tbFrom" runat="server" EditFormat="Custom" EditFormatString="dd MMMM yyyy">
                            </dxe:ASPxDateEdit>                
                        </td>
                        <td>
                            To &nbsp;
                        </td>
                        <td>
                            <dxe:ASPxDateEdit ID="tbSampai" runat="server" EditFormat="Custom" EditFormatString="dd MMMM yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Kapal
                        </td>
                        <td>:
                            <asp:TextBox ID = "TxtKapalhis" runat = "server" Enabled="False" ></asp:TextBox>
                            <img alt="Browse" onclick="javascript:getKapalhis();" src="../images/search.png" /></td>
                    </tr>
                     <tr>
                        <td class="style1">Header</td>
                        <td>:
                            <asp:DropDownList ID="ddlHeaderHist" runat="server">
                             </asp:DropDownList>
                        </td>
                    </tr>
                          <tr>
                        <td style = " width : 250px ;  "  >
                            Kota Tujuan
                        </td>
                        <td >:
                            <asp:TextBox ID="TxtKotaTujuanHis" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  >
                            Yang Menyerahkan</td>
                        <td   >: 
                            <asp:TextBox ID="TxtMenyerahkanHist" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  >
                            Kota Bongkar</td>
                            
                        <td   >:
                            <asp:TextBox ID="txtKotaBongkarHist" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan = "3" align = "center">
                            <asp:Label ID= "lblerror2" runat ="server" CssClass = "error"
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        <table>
                        <tr>
                        <td>
                            <dxe:ASPxButton ID="ViewGrid" runat="server" Text="ViewGrid">
                            </dxe:ASPxButton>
                        </td>
                           <td>
                            <dxe:ASPxButton ID="Back" runat="server" Text="Back">
                            </dxe:ASPxButton>
                        </td>
                        </tr>  
                        </table> 
                        </td>
                    </tr>
                </table>
                </div>
            </asp:Panel>
        </div>
        <div>
            <asp:Panel ID ="historygrid" runat ="server" >
            <dxwgv:ASPxGridView ID="Grid_History" runat="server" 
                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                    Width="80%" ClientInstanceName = "grid_manifest" >
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
                    <SettingsPager Mode="ShowPager" PageSize ="20"></SettingsPager>
			        <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
			        <Columns>
                        <dxwgv:GridViewDataColumn Caption="ID" FieldName="ID" Name="ID" 
                            Visible="false" VisibleIndex="1">
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pengirim" FieldName="Nama_Customer" Name="Nama_Customer" 
                             VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Penerima" FieldName="Penerima" Name="Penerima" 
                             VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>     
                        <dxwgv:GridViewDataColumn FieldName="BastNo" Name="BastNo" 
                            Visible="true" VisibleIndex= "4" Caption="Nomor BAST" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="Tanggal" Name="Tanggal" 
                            Visible="true" VisibleIndex= "5" Caption="Tanggal BAST" >
						    <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
						</dxwgv:GridViewDataDateColumn>
						<dxwgv:GridViewDataColumn FieldName="Kapal" Name="Kapal" 
                            Visible="false" VisibleIndex= "6" Caption="Kapal" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="Nama_Kapal" Name="Nama_Kapal" 
                            VisibleIndex= "7" Caption="Nama Kapal" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="MB_ID" Name="MB_ID" 
                            VisibleIndex= "8" Caption="MB NO" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Print" VisibleIndex="9" Width="1%">
                                            <dataitemtemplate>
                                                <asp:LinkButton ID="tbPrint" runat="server" CommandName="Print" 
                                                    ToolTip="Print">Print</asp:LinkButton>
                                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                    </Columns>
                </dxwgv:ASPxGridView>     
               
            </asp:Panel>
        </div>
        <div>
            <asp:Panel ID ="Panel_Grid" runat ="server" >
                               <dxwgv:ASPxGridView ID="Grid_Kapal_Parent" runat="server" 
                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="Mb_No" 
                    Width="80%" ClientInstanceName = "grid_kapal_parent">
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
                    <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
                    <SettingsPager Mode="ShowPager" PageSize ="20"></SettingsPager>
                    <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                            Visible="false" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Warehouse header ID" FieldName="WarehouseHeaderID" Name="WarehouseHeaderID" 
                            Visible="false" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Customer Id" FieldName="Customer_Id" Name="Customer_Id" 
                            Visible="false" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pengirim" FieldName="Nama_Customer" Name="Nama_Customer" 
                             VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Penerima" FieldName="Penerima" Name="Penerima" 
                             VisibleIndex="4">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Muat Barang Number" FieldName="Mb_No" 
                            Name="Mb_No"  Visible="true" VisibleIndex="5" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Tanggal Berangkat" FieldName="Tanggal" 
                            Name="Tanggal"  Visible="true" VisibleIndex="6" Width="140px">
                            <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        
                        <dxwgv:GridViewDataTextColumn Caption="KapalID" FieldName="Kapal_ID" 
                            Name="Kapal_ID"  Visible="false" VisibleIndex="7" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Kapal" FieldName="Kapal" 
                            Name="Kapal"  Visible="true" VisibleIndex="8" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        
                        
                        <dxwgv:GridViewDataColumn Caption="#" Name="PrintBL" VisibleIndex="11" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbPrintBAST" runat="server" CommandName="PrintBAST" 
                                    ToolTip="Print BAST ">PrintBAST</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="Grid_Kapal_Child" runat="server" 
                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="Mb_No" 
                                Width="569px" ClientInstanceName = "grid_kapal_child" onbeforeperformdataselect="Grid_Kapal_Child_DataSelect">
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
                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                                        Visible="false" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Muat Barang Number" FieldName="Mb_No" 
                                        Name="Mb_No"  Visible="true" VisibleIndex="1" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="Nama_Barang" 
                                        Name="Nama_Barang"  Visible="true" VisibleIndex="2" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Quantity" FieldName="Quantity" 
                                        Name="Quantity"  Visible="true" VisibleIndex="3" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="PackedContID" FieldName="PackedContID" 
                                            Name="PackedContID"  Visible="false" VisibleIndex="4" Width="140px">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="No Packed Container" FieldName="NoContainer" 
                                            Name="NoContainer"  Visible="true" VisibleIndex="5" Width="140px">
                                        </dxwgv:GridViewDataTextColumn>
                                </Columns>        
                                </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dxwgv:ASPxGridView>         
            </asp:Panel>
        </div>
            	<asp:HiddenField ID="hfHID" runat="server" />
    	<asp:HiddenField ID="hfMID" runat="server" />
    	<asp:HiddenField ID="hfnamakapal" runat="server" />
    	<asp:HiddenField ID="hfBastNo" runat="server" />
    	<asp:HiddenField ID="hfIDKHIS" runat="server" />
    	<asp:HiddenField ID="hfIDK" runat="server" />
        <div align ="center">
            <asp:Panel ID = "Panel_Report" runat = "server" >
            <asp:Label  ID="lblReport" runat="server"></asp:Label>
            
            <table style="width: 100%" align="center">
                <tr align ="center">
                    <td align ="center" >
                    <table align ="center">
                        <tr align ="center">
                            <td>
                                <input type="button" onclick="window.print()" value="Print" class="btn" />
                            </td>
                            <td>
                                <asp:Button CssClass="btn" ID="btKembaliDevPeriod" runat="server" 
                        Text="Kembali" Width="89px" /> 
                            </td>
                        </tr>
                    </table>
                        
                    </td>
                </tr>
            </table>
            </asp:Panel>
            
                </div>
                


    </form>
</body>
</html>
