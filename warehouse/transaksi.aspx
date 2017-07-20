<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.transaksi" Codebehind="transaksi.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v7.3" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<uc1:header runat="server" ID="header" />

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
      <section class="content-header">
          <h1>Transaksi Kontainer
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-truck"></i>Transaksi</a></li>
              <li class="active">Kontainer</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-truck"></i> Transaksi Kontainer</h3>
              </div>
              <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>

    <form id="form1" runat="server" class="form-horizontal">
                  <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">No. Transaksi :</label>
                          <div class="col-sm-4">
                              <asp:Label ID = "LblCode" runat="server" CssClass="form-control"></asp:Label>
                          </div>
                          <label class="col-sm-2 control-label text-right">Tanggal :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtDate" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Kontainer :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtContainer" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">No. Seal :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtSeal" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Pengirim :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtSender" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Penerima :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtReceiver" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Jenis Barang :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtType" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Merek :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtBrand" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Berat (kg) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtWeight" runat="server" CssClass="form-control"></asp:TextBox>
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

				      <asp:HiddenField ID="hfID" runat="server" />
				      <asp:HiddenField ID="hfMode" runat="server" />
			
			          <br />
			          <dxwgv:ASPxGridView ID="Grid_Transaction" ClientInstanceName="grid" runat="server" Font-Size = "10pt" KeyFieldName="ID" 
								Width="100%" AutoGenerateColumns="False">			
								<Styles>
									<Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#ffffff" Font-Bold="true" HorizontalAlign=Center>
                                        <HoverStyle>
                                        <Border BorderColor="#515763"></Border>
                                        </HoverStyle>
                                    </Header>
									<FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
									<Row BackColor="#ffffff"></Row>
							
								</Styles>
								<SettingsPager AlwaysShowPager="True" PageSize="15">
                                </SettingsPager>
								<Settings ShowFilterRow="True"  />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
								<Columns>
									<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="false" Caption="ID" Name="ID">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="code" caption = "No. Transaksi" VisibleIndex="1" Name="code">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="date" caption = "Tanggal" VisibleIndex="2" Name="date" >
									</dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn FieldName="container" caption = "Kontainer" VisibleIndex="2" Name="container" >
									</dxwgv:GridViewDataColumn>
	                                <dxwgv:GridViewDataTextColumn Caption="Seal" FieldName="seal" Name="seal" VisibleIndex="3">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Pengirim" FieldName="sender" Name="sender" VisibleIndex="4">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Penerima" FieldName="receiver" Name="receiver" VisibleIndex="5">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Jenis Barang" FieldName="type" Name="type" VisibleIndex="6">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Merek" FieldName="brand" Name="brand" VisibleIndex="7">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Berat" FieldName="weight" Name="weight" VisibleIndex="8">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="note" Name="note" VisibleIndex="9">
                                    </dxwgv:GridViewDataTextColumn>
	                                <dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="10" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;" runat="server"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>
    								
								    <dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="11" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" cssClass="btn btn-block btn-danger btn-xs" style="color : #FFF;" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" ><i class="fa fa-trash"></i> Delete</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>								
								    </Columns>
							    </dxwgv:ASPxGridView>
         </div>			
    </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />
