<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="tujuan.aspx.vb" Inherits="LMS.tujuan" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
	<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<uc1:header runat="server" ID="header" />

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
      <section class="content-header">
          <h1>Master Tujuan
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-database"></i>Master</a></li>
              <li class="active">Tujuan</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-map-marker"></i> Tujuan</h3>
              </div>
              <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>

    <form id="form1" runat="server" class="form-horizontal">
        <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Tujuan :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="TxtTujuan" runat="server" CssClass="form-control"></asp:TextBox>
                          </div>
                      </div>
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Pelabuhan :</label>
                          <div class="col-sm-10">
                              <asp:TextBox ID="Txtpelabuhan" runat="server" CssClass="form-control"></asp:TextBox>
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
        <div class="div_umum">
        
            <dxwgv:ASPxGridView ID="Gridview_Tujuan" runat="server"  Font-Size = "10pt"	
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
								<dxwgv:GridViewDataTextColumn Caption="ID" Name="ID" Visible="false" 
									VisibleIndex="0" FieldName="ID">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Tujuan" Name="tujuan" VisibleIndex="1" 
									FieldName="Tujuan" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								<dxwgv:GridViewDataTextColumn Caption="Pelabuhan" Name="Pelabuhan" VisibleIndex="2" 
									FieldName="Pelabuhan" Width="20px">
								</dxwgv:GridViewDataTextColumn>
								
								
								<dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="3" Width="1%">
								<DataItemTemplate>
								<asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server" cssClass="btn btn-block btn-primary btn-xs" style="color : #FFF;"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
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
