Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Public Class AsuransiLain
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DS As DataSet
    Private DR As DataRow
    Private sqlstring As String
    Private result As String
    Private hasil As String

#Region "Page"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                TxtHargaAsuransi.Attributes.Add("onkeyup", "changenumberformat('" & TxtHargaAsuransi.ClientID & "')")
                TxtPolis.Attributes.Add("onkeyup", "changenumberformat('" & TxtPolis.ClientID & "')")
                hfMode.Value = "Insert"
                TxtNoAsuransi.Text = Load_N0_Asuransi()
                DtAsuransi.Date = Today
                Create_Session()
                Load_Grid()

            End If

            If Not Session("GridAsuransiLain") Is Nothing Then
                Grid_Asuransi.DataSource = CType(Session("GridAsuransiLain"), DataTable)
                Grid_Asuransi.DataBind()
            End If
        Catch ex As Exception
            Response.Write("<b>Error Page Load :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Validation"

#End Region

#Region "Methods"
    Private Function Load_N0_Asuransi() As String
        Try
            Dim nodpn As String = ""
            Dim pisah() As String
            Dim no As Integer
            Dim bulan As String = Date.Today.ToString("MM")
            Dim value As String = ""

            sqlstring = "SELECT TOP 1 NoAsuransi FROM InvoiceHeader " & _
                        "WHERE [status] = 1 AND NoAsuransi <> '' " & _
                        "ORDER BY ID DESC"
            hasil = SQLExecuteScalar(sqlstring)

            sqlstring = "SELECT TOP 1 NoAsuransi FROM AsuransiLain " & _
                        "WHERE [status] = 1 " & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring)

            If hasil = "" And result = "" Then
                no = 1
                value = "A" & no & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")

            Else

                If hasil <> "" Then
                    pisah = hasil.ToString.Split("/")
                    no = CDbl(pisah(0).Replace("A", "0").Replace("a", "0")) + 1

                    value = "A" & no & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")
                ElseIf result <> "" Then
                    pisah = result.ToString.Split("/")
                    no = CDbl(pisah(0).Replace("A", "0").Replace("a", "0")) + 1

                    value = "A" & no & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")

                ElseIf hasil <> "" And result <> "" Then
                    pisah = result.ToString.Split("/")
                    no = CDbl(pisah(0).Replace("A", "0").Replace("a", "0")) + 1

                    value = "A" & no & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")
                End If



            End If

            Return value

        Catch ex As Exception
            Throw New Exception("<b>Error Load No Asuransi :</b>" & ex.ToString)
        End Try
    End Function

    Private Sub Create_Session()
        Try
            Dim adt As New DataTable

            With adt.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("Tanggal", GetType(String)))
                .Add(New DataColumn("NamaCustomer", GetType(String)))
                .Add(New DataColumn("KodeCustomer", GetType(String)))
                .Add(New DataColumn("Periode", GetType(String)))
                .Add(New DataColumn("NoAsuransi", GetType(String)))
                .Add(New DataColumn("HargaAsuransi", GetType(Double)))
                .Add(New DataColumn("Premi", GetType(String)))
                .Add(New DataColumn("Polis", GetType(Double)))
                .Add(New DataColumn("Discount", GetType(String)))
                .Add(New DataColumn("TotalAsuransi", GetType(Double)))
                .Add(New DataColumn("Keterangan", GetType(String)))
            End With

            Session("GridAsuransiLain") = adt
            Grid_Asuransi.DataSource = adt
            Grid_Asuransi.KeyFieldName = "ID"
            Grid_Asuransi.DataBind()
        Catch ex As Exception
            Throw New Exception("<b>Error Create Session :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Insert()
        Try
            Dim TotalAsuransi As Double

            TotalAsuransi = Hitung_Total_Asuransi()

            sqlstring = "INSERT INTO AsuransiLain(NoAsuransi, Tanggal, KodeCustomer,TglPeriodeAwal, TglPeriodeAkhir, " & _
                        "HargaAsuransi, Premi, Polis,Discount, TotalAsuransi, Keterangan, [status], UserName) " & _
                        "VALUES " & _
                        "('" & TxtNoAsuransi.Text.ToString & "', '" & DtAsuransi.Text & "', " & _
                        " '" & hfCodeCustomer.Value & "', '" & DtPeriodeAwal.Date & "', '" & DtPeriodeAkhir.Date & "',  " & _
                        " " & ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim) & ", '" & TxtPremi.Text.Replace("'", "''").Replace(",", ".").Trim & "', " & _
                        "" & ReplaceString(TxtPolis.Text.Replace("'", "''").Trim) & ", " & zeroifblank(TxtDiscount.Text.ToString.Trim.Replace("'", "''").Replace(",", ".")) & " ," & TotalAsuransi & " , " & _
                        "'" & TxtKetAsuransi.Text.Trim.Replace("'", "''") & "', 1, '" & Session("UserId").ToString & "')"
            result = SQLExecuteNonQuery(sqlstring)

            If result <> 0 Then
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Simpan Berhasil"
                TxtNoAsuransi.Text = Load_N0_Asuransi()
                Clear()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Insert :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Update(ByVal NoAsuransi As String)
        Try
            Dim TotalAsuransi As Double

            TotalAsuransi = Hitung_Total_Asuransi()

            sqlstring = "UPDATE AsuransiLain " & _
                        "SET " & _
                        "Tanggal = '" & DtAsuransi.Date & "', " & _
                        "KodeCustomer = '" & hfCodeCustomer.Value & "', " & _
                        "TglPeriodeAwal = '" & DtPeriodeAwal.Date & "', " & _
                        "TglPeriodeAkhir = '" & DtPeriodeAkhir.Date & "', " & _
                        "HargaAsuransi = " & ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim) & ", " & _
                        "Premi = '" & TxtPremi.Text.Replace("'", "''").Replace(",", ".").Trim & "', " & _
                        "Polis = " & ReplaceString(TxtPolis.Text.Replace("'", "''").Trim) & ", " & _
                        "Discount = " & zeroifblank(TxtDiscount.Text.Replace("'", "''").Replace(",", ".").Trim) & ", " & _
                        "TotalAsuransi = " & TotalAsuransi & ", " & _
                        "Keterangan = '" & TxtKetAsuransi.Text.Replace("'", "''").Trim & "', " & _
                        "UserName = '" & Session("UserId").ToString & "', " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        "WHERE NoAsuransi = '" & NoAsuransi & "' AND [status] = 1 "
            result = SQLExecuteNonQuery(sqlstring)

            If result > 0 Then
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Update Berhasil"
                DtAsuransi.ReadOnly = False
                TxtNoAsuransi.Text = Load_N0_Asuransi()
                Clear()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Update :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Delete(ByVal NoAsuransi As String)
        Try
            sqlstring = "UPDATE AsuransiLain " & _
                        "SET " & _
                        "[status] = 0, " & _
                        "UserName = '" & Session("UserId").ToString & "', " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        "WHERE NoAsuransi = '" & NoAsuransi & "'"
            result = SQLExecuteNonQuery(sqlstring)

            If result > 0 Then
                Load_Grid()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Delete :</b>" & ex.ToString)
        End Try
    End Sub

    Private Function Hitung_Total_Asuransi() As Double
        Try
            Dim TotalAsuransi As Double = 0
            Dim Temptotal As Double = 0

            Temptotal = CDbl(ReplaceString(TxtHargaAsuransi.Text)) * (CDbl(ReplaceString(TxtPremi.Text)) / 100)
            TotalAsuransi = (Temptotal - ((UbahKomaJdTitik(TxtDiscount.Text) / 100) * Temptotal)) + CDbl(ReplaceString(TxtPolis.Text))

            Return TotalAsuransi
        Catch ex As Exception
            Throw New Exception("<b>Error Hitung Total Asuransi :</b>" & ex.ToString)
        End Try
    End Function

    Private Function Validation() As Boolean
        Try
            If hfCodeCustomer.Value = "" Then
                lInfo.Visible = True
                lInfo.Text = "Pilih Nama Customer"
                Return False
            End If

            If DtPeriodeAwal.Text = "" Then

                lInfo.Visible = True
                lInfo.Text = "Masukan Tanggal Periode Awal"
                Return False

            End If

            If DtPeriodeAwal.Text = "" Then

                lInfo.Visible = True
                lInfo.Text = "Masukan Tanggal Periode Akhir"
                Return False

            End If

            If TxtHargaAsuransi.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Masukan Harga Asuransi"
                Return False
            End If

            If TxtPremi.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Masukan Premi Asuransi"
                Return False
            End If

            If TxtPolis.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Masukan Polis Asuransi"
                Return False
            End If

            If IsNumeric(TxtHargaAsuransi.Text.Trim) = False Then
                lInfo.Visible = True
                lInfo.Text = "Harga asuransi harus angka"
                Return False
            End If

            If IsNumeric(TxtPolis.Text.Trim) = False Then
                lInfo.Visible = True
                lInfo.Text = "Harga polis harus angka"
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("<b>Error Validasi :</b>" & ex.ToString)
        End Try
    End Function

    Private Sub Clear()
        hfCodeCustomer.Value = ""
        hfNamaCustomer.Value = ""
        TxtCustName.Text = ""
        DtPeriodeAwal.Text = ""
        DtPeriodeAkhir.Text = ""
        TxtDiscount.Text = ""
        TxtKetAsuransi.Text = ""
        TxtHargaAsuransi.Text = ""
        TxtPremi.Text = ""
        TxtPolis.Text = ""
        hfMode.Value = "Insert"
    End Sub

    Private Sub clearlabel()
        lInfo.Visible = False
        lInfo.Text = ""
        linfoberhasil.Visible = False
        linfoberhasil.Text = ""
    End Sub
#End Region

#Region "Button"
    Private Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            If hfMode.Value = "Insert" Then
                If Validation() Then
                    Insert()
                    Load_Grid()
                End If
            Else
                If Validation() Then
                    Update(hfNoAsuransi.Value)
                    Load_Grid()
                End If
            End If
        Catch ex As Exception
            Response.Write("<b>Error BtnSimpan :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Clear()
        clearlabel()
        Session("GridAsuransiLain") = Nothing
    End Sub

#End Region

#Region "Grid"
    Private Sub Load_Grid()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("Tanggal", GetType(DateTime)))
                .Add(New DataColumn("NamaCustomer", GetType(String)))
                .Add(New DataColumn("KodeCustomer", GetType(String)))
                .Add(New DataColumn("Periode", GetType(String)))
                .Add(New DataColumn("NoAsuransi", GetType(String)))
                .Add(New DataColumn("HargaAsuransi", GetType(Double)))
                .Add(New DataColumn("Premi", GetType(String)))
                .Add(New DataColumn("Polis", GetType(Double)))
                .Add(New DataColumn("Discount", GetType(String)))
                .Add(New DataColumn("TotalAsuransi", GetType(Double)))
                .Add(New DataColumn("Keterangan", GetType(String)))
            End With

            sqlstring = "SELECT AL.ID, AL.Tanggal, MC.Nama_Customer, AL.KodeCustomer, AL.TglPeriodeAwal, AL.TglPeriodeAkhir, AL.NoAsuransi, AL.HargaAsuransi, " & _
                        "AL.Premi, AL.Polis,Discount, AL.TotalAsuransi, AL.Keterangan " & _
                        "FROM AsuransiLain AL " & _
                        "JOIN MasterCustomer MC ON AL.KodeCustomer = Kode_Customer " & _
                        "WHERE AL.[status] = 1 AND MC.[status] <> 0 " & _
                        "ORDER BY ID"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("Tanggal") = CDate(.Item("Tanggal").ToString).ToString("dd MMMM yyyy")
                        DR("NamaCustomer") = .Item("Nama_Customer").ToString
                        DR("KodeCustomer") = .Item("KodeCustomer").ToString
                        DR("Periode") = CDate(.Item("TglPeriodeAwal").ToString).ToString("dd MMMM yyyy") & " s/d " & CDate(.Item("TglPeriodeAkhir").ToString).ToString("dd MMMM yyyy")
                        DR("NoAsuransi") = .Item("NoAsuransi").ToString
                        DR("HargaAsuransi") = CDbl(.Item("HargaAsuransi").ToString)
                        DR("Premi") = .Item("Premi").ToString
                        DR("Polis") = CDbl(.Item("Polis").ToString)
                        DR("Discount") = .Item("Discount").ToString
                        DR("TotalAsuransi") = CDbl(.Item("TotalAsuransi").ToString)
                        DR("Keterangan") = .Item("Keterangan").ToString
                    End With
                    iDT.Rows.Add(DR)
                Next

                Session("GridAsuransiLain") = iDT
                Grid_Asuransi.DataSource = iDT
                Grid_Asuransi.KeyFieldName = "ID"
                Grid_Asuransi.DataBind()
            Else
                Grid_Asuransi.DataSource = Nothing
                Grid_Asuransi.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Load Grid :</b>" & ex.ToString)
        End Try
    End Sub


    Private Sub Grid_Asuransi_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Asuransi.RowCommand
        Try
            clearlabel()
            Dim pisah() As String
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    hfCodeCustomer.Value = Grid_Asuransi.GetRowValues(e.VisibleIndex, "KodeCustomer").ToString
                    hfNamaCustomer.Value = Grid_Asuransi.GetRowValues(e.VisibleIndex, "NamaCustomer").ToString
                    TxtCustName.Text = Grid_Asuransi.GetRowValues(e.VisibleIndex, "NamaCustomer").ToString
                    DtAsuransi.Date = CDate(Grid_Asuransi.GetRowValues(e.VisibleIndex, "Tanggal").ToString)
                    pisah = Grid_Asuransi.GetRowValues(e.VisibleIndex, "Periode").ToString.Replace("s/d", "-").Split("-")
                    DtPeriodeAwal.Date = CDate(pisah(0))
                    DtPeriodeAkhir.Date = CDate(pisah(1))

                    hfNoAsuransi.Value = Grid_Asuransi.GetRowValues(e.VisibleIndex, "NoAsuransi").ToString
                    TxtNoAsuransi.Text = Grid_Asuransi.GetRowValues(e.VisibleIndex, "NoAsuransi").ToString
                    TxtHargaAsuransi.Text = UbahKoma(Grid_Asuransi.GetRowValues(e.VisibleIndex, "HargaAsuransi").ToString)
                    TxtPremi.Text = Grid_Asuransi.GetRowValues(e.VisibleIndex, "Premi").ToString
                    TxtPolis.Text = UbahKoma(Grid_Asuransi.GetRowValues(e.VisibleIndex, "Polis").ToString)
                    TxtDiscount.Text = Grid_Asuransi.GetRowValues(e.VisibleIndex, "Discount").ToString
                    TxtKetAsuransi.Text = Grid_Asuransi.GetRowValues(e.VisibleIndex, "Keterangan").ToString

                Case "Delete"
                    Delete(Grid_Asuransi.GetRowValues(e.VisibleIndex, "NoAsuransi").ToString)
            End Select
        Catch ex As Exception
            Throw New Exception("<b>Error Row Command :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

End Class