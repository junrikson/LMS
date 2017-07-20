<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="KonfigurasiHeader.aspx.vb" Inherits="LMS.KonfigurasiHeader" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Master Kondisi</title>
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
        .style5
        {
            width: 122px;
        }
    </style>
</head>
<body class="mainmenu">
    <form id="form1" runat="server">
    
    <div class="Divutama">
        <div class="formtitle">Konfigurasi Header</div>
        <br />
        <asp:Panel ID="panelutama" runat="server">
   
        <div class="div_input">
        <div class="div_umum">
            <table style="width:500px;">
				<tr>
				    <td>
				        <table class = "borderdot">
				            <tr>
						<td class="style2"><b>Jenis :</b></td><td class="style1" colspan="2">
						<asp:Label ID="lbljenis" runat="server" Width="150px" 
							MaxLength="100">PERUSAHAAN PELAYARAN</asp:Label></td>
					</tr>
					<tr>
						<td class="style2"><b>Nama :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbnama" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style2"><b>Daerah :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbdaerah" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style2"><b>Alamat :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbAlamat" runat="server" CssClass="tb1" Width="172px" 
							MaxLength="100" Height="77px" TextMode="MultiLine"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style2"><b>No Telp1 :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbNotelp1" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style2"><b>No Telp2 :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbNoTelp2" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
						<tr>
						<td class="style2"><b>Fax :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbfax" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
						<tr>
						<td class="style2"><b>Email :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbemail" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					
						<tr>
						<td class="style2"><b>No Anggota INSA :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbnoanggota" runat="server" CssClass="tb1" Width="150" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style2">
                            <asp:Image ID="img" runat="server" Height="135
							        px" Visible="False" Width="135px" />
                        </td><td class="style1" colspan="2">
						<asp:FileUpload ID="uploadfile" runat="server" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="1px" Width="249px" />
						<asp:Button ID="btnupload" runat="server" Text="Upload" Width="76px" class= "btn" /></td>
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
				</div>
            <br />
        <asp:HiddenField ID="hfID" runat="server" />
				<asp:HiddenField ID="hfMode" runat="server" />
        <div class="div_umum">
        
            <dxwgv:ASPxGridView ID="GridViewHeader" runat="server"  Font-Size = "9pt"	
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
								<dxwgv:GridViewDataTextColumn Caption="Nama" Name="nama" VisibleIndex="1" 
									FieldName="nama" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Daerah" Name="daerah" VisibleIndex="1" 
									FieldName="daerah" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Alamat" Name="alamat" VisibleIndex="1" 
									FieldName="alamat" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="No Tlp1" Name="notelp1" VisibleIndex="1" 
									FieldName="notelp1" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="No Tlp2" Name="notelp2" VisibleIndex="1" 
									FieldName="notelp2" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Fax" Name="fax" VisibleIndex="1" 
									FieldName="fax" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="E-mail" Name="email" VisibleIndex="1" 
									FieldName="email" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="No Anggota INSA" Name="noagtinsa" VisibleIndex="1" 
									FieldName="noagtinsa" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Path" Name="pathlogo" VisibleIndex="1" 
									FieldName="pathlogo" Width="20px">
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
    <br />
            <asp:Button ID="btcabang" runat="server" Text="Cabang" />
     </asp:Panel>
     
     <asp:Panel ID="panelcabang" runat="server">
     <div class="div_input">
     <div class="div_umum">
        <table>
				
					<tr>
						<td class="style5">&nbsp;</td><td class="style1" colspan="2">
						&nbsp;</td>
					</tr>
					<tr>
						<td class="style5"><b>Nama Perusahaan :</b></td><td class="style1" colspan="2">
						<asp:DropDownList ID="ddlnamaperusahaan" runat="server">
                        </asp:DropDownList>
                        </td>
					</tr>
					<tr>
						<td class="style5"><b>Daerah :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbdaerahcbg" runat="server" CssClass="tb1" Width="215px" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style5"><b>Alamat :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbalamatcbg" runat="server" CssClass="tb1" Width="214px" 
							MaxLength="100" Height="94px" TextMode="MultiLine"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style5"><b>No Telp1 :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbtelp1cbg" runat="server" CssClass="tb1" Width="211px" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style5"><b>No Telp2 :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbtelp2cbg" runat="server" CssClass="tb1" Width="209px" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					<tr>
						<td class="style5"><b>No Telp3 :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbtelp3cbg" runat="server" CssClass="tb1" Width="209px" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
						<tr>
						<td class="style5"><b>Fax :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbfaxcbg" runat="server" CssClass="tb1" Width="207px" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
						<tr>
						<td class="style5"><b>Email :</b></td><td class="style1" colspan="2">
						<asp:TextBox ID="tbemailcbg" runat="server" CssClass="tb1" Width="206px" 
							MaxLength="100"></asp:TextBox></td>
					</tr>
					
						<tr>
						<td class="style5">&nbsp;</td><td class="style1" colspan="2">
						    &nbsp;</td>
					</tr>
					<tr>
					    <td colspan = "3" align="center">
					        <asp:Label ID="Linfo2" runat="server" CssClass="error" Visible="False"></asp:Label>
			                <asp:Label ID="Linfoberhasil2" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					    </td>
					</tr>
				
					
					<tr>
						<td class="style5">&nbsp;</td><td class="style4">
								<dxe:ASPxButton ID="btsavecbg" runat="server" Text="Simpan" Width="90px">
									<Image Url="../images/save-alt.png" />
								</dxe:ASPxButton>
						</td>
					    <td class="style3">
								<dxe:ASPxButton ID="tbbatalcbg" runat="server" Text="Reset" Width="90px">
									<Image Url="../images/undo.png" />
								</dxe:ASPxButton>
							</td>
					</tr>
				</table>
				</div>
            <br />
        <asp:HiddenField ID="Hfidcbg" runat="server" />
				<asp:HiddenField ID="hfmodecbg" runat="server" />
        <div class="div_umum">
        
            <dxwgv:ASPxGridView ID="GridViewHeadercbg" runat="server"  Font-Size = "9pt"	
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
								<dxwgv:GridViewDataTextColumn Caption="Nama Perusahaan" Name="namaperusahaan" VisibleIndex="1" 
									FieldName="namaperusahaan" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Daerah" Name="daerah" VisibleIndex="1" 
									FieldName="daerah" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Alamat" Name="alamat" VisibleIndex="1" 
									FieldName="alamat" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="No Tlp1" Name="notelp1" VisibleIndex="1" 
									FieldName="notelp1" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="No Tlp2" Name="notelp2" VisibleIndex="1" 
									FieldName="notelp2" Width="20px">
								</dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="No Tlp3" Name="notelp3" VisibleIndex="1" 
									FieldName="notelp3" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Fax" Name="fax" VisibleIndex="1" 
									FieldName="fax" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="E-mail" Name="email" VisibleIndex="1" 
									FieldName="email" Width="20px">
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
			<br />
            <asp:Button ID="btnkembali" runat="server" Text="Kembali" CssClass="btn" />
     </div>
     </div>
</asp:Panel>     
</div>
    </form>
</body>
</html>
