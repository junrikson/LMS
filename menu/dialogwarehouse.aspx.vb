Imports System.Data
Imports System.Data.SqlClient
Partial Public Class dialogwarehouse
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlString As String

#Region "PAGE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Select Case Request("Mode").ToString
                    Case "Quotation"
                        Call load_grid_warehouse()
                    Case "Item"
                        Call load_grid_item()
                    Case "Container"
                        Call load_Grid_item_Container()
                    Case "MuatReport"
                        Call load_grid_mbr()
                    Case "Nama"
                        Call load_grid_nama()
                    Case "ItemQuo"
                        Call load_grid_itemquo()
                    Case "Kubikasi"
                        Call load_Grid_kubikasi()
                    Case "MuatBarang"
                        Call Load_Muat_Barang()
                    Case "MuatBarangSementara"
                        Call Load_Muat_BarangSementara()
                    Case "Stuffing"
                        LoadStuffing()
                    Case "LoadContainer"
                        Load_Container()
                    Case "LoadContainerSementara"
                        Load_ContainerSementara()
                    Case "StuffingSementara"
                        LoadStuffingSementara()
                End Select
            End If

            If Not Session("grid_Dialog_Warehouse") Is Nothing Then
                Grid_item.DataSource = CType(Session("grid_Dialog_Warehouse"), DataTable)
                Grid_item.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Page Load Dialog :<BR>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "GRID VIEW"
    Protected Sub Grid_item_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_item.PreRender
        If Not Page.IsPostBack Then
            Grid_item.FocusedRowIndex = -1
        End If
    End Sub

    Private Sub Create_Grid_Session()
        Dim iDT As New DataTable
        Session("grid_Dialog_Warehouse") = iDT
    End Sub
    Private Sub load_grid_item()
        Try
            Dim iDT As New DataTable
            Dim quotation As String
            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With
            quotation = Request("Quotation").ToString
            sqlString = " SELECT QD.IDDetail as ID, MC.Nama_Customer " & _
                        " ,MQ.Penerima, QD.Nama_Barang,MHD.NamaHarga, QD.Keterangan, MQ.Tujuan  " & _
                        " FROM MasterCustomer MC " & _
                        " JOIN MasterQuotation MQ on MC.Kode_Customer = MQ.Customer_Id " & _
                        " JOIN QuotationDetail QD on MQ.Quotation_No = QD.Quotation_No " & _
                        " left JOIN MasterHargaDefault MHD on QD.SatuanID = MHD.ID " & _
                        " WHERE MC.[status] = 1 " & _
                        " AND (MQ.[status] = 1 or MQ.[status] = 2) " & _
                        " AND (QD.[status] = 1 or QD.[status] = 2)  AND MQ.Quotation_No = '" & quotation.ToString & "' " & _
                        " AND MHD.[status] <> 0 AND MHD.NamaHarga <> 'Container' " & _
                        " ORDER BY QD.Nama_Barang "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("ID").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    DR("Penerima") = .Item("Penerima").ToString
                    DR("Nama_Barang") = .Item("Nama_Barang").ToString
                    DR("Kubik") = ""
                    DR("Unit") = ""
                    DR("Others") = .Item("Keterangan").ToString
                    DR("Satuan") = .Item("NamaHarga").ToString
                    DR("Berat") = .Item("Tujuan").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Kubik").Visible = False
            Grid_item.Columns("Berat").Caption = "Tujuan"
            Grid_item.Columns("Others").Caption = "Keterangan"
            Grid_item.Columns("Quotation_No").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()
        Catch ex As Exception
            Response.Write("Show dialog warehouse error :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub load_grid_itemquo()
        Try
            Dim iDT As New DataTable
            Dim MbNo As String
            Dim MbId As String
            Dim str As String
         
            Dim cdt As DataTable
            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            MbId = Request("Muat").ToString
            str = "select Mb_No from MuatBarang where ID = '" & MbId.ToString & "' and [status] = 5"
            MbNo = SQLExecuteScalar(str)
            sqlString = " SELECT mbd.PackedContID,wd.Container,mbd.Warehouse_Id " & _
                        " from MuatBarangDetail mbd " & _
                        " left join WarehouseDetail wd on mbd.Warehouse_Id = wd.ID " & _
                        " left join QuotationDetail qd on wd.QuotationDetailID = qd.ID " & _
                        " left join MasterSatuanOther mso on mso.ID = qd.OthersID " & _
                        " where " & _
                        " mbd.[status] = 5  AND mbd.Mb_No = '" & MbNo.ToString & "' " & _
                        " ORDER BY wd.Nama_Barang "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    If DT.Rows(i).Item("Container").ToString = "true" Then
                        sqlString = "SELECT MBD.ID, whd.Container, QD.Nama_Barang, MBD.Quantity , QD.Harga " & _
                                "FROM MuatBarangDetail MBD " & _
                                "JOIN WarehouseDetail WHD on MBD.Warehouse_Id = WHD.ID " & _
                                "JOIN Containerheader CH ON WHD.Nama_Barang = CH.ID " & _
                                "JOIN QuotationDetail QD on CH.QuotationDetailID = QD.ID " & _
                                "WHERE MBD.MB_No = '" & MbNo.ToString & "' " & _
                                "AND MBD.[status] = 5 " & _
                                "AND WHD.[status] = 1 " & _
                                "AND MBD.Warehouse_Id = '" & DT.Rows(i).Item("Warehouse_Id").ToString & "'  " & _
                                "AND CH.[status] = 1 " & _
                                "AND (QD.[status] = 1 or QD.[status] = 2)"
                        DS = SQLExecuteQuery(sqlString)
                        cdt = DS.Tables(0)

                        If cdt.Rows.Count > 0 Then
                            For j As Integer = 0 To cdt.Rows.Count - 1
                                With cdt.Rows(j)
                                    DR("Quotation_No") = .Item("ID").ToString
                                    DR("Nama_Barang") = .Item("Nama_Barang").ToString
                                    DR("Nama_Customer") = .Item("Quantity").ToString
                                    DR("Satuan") = "Container"
                                    DR("Berat") = .Item("Harga").ToString

                                End With
                            Next
                        End If
                    Else
                        sqlString = "SELECT MBD.ID, whd.Container, " & _
                                "QD.Nama_Barang, MHD.NamaHarga as PenentuanSatuan, MSO.Nama_Satuan as Satuan, MBD.Quantity , QD.Harga " & _
                                "From MuatBarangDetail MBD " & _
                                "JOIN WarehouseDetail WHD on MBD.Warehouse_Id = WHD.ID " & _
                                "JOIN QuotationDetail QD on WHD.QuotationDetailID = QD.ID " & _
                                "JOIN MasterHargaDefault MHD on MHD.ID = QD.SatuanID " & _
                                "JOIN MasterSatuanOther MSO on MSO.ID = QD.OthersID " & _
                                "WHERE MBD.MB_No = '" & MbNo.ToString & "' " & _
                                "AND MBD.Warehouse_Id = '" & DT.Rows(i).Item("Warehouse_Id").ToString & "'  " & _
                                "AND MBD.[status] = 5 " & _
                                "AND WHD.[status] = 1 " & _
                                "AND (QD.[status] = 1 or QD.[status] = 2) "
                        DS = SQLExecuteQuery(sqlString)
                        cdt = DS.Tables(0)

                        If cdt.Rows.Count > 0 Then
                            For j As Integer = 0 To cdt.Rows.Count - 1
                                With cdt.Rows(j)
                                    DR("Quotation_No") = .Item("ID").ToString
                                    DR("Nama_Barang") = .Item("Nama_Barang").ToString
                                    DR("Kubik") = .Item("Quantity").ToString
                                    DR("Satuan") = .Item("Satuan").ToString
                                    DR("Berat") = .Item("Harga").ToString
                                    DR("Nama_Customer") = .Item("Quantity").ToString
                                End With
                            Next
                        End If
                    End If
                End With
                iDT.Rows.Add(DR)

            Next


            Grid_item.Columns("Quotation_No").Visible = False
            Grid_item.Columns("Nama_Barang").Caption = "Nama Barang"
            Grid_item.Columns("Berat").Caption = " Harga"
            Grid_item.Columns("Nama_Customer").Caption = "Quantity"
            Grid_item.Columns("Nama_Customer").Visible = True
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Kubik").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Penerima").Visible = False
            Grid_item.Columns("Satuan").Caption = "Satuan"
            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()
        Catch ex As Exception
            Response.Write("Show dialog warehouse error :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub load_grid_nama()
        Try
            Dim iDT As New DataTable
            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With
            ' quotation = Request("Quotation").ToString
            sqlString = "Select mb.ID,mb.Mb_No,mc.Nama_Customer,mb.Penerima,mq.Tujuan, mb.Kapal, K.Nama_Kapal " & _
                        "from MuatBarang mb " & _
                        "left join MasterCustomer mc on mb.Customer_Id = mc.ID " & _
                        "left join WarehouseHeader wh on mb.WarehouseHeaderID = wh.ID " & _
                        "left join MasterQuotation mq on wh.Quotation_No = mq.Quotation_No " & _
                        "left join Kapal K on mb.Kapal = K.ID " & _
                        "where " & _
                        "mb.status = 5 " & _
                        "and K.[status] = 1 "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("ID").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    DR("Penerima") = .Item("Penerima").ToString
                    DR("Nama_Barang") = .Item("Mb_No").ToString
                    DR("Satuan") = .Item("Tujuan").ToString
                    DR("Berat") = .Item("Kapal").ToString
                    DR("Kubik") = .Item("Nama_Kapal").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Quotation_No").Visible = False
            Grid_item.Columns("Nama_Barang").Caption = "Muat Barang Number"
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Kubik").Caption = "Nama Kapal"
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Caption = "Tujuan"
            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()
        Catch ex As Exception
            Response.Write("Show dialog warehouse error :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub load_grid_warehouse()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            sqlString = " SELECT MC.Kode_Customer as IDCustomer, MQ.Quotation_No, MC.Nama_Customer " & _
                        " ,MQ.Penerima, MQ.Tujuan " & _
                        " FROM MasterCustomer MC " & _
                        " JOIN MasterQuotation MQ on MC.Kode_Customer = MQ.Customer_Id " & _
                        " WHERE MC.[status] = 1 " & _
                        " AND (MQ.[status] = 1 or MQ.Status = 2 ) " & _
                        " ORDER BY MQ.Quotation_No"

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("Quotation_No").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    DR("Penerima") = .Item("Penerima").ToString
                    DR("Nama_Barang") = .Item("IDCustomer").ToString
                    DR("Berat") = .Item("Tujuan").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Nama_Barang").Visible = False
            Grid_item.Columns("Berat").Caption = "Tujuan"
            Grid_item.Columns("Kubik").Visible = False
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()
        Catch ex As Exception
            Response.Write("Show dialog warehouse error :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub load_grid_mbr()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            sqlString = " SELECT MBR.ID, MC.Nama_Customer " & _
                        " ,MB.Penerima " & _
                        " FROM MuatBarangReport MBR " & _
                        " JOIN MuatBarang MB on MBR.Mb_Id = MB.Mb_No " & _
                        " JOIN MasterCustomer MC on MB.Customer_Id= MC.Kode_Customer " & _
                        " WHERE MC.[status] = 1 " & _
                        " AND MBR.[status] = 1 " & _
                        " AND MB.[status] = 1 " & _
                        " ORDER BY MBR.ID desc"

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("ID").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    DR("Penerima") = .Item("Penerima").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Quotation_No").Caption = "ID"
            Grid_item.Columns("Quotation_No").Visible = False
            Grid_item.Columns("Nama_Barang").Visible = False
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Kubik").Visible = False
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()
        Catch ex As Exception
            Response.Write("Show dialog warehouse error :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_Grid_item_Container()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
            End With

            sqlString = "SELECT QD.Quotation_No, QD.IDDetail as ID, MC.Nama_Customer, MQ.Penerima,QD.Nama_Barang, MHD.NamaHarga, QD.Customer_Id, QD.Keterangan, MQ.Tujuan " & _
                        "FROM MasterQuotation MQ " & _
                        "JOIN QuotationDetail QD ON MQ.Quotation_No = QD.Quotation_No " & _
                        "JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer " & _
                        "JOIN MasterHargaDefault MHD ON MHD.ID = QD.satuanID " & _
                        "WHERE (MHD.NamaHarga = 'Container' OR MHD.NamaHarga = 'Kontainer') " & _
                         "AND (MQ.[status] = 1 or MQ.[status] = 2) " & _
                        "AND MC.[status] = 1 " & _
                        "AND (QD.[status] = 1 or QD.[status] = 2 ) " & _
                        "AND MHD.[status] = 1 "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("Quotation_No").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    DR("Penerima") = .Item("Penerima").ToString
                    DR("Satuan") = .Item("NamaHarga").ToString
                    DR("Nama_Barang") = .Item("Customer_Id").ToString
                    DR("Berat") = .Item("ID")
                    DR("Kubik") = .Item("Nama_Barang").ToString
                    DR("Others") = .Item("Keterangan").ToString
                    DR("Unit") = .Item("Tujuan").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Grid_item.Columns("Kubik").Caption = "Nama Barang"
            Grid_item.Columns("Unit").Caption = "Tujuan"
            Grid_item.Columns("Others").Caption = "Keterangan"
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Nama_Barang").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()

        Catch ex As Exception
            Throw New Exception("<b>Error Load grid Container:</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_Grid_kubikasi()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
            End With

            sqlString = "SELECT QD.Quotation_No, QD.IDDetail as ID, MC.Nama_Customer, MQ.Penerima,QD.Nama_Barang, MHD.NamaHarga, QD.Customer_Id " & _
                        "FROM MasterQuotation MQ " & _
                        "JOIN QuotationDetail QD ON MQ.Quotation_No = QD.Quotation_No " & _
                        "JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer " & _
                        "JOIN MasterHargaDefault MHD ON MHD.ID = QD.satuanID " & _
                        "WHERE (MHD.NamaHarga = 'kubikasi' OR MHD.NamaHarga = 'Kubikasi') " & _
                         "AND (MQ.[status] = 1 or MQ.[status] = 2) " & _
                        "AND MC.[status] = 1 " & _
                        "AND (QD.[status] = 1 or QD.[status] = 2 ) " & _
                        "AND MHD.[status] = 1 "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("Quotation_No").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    DR("Penerima") = .Item("Penerima").ToString
                    DR("Satuan") = .Item("Nama_Barang").ToString
                    DR("Nama_Barang") = .Item("Customer_Id").ToString
                    DR("Berat") = .Item("ID")
                    DR("Kubik") = .Item("Nama_Barang").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Grid_item.Columns("Kubik").Caption = "Nama Barang"
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Nama_Barang").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()

        Catch ex As Exception
            Throw New Exception("<b>Error Load grid Container:</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Load_Muat_Barang()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            sqlString = "SELECT distinct MBRD.Mbr_No, VMBD.Customer_Id, MC.Nama_Customer, MBR.NoPelayaran, MBR.Kapal, K.Nama_Kapal, VMBD.Penerima " & _
                        "from MBRDetail MBRD  " & _
                        "LEFT JOIN V_MuatBarang_Detail VMBD ON (MBRD.Mb_No = VMBD.Mb_No AND MBRD.Mbd_ID = VMBD.IDDetail) " & _
                        "LEFT JOIN MuatBarangReport MBR ON (MBRD.Mbr_No = MBR.Mbr_No) " & _
                        "LEFT JOIN Kapal K ON VMBD.Kapal = K.IDDetail " & _
                        "LEFT JOIN MasterCustomer MC ON VMBD.Customer_Id = MC.Kode_Customer " & _
                        "where " & _
                        "VMBD.status <> 0 " & _
                        "AND MC.[status] <> 0   " & _
                        "AND VMBD.[status] <> 0 " & _
                        "and MBR.[status] <> 0 " & _
                        "and MBR.Depart_Date >= dateadd(MONTH,-2,GETDATE()) " & _
                        "and MBRD.[status] <> 0 " & _
                        "and K.[status] = 1 " & _
                        "order by MBR.NoPelayaran "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("Customer_Id").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    DR("Penerima") = .Item("Penerima").ToString
                    DR("Nama_Barang") = .Item("NoPelayaran").ToString
                    DR("Berat") = .Item("Mbr_No").ToString
                    DR("Satuan") = .Item("Kapal").ToString
                    DR("Kubik") = .Item("Nama_Kapal").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Quotation_No").Caption = "Customer ID"
            Grid_item.Columns("Quotation_No").Visible = False
            Grid_item.Columns("Nama_Barang").Caption = "No Pelayaran"
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Kubik").Caption = "Nama Kapal"
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()
        Catch ex As Exception
            Throw New Exception("<b>Error Load Muat Barang :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Load_Muat_BarangSementara()
        Try
            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            sqlString = "SELECT distinct MBRD.Mbr_No, VMBD.Customer_Id, MC.Nama_Customer, MBR.NoPelayaran, MBR.Kapal, K.Nama_Kapal, VMBD.Penerima " & _
                        "from MBRTempDetail MBRD  " & _
                        "LEFT JOIN V_MuatBarang_Detail VMBD ON (MBRD.Mb_No = VMBD.Mb_No AND MBRD.Mbd_ID = VMBD.IDDetail) " & _
                        "LEFT JOIN MuatBarangTempReport MBR ON (MBRD.Mbr_No = MBR.Mbr_No) " & _
                        "LEFT JOIN Kapal K ON VMBD.Kapal = K.IDDetail " & _
                        "LEFT JOIN MasterCustomer MC ON VMBD.Customer_Id = MC.Kode_Customer " & _
                        "where VMBD.status <> 0 " & _
                        "AND MC.[status] <> 0   " & _
                        "AND VMBD.[status] <> 0 " & _
                        "and MBR.[status] <> 0 " & _
                        "and MBRD.[status] <> 0 " & _
                        "and K.[status] = 1 " & _
                        "order by MBR.NoPelayaran "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("Customer_Id").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    DR("Penerima") = .Item("Penerima").ToString
                    DR("Nama_Barang") = .Item("NoPelayaran").ToString
                    DR("Berat") = .Item("Mbr_No").ToString
                    DR("Satuan") = .Item("Kapal").ToString
                    DR("Kubik") = .Item("Nama_Kapal").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Quotation_No").Caption = "Customer ID"
            Grid_item.Columns("Quotation_No").Visible = False
            Grid_item.Columns("Nama_Barang").Caption = "No Pelayaran"
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Kubik").Caption = "Nama Kapal"
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()
        Catch ex As Exception
            Throw New Exception("<b>Error Load Muat Barang :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub LoadStuffing()
        Try

            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            sqlString = "select distinct MBR.Mbr_No,VMBD.PackedContID, MBR.NoPelayaran, VMBD.Kapal, K.Nama_Kapal " & _
                        "FROM MBRDetail MBRD " & _
                        "LEFT JOIN V_MuatBarang_Detail VMBD ON (MBRD.Mb_No = VMBD.Mb_No AND MBRD.Mbd_ID = VMBD.IDDetail) " & _
                        "LEFT JOIN MuatBarangReport MBR ON (MBRD.Mbr_No = MBR.Mbr_No) " & _
                        "LEFT JOIN Kapal K ON VMBD.Kapal = K.IDDetail " & _
                        "where " & _
                        "VMBD.status <> 0 " & _
                        "AND MBRD.[status] <> 0 " & _
                        "AND MBR.Depart_Date >= dateadd(MONTH,-2,GETDATE()) " & _
                        "AND K.[status] <> 0 " & _
                        "AND VMBD.PackedContID <> '--' AND VMBD.PackedContID <> '0' " & _
                        "ORDER BY MBR.NoPelayaran "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("PackedContID").ToString
                    DR("Nama_Customer") = .Item("NoPelayaran").ToString
                    DR("Penerima") = .Item("Kapal").ToString
                    DR("Nama_Barang") = .Item("Nama_Kapal").ToString
                    DR("Berat") = .Item("Mbr_No").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Quotation_No").Caption = "NO Kontainer"
            Grid_item.Columns("Nama_Barang").Caption = "Nama Kapal"
            Grid_item.Columns("Penerima").Caption = "IDKapal"
            Grid_item.Columns("Nama_Customer").Caption = "NoPelayaran"
            Grid_item.Columns("Penerima").Visible = False
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Kubik").Visible = False
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub LoadStuffingSementara()
        Try

            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            sqlString = "select distinct MBR.Mbr_No,VMBD.PackedContID, MBR.NoPelayaran, VMBD.Kapal, K.Nama_Kapal " & _
                        "FROM MBRTempDetail MBRD " & _
                        "LEFT JOIN V_MuatBarang_Detail VMBD ON (MBRD.Mb_No = VMBD.Mb_No AND MBRD.Mbd_ID = VMBD.IDDetail) " & _
                        "LEFT JOIN MuatBarangTempReport MBR ON (MBRD.Mbr_No = MBR.Mbr_No) " & _
                        "LEFT JOIN Kapal K ON VMBD.Kapal = K.IDDetail " & _
                        "where VMBD.[status] <> 0 " & _
                        "AND MBRD.[status] <> 0 " & _
                        "AND K.[status] <> 0 " & _
                        "AND VMBD.PackedContID <> '--' AND VMBD.PackedContID <> '0' AND VMBD.PackedContID <> ' -' " & _
                        "ORDER BY MBR.NoPelayaran "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("PackedContID").ToString
                    DR("Nama_Customer") = .Item("NoPelayaran").ToString
                    DR("Penerima") = .Item("Kapal").ToString
                    DR("Nama_Barang") = .Item("Nama_Kapal").ToString
                    DR("Berat") = .Item("Mbr_No").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Quotation_No").Caption = "NO Kontainer"
            Grid_item.Columns("Nama_Barang").Caption = "Nama Kapal"
            Grid_item.Columns("Penerima").Caption = "IDKapal"
            Grid_item.Columns("Nama_Customer").Caption = "NoPelayaran"
            Grid_item.Columns("Penerima").Visible = False
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Kubik").Visible = False
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Load_Container()
        Try

            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            sqlString = "select distinct ch.ContainerCode, MBR.Mbr_No, CH.NoKontainer, MBR.NoPelayaran, VMBD.Kapal, K.Nama_Kapal  " & _
                        "FROM MBRDetail MBRD  " & _
                        "LEFT JOIN V_MuatBarang_Detail VMBD ON (MBRD.Mb_No = VMBD.Mb_No AND MBRD.Mbd_ID = VMBD.IDDetail) " & _
                        "LEFT JOIN MuatBarangReport MBR ON (MBRD.Mbr_No = MBR.Mbr_No) " & _
                        "LEFT JOIN Kapal K ON VMBD.Kapal = K.IDDetail " & _
                        "LEFT JOIN WarehouseDetail WD ON VMBD.WarehouseHeaderID = WD.WarehouseItem_Code " & _
                        "LEFT JOIN Containerheader CH ON WD.Nama_Barang = CH.ContainerCode  " & _
                        "where MBR.Depart_Date >= dateadd(MONTH,-2,GETDATE()) " & _
                        "AND VMBD.status <> 0 " & _
                        "AND MBRD.[status] <> 0 " & _
                        "AND K.[status] <> 0 " & _
                        "AND WD.[status] <> 0 " & _
                        "AND CH.[status] <> 0 " & _
                        "AND WD.[statuspengiriman] <> 0 " & _
                        "and WD.[Container] = 'true' " & _
                        "ORDER BY MBR.NoPelayaran "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("NoKontainer").ToString
                    DR("Nama_Customer") = .Item("NoPelayaran").ToString
                    DR("Penerima") = .Item("Kapal").ToString
                    DR("Nama_Barang") = .Item("Nama_Kapal").ToString
                    DR("Berat") = .Item("Mbr_No").ToString
                    DR("Satuan") = .Item("ContainerCode").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Quotation_No").Caption = "NO Kontainer"
            Grid_item.Columns("Nama_Barang").Caption = "Nama Kapal"
            Grid_item.Columns("Penerima").Caption = "IDKapal"
            Grid_item.Columns("Nama_Customer").Caption = "NoPelayaran"
            Grid_item.Columns("Penerima").Visible = False
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Kubik").Visible = False
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Load_ContainerSementara()
        Try

            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("Quotation_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Kubik", GetType(String)))
                .Add(New DataColumn("Unit", GetType(String)))
                .Add(New DataColumn("Others", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
            End With

            sqlString = "select distinct ch.ContainerCode, MBR.Mbr_No, CH.NoKontainer, MBR.NoPelayaran, VMBD.Kapal, K.Nama_Kapal  " & _
                        "FROM MBRTempDetail MBRD  " & _
                        "LEFT JOIN V_MuatBarang_Detail VMBD ON (MBRD.Mb_No = VMBD.Mb_No AND MBRD.Mbd_ID = VMBD.IDDetail) " & _
                        "LEFT JOIN MuatBarangTempReport MBR ON (MBRD.Mbr_No = MBR.Mbr_No) " & _
                        "LEFT JOIN Kapal K ON VMBD.Kapal = K.IDDetail " & _
                        "LEFT JOIN WarehouseDetail WD ON VMBD.WarehouseHeaderID = WD.WarehouseItem_Code " & _
                        "LEFT JOIN Containerheader CH ON WD.Nama_Barang = CH.ContainerCode  " & _
                        "where VMBD.[status] <> 0 " & _
                        "AND MBRD.[status] <> 0 " & _
                        "AND K.[status] <> 0 " & _
                        "AND WD.[status] <> 0 " & _
                        "AND CH.[status] <> 0 " & _
                        "AND WD.[statuspengiriman] <> 0 " & _
                        "and WD.[Container] = 'true' " & _
                        "ORDER BY MBR.NoPelayaran "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("Quotation_No") = .Item("NoKontainer").ToString
                    DR("Nama_Customer") = .Item("NoPelayaran").ToString
                    DR("Penerima") = .Item("Kapal").ToString
                    DR("Nama_Barang") = .Item("Nama_Kapal").ToString
                    DR("Berat") = .Item("Mbr_No").ToString
                    DR("Satuan") = .Item("ContainerCode").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_item.Columns("Quotation_No").Caption = "NO Kontainer"
            Grid_item.Columns("Nama_Barang").Caption = "Nama Kapal"
            Grid_item.Columns("Penerima").Caption = "IDKapal"
            Grid_item.Columns("Nama_Customer").Caption = "NoPelayaran"
            Grid_item.Columns("Penerima").Visible = False
            Grid_item.Columns("Berat").Visible = False
            Grid_item.Columns("Kubik").Visible = False
            Grid_item.Columns("Unit").Visible = False
            Grid_item.Columns("Others").Visible = False
            Grid_item.Columns("Satuan").Visible = False

            Session("grid_Dialog_Warehouse") = iDT
            Grid_item.DataSource = iDT
            Grid_item.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

#End Region

    
End Class