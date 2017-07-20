Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Partial Public Class ReportQuotation
    Inherits System.Web.UI.Page
    Dim reportPath As String
    Dim report As New ReportDocument
    Dim dt As DataTable
    Dim dt2 As New DataTable
    Dim sqlString As String
    Dim sqlstr As String
#Region "PAGE"
    Private Sub ReportAcc_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Session("reportDocQuo") Is Nothing Then
            report = New ReportDocument
            report = CType(Session("reportDocQuo"), ReportDocument)
            'report.SetDatabaseLogon(username, password)
            CrystalReportViewer1.ReportSource = report
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then

                Response.Cache.SetCacheability(HttpCacheability.NoCache)

                With CrystalReportViewer1
                    .HasCrystalLogo = False
                    .HasToggleGroupTreeButton = False
                    .HasViewList = False
                    .HasZoomFactorList = True
                    .HasSearchButton = False
                    .DisplayGroupTree = False
                    .HasSearchButton = True
                End With

                If Not Request("Jenis") Is Nothing Then
                    Select Case Request("Jenis").ToString
                        Case "variance_analysis"

                        Case Else
                            openReport(Request("Jenis").ToString & ".rpt")
                    End Select
                End If
            End If

        Catch ex As Exception
            Throw New Exception("Error Page Load :" & ex.ToString)
        End Try

    End Sub
#End Region

#Region "REPORT"
    Private Sub openReport(ByVal reportPath As String)
        Try
            report.Load(Server.MapPath(reportPath))
            dt = setqueryreport(Request("Jenis"))
            report.SetDataSource(dt)
            'setparameter()
            CrystalReportViewer1.ReportSource = report
            Session("reportDocQuo") = report

            'sqlstr = "SELECT Nama FROM HeaderForm where nama LIKE '%" & initial & "%' [status] = 1"
            'dt = SQLExecuteQuery(sqlstr).Tables(0)


        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

        'setRecordSelectionFormula()
        'report.SetDatabaseLogon(username, password)
    End Sub
    Private Sub setparameter()

        Dim tahunSebelumnya As Integer
        Dim tahun As Integer
        Select Case Request("rpt")
            Case "balance"
                report.SetParameterValue("lastmonth", Request("lastmonth"))
                report.SetParameterValue("currentmonth", Request("currentmonth"))
            Case "LabaRugi"
                report.SetParameterValue("lastdayofyear", Request("lastdayofyear"))
            Case "ReportNeraca"
                report.SetParameterValue("lastdayofyear", Request("lastdayofyear"))
            Case "NeracaLajur"
                tahun = CDate(Request("startdate")).ToString("yyyy")
                tahunSebelumnya = tahun - 1
                report.SetParameterValue("lastdayofyear", Request("lastdayofyear"))
                report.SetParameterValue("tahun", tahun)
                report.SetParameterValue("tahunSebelumnya", tahunSebelumnya)
            Case "rptJournal"

        End Select

        If Not IsNothing(Request("str")) Then
            If Request("str") <> "" Then
                'If Not IsNothing(Request("itemcode")) Then
                '    If Request("itemcode").ToString.Trim <> "" Then
                '        report.SetParameterValue("str", Request("itemcode").ToString & " - " & GetItemName(Request("itemcode").ToString) & " (" & GetPackingUnit(Request("itemcode").ToString) & ")" & " " & Request("str"))
                '    Else
                '        report.SetParameterValue("str", Request("str"))
                '    End If
                'Else
                report.SetParameterValue("str", Request("str"))
                'End If
            Else
                report.SetParameterValue("str", "*")
            End If
        End If
    End Sub
    Private Function setqueryreport(ByVal rpt As String) As DataTable
        Dim RDT As DataTable
        Dim str As String = ""
       

        Try
            str = "select MQ.Quotation_No, MC.Nama_Customer,mq.Penerima, QD.Nama_Barang, MHD.NamaHarga, QD.Harga " & _
                    "from MasterQuotation MQ " & _
                    "JOIN QuotationDetail QD ON MQ.Quotation_No = QD.Quotation_No " & _
                    "JOIN MasterHargaDefault MHD ON QD.SatuanID = MHD.ID " & _
                    "JOIN MasterCustomer MC on MQ.Customer_Id = MC.Kode_Customer " & _
                    "WHERE MQ.status <> 0 " & _
                    "AND QD.status <> 0 " & _
                    "and MHD.status <> 0 " & _
                    "and MC.status <> 0 " & _
                    "order by MC.Nama_Customer "

            RDT = SQLExecuteQuery(str).Tables(0)

            Return RDT




        Catch ex As Exception
            Throw New Exception("Error function setqueryreport :" & ex.ToString)
        End Try
        Return RDT
    End Function


#End Region
End Class