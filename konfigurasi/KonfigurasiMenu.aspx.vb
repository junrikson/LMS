Imports System.Data
Imports System.Data.SqlClient
Partial Public Class KonfigurasiMenu
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Dim hasil As Integer
    Dim result As String = ""

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Session("namaroles").ToString.Contains("Account") = False Then
                Response.Redirect("../NoRoles.aspx")
            End If

            If Not Page.IsPostBack Then
                hfMode.Value = "Insert"
                'LoadDDL()
                Create_Session()
                Load_Grid()
            End If

            If Not Session("KonfMenu") Is Nothing Then
                GridKonfigurasiMenu.DataSource = CType(Session("KonfMenu"), DataTable)
                GridKonfigurasiMenu.DataBind()
            End If
        Catch ex As Exception
            Response.Write("<b>Error Page Load :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "GRID"
    Private Sub Load_Grid()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("Parent", GetType(String)))
                .Add(New DataColumn("Child", GetType(String)))
                .Add(New DataColumn("URL", GetType(String)))
            End With

            sqlstring = "SELECT ID, Parent, Child, URL FROM MasterMenu " & _
                        "WHERE [status] = 1 ORDER BY Parent"
            DT = SQLExecuteQuery(sqlstring).Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow
                        DR("ID") = .Item("ID")
                        DR("Parent") = .Item("Parent")
                        DR("Child") = .Item("Child")
                        DR("URL") = .Item("URL")
                        iDT.Rows.Add(DR)
                    End With

                Next

                Session("KonfMenu") = iDT
                GridKonfigurasiMenu.DataSource = iDT
                GridKonfigurasiMenu.KeyFieldName = "ID"
                GridKonfigurasiMenu.DataBind()
            Else
                GridKonfigurasiMenu.DataSource = Nothing
                GridKonfigurasiMenu.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Load Grid :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "BUTTON"
    Private Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            Clear_Label()
            If hfMode.Value = "Insert" Then
                If Validation_Insert() Then
                    Insert()
                    Clear()
                End If

            Else
                If Validation_Update() Then
                    Update(hfID.Value)
                    Clear()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Btn Simpan :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            Clear()
            Clear_Label()
        Catch ex As Exception
            Throw New Exception("<b>Error Btn batal :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub GridKonfigurasiMenu_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridKonfigurasiMenu.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    hfID.Value = GridKonfigurasiMenu.GetRowValues(e.VisibleIndex, "ID").ToString
                    DDLParent.SelectedValue = GridKonfigurasiMenu.GetRowValues(e.VisibleIndex, "Parent").ToString
                    TxtChild.Text = GridKonfigurasiMenu.GetRowValues(e.VisibleIndex, "Child").ToString
                    TxtUrl.Text = GridKonfigurasiMenu.GetRowValues(e.VisibleIndex, "URL").ToString

                Case "Delete"
                    Delete(GridKonfigurasiMenu.GetRowValues(e.VisibleIndex, "ID").ToString)
            End Select
        Catch ex As Exception
            Throw New Exception("<b>error Row Command : </b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "METHODS"
    Private Sub Create_Session()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("Parent", GetType(String)))
                .Add(New DataColumn("Child", GetType(String)))
                .Add(New DataColumn("URL", GetType(String)))
            End With

            Session("KonfMenu") = iDT
            GridKonfigurasiMenu.DataSource = iDT
            GridKonfigurasiMenu.KeyFieldName = "ID"
            GridKonfigurasiMenu.DataBind()
        Catch ex As Exception
            Throw New Exception("<b>Error Create Session :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Insert()
        Try
            sqlstring = "INSERT INTO MasterMenu(Parent, Child, url, Target, [status]) VALUES " & _
                        "('" & DDLParent.SelectedValue & "', '" & TxtChild.Text.Trim.Replace("'", "''") & "', " & _
                        "'" & TxtUrl.Text.Trim.Replace("'", "''") & "', 'content', 1)"
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                linfoberhasil.Text = "Simpan Berhasil"
                linfoberhasil.Visible = True
                Load_Grid()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Inser :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Update(ByVal ID As Integer)
        Try
            sqlstring = "UPDATE MasterMenu " & _
                        "SET " & _
                        "Parent = '" & DDLParent.SelectedValue & "', " & _
                        "Child = '" & TxtChild.Text.Replace("'", "''").Trim & "', " & _
                        "url = '" & TxtUrl.Text.Replace("'", "''").Trim & "', " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        "WHERE ID = " & ID & ""
            hasil = SQLExecuteNonQuery(sqlstring)

            'Response.Write(sqlstring)

            If hasil > 0 Then
                linfoberhasil.Text = "Update Berhasil"
                linfoberhasil.Visible = True
                Load_Grid()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Update :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Delete(ByVal ID As String)
        Try
            sqlstring = "UPDATE MasterMenu " & _
                        "SET " & _
                        "[status] = 0, " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        "WHERE ID = " & ID & ""
            If hasil > 0 Then
                linfoberhasil.Text = "Delete Berhasil"
                linfoberhasil.Visible = True
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Delete : </b>" & ex.ToString)
        End Try
    End Sub

    Private Function Validation_Insert() As Boolean
        Try
            sqlstring = "SELECT ID FROM MasterMenu " & _
                        "WHERE " & _
                        "Parent = '" & DDLParent.SelectedValue & "' AND " & _
                        "Child = '" & TxtChild.Text.Trim & "' AND " & _
                        "URL = '" & TxtUrl.Text.Trim & "' AND [status] = 1"
            result = SQLExecuteScalar(sqlstring)

            If result <> "" Then
                lInfo.Visible = True
                lInfo.Text = "Data sudah ada"
                Return False
            End If

            If DDLParent.SelectedIndex = 0 Then
                lInfo.Visible = True
                lInfo.Text = "Pilih Parent"
                Return False
            End If

            If TxtChild.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Child belum diisi "
                Return False
            End If

            If TxtUrl.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "URL belum diisi "
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("<b>Error Validation Insert :<b>" & ex.ToString)
        End Try
    End Function

    Private Function Validation_Update() As Boolean
        Try
            If DDLParent.SelectedIndex = 0 Then
                lInfo.Visible = True
                lInfo.Text = "Pilih Parent"
                Return False
            End If

            If TxtChild.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Child belum diisi "
                Return False
            End If

            If TxtUrl.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "URL belum diisi "
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("<b>Error Validation update :</b>" & ex.ToString)
        End Try
    End Function

    Private Sub Clear()
        Try
            DDLParent.SelectedIndex = 0
            TxtChild.Text = ""
            TxtUrl.Text = ""
            hfMode.Value = "Insert"
        Catch ex As Exception
            Throw New Exception("<b>Error Clear :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Clear_Label()
        Try
            lInfo.Text = ""
            lInfo.Visible = False
            linfoberhasil.Text = ""
            linfoberhasil.Visible = False
        Catch ex As Exception
            Throw New Exception("<b>Error Clear label :</b>" & ex.ToString)
        End Try
    End Sub

    'Private Sub LoadDDL()
    '    Try
    '        sqlstring = "SELECT Distinct Parent FROM MasterMenu WHERE [status] = 1"
    '        Dim dt As DataTable = SQLExecuteQuery(sqlstring).Tables(0)
    '        With DDLParent
    '            .DataSource = dt
    '            .DataTextField = "Parent"
    '            .DataValueField = "Parent"
    '            .DataBind()
    '        End With
    '        DDLParent.Items.Insert(0, "Pilih Parent")
    '        DDLParent.Items.Item(0).Value = "Pilih Parent"
    '    Catch ex As Exception
    '        Throw New Exception("<b>Error Load DDL :</b>" & ex.ToString)
    '    End Try
    'End Sub
#End Region

End Class