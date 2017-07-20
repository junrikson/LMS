Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Public Class ReportManifest
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DS As DataSet
    Private DR As DataRow
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim result As String
    Dim hasil As Integer
    Dim role As String = ""

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                Session("Grid_Manifest") = Nothing
                Session("Grid_History") = Nothing
                load_ddl()
                load_ddl_hist()
                ManifestDate.Date = Today
                LoadDDLKota()
                DDL_nama_gudang()
                Panel_Input.Visible = True
                Panel_Grid.Visible = True
                historygrid.Visible = False
                historyinput.Visible = False
                Panel_Report.Visible = False
                PanelInputKontainer.Visible = False

                role = GETNAMAROLE(Session("UserId").ToString)


                If (role.ToString.ToUpper = "ADMIN" Or role.ToString.ToUpper = "MASTER ACCOUNTING") Then
                    btView.Enabled = True
                Else
                    btView.Enabled = False
                End If
                create_session()
            End If

            If Not Session("Grid_Manifest") Is Nothing Then
                Grid_Manifest.DataSource = CType(Session("Grid_Manifest"), DataTable)
                Grid_Manifest.DataBind()
            End If

            If Not Session("Grid_History") Is Nothing Then
                Grid_History.DataSource = CType(Session("Grid_History"), DataTable)
                Grid_History.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

        
    End Sub
    Private Function load_mb_number() As String
        Dim month, year, tanggal As String
        Dim value As String = ""
        Dim no As Integer

        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 Mbr_No FROM MuatBarangReport " & _
                        "WHERE Mbr_No LIKE 'MBR/" & year.ToString & month.ToString & tanggal.ToString & "%' and status <> 0" & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring)

            If result.ToString <> "" Then
                no = CDbl(Right(result, 4)) + 1
            Else
                no = 1
            End If
            value = "MBR/" & year.ToString & month.ToString & tanggal.ToString & no.ToString("0000")

        Catch ex As Exception
            Response.Write("Error Load_Mb_No :<BR>" & ex.ToString)
        End Try
        Return value
    End Function
#End Region

#Region "GRID"
   
    Private Function create_session() As DataTable
        Dim mdt As New DataTable
        Try
            mdt.Columns.Add(New DataColumn("IDM", GetType(String)))
            mdt.Columns.Add(New DataColumn("MB_IDM", GetType(String)))
            mdt.Columns.Add(New DataColumn("MB_NOM", GetType(String)))
            mdt.Columns.Add(New DataColumn("NoContainerM", GetType(String)))
            mdt.Columns.Add(New DataColumn("CustomerM", GetType(String)))
            mdt.Columns.Add(New DataColumn("PenerimaM", GetType(String)))
            mdt.Columns.Add(New DataColumn("MerkM", GetType(String)))
            mdt.Columns.Add(New DataColumn("JumlahColliM", GetType(String)))
            mdt.Columns.Add(New DataColumn("NamaBarangM", GetType(String)))
            mdt.Columns.Add(New DataColumn("BeratM", GetType(String)))
            mdt.Columns.Add(New DataColumn("UkuranM", GetType(String)))
            mdt.Columns.Add(New DataColumn("PanjangM", GetType(String)))
            mdt.Columns.Add(New DataColumn("LebarM", GetType(String)))
            mdt.Columns.Add(New DataColumn("QuantityM", GetType(String)))
            mdt.Columns.Add(New DataColumn("TinggiM", GetType(String)))
            mdt.Columns.Add(New DataColumn("VolumeM", GetType(String)))
            mdt.Columns.Add(New DataColumn("Nama_KapalM", GetType(String)))
            mdt.Columns.Add(New DataColumn("KapalM", GetType(String)))
            mdt.Columns.Add(New DataColumn("QuotationDetailIDM", GetType(String)))
            mdt.Columns.Add(New DataColumn("KeteranganM", GetType(String)))
            mdt.Columns.Add(New DataColumn("NoSealM", GetType(String)))
            mdt.Columns.Add(New DataColumn("WarehouseHeaderIDM", GetType(String)))
            mdt.Columns.Add(New DataColumn("IDDetailWarehouseDetail", GetType(String)))


            Return mdt
        Catch ex As Exception
            Throw New Exception(" error function create_session :" & ex.ToString)
        End Try

    End Function
    Private Sub load_muat_barang()
        Dim mDT As New DataTable
        Dim namabarang As String
        Dim berat As String
        Dim ukuran As String
        Dim cDT As DataTable
        Dim cDS As DataSet
        Dim cST As String
        Dim jumlahcolli As String
        Dim TotalCont As Integer
        Dim nocontainer As String
        Dim volumetotal As String = ""
        Dim NoSeal As String = ""


        Try


            sqlstring = " select wd.IDDetail as IDDetailWarehouseDetail, mbd.IDDetail as ID,mb.Penerima,mbd.MB_No,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer, " & _
                     "wd.Container,wd.Merk, wd.Nama_Barang,mbd.PackedContID,mb.Customer_Id as Keterangan ," & _
                     " mbd.Quantity,mb.Kapal,wd.Nama_Satuan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat ,wd.QuotationDetailID as QuotationDetailIDM, wd.WarehouseItem_Code, " & _
                     " cast(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity as Numeric(30,10)) as totalukuran, " & _
                     " wd.Panjang, wd.Lebar, wd.Tinggi,wd.NamaSupplier, " & _
                     " cast(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity as Numeric(30,10)) as volumetotal, mbd.NoSeal " & _
                     " from " & _
                     " MuatBarang mb left join MuatBarangDetail mbd on (mb.Mb_No = mbd.Mb_No  ) " & _
                     " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID) " & _
                     " left join Kapal mk on (mb.Kapal = mk.IDDetail  ) " & _
                     " left join MasterCustomer mc on (mb.Customer_Id = mc.Kode_Customer ) " & _
                     " where mb.status = 1 And mb.Kapal = '" & hfIDK.Value & "' and mbd.status = 1 " & _
                     " and wd.statusheader <> 0 and mc.status = 1 and mk.status = 1 and wd.[status] <> 0 and wd.Warehouse_Code = '" & DdlNamaGudang.SelectedValue & "'"

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)



            mDT.Columns.Add(New DataColumn("IDM", GetType(String)))
            mDT.Columns.Add(New DataColumn("MB_IDM", GetType(String)))
            mDT.Columns.Add(New DataColumn("MB_NOM", GetType(String)))
            mDT.Columns.Add(New DataColumn("NoContainerM", GetType(String)))
            mDT.Columns.Add(New DataColumn("CustomerM", GetType(String)))
            mDT.Columns.Add(New DataColumn("PenerimaM", GetType(String)))
            mDT.Columns.Add(New DataColumn("MerkM", GetType(String)))
            mDT.Columns.Add(New DataColumn("JumlahColliM", GetType(String)))
            mDT.Columns.Add(New DataColumn("NamaBarangM", GetType(String)))
            mDT.Columns.Add(New DataColumn("BeratM", GetType(String)))
            mDT.Columns.Add(New DataColumn("UkuranM", GetType(String)))
            mDT.Columns.Add(New DataColumn("PanjangM", GetType(String)))
            mDT.Columns.Add(New DataColumn("LebarM", GetType(String)))
            mDT.Columns.Add(New DataColumn("QuantityM", GetType(String)))
            mDT.Columns.Add(New DataColumn("TinggiM", GetType(String)))
            mDT.Columns.Add(New DataColumn("VolumeM", GetType(String)))
            mDT.Columns.Add(New DataColumn("Nama_KapalM", GetType(String)))
            mDT.Columns.Add(New DataColumn("KapalM", GetType(String)))
            mDT.Columns.Add(New DataColumn("QuotationDetailIDM", GetType(String)))
            mDT.Columns.Add(New DataColumn("KeteranganM", GetType(String)))
            mDT.Columns.Add(New DataColumn("NamaSupplierM", GetType(String)))
            mDT.Columns.Add(New DataColumn("NoSealM", GetType(String)))
            mDT.Columns.Add(New DataColumn("WarehouseHeaderIDM", GetType(String)))
            mDT.Columns.Add(New DataColumn("IDDetailWarehouseDetail", GetType(String)))

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    TotalCont = 0
                    DR = mDT.NewRow
                    With DT.Rows(i)
                        If .Item("Container") = "true" Or .Item("Container") = "kubikasi" Or .Item("Container") = "Kubikasi" Then
                            cST = " select cd.NamaBarang,cd.Qty,ch.NoKontainer as ContainerCode,ch.totalberat, ch.NoSeal from ContainerHeader ch,ContainerDetail cd where ch.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and ch.ContainerCode = cd.ContainerCode and ch.status = 1"
                            cDS = SQLExecuteQuery(cST)
                            cDT = cDS.Tables(0)
                            namabarang = ""
                            If cDT.Rows.Count > 0 Then
                                For e As Integer = 0 To cDT.Rows.Count - 1
                                    TotalCont = TotalCont + CInt(cDT.Rows(e).Item("Qty").ToString)
                                    If e > 3 Then
                                        namabarang &= "dll."
                                        Exit For
                                    End If
                                    If e = cDT.Rows.Count - 1 Then
                                        namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + "."
                                    Else
                                        namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + ","
                                    End If
                                Next
                            End If
                            If .Item("Container") = "true" Then
                                ukuran = "-"
                                berat = "20000"
                                jumlahcolli = TotalCont.ToString + " Cont"
                                volumetotal = "0"
                                If cDT.Rows.Count > 0 Then
                                    nocontainer = cDT.Rows(0).Item("ContainerCode").ToString
                                    NoSeal = cDT.Rows(0).Item("NoSeal").ToString
                                Else
                                    nocontainer = "Kosong"
                                    NoSeal = "Kosong"
                                End If


                            Else
                                ukuran = cDT.Rows(0).Item("totalberat")
                                jumlahcolli = TotalCont.ToString + " Cont"
                                berat = "0"

                                If .Item("PackedContID").ToString = "0" Then
                                    nocontainer = "--"
                                    NoSeal = "--"
                                Else
                                    nocontainer = .Item("PackedContID").ToString
                                    NoSeal = .Item("NoSeal").ToString
                                End If
                            End If
                        Else
                            namabarang = .Item("Nama_Barang").ToString
                            berat = .Item("totalberat").ToString
                            ukuran = .Item("totalukuran").ToString
                            jumlahcolli = .Item("Quantity").ToString + " " + .Item("Nama_Satuan").ToString
                            volumetotal = .Item("VolumeTotal")
                            TotalCont = .Item("Quantity").ToString
                            If .Item("PackedContID").ToString = "0" Then
                                nocontainer = "--"
                                NoSeal = "--"
                            Else
                                nocontainer = .Item("PackedContID").ToString
                                NoSeal = .Item("NoSeal").ToString
                            End If
                        End If

                        DR("MB_IDM") = .Item("ID").ToString
                        DR("MB_NOM") = .Item("MB_No").ToString
                        DR("NoContainerM") = nocontainer.ToString
                        DR("CustomerM") = .Item("Nama_Customer").ToString
                        DR("PenerimaM") = .Item("Penerima").ToString
                        DR("NamaSupplierM") = .Item("NamaSupplier").ToString
                        DR("MerkM") = .Item("Merk").ToString
                        DR("JumlahColliM") = jumlahcolli.ToString
                        DR("NamaBarangM") = namabarang.ToString
                        DR("BeratM") = berat
                        DR("UkuranM") = ukuran
                        DR("Nama_KapalM") = .Item("Nama_Kapal").ToString
                        DR("KapalM") = .Item("Kapal").ToString
                        DR("KeteranganM") = .Item("Keterangan").ToString
                        DR("PanjangM") = .Item("Panjang").ToString
                        DR("QuantityM") = TotalCont
                        DR("LebarM") = .Item("Lebar").ToString
                        DR("QuotationDetailIDM") = .Item("QuotationDetailIDM").ToString
                        DR("TinggiM") = .Item("Tinggi").ToString
                        DR("VolumeM") = volumetotal.ToString
                        DR("NoSealM") = NoSeal.ToString
                        DR("WarehouseHeaderIDM") = .Item("WarehouseItem_Code").ToString
                        DR("IDDetailWarehouseDetail") = .Item("IDDetailWarehouseDetail").ToString
                        mDT.Rows.Add(DR)
                    End With
                Next
            End If
            Session("Grid_Manifest") = Nothing
            Session("Grid_Manifest") = mDT
            Grid_Manifest.DataSource = mDT
            Grid_Manifest.DataBind()

        Catch ex As Exception
            Throw New Exception("Error Function Load_Muat_Barang :" & ex.ToString)
        End Try


    End Sub
    Private Sub load_grid()
        Dim hDT As New DataTable
        Try
            sqlstring = " Select mbr.Mbr_No as ID , mbr.Depart_Date,mbr.NoPelayaran, k.Nama_Kapal,mbr.Kapal, mbr.Dari, mbr.Tujuan from MuatBarangReport mbr left join Kapal k on (mbr.Kapal = k.IDDetail and k.status = 1) where mbr.status = 1 or mbr.status = 5 Order BY mbr.ID Desc "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            hDT.Columns.Add(New DataColumn("ID", GetType(String)))
            hDT.Columns.Add(New DataColumn("Depart_Date", GetType(DateTime)))
            hDT.Columns.Add(New DataColumn("Kapal", GetType(String)))
            hDT.Columns.Add(New DataColumn("Nama_Kapal", GetType(String)))
            hDT.Columns.Add(New DataColumn("NoPelayaran", GetType(String)))
            hDT.Columns.Add(New DataColumn("Dari", GetType(String)))
            hDT.Columns.Add(New DataColumn("Tujuan", GetType(String)))

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = hDT.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("Depart_Date") = CDate(.Item("Depart_Date").ToString).ToString("MM/dd/yyyy")
                        DR("Kapal") = .Item("Kapal").ToString
                        DR("Nama_Kapal") = .Item("Nama_Kapal").ToString
                        DR("NoPelayaran") = .Item("NoPelayaran").ToString
                        DR("Dari") = .Item("Dari").ToString
                        DR("Tujuan") = .Item("Tujuan").ToString
                        hDT.Rows.Add(DR)
                    End With
                Next
                Session("Grid_History") = hDT
                Grid_History.DataSource = hDT
                Grid_History.DataBind()
            Else
                Grid_History.DataSource = Nothing
                Grid_History.DataBind()
            End If


        Catch ex As Exception
            Throw New Exception("Error Load Grid :" & ex.ToString)
        End Try
    End Sub
    Private Sub Grid_Manifest_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Manifest.RowCommand
        Try

            Select Case e.CommandArgs.CommandName
                Case "Delete"
                    remove_item(Grid_Manifest.GetRowValues(e.VisibleIndex, "IDM").ToString)
            End Select
        Catch ex As Exception
            Throw New Exception("Error Grid Manifest row command : " & ex.ToString)
        End Try
    End Sub
    Protected Sub Grid_Manifest_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Manifest.PreRender
        If Not Page.IsPostBack Then
            Grid_Manifest.FocusedRowIndex = -1
        End If
    End Sub

    Private Sub Grid_History_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles Grid_History.HtmlRowCreated
        Dim Tanggal As Date
        Dim ItemCome As Date
        Dim div As Integer
        Dim str As String
        Dim status As Integer
        Dim ID As String
        Dim NamaRoles As String = ""

        Try
            If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
                Return
            End If
            ID = Grid_History.GetRowValues(e.VisibleIndex, "ID").ToString
            str = " select status from MuatBarangReport where Mbr_No = '" & ID.ToString & "' "
            status = SQLExecuteScalar(str)

            str = "select R.Nama from Roles R " & _
                    "LEFT JOIN MasterUser MU on R.RoleID = MU.RoleID " & _
                    "WHERE r.[status] <> 0 " & _
                    "AND MU.[Status] <> 0 " & _
                    "AND MU.UserID = '" & Session("UserId").ToString & "' "
            NamaRoles = SQLExecuteScalar(str)

            div = DateDiff(DateInterval.Day, ItemCome, Tanggal)

            Dim butto As LinkButton = CType(Grid_History.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "tbArrive"), System.Web.UI.WebControls.LinkButton)
            If status = 1 And (NamaRoles.ToUpper = "ADMIN" Or NamaRoles.ToUpper = "MASTER ACCOUNTING") Then
                butto.Visible = True
            Else
                butto.Visible = False
            End If


            Dim butto1 As LinkButton = CType(Grid_History.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "tbResetPengiriman"), System.Web.UI.WebControls.LinkButton)
            If (NamaRoles.ToUpper = "ADMIN" Or NamaRoles.ToUpper = "MASTER ACCOUNTING") Then
                butto1.Visible = True
            Else
                butto1.Visible = False
            End If

        Catch ex As Exception
            Throw New Exception("Error new Row created " & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_History_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_History.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Arrive"
                    Call Arrive(Grid_History.GetRowValues(e.VisibleIndex, "ID").ToString)

                Case "Print"
                    If validasi_view_history() Then
                        Panel_Input.Visible = False
                        Panel_Grid.Visible = False
                        Panel_Report.Visible = True
                        historygrid.Visible = False
                        historyinput.Visible = False
                        PanelJudul.Visible = False
                        PanelPilihReport.Visible = False
                        hfIDK.Value = Grid_History.GetRowValues(e.VisibleIndex, "Kapal").ToString
                        hfHID.Value = Grid_History.GetRowValues(e.VisibleIndex, "ID").ToString
                        lblReport.Text = ""
                        lblReport.Text &= manifestHeaderhist(Grid_History.GetRowValues(e.VisibleIndex, "NoPelayaran").ToString)
                        lblReport.Text &= manifestdata(1)
                    End If

                Case "Reset"
                    If validation_reset(Grid_History.GetRowValues(e.VisibleIndex, "ID").ToString) Then
                        UpdateMuatBarang(Grid_History.GetRowValues(e.VisibleIndex, "ID").ToString)
                    End If

            End Select
        Catch ex As Exception
            Throw New Exception("Error Grid_History_RowCommand function <BR>:" & ex.ToString)
        End Try
    End Sub
    Protected Sub Grid_History_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_History.PreRender
        If Not Page.IsPostBack Then
            Grid_History.FocusedRowIndex = -1
        End If
    End Sub
    Private Sub load_grid_child(ByVal grid As ASPxGridView)
        Dim dt As DataTable
        Dim ds As DataSet
        Dim str As String


        'str = " select Cast(Ukuran as numeric(10,5)) as Ukuran,Nama_Barang,Jumlah_Colli,ID,Total_Berat  from MBRDetail where Mbr_No = '" & grid.GetMasterRowKeyValue & "' and (status = 1 or status = 5)"

        str = " select *  from MBRDetail where Mbr_No = '" & grid.GetMasterRowKeyValue & "' and (status = 1 or status = 5)"

        ds = SQLExecuteQuery(str)
        dt = ds.Tables(0)
        grid.DataSource = dt

    End Sub
    Protected Sub Grid_History_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_grid_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "VALIDATION"
    Private Function validation_reset(ByVal MBRNO As String) As Boolean
        Try
            Dim dDT As New DataTable
            Dim dDS As New DataSet
            Dim Status As String = ""

            sqlstring = "select Distinct MB_no FROM MBRDetail WHERE Mbr_No = '" & MBRNO & "' and [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    sqlstring = "SELECT [status] from MuatBarang WHERE Mb_No = '" & DT.Rows(i).Item("MB_no").ToString & "' AND [status] <> 0 "
                    Status = SQLExecuteScalar(sqlstring)

                    If Status <> "0" And Status <> "2" Then
                        Return False
                        Exit For
                        lblError.Visible = True
                        lblError.Text = "Tidak dapat direset karena sudah dianggap tiba ke tujuan"
                    End If

                Next
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Private Function validation() As Boolean
        If hfIDK.Value = "" Then
            lblError.Text = ""
            lblError.Text = "Pilih Kapal"
            lblError.Visible = True
        End If

        If ddltype.SelectedIndex = 0 Then
            lblError.Text = ""
            lblError.Text = "Pilih Tipe Report"
            lblError.Visible = True
        End If

        If DdlNamaGudang.SelectedIndex = 0 Then
            lblError.Text = ""
            lblError.Text = "Pilih Gudang "
            lblError.Visible = True
        End If

        Return True
    End Function

    Private Function validation_kapal() As Boolean
        DT = CType(Session("Grid_Manifest"), DataTable)
        lblError.Text = ""
        If ManifestDate.Text = "" Then
            lblError.Text = "Pilih Tanggal"
            lblError.Visible = True
            Return False
        End If

        If Grid_Manifest.VisibleRowCount = 0 Then
            Return False
        End If

        If DT.Rows.Count < 1 Then
            lblError.Text = " Silahkan Tambah berdasarkan kapal yg dipilih"
            lblError.Visible = True
            Return False
        End If

        If DDLDari.SelectedIndex = 0 Then
            lblError.Text = "Pilih Kota awal Kapal"
            lblError.Visible = True
            Return False
        End If
        If DDLKe.SelectedIndex = 0 Then
            lblError.Text = "Pilih Kota Tujuan Akhir Kapal"
            lblError.Visible = True
            Return False
        End If

        If DdlNamaGudang.SelectedIndex = 0 Then
            lblError.Text = "Pilih Gudang"
            lblError.Visible = True
            Return False
        End If

        Return True
    End Function

    Private Function validasi_view_history() As Boolean
        Try

            If ddltype.SelectedIndex = 0 Then
                lblerror2.Text = ""
                lblerror2.Text = "Pilih Tipe Report"
                lblerror2.Visible = True
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("error validasi history : " & ex.ToString)
        End Try

    End Function
#End Region

#Region "BUTTON"
    Protected Sub btKembaliDevPeriod_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btKembaliDevPeriod.Click
        Panel_Input.Visible = True
        Panel_Grid.Visible = True
        Panel_Report.Visible = False
        PanelJudul.Visible = True
        PanelPilihReport.Visible = True
        Session("Grid_Manifest") = Nothing
    End Sub
    'Protected Sub ViewGrid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewGrid.Click
    '    If validasi_view_history() Then
    '        load_grid()
    '        lblerror2.Visible = False
    '    End If

    'End Sub

    Protected Sub Back_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Back.Click
        Panel_Input.Visible = True
        Panel_Grid.Visible = True
        historygrid.Visible = False
        historyinput.Visible = False
        Panel_Report.Visible = False
        PanelJudul.Visible = True
        PanelPilihReport.Visible = True
    End Sub
    Protected Sub btViewHistory_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btViewHistory.Click
        Try
            clear_label()
            Panel_Input.Visible = False
            Panel_Grid.Visible = False
            historygrid.Visible = True
            historyinput.Visible = True
            Panel_Report.Visible = False
            PanelPilihReport.Visible = True
            PanelJudul.Visible = True
            PanelNamaPenerima.Visible = False
            PanelContainer.Visible = False
            DDLTipePrint.SelectedIndex = 0
            load_grid()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Protected Sub btReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btReset.Click
        create_session()
        Clear()
        hfIDK.Value = ""
    End Sub

    Protected Sub btAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAdd.Click

        Try
            If validation() Then
                load_muat_barang()
                TxtKapal.Text = hfnamakapal.Value
                Add_Clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error Button Add : " & ex.ToString)
        End Try
    End Sub

    Private Sub Arrive(ByVal id As String)
        Dim sql As String
        Dim sqlstr As String = ""
        Dim sDT As New DataTable
        
        Try
            sql = "select mbrd.Mbd_ID,mbrd.Mb_No from MuatBarangReport mbr left join MBRDetail mbrd on mbr.Mbr_No = mbrd.Mbr_No where mbr.Mbr_No = '" & id.ToString & "' and mbr.status <> 0 "
            DS = SQLExecuteQuery(sql)
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                sqlstring = ""
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        sqlstring &= " UPDATE MuatBarang " & _
                           " SET " & _
                           " LastModified = '" & Now.ToString & "', " & _
                           " [status] = 5 " & _
                           " WHERE Mb_No = '" & .Item("Mb_No").ToString & "' And [status] = 2; "

                        sqlstring &= " UPDATE MuatBarangDetail " & _
                                     " SET " & _
                                     " LastModified = '" & Now.ToString & "', " & _
                                     " [status] = 5 " & _
                                     " WHERE Mb_No= '" & .Item("Mb_No").ToString & "' And [status] = 2; "

                    End With
                   
                Next
                result = SQLExecuteNonQuery(sqlstring)

                sqlstring = " UPDATE MuatBarangReport " & _
                                " SET " & _
                                " LastModified = '" & Now.ToString & "', " & _
                                " UserName = '" & Session("UserId").ToString & "', " & _
                                " [status] = 5 " & _
                                " WHERE Mbr_No = '" & id.ToString & "' And [status] = 1; "
                sqlstring &= " UPDATE MBRDetail " & _
                                " SET " & _
                                " LastModified = '" & Now.ToString & "', " & _
                                " UserName = '" & Session("UserId").ToString & "', " & _
                                " [status] = 5 " & _
                                " WHERE Mbr_No = '" & id.ToString & "' And [status] = 1; "

                sqlstr = "select Distinct MB_no FROM MBRDetail WHERE Mbr_No = '" & id & "' and [status] = 1"
                DS = SQLExecuteQuery(sqlstr)
                DT = DS.Tables(0)

                If DT.Rows.Count > 0 Then

                    For i As Integer = 0 To DT.Rows.Count - 1
                        sqlstr = "select Distinct wd.container, wd.warehouseitem_code, wd.nama_barang from MuatBarang mb " & _
                                 " JOIN V_Warehouse_Satuan wd on mb.WarehouseHeaderID = wd.WarehouseItem_Code " & _
                                 " where mb.status = 2 and wd.status = 1 and mb.Mb_No = '" & DT.Rows(i).Item("MB_no").ToString & "' "
                        sDT = SQLExecuteQuery(sqlstr).Tables(0)

                        If sDT.Rows.Count > 0 Then
                            For j As Integer = 0 To sDT.Rows.Count - 1

                                If sDT.Rows(j).Item("container") = "true" Or sDT.Rows(j).Item("container") = "kubikasi" Or sDT.Rows(j).Item("container") = "Kubikasi" Then
                                    sqlstring &= "UPDATE ContainerHeader " & _
                                                 "SET " & _
                                                 "[statuspengiriman] = 2 where ContainerCode = '" & sDT.Rows(j).Item("nama_barang").ToString & "' and [status] <> 0;"
                                    sqlstring &= "UPDATE Warehouseheader " & _
                                                 "SET " & _
                                                 "UserName = '" & Session("UserId").ToString & "', " & _
                                                 "[statuspengiriman] = 2 where warehouseItem_Code = '" & sDT.Rows(j).Item("warehouseitem_code").ToString & "' and [status] <> 0; "
                                Else
                                    sqlstring &= "UPDATE Warehouseheader " & _
                                                 "SET " & _
                                                 "[statuspengiriman] = 2 where warehouseItem_Code = '" & sDT.Rows(j).Item("warehouseitem_code").ToString & "' and [status] <> 0; "
                                End If

                            Next

                        End If

                    Next


                End If


                result = SQLExecuteNonQuery(sqlstring)

                load_grid()

            End If
        Catch ex As Exception
            Throw New Exception("Error function Arrive : <BR> " & ex.ToString)
        End Try
    End Sub
    Protected Sub btView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btView.Click
        Dim rDT As New DataTable
        Dim mDT As New DataTable
        Dim SDT As New DataTable
        Dim ZDT As New DataTable
        Dim mDR As DataRow
        Dim sqlstring As String
        Dim mbnoboo As Boolean
        Dim beratpercustomer As Double = 0
        Dim totalvolumepercustomer As Double = 0
        Dim TOtalColiPercustomer As Double = 0
        Dim totalcoli As Double = 0
        Dim totalvolume As Double = 0
        Dim namacustomer As String = ""
        Dim namacustomerdatas As String = ""
        Dim mbr_no As String
        Dim iddetail As Integer
        Dim beratsemua As Double = 0
        Dim nopelayaran As Integer = 0
        Dim sqlstr As String = ""
        Dim strsqlstring As String = ""
        Dim zzDT As DataTable
        Dim QtyWarehouse As String = ""


        Try
            If validation_kapal() Then
                rDT = CType(Session("Grid_Manifest"), DataTable)
                mDT = create_session()
                If rDT.Rows.Count > 0 Then
                    For f As Integer = 0 To rDT.Rows.Count - 1
                        With rDT.Rows(f)
                            mbnoboo = True
                            If mDT.Rows.Count > 0 Then
                                For j As Integer = 0 To mDT.Rows.Count - 1
                                    If .Item("QuotationDetailIDM").ToString = mDT.Rows(j).Item("QuotationDetailIDM").ToString Then
                                        If mDT.Rows(j).Item("NamaBarangM").ToString.Length < 70 Then
                                            mDT.Rows(j).Item("NamaBarangM") = mDT.Rows(j).Item("NamaBarangM").ToString + "," + .Item("NamaBarangM").ToString
                                        End If
                                        mDT.Rows(j).Item("BeratM") = CDbl(mDT.Rows(j).Item("BeratM").ToString) + CDbl(.Item("BeratM").ToString)
                                        mDT.Rows(j).Item("QuantityM") = CDbl(mDT.Rows(j).Item("QuantityM").ToString) + CDbl(.Item("QuantityM").ToString)
                                        mDT.Rows(j).Item("VolumeM") = CDbl(mDT.Rows(j).Item("VolumeM").ToString) + CDbl(.Item("VolumeM").ToString)
                                        mDT.Rows(j).Item("JumlahColliM") = (CDbl(mDT.Rows(j).Item("QuantityM").ToString)).ToString + " Container"
                                        mbnoboo = False
                                        Exit For
                                    End If
                                Next
                                If mbnoboo = True Then
                                    mDR = mDT.NewRow
                                    mDR("MB_IDM") = .Item("MB_IDM").ToString
                                    mDR("MB_NOM") = .Item("MB_NOM").ToString
                                    mDR("NoContainerM") = .Item("NoContainerM").ToString
                                    mDR("CustomerM") = .Item("CustomerM").ToString
                                    mDR("PenerimaM") = .Item("PenerimaM").ToString
                                    mDR("MerkM") = .Item("MerkM").ToString
                                    mDR("JumlahColliM") = .Item("JumlahColliM").ToString
                                    mDR("NamaBarangM") = .Item("NamaBarangM").ToString
                                    mDR("BeratM") = .Item("BeratM").ToString
                                    mDR("UkuranM") = .Item("UkuranM").ToString
                                    mDR("Nama_KapalM") = .Item("Nama_KapalM").ToString
                                    mDR("KapalM") = .Item("KapalM").ToString
                                    mDR("KeteranganM") = .Item("KeteranganM").ToString
                                    mDR("PanjangM") = .Item("PanjangM").ToString
                                    mDR("LebarM") = .Item("LebarM").ToString
                                    mDR("TinggiM") = .Item("TinggiM").ToString
                                    If .Item("VolumeM").ToString = "" Then
                                        mDR("VolumeM") = "0"
                                    Else
                                        mDR("VolumeM") = .Item("VolumeM").ToString
                                    End If

                                    mDR("QuantityM") = .Item("QuantityM").ToString
                                    mDR("QuotationDetailIDM") = .Item("QuotationDetailIDM").ToString
                                    mDR("NoSealM") = .Item("NoSealM").ToString
                                    mDR("WarehouseHeaderIDM") = .Item("WarehouseHeaderIDM").ToString
                                    mDR("IDDetailWarehouseDetail") = .Item("IDDetailWarehouseDetail").ToString
                                    mDT.Rows.Add(mDR)
                                End If
                            Else
                                mDR = mDT.NewRow
                                mDR("MB_IDM") = .Item("MB_IDM").ToString
                                mDR("MB_NOM") = .Item("MB_NOM").ToString
                                mDR("NoContainerM") = .Item("NoContainerM").ToString
                                mDR("CustomerM") = .Item("CustomerM").ToString
                                mDR("PenerimaM") = .Item("PenerimaM").ToString
                                mDR("MerkM") = .Item("MerkM").ToString
                                mDR("JumlahColliM") = .Item("JumlahColliM").ToString
                                mDR("NamaBarangM") = .Item("NamaBarangM").ToString
                                mDR("BeratM") = .Item("BeratM").ToString
                                mDR("UkuranM") = .Item("UkuranM").ToString
                                mDR("Nama_KapalM") = .Item("Nama_KapalM").ToString
                                mDR("KapalM") = .Item("KapalM").ToString
                                mDR("KeteranganM") = .Item("KeteranganM").ToString
                                mDR("PanjangM") = .Item("PanjangM").ToString
                                mDR("LebarM") = .Item("LebarM").ToString
                                mDR("TinggiM") = .Item("TinggiM").ToString
                                If .Item("VolumeM").ToString = "" Then
                                    mDR("VolumeM") = "0"
                                Else
                                    mDR("VolumeM") = .Item("VolumeM").ToString
                                End If

                                mDR("QuantityM") = .Item("QuantityM").ToString
                                mDR("QuotationDetailIDM") = .Item("QuotationDetailIDM").ToString
                                mDR("NoSealM") = .Item("NoSealM").ToString
                                mDR("WarehouseHeaderIDM") = .Item("WarehouseHeaderIDM").ToString
                                mDR("IDDetailWarehouseDetail") = .Item("IDDetailWarehouseDetail").ToString
                                mDT.Rows.Add(mDR)

                            End If
                        End With
                    Next
                End If
                

                mbr_no = load_mb_number()
                iddetail = 0

                If rDT.Rows.Count > 0 Then
                    nopelayaran = getNoPelayaran("MuatBarangReport", "Kapal", hfIDK.Value, "1/1/" + ManifestDate.Date.ToString("yyyy"), "12/31/" + ManifestDate.Date.ToString("yyyy")) + 1
                    sqlstring = "set nocount on; declare @id as int;Insert into MuatBarangReport(Kapal,Mbr_No,Depart_Date,Dari,Tujuan,NoPelayaran,UserName,[status]) values " & _
                    "('" & hfIDK.Value & "','" & mbr_no & "','" & ManifestDate.Text.ToString & "' , '" & DDLDari.SelectedValue & "', " & _
                    " '" & DDLKe.SelectedValue & "'," & nopelayaran & ",'" & Session("UserId").ToString & "', 1); " & " select @id = @@identity; "
                    For c As Integer = 0 To rDT.Rows.Count - 1
                        iddetail = iddetail + 1
                        With rDT.Rows(c)

                            sqlstring &= "Insert into MBRDetail(Mbr_ID,Mbd_ID,IDDetail,Mbr_No,Mb_No,NoContainer,Depart_Date,Nama_Barang,Ukuran,Total_Berat,NoPelayaran,Jumlah_Colli,UserName,[status], NoSeal)" & _
                                        " values ( @id" & ", '" & .Item("MB_IDM").ToString & "'," & iddetail & ", '" & mbr_no & "','" & .Item("Mb_NoM").ToString & "','" & .Item("NoContainerM").ToString & "','" & ManifestDate.Text.ToString & "','" & .Item("NamaBarangM").ToString.Replace("'", "''") & "', " & _
                                        " '" & .Item("VolumeM").ToString & "','" & .Item("BeratM").ToString & "'," & nopelayaran & ",'" & .Item("JumlahColliM").ToString & "'," & _
                                        " '" & Session("UserId").ToString & "', 1, '" & .Item("NoSealM").ToString & "');"

                            sqlstring &= " UPDATE MuatBarang " & _
                                        " SET " & _
                                        " LastModified = '" & Now.ToString & "', " & _
                                        " [status] = 2 " & _
                                        " WHERE Mb_No =  '" & .Item("MB_NOM").ToString & "' and [status] <> 0 ; "

                            sqlstring &= " UPDATE MuatBarangDetail " & _
                                         " SET " & _
                                         " LastModified = '" & Now.ToString & "', " & _
                                         " [status] = 2 " & _
                                         " WHERE Mb_No=  '" & .Item("MB_NOM").ToString & "' and [status] <> 0; "

                            sqlstr = "Select Quantity FROM WarehouseDetail WHERE " & _
                                     "WarehouseItem_Code = '" & .Item("WarehouseHeaderIDM") & "' " & _
                                     "And [status] <> 0 and IDDetail = " & .Item("IDDetailWarehouseDetail") & " "
                            QtyWarehouse = SQLExecuteScalar(sqlstr)

                            If CDbl(QtyWarehouse) = 0 Then
                                sqlstring &= " UPDATE WarehouseDetail " & _
                                             " SET [statuspengiriman] = 1 " & _
                                             " WHERE WarehouseItem_Code = '" & .Item("WarehouseHeaderIDM") & "' And [status] <> 0 and IDDetail = " & .Item("IDDetailWarehouseDetail") & ";"
                            End If

                        End With
                    Next

                    sqlstr = " select Distinct wd.WarehouseItem_Code " & _
                             " from " & _
                             " MuatBarang mb left join MuatBarangDetail mbd on (mb.Mb_No = mbd.Mb_No  ) " & _
                             " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID) " & _
                             " left join Kapal mk on (mb.Kapal = mk.IDDetail  ) " & _
                             " left join MasterCustomer mc on (mb.Customer_Id = mc.Kode_Customer ) " & _
                             " where mb.status = 1 And mb.Kapal = '" & hfIDK.Value & "' and wd.Warehouse_Code = '" & DdlNamaGudang.SelectedValue & "' " & _
                             " and mbd.status <> 0 and wd.statusheader <> 0 and mc.status = 1 and mk.status = 1 "
                    SDT = SQLExecuteQuery(sqlstr).Tables(0)



                    If SDT.Rows.Count > 0 Then
                        Dim hslQtyGudang As String = ""
                        'untuk ngecek quantity nya 

                        'For b As Integer = 0 To SDT.Rows.Count - 1
                        '    sqlstr = "SELECT Quantity FROM WarehouseDetail Where WarehouseItem_Code = '" & SDT.Rows(b).Item("WarehouseItem_Code").ToString & "' AND [status] <> 0 "
                        '    ZDT = SQLExecuteQuery(sqlstr).Tables(0)

                        '    If ZDT.Rows.Count > 0 Then
                        '        Dim cek As Integer = 0

                        '        'ngecek quantity di warehouse. kalo udah 0 diupdateheadernya
                        '        For c As Integer = 0 To ZDT.Rows.Count - 1
                        '            If CInt(ZDT.Rows(c).Item("Quantity")) > 0 Then
                        '                cek = cek + 1
                        '            End If
                        '        Next

                        '        If cek = 0 Then
                        '            sqlstring &= "UPDATE WarehouseHeader " & _
                        '                         "SET " & _
                        '                         "[statuspengiriman] = 1 " & _
                        '                         "WHERE WarehouseItem_Code = '" & SDT.Rows(b).Item("WarehouseItem_Code") & "' And [status] <> 0;"
                        '        End If

                        '    End If
                        'Next

                        For b As Integer = 0 To SDT.Rows.Count - 1
                            sqlstr = "SELECT SUM(Quantity) FROM WarehouseDetail Where WarehouseItem_Code = '" & SDT.Rows(b).Item("WarehouseItem_Code").ToString & "' AND [status] <> 0 "
                            hslQtyGudang = SQLExecuteScalar(sqlstr)

                            If hslQtyGudang <> "" Then
                                If CInt(hslQtyGudang) <= 0 Then
                                    sqlstring &= "UPDATE WarehouseHeader " & _
                                                 "SET " & _
                                                 "statuspengiriman = 1 " & _
                                                 "WHERE WarehouseItem_Code = '" & SDT.Rows(b).Item("WarehouseItem_Code") & "' " & _
                                                 "And [status] <> 0;"
                                End If
                            End If
                        Next

                    End If



                    sqlstr = " select wd.Nama_Barang " & _
                             " from " & _
                             " MuatBarang mb left join MuatBarangDetail mbd on (mb.Mb_No = mbd.Mb_No  ) " & _
                             " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID) " & _
                             " left join Kapal mk on (mb.Kapal = mk.IDDetail  ) " & _
                             " left join MasterCustomer mc on (mb.Customer_Id = mc.Kode_Customer ) " & _
                             " where mb.status = 1 And mb.Kapal = '" & hfIDK.Value & "' and wd.Warehouse_Code = '" & DdlNamaGudang.SelectedValue & "' " & _
                             " and mbd.status = 1 and wd.statusheader <> 0 and mc.status = 1 and mk.status = 1 and wd.container = 'true' "
                    SDT = SQLExecuteQuery(sqlstr).Tables(0)

                    If SDT.Rows.Count > 0 Then
                        For i As Integer = 0 To SDT.Rows.Count - 1
                            sqlstring &= "UPDATE ContainerHeader " & _
                                         "SET " & _
                                         "[statuspengiriman] = 1 " & _
                                         "WHERE ContainerCode = '" & SDT.Rows(i).Item("Nama_Barang").ToString & "' AND [status] <> 0;"
                        Next
                    End If

                    result = SQLExecuteNonQuery(sqlstring)

                    If result <> 0 Then
                        sqlstring = ""
                        Dim nomor As Integer = 1

                        strsqlstring = "Select mbrd.NoContainer,mbrd.Nama_Barang,mbrd.Total_Berat,wd.Merk,wd.Keterangan, mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Penerima, " & _
                                " mbrd.Ukuran,mbrd.Jumlah_Colli, wd.Panjang, wd.Lebar,wd.Tinggi,mbd.Quantity,wd.QuotationDetailID,mb.Mb_No,wd.NamaSupplier, mbrd.NoSeal  from MBRDetail mbrd" & _
                                " left join MuatBarangDetail mbd on (mbrd.Mbd_ID = mbd.IDDetail and mbd.Mb_No =mbrd.Mb_No ) " & _
                                "  left join MuatBarang mb on (mbd.Mb_No = mb.Mb_No) " & _
                                " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID ) " & _
                                "  left join MasterCustomer mc on (mb.Customer_Id = mc.Kode_Customer ) " & _
                                " where mbrd.Mbr_No = '" & mbr_no & "' and (mbrd.status = 1 or mbrd.status = 5) and mbd.status <>0 " & _
                                " and mb.status <> 0 and mc.status = 1 and wd.status <> 0 order by mbrd.Mb_No, mc.Nama_Customer"
                        zzDT = SQLExecuteQuery(strsqlstring).Tables(0)

                        If zzDT.Rows.Count > 0 Then

                            Dim nocont = zzDT.Rows(0).Item("Mb_No").ToString

                            For k As Integer = 0 To zzDT.Rows.Count - 1
                                With zzDT.Rows(k)
                                    If k = 0 Then
                                        sqlstring &= " UPDATE MuatBarang " & _
                                              " SET " & _
                                              " NoKonosemen = " & nomor & " , " & _
                                              " LastModified = '" & Now.ToString & "' " & _
                                              " WHERE Mb_No =  '" & .Item("MB_No").ToString & "' And [status] <> 0; "
                                        nocont = zzDT.Rows(k).Item("Mb_No").ToString

                                    ElseIf k = zzDT.Rows.Count - 1 Then
                                        If nocont = zzDT.Rows(k).Item("Mb_No").ToString Then
                                            nocont = zzDT.Rows(k).Item("Mb_No").ToString
                                        Else
                                            sqlstring &= " UPDATE MuatBarang " & _
                                              " SET " & _
                                              " NoKonosemen = " & nomor + 1 & " , " & _
                                              " LastModified = '" & Now.ToString & "' " & _
                                              " WHERE Mb_No =  '" & .Item("MB_No").ToString & "' And [status] <> 0; "
                                            nocont = zzDT.Rows(k).Item("Mb_No").ToString
                                            nomor = nomor + 1
                                        End If

                                    Else

                                        If nocont = zzDT.Rows(k).Item("Mb_No").ToString Then
                                            nocont = zzDT.Rows(k).Item("Mb_No").ToString
                                        Else
                                            sqlstring &= " UPDATE MuatBarang " & _
                                          " SET " & _
                                          " NoKonosemen = " & nomor + 1 & " , " & _
                                          " LastModified = '" & Now.ToString & "' " & _
                                          " WHERE Mb_No =  '" & .Item("MB_No").ToString & "' And [status] <> 0; "
                                            nocont = zzDT.Rows(k).Item("Mb_No").ToString
                                            nomor = nomor + 1
                                        End If



                                    End If



                                End With

                            Next
                        End If

                        SQLExecuteNonQuery(sqlstring)

                        Clear()
                        lblError.Visible = True
                        lblError.Text = "Pembuatan Surat Muat berhasil"
                        Session("Grid_Manifest") = Nothing
                        Grid_Manifest.DataSource = Nothing
                        Grid_Manifest.DataBind()
                    End If
                End If

            End If



        Catch ex As Exception
            Throw New Exception("Error View Report : " & ex.ToString)
        End Try

    End Sub

    Protected Sub DDLTipePrint_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLTipePrint.SelectedIndexChanged
        Try
            If DDLTipePrint.SelectedValue = "SemuaKapal" Then
                PanelNamaPenerima.Visible = False
                historygrid.Visible = True
                PanelContainer.Visible = False
                PanelInputKontainer.Visible = False
            ElseIf DDLTipePrint.SelectedValue = "Stuffing" Then
                historygrid.Visible = False
                PanelNamaPenerima.Visible = False
                PanelContainer.Visible = True
                PanelInputKontainer.Visible = False
            ElseIf DDLTipePrint.SelectedValue = "InputContainer" Then
                PanelNamaPenerima.Visible = False
                PanelInputKontainer.Visible = True
                historygrid.Visible = False
                PanelContainer.Visible = False
            Else
                historygrid.Visible = False
                PanelNamaPenerima.Visible = True
                PanelContainer.Visible = False
                PanelInputKontainer.Visible = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Protected Sub BtnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnPrint.Click
        Try
            If validasi_view_history() Then
                Panel_Input.Visible = False
                Panel_Grid.Visible = False
                Panel_Report.Visible = True
                historygrid.Visible = False
                historyinput.Visible = False
                PanelJudul.Visible = False
                PanelPilihReport.Visible = False
                PanelNamaPenerima.Visible = False
                PanelContainer.Visible = False
                lblReport.Text = ""
                lblReport.Text &= manifestHeaderhist(hfnopelayaran.Value)
                lblReport.Text &= manifestdata(2)
            End If
            
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Protected Sub BtnView2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnView2.Click
        Try
            If validasi_view_history() Then
                Panel_Input.Visible = False
                Panel_Grid.Visible = False
                Panel_Report.Visible = True
                historygrid.Visible = False
                historyinput.Visible = False
                PanelJudul.Visible = False
                PanelPilihReport.Visible = False
                PanelNamaPenerima.Visible = False
                PanelContainer.Visible = False
                lblReport.Text = ""
                lblReport.Text &= manifestHeaderhist(hfnopelayaran.Value)
                lblReport.Text &= manifestdata(3)
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Protected Sub BtnPrintInputContainer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnPrintInputContainer.Click
        Try
            If validasi_view_history() Then
                Panel_Input.Visible = False
                Panel_Grid.Visible = False
                Panel_Report.Visible = True
                historygrid.Visible = False
                historyinput.Visible = False
                PanelInputKontainer.Visible = False
                PanelJudul.Visible = False
                PanelPilihReport.Visible = False
                PanelNamaPenerima.Visible = False
                PanelContainer.Visible = False
                lblReport.Text = ""
                lblReport.Text &= manifestHeaderhist(hfnopelayaran.Value)
                lblReport.Text &= manifestdata(4)
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

#End Region

#Region "METHOD"

    Private Sub UpdateMuatBarang(ByVal MBRNO As String)
        Try
            Dim sqlstr As String = ""
            Dim warehouseno As String = ""
            Dim sDT As New DataTable


            sqlstring = "select Distinct MB_no FROM MBRDetail WHERE Mbr_No = '" & MBRNO & "' and [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                sqlstring = ""
                For i As Integer = 0 To DT.Rows.Count - 1
                    sqlstr = "select wd.container, wd.warehouseitem_code, wd.nama_barang from MuatBarang MB " & _
                             "JOIN MuatBarangDetail MBD ON MB.Mb_No = MBD.Mb_No " & _
                             "JOIN WarehouseDetail WD on (MB.WarehouseHeaderID = WD.WarehouseItem_Code and MBD.Warehouse_Id = WD.IDDetail) " & _
                             "where mbd.status <> 0 and wd.status = 1 and mbd.Mb_No = '" & DT.Rows(i).Item("MB_no").ToString & "' "
                    sDT = SQLExecuteQuery(sqlstr).Tables(0)

                    If sDT.Rows.Count > 0 Then
                        For j As Integer = 0 To sDT.Rows.Count - 1

                            If sDT.Rows(j).Item("container") = "true" Or sDT.Rows(j).Item("container") = "Kubikasi" Then
                                sqlstring &= "UPDATE ContainerHeader " & _
                                             "SET " & _
                                             "[statuspengiriman] = 0 where ContainerCode = '" & sDT.Rows(j).Item("nama_barang").ToString & "' and [status] <> 0;"
                                sqlstring &= "UPDATE Warehouseheader " & _
                                             "SET " & _
                                             "[statuspengiriman] = 0 where warehouseItem_Code = '" & sDT.Rows(j).Item("warehouseitem_code").ToString & "' and [status] <> 0; "
                                sqlstring &= "UPDATE Warehousedetail " & _
                                             "SET " & _
                                             "[statuspengiriman] = 0 where warehouseItem_Code = '" & sDT.Rows(j).Item("warehouseitem_code").ToString & "' and [status] <> 0; "
                            Else
                                sqlstring &= "UPDATE Warehouseheader " & _
                                             "SET " & _
                                             "[statuspengiriman] = 0 where warehouseItem_Code = '" & sDT.Rows(j).Item("warehouseitem_code").ToString & "' and [status] <> 0; "
                                sqlstring &= "UPDATE Warehousedetail " & _
                                             "SET " & _
                                             "[statuspengiriman] = 0 where warehouseItem_Code = '" & sDT.Rows(j).Item("warehouseitem_code").ToString & "' and [status] <> 0; "
                            End If

                        Next

                    End If


                    sqlstring &= "UPDATE MuatBarang SET  [status] = 1 WHERE Mb_No = '" & DT.Rows(i).Item("MB_no").ToString & "' and [status] <> 0;"
                    sqlstring &= "UPDATE MuatBarangDetail SET [status] = 1 WHERE Mb_No = '" & DT.Rows(i).Item("MB_no").ToString & "' and [status] <> 0;"
                    sqlstring &= "Delete BillLand Where Mb_ID = '" & DT.Rows(i).Item("MB_no").ToString & "';"
                    sqlstring &= "Delete BAST Where Mb_ID = '" & DT.Rows(i).Item("MB_no").ToString & "';"
                Next
                sqlstring &= "DELETE MUATBARANGREPORT WHERE Mbr_No = '" & MBRNO & "';Delete MBRdetail where mbr_no = '" & MBRNO & "';"

            End If



            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil <> 0 Then

                Session("Grid_Manifest") = Nothing
                Session("Grid_History") = Nothing
                load_grid()
                lblBerhasil.Visible = True
                lblBerhasil.Text = "Berhasil di Reset, Silahkan ulang proses pengiriman barang"
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Function manifestHeaderhist(ByVal nopelayaran As Integer) As String
        Dim HeaderReport As String
        Dim hST As String
        Dim hDT As DataTable
        Dim hDS As DataSet
        Dim kST As String
        Dim kDT As DataTable
        Dim kDS As DataSet
        Dim hisDT As DataTable
        Dim hisDS As DataSet
        Dim hisString As String
        Dim sesHeader As String
        Dim NamaPerusahaan As String
        Try

            sesHeader = ddlHeaderHist.SelectedValue
            hST = "Select * from HeaderForm where ID = '" & sesHeader & "' and status = 1;"
            hDS = SQLExecuteQuery(hST)
            hDT = hDS.Tables(0)
            kST = " select * from Kapal where IDDetail = '" & hfIDK.Value & "' and status = 1"
            kDS = SQLExecuteQuery(kST)
            kDT = kDS.Tables(0)
            hisString = " select * from MuatBarangReport where Mbr_No = '" & hfHID.Value & "' and (status = 5 or status = 1 ) "
            hisDS = SQLExecuteQuery(hisString)
            hisDT = hisDS.Tables(0)
            NamaPerusahaan = hDT.Rows(0).Item("Nama").ToString

            If hDT.Rows.Count > 0 And kDT.Rows.Count > 0 And hisDT.Rows.Count > 0 Then

                HeaderReport = "<table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;table-layout:fixed;"">" & _
                      " <tr>" & _
                      "     <td style=""width:60px;vertical-align:top;"" colspan=2 align=center  >" & _
                      "         <img src=""" & hDT.Rows(0).Item("PathLogo").ToString & """ style=""height: 100px; width: 100px"" />" & _
                      "     </td>" & _
                      "     <td style=""width:1380px;vertical-align:top;#2c3848;"" colspan=2 align=center >" & _
                      "         <table width=1380px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
                      "             <tr> " & _
                      "                <td colspan=2 align=center>" & _
                      "                     <font size=""4""><b>" & hDT.Rows(0).Item("Jenis").ToString & "</b>" & _
                      "                </td>" & _
                      "             </tr>"
                If NamaPerusahaan = "PT. Ligita Jaya" Or NamaPerusahaan = "PT.Ligita Jaya" Then
                    HeaderReport &= "             <tr>" & _
                                                    "                 <td colspan=2 align=center>" & _
                                                    "                     <font size=""7""><b>" & hDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                                    "                 </td>" & _
                                                    "             </tr>"
                Else
                    HeaderReport &= "             <tr>" & _
                                                    "                 <td colspan=2 align=center>" & _
                                                    "                     <font size=""7""><b>" & hDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                                    "                 </td>" & _
                                                    "             </tr>"
                End If

                If ddltype.SelectedValue = "SuratMuat" Then
                    HeaderReport &= "             <tr>" & _
                                                  "                 <td colspan=2 align=center>" & _
                                                  "                     <font size=""3""><b>SURAT MUAT BARANG</b>" & _
                                                  "                 </td>" & _
                                                  "             </tr>" & _
                                                  "         </table>" & _
                                                  "     </td>" & _
                                                  "     <td style=""width:60px;vertical-align:top;"" colspan=2 align=center  >" & _
                                                  "      .  &nbsp;" & _
                                                  "     </td>" & _
                                                  " </tr>" & _
                                                  " <tr>" & _
                                                  "     <td>" & _
                                                  "         <table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
                                                  "             <tr> " & _
                                                  "                <td colspan=2 align=left style=""font-family:verdana;font-size:12px;"">" & _
                                               "                     <b>Reis :" & nopelayaran.ToString & " </b>" & _
                                                 "                </td>" & _
                                                  "             </tr>" & _
                                                  "             <tr>" & _
                                                  "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                                  "                     Kapal : <b>" & kDT.Rows(0).Item("Nama_Kapal").ToString & " </b> " & _
                                                  "                 </td>" & _
                                                  "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                                  "                     Dari : <b>" & hisDT.Rows(0).Item("Dari") & " </b> " & _
                                                  "                 </td>" & _
                                                  "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                                  "                     Ke : <b>" & hisDT.Rows(0).Item("Tujuan") & " </b> " & _
                                                  "                 </td>" & _
                                                  "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                                  "                     Nahkoda : <b>" & kDT.Rows(0).Item("Nahkoda_Kapal").ToString & " </b> " & _
                                                  "                 </td>" & _
                                                  "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                                  "                     Tanggal jalan : <b>" & CDate(hisDT.Rows(0).Item("Depart_Date")).ToString("dd MMMM yyy") & " </b> " & _
                                                  "                 </td>" & _
                                                  "             </tr>" & _
                                                  "         </table>" & _
                                                  "     </td>" & _
                                                  " </tr>" & _
                                                  " <tr>" & _
                                                  "   <td style=""padding-right:4px;"" valign=top>" & _
                                                  "     <table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:13px;table-layout:fixed;"">" & _
                                                  "       <tr>" & _
                                                  "         <td align=center style=""width: 20px; border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>NO</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 50px; border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>No Cont</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 50px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>NoSeal<br>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 100px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>PENGIRIM</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 100px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>PENERIMA</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 50px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>JUMLAH COLLI</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 200px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>JENIS BARANG</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>BERAT</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>PANJANG</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>LEBAR</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>TINGGI</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>VOLUME</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 65px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>KETERANGAN</b>" & _
                                                  "         </td>" & _
                                                  "       </tr>"
                Else
                    HeaderReport &= "             <tr>" & _
                                                  "                 <td colspan=2 align=center>" & _
                                                  "                     <font size=""3""><b>DAFTAR MANIFEST</b>" & _
                                                  "                 </td>" & _
                                                  "             </tr>" & _
                                                  "         </table>" & _
                                                  "     </td>" & _
                                                  "     <td style=""width:60px;vertical-align:top;"" colspan=2 align=center  >" & _
                                                  "      .  &nbsp;" & _
                                                  "     </td>" & _
                                                  " </tr>" & _
                                                  " <tr>" & _
                                                  "     <td>" & _
                                                  "         <table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
                                                  "             <tr> " & _
                                                  "                <td colspan=2 align=left>" & _
                                               "                     <b>Reis :" & nopelayaran.ToString & " </b>" & _
                                                  "                </td>" & _
                                                  "             </tr>" & _
                                                  "             <tr>" & _
                                                  "                 <td colspan=2 align=center >" & _
                                                  "                     Kapal : <b>" & kDT.Rows(0).Item("Nama_Kapal").ToString & " </b> " & _
                                                  "                 </td>" & _
                                                  "                 <td colspan=2 align=center >" & _
                                                  "                     Dari : <b>" & hisDT.Rows(0).Item("Dari") & " </b> " & _
                                                  "                 </td>" & _
                                                  "                 <td colspan=2 align=center >" & _
                                                  "                     Ke : <b>" & hisDT.Rows(0).Item("Tujuan") & " </b> " & _
                                                  "                 </td>" & _
                                                  "                 <td colspan=2 align=center >" & _
                                                  "                     Nahkoda : <b>" & kDT.Rows(0).Item("Nahkoda_Kapal").ToString & " </b> " & _
                                                  "                 </td>" & _
                                                  "                 <td colspan=2 align=center >" & _
                                                  "                     Tanggal jalan : <b>" & CDate(hisDT.Rows(0).Item("Depart_Date")).ToString("dd MMMM yyy") & " </b> " & _
                                                  "                 </td>" & _
                                                  "             </tr>" & _
                                                  "         </table>" & _
                                                  "     </td>" & _
                                                  " </tr>" & _
                                                  " <tr>" & _
                                                  "   <td style=""padding-right:4px;"" valign=top>" & _
                                                  "     <table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:13px;table-layout:fixed;"">" & _
                                                  "       <tr>" & _
                                                  "         <td align=center style=""width: 20px; border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>NO</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 50px; border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>No Cont</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 50px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>No Seal<br>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 100px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>PENGIRIM</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 100px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>PENERIMA</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 50px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>JUMLAH COLLI</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 240px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>JENIS BARANG</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>BERAT</b>" & _
                                                  "         </td>" & _
                                                  "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                                  "           <b>VOLUME</b>" & _
                                                  "         </td>" & _
                                                  "       </tr>"
                End If
            End If

        Catch ex As Exception
            Throw New Exception("error manifestheader function : " & ex.ToString)
        End Try
        Return HeaderReport
    End Function
    Private Function create_session_history() As DataTable
        Dim mdt As New DataTable
        Try
            mdt.Columns.Add(New DataColumn("NoContainer", GetType(String)))
            mdt.Columns.Add(New DataColumn("Nama_Barang", GetType(String)))
            mdt.Columns.Add(New DataColumn("Total_Berat", GetType(String)))
            mdt.Columns.Add(New DataColumn("Merk", GetType(String)))
            mdt.Columns.Add(New DataColumn("Keterangan", GetType(String)))
            mdt.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            mdt.Columns.Add(New DataColumn("Jumlah_Colli", GetType(String)))
            mdt.Columns.Add(New DataColumn("Penerima", GetType(String)))
            mdt.Columns.Add(New DataColumn("Ukuran", GetType(String)))
            mdt.Columns.Add(New DataColumn("Panjang", GetType(String)))
            mdt.Columns.Add(New DataColumn("Lebar", GetType(String)))
            mdt.Columns.Add(New DataColumn("Tinggi", GetType(String)))
            mdt.Columns.Add(New DataColumn("Quantity", GetType(String)))
            mdt.Columns.Add(New DataColumn("QuotationDetailID", GetType(String)))
            mdt.Columns.Add(New DataColumn("Mb_No", GetType(String)))
            mdt.Columns.Add(New DataColumn("NoSeal", GetType(String)))

            Return mdt
        Catch ex As Exception
            Throw New Exception(" error function create_session :" & ex.ToString)
        End Try

    End Function
    Private Function manifestdata(ByVal penanda As Integer) As String
        Dim dataManifest As String
        Dim rDT As New DataTable
        Dim BorderBottom As String
        Dim sqlstring As String
        Dim cekDS As DataSet
        dataManifest = ""
        Dim berattotal As Double
        Dim beratsementara As Double
        Dim beratpercustomer As Double = 0
        Dim totalvolumepercustomer As Double = 0
        Dim TOtalColiPercustomer As Double = 0
        Dim totalcoli As Double = 0
        Dim totalvolume As Double = 0
        Dim namacustomer As String = ""
        Dim namacustomerdatas As String = ""
        Dim nocont As String = ""
        Dim index As Integer
        Dim mDT As New DataTable
        Dim mDR As DataRow
        Dim mbnoboo As Boolean
        Dim flag As Integer = 0
        Dim nomor As Integer = 1


        Try
            If validasi_view_history() Then
                mDT = create_session_history()

                If penanda = 1 Then
                    If ddltype.SelectedValue = "Manifest" Then
                        sqlstring = "Select mbrd.NoContainer,mbrd.Nama_Barang,mbrd.Total_Berat,wd.Merk,wd.Keterangan, mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Penerima, " & _
                                " mbrd.Ukuran,mbrd.Jumlah_Colli, wd.Panjang, wd.Lebar,wd.Tinggi,mbd.Quantity,wd.QuotationDetailID,mb.Mb_No,wd.NamaSupplier, mbrd.NoSeal  from MBRDetail mbrd" & _
                                " left join MuatBarangDetail mbd on (mbrd.Mbd_ID = mbd.IDDetail and mbd.Mb_No =mbrd.Mb_No ) " & _
                                "  left join MuatBarang mb on (mbd.Mb_No = mb.Mb_No) " & _
                                " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID ) " & _
                                "  left join MasterCustomer mc on (mb.Customer_Id = mc.Kode_Customer ) " & _
                                " where (mbrd.status = 1 or mbrd.status = 5) and mbrd.Mbr_No = '" & hfHID.Value & "' and mbd.status <>0 " & _
                                " and mb.status <> 0 and mc.status = 1 and wd.status <> 0 order by mbrd.Mb_No, mc.Nama_Customer"
                    Else

                        sqlstring = "Select mbrd.NoContainer,mbrd.Nama_Barang,mbrd.Total_Berat,wd.Merk,wd.Keterangan, mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mbd.Penerima, " & _
                                    "mbrd.Ukuran, mbrd.Jumlah_Colli, wd.Panjang, wd.Lebar, wd.Tinggi, mbd.Quantity, wd.QuotationDetailID, mbd.Mb_No, wd.NamaSupplier, mbrd.NoSeal, mbd.IDDetail " & _
                                    "from MBRDetail mbrd " & _
                                    "LEFT JOIN V_MuatBarang_Detail mbd ON (mbd.Mb_No = mbrd.Mb_No and mbrd.Mbd_ID = mbd.IDDetail) " & _
                                    "LEFT JOIN V_Warehouse_Satuan wd ON (mbd.WarehouseHeaderID = wd.WarehouseItem_Code and mbd.Warehouse_Id = wd.IDDetail ) " & _
                                    "left join MasterCustomer mc on (mbd.Customer_Id = mc.Kode_Customer )   " & _
                                    "where (mbrd.status = 1 or mbrd.status = 5) and mbrd.Mbr_No = '" & hfHID.Value & "' and mbd.[status] <> 0  " & _
                                    "and mc.status = 1 and wd.status <> 0 order by mbd.penerima,wd.NamaSupplier, mbrd.NoContainer"

                    End If

                ElseIf penanda = 3 Then
                    sqlstring = "Select mbrd.NoContainer,mbrd.Nama_Barang,mbrd.Total_Berat,wd.Merk,wd.Keterangan,mc.Jenis_Perusahaan + mc.Nama_Customer as Nama_Customer,mb.Penerima, " & _
                                " mbrd.Ukuran,mbrd.Jumlah_Colli, wd.Panjang, wd.Lebar,wd.Tinggi,mbd.Quantity,wd.QuotationDetailID,mb.Mb_No,wd.NamaSupplier, mbrd.NoSeal  from MBRDetail mbrd" & _
                                " left join MuatBarangDetail mbd on (mbrd.Mbd_ID = mbd.IDDetail and mbd.Mb_No =mbrd.Mb_No ) " & _
                                "  left join MuatBarang mb on (mbd.Mb_No = mb.Mb_No) " & _
                                " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID ) " & _
                                "  left join MasterCustomer mc on (mb.Customer_Id = mc.Kode_Customer ) " & _
                                " where mbrd.Mbr_No = '" & hfHID.Value & "' and (mbrd.status = 1 or mbrd.status = 5) and mbd.status <> 0 " & _
                                " and mb.status <> 0 and mc.status = 1 and wd.status <> 0 and mbd.PackedContID <> '0' and mbd.PackedContID <> '--' " & _
                                " and mbrd.NoPelayaran = " & hfnopelayaran.Value & " and mbd.PackedContID = '" & HfContStuffing.Value & "' and mb.kapal = " & HFIDKapal.Value & " " & _
                                "order by mb.penerima,wd.NamaSupplier, mbrd.NoContainer"
                ElseIf penanda = 4 Then
                    sqlstring = "Select mbrd.NoContainer,mbrd.Nama_Barang,mbrd.Total_Berat,wd.Merk,wd.Keterangan,mc.Jenis_Perusahaan + mc.Nama_Customer as Nama_Customer,mb.Penerima, " & _
                                "mbrd.Ukuran, mbrd.Jumlah_Colli, wd.Panjang, wd.Lebar, wd.Tinggi, mbd.Quantity, wd.QuotationDetailID, mb.Mb_No, wd.NamaSupplier, mbrd.NoSeal " & _
                                "from MBRDetail mbrd  " & _
                                "left join MuatBarangDetail mbd on (mbrd.Mbd_ID = mbd.IDDetail and mbd.Mb_No =mbrd.Mb_No ) " & _
                                "left join MuatBarang mb on (mbd.Mb_No = mb.Mb_No) " & _
                                "left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID )   " & _
                                "left join MasterCustomer mc on (mb.Customer_Id = mc.Kode_Customer ) " & _
                                "left join Containerheader ch on wd.Nama_Barang = ch.ContainerCode  " & _
                                "where mbrd.Mbr_No = '" & hfHID.Value & "' and (mbrd.status = 1 or mbrd.status = 5) and mbd.status <> 0  " & _
                                "and mb.status <> 0 and mc.status = 1 and wd.status <> 0 and wd.Container = 'true'  AND CH.status <> 0  " & _
                                "and mbrd.NoPelayaran =  " & hfnopelayaran.Value & "  and mb.kapal = " & HFIDKapal.Value & "  and ch.ContainerCode = '" & hfcontainercode.Value & "' " & _
                                "order by mb.penerima,wd.NamaSupplier, mbrd.NoContainer"
                Else

                    sqlstring = "Select mbrd.NoContainer,mbrd.Nama_Barang,mbrd.Total_Berat,wd.Merk,wd.Keterangan,mc.Jenis_Perusahaan + mc.Nama_Customer as Nama_Customer,mb.Penerima, " & _
                                " mbrd.Ukuran,mbrd.Jumlah_Colli, wd.Panjang, wd.Lebar,wd.Tinggi,mbd.Quantity,wd.QuotationDetailID,mb.Mb_No,wd.NamaSupplier, mbrd.NoSeal  from MBRDetail mbrd" & _
                                " left join MuatBarangDetail mbd on (mbrd.Mbd_ID = mbd.IDDetail and mbd.Mb_No =mbrd.Mb_No ) " & _
                                "  left join MuatBarang mb on (mbd.Mb_No = mb.Mb_No) " & _
                                " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID ) " & _
                                "  left join MasterCustomer mc on (mb.Customer_Id = mc.Kode_Customer ) " & _
                                " where mb.Customer_Id = '" & hfidcustomer.Value & "' and mbrd.Mbr_No = '" & hfHID.Value & "' and (mbrd.status = 1 or mbrd.status = 5) and mbd.status <>0 " & _
                                " and mb.status <> 0 and mc.status = 1 and wd.status <> 0 order by mb.penerima,wd.NamaSupplier, mbrd.NoContainer"
                End If

                cekDS = SQLExecuteQuery(sqlstring)
                rDT = cekDS.Tables(0)
                Panel_Input.Visible = False
                Panel_Grid.Visible = False
                Panel_Report.Visible = True

                If rDT.Rows.Count > 0 Then
                    If ddltype.SelectedValue = "Manifest" Then
                        If rDT.Rows.Count > 0 Then
                            For f As Integer = 0 To rDT.Rows.Count - 1
                                With rDT.Rows(f)
                                    mbnoboo = True
                                    If mDT.Rows.Count > 0 Then
                                        For j As Integer = 0 To mDT.Rows.Count - 1
                                            If .Item("Mb_No").ToString = mDT.Rows(j).Item("Mb_No").ToString And .Item("NoContainer").ToString = mDT.Rows(j).Item("NoContainer").ToString Then
                                                If mDT.Rows(j).Item("Nama_Barang").ToString.Length < 70 Then
                                                    mDT.Rows(j).Item("Nama_Barang") = mDT.Rows(j).Item("Nama_Barang").ToString + "," + .Item("Nama_Barang").ToString
                                                End If
                                                mDT.Rows(j).Item("Total_Berat") = CDbl(mDT.Rows(j).Item("Total_Berat").ToString) + CDbl(.Item("Total_Berat").ToString)
                                                mDT.Rows(j).Item("Quantity") = CDbl(mDT.Rows(j).Item("Quantity").ToString) + CDbl(.Item("Quantity").ToString)
                                                mDT.Rows(j).Item("Ukuran") = CDbl(mDT.Rows(j).Item("Ukuran").ToString) + CDbl(.Item("Ukuran").ToString)
                                                mDT.Rows(j).Item("Jumlah_Colli") = (CDbl(mDT.Rows(j).Item("Quantity").ToString).ToString) + " Colli"
                                                mbnoboo = False
                                                Exit For
                                            End If
                                        Next
                                        If mbnoboo = True Then
                                            mDR = mDT.NewRow
                                            mDR("NoContainer") = .Item("NoContainer").ToString
                                            mDR("Nama_Barang") = .Item("Nama_Barang").ToString
                                            mDR("Total_Berat") = .Item("Total_Berat").ToString
                                            mDR("Merk") = .Item("Merk").ToString
                                            mDR("Keterangan") = .Item("Keterangan").ToString
                                            mDR("Nama_Customer") = .Item("Nama_Customer").ToString
                                            mDR("Jumlah_Colli") = .Item("Jumlah_Colli").ToString
                                            mDR("Penerima") = TukarNamaPerusahaan(.Item("Penerima").ToString)
                                            mDR("Ukuran") = .Item("Ukuran").ToString
                                            'Response.Write("Ukuran : " & mDR("Ukuran"))
                                            mDR("Panjang") = .Item("Panjang").ToString
                                            mDR("Lebar") = .Item("Lebar").ToString
                                            mDR("Tinggi") = .Item("Tinggi").ToString
                                            mDR("Quantity") = .Item("Quantity").ToString
                                            mDR("QuotationDetailID") = .Item("QuotationDetailID").ToString
                                            mDR("Mb_No") = .Item("Mb_No").ToString
                                            mDR("NoSeal") = .Item("NoSeal").ToString
                                            mDT.Rows.Add(mDR)
                                        End If
                                    Else
                                        mDR = mDT.NewRow
                                        mDR("NoContainer") = .Item("NoContainer").ToString
                                        mDR("Nama_Barang") = .Item("Nama_Barang").ToString
                                        mDR("Total_Berat") = .Item("Total_Berat").ToString
                                        mDR("Merk") = .Item("Merk").ToString
                                        mDR("Keterangan") = .Item("Keterangan").ToString
                                        mDR("Nama_Customer") = .Item("Nama_Customer").ToString
                                        mDR("Jumlah_Colli") = .Item("Jumlah_Colli").ToString
                                        mDR("Penerima") = TukarNamaPerusahaan(.Item("Penerima").ToString)
                                        mDR("Ukuran") = .Item("Ukuran").ToString
                                        'Response.Write("Ukuran : " & mDR("Ukuran"))
                                        mDR("Panjang") = .Item("Panjang").ToString
                                        mDR("Lebar") = .Item("Lebar").ToString
                                        mDR("Tinggi") = .Item("Tinggi").ToString
                                        mDR("Quantity") = .Item("Quantity").ToString
                                        mDR("QuotationDetailID") = .Item("QuotationDetailID").ToString
                                        mDR("Mb_No") = .Item("Mb_No").ToString
                                        mDR("NoSeal") = .Item("NoSeal").ToString
                                        mDT.Rows.Add(mDR)

                                    End If
                                End With
                            Next
                        End If
                    End If

                    If ddltype.SelectedValue = "SuratMuat" Then
                        namacustomer = rDT.Rows(0).Item("NamaSupplier").ToString
                        namacustomerdatas = rDT.Rows(0).Item("NamaSupplier").ToString

                        For c As Integer = 0 To rDT.Rows.Count - 1
                            BorderBottom = ""
                            If c = rDT.Rows.Count - 1 Then
                                BorderBottom = "border-bottom:1px black solid;"
                            End If
                            dataManifest &= " <tr> " & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "   " & c + 1 & " " & _
                                     " </td>"
                            Dim namaPengirim As String = TukarNamaPerusahaan(rDT.Rows(c).Item("NamaSupplier").ToString)
                            Dim namapenerima As String = TukarNamaPerusahaan(rDT.Rows(c).Item("Penerima").ToString)
                            If c = 0 Then
                                dataManifest &= " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoContainer").ToString & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoSeal").ToString & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & namaPengirim & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & namapenerima & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(rDT.Rows(c).Item("Jumlah_Colli").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>&nbsp; " & rDT.Rows(c).Item("Nama_Barang").ToString & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(rDT.Rows(c).Item("Total_Berat")), "##,###,###,##").ToString)) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Panjang").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Lebar")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Tinggi")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>" & Cek_Format(rDT.Rows(c).Item("Ukuran")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div><b>" & rDT.Rows(c).Item("Keterangan").ToString & "</b> </div> " & _
                                                     " </td>" & _
                                                     "</tr>"
                                namacustomerdatas = rDT.Rows(c).Item("NamaSupplier").ToString

                            ElseIf c = rDT.Rows.Count - 1 Then

                                If namacustomerdatas = rDT.Rows(c).Item("NamaSupplier").ToString Then

                                    dataManifest &= " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoContainer").ToString & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoSeal").ToString & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>&nbsp;</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>&nbsp;</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(rDT.Rows(c).Item("Jumlah_Colli").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>&nbsp; " & rDT.Rows(c).Item("Nama_Barang").ToString & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(rDT.Rows(c).Item("Total_Berat")), "##,###,###,##").ToString)) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Panjang").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Lebar")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Tinggi")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>" & Cek_Format(rDT.Rows(c).Item("Ukuran")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div><b>" & rDT.Rows(c).Item("Keterangan").ToString & "</b> </div> " & _
                                                     " </td>" & _
                                                     "</tr>"
                                    namacustomerdatas = rDT.Rows(c).Item("NamaSupplier").ToString
                                Else
                                    dataManifest &= " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoContainer").ToString & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoSeal").ToString & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & namaPengirim & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & namapenerima & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(rDT.Rows(c).Item("Jumlah_Colli").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>&nbsp; " & rDT.Rows(c).Item("Nama_Barang").ToString & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(rDT.Rows(c).Item("Total_Berat")), "##,###,###,##").ToString)) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Panjang").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Lebar")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Tinggi")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>" & Cek_Format(rDT.Rows(c).Item("Ukuran")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div><b>" & rDT.Rows(c).Item("Keterangan").ToString & "</b> </div> " & _
                                                     " </td>" & _
                                                     "</tr>"
                                    namacustomerdatas = rDT.Rows(c).Item("NamaSupplier").ToString

                                End If
                            Else

                                If namacustomerdatas = rDT.Rows(c).Item("NamaSupplier").ToString Then

                                    dataManifest &= " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoContainer").ToString & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoSeal").ToString & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>&nbsp;</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>&nbsp;</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(rDT.Rows(c).Item("Jumlah_Colli").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>&nbsp; " & rDT.Rows(c).Item("Nama_Barang").ToString & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(rDT.Rows(c).Item("Total_Berat")), "##,###,###,##").ToString)) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Panjang").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Lebar")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Tinggi")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>" & Cek_Format(rDT.Rows(c).Item("Ukuran")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div><b>" & rDT.Rows(c).Item("Keterangan").ToString & "</b> </div> " & _
                                                     " </td>" & _
                                                     "</tr>"
                                    namacustomerdatas = rDT.Rows(c).Item("NamaSupplier").ToString
                                Else
                                    dataManifest &= " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoContainer").ToString & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & rDT.Rows(c).Item("NoSeal").ToString & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & namaPengirim & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & namapenerima & "</div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(rDT.Rows(c).Item("Jumlah_Colli").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>&nbsp; " & rDT.Rows(c).Item("Nama_Barang").ToString & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(rDT.Rows(c).Item("Total_Berat")), "##,###,###,##").ToString)) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Panjang").ToString) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Lebar")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "   <div>" & Cek_Format(rDT.Rows(c).Item("Tinggi")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div>" & Cek_Format(rDT.Rows(c).Item("Ukuran")) & " </div> " & _
                                                     " </td>" & _
                                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                                     "  <div><b>" & rDT.Rows(c).Item("Keterangan").ToString & "</b> </div> " & _
                                                     " </td>" & _
                                                     "</tr>"
                                    namacustomerdatas = rDT.Rows(c).Item("NamaSupplier").ToString

                                End If


                            End If




                            If c = rDT.Rows.Count - 1 Then
                                index = c + 1
                            End If

                            beratsementara = CDbl(rDT.Rows(c).Item("Total_Berat").ToString)
                            berattotal = berattotal + beratsementara
                            totalcoli = totalcoli + CDbl(rDT.Rows(c).Item("Quantity"))
                            TOtalColiPercustomer = TOtalColiPercustomer + CDbl(rDT.Rows(c).Item("Quantity"))
                            beratpercustomer = beratpercustomer + CDbl(rDT.Rows(c).Item("Total_Berat"))

                            If IsNumeric(rDT.Rows(c).Item("Ukuran")) Then
                                totalvolumepercustomer = totalvolumepercustomer + CDbl(FormatNumber(rDT.Rows(c).Item("Ukuran"), 3))
                                totalvolume = totalvolume + CDbl(FormatNumber(rDT.Rows(c).Item("Ukuran"), 3))
                            End If


                            If c < rDT.Rows.Count - 1 Then
                                If namacustomer <> rDT.Rows(c + 1).Item("NamaSupplier").ToString Then
                                    dataManifest &= " <tr> " & _
                                                    "  <td colspan = ""5"" align=""Left"" style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-left:solid 1px black;border-bottom:1px black solid;"" > " & _
                                                    "    <div><b>&nbsp;TOTAL<b> </div> " & _
                                                    "  </td> " & _
                                                    "  <td colspan =""2"" align=""Left"" style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-bottom:1px black solid;""> " & _
                                                    "    <div>&nbsp;&nbsp;&nbsp;<b>" & Cek_Data(Format(CDbl(TOtalColiPercustomer), "##,###,###,##")) & "</b></div> " & _
                                                    "  </td> " & _
                                                    "  <td colspan =""2"" align=""Left"" style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-bottom:1px black solid;""> " & _
                                                    "    <div>&nbsp;&nbsp;&nbsp;<b>" & Cek_Data(CekValue(Format(CDbl(beratpercustomer), "##,###,###,##"))) & "&nbsp;Kg</b></div> " & _
                                                    "  </td> " & _
                                                    "  <td colspan =""3"" align=""right"" style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-bottom:1px black solid;""> " & _
                                                    "    <div><b>" & Cek_Data(totalvolumepercustomer) & "&nbsp;m<sup>3</sup></b></div> " & _
                                                    "  </td> " & _
                                                    "  <td style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-bottom:1px black solid;border-right:1px black solid;""> " & _
                                                    "    <div></div> " & _
                                                    "  </td> " & _
                                                    " </tr>"

                                    namacustomer = rDT.Rows(c + 1).Item("NamaSupplier").ToString

                                    TOtalColiPercustomer = 0
                                    beratpercustomer = 0
                                    totalvolumepercustomer = 0
                                End If

                            End If

                            If c = rDT.Rows.Count - 1 Then
                                dataManifest &= " <tr> " & _
                                                "  <td colspan = ""5"" align=""Left"" style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-left:solid 1px black;border-bottom:1px black solid;"" > " & _
                                                "    <div><b>&nbsp;TOTAL<b> </div> " & _
                                                "  </td> " & _
                                                "  <td colspan =""2"" align=""Left"" style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-bottom:1px black solid;""> " & _
                                                "    <div>&nbsp;&nbsp;&nbsp;<b>" & Cek_Data(Format(CDbl(TOtalColiPercustomer), "##,###,###,##")) & "</b></div> " & _
                                                "  </td> " & _
                                                "  <td colspan =""2"" align=""Left"" style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-bottom:1px black solid;""> " & _
                                                "    <div>&nbsp;&nbsp;&nbsp;<b>" & Cek_Data(CekValue(Format(CDbl(beratpercustomer), "##,###,###,##").ToString)) & "&nbsp;Kg<b></div> " & _
                                                "  </td> " & _
                                                "  <td colspan =""3"" align=""right"" style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-bottom:1px black solid;""> " & _
                                                "    <div><b>" & Cek_Data(totalvolumepercustomer) & "&nbsp;m<sup>3</sup></b></div> " & _
                                                "  </td> " & _
                                                "  <td style=""" & BorderBottom.ToString & "border-top:solid 1px black;border-bottom:1px black solid;border-right:1px black solid;""> " & _
                                                    "    <div></div> " & _
                                                    "  </td> " & _
                                                " </tr>"
                            End If
                        Next

                    Else
                        namacustomerdatas = mDT.Rows(0).Item("Mb_No").ToString
                        nocont = mDT.Rows(0).Item("Mb_No").ToString
                        For j As Integer = 0 To mDT.Rows.Count - 1

                            BorderBottom = ""
                            If j = mDT.Rows.Count - 1 Then
                                BorderBottom = "border-bottom:1px black solid;"
                            End If

                            Dim namaPengirim As String = TukarNamaPerusahaan(mDT.Rows(j).Item("Nama_Customer").ToString)
                            Dim namapenerima As String = TukarNamaPerusahaan(mDT.Rows(j).Item("Penerima").ToString)

                            If j = 0 Then

                                dataManifest &= " <tr> " & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "     " & nomor & " " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("NoContainer").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("NoSeal").ToString & "</div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & namaPengirim & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & namapenerima & "</div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("Jumlah_Colli").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "  <div> " & mDT.Rows(j).Item("Nama_Barang").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(mDT.Rows(j).Item("Total_Berat").ToString), "##,###,###,##").ToString)) & "&nbsp;Kg </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & Cek_Data(mDT.Rows(j).Item("Ukuran")) & "&nbsp;m<sup>3</sup></b> </div> " & _
                                     " </td>" & _
                                     "</tr>"

                                nocont = mDT.Rows(j).Item("Mb_No").ToString

                            ElseIf j = mDT.Rows.Count - 1 Then
                                If nocont = mDT.Rows(j).Item("Mb_No").ToString Then
                                    dataManifest &= " <tr> " & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "     &nbsp; " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("NoContainer").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("NoSeal").ToString & "</div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & namaPengirim & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & namapenerima & "</div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("Jumlah_Colli").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "  <div> " & mDT.Rows(j).Item("Nama_Barang").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(mDT.Rows(j).Item("Total_Berat").ToString), "##,###,###,##").ToString)) & "&nbsp;Kg </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & Cek_Data(mDT.Rows(j).Item("Ukuran")) & "&nbsp;m<sup>3</sup></b> </div> " & _
                                     " </td>" & _
                                     "</tr>"

                                    nocont = mDT.Rows(j).Item("Mb_No").ToString
                                Else
                                    dataManifest &= " <tr> " & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "     " & nomor + 1 & " " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("NoContainer").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("NoSeal").ToString & "</div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & namaPengirim & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & namapenerima & "</div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("Jumlah_Colli").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "  <div> " & mDT.Rows(j).Item("Nama_Barang").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(mDT.Rows(j).Item("Total_Berat").ToString), "##,###,###,##").ToString)) & "&nbsp;Kg </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & Cek_Data(mDT.Rows(j).Item("Ukuran")) & "&nbsp;m<sup>3</sup></b> </div> " & _
                                     " </td>" & _
                                     "</tr>"
                                    nomor = nomor + 1
                                    nocont = mDT.Rows(j).Item("Mb_No").ToString
                                End If

                            Else
                                If mDT.Rows(j).Item("Mb_No").ToString = "--" Then
                                    dataManifest &= " <tr> " & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "     " & nomor + 1 & " " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("NoContainer").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("NoSeal").ToString & "</div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & namaPengirim & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & namapenerima & "</div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & mDT.Rows(j).Item("Jumlah_Colli").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "  <div> " & mDT.Rows(j).Item("Nama_Barang").ToString & " </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & Cek_Data(CekValue(Format(CDbl(mDT.Rows(j).Item("Total_Berat").ToString), "##,###,###,##").ToString)) & "&nbsp;Kg </div> " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   <div>" & Cek_Data(mDT.Rows(j).Item("Ukuran")) & "&nbsp;m<sup>3</sup></b> </div> " & _
                                     " </td>" & _
                                     "</tr>"
                                    nomor = nomor + 1
                                    nocont = mDT.Rows(j).Item("Mb_No").ToString

                                Else
                                    If nocont = mDT.Rows(j).Item("Mb_No").ToString Then
                                        dataManifest &= " <tr> " & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                         "     &nbsp; " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                         "   <div>" & mDT.Rows(j).Item("NoContainer").ToString & " </div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & mDT.Rows(j).Item("NoSeal").ToString & "</div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & namaPengirim & " </div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & namapenerima & "</div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & mDT.Rows(j).Item("Jumlah_Colli").ToString & " </div> " & _
                                         " </td>" & _
                                         " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "  <div> " & mDT.Rows(j).Item("Nama_Barang").ToString & " </div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & Cek_Data(CekValue(Format(CDbl(mDT.Rows(j).Item("Total_Berat").ToString), "##,###,###,##").ToString)) & "&nbsp;Kg </div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & Cek_Data(mDT.Rows(j).Item("Ukuran")) & "&nbsp;m<sup>3</sup></b> </div> " & _
                                         " </td>" & _
                                         "</tr>"

                                        nocont = mDT.Rows(j).Item("Mb_No").ToString

                                    Else
                                        dataManifest &= " <tr> " & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                         "     " & nomor + 1 & " " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                         "   <div>" & mDT.Rows(j).Item("NoContainer").ToString & " </div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & mDT.Rows(j).Item("NoSeal").ToString & "</div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & namaPengirim & " </div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & namapenerima & "</div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & mDT.Rows(j).Item("Jumlah_Colli").ToString & " </div> " & _
                                         " </td>" & _
                                         " <td align=left style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "  <div> " & mDT.Rows(j).Item("Nama_Barang").ToString & " </div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & Cek_Data(CekValue(Format(CDbl(mDT.Rows(j).Item("Total_Berat").ToString), "##,###,###,##").ToString)) & "&nbsp;Kg </div> " & _
                                         " </td>" & _
                                         " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                         "   <div>" & Cek_Data(mDT.Rows(j).Item("Ukuran")) & "&nbsp;m<sup>3</sup></b> </div> " & _
                                         " </td>" & _
                                         "</tr>"
                                        nomor = nomor + 1
                                        nocont = mDT.Rows(j).Item("Mb_No").ToString
                                    End If



                                End If
                            End If


                            If j = mDT.Rows.Count - 1 Then
                                index = j + 1
                            End If
                            beratsementara = CDbl(mDT.Rows(j).Item("Total_Berat").ToString)
                            berattotal = berattotal + beratsementara
                        Next
                    End If



                    BorderBottom = "border-bottom:1px black solid;"
                    If ddltype.SelectedValue = "SuratMuat" Then
                        dataManifest &= " <tr> " & _
                                     " <td colspan=""5"" align=""Left"" style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "   <b>&nbsp;TOTAL SEMUA</b>" & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & Cek_Data(Format(CDbl(totalcoli), "##,###,###,##").ToString) & "" & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & """>" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td colspan=""2""align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & Cek_Data(Format(CDbl(berattotal), "##,###,###,##").ToString) & "&nbsp;Kg " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & Cek_Data(totalvolume) & "&nbsp;m<sup>3</sup>" & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     "</tr>"
                    Else
                        dataManifest &= " <tr> " & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-left:1px black solid;border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & Cek_Data(Format(CDbl(berattotal), "##,###,###,##").ToString) & "&nbsp;Kg " & _
                                     " </td>" & _
                                     " <td align=center style=""" & BorderBottom.ToString & "border-right:1px black solid;"">" & _
                                     "   " & _
                                     " </td>" & _
                                     "</tr></table></td>"
                    End If

                End If

            End If

        Catch ex As Exception
            Throw New Exception("Error DataManifest function : <BR>" & ex.ToString)
        End Try
        Return dataManifest
    End Function
    Private Function manifestHeader(ByVal nopelayaran As Integer) As String
        Dim HeaderReport As String
        Dim hST As String
        Dim hDT As DataTable
        Dim hDS As DataSet
        Dim kST As String
        Dim kDT As DataTable
        Dim kDS As DataSet
        Dim sesHeader As String
        Dim NamaPerusahaan As String
        Try
            sesHeader = ddlHeader.SelectedValue
            hST = "Select * from HeaderForm where ID = '" & sesHeader & "' and status = 1;"
            hDS = SQLExecuteQuery(hST)
            hDT = hDS.Tables(0)
            kST = " select * from Kapal where IDDetail = '" & hfIDK.Value & "' and status = 1"
            kDS = SQLExecuteQuery(kST)
            kDT = kDS.Tables(0)
            NamaPerusahaan = hDT.Rows(0).Item("Nama").ToString
            HeaderReport = "<table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;table-layout:fixed;"">" & _
                  " <tr>" & _
                  "     <td style=""width:60px;vertical-align:top;"" colspan=2 align=center  >" & _
                  "         <img src=""" & hDT.Rows(0).Item("PathLogo").ToString & """ style=""height: 100px; width: 100px"" />" & _
                  "     </td>" & _
                  "     <td style=""width:1380px;vertical-align:top;#2c3848;"" colspan=2 align=center >" & _
                  "         <table width=1380px; bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
                  "             <tr> " & _
                  "                <td colspan=2 align=center>" & _
                  "                     <font size=""4""><b>" & hDT.Rows(0).Item("Jenis").ToString & "</b>" & _
                  "                </td>" & _
                  "             </tr>"
            If NamaPerusahaan = "PT. Ligita Jaya" Or NamaPerusahaan = "PT.Ligita Jaya" Then
                HeaderReport &= "             <tr>" & _
                                                "                 <td colspan=2 align=center>" & _
                                                "                     <font size=""7""><b>" & hDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                                "                 </td>" & _
                                                "             </tr>"
            Else
                HeaderReport &= "             <tr>" & _
                                                "                 <td colspan=2 align=center>" & _
                                                "                     <font size=""7""><b>" & hDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                                "                 </td>" & _
                                                "             </tr>"
            End If
            If ddltype.SelectedValue = "SuratMuat" Then
                HeaderReport &= "             <tr>" & _
                                              "                 <td colspan=2 align=center>" & _
                                              "                     <font size=""3""><b>SURAT MUAT BARANG</b>" & _
                                              "                 </td>" & _
                                              "             </tr>" & _
                                              "         </table>" & _
                                              "     </td>" & _
                                              "     <td style=""width:60px;vertical-align:top;"" colspan=2 align=center  >" & _
                                              "      .  &nbsp;" & _
                                              "     </td>" & _
                                              " </tr>" & _
                                              " <tr>" & _
                                              "     </td>" & _
                                              "         <table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
                                              "             <tr> " & _
                                              "                <td colspan=2 align=left style=""font-family:verdana;font-size:12px;"">" & _
                                               "                     <b>Reis :" & nopelayaran.ToString & " </b>" & _
                                              "                </td>" & _
                                              "             </tr>" & _
                                              "             <tr>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                              "                     Kapal : <b>" & kDT.Rows(0).Item("Nama_Kapal").ToString & " </b> " & _
                                              "                 </td>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                              "                     Dari : <b>" & DDLDari.SelectedValue & " </b> " & _
                                              "                 </td>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                              "                     Ke : <b>" & DDLKe.SelectedValue.ToString & " </b> " & _
                                              "                 </td>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                              "                     Nahkoda : <b>" & kDT.Rows(0).Item("Nahkoda_Kapal").ToString & " </b> " & _
                                              "                 </td>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;"">" & _
                                              "                     Tanggal jalan : <b>" & ManifestDate.Text.ToString & " </b> " & _
                                              "                 </td>" & _
                                              "             </tr>" & _
                                              "         </table>" & _
                                              "     </td>" & _
                                              " </tr>" & _
                                              " <tr>" & _
                                              "   <td style=""padding-right:4px;"" valign=top>" & _
                                              "     <table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;table-layout:fixed;"">" & _
                                              "       <tr>" & _
                                              "         <td align=center style=""width: 20px; border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>NO</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 50px; border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>No Cont</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 100px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>PENGIRIM</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 100px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>PENERIMA</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 50px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>MERK<br>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 50px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>JUMLAH COLLI</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 200px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>JENIS BARANG</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>BERAT</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>PANJANG</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>LEBAR</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>TINGGI</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>VOLUME</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 65px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>KETERANGAN</b>" & _
                                              "         </td>" & _
                                              "       </tr>"
            Else
                HeaderReport &= "             <tr>" & _
                                              "                 <td colspan=2 align=center>" & _
                                              "                     <font size=""3""><b>DAFTAR MANIFEST</b>" & _
                                              "                 </td>" & _
                                              "             </tr>" & _
                                              "         </table>" & _
                                              "     </td>" & _
                                              "     <td style=""width:60px;vertical-align:top;"" colspan=2 align=center  >" & _
                                              "      .  &nbsp;" & _
                                              "     </td>" & _
                                              " </tr>" & _
                                              " <tr>" & _
                                              "     </td>" & _
                                              "         <table width=1500px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
                                              "             <tr> " & _
                                              "                <td colspan=2 align=left>" & _
                                              "                     <b>Reis :" & nopelayaran.ToString & " </b>" & _
                                              "                </td>" & _
                                              "             </tr>" & _
                                              "             <tr>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;>" & _
                                              "                     Kapal : <b>" & kDT.Rows(0).Item("Nama_Kapal").ToString & " </b> " & _
                                              "                 </td>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;>" & _
                                              "                     Dari : <b>" & DDLDari.SelectedValue.ToString & " </b> " & _
                                              "                 </td>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;>" & _
                                              "                     Ke : <b>" & DDLKe.SelectedValue.ToString & " </b> " & _
                                              "                 </td>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;>" & _
                                              "                     Nahkoda : <b>" & kDT.Rows(0).Item("Nahkoda_Kapal").ToString & " </b> " & _
                                              "                 </td>" & _
                                              "                 <td colspan=2 align=center style=""font-family:verdana;font-size:12px;>" & _
                                              "                     Tanggal jalan : <b>" & ManifestDate.Text.ToString & " </b> " & _
                                              "                 </td>" & _
                                              "             </tr>" & _
                                              "         </table>" & _
                                              "     </td>" & _
                                              " </tr>" & _
                                              " <tr>" & _
                                              "   <td style=""padding-right:4px;"" valign=top>" & _
                                              "     <table width=1500px bgcolor=white cellpadding=4 cellspacing=0 style=""font-family:verdana;font-size:12px;table-layout:fixed;"">" & _
                                              "       <tr>" & _
                                              "         <td align=center style=""width: 20px; border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>NO</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 50px; border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>No Cont</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 100px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>PENGIRIM</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 100px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>PENERIMA</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 50px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>MERK<br>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 50px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>JUMLAH COLLI</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 240px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>JENIS BARANG</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>BERAT</b>" & _
                                              "         </td>" & _
                                              "         <td align=center style=""width: 45px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                              "           <b>VOLUME</b>" & _
                                              "         </td>" & _
                                              "       </tr>"
            End If

        Catch ex As Exception
            Throw New Exception("error manifestheader function : " & ex.ToString)
        End Try
        Return HeaderReport
    End Function
    Private Sub remove_item(ByVal mb_id As String)
        Try
            DT = CType(Session("Grid_Manifest"), DataTable)
            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("IDM").ToString = mb_id.ToString Then
                    DT.Rows(i).Delete()
                    Exit For
                End If
            Next
            'create_session()
            Session("Grid_Manifest") = DT
            Grid_Manifest.DataSource = DT
            Grid_Manifest.DataBind()

            Call Refresh_Grid()
        Catch ex As Exception
            Response.Write("Error Remove_Item <BR> : " & ex.ToString)
        End Try
    End Sub
    Private Sub Clear()
        DDLDari.SelectedIndex = 0
        DDLKe.SelectedIndex = 0
        TxtKapal.Text = ""
        ManifestDate.Text = Today
        ddltype.SelectedIndex = 0
        DdlNamaGudang.SelectedIndex = 0
        Session("Grid_Manifest") = Nothing
        lblError.Text = ""
        lblBerhasil.Text = ""
        lblError.Visible = False
        lblBerhasil.Visible = False
    End Sub
    Private Sub Add_Clear()
        lblError.Text = ""
        lblBerhasil.Text = ""
    End Sub

    Private Sub clear_label()
        lblError.Visible = False
        lblerror2.Visible = False
        lblError.Text = ""
        lblerror2.Text = ""
        lblBerhasil.Visible = False
    End Sub
    Private Sub Refresh_Grid()
        Dim DT As DataTable
        Try
            DT = CType(Session("Grid_Manifest"), DataTable)
            For i As Integer = 0 To DT.Rows.Count - 1
                DT.DefaultView(i).Row("IDM") = (i + 1).ToString
            Next
            Session("Grid_Manifest") = DT
            Grid_Manifest.DataSource = DT
            Grid_Manifest.DataBind()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Private Sub LoadDDLKota()
        Try
            sqlstring = " SELECT DISTINCT" & _
                          "	Pelabuhan, status " & _
                          "	FROM MasterTujuan " & _
                          " WHERE status = 1 ORDER BY Pelabuhan "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            DDLKe.Items.Clear()
            DDLDari.Items.Clear()

            With DDLDari
                .DataSource = DT
                .DataTextField = "Pelabuhan"
                .DataValueField = "Pelabuhan"
                .DataBind()
            End With

            DDLDari.Items.Insert(0, "Pilih Pelabuhan")

            With DDLKe
                .DataSource = DT
                .DataTextField = "Pelabuhan"
                .DataValueField = "Pelabuhan"
                .DataBind()
            End With

            DDLKe.Items.Insert(0, "Pilih Pelabuhan")
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
#End Region

#Region "Load DDL"
    Private Sub load_ddl_hist()
        Try
            sqlstring = "select ID,Nama from HeaderForm where [status] = 1 and Nama LIKE '%" & Profil & "%'"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With ddlHeaderHist
                .DataSource = DT
                .DataTextField = "Nama"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            Throw New Exception("Error load ddl header: " & ex.ToString)
        End Try
    End Sub
    Private Sub load_ddl()
        Try
            sqlstring = "select ID,Nama from HeaderForm where [status] = 1 and Nama LIKE '%" & Profil & "%'"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With ddlHeader
                .DataSource = DT
                .DataTextField = "Nama"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            Throw New Exception("Error load ddl header: " & ex.ToString)
        End Try
    End Sub

    Private Sub DDL_nama_gudang()
        Try
            sqlstring = " SELECT Warehouse_code, Warehouse_Name from MasterWarehouse where status = 1 order by Warehouse_Name"
            Dim dt As DataTable = SQLExecuteQuery(sqlstring).Tables(0)
            With DdlNamaGudang
                .DataSource = dt
                .DataTextField = "Warehouse_Name"
                .DataValueField = "Warehouse_code"
                .DataBind()
            End With
            DdlNamaGudang.Items.Insert(0, "Pilih Gudang")
        Catch ex As Exception
            Response.Write("DDL_nama_gudang Exception :<br>" & ex.ToString)
        End Try
    End Sub
#End Region


    

End Class