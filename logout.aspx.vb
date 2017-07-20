Public Class logout
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName)

            If Not Request.Cookies Is Nothing Then
                Response.Cookies.Remove("userCookie")
            End If
            FormsAuthentication.SignOut()
            Session.Clear()
            Session.Abandon()
            Response.Write("<script language=javascript>parent.location.href = 'login.aspx';</script>")

        Catch ex As Exception
            Response.Write("btLogOut Exception :<br>" & ex.ToString)
        End Try
    End Sub

End Class