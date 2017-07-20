Imports System.Data
Imports System.Data.SqlClient
Partial Public Class KonfigurasiHeader
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
                panelcabang.Visible = False
                hfMode.Value = "Insert"
                load_ddl_namaperusahaan()
                Load_Grid_Header()
            End If

            If Not Session("GridViewHeader") Is Nothing Then
                GridViewHeader.DataSource = CType(Session("GridViewHeader"), DataTable)
                GridViewHeader.DataBind()
            End If
        Catch ex As Exception
            Response.Write("<b>Error Loading Page :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Validasi"
    Private Function validasi() As Boolean
        Try
            clearlabel()

            If hfMode.Value = "Insert" Then
                If tbnama.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Nama masih kosong"
                    Return False
                End If

                If tbdaerah.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "daerah masih kosong"
                    Return False
                End If

                If tbAlamat.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "alamat masih kosong"
                    Return False
                End If

                If tbNotelp1.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "No Telp 1 masih kosong"
                    Return False
                End If

                If tbNoTelp2.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "No Telp 2 masih kosong"
                    Return False
                End If

                If tbemail.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "email masih kosong"
                    Return False
                End If

                If IsNumeric(tbnama.Text) Then
                    lInfo.Visible = True
                    lInfo.Text = "Nama tidak boleh angka"
                    Return False
                End If

                If IsNumeric(tbdaerah.Text) Then
                    lInfo.Visible = True
                    lInfo.Text = "daerah tidak boleh angka"
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("<b>Error Function Validation :</b> " & ex.ToString)
        End Try
    End Function

    Private Function validasicabang() As Boolean
        If ddlnamaperusahaan.SelectedIndex = 0 Then
            Linfo2.Visible = True
            Linfo2.Text = "Pilih nama perusahaan"
            Return False
        End If

        If tbdaerahcbg.Text = "" Then
            Linfo2.Visible = True
            Linfo2.Text = "Nama daerah masih kosong"
            Return False
        End If

        If tbalamatcbg.Text = "" Then
            Linfo2.Visible = True
            Linfo2.Text = "Alamat masih kosong"
            Return False
        End If

        If tbtelp1cbg.Text = "" Then
            Linfo2.Visible = True
            Linfo2.Text = "No Telp1 masih kosong"
            Return False
        End If

        If tbtelp2cbg.Text = "" Then
            Linfo2.Visible = True
            Linfo2.Text = "No Telp2 masih kosong"
            Return False
        End If

        If IsNumeric(tbdaerahcbg.Text) Then
            Linfo2.Visible = True
            Linfo2.Text = "Nama daerah tidak boleh angka"
            Return False
        End If

        Return True
    End Function
#End Region

#Region "Methods"
    Private Sub insert()
        Try
            sqlstring = " INSERT INTO HeaderForm(Jenis, Nama, Daerah, Alamat, No_Telp1, No_Telp2, Fax, Email, NO_Angta_INSA,UserName,PathLogo, [status]) " & _
                        " VALUES('" & lbljenis.Text & "', '" & tbnama.Text.Replace("'", "''") & "', '" & tbdaerah.Text.Replace("'", "''") & "', " & _
                        " '" & tbAlamat.Text.Replace("'", "''") & "', '" & tbNotelp1.Text.Replace("'", "''") & "', '" & tbNoTelp2.Text.Replace("'", "''") & "', " & _
                        " '" & tbfax.Text.Replace("'", "''") & "', '" & tbemail.Text.Replace("'", "''") & "', " & _
                        " '" & tbnoanggota.Text.Replace("'", "''") & "','" & Session("UserId") & "','" & img.ImageUrl.ToString & "', 1) "
            result = SQLExecuteNonQuery(sqlstring)

            If result > 0 Then
                Load_Grid_Header()
                clear()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error function insert : </b> " & ex.ToString)
        End Try
    End Sub

    Private Sub update(ByVal id As String)
        Try
            sqlstring = "UPDATE HeaderForm " & _
                        "SET Nama = '" & tbnama.Text.Replace("'", "''") & "' " & _
                        ", Daerah =  '" & tbdaerah.Text.Replace("'", "''") & "' " & _
                        ", Alamat = '" & tbAlamat.Text.Replace("'", "''") & "' " & _
                        ", No_Telp1 = '" & tbNotelp1.Text.Replace("'", "''") & "' " & _
                        ", No_Telp2 = '" & tbNoTelp2.Text.Replace("'", "''") & "' " & _
                        ", Fax = '" & tbfax.Text.Replace("'", "''") & "' " & _
                        ", Email = '" & tbemail.Text.Replace("'", "''") & "' " & _
                        ", NO_Angta_INSA = '" & tbnoanggota.Text.Replace("'", "''") & "' " & _
                        ", PathLogo = '" & img.ImageUrl.ToString & "' " & _
                        ", UserName = '" & Session("UserId") & "' " & _
                        ", LastModified = '" & Now.ToString & "' " & _
                        "WHERE ID = " & id.ToString & ""
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Load_Grid_Header()
                clear()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error function update:</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub del(ByVal id As String)
        Try
            sqlstring = "UPDATE HeaderForm " & _
                        "SET UserName = '" & Session("UserId") & "' " & _
                        "LastModified = '" & Now.ToString & "', " & _
                        "[status] = 0 " & _
                        "WHERE ID = " & id.ToString & ""
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Load_Grid_Header()
                clear()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error function delete :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub clear()
        Try
            hfID.Value = ""
            hfMode.Value = "Insert"
            tbnama.Text = ""
            tbdaerah.Text = ""
            tbAlamat.Text = ""
            tbNotelp1.Text = ""
            tbNoTelp2.Text = ""
            tbfax.Text = ""
            tbemail.Text = ""
            tbnoanggota.Text = ""
            lInfo.Text = ""
            linfoberhasil.Text = ""
            lInfo.Visible = False
            linfoberhasil.Visible = False
            img.Visible = False
            img.ImageUrl = ""
        Catch ex As Exception
            Throw New Exception("<b>Error function clear :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub clearlabel()
        lInfo.Text = ""
        linfoberhasil.Text = ""
        lInfo.Visible = False
        linfoberhasil.Visible = False
        Linfo2.Text = ""
        Linfoberhasil2.Text = ""
        Linfo2.Visible = False
        Linfoberhasil2.Visible = False
    End Sub

    Private Sub insertcbg()
        Try
            sqlstring = " INSERT INTO DetailHeaderForm(IDHeaderForm, Daerah, Alamat, No_Telp1, No_Telp2, No_Telp3, Fax, Email, UserName, [status]) " & _
                        " VALUES " & _
                        "(" & ddlnamaperusahaan.SelectedValue & ", '" & tbdaerahcbg.Text.Replace("'", "''") & "', " & _
                        " '" & tbalamatcbg.Text.Replace("'", "''") & "', '" & tbtelp1cbg.Text.Replace("'", "''") & "', " & _
                        " '" & tbtelp2cbg.Text.Replace("'", "''") & "', '" & tbtelp3cbg.Text.Replace("'", "''") & "', '" & tbfaxcbg.Text.Replace("'", "''") & "', " & _
                        " '" & tbemailcbg.Text.Replace("'", "''") & "', '" & Session("UserId") & "', 1)"
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                load_grid_cabang()
                clearcabang()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error insert cbg :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub updatecbg(ByVal id As String)
        Try
            sqlstring = "UPDATE DetailHeaderForm " & _
                        "SET " & _
                        " IDHeaderForm = '" & ddlnamaperusahaan.SelectedValue & "' " & _
                        ",Daerah = '" & tbdaerahcbg.Text.Replace("'", "''") & "' " & _
                        ",Alamat = '" & tbalamatcbg.Text.Replace("'", "''") & "' " & _
                        ",No_Telp1 = '" & tbtelp1cbg.Text.Replace("'", "''") & "' " & _
                        ",No_Telp2 = '" & tbtelp2cbg.Text.Replace("'", "''") & "' " & _
                        ",No_Telp3 = '" & tbtelp3cbg.Text.Replace("'", "''") & "' " & _
                        ",Fax = '" & tbfaxcbg.Text.Replace("'", "''") & "' " & _
                        ",Email = '" & tbemailcbg.Text.Replace("'", "''") & "' " & _
                        ",UserName = '" & Session("UserId") & "' " & _
                        ",LastModified = '" & Now.ToString & "' " & _
                        "WHERE ID = " & id & ""
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                load_grid_cabang()
                clearcabang()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error Updatecabang:</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub delcabang(ByVal id As String)
        Try
            sqlstring = "UPDATE DetailHeaderForm " & _
                        "SET " & _
                        "UserName = '" & Session("UserId") & "' " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 0 " & _
                        "WHERE ID = " & id.ToString & ""
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                load_grid_cabang()
                clearcabang()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error function delete :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub clearcabang()
        Try
            Hfidcbg.Value = ""
            hfmodecbg.Value = "Insert"
            tbdaerahcbg.Text = ""
            tbemailcbg.Text = ""
            tbalamatcbg.Text = ""
            tbtelp1cbg.Text = ""
            tbtelp2cbg.Text = ""
            tbtelp3cbg.Text = ""
            tbfaxcbg.Text = ""
            tbemailcbg.Text = ""
            Linfoberhasil2.Visible = False
            Linfo2.Visible = False

        Catch ex As Exception
            Throw New Exception("<b>Error function clear :</b>" & ex.ToString)
        End Try
    End Sub

    Public Function changed(ByVal name As String) As String
        Dim tgl As String = Format(Now.Date, "dd/MM/yyyy")
        tgl = tgl.Replace("/", "")

        Dim fill() As String = name.Split(".")
        Dim extension As String = fill(fill.Length - 1)
        name = "LogoHeader" & tgl
        name &= "." & extension
        Return name
    End Function

#End Region

#Region "Load Grid"
    Private Sub Load_Grid_Header()
        Try
            sqlstring = "SELECT ID, Nama, Daerah, Alamat, No_Telp1, No_Telp2, Fax, Email, NO_Angta_INSA, PathLogo " & _
                        "FROM HeaderForm where [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("nama", GetType(String)))
                .Add(New DataColumn("daerah", GetType(String)))
                .Add(New DataColumn("alamat", GetType(String)))
                .Add(New DataColumn("notelp1", GetType(String)))
                .Add(New DataColumn("notelp2", GetType(String)))
                .Add(New DataColumn("fax", GetType(String)))
                .Add(New DataColumn("email", GetType(String)))
                .Add(New DataColumn("noagtinsa", GetType(String)))
                .Add(New DataColumn("pathlogo", GetType(String)))
            End With

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("nama") = .Item("Nama").ToString
                    DR("daerah") = .Item("Daerah").ToString
                    DR("alamat") = .Item("Alamat").ToString
                    DR("notelp1") = .Item("No_Telp1").ToString
                    DR("notelp2") = .Item("No_Telp2").ToString
                    DR("fax") = .Item("Fax").ToString
                    DR("email") = .Item("Email").ToString
                    DR("noagtinsa") = .Item("NO_Angta_INSA").ToString
                    DR("pathlogo") = .Item("PathLogo").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("GridViewHeader") = iDT
            GridViewHeader.DataSource = iDT
            GridViewHeader.DataBind()

        Catch ex As Exception
            Throw New Exception("<b>Error load_grid_header :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_grid_cabang()
        Try
            sqlstring = "SELECT DHF.ID, HF.Nama, DHF.Daerah, DHF.Alamat, DHF.No_Telp1, DHF.No_Telp2, DHF.No_Telp3, DHF.Fax, DHF.Email " & _
                        "FROM DetailHeaderForm DHF " & _
                        "JOIN HeaderForm HF ON HF.ID=DHF.IDHeaderForm " & _
                        "Where DHF.[status] = 1 " & _
                        "AND HF.[status]=1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("namaperusahaan", GetType(String)))
                .Add(New DataColumn("daerah", GetType(String)))
                .Add(New DataColumn("alamat", GetType(String)))
                .Add(New DataColumn("notelp1", GetType(String)))
                .Add(New DataColumn("notelp2", GetType(String)))
                .Add(New DataColumn("notelp3", GetType(String)))
                .Add(New DataColumn("fax", GetType(String)))
                .Add(New DataColumn("email", GetType(String)))

            End With

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("namaperusahaan") = .Item("Nama").ToString
                    DR("daerah") = .Item("Daerah").ToString
                    DR("alamat") = .Item("Alamat").ToString
                    DR("notelp1") = .Item("No_Telp1").ToString
                    DR("notelp2") = .Item("No_Telp2").ToString
                    DR("notelp3") = .Item("No_Telp3").ToString
                    DR("fax") = .Item("Fax").ToString
                    DR("email") = .Item("Email").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("GridViewHeaderCbg") = iDT
            GridViewHeadercbg.DataSource = iDT
            GridViewHeadercbg.DataBind()

        Catch ex As Exception
            Throw New Exception("<b>Error Load grid cabang :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "Button"
    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSimpan.Click
        Try
            If validasi() Then
                If hfMode.Value = "Insert" Then
                    insert()
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Save Berhasil"

                Else
                    update(hfID.Value)
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Update Berhasil"
                End If
            End If
        Catch ex As Exception
            Response.Write("<b>Error btsimpan :</b> " & ex.ToString)
        End Try
    End Sub

    Private Sub GridViewHeader_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridViewHeader.RowCommand
        Try
            clearlabel()
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    hfID.Value = GridViewHeader.GetRowValues(e.VisibleIndex, "ID")
                    tbnama.Text = GridViewHeader.GetRowValues(e.VisibleIndex, "nama")
                    tbdaerah.Text = GridViewHeader.GetRowValues(e.VisibleIndex, "daerah")
                    tbAlamat.Text = GridViewHeader.GetRowValues(e.VisibleIndex, "alamat")
                    tbNotelp1.Text = GridViewHeader.GetRowValues(e.VisibleIndex, "notelp1")
                    tbNoTelp2.Text = GridViewHeader.GetRowValues(e.VisibleIndex, "notelp2")
                    tbemail.Text = GridViewHeader.GetRowValues(e.VisibleIndex, "email")
                    tbnoanggota.Text = GridViewHeader.GetRowValues(e.VisibleIndex, "noagtinsa")
                    img.ImageUrl = GridViewHeader.GetRowValues(e.VisibleIndex, "pathlogo").ToString
                    img.Visible = True
                Case "Delete"
                    del(GridViewHeader.GetRowValues(e.VisibleIndex, "ID"))
            End Select
        Catch ex As Exception
            Response.Write("<b>Error GridviewCommand :<b>" & ex.ToString)
        End Try
    End Sub

    Private Sub GridViewHeadercbg_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridViewHeadercbg.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfmodecbg.Value = "Update"
                    Hfidcbg.Value = GridViewHeader.GetRowValues(e.VisibleIndex, "ID")
                    tbdaerahcbg.Text = GridViewHeadercbg.GetRowValues(e.VisibleIndex, "daerah")
                    tbalamatcbg.Text = GridViewHeadercbg.GetRowValues(e.VisibleIndex, "alamat")
                    tbtelp1cbg.Text = GridViewHeadercbg.GetRowValues(e.VisibleIndex, "notelp1")
                    tbtelp2cbg.Text = GridViewHeadercbg.GetRowValues(e.VisibleIndex, "notelp2")
                    tbemailcbg.Text = GridViewHeadercbg.GetRowValues(e.VisibleIndex, "email")
                    tbfaxcbg.Text = GridViewHeadercbg.GetRowValues(e.VisibleIndex, "fax")
                Case "Delete"
                    delcabang(GridViewHeadercbg.GetRowValues(e.VisibleIndex, "ID"))
            End Select
        Catch ex As Exception
            Response.Write("<b>Error GridviewCommand :<b>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBatal.Click
        Try
            clear()
        Catch ex As Exception
            Response.Write("<b>Error btcancel :</b>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btcabang_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btcabang.Click
        Try
            clear()
            clearcabang()
            clearlabel()
            panelutama.Visible = False
            panelcabang.Visible = True
            load_ddl_namaperusahaan()
            load_grid_cabang()
        Catch ex As Exception
            Response.Write("<b>Error btncabang :</b> " & ex.ToString)
        End Try
    End Sub

    Protected Sub btsavecbg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btsavecbg.Click
        Try
            If validasicabang() Then
                If hfmodecbg.Value = "Insert" Then
                    insertcbg()
                    Linfoberhasil2.Visible = True
                    Linfoberhasil2.Text = "Save Berhasil"
                Else
                    updatecbg(Hfidcbg.Value)
                    Linfoberhasil2.Visible = True
                    Linfoberhasil2.Text = "Update Berhasil"
                End If
            End If
        Catch ex As Exception
            Response.Write("<b>Error btnsavecbg :</b>" & ex.ToString)
        End Try
    End Sub

    Protected Sub tbbatalcbg_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tbbatalcbg.Click
        Try
            clearcabang()
        Catch ex As Exception
            Response.Write("<b>Error btnbatalcbg:</b>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btnkembali_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnkembali.Click
        Try
            panelcabang.Visible = False
            panelutama.Visible = True
            clearcabang()
            clearlabel()
        Catch ex As Exception
            Response.Write("<b>Error btnkembali:</b>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btnupload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnupload.Click
        Try
            Dim filename As String = changed(uploadfile.FileName)

            Dim pisah() As String = filename.Split(".")
            Dim extension As String = pisah(pisah.Length - 1)

            If uploadfile.HasFile = False Then
                lInfo.Visible = True
                lInfo.Text = "Silakan Pilih FIle"
                Exit Sub
            End If

            If pisah(1) <> "png" And pisah(1) <> "jpg" And pisah(1) <> "jpeg" And pisah(1) <> "gif" And pisah(1) <> "JPG" And pisah(1) <> "JPEG" And pisah(1) <> "PNG" And pisah(1) <> "GIF" Then
                lInfo.Visible = True
                lInfo.Text = "Tipe data yang dimasukan salah"
                Exit Sub
            End If

            If uploadfile.HasFile Then
                uploadfile.PostedFile.SaveAs(Server.MapPath("../Images/" & filename))
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Upload Success"
                img.Visible = True
                img.ImageUrl = "../Images/" & filename

            End If

        Catch ex As Exception
            Response.Write("<b>Error btnupload :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "Load DDL"
    Private Sub load_ddl_namaperusahaan()
        Try
            sqlstring = "SELECT ID,Nama from HeaderForm where [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With ddlnamaperusahaan
                .DataSource = DT
                .DataTextField = "Nama"
                .DataValueField = "ID"
                .DataBind()
            End With
            ddlnamaperusahaan.Items.Insert(0, "----Pilih----")

        Catch ex As Exception
            Throw New Exception("<b>Error load DDL :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Create Session"
    Private Sub createsessionheader()
        Try
            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("nama", GetType(String)))
                .Add(New DataColumn("daerah", GetType(String)))
                .Add(New DataColumn("alamat", GetType(String)))
                .Add(New DataColumn("notelp1", GetType(String)))
                .Add(New DataColumn("notelp2", GetType(String)))
                .Add(New DataColumn("fax", GetType(String)))
                .Add(New DataColumn("email", GetType(String)))
                .Add(New DataColumn("noagtinsa", GetType(String)))
                .Add(New DataColumn("pathlogo", GetType(String)))
            End With

            Session("GridViewHeader") = iDT
            GridViewHeader.DataSource = iDT
            GridViewHeader.KeyFieldName = "ID"
            GridViewHeader.DataBind()

        Catch ex As Exception
            Throw New Exception("<b>Error createsessionheader :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub createsessioncbg()
        Try
            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("namaperusahaan", GetType(String)))
                .Add(New DataColumn("daerah", GetType(String)))
                .Add(New DataColumn("alamat", GetType(String)))
                .Add(New DataColumn("notelp1", GetType(String)))
                .Add(New DataColumn("notelp2", GetType(String)))
                .Add(New DataColumn("fax", GetType(String)))
                .Add(New DataColumn("email", GetType(String)))

            End With

            Session("GridViewHeaderCbg") = iDT
            GridViewHeadercbg.DataSource = iDT
            GridViewHeadercbg.KeyFieldName = "ID"
            GridViewHeadercbg.DataBind()
        Catch ex As Exception
            Throw New Exception("<b>Error createsessioncbg :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

    
End Class