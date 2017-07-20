<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportItem.aspx.vb" Inherits="LMS.ReportItem" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
	Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Item Report</title>
     <style type="text/css" media="screen">
			.NoPrint { FONT-SIZE: 9pt; FONT-FAMILY: Arial }
		</style>
		<style type="text/css" media="print">
			.NoPrint, .btn, .kembali { DISPLAY: none }
		</style>		
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <link rel="stylesheet" type="text/css" href="../css/print.css"  />
    <script language="javascript" type ="text/javascript" src="../script/main.js" ></script>
     <script type="text/javascript" language="javascript">
         function getCust() {
             returnValue = ShowDialog2('CustItem', 'Arg', '610', '450');
             if (returnValue) {
                 var comp = new Array();
                 comp = returnValue.split(";");
                 var Name = document.getElementById("TxtCust");
                 Name.value = comp[1];
                 var ID = document.getElementById("hfCID");
                 ID.value = comp[0]
             }
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama">
         <div class="formtitle"><b>Item Report </b></div>
         <br />
            <div class = "div_input">
                 <table>
                    <tr>
                        <td style = " width : 200px ">
                            Customer Name
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID = "TxtCust" runat = "server" ></asp:TextBox>
                            <img alt="Browse" onclick="javascript:getCust();" src="../images/search.png" /></td>
                    </tr>
                    <tr>
                        <td>
                             <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btView" runat="server" Text="View Report">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <asp:Label ID = "lblError" runat = "server" ForeColor = "Black" BackColor ="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                 
                 
                 </table>
                 <CR:CrystalReportViewer ID="crv1" runat="server" AutoDataBind="true" />  
            </div>
        
                 
    </div>
    <asp:HiddenField ID="hfCID" runat="server" />
    </form>
</body>
</html>
