<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="company.aspx.vb" Inherits="LMS.company" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v7.3" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<uc1:header runat="server" ID="header" />

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
      <section class="content-header">
          <h1>Master Company
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-database"></i>Master</a></li>
              <li class="active">Company</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-building"></i> Company</h3>
              </div>
              <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>

    <form id="form1" runat="server" class="form-horizontal">
                  <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Kode (*) :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtCompanyID" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Jenis Perusahaan :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtType" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Nama Perusahaan (*) :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="TxtName" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Alamat :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="TxtAddress" runat="server" CssClass="form-control" textmode="MultiLine"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Kota :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtCity" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Kode Pos :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtZip" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Telepon 1 :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtPhone1" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Telepon 2 :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtPhone2" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Fax :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtFax" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Email :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtEmail" runat="server" CssClass="form-control"></asp:TextBox>
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
        
        
            <dxwgv:ASPxGridView ID="GridView_MasterCompany" runat="server"  Font-Size = "10pt"	
                AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
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
								<dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" Visible="False" VisibleIndex="0">
                                </dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Kode" Name="companyID" 
									VisibleIndex="0" FieldName="companyID">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Nama" Name="name" VisibleIndex="1" 
									FieldName="name">
								</dxwgv:GridViewDataTextColumn>
								
								<dxwgv:GridViewDataTextColumn Caption="Jenis " FieldName="type" Name="type" VisibleIndex="2">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Kota" FieldName="city" Name="city" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
								
								<dxwgv:GridViewDataTextColumn Caption="Alamat" FieldName="address" Name="address" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Kode Pos" FieldName="zip" Name="zip" VisibleIndex="5">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Telepon 1" FieldName="phone1" Name="phone1" VisibleIndex="6">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Telepon 2" FieldName="phone2" Name="phone2" VisibleIndex="7">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Fax" FieldName="fax" Name="fax" VisibleIndex="8">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="email" Name="email" VisibleIndex="9">
                                </dxwgv:GridViewDataTextColumn>
								
								<dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="10" Width="1%">
								<DataItemTemplate>
								    <asp:LinkButton ID="tbdelete" ToolTip="Delete Item" CommandName="Delete" cssClass="btn btn-block btn-danger btn-xs" style="color : #FFF;" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
								</DataItemTemplate>
								</dxwgv:GridViewDataColumn>
								
								<dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="11" Width="1%">
								<DataItemTemplate>
								<asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;" runat="server"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
								</DataItemTemplate>
								</dxwgv:GridViewDataColumn>
							
						</Columns>
			</dxwgv:ASPxGridView>
    </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />
