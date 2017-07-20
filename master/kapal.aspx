<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="kapal.aspx.vb" Inherits="LMS.kapal" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<uc1:header runat="server" ID="header" />

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
      <section class="content-header">
          <h1>Master Kapal
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-database"></i>Master</a></li>
              <li class="active">Kapal</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-ship"></i> Kapal</h3>
              </div>
              <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>

    <form id="form1" runat="server" class="form-horizontal">
                  <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Nama Kapal :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="TxtNamaKapal" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Nahkoda Kapal :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="TxtNahkoda" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Singkatan Kapal :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="TxtSingkatanKapal" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Kapal Milik :</label>
                          <div class="col-sm-10">
                              <asp:DropDownList ID="DDLMilik" runat="server" CssClass="form-control">
                                  <asp:ListItem Text="Sendiri" Value="Sendiri">Sendiri</asp:ListItem>
                                  <asp:ListItem Text="PerusahaanLain" Value="PerusahaanLain">Perusahaan Lain</asp:ListItem>
                              </asp:DropDownList>
                          </div>
                      </div>
                   
                        <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
				        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
         
                      <div class="box-footer text-center">
                          <dxe:aspxbutton id="btSimpan" runat="server" text="Simpan" cssclass="btn btn-primary" enabledefaultappearance="False"></dxe:aspxbutton>
                          <dxe:aspxbutton id="btBatal" runat="server" text="Reset" cssclass="btn btn-danger" enabledefaultappearance="False"></dxe:aspxbutton>
                      </div>
        <br />
            <dxwgv:ASPxGridView ID="Grid_Kapal" runat="server" AutoGenerateColumns = "false" ClientInstanceName = "grid"
                 KeyFieldName = "ID" Font-Size = "10pt" Width = "100%">
                <SettingsBehavior AllowFocusedRow="True" />
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
                    <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
                    <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                        Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nama Kapal" FieldName="Nama_Kapal" 
                        Name="Nama_Kapal"  Visible="true" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Nahkoda Kapal" FieldName="Nahkoda_Kapal" 
                        Name="Nahkoda_Kapal"  Visible="true" VisibleIndex="2" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Singkatan Kapal" FieldName="Alias_Kapal" 
                        Name="Alias_Kapal"  Visible="true" VisibleIndex="3" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ket Kapal" FieldName="KeteranganKapal" 
                        Name="KeteranganKapal"  Visible="true" VisibleIndex="4" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="5" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" ToolTip="Edit Barang" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="6" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" ToolTip="Delete Barang" cssClass="btn btn-block btn-danger btn-xs" style="color : #FFF;" OnClientClick="return confirm('Are You Sure Want to Delete ?');" ><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            
            <br />
	<asp:HiddenField ID="hfID" runat="server" />
	<asp:HiddenField ID="hfMode" runat="server" />
    </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />
