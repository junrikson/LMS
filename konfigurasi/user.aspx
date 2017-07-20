<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.user" Codebehind="user.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v7.3" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<uc1:header runat="server" ID="header" />

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
      <section class="content-header">
          <h1>Master User
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-database"></i>Master</a></li>
              <li class="active">User</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-user"></i> User</h3>
              </div>
              <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>

    <form id="form1" runat="server" class="form-horizontal">
                  <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Role :</label>
                          <div class="col-sm-4">
                              <asp:DropDownList ID="DDLGolongan" runat="server" CssClass="form-control" DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="roleID"></asp:DropDownList>
                              <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>" SelectCommand="SELECT [roleID], [name] FROM [roles] WHERE [status] <> 0">
                                  <SelectParameters>
                                      <asp:Parameter DefaultValue="1" Name="status" Type="Int16" />
                                  </SelectParameters>
                              </asp:SqlDataSource>
                          </div>
                          <label class="col-sm-2 control-label text-right">Company :</label>
                          <div class="col-sm-4">
                             <asp:DropDownList ID="DDLCompany" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="companyID"></asp:DropDownList>
                             <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>" SelectCommand="SELECT [companyID], name FROM [masterCompany] WHERE [status] <> 0"></asp:SqlDataSource>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">User ID :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtKode" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Nama :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtNama" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Password :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="Txtpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                          </div>
                          <label class="col-sm-2 control-label text-right">Confirm Password :</label>
                          <div class="col-sm-4">
                              <asp:TextBox ID="TxtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                          </div>
                      </div>
                      

					  <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
			          <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>

                      <div class="box-footer text-center">
                          <dxe:aspxbutton id="btSimpan" runat="server" text="Simpan" cssclass="btn btn-primary" enabledefaultappearance="False"></dxe:aspxbutton>
                          <dxe:aspxbutton id="btBatal" runat="server" text="Reset" cssclass="btn btn-danger" enabledefaultappearance="False"></dxe:aspxbutton>
                      </div>

				<asp:HiddenField ID="hfMode" runat="server" />
			
			
			
				<br />
							<dxwgv:ASPxGridView ID="grid_Master_User" Font-Size = "10pt"	
                                runat="server" KeyFieldName="ID" 
								Width="100%" AutoGenerateColumns="False">	
								<SettingsPager PageSize="15" alwaysshowpager="True">
                            </SettingsPager>
								<Settings ShowFilterRow="True"  />
								<Columns>
									<dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" Visible="False" VisibleIndex="0">
                                    </dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataColumn FieldName="userID" VisibleIndex="0" Caption="User">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="name" Caption="Nama" VisibleIndex="1">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataTextColumn Caption="Role" FieldName="roleID" Name="roleID" VisibleIndex="2">
                                    </dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataTextColumn Caption="Company" FieldName="companyID" Name="companyID" VisibleIndex="3"></dxwgv:GridViewDataTextColumn><dxwgv:GridViewDataColumn Caption = "#" VisibleIndex="4" Name="Edit" Width="1%" >
									    <DataItemTemplate>
                                            <asp:LinkButton ID="tbedit" runat="server" CommandName="Edit" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;" ToolTip="Edit Item"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                                        </DataItemTemplate>
									</dxwgv:GridViewDataColumn>
	                                <dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="5" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" cssClass="btn btn-block btn-danger btn-xs" style="color : #FFF;" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>
    								
								    <dxwgv:GridViewDataColumn Name="Reset Password" Caption="#" VisibleIndex="6" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbReset" ToolTip="Reset Password" CommandName="ResetPassword" cssClass="btn btn-block btn-warning btn-xs" style="color : #FFF;" runat="server" OnClientClick="return confirm('Are You Sure Want to Reset password ?');" ><i class="fa fa-lock"></i> Reset Password</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>	
								    </Columns>
								
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
							    </dxwgv:ASPxGridView>			
					
				</div>
			</div>
			<p>
                <asp:HiddenField ID="hiddenid" runat="server" />
            </p>
			</div>
    </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />
