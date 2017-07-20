Imports System.Data
Imports System.Data.SqlClient
Partial Class roles
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DS As DataSet
    Private SDR As SqlDataReader
    Private sqlstring As String
    Private sqlstring2 As String
#Region " Page "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.Title = "Konfigurasi Roles - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                Call load_grid()
                hfMode.Value = "Insert"
                Hfroleid.Value = ""
            End If

            If Not Session("grid_MasterRole") Is Nothing Then
                grid_MasterRole.DataSource = CType(Session("grid_MasterRole"), DataTable)
                grid_MasterRole.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try

    End Sub

#End Region

#Region "gridview"
    Private Sub load_grid()
        Try
            sqlstring = " SELECT " & _
         "		ID, " & _
         "		roleID, " & _
         "		name" & _
         "	FROM roles" & _
         "	WHERE [Status] = 1 " & _
         "	ORDER BY roleID "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            Session("grid_masterrole") = DT
            grid_MasterRole.DataSource = DT
            grid_MasterRole.DataBind()

        Catch ex As Exception
            Response.Write("Load_Grid Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Private Sub grid_MasterRole_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles grid_MasterRole.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    btSimpan.Text = "Update"
                    hfID.Value = grid_MasterRole.GetRowValues(e.VisibleIndex, "ID")
                    TxtKode.Text = grid_MasterRole.GetRowValues(e.VisibleIndex, "roleID").ToString
                    Hfroleid.Value = grid_MasterRole.GetRowValues(e.VisibleIndex, "roleID").ToString
                    TxtNama.Text = grid_MasterRole.GetRowValues(e.VisibleIndex, "name").ToString

                Case "Delete"
                    Dim hsl As Integer

                    hsl = CekUsingNumber("masterUser", "roleID", grid_MasterRole.GetRowValues(e.VisibleIndex, "roleID").ToString)

                    If hsl = 0 Then
                        Delete(grid_MasterRole.GetRowValues(e.VisibleIndex, "ID"))
                    Else
                        ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Tidak bisa dihapus, sudah pernah digunakan!</h4></div>"
                    End If
                Case "Detail"
                    Response.Redirect("role-detail.aspx?roleID=" & grid_MasterRole.GetRowValues(e.VisibleIndex, "roleID").ToString)
            End Select
        Catch ex As Exception
            Response.Write("error row command : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "validation"

    Private Function validation() As Boolean
        Try
            If TxtKode.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode harus diisi!</h4></div>"
                TxtKode.Focus()
                Return False
            End If

            If TxtNama.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama harus diisi!</h4></div>"
                TxtNama.Focus()
                Return False
            End If

            Return True


        Catch ex As Exception
            Response.Write("validation exception: <br>" & ex.ToString)
        End Try

    End Function


    Private Function validation_insert() As Boolean

        Dim result As String

        Try
            sqlstring = " SELECT " & _
         "		ID " & _
         "	FROM Roles " & _
         "	WHERE RoleID = '" & TxtKode.Text.Replace("'", "''") & "' " & _
         "	AND Status = 1 "

            result = SQLExecuteScalar(sqlstring)
            If result.ToString <> "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode " & TxtKode.Text & " sudah ada!</h4></div>"
                TxtKode.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Response.Write("Validation_Insert Exception :<br>" & ex.ToString)
        End Try

    End Function

#End Region

#Region " Button "

    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            If validation() Then
                If hfMode.Value = "Insert" Then
                    If validation_insert() Then
                        Call Insert()
                    End If
                Else
                    Call Update(hfID.Value)
                End If
            End If
        Catch ex As Exception
            Response.Write("btSimpan Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            Call Clear()
        Catch ex As Exception
            Response.Write("btBatal Exception :<br>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region " Methods "

    Private Sub Insert()
        Try
            sqlstring = " INSERT INTO " & _
                                    " roles(roleID, name, userID, [status]) " & _
                                    " VALUES ('" & TxtKode.Text.Replace("'", "''") & "','" & TxtNama.Text.Replace("'", "`") & "', '" & Session("userId").ToString & "', 1); "

            sqlstring2 = " INSERT INTO " & _
                      "	rolesDetail(roleID, menuID, active, userID, [status]) " & _
                      "	SELECT '" & TxtKode.Text.Replace("'", "''") & "', menuID, 1, '" & Session("userId").ToString & "', 1 from masterMenu where status = 1" & _
                      " AND menuID NOT IN ( SELECT DISTINCT menuID FROM rolesDetail WHERE roleID = '" & TxtKode.Text.Replace("'", "''") & "')"
            SQLExecuteNonQuery(sqlstring2)

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                Clear()
            End If
        Catch ex As Exception
            Response.Write("Insert Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Private Sub Update(ByVal ID As String)
        Try
            Dim tempRoleID As String
            tempRoleID = SQLExecuteQuery("SELECT roleID FROM roles WHERE ID = '" & ID.ToString & "'").Tables(0).Rows(0).Item("roleID").ToString

            sqlstring = " UPDATE " & _
                  "		roles " & _
                  "	    SET " & _
                  "		roleID = '" & TxtKode.Text.Replace("'", "`") & "', " & _
                  "		name = '" & TxtNama.Text.Replace("'", "`") & "', " & _
                  "	    userID   = '" & Session("userId").ToString & "', " & _
                  "		lastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                  "	WHERE ID = '" & ID.ToString & "'; "

            sqlstring2 = " UPDATE " & _
                  "		rolesDetail " & _
                  "	    SET " & _
                  "		roleID = '" & TxtKode.Text.Replace("'", "`") & "' " & _
                  "	WHERE roleID = '" & tempRoleID & "'; "
            SQLExecuteNonQuery(sqlstring2)

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                btSimpan.Text = "Simpan"
                ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                Clear()
            End If
        Catch ex As Exception
            Response.Write("Update Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Private Sub Delete(ByVal ID As String)
        Try
            sqlstring = " UPDATE " & _
                  "		Roles " & _
                  "	    SET " & _
                  "		userID   = '" & Session("UserId").ToString & "', " & _
                  "		LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                  "		[status] = 0 " & _
                  "	    WHERE ID = '" & ID.ToString & "'; "

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                Clear()
            End If
        Catch ex As Exception
            Response.Write("Delete Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Private Sub Clear()
        Try
            lInfo.Visible = False
            linfoberhasil.Visible = False
            linfoberhasil.Text = ""
            TxtKode.Text = ""
            TxtNama.Text = ""
            hfID.Value = ""
            lInfo.Text = ""

            hfMode.Value = "Insert"
            Hfroleid.Value = ""
        Catch ex As Exception
            Response.Write("Clear Exception :<br>" & ex.ToString)
        End Try
    End Sub

#End Region
End Class
