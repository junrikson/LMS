Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Partial Public Class Warehouse_Report
    Inherits System.Web.UI.Page

    Private sqlString As String
    Private report As New ReportDocument
    Private reportPath As String
    Private ds As DataSet
    Private dt As DataTable

#Region "Page"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Not Session("ReportDocument") Is Nothing Then 'utk next agar tidak postback
                Session("ReportDocument") = Nothing
            End If
            DDL_Warehouse()
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

                If Not Session("ReportDocument") Is Nothing Then
                    crv1.ReportSource = CType(Session("ReportDocument"), ReportDocument)
                End If

            Catch ex As Exception
                Response.Write(" Error Page Load : <BR> " & ex.ToString)
            End Try
        End If
    End Sub
#End Region

#Region "DDL"
    Private Sub DDL_Warehouse()
        Try
            sqlString = " SELECT Warehouse_Code,Warehouse_Name from MasterWarehouse where [status] = 1 order by Warehouse_Code"
            Dim dt As DataTable = SQLExecuteQuery(sqlString).Tables(0)
            With DDLSource
                .DataSource = dt
                .DataTextField = "Warehouse_Name"
                .DataValueField = "Warehouse_Code"
                .DataBind()
            End With
            DDLSource.Items.Insert(0, "-Please Select Source-")

        Catch ex As Exception
            Response.Write("DDL_Warehouse Exception :<br>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "REPORT"
    Private Sub LoadReport()

        report = New ReportDocument

        Try
            sqlString = " select WD.Nama_Barang,WD.MERK,WD.Berat,WD.Panjang,WD.Lebar,WD.Tinggi,WD.Unit,WD.Others, " & _
                        " convert(varchar,WH.Time_Itemcome,105) as Time_Itemcome,MW.Warehouse_Code,MW.Warehouse_Name,MC.Nama_Customer,MQ.Penerima " & _
                        " from WarehouseHeader WH, WarehouseDetail WD,MasterCustomer MC,MasterWarehouse MW,MasterQuotation MQ " & _
                        " where WH.Warehouse_Code = MW.Warehouse_Code and WH.ID= WD.WarehouseHeader_ID and WH.Quotation_No = MQ.Quotation_No" & _
                        " and WH.Warehouse_Code = '" & DDLSource.SelectedValue & "'  and MC.ID = MQ.Customer_Id AND WH.status = 1 " & _
                        " and WD.status = 1 and MW.status = 1 and MC.status = 1 AND MQ.status = 1 AND WH.Time_Itemcome Between '" & tbFrom.Text.ToString & "' AND '" & tbSampai.Text.ToString & "'"

            ds = SQLExecuteQuery(sqlString)
            dt = ds.Tables(0)
            'Response.Write(sqlString)
            reportPath = Server.MapPath("Warehouse_ReportT.rpt")
            report.Load(reportPath) ' buad ngeload reportnya
            report.SetDataSource(dt) ' untuk penggabungan report dan datatable
            crv1.ReportSource = report   ' nampilin ke viewernya
            Session("ReportDocument") = report


        Catch ex As Exception
            Response.Write("LoadReport Exception : <br>" & ex.ToString)

        End Try

    End Sub

#End Region

#Region "Button"
    Protected Sub btSubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSubmit.Click
        Try
            If validation() Then
                lstatus.Visible = False
                LoadReport()
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub
#End Region

#Region "function"

    Private Function validation() As Boolean
        Try
            If tbFrom.Text = "" Then
                lstatus.Visible = True
                lstatus.Text = "Please fill start date"
                Return False
            End If

            If tbSampai.Text = "" Then
                lstatus.Visible = True
                lstatus.Text = "Please fill end date"
                Return False
            End If

            If Date.Compare(CDate(tbFrom.Text), CDate(tbSampai.Text)) > 0 Then
                lstatus.Visible = True
                lstatus.Text = "date not correct"
                Return False
            End If

            If DDLSource.SelectedIndex = 0 Then
                lstatus.Visible = True
                lstatus.Text = "Please choose source"

                Return False
            End If


            Return True

        Catch ex As Exception
            Throw New Exception("Error validasi :<BR> " & ex.ToString)
        End Try
    End Function


#End Region

End Class