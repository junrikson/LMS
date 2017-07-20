Partial Public Class LaporanPembayaran
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


End Class