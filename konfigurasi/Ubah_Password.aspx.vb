Imports System.Data
Imports System.Data.SqlClient
Partial Class Konfigurasi_Ubah_Password
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private sqlstring As String

#Region "Page"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("UserID") = Nothing Then

            FormsAuthentication.SignOut()
            Response.Redirect("index.aspx")

        End If

        If Not Page.IsPostBack Then
            LblUserName.Text = Session("UserId")
        End If
    End Sub

#End Region

#Region "Button"

    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            If Validation_Update() Then
                Call update()
            End If

        Catch ex As Exception
            Response.Write("btSimpan Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Protected Sub btKeluar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btKeluar.Click
        Response.Redirect("~//blank.aspx")
    End Sub

#End Region

#Region "Validation"
    Private Function Validation_Update() As Boolean
        If tbpass.Text.Trim = "" Then
            lInfo.Visible = False
            lInfo.Text = "Password Harus Diisi"
            tbpass.Focus()
            Return False
        End If
        If tbconfirm.Text.Trim = "" Then
            lInfo.Visible = False
            lInfo.Text = "Confirm Password Harus Diisi"
            tbpass.Focus()
            Return False
        End If
        If tbconfirm.Text.Trim <> tbpass.Text.Trim Then
            lInfo.Visible = False
            lInfo.Text = "Confirm Password Tidak cocok"
            tbpass.Focus()
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Methods"

    Private Sub update()

        Try

            sqlstring = " UPDATE " & _
                  "		MasterUser " & _
                  "	    SET " & _
                  "	    Password   = '" & tbpass.Text.Replace("'", "`") & "', " & _
                  "     UserName   = '" & Session("UserId").ToString & "', " & _
                  "	    LastModified = '" & Now.ToString & "' " & _
                  "	    WHERE UserID  ='" & Session("UserId") & "'; "
            SQLExecuteNonQuery(sqlstring)
            linfoberhasil.Visible = True
            linfoberhasil.Text = "Berhasil Merubah Password"
        Catch ex As Exception
            Response.Write("Insert Exception :<br>" & ex.ToString)
        End Try

    End Sub

#End Region
End Class
