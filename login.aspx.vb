Imports System.Data
Imports System.Data.SqlClient

Partial Class login
    Inherits System.Web.UI.Page
    Private dt As DataTable
    Private ds As DataSet
    Private sqlstring As String
    Private con As SqlConnection
    Private sdr As SqlDataReader
    Private dsHasil As DataSet
    Private dtHasil As DataTable

#Region "validation"
    Private Function validation() As Boolean

        If check_server_connection(ConnectionString) = False And check_server_connection(ConnectionString2) = False Then
            lblInfo.Visible = True
            lblInfo.Text = "<div class=""alert alert-danger alert-dismissible fade in"" role=""alert"">" & _
                    "<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">×</span>" & _
                    "</button>" & _
                    "<p style=""text-align: center;"">Koneksi database error.</p>" & _
                    "</div>"
            txtUserID.Focus()
            Return False
        End If

        Try
            If txtUserID.Text.Trim = "" Then
                lblInfo.Visible = True
                lblInfo.Text = "<div class=""alert alert-danger alert-dismissible fade in"" role=""alert"">" & _
                    "<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">×</span>" & _
                    "</button>" & _
                    "<p style=""text-align: center;"">Username harus diisi.</p>" & _
                    "</div>"
                txtUserID.Focus()
                Return False
            End If

            If txtPassword.Text.Trim = "" Then
                lblInfo.Visible = True
                lblInfo.Text = "<div class=""alert alert-danger alert-dismissible fade in"" role=""alert"">" & _
                    "<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">×</span>" & _
                    "</button>" & _
                    "<p style=""text-align: center;"">Password harus diisi.</p>" & _
                    "</div>"
                txtPassword.Focus()
                Return False
            End If

            sqlstring = " SELECT [name], [companyID], [roleID] " & _
                        " FROM masterUser " & _
                        " WHERE [status] = 1 " & _
                        " AND [userID] = '" & txtUserID.Text.Trim.Replace("'", "''") & "' " & _
                        " AND [password] = '" & txtPassword.Text.Trim.Replace("'", "''") & "' "
            ds = SQLExecuteQuery(sqlstring)
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then
                Session("userName") = dt.Rows(0).Item("name").ToString
                Session("companyID") = dt.Rows(0).Item("companyID").ToString
                Session("roleID") = dt.Rows(0).Item("roleID").ToString
                Session("userID") = txtUserID.Text

                sqlstring = " SELECT [name], [address], [phone1] " & _
                            " FROM masterCompany " & _
                            " WHERE [companyID] = '" & Session("companyID") & "'"
                dsHasil = SQLExecuteQuery(sqlstring)
                dtHasil = dsHasil.Tables(0)

                Session("companyName") = dtHasil.Rows(0).Item("name").ToString
                Session("companyAddress") = dtHasil.Rows(0).Item("address").ToString
                Session("companyPhone") = dtHasil.Rows(0).Item("phone1").ToString

                Return True
            Else
                lblInfo.Visible = True
                lblInfo.Text = "<div class=""alert alert-danger alert-dismissible fade in"" role=""alert"">" & _
                    "<button type=""button"" class=""close"" data-dismiss=""alert"" aria-label=""Close""><span aria-hidden=""true"">×</span>" & _
                    "</button>" & _
                    "<p style=""text-align: center;"">Username atau Password salah.</p>" & _
                    "</div>"
                txtUserID.Focus()
                Return False
            End If

        Catch ex As Exception
            Response.Write("Validation Exception :<br>" & ex.ToString)
        End Try


    End Function
#End Region


#Region "button"
    Protected Sub btSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Try
            If validation() Then
                FormsAuthentication.RedirectFromLoginPage(txtUserID.Text, False)
                Response.Redirect("index.aspx")
            End If
        Catch ex As Exception
            Response.Write("btnSubmit Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Protected Sub btCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click

        Try
            Response.Cookies.Remove(FormsAuthentication.FormsCookieName)

            If Not Request.Cookies Is Nothing Then
                Response.Cookies.Remove("userCookie")
            End If

            FormsAuthentication.SignOut()
            Session.Clear()
            Session.Abandon()

            txtUserID.Focus()
            txtUserID.Text = ""
            txtPassword.Text = ""
        Catch ex As Exception
            Response.Write("btnReset Exception :<br>" & ex.ToString)
        End Try

    End Sub
#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Me.Page.User.Identity.IsAuthenticated Then
                Response.Redirect("index.aspx")
            End If

            lblInfo.Visible = False
            txtUserID.Focus()
        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try
    End Sub
End Class
