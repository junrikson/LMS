Public Partial Class LaporanTidakBalace
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = Nothing Then
            FormsAuthentication.SignOut()
            Response.Redirect("Index.aspx")
        End If
    End Sub

End Class