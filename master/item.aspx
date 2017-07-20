<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.item" Codebehind="item.aspx.vb" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<%@ Register TagPrefix="dxe" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v7.3" %>
<%@ Register TagPrefix="dxwgv" Namespace="DevExpress.Web.ASPxGridView" Assembly="DevExpress.Web.ASPxGridView.v7.3" %>
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
                  <div class="box-body">
                      <div class="form-group">
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Nama Customer (*) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtCustomer" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Kode Barang (*) :</label>
                          <div class="col-sm-4">
                              <asp:Label ID="LblCode" runat="server" Text="" CssClass="form-control"></asp:Label>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Nama Barang (*) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtName" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Berat (Kg) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtWeight" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div> 
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Panjang (cm) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtLength" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Lebar (cm) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtWidth" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div> 
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Tinggi (cm) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtHeight" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Quantity :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtQuantity" runat="server" CssClass="form-control">1</asp:TextBox>
                          </div>
                      </div> 
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Keterangan :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="TxtNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                          </div>
                      </div> 

					  <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			          <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					     
                      <div class="box-footer text-center">
                          <dxe:aspxbutton id="btSimpan" runat="server" text="Simpan" cssclass="btn btn-primary" enabledefaultappearance="False"></dxe:aspxbutton>
                          <dxe:aspxbutton id="btBatal" runat="server" text="Reset" cssclass="btn btn-danger" enabledefaultappearance="False"></dxe:aspxbutton>
                      </div>
       <br />       
         <asp:Panel ID="PanelApprove" runat="server">
            <div class="div_input">
                <div class="div_umum">
                    <div align="center">
                 <br />
                    <dxwgv:ASPxGridView ID="GridItem_Approve" runat="server" 
                        AutoGenerateColumns="False" Font-Size="10pt" KeyFieldName="ID" 
                        Width="100%" ClientInstanceName = "grid">
                        <ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
                        <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" Visible="False" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="customer" Name="customer" VisibleIndex="0">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Kode Barang" FieldName="code" Name="code" VisibleIndex="1" Width="120px">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Nama Barang" FieldName="name" Name="name" VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Berat" FieldName="weight" Name="weight" VisibleIndex="3">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Panjang" FieldName="length" Name="length" VisibleIndex="4">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Lebar" FieldName="width" Name="width" VisibleIndex="5">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Tinggi" FieldName="height" Name="height" VisibleIndex="6">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Quantity" FieldName="quantity" Name="quantity" VisibleIndex="7">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="note" Name="note" VisibleIndex="8">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="9" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbedit" runat="server" CommandName="Edit" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;" ToolTip="Edit Item"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="10" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbdelete" runat="server" CommandName="Delete" cssClass="btn btn-block btn-danger btn-xs" OnClientClick="return confirm('Are You Sure Want to Delete ?');" style="color : #FFF;" ToolTip="Delete Item"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                        </Columns>
                        <SettingsBehavior AllowFocusedRow="True" />
                        
                        <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
						<Settings ShowFilterRow="True"  />
                        <styles>
                            <header backcolor="#2c3848" font-bold="true" forecolor="#FFFFFF" horizontalalign="Center" hoverstyle-border-bordercolor="#515763">
                                <hoverstyle><border bordercolor="#515763"></border></hoverstyle>
                            </header>
                            <row backcolor="#ffffff">
                            </row>
                            <focusedrow backcolor="#D3D1D4" forecolor="#000000">
                            </focusedrow>
                        </styles>
                    </dxwgv:ASPxGridView>
			    <br />        
         <asp:HiddenField id="hfMode" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfCID" runat="server"></asp:HiddenField>
         <asp:HiddenField id="HFNamaCustomer" runat="server"></asp:HiddenField>
         <asp:HiddenField id="HFkodecust" runat="server"></asp:HiddenField>
                      </asp:panel>
              </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />