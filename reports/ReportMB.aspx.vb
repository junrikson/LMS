Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Partial Public Class ReportMB
    Inherits System.Web.UI.Page
    Private sqlString As String
    Private report As New ReportDocument
    Private reportPath As String
    Private ds As DataSet
    Private dt As DataTable


#Region "PAGE"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try

            If Not Session("ReportDocument") Is Nothing Then 'utk next agar tidak postback
                Call LoadReport()
            End If

        Catch ex As Exception
            Response.Write("Page_Init Exception :<br>" & ex.ToString)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                crv1.HasCrystalLogo = False
                crv1.HasToggleGroupTreeButton = False
                crv1.HasViewList = False
                crv1.HasZoomFactorList = True
                crv1.HasSearchButton = True
                crv1.DisplayGroupTree = False

                Call LoadReport()

                If Not Session("ReportDocument") Is Nothing Then
                    crv1.ReportSource = CType(Session("ReportDocument"), ReportDocument)

                End If

            Catch ex As Exception
                Response.Write("Page load exception : <br>" & ex.ToString)
            End Try
        End If
    End Sub

#End Region

#Region "REPORT"
    Private Sub LoadReport()

        report = New ReportDocument

        Try
            Dim mb_no As String = Session("mb_no")
            sqlString = " Select wd.Merk, mc.Nama_Customer, wd.Nama_Barang, wd.Berat,wd.Panjang," & _
                        " wd.Lebar,wd.Tinggi,wd.Unit,wd.Others, wd.Quantity , mb.Kapal ,mq.Penerima,k.Nahkoda_Kapal from MuatBarang mb, " & _
                        " MuatBarangDetail mbd,MasterQuotation mq,MasterCustomer mc," & _
                        " WarehouseHeader wh,WarehouseDetail wd,Kapal k where mb.Mb_No = mbd.Mb_No" & _
                        " and mbd.Warehouse_Id = wd.ID and wd.Warehouseheader_ID = wh.ID AND " & _
                        " wh.Quotation_No = mq.Quotation_No and mb.Mb_No = '" & mb_no.ToString & "' " & _
                        " and mb.Customer_id = mc.ID and mb.Kapal = k.Nama_Kapal and mb.status = 1  order by mbd.ID "

            ds = SQLExecuteQuery(sqlString)
            dt = ds.Tables(0)
            'Response.Write(sqlString)
            reportPath = Server.MapPath("ReportMBT.rpt")
            report.Load(reportPath) ' buad ngeload reportnya
            report.SetDataSource(dt) ' untuk penggabungan report dan datatable
            crv1.ReportSource = report   ' nampilin ke viewernya
            Session("ReportDocument") = report

        Catch ex As Exception
            Response.Write("LoadReport Exception : <br>" & ex.ToString)

        End Try

    End Sub

#End Region
End Class