Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Public Class nominal
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DS As DataSet
    Private DR As DataRow
    Private aDT As New DataTable
    Private sqlstring As String
    Private result As String
    Private hasil As String

#Region "Page"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.Title = "Master Nominal - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                hfMode.Value = "Insert"
                Create_Session()
                Load_Grid()
                TxtNominal.Attributes.Add("onkeyup", "changenumberformat('" & TxtNominal.ClientID & "')")
            End If

            If Not Session("GridJenis") Is Nothing Then
                Grid_Jenis.DataSource = CType(Session("GridJenis"), DataTable)
                Grid_Jenis.DataBind()
            End If
        Catch ex As Exception
            Response.Write("<b>Error Page Load :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Grid"

    Private Sub Load_Grid()
        Try


            With aDT.Columns
                .Add(New DataColumn("IDMasterRangeNominal", GetType(String)))
                .Add(New DataColumn("Nominal", GetType(Double)))

            End With


            sqlstring = "SELECT IDMasterRangeNominal, Nominal FROM MasterRangeNominal WHERE [status] = 1 ORDER BY ID "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = aDT.NewRow
                        DR("IDMasterRangeNominal") = .Item("IDMasterRangeNominal").ToString
                        DR("Nominal") = CDbl(.Item("Nominal").ToString)
                        aDT.Rows.Add(DR)
                    End With
                Next

                Session("GridJenis") = aDT
                Grid_Jenis.DataSource = aDT
                Grid_Jenis.KeyFieldName = "IDMasterRangeNominal"
                Grid_Jenis.DataBind()
            Else
                Grid_Jenis.DataSource = Nothing
                Grid_Jenis.DataBind()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error Load Grid :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_Jenis_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Jenis.RowCommand
        Try
            ClearLabel()

            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    btSimpan.Text = "Update"
                    hfMode.Value = "Update"
                    TxtNominal.Text = Grid_Jenis.GetRowValues(e.VisibleIndex, "Nominal").ToString
                    hfID.Value = Grid_Jenis.GetRowValues(e.VisibleIndex, "IDMasterRangeNominal").ToString
                Case "Delete"
                    Delete(Grid_Jenis.GetRowValues(e.VisibleIndex, "IDMasterRangeNominal").ToString)
            End Select
        Catch ex As Exception
            Response.Write("<b>Error GridRowCommand :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "Methods"

    Private Sub Create_Session()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))

                .Add(New DataColumn("Nominal", GetType(String)))

            End With

            Session("GridJenis") = iDT
            Grid_Jenis.DataSource = iDT
            Grid_Jenis.KeyFieldName = "ID"
            Grid_Jenis.DataBind()
        Catch ex As Exception
            Throw New Exception("<b>Error Create Session :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Insert()
        Try
            sqlstring = "SELECT ID FROM MasterRangeNominal WHERE [status] = 1 "
            DT = SQLExecuteQuery(sqlstring).Tables(0)

            If DT.Rows.Count > 0 Then
                sqlstring = "UPDATE MasterRangeNominal SET Nominal = " & ReplaceString(TxtNominal.Text.Replace("'", "''").Trim) & " "
            Else
                sqlstring = "INSERT INTO MasterRangeNominal(IDMasterRangeNominal, Nominal, UserName, [status]) " & _
                            "VALUES " & _
                            "(" & MakeIDTable("MasterRangeNominal", "IDMasterRangeNominal") & " , '" & ReplaceString(TxtNominal.Text.Replace("'", "''").Trim) & "', '" & Session("UserId").ToString & "', 1)"
            End If

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                Clear()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Insert :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Update(ByVal ID As String)
        Try
            sqlstring = "UPDATE MasterRangeNominal SET " & _
                        "Nominal = '" & ReplaceString(TxtNominal.Text.Replace("'", "''").Trim) & "', " & _
                        "LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        "UserName = '" & Session("UserId").ToString & "' " & _
                        "WHERE IDMasterRangeNominal = " & ID.ToString & " " & _
                        "AND [status] = 1"
            
            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                btSimpan.Text = "Simpan"
                ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                Clear()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Update :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Delete(ByVal ID As String)
        Try
            sqlstring = "UPDATE MasterRangeNominal SET " & _
                        "[status] = 0, " & _
                        "LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        "UserName = '" & Session("UserId").ToString & "' " & _
                        "WHERE IDMasterRangeNominal = " & ID.ToString & " "
            
            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                Clear()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Update :</b>" & ex.ToString)
        End Try
    End Sub

    Private Function Validation() As Boolean
        If TxtNominal.Text.Trim = "" Then
            ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Masukkan Nominal Range!</h4></div>"
            Return False
        End If

        If IsNumeric(TxtNominal.Text.Trim) = False Then
            ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nominal Harus Angka!</h4></div>"
            Return False
        End If

        Return True
    End Function

    Private Sub Clear()
        TxtNominal.Text = ""
        hfMode.Value = "Insert"
    End Sub

    Private Sub ClearLabel()
        linfo.Text = ""
        linfo.Visible = False
        linfoberhasil.Text = ""
        linfoberhasil.Visible = False
    End Sub
#End Region

#Region "Button"
    Private Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            If Validation() Then
                If hfMode.Value = "Insert" Then
                    Insert()
                Else
                    Update(hfID.Value)
                End If
            End If
        Catch ex As Exception
            Throw New Exception("<b>error Btsimpan :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Clear()
        ClearLabel()
    End Sub
#End Region


End Class