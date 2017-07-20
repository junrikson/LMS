<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Warehouse_Report.aspx.vb" Inherits="LMS.Warehouse_Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
	Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Warehouse Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style =  " width : 200px ">
                    From &nbsp;
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="tbFrom" runat="server" EditFormat="Custom" EditFormatString="MM/dd/yyyy">
                    </dxe:ASPxDateEdit>                
                </td>
                <td>
                    To &nbsp;
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="tbSampai" runat="server" EditFormat="Custom" EditFormatString="MM/dd/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    Gudang
                </td>
                <td >
                    <asp:DropDownList ID="DDLSource" runat="server" Width="170">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align =right>
                    <asp:Label ID="lstatus" runat="server" Visible="False" Font-Names="Tahoma" 
                        Font-Size="10pt" ForeColor="#CC0000"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <dxe:ASPxButton ID="btSubmit" runat="server" Text="Submit" >
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <CR:CrystalReportViewer ID="crv1" runat="server" AutoDataBind="true" />  
    </div>
    </form>
</body>
</html>
