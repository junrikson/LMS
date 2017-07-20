Public Class loadingPlan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.Title = "Rencana Muat Kapal - Logistics Management System"
            ltrInfo.Text = ""
            TxtDate.Text = Now.ToString("yyyy-MM-dd")
            TxtDate2.Text = Now.ToString("yyyy-MM-dd")

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

End Class