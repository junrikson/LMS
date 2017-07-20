<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="loadingPlan.aspx.vb" Inherits="LMS.loadingPlan" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>
<%@ Register assembly="DevExpress.Web.ASPxGridView.v7.3" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>

<%@ Register Src="~/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/footer.ascx" TagPrefix="uc1" TagName="footer" %>

<uc1:header runat="server" ID="header" />

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
      <section class="content-header">
          <h1>Rencana Muat Kapal
          </h1>
          <ol class="breadcrumb">
              <li><a href="#"><i class="fa fa-file-text"></i>Report </a></li>
              <li class="active">Rencana Muat Kapal</li>
          </ol>
      </section>

    <!-- Main content -->
      <section class="content">

          <!-- COLOR PALETTE -->
          <div class="box box-default color-palette-box">
              <div class="box-header with-border">
                  <h3 class="box-title"><i class="fa fa-file-text"></i> Laporan Rencana Muat Kapal</h3>
              </div>
              <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>

    <form id="form1" runat="server" class="form-horizontal">
                  <div class="box-body">
                      <div class="form-group">
                          <label class="col-sm-2 control-label text-right">Periode :</label>
                          <div class="col-sm-2">
                              <asp:TextBox ID="TxtDate" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                          </div>
                          <label class="col-sm-1 control-label text-center" style="width: 4%">s/d</label>
                          <div class="col-sm-2">
                              <asp:TextBox ID="TxtDate2" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                          </div>
                          <div class="col-sm-2">
                              <dxe:aspxbutton id="BtnSubmit" runat="server" text="Submit" cssclass="btn btn-primary" enabledefaultappearance="False"></dxe:aspxbutton>     
                          </div>                     
                      </div>                    
         </div>			
    </form>
          </div>
      </section>
    <!-- /.content -->
  </div>

<uc1:footer runat="server" id="footer" />