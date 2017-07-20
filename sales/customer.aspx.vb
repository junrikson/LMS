Imports System.Data
Imports System.Data.SqlClient
Partial Class customer
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Private iDT As New DataTable
    Dim hasil As Integer
    Dim result As String

#Region "PAGE LOAD"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.Title = "Input Customer - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            Page.Header.Controls.Add(
                New LiteralControl(
"    <script type=""text/javascript"" language=""javascript"">" & _
"        function OnGridFocusedRowChanged() {" & _
"            grid.GetRowValues(grid.GetFocusedRowIndex(), 'ID;CustomerID;NamaBarang;Berat;Lebar;Tinggi', OnGetRowValues);" & _
"        }" & _
"        function OnGetRowValues(values) {" & _
"            if (values[1] == null) values[1] = '';" & _
"            var id = document.getElementById(""hfID"");" & _
"            id.value = values[0];" & _
"        }" & _
"        function getTujuan() {" & _
"            returnValue = ShowDialog2('Tujuan', 'Arg', '610', '450');" & _
"            if (returnValue) {" & _
"                var comp = new Array();" & _
"                comp = returnValue.split("";"");" & _
"                var Name = document.getElementById(""TxtKotaDitunjukan"");" & _
"                Name.value = comp[1];" & _
"            }" & _
"        }" & _
"</script>"
                )
            )

            If Not Page.IsPostBack Then
                Panel2.Visible = False
                hfMode.Value = "Insert"
                Date_Tgl.Date = Today
                Call load_gridapprove()
                LblKodeCustomer.Visible = True
                LblKodeCustomer.Text = Load_Kode_Customer()
                CbJenisPerusahaan.Visible = False
                LoadDDLKOTA()
            End If

            If Not Session("grid_customerapprove") Is Nothing Then
                GridView_CustomerApprove.DataSource = CType(Session("grid_customerapprove"), DataTable)
                GridView_CustomerApprove.DataBind()
            End If


        Catch ex As Exception
            Response.Write("Error Page Load :<BR>" & ex.ToString)
        End Try
    End Sub


    Private Function Load_Kode_Customer() As String
        Dim month, year, tanggal As String
        Dim value As String
        Dim no As Integer

        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 Kode_Customer FROM MasterCustomer " & _
                        "WHERE Kode_Customer LIKE 'CS/" & year.ToString & month.ToString & tanggal.ToString & "%' and status <> 0" & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring)

            If result.ToString <> "" Then
                no = CDbl(Right(result, 4)) + 1
            Else
                no = 1
            End If
            value = "CS/" & year.ToString & month.ToString & tanggal.ToString & no.ToString("0000")




            Return value.ToString
        Catch ex As Exception
            Throw New Exception("Error Load_Kode_Customer :<BR>" & ex.ToString)
        End Try

    End Function

#End Region

#Region "VALIDASI"

    Private Function validation_input() As Boolean
        Try
            If DDLTipeCustomer.SelectedIndex = 0 Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Tipe Customer belum diisi!</h4></div>"
                DDLTipeCustomer.Focus()
                Return False
            End If

            If DDLTipeCustomer.SelectedValue <> "Perusahaan" Then
                If cb_JenisKelamin.SelectedValue = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Jenis kelamin customer belum diisi!</h4></div>"
                    Return False
                End If

            End If

            If TxtNamaCustomer.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama belum diisi!</h4></div>"
                TxtNamaCustomer.Focus()
                Return False
            End If

            If TxtNamaCustomer.Text.Trim Like "*-*" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama tidak boleh menggunakan -</h4></div>"
                TxtNamaCustomer.Focus()
                Return False
            End If

            If TxtAlamat.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Alamat belum diisi!</h4></div>"
                TxtAlamat.Focus()
                Return False
            End If

            If TxtArea.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kota Customer belum diisi!</h4></div>"
                TxtArea.Focus()
                Return False
            End If

            If DDLKotaASAlBarang.SelectedIndex = 0 Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Daerah asal barang belum diisi!</h4></div>"
                Return False
            End If

            'If TxtArea.Text.Trim = "" Then
            '    lInfo.Visible = True
            '    lInfo.Text = "Area Daerah Belum diisi"
            '    TxtArea.Focus()
            '    Return False
            'End If

            'If TxtKotaDitunjukan.Text.Trim = "" Then
            '    lInfo.Visible = True
            '    lInfo.Text = "Kota di tunjukan Belum diisi"
            '    TxtKotaDitunjukan.Focus()
            '    Return False
            'End If

            'If IsNumeric(TxtKotaDitunjukan.Text.Trim) = True Then
            '    lInfo.Visible = True
            '    lInfo.Text = "Nama Kota Harus Huruf"
            '    TxtKotaDitunjukan.Focus()
            '    Return False
            'End If

            If txtnoarea1.Text.Replace(" ", "").Trim <> "" Then
                If IsNumeric(txtnoarea1.Text.Replace(" ", "").Trim) = False Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode area No Telp1 harus berupa angka!</h4></div>"
                    txtnoarea1.Focus()
                    Return False
                End If
            End If

            If TxtNoTelp1.Text.Trim <> "" Then
                If IsNumeric(TxtNoTelp1.Text.Replace(" ", "").Trim) = False Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> No Telp1 harus berupa angka!</h4></div>"
                    TxtNoTelp1.Focus()
                    Return False
                End If

                If txtnoarea1.Text.Replace(" ", "").Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Area No Telp1 harus diisi apabila ingin mengisi No Telp1!</h4></div>"
                    txtnoarea1.Focus()
                    Return False
                End If
            End If


            If Txtnoarea2.Text.Replace(" ", "").Trim <> "" Then
                If IsNumeric(Txtnoarea2.Text.Replace(" ", "").Trim) = False Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode Area No Telp1 harus berupa angka!</h4></div>"
                    TxtNoTelp1.Focus()
                    Return False
                End If

            End If

            If TxtNoTelp2.Text.Trim <> "" Then
                If IsNumeric(TxtNoTelp2.Text) = False Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode Area No Telp2 harus berupa angka!</h4></div>"
                    TxtNoTelp2.Focus()
                    Return False
                End If

                If Txtnoarea2.Text.Replace(" ", "").Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Area No Telp2 harus diisi apabila ingin mengisi Nomor Telp2!</h4></div>"
                    Txtnoarea2.Focus()
                    Return False
                End If
            End If

            If TxtAreaNoHP.Text.Replace(" ", "").Trim <> "" Then
                If IsNumeric(TxtAreaNoHP.Text.Replace(" ", "").Replace("'", "''").Trim) = False Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode handphone harus berupa angka!</h4></div>"
                    TxtAreaNoHP.Focus()
                    Return False
                End If
            End If

            If TxtNoHP.Text.Trim <> "" Then
                If IsNumeric(TxtNoHP.Text) = False Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> No Handphone harus berupa angka!</h4></div>"
                    TxtNoHP.Focus()
                    Return False
                End If


                If TxtAreaNoHP.Text.Replace(" ", "").Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Area No Handphone harus diisi apabila anda mau mengisi No handphone!</h4></div>"
                    TxtAreaNoHP.Focus()
                    Return False
                End If
            End If

            If TxtAreaFax.Text.Replace(" ", "").Trim <> "" Then
                If IsNumeric(TxtAreaFax.Text.Replace(" ", "").Trim) = False Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode Area No Fax harus berupa angka!</h4></div>"
                    TxtAreaFax.Focus()
                    Return False
                End If
            End If

            If TxtFax.Text.Trim <> "" Then
                If IsNumeric(TxtFax.Text) = False Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Fax harus berupa angka!</h4></div>"
                    TxtFax.Focus()
                    Return False
                End If
            End If


            If TxtNoTelp1.Text.Trim = "" And TxtNoTelp2.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> No Telp belum diisi!</h4></div>"
                TxtNoTelp1.Focus()
                Return False
            End If


            'If TxtNoTelp1.Text.Trim = "" Then
            '    TxtNoTelp1.Text = "0"
            'End If

            'If TxtNoTelp2.Text.Trim = "" Then
            '    TxtNoTelp2.Text = "0"
            'End If

            'If TxtNoHP.Text.Trim = "" Then
            '    TxtNoHP.Text = "0"
            'End If

            'If Not IsNumeric(TxtNoTelp1.Text.ToString) Then
            '    lInfo.Visible = True
            '    lInfo.Text = "No Telp harus angka"
            '    TxtNoTelp1.Focus()
            '    Return False
            'End If

            'If Not IsNumeric(TxtNoTelp2.Text.ToString) Then
            '    lInfo.Visible = True
            '    lInfo.Text = "No Telp harus angka"
            '    TxtNoTelp1.Focus()
            '    Return False
            'End If

            'If Not IsNumeric(TxtNoHP.Text.ToString) Then
            '    lInfo.Visible = True
            '    lInfo.Text = "No Telp harus angka"
            '    TxtNoTelp1.Focus()
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Response.Write("Error Validation Input :<BR>" & ex.ToString)
        End Try
    End Function

    Private Function validation_insert() As Boolean
        Try
            sqlstring = "SELECT Nama_Customer " & _
                        "FROM MasterCustomer " & _
                        "WHERE " & _
                        "(Kode_Area_Telp1 + No_Telp1 = '" & txtnoarea1.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' + '" & TxtNoTelp1.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' OR " & _
                        "Kode_Area_Telp1 + No_Telp1 = '" & txtnoarea1.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' + '" & TxtNoTelp2.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' OR " & _
                        "Kode_Area_Telp1 + No_Telp1 = '" & TxtAreaNoHP.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "'  + '" & TxtNoHP.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' OR " & _
                        "Kode_Area_Telp2 + No_Telp2 = '" & Txtnoarea2.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' + '" & TxtNoTelp2.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' OR " & _
                        "Kode_Area_Telp2 + No_Telp2 = '" & Txtnoarea2.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' + '" & TxtNoTelp1.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' OR " & _
                        "Kode_Area_Telp2 + No_Telp2 = '" & TxtAreaNoHP.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' + '" & TxtNoHP.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' OR " & _
                        "Kode_Area_HP + No_HP = '" & TxtAreaNoHP.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' + '" & TxtNoHP.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' OR " & _
                        "Kode_Area_HP + No_HP = '" & txtnoarea1.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' + '" & TxtNoTelp1.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' OR " & _
                        "Kode_Area_HP + No_HP = '" & Txtnoarea2.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "' + '" & TxtNoTelp2.Text.ToString.Replace("'", "''").Replace(" ", "").Trim & "') AND " & _
                        "[Status] = 1 AND Nama_Customer = '" & TxtNamaCustomer.Text.Trim.Replace("'", "''") & "' AND Jenis_Perusahaan = '" & CbJenisPerusahaan.SelectedValue & "'"
            result = SQLExecuteScalar(sqlstring)
            If result.ToString <> "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Customer " & TxtNamaCustomer.Text & " sudah terdaftar!</h4></div>"
                TxtNamaCustomer.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Response.Write("Error Validasi Insert :<BR>" & ex.ToString)
        End Try
    End Function

#End Region

#Region "BUTTON"

    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            clear_label()
            If hfMode.Value = "Insert" Then
                If validation_input() Then
                    If validation_insert() Then
                        Call Insert()
                        linfoberhasil.Visible = True
                        linfoberhasil.Text = "Data berhasil disimpan"

                    End If
                End If
            Else
                If validation_input() Then
                    Call Update(hfID.Value)
                    clear()
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Data berhasil diupdate"
                End If

            End If
            'Call clear()
        Catch ex As Exception
            Response.Write("Error BtSimpan :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            Call clear()
        Catch ex As Exception
            Response.Write("Error btBatal :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        Try
            Panel1.Visible = False
            Panel2.Visible = True
            Typekonsumen.Text = "Tipe Konsumen"
            CbJenisPerusahaan.Visible = False
            Call clear()
            Call clear_label()
            Call Load_Kode_Customer()

        Catch ex As Exception
            Response.Write("Error Btn_New :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btback_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btback.Click
        Try
            Panel1.Visible = True
            Panel2.Visible = False
            clear()
            load_gridapprove()
        Catch ex As Exception
            Response.Write("<b>Error btback :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "METHOD"

    Private Sub Insert()
        Try
            Dim KodeCustomer As String = Load_Kode_Customer()
            Dim sex As String = ""
            Dim jenisperusahaan As String = ""

            Dim telp1 As String
            Dim telp2 As String
            Dim nohp As String
            If TxtNoTelp1.Text.Trim = "" Then
                telp1 = "0"
            Else
                telp1 = TxtNoTelp1.Text.Replace("'", "''").Replace(" ", "")
            End If
            If TxtNoTelp2.Text.Trim = "" Then
                telp2 = "0"
            Else
                telp2 = TxtNoTelp2.Text.Replace("'", "''").Replace(" ", "")
            End If
            If TxtNoHP.Text.Trim = "" Then
                nohp = "0"
            Else
                nohp = TxtNoHP.Text.Replace("'", "''").Replace(" ", "")
            End If

            If DDLTipeCustomer.SelectedValue = "Perusahaan" Then
                If CbJenisPerusahaan.SelectedValue = Nothing Then
                    sex = ""
                    jenisperusahaan = ""
                Else
                    jenisperusahaan = CbJenisPerusahaan.SelectedValue.ToString
                End If
            Else
                jenisperusahaan = ""
                If cb_JenisKelamin.SelectedValue = "Pria" Then
                    sex = "Pria"
                ElseIf cb_JenisKelamin.SelectedValue = "Wanita" Then
                    sex = "Wanita"
                End If
            End If



            sqlstring = " INSERT INTO MasterCustomer " & _
                        " (Kode_Customer, Tipe_Customer, " & _
                        " Nama_Customer, Jenis_Kelamin, Alamat, Area, KotaDitunjukan, KodePos, " & _
                        " Kode_Area_Telp1, NO_Telp1, Kode_Area_Telp2, No_Telp2, Kode_Area_HP, No_HP, Kode_Area_Fax, Fax, Email, Notes, " & _
                        " [Status], UserName, Jenis_Perusahaan, ContactPerson) VALUES " & _
                        " ('" & KodeCustomer & "', " & _
                        " '" & DDLTipeCustomer.SelectedValue.ToString & "', '" & TxtNamaCustomer.Text.Replace("'", "''") & "', " & _
                        " '" & sex.ToString & "', '" & TxtAlamat.Text.Replace("'", "''") & "', '" & TxtArea.Text.Replace("'", "''").Trim & "', '" & DDLKotaASAlBarang.SelectedValue & "', " & _
                        " '" & TxtKodePos.Text.Replace("'", "''") & "', '" & txtnoarea1.Text.Replace("'", "''").Replace(" ", "").ToString & "' , '" & telp1.ToString & "', " & _
                        " '" & Txtnoarea2.Text.Replace("'", "''").Replace(" ", "").ToString & "', '" & telp2.ToString & "', '" & TxtAreaNoHP.Text.Replace("'", "''").Replace(" ", "").ToString & "' , '" & nohp.ToString & "', " & _
                        " '" & TxtAreaFax.Text.Replace("'", "''").Replace(" ", "").ToString & "'  , '" & TxtFax.Text.Replace("'", "''") & "', '" & TxtEmail.Text.Replace("'", "''") & "', '" & TxtNotes.Text.Replace("'", "''") & "', " & _
                        " 1, '" & Session("UserId").ToString & "', '" & jenisperusahaan.ToString & "', '" & TxtContactPerson.Text.Trim.Replace("'", "''") & "') "
            hasil = SQLExecuteNonQuery(sqlstring)
            If hasil > 0 Then
                Call clear()
                Panel2.Visible = False
                Panel1.Visible = True
                load_gridapprove()

            End If
        Catch ex As Exception
            Response.Write("Error Function Insert() :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub Update(ByVal ID As String)

        '#
        Dim result As Integer
        Dim jenisperusahaan As String = ""

        Try
            Dim sex As String = ""

            Dim telp1 As String
            Dim telp2 As String
            Dim nohp As String
            If TxtNoTelp1.Text.Trim = "" Then
                telp1 = "0"
            Else
                telp1 = TxtNoTelp1.Text.Replace("'", "''").Replace(" ", "")
            End If
            If TxtNoTelp2.Text.Trim = "" Then
                telp2 = "0"
            Else
                telp2 = TxtNoTelp2.Text.Replace("'", "''").Replace(" ", "")
            End If
            If TxtNoHP.Text.Trim = "" Then
                nohp = "0"
            Else
                nohp = TxtNoHP.Text.Replace("'", "''").Replace(" ", "")
            End If

            If DDLTipeCustomer.SelectedValue = "Perusahaan" Then
                If CbJenisPerusahaan.SelectedValue = Nothing Then
                    sex = ""
                    jenisperusahaan = ""
                Else
                    jenisperusahaan = CbJenisPerusahaan.SelectedValue.ToString
                End If
            Else
                jenisperusahaan = ""
                If cb_JenisKelamin.SelectedValue = "Pria" Then
                    sex = "Pria"
                ElseIf cb_JenisKelamin.SelectedValue = "Wanita" Then
                    sex = "Wanita"
                End If
            End If



            sqlstring = " UPDATE " & _
                  "		MasterCustomer " & _
                  "	    SET " & _
                  "		Tipe_Customer = '" & DDLTipeCustomer.SelectedValue.ToString & "', " & _
                  "		Nama_Customer = '" & TxtNamaCustomer.Text.Replace("'", "''") & "', " & _
                  "		Jenis_Kelamin = '" & sex & "', " & _
                  "		Alamat = '" & TxtAlamat.Text.Replace("'", "''") & "', " & _
                  "		Area = '" & TxtArea.Text.Trim.Replace("'", "''") & "', " & _
                  "     KotaDitunjukan = '" & DDLKotaASAlBarang.SelectedValue & "', " & _
                  "		KodePos = '" & TxtKodePos.Text.Replace("'", "''") & "', " & _
                  "     Kode_Area_Telp1 = '" & txtnoarea1.Text.Replace("'", "''").Replace(" ", "").Trim & "', " & _
                  "		NO_Telp1 = '" & telp1 & "', " & _
                  "     Kode_Area_Telp2 = '" & Txtnoarea2.Text.Replace("'", "''").Replace(" ", "").Trim & "', " & _
                  "		No_Telp2 = '" & telp2 & "', " & _
                  "     Kode_Area_HP = '" & TxtAreaNoHP.Text.Replace("'", "''").Replace(" ", "").Trim & "', " & _
                  "		No_HP = '" & nohp & "', " & _
                  "     Kode_Area_Fax = '" & TxtAreaFax.Text.Replace("'", "''").Replace(" ", "").Trim & "', " & _
                  "		Fax = '" & TxtFax.Text.Replace("'", "`") & "', " & _
                  "		Email = '" & TxtEmail.Text.Replace("'", "`") & "', " & _
                  "		Notes = '" & TxtNotes.Text.Replace("'", "`") & "', " & _
                  "		LastModified = '" & Now.ToString & "', " & _
                  "     UserName = '" & Session("UserId").ToString & "', " & _
                  "     Jenis_Perusahaan = '" & jenisperusahaan.ToString & "', " & _
                  "     ContactPerson = '" & TxtContactPerson.Text.Trim.Replace("'", "''") & "' " & _
                  "	    WHERE Kode_Customer = '" & ID.ToString & "' and [status] = 1; "
            result = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Call clear()
                Panel2.Visible = False
                Panel1.Visible = True
                load_gridapprove()
            End If


        Catch ex As Exception
            Response.Write("Update Exception :<br>" & ex.ToString)
        End Try

    End Sub
    Private Sub Delete(ByVal ID As String)
        linfoberhasil.Visible = True
        linfoberhasil.Text = ID
        '#
        Try
            sqlstring = " UPDATE " & _
                  "		MasterCustomer " & _
                  "	    SET " & _
                  "		LastModified = '" & Now.ToString & "', " & _
                  "		[status] = 0 " & _
                  "	    WHERE ID = '" & ID.ToString & "'; "
            Dim result As Integer = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Call clear()
            End If
        Catch ex As Exception
            Response.Write("Delete Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Private Sub clear()
        Try
            DDLTipeCustomer.SelectedIndex = 0
            TxtNamaCustomer.Text = ""
            TxtAlamat.Text = ""
            TxtArea.Text = ""
            TxtKodePos.Text = ""
            txtnoarea1.Text = ""
            TxtNoTelp1.Text = ""
            Txtnoarea2.Text = ""
            TxtNoTelp2.Text = ""
            TxtAreaNoHP.Text = ""
            TxtNoHP.Text = ""
            TxtEmail.Text = ""
            TxtAreaFax.Text = ""
            TxtFax.Text = ""
            TxtNotes.Text = ""
            cb_JenisKelamin.Visible = True
            DDLKotaASAlBarang.SelectedIndex = 0
            'TxtKotaDitunjukan.Text = ""
        Catch ex As Exception
            Response.Write("Error Function Clear :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub clear_label()
        Try
            lInfo.Visible = False
            linfoberhasil.Visible = False
            lInfo.Text = ""
            linfoberhasil.Text = ""
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

#End Region

#Region "GRID"
    Private Sub load_gridapprove()
        Try
            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("kode_customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("tipe_customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("nama_customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("jenis_kelamin", GetType(String)))
            iDT.Columns.Add(New DataColumn("alamat", GetType(String)))
            iDT.Columns.Add(New DataColumn("area", GetType(String)))
            iDT.Columns.Add(New DataColumn("kotaditunjukan", GetType(String)))
            iDT.Columns.Add(New DataColumn("kodepos", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kode_Area_Telp1", GetType(String)))
            iDT.Columns.Add(New DataColumn("no_telp1", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kode_Area_Telp2", GetType(String)))
            iDT.Columns.Add(New DataColumn("no_telp2", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kode_Area_HP", GetType(String)))
            iDT.Columns.Add(New DataColumn("no_hp", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kode_Area_Fax", GetType(String)))
            iDT.Columns.Add(New DataColumn("fax", GetType(String)))
            iDT.Columns.Add(New DataColumn("email", GetType(String)))
            iDT.Columns.Add(New DataColumn("notes", GetType(String)))
            iDT.Columns.Add(New DataColumn("Jenis_Perusahaan", GetType(String)))
            iDT.Columns.Add(New DataColumn("ContactPerson", GetType(String)))
            iDT.Columns.Add(New DataColumn("YgInput", GetType(String)))

            sqlstring = " SELECT ID, Kode_Customer, Tipe_Customer, " & _
                        " Nama_Customer, Jenis_Kelamin, Alamat, Area, kotaditunjukan, KodePos, Kode_Area_Telp1, No_Telp1, Kode_Area_Telp2, No_Telp2, Kode_Area_Fax, Kode_Area_HP, No_HP, " & _
                        " Kode_Area_Fax, Fax, Email, Notes, Jenis_Perusahaan, ContactPerson, Username FROM MasterCustomer " & _
                        " WHERE Status = 1 ORDER BY ID Desc"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow
                        DR("ID") = .Item("ID").ToString()
                        DR("kode_customer") = .Item("kode_customer").ToString
                        DR("tipe_customer") = .Item("tipe_customer").ToString
                        DR("nama_customer") = .Item("nama_customer").ToString
                        DR("jenis_kelamin") = .Item("jenis_kelamin").ToString
                        DR("alamat") = .Item("alamat").ToString
                        DR("area") = .Item("area").ToString
                        DR("kotaditunjukan") = .Item("kotaditunjukan").ToString
                        DR("kodepos") = .Item("kodepos").ToString
                        DR("Jenis_Perusahaan") = .Item("Jenis_Perusahaan").ToString


                        If .Item("no_telp1").ToString = "0" Then
                            DR("no_telp1") = ""
                        Else
                            'Response.Write("<b>Error looping : " & i & "No Telp :" & DR("no_telp1"))

                            DR("no_telp1") = DisplayPhone(.Item("Kode_Area_Telp1").ToString, .Item("no_telp1").ToString)
                        End If

                        If .Item("no_telp2").ToString = "0" Then
                            DR("no_telp2") = ""
                        Else
                            DR("no_telp2") = DisplayPhone(.Item("Kode_Area_Telp2").ToString, .Item("no_telp2").ToString)
                        End If

                        If .Item("no_hp").ToString = "0" Then
                            DR("no_hp") = ""
                        Else
                            DR("no_hp") = DisplayPhone(.Item("Kode_Area_HP").ToString, .Item("no_hp").ToString)
                        End If
                        If .Item("fax").ToString = "" Then
                            DR("fax") = ""
                        Else
                            DR("fax") = DisplayPhone(.Item("Kode_Area_Fax").ToString, .Item("fax").ToString)
                        End If
                        DR("email") = .Item("email").ToString
                        DR("notes") = .Item("notes").ToString
                        DR("ContactPerson") = .Item("ContactPerson").ToString
                        DR("YgInput") = .Item("Username").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next

                'Response.Write(Session("namaroles"))

                GridView_CustomerApprove.Columns("YgInput").Visible = True
                
                Session("grid_customerapprove") = iDT
                GridView_CustomerApprove.DataSource = iDT
                GridView_CustomerApprove.DataBind()
            Else
                GridView_CustomerApprove.DataSource = Nothing
                GridView_CustomerApprove.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Load Grid customer approve :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub GridView_CustomerApprove_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView_CustomerApprove.PreRender
        Try
            If Not Page.IsPostBack Then
                GridView_CustomerApprove.FocusedRowIndex = -1
            End If

        Catch ex As Exception
            Response.Write("GridView_CustomerApprove Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Protected Sub GridView_CustomerApprove_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridView_CustomerApprove.RowCommand
        Try
            Dim pisah() As String
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    Call clear()
                    clear_label()
                    hfMode.Value = "Update"
                    hfID.Value = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "kode_customer").ToString
                    Panel1.Visible = False
                    Panel2.Visible = True
                    LblKodeCustomer.Text = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "kode_customer").ToString
                    If GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "tipe_customer").ToString <> "" Then
                        DDLTipeCustomer.SelectedValue = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "tipe_customer").ToString
                    End If
                    TxtNamaCustomer.Text = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "nama_customer").ToString

                    If DDLTipeCustomer.SelectedValue = "Perusahaan" Then
                        If GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "Jenis_Perusahaan").ToString <> "" Then
                            CbJenisPerusahaan.SelectedValue = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "Jenis_Perusahaan").ToString
                        End If
                        cb_JenisKelamin.Visible = False
                        CbJenisPerusahaan.Visible = True
                        Typekonsumen.Text = "Tipe Perusahaan"
                    Else
                        CbJenisPerusahaan.Visible = False
                        cb_JenisKelamin.Visible = True
                        Typekonsumen.Text = "Tipe Konsumen"
                        If GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "jenis_kelamin").ToString <> "" Then
                            cb_JenisKelamin.SelectedValue = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "jenis_kelamin").ToString
                        End If
                    End If

                    TxtAlamat.Text = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "alamat").ToString
                    TxtArea.Text = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "area").ToString
                    DDLKotaASAlBarang.SelectedValue = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "kotaditunjukan").ToString
                    'TxtKotaDitunjukan.Text = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "area").ToString
                    TxtKodePos.Text = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "kodepos").ToString
                    If GridView_CustomerApprove.GetRowValues(e.VisibleIndex.ToString, "no_telp1").ToString <> "" Then
                        pisah = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "no_telp1").ToString.Split(")")
                        txtnoarea1.Text = pisah(0).Replace("(", "").ToString
                        TxtNoTelp1.Text = pisah(1).Replace("-", "").ToString
                    End If
                    If GridView_CustomerApprove.GetRowValues(e.VisibleIndex.ToString, "no_telp2").ToString <> "" Then
                        pisah = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "no_telp2").ToString.Split(")")
                        Txtnoarea2.Text = pisah(0).Replace("(", "").ToString
                        TxtNoTelp2.Text = pisah(1).Replace("-", "").ToString
                    End If
                    If GridView_CustomerApprove.GetRowValues(e.VisibleIndex.ToString, "no_hp").ToString <> "" Then
                        pisah = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "no_hp").ToString.Split(")")
                        TxtAreaNoHP.Text = pisah(0).Replace("(", "").ToString
                        TxtNoHP.Text = pisah(1).Replace("-", "").ToString
                    End If
                    If GridView_CustomerApprove.GetRowValues(e.VisibleIndex.ToString, "fax").ToString <> "" Then
                        pisah = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "fax").ToString.Split(")")
                        TxtAreaFax.Text = pisah(0).Replace("(", "").ToString
                        TxtFax.Text = pisah(1).Replace("-", "").ToString
                    End If

                    TxtEmail.Text = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "email").ToString
                    TxtNotes.Text = GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "notes").ToString
                Case "Delete"
                    Dim hsl As Integer

                    hsl = CekUsingNumber("MasterQuotation", "Customer_Id", GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "kode_customer").ToString)

                    If hsl = 0 Then
                        Delete(GridView_CustomerApprove.GetRowValues(e.VisibleIndex, "ID").ToString)
                        linfoberhasil.Text = "Customer berhasil dihapus"
                    Else
                        lInfo.Text = "Customer masih terpakai pada quotation"
                    End If


                    Call load_gridapprove()
            End Select
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub
#End Region

#Region "DDL "
    Protected Sub DDLTipeCustomer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLTipeCustomer.SelectedIndexChanged
        Try
            If DDLTipeCustomer.SelectedValue = "Perusahaan" Then
                cb_JenisKelamin.Visible = False
                Typekonsumen.Text = "Tipe Perusahaan"
                CbJenisPerusahaan.Visible = True
            Else
                cb_JenisKelamin.Visible = True
                Typekonsumen.Visible = True
                titikdua.Visible = True
                Typekonsumen.Text = "Tipe Konsumen"
                CbJenisPerusahaan.Visible = False
            End If
        Catch ex As Exception
            Response.Write("<b>Error ddl :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub LoadDDLKOTA()
        Try
            sqlstring = "SELECT ID,TUJUAN FROM MasterTujuan WHERE [status] = 1"
            DT = SQLExecuteQuery(sqlstring).Tables(0)

            With DDLKotaASAlBarang
                .DataSource = DT
                .DataTextField = "TUJUAN"
                .DataValueField = "TUJUAN"
                .DataBind()
            End With

            DDLKotaASAlBarang.Items.Insert(0, "Pilih Kota Asal")

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
#End Region


End Class
