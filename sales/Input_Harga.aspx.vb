Imports System.Data
Imports System.Data.SqlClient
Partial Class Sales_Input_Harga
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim hasil As Integer
    Dim result As String

#Region "page"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                Call LoadGridHarga()
                tbNamaHarga.Focus()
                hfMode.Value = "Insert"
            End If

            If Not Session("gridview_harga") Is Nothing Then
                GridView_Harga.DataSource = CType(Session("gridview_harga"), DataTable)
                GridView_Harga.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Page Load Exception :<BR>" & ex.ToString)
        End Try

    End Sub
#End Region

#Region "Load Grid"
    Private Sub LoadGridHarga()
        Try
            sqlstring = "SELECT ID, NamaHarga, Harga from MasterHargaDefault WHERE [Status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("namaharga", GetType(String)))
                .Add(New DataColumn("harga", GetType(Double)))
            End With


            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("namaharga") = .Item("namaharga").ToString
                    DR("harga") = .Item("harga").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("gridview_harga") = iDT
            GridView_Harga.DataSource = iDT
            GridView_Harga.DataBind()

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub
#End Region

#Region "Create Session"

    Private Sub createseason()
        'Dim iDT As New DataTable 'untuk data table manipulasi

        With iDT.Columns
            .Add(New DataColumn("ID", GetType(String)))
            .Add(New DataColumn("NamaHarga", GetType(String)))
            .Add(New DataColumn("Harga", GetType(String)))

            Session("gridview_harga") = iDT
            GridView_Harga.DataSource = iDT
            GridView_Harga.KeyFieldName = "ID"
            GridView_Harga.DataBind()
        End With
    End Sub
#End Region

#Region "Validasi"
    Private Function Validasi() As Boolean
        Try
            If hfMode.Value = "Insert" Then
                If tbNamaHarga.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Nama Harus Harga Diisi"
                    tbNamaHarga.Focus()
                    Return False
                End If

                If tbHarga.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Harga Harus diisi"
                    tbHarga.Focus()
                    Return False
                End If

                sqlstring = "SELECT ID FROM MasterHargaDefault " & _
                            "WHERE NamaHarga = '" & tbNamaHarga.Text.Replace("'", "`") & "' " & _
                            "AND Status = 1 "
                result = SQLExecuteScalar(sqlstring)

                If result.ToString <> "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Nama Harga Sudah ada"
                    tbNamaHarga.Focus()
                End If
            End If

            If hfMode.Value = "Update" Then
                If tbNamaHarga.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Nama Harga Harus Diisi"
                    tbNamaHarga.Focus()
                    Return False
                End If

                If tbHarga.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Harga Harus diisi"
                    tbHarga.Focus()
                    Return False
                End If
            End If

            Return True

        Catch ex As Exception
            Response.Write("Validasi Exception :<BR>" & ex.ToString)
        End Try
    End Function
#End Region

#Region "Button"
    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            If Validasi() Then
                If hfMode.Value = "Insert" Then
                    Call insert()
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Simpan Berhasil"

                Else
                    Call Update(hfID.Value)
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Update Berhasil"
                End If
            End If
        Catch ex As Exception
            Response.Write("btSimpan Exception :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            Call Clear()

        Catch ex As Exception
            Response.Write("btBatal Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Protected Sub GridView_Harga_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridView_Harga.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfID.Value = GridView_Harga.GetRowValues(e.VisibleIndex, "ID").ToString
                    tbNamaHarga.Text = GridView_Harga.GetRowValues(e.VisibleIndex, "namaharga").ToString
                    tbHarga.Text = GridView_Harga.GetRowValues(e.VisibleIndex, "harga").ToString

                    hfMode.Value = "Update"
                    Call LoadGridHarga()

                Case "Delete"
                    Dim hsl As Integer

                    hsl = CekUsingNumber("QuotationDetail", "SatuanID", GridView_Harga.GetRowValues(e.VisibleIndex, "ID").ToString)


                    If hsl = 0 Then
                        Call delete(GridView_Harga.GetRowValues(e.VisibleIndex, "ID").ToString())
                    Else
                        lInfo.Visible = True
                        lInfo.Text = "Tidak Dapat Di Delete, Satuan Sudah Pernah Digunakan"
                    End If


            End Select

        Catch ex As Exception
            Response.Write("Row Command Exception :<BR>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "method"
    Private Sub change_harga_global(ByVal ID As String)
        Dim hargaPrevious As Double
        Dim hargaNow As Integer
        Dim hargaChange As Integer

        Try
            sqlstring = "Select Harga from MasterHargaDefault where ID = '" & ID.ToString & "' "
            hargaPrevious = SQLExecuteScalar(sqlstring)
            hargaNow = Integer.Parse(tbHarga.Text.ToString)

            If hargaNow > hargaPrevious Then
                hargaChange = hargaNow - hargaPrevious
                add_quantity(hargaChange)
            ElseIf hargaNow < hargaPrevious Then
                hargaChange = hargaPrevious - hargaNow
                subtract_quantity(hargaChange)
            End If
        Catch ex As Exception
            Throw New Exception("Error change_harga_global function :" & ex.ToString)
        End Try
    End Sub
    Private Sub subtract_quantity(ByVal Quan As Integer)
        Dim sqlstringval As String
        Try
            sqlstringval = " Update QuotationDetail Set Harga = Harga - " & Quan & " "
            result = SQLExecuteNonQuery(sqlstringval)
        Catch ex As Exception
            Throw New Exception("Error Subtract_Quantity function : " & ex.ToString)
        End Try
    End Sub

    Private Sub add_quantity(ByVal Quan As Integer)
        Dim sqlstringval As String
        Try
            sqlstringval = " Update QuotationDetail Set Harga = Harga + " & Quan & " "
            result = SQLExecuteNonQuery(sqlstringval)
        Catch ex As Exception
            Throw New Exception("Error Add_Quantity function : " & ex.ToString)
        End Try
    End Sub
    Private Sub insert()

        Try
            sqlstring = " INSERT INTO MasterHargaDefault " & _
                        " (NamaHarga, Harga, UserName, [Status]) " & _
                        " VALUES('" & tbNamaHarga.Text.Replace("'", "`").ToString & "', " & tbHarga.Text.Replace("'", "`") & ", '" & Session("UserId") & "', 1)"
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Call LoadGridHarga()
                Call Clear()
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Sub Update(ByVal id As String)
        Try
            Call change_harga_global(id.ToString)
            sqlstring = " UPDATE MasterHargaDefault SET " & _
                        " NamaHarga = '" & tbNamaHarga.Text.Replace("'", "`") & "', " & _
                        " Harga = " & tbHarga.Text.Replace("'", "`") & ", " & _
                        " UserName = '" & Session("UserId") & "', " & _
                        " LastModified = '" & Now.ToString & "' " & _
                        " WHERE ID = '" & id.ToString & "' "
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Call LoadGridHarga()
                Call Clear()
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Sub Clear()
        Try
            tbNamaHarga.Text = ""
            tbHarga.Text = ""
            hfID.Value = ""
            hfMode.Value = "Insert"
            lInfo.Text = ""
            lInfo.Visible = False
            linfoberhasil.Visible = False
            linfoberhasil.Text = ""
            tbNamaHarga.Focus()

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Sub delete(ByVal ID As String)
        Try
            sqlstring = " UPDATE MasterHargaDefault SET " & _
                        " UserName = '" & Session("UserId") & "', " & _
                        " LastModified = '" & Now.ToString & "', " & _
                        " [Status] = 0 " & _
                        " WHERE ID = '" & ID.ToString & "' "
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Call LoadGridHarga()
                Call Clear()
            End If
        Catch ex As Exception
            Response.Write("Delete Exception :<BR>" & ex.ToString)
        End Try
    End Sub
#End Region
   
   
End Class
