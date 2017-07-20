
Partial Class index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Page.Title = "Dashboard - Logistics Management System"

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub
End Class
