<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.roles" Codebehind="roles.aspx.vb" %>

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
          <h1>Konfigurasi Roles (Otoritas)
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-gears"></i> Konfigurasi</a></li>
              <li class="active">Roles (Otoritas)</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-unlock"></i> Roles (Otoritas)</h3>
              </div>
              <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>

    <form id="form1" runat="server" class="form-horizontal">
                  <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Kode (*) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtKode" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>        
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Nama (*) :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="TxtNama" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>        

					  <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			          <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>

                      <div class="box-footer text-center">
                          <dxe:aspxbutton id="btSimpan" runat="server" text="Simpan" cssclass="btn btn-primary" enabledefaultappearance="False"></dxe:aspxbutton>
                          <dxe:aspxbutton id="btBatal" runat="server" text="Reset" cssclass="btn btn-danger" enabledefaultappearance="False"></dxe:aspxbutton>
                      </div>

				<br />
				<asp:HiddenField ID="hfID" runat="server" />
				<asp:HiddenField ID="hfMode" runat="server" />
				<asp:HiddenField ID="Hfroleid" runat="server" />
			
							<dxwgv:ASPxGridView ID="grid_MasterRole" ClientInstanceName="grid" runat="server" KeyFieldName="ID" 
								Width="100%" AutoGenerateColumns="False">	
								<SettingsPager PageSize="10">
                            </SettingsPager>
                            <settings showfilterrow="True" />		
								<Styles>
									<Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#ffffff" Font-Bold="true" HorizontalAlign= "Center">
                                        <HoverStyle>
                                        <Border BorderColor="#515763"></Border>
                                        </HoverStyle>
                                    </Header>
									<FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
									<Row BackColor="#ffffff"></Row>
							
								</Styles>
								<Settings ShowFilterRow="True"  />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
								<Columns>
									<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="roleID" Caption="Role ID" VisibleIndex="1">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="name" Caption = "Nama Role" VisibleIndex="2" Name="name" >
									</dxwgv:GridViewDataColumn>
	                                <dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="4" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;" runat="server"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>
    								
								    <dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="5" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" cssClass="btn btn-block btn-danger btn-xs" style="color : #FFF;" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" ><i class="fa fa-trash"></i> Delete</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>								
								    <dxwgv:GridViewDataTextColumn Caption="#" Name="Detail" VisibleIndex="4" Width="1%">
                                    <DataItemTemplate>
								    <asp:LinkButton ID="tbDetail" ToolTip="Detail" CommandName="Detail" cssClass="btn btn-block btn-success btn-xs" style="color : #FFF;" runat="server"><i class="fa fa-eye"></i> Detail</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataTextColumn>
								    </Columns>
							    </dxwgv:ASPxGridView>	
			</div>
			<p></p>
			
        </div>
    </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />
