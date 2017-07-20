Imports System.Data
Imports System.Data.SqlClient
Partial Class user
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Dim iDT As New DataTable
    Private sqlstring As String
    Private sdr As SqlDataReader
    Private DefaultPassword As String = System.Configuration.ConfigurationManager.AppSettings.Get("DefaultPassword")
#Region " Page "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Pendaftaran User - Logistics Management System"
        ltrInfo.Text = ""

        If Session("userID") = Nothing Then
            FormsAuthentication.SignOut()
            Response.Redirect("login.aspx")
        End If

        Try
            If Not Page.IsPostBack Then
                load_grid()
                hfMode.Value = "Insert"
            End If

            If Not Session("grid_Master_User") Is Nothing Then
                grid_Master_User.DataSource = CType(Session("grid_Master_User"), DataTable)
                grid_Master_User.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try

    End Sub

#End Region

#Region "Validation"

    Private Function Validation() As Boolean

        Try
            If TxtNama.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama harus diisi!</h4></div>"
                TxtNama.Focus()
                Return False
            End If

            If TxtKode.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> User ID harus diisi!</h4></div>"
                TxtKode.Focus()
                Return False
            End If
            Return True

        Catch ex As Exception
            Response.Write("Validation Exception :<br>" & ex.ToString)
        End Try

    End Function

    Private Function Validation_Insert() As Boolean

        Dim result As String

        Try
            If Txtpassword.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Password harus diisi!</h4></div>"
                Txtpassword.Focus()
                Return False
            End If
            If TxtConfirmPassword.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Confirm password harus diisi!</h4></div>"
                Txtpassword.Focus()
                Return False
            End If
            If TxtConfirmPassword.Text.Trim <> Txtpassword.Text.Trim Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Password dan Konfirmasi tidak sesuai!</h4></div>"
                Txtpassword.Focus()
                Return False
            End If

            sqlstring = " SELECT " & _
                  "		userID " & _
                  "	    FROM MasterUser " & _
                  "	    WHERE UserID = '" & TxtKode.Text.ToString & "' " & _
                  "	    AND Status = 1 "
            result = SQLExecuteScalar(sqlstring)
            If result.ToString <> "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> User ID <strong>" & TxtKode.Text & "</strong> sudah ada!</h4></div>"
                TxtKode.Focus()
                Return False
            End If

            Return True

        Catch ex As Exception
            Response.Write("Validation_Insert Exception :<br>" & ex.ToString)
        End Try

    End Function

#End Region

#Region "GridView"

    Private Sub load_grid()
        Try
            sqlstring = "select ID, userID, name, roleID, companyID from masterUser where status = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("userID", GetType(String)))
                .Add(New DataColumn("name", GetType(String)))
                .Add(New DataColumn("companyID", GetType(String)))
            End With

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("userID") = .Item("userID").ToString
                    DR("name") = .Item("name").ToString
                    DR("companyID") = .Item("companyID").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("grid_Master_User") = dt
            grid_Master_User.DataSource = dt
            grid_Master_User.DataBind()

        Catch ex As Exception
            Response.Write("Load_Grid Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Private Sub grid_Master_User_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles grid_Master_User.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    btSimpan.Text = "Update"
                    hiddenid.Value = grid_Master_User.GetRowValues(e.VisibleIndex, "ID").ToString
                    DDLGolongan.SelectedValue = grid_Master_User.GetRowValues(e.VisibleIndex, "roleID").ToString
                    DDLCompany.SelectedValue = grid_Master_User.GetRowValues(e.VisibleIndex, "companyID").ToString
                    TxtKode.Text = grid_Master_User.GetRowValues(e.VisibleIndex, "userID").ToString
                    TxtNama.Text = grid_Master_User.GetRowValues(e.VisibleIndex, "name").ToString
                Case "Delete"
                    Call Delete(grid_Master_User.GetRowValues(e.VisibleIndex, "ID"))
                Case "ResetPassword"
                    Call Reset_Password(grid_Master_User.GetRowValues(e.VisibleIndex, "ID"))

            End Select
        Catch ex As Exception
            Response.Write("error row command: " & ex.ToString)
        End Try
    End Sub
#End Region

#Region " Button "

    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try

            If Validation() Then

                If hfMode.Value = "Insert" Then

                    If Validation_Insert() Then
                        Call insert()
                    End If
                Else
                    Call Update(hiddenid.Value)
                End If

            End If
        Catch ex As Exception
            Response.Write("btSimpan Exception :<br>" & ex.ToString)
        End Try
    End Sub


    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            Call clear()

        Catch ex As Exception
            Response.Write("btBatal Exception :<br>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region " methods "
    Private Sub insert()
        Try
            sqlstring = " INSERT INTO " & _
                 "		masterUser(userID, [Password], name, companyID, roleID) " & _
                 "	VALUES ('" & _
                 TxtKode.Text.ToString & "','" & _
                 Txtpassword.Text.Replace("'", "`") & "','" & _
                 TxtNama.Text.Replace("'", "`") & "','" & _
                 DDLCompany.SelectedValue.ToString & "','" & _
                 DDLGolongan.SelectedValue.ToString & "') "
            
            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                clear()
            End If
        Catch ex As Exception
            Response.Write("Insert Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Private Sub clear()
        Try
            DDLGolongan.Enabled = True
            TxtKode.Text = ""
            TxtKode.Enabled = True
            TxtNama.Text = ""
            Txtpassword.Text = ""
            TxtConfirmPassword.Text = ""
            hiddenid.Value = ""
            lInfo.Text = ""
            lInfo.Visible = False
            Txtpassword.Enabled = True
            TxtConfirmPassword.Enabled = True
            hfMode.Value = "Insert"
            TxtNama.Focus()

        Catch ex As Exception
            Response.Write("Clear Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Private Sub Update(ByVal id As String)
        Try
            If TxtConfirmPassword.Text.Trim <> "" Or Txtpassword.Text.Trim <> "" Then
                If TxtConfirmPassword.Text.Trim <> Txtpassword.Text.Trim Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Password dan Konfirmasi tidak sesuai!</h4></div>"
                    Txtpassword.Focus()
                Else
                    sqlstring = " UPDATE " & _
                          "		masterUser " & _
                          "	    SET " & _
                          "		UserID = '" & TxtKode.Text.Replace("'", "`") & "', " & _
                          "		name  = '" & TxtNama.Text.Replace("'", "`") & "', " & _
                          "		password  = '" & Txtpassword.Text.Replace("'", "`") & "', " & _
                          "		roleID   = '" & DDLGolongan.SelectedValue.ToString & "', " & _
                          "		companyID   = '" & DDLCompany.SelectedValue.ToString & "', " & _
                          "		LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                          "	WHERE ID = '" & id.ToString & "'; "

                    If SQLExecuteNonQuery(sqlstring) > 0 Then
                        load_grid()
                        btSimpan.Text = "Simpan"
                        ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                        clear()
                    End If
                End If
            Else
                sqlstring = " UPDATE " & _
                      "		masterUser " & _
                      "	    SET " & _
                      "		UserID = '" & TxtKode.Text.Replace("'", "`") & "', " & _
                      "		name  = '" & TxtNama.Text.Replace("'", "`") & "', " & _
                      "		roleID   = '" & DDLGolongan.SelectedValue.ToString & "', " & _
                      "		companyID   = '" & DDLCompany.SelectedValue.ToString & "', " & _
                      "		LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                      "	WHERE ID = '" & id.ToString & "'; "

                If SQLExecuteNonQuery(sqlstring) > 0 Then
                    load_grid()
                    btSimpan.Text = "Simpan"
                    ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                    clear()
                End If
            End If
        Catch ex As Exception
            Response.Write("Update Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Private Sub Delete(ByVal ID As String)
        Try

            sqlstring = " UPDATE " & _
                  "		masterUser " & _
                  "	    SET " & _
                  "		lastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                  "		Status = 0 " & _
                  "	WHERE ID = '" & ID.ToString & "'; "
            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                clear()
            End If

        Catch ex As Exception
            Response.Write("Delete Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Private Sub Reset_Password(ByVal ID As String)
        Try
            sqlstring = " UPDATE " & _
                  "		masterUser " & _
                  "	    SET " & _
                  "		LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                  "		Password = '" & DefaultPassword & "' " & _
                  "	WHERE ID = '" & ID.ToString & "'; "
            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Reset Password Berhasil!</h4></div>"
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error Reset password :" & ex.ToString)
        End Try
    End Sub
#End Region


End Class
