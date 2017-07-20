<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportQuoKub.aspx.vb" Inherits="LMS.ReportQuoKub" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>Quotation Report</title>
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
   function getKondisi() {
        returnValue = ShowDialog2('Kondisi','Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var ID = document.getElementById("hfTID");
            ID.value = comp[0]
            var Name = document.getElementById("TxtKondisi");
            Name.value = comp[1];
            var Kondisi = document.getElementById("hfKID");
            Kondisi.value = comp[1];
        }
    }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama">
        <div class = "div_input">
            <asp:Panel ID = "Panel_Input" runat = "server">
                <table >
                <tr>
                        <td style = " width : 250px ;  "  >
                            <asp:CheckBox ID="ChkTmplHeader" runat="server" Text="Tampil Header" />
                        </td>
                        <td ></td>
                        <td>
                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td style = " width : 250px ;  "  >
                            Header
                        </td>
                        <td >:</td>
                        <td>
                            <asp:DropDownList ID="ddlHeader" runat = "server" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style = " width : 250px ; ">
                            Kondisi
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID = "TxtKondisi" runat = "server" Enabled="False" 
                                TextMode="MultiLine" Height="88px" Width="632px"></asp:TextBox>
                            <img alt="Browse" onclick="javascript:getKondisi();" src="../images/search.png" /></td>
                    </tr>
                    <tr>
                        <td style = " width : 250px ; ">
                            Tulisan Header 
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID = "TxtTulisan" runat = "server" TextMode="MultiLine" Height="88px" Width="632px"></asp:TextBox>
                    </tr>
                      <tr>
                        <td style = " width : 250px ; ">
                            Penanda Tangan 
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID = "Txttanda" runat = "server" Width="229px" ></asp:TextBox>
                    </tr>
                    <tr>
                        <td style = " width : 250px ; ">
                            Jabatan 
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID = "TxtJabatan" runat = "server" Width="229px" ></asp:TextBox>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="left" >
                                        <dxe:ASPxButton ID="btAdd" runat="server" Text="Add">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btView" runat="server" Text="View Report" Width="95px" >
                                        </dxe:ASPxButton>
                                    </td>  
                                    <td>
                                        <dxe:ASPxButton ID="btviewquotation" runat="server" Text="Quotation">
                                        </dxe:ASPxButton>
                                    </td>                
                                </tr>
                            </table>
                        </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID= "lblError" runat ="server" CssClass="error"></asp:Label>                    
                    </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div>
            <asp:Panel ID ="Panel_Grid" runat ="server" >
               <dxwgv:ASPxGridView ID="Grid_Kondisi" runat="server"  Font-Size = "9pt"	
                AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
				<SettingsPager PageSize="20">
                </SettingsPager>
				<settings showfilterrow="True" />
				<Styles>
                <Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#FFFFFF" Font-Bold="true" HorizontalAlign="Center" >
                <HoverStyle>
                <Border BorderColor="#515763"></Border>
                </HoverStyle>
                    </Header>
						<FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
					    <AlternatingRow Enabled="True" BackColor="#f4f4e9"></AlternatingRow>
					    <Cell Paddings-PaddingLeft="3" Paddings-PaddingRight="3" Paddings-PaddingBottom="1" Paddings-PaddingTop="1">
                        <Paddings PaddingLeft="3px" PaddingTop="1px" PaddingRight="3px" PaddingBottom="1px"></Paddings>
                    </Cell>
						</Styles>
						<Columns>
								<dxwgv:GridViewDataTextColumn Caption="ID" Name="ID" Visible="false" 
									VisibleIndex="0" FieldName="ID">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Kondisi" Name="Nama_Kondisi" VisibleIndex="1" 
									FieldName="Nama_Kondisi" Width="500px">
								</dxwgv:GridViewDataTextColumn>
								
								<dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="4" Width="1%">
								<DataItemTemplate>
								    <asp:LinkButton ID="tbdelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');">Delete</asp:LinkButton>
								</DataItemTemplate>
								</dxwgv:GridViewDataColumn>
						</Columns>
			</dxwgv:ASPxGridView>
            </asp:Panel>
        </div>
        <div align ="center" style ="display : block">
            <asp:Panel ID = "Panel_Report" runat = "server" >
           <asp:Label  ID="lblReport" runat="server" ></asp:Label>
            <table style="width: 100%" align="center">
                <tr align ="center">
                    <td align ="center">
                    <br />
                    <table align ="center" >
                        <tr align ="center">
                            <td > 
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
    </div>
    <asp:HiddenField ID="hfTID" runat="server" />
    <asp:HiddenField ID="hfKID" runat="server" />
    
    </form>
</body>
</html>
