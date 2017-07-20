Imports System.Data
Imports System.Data.SqlClient
Partial Public Class Master_Satuan
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim hasil As Integer
    Dim result As String

#Region "Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                Load_Grid_Satuan()
                hfMode.Value = "Insert"
            End If

            If Not Session("GridViewSatuanOther") Is Nothing Then
                GridView_satuanother.DataSource = CType(Session("GridViewSatuanOther"), DataTable)
                GridView_satuanother.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Page Load : " & ex.ToString)
        End Try
    End Sub
#End Region
    
#Region "Button"
    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSimpan.Click
        Try
            If Validation() Then
                If hfMode.Value = "Insert" Then
                    Insert()
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Simpan Berhasil"

                Else
                    Update(hfID.Value)
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Update Berhasil"

                End If
            End If
        Catch ex As Exception
            Response.Write("Error button save : " & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBatal.Click
        Try
            clear()
        Catch ex As Exception
            Response.Write("Error button Batal " & ex.ToString)
        End Try
    End Sub

    Private Sub GridView_satuanother_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridView_satuanother.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    hfID.Value = GridView_satuanother.GetRowValues(e.VisibleIndex, "ID").ToString
                    tbsatuan.Text = GridView_satuanother.GetRowValues(e.VisibleIndex, "namasatuan").ToString

                Case "Delete"
                    Dim hsl As Integer

                    hsl = CekUsingNumber("WarehouseDetail", "Others", GridView_satuanother.GetRowValues(e.VisibleIndex, "ID").ToString)


                    If hsl = 0 Then
                        delete(GridView_satuanother.GetRowValues(e.VisibleIndex, "ID").ToString)
                    Else
                        lInfo.Visible = True
                        lInfo.Text = "Tidak Dapat Di Delete, Satuan Sudah Pernah Digunakan"
                    End If

            End Select
        Catch ex As Exception
            Response.Write("Error Row Command : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Methods"
    Private Sub Insert()
        Dim iddetail As Integer
        Dim cekstring As String
        Dim cekdt As DataTable

        Try
            cekstring = "select id from MasterSatuanOther where status = 1 "
            cekdt = SQLExecuteQuery(cekstring).Tables(0)
            If cekdt.Rows.Count > 0 Then
                iddetail = getDetailIDMaster("MasterSatuanOther") + 1
            Else
                iddetail = 1
            End If
            sqlstring = " INSERT INTO MasterSatuanOther " & _
                        " (IDDetail,Nama_Satuan, UserName, [status]) VALUES " & _
                        " ( " & iddetail & ",'" & tbsatuan.Text.Replace("'", "''") & "', '" & Session("UserId") & "', 1)"
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Load_Grid_Satuan()
                clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Method insert : " & ex.ToString)
        End Try

    End Sub

    Private Sub Update(ByVal id As String)
        Try
            sqlstring = " UPDATE MasterSatuanOther " & _
                        " SET Nama_Satuan = '" & tbsatuan.Text.Replace("'", "''") & "', " & _
                        " UserName = '" & Session("UserId") & "', " & _
                        " LastModified = '" & Now.ToString & "' " & _
                        " WHERE IDDetail = " & id.ToString & " and status <>0"
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Load_Grid_Satuan()
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error function Update : " & ex.ToString)
        End Try
    End Sub

    Private Sub delete(ByVal id As String)
        Try
            sqlstring = "UPDATE MasterSatuanOther " & _
                        "SET UserName = '" & Session("UserId") & "', " & _
                        "LastModified = '" & Now.ToString & "', " & _
                        "[status] = 0 " & _
                        "WHERE IDDetail = " & id.ToString & " and status <>0"
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Load_Grid_Satuan()
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error function delete : " & ex.ToString)
        End Try
    End Sub

    Private Sub clear()
        Try
            tbsatuan.Text = ""
            hfMode.Value = "Insert"
            lInfo.Visible = False
            lInfo.Text = ""
            linfoberhasil.Visible = False
            linfoberhasil.Text = ""
            hfID.Value = ""
            tbsatuan.Focus()
        Catch ex As Exception
            Throw New Exception("Error Function Clear : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Load Grid"
    Private Sub Load_Grid_Satuan()
        Try
            sqlstring = "SELECT IDDetail, Nama_Satuan from MasterSatuanOther where [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)


            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("namasatuan", GetType(String)))
            End With

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("IDDetail").ToString
                    DR("namasatuan") = .Item("Nama_Satuan").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("GridViewSatuanOther") = iDT
            GridView_satuanother.DataSource = iDT
            GridView_satuanother.DataBind()


        Catch ex As Exception
            Throw New Exception("Error load grid satuan : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Create Session"
    Private Sub createseason()
        'Dim iDT As New DataTable 'untuk data table manipulasi

        With iDT.Columns
            .Add(New DataColumn("ID", GetType(String)))
            .Add(New DataColumn("namasatuan", GetType(String)))

            Session("gridview_harga") = iDT
            GridView_satuanother.DataSource = iDT
            GridView_satuanother.KeyFieldName = "ID"
            GridView_satuanother.DataBind()
        End With
    End Sub
#End Region

#Region "Validasi"
    Private Function Validation() As Boolean
        Try
            If hfMode.Value = "Insert" Then
                If tbsatuan.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Nama satuan harus di isi"
                    Return False
                End If

                sqlstring = "SELECT ID From MasterSatuanOther WHERE Nama_Satuan = '" & tbsatuan.Text.Replace("'", "''") & "' AND [status] = 1"
                result = SQLExecuteScalar(sqlstring)

                If result.ToString <> "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Nama satuan Sudah ada"
                    tbsatuan.Focus()
                    Return False
                End If

            End If

            If hfMode.Value = "Update" Then
                If tbsatuan.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Nama satuan harus diisi"
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("Error Function Validasi : " & ex.ToString)
        End Try
    End Function
#End Region

End Class