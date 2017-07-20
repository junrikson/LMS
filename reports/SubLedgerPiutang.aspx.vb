Partial Public Class SubLedgerPiutang
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                tbStartDate.Date = Today
                tbEndDate.Date = Today
            End If

        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try
    End Sub


    'Protected Sub DDLCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLCustomer.SelectedIndexChanged
    '    If DDLCustomer.SelectedValue = "Semua" Then
    '        PanelTampil.Visible = False
    '    Else
    '        PanelTampil.Visible = True
    '    End If
    'End Sub
End Class