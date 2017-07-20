Public Partial Class Neraca
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If


            'If Session("namaroles").ToString = "Master Accounting" Then
            '    If Session("UserId").ToString.ToUpper <> "IRA" _
            '    Or Session("UserId").ToString.ToUpper <> "GIOVANI" _
            '    Or Session("UserId").ToString.ToUpper <> "LIGITAADMIN" Then
            '        Response.Redirect("../NoRoles.aspx")
            '    End If
            'Else
            '    Response.Redirect("../NoRoles.aspx")
            'End If

            If Session("namaroles").ToString = "Master Accounting" And (Session("UserId").ToString.ToUpper <> "IRA" And Session("UserId").ToString.ToUpper <> "GIOVANI" And Session("UserId").ToString.ToUpper <> "LIGITAADMIN") Then
                Response.Redirect("../NoRoles.aspx")
            End If

            If Not Page.IsPostBack Then
                tbDate.Date = Today
            End If

        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try
    End Sub


End Class