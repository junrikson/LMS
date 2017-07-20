<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Rekening.aspx.vb" Inherits="LMS.Rekening" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
	<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Master Rekening</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style1
        {
            width: 45px;
        }
        .style2
        {
            width: 103px;
        }
        .style3
        {
            width: 93px;
        }
        .style4
        {
            width: 98px;
        }
    </style>
</head>

<body class="mainmenu">
    <form id="form1" runat="server">
   
    <div class="Divutama">
        <div class="formtitle">Master Satuan Other</div>
        <br />
        <div class="div_input">
        <div class="div_umum">
            
            <table >
                <tr>
                    <td>
                         <table class = "borderdot">
					<tr>
						<td class="style2"><b>No Rekening :</B></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbnorek" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style2"><b>Atas Nama :</B></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbnama" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style2"><b>Nama Bank :</B></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbbank" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
					    <td colspan = "3" align="center">
					        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			                <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					    </td>
					</tr>
					
					<tr>
						<td class="style2">&nbsp;</td><td class="style4">
								<dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
									<Image Url="../images/save-alt.png" />
								</dxe:ASPxButton>
						</td>
					    <td class="style3">
								<dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
									<Image Url="../images/undo.png" />
								</dxe:ASPxButton>
							</td>
					</tr>
				</table>
                    </td>
                </tr>
            </table>
           
				
            <br />
        <asp:HiddenField ID="hfID" runat="server" />
				<asp:HiddenField ID="hfMode" runat="server" />
        
        
            <dxwgv:ASPxGridView ID="GridView_satuanother" runat="server"  Font-Size = "9pt"	
                AutoGenerateColumns="False" KeyFieldName="ID" Width="300px">
				<SettingsPager PageSize="15" AlwaysShowPager="True">
                </SettingsPager>
				<settings showfilterrow="True" />
				<Styles>
                <Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#FFFFFF" Font-Bold="true" HorizontalAlign=Center >
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
								<dxwgv:GridViewDataTextColumn Caption="No Rekening" Name="NoRek" VisibleIndex="1" 
									FieldName="NoRek" Width="20px">
								</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn Caption="Atas Nama" Name="AN" VisibleIndex="2" 
									FieldName="AN" Width="20px">
								</dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn Caption="Nama Bank" Name="NamaBank" VisibleIndex="3" 
									FieldName="NamaBank" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								
								<dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="4" Width="1%">
								<DataItemTemplate>
								    <asp:LinkButton ID="tbdelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');">Delete</asp:LinkButton>
								</DataItemTemplate>
								</dxwgv:GridViewDataColumn>
								
								<dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="3" Width="1%">
								<DataItemTemplate>
								<asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								</DataItemTemplate>
								</dxwgv:GridViewDataColumn>
						</Columns>
			</dxwgv:ASPxGridView>
    </div>
</div>
    </form>

</body>
</html>
