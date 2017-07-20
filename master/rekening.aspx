<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="rekening.aspx.vb" Inherits="LMS.rekening" %>


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
          <h1>Master Rekening
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-database"></i>Master</a></li>
              <li class="active">Rekening</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-credit-card"></i> Rekening</h3>
              </div>
              <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>

              <form id="form1" runat="server" class="form-horizontal">
                  <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">No Rekening :</label>
                          <div class="col-sm-10">
                              <asp:textbox id="tbnorek" runat="server" cssclass="form-control"></asp:textbox>
                          </div>
                      </div>

                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Atas Nama :</label>
                          <div class="col-sm-10">
                              <asp:textbox id="tbnama" runat="server" cssclass="form-control"></asp:textbox>
                          </div>
                      </div>

                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Nama Bank :</label>
                          <div class="col-sm-10">
                              <asp:textbox id="tbbank" runat="server" cssclass="form-control"></asp:textbox>
                          </div>
                      </div>
                  </div>

                  <asp:label id="lInfo" runat="server" cssclass="error" visible="False"></asp:label>
                  <asp:label id="linfoberhasil" runat="server" cssclass="berhasil" visible="False"></asp:label>

                  
              <!-- /.box-body -->
                  <div class="box-footer text-center">
                      <dxe:aspxbutton id="btSimpan" runat="server" text="Simpan" cssclass="btn btn-primary" EnableDefaultAppearance="False"></dxe:aspxbutton>
                      <dxe:aspxbutton id="btBatal" runat="server" text="Reset" cssclass="btn btn-danger" EnableDefaultAppearance="False"></dxe:aspxbutton>
                  </div>
              <!-- /.box-footer -->

                  <asp:hiddenfield id="hfID" runat="server" />
                  <asp:hiddenfield id="hfMode" runat="server" />


                  <dxwgv:aspxgridview id="GridView_satuanother" runat="server" font-size="10pt"
                      autogeneratecolumns="False" keyfieldname="ID" width="100%" EnableCallBacks="False">
                    <SettingsPager PageSize="15" AlwaysShowPager="True">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" />
                    <Styles>
                        <Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#FFFFFF" Font-Bold="true" HorizontalAlign="Center">
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
                                <asp:linkbutton id="tbdelete" tooltip="Delete Item" commandname="Delete" runat="server" cssClass="btn btn-block btn-danger btn-xs" style="color : #FFF;" onclientclick="return confirm('Are You Sure Want to Delete ?');"><i class="fa fa-trash"></i> Delete</asp:linkbutton>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataColumn>

                        <dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="3" Width="1%">
                            <DataItemTemplate>
                                <asp:linkbutton id="tbedit" tooltip="Edit Item" commandname="Edit" runat="server" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;"><i class="fa fa-edit"></i> Edit</asp:linkbutton>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataColumn>
                    </Columns>
                </dxwgv:aspxgridview>

              </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />
