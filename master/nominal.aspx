<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="nominal.aspx.vb" Inherits="LMS.nominal" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
    
<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxm" %>

<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc1" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<%@ Register TagPrefix="dxe" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v7.3" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v7.3" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
<uc1:header runat="server" ID="header" />

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
      <section class="content-header">
          <h1>Master Nominal
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-database"></i> Master</a></li>
              <li class="active">Nominal</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-arrows-h"></i> Range Nominal</h3>
              </div>
              <asp:literal id="ltrInfo" runat="server"></asp:literal>

              <form id="form1" runat="server" class="form-horizontal">
                  <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Nominal :</label>
                          <div class="col-sm-10">
                              <asp:textbox id="TxtNominal" runat="server" cssclass="form-control"></asp:textbox>
                          </div>
                      </div>
                      <asp:label id="linfo" runat="server" text="" cssclass="error"></asp:label>
                      <asp:label id="linfoberhasil" runat="server" text="" cssclass="berhasil"></asp:label>


                      <div class="box-footer text-center">
                          <dxe:aspxbutton id="btSimpan" runat="server" text="Simpan" cssclass="btn btn-primary" enabledefaultappearance="False"></dxe:aspxbutton>
                          <dxe:aspxbutton id="btBatal" runat="server" text="Reset" cssclass="btn btn-danger" enabledefaultappearance="False"></dxe:aspxbutton>
                      </div>
                      <br />
                      <dxwgv:ASPxGridView ID="Grid_Jenis" runat="server" font-size="10pt" autogeneratecolumns="False" ClientInstanceName="Grid_LinkedAccount" KeyFieldName="IDMasterRangeNominal" width="100%">
                          <SettingsBehavior AllowFocusedRow="True" />
                          <SettingsPager PageSize="15">
                          </SettingsPager>
                          <Settings ShowFilterRow="True" />

                          <Styles>
                              <Header BackColor="#2c3848" Font-Bold="true" ForeColor="#FFFFFF"
                                  HorizontalAlign="Center" HoverStyle-Border-BorderColor="#515763">
                                  <HoverStyle>
                                      <Border BorderColor="#515763"></Border>
                                  </HoverStyle>
                              </Header>

                              <FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>

                          </Styles>
                          <Columns>

                              <dxwgv:GridViewDataTextColumn Caption="IDMasterRangeNominal" FieldName="IDMasterRangeNominal"
                                  Name="IDMasterRangeNominal" Visible="False" VisibleIndex="2">
                              </dxwgv:GridViewDataTextColumn>

                              <dxwgv:GridViewDataTextColumn Name="Nominal" Caption="Nominal" VisibleIndex="0" Width="20%" FieldName="Nominal">
                                  <PropertiesTextEdit DisplayFormatString="{0:###,###,###}">
                                  </PropertiesTextEdit>
                              </dxwgv:GridViewDataTextColumn>
                              <dxwgv:GridViewDataTextColumn Name="Edit" Caption="#" VisibleIndex="1" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;" CommandName="Edit" runat="server"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
								    </DataItemTemplate>
						</dxwgv:GridViewDataTextColumn>
                              <dxwgv:GridViewDataTextColumn Caption="#" Name="Delete" VisibleIndex="2" Width="1%">
                                  <DataItemTemplate>
                                      <asp:LinkButton ID="tbdelete" runat="server" CommandName="Delete" cssClass="btn btn-block btn-danger btn-xs" OnClientClick="return confirm('Are You Sure Want to Delete ?');" style="color : #FFF;" ToolTip="Delete Item"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                                  </DataItemTemplate>
                              </dxwgv:GridViewDataTextColumn>
                          </Columns>

                      </dxwgv:ASPxGridView>
                  <asp:hiddenfield id="hfID" runat="server" />
                  <asp:hiddenfield id="hfMode" runat="server" />
    </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />
