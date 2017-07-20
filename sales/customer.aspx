<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.customer" Codebehind="customer.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<uc1:header runat="server" ID="header" />

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">
    <!-- Content Header (Page header) -->
        <section class="content-header">
          <h1>Master Item
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-database"></i>Master</a></li>
              <li class="active">Item</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-cubes"></i>Item</h3>
              </div>
              <asp:literal id="ltrInfo" runat="server"></asp:literal>

              <form id="form1" runat="server" class="form-horizontal">
   <asp:Panel ID="Panel1" runat ="server" >
        <div class="formtitle2">
            <b>Input Customer</b>  
        </div>
        <br />
            <asp:Panel ID="PanelApprove" runat="server">
            <div class = "div_umum2" >
               <div class="div_input2">    
                <table border = "0">
                    <tr>
                        <td>
                            <dxwgv:ASPxGridView ID="GridView_CustomerApprove" runat="server"  Font-Size = "9pt"	
                    AutoGenerateColumns="False" KeyFieldName="ID" Width="90%">
                    <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
								<Settings ShowFilterRow="True" />
								<SettingsBehavior AllowFocusedRow="True" />
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
								<dxwgv:GridViewDataTextColumn Caption="Kode Konsumen" Name="kode_customer" VisibleIndex="1" 
									FieldName="kode_customer" Width="100px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Tipe Konsumen" Name="tipe_customer" VisibleIndex="2" 
									FieldName="tipe_customer" Width="120px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Nama Konsumen" Name="nama_customer" VisibleIndex="3" 
									FieldName="nama_customer" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Jenis Kelamin" Name="jenis_kelamin"  Visible="false" VisibleIndex="4" 
									FieldName="jenis_kelamin" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Tipe Perusahaan" Name="Jenis_Perusahaan"  Visible="true" VisibleIndex="4" 
									FieldName="Jenis_Perusahaan" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Alamat" Name="alamat" VisibleIndex="5" 
									FieldName="alamat" Width="200px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Area" Name="area" VisibleIndex="6" 
									FieldName="area" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Kota Asal barang" Name="kotaditunjukan" VisibleIndex="6" 
									FieldName="kotaditunjukan" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Kode Pos" Name="kodepos" VisibleIndex="7" 
									FieldName="kodepos" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="No Telp1" Name="no_telp1" VisibleIndex="8" 
									FieldName="no_telp1" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="No Telp2" Name="no_telp2" VisibleIndex="9" 
									FieldName="no_telp2" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="No HP" Name="no_hp" VisibleIndex="10" 
									FieldName="no_hp" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Fax" Name="fax"  Visible="true" VisibleIndex="11" 
									FieldName="fax" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="email" Name="email" VisibleIndex="12" 
									FieldName="email" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Notes" Name="notes" VisibleIndex="13" 
									FieldName="notes" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Contact Person" Name="ContactPerson" VisibleIndex="14" 
									FieldName="ContactPerson" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Yang Input" Name="YgInput" VisibleIndex="15" 
									FieldName="YgInput" Width="150px">
								</dxwgv:GridViewDataTextColumn>
								
								<dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="16" Width="1%">
								<DataItemTemplate>
								<asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								</DataItemTemplate>
								</dxwgv:GridViewDataColumn>
								
								<dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="17" Width="1%">
								<DataItemTemplate>
								<asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
								</DataItemTemplate>
								</dxwgv:GridViewDataColumn>
						</Columns>
			</dxwgv:ASPxGridView>
			<br />
			<div align="center">
                        <dxe:ASPxButton ID="btn_new" runat="server" Text="Tambah Baru" Font-Size ="Large" >
                        </dxe:ASPxButton>
                </div>
                        </td>
                    </tr>
                </table>
                    
			  </div>
            </div>
            </asp:Panel>
            
			    
			    
              
            
   </asp:Panel>
  <div class="Divutama"> 
   <asp:Panel ID="Panel2" runat="server">     
        <div class="formtitle">
            <b>Input Customer</b>  
        </div> 
        <br />
        <div class="div_input">
            <div class="div_umum">
                
                <table style="width:100%;">
                    <tr>
                        <td class="style1">
                            Tanggal Input</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <dxe:ASPxDateEdit ID="Date_Tgl" EditFormat="Custom" EditFormatString="MM/dd/yyyy" Enabled="false" runat="server">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Kode Konsumen</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:Label ID="LblKodeCustomer" runat="server" Text="" Font-Bold="true" Font-Size="Large"></asp:Label>
                        </td>
                        <td class="style6">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style4">
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Tipe Konsumen</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:DropDownList ID="DDLTipeCustomer" runat="server" AutoPostBack = "true" >
                                <asp:ListItem Value="-Pilih-">-Pilih-</asp:ListItem>
                                <asp:ListItem Value="Perusahaan" >Perusahaan</asp:ListItem>
                                <asp:ListItem Value="Perorangan">Perorangan</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Nama</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtNamaCustomer" runat="server" Width="184px"></asp:TextBox>
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Contact Person</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtContactPerson" runat="server" Width="184px" MaxLength="199"></asp:TextBox>
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>

                    <tr>
                        <td class="style1">
                            <asp:Label ID = "Typekonsumen" runat = "server" Text = "Tipe Konsumen " ></asp:Label></td>
                        <td class="style2">
                            <asp:Label ID = "titikdua" runat = "server" Text = ":" ></asp:Label></td>
                        <td class="style3">
                            <asp:RadioButtonList ID="cb_JenisKelamin" runat="server" RepeatDirection="Horizontal" 
                                Width="86px">
                                <asp:ListItem>Pria</asp:ListItem>
                                <asp:ListItem>Wanita</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RadioButtonList ID="CbJenisPerusahaan" runat="server" RepeatDirection="Horizontal" 
                                Width="300px">
                                <asp:ListItem>PT</asp:ListItem>
                                <asp:ListItem>PD</asp:ListItem>
                                <asp:ListItem>CV</asp:ListItem>
                                <asp:ListItem>TOKO</asp:ListItem>
                                <asp:ListItem>UD</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>

                    </tr>
                    
                    <tr>
                        <td class="style1">
                            Alamat</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtAlamat" runat="server" Height="99px" Width="247px" TextMode = "MultiLine"></asp:TextBox>
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Kota</td>
                        <td class="style2">
                            : </td>
                        <td class="style3">
                            <asp:TextBox ID="TxtArea" runat="server" Width="181px"></asp:TextBox>
                        </td>
                        <td class="style6">
                            </td>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                        
                            
                        </td>
                        <td>
                            </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Kota Asal Barang</td>
                        <td class="style2">
                            : </td>
                        <td class="style3">
                            <%--<asp:TextBox ID="TxtKotaDitunjukan" runat="server" Width="181px"></asp:TextBox>
                            <img alt="Browse" src="../images/search.png" onclick="javascript:getTujuan();" />--%>
                            <asp:DropDownList ID="DDLKotaASAlBarang" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="style6">
                            </td>
                        <td class="style5">
                            No Handphone</td>
                        <td class="style4">
                            :</td>
                        <td>
                            <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TxtAreaNoHP" runat="server" Width="50px" MaxLength="4"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtNoHP" runat="server" Width="150px"></asp:TextBox>    
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Kode Pos</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <asp:TextBox ID="TxtKodePos" runat="server" Width="181px"></asp:TextBox>
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            Email</td>
                        <td class="style4">
                            :</td>
                        <td>
                            <asp:TextBox ID="TxtEmail" runat="server" Width="181px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            No Telp #1</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                         <table>
                          <tr>
                            <td>
                                <asp:TextBox ID="txtnoarea1" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNoTelp1" runat="server" Width="130px"></asp:TextBox>
                            </td>
                          </tr>
                         </table>
                            
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            Fax</td>
                        <td class="style4">
                            :</td>
                        <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="TxtAreaFax" runat="server" Width="50px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtFax" runat="server" Width="150px"></asp:TextBox>                                
                                </td>
                            </tr>
                        </table>
                            
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            No Telp #2</td>
                        <td class="style2">
                            :</td>
                        <td class="style3">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="Txtnoarea2" runat="server" Width="50px"></asp:TextBox>
                                    </td>
                                    <td>
                                            <asp:TextBox ID="TxtNoTelp2" runat="server" Width="130px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            Notes</td>
                        <td class="style4">
                            :</td>
                        <td>
                            <asp:TextBox ID="TxtNotes" runat="server" Height="99px" Width="247px" TextMode = "MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            &nbsp;</td>
                        <td class="style2">
                            &nbsp;</td>
                        <td class="style3">
                            &nbsp;</td>
                        <td class="style6">
                            &nbsp;</td>
                        <td class="style5">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="8" align="center">
					        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			                <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					    </td>
                    </tr>
                    <tr>
                        <td class="style1" colspan="8" align="center">
                           <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
                                            <Image Url="../images/save-alt.png" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
                                            <Image Url="../images/undo.png" />
                                        </dxe:ASPxButton>
                                    </td>   
                                    <td>
                                        <dxe:ASPxButton ID="btback" runat="server" Text="Kembali" Width="90px">
                                            <Image Url="../images/left.gif" />
                                        </dxe:ASPxButton>
                                    </td> 
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
            </div>
        </div>
            <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
            <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>
    </asp:Panel>
    </div>
    </form>
          
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />