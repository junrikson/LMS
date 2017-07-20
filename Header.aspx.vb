
Partial Class header2
    Inherits System.Web.UI.Page

#Region " Page "
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then

                Dim STR As String = "  "

                If Not Session("UserId") Is Nothing Then

                    STR &= " " & Session("UserId").ToString

                End If

                lUser.Text = STR.ToString
                lroles.Text = Session("namaroles")

            End If

        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try

    End Sub

#End Region


    Protected Sub btlogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btlogout.Click
        Try

            Response.Cookies.Remove(FormsAuthentication.FormsCookieName)

            If Not Request.Cookies Is Nothing Then
                Response.Cookies.Remove("userCookie")
            End If
            FormsAuthentication.SignOut()
            Session.Clear()
            Session.Abandon()
            Response.Write("<script language=javascript>parent.location.href = 'index.aspx';</script>")

        Catch ex As Exception
            Response.Write("btLogOut Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btnhome_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnhome.Click
        Response.Write("<script language=javascript>parent.location.href = 'MainMenu.aspx';</script>")
    End Sub

    Protected Sub btnswitch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnswitch.Click
        Try

            Response.Cookies.Remove(FormsAuthentication.FormsCookieName)

            If Not Request.Cookies Is Nothing Then
                Response.Cookies.Remove("userCookie")
            End If
            FormsAuthentication.SignOut()
            Session.Clear()
            Session.Abandon()
            Response.Write("<script language=javascript>parent.location.href = 'index.aspx';</script>")

        Catch ex As Exception
            Response.Write("btLogOut Exception :<br>" & ex.ToString)
        End Try
    End Sub
End Class
