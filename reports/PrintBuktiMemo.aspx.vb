Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Partial Public Class PrintBuktiMemo
    Inherits System.Web.UI.Page
    Dim reportPath As String
    Dim report As New ReportDocument
    Dim dt As DataTable
    Dim sqlString As String

    Private Sub PrintBuktiMemo_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Session("reportTipeJournal") Is Nothing Then
            report = New ReportDocument
            report = CType(Session("reportTipeJournal"), ReportDocument)
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

                If Not Request("TypeJournal") Is Nothing Then
                    openReport(Request("TypeJournal").ToString & ".rpt")
                End If
            End If

        Catch ex As Exception
            Throw New Exception("Error page_load function :" & ex.ToString)
        End Try
    End Sub


    Private Sub openReport(ByVal reportPath As String)
        Try
            Dim Number As String
            If Not Request("GeneralJournalID") Is Nothing Then
                Number = Request("GeneralJournalID")
            Else
                Number = Request("GeneralJournalIDB")
            End If

            report.Load(Server.MapPath(reportPath))
            dt = setqueryreport(Number)
            report.SetDataSource(dt)
            CrystalReportViewer1.ReportSource = report
            Session("reportTipeJournal") = report
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

        'setRecordSelectionFormula()
        'report.SetDatabaseLogon(username, password)
    End Sub
    Private Function setqueryreport(ByVal rpt As String) As DataTable
        Dim RDT As New DataTable
        Dim dt As DataTable
        Dim DR As DataRow
        Try
            RDT.Columns.Add(New DataColumn("Tanggal", GetType(String)))
            RDT.Columns.Add(New DataColumn("AccCode", GetType(String)))
            RDT.Columns.Add(New DataColumn("DebitAmount", GetType(String)))
            RDT.Columns.Add(New DataColumn("CreditAmount", GetType(String)))
            RDT.Columns.Add(New DataColumn("Description", GetType(String)))
            RDT.Columns.Add(New DataColumn("DorC", GetType(String)))
            Select Case Request("TypeJournal").ToString
                Case "Pembayaran"
                    sqlString = " Select gj.Tanggal,gjd.AccCode,gjd.DorC,gjd.Amount as DebitAmount,gjd.Amount as CreditAmount,gjd.Description from GeneralJournal gj,GeneralJournalDetail gjd where gj.GJNO= gjd.GJNO and " & _
                                        " gj.GJNO= '" & rpt.ToString & "' and (gj.status = 1 or gj.status = 10  )"
                Case "Memo"
                    sqlString = " Select gj.Tanggal,gjd.AccCode,gjd.DorC, " & _
                                        "(Select case when gjd.DorC='Debit' then (gjd.Amount) else (" & _
                                        "'0') end) as DebitAmount ," & _
                                        "(Select case when gjd.DorC='Credit' then (gjd.Amount) else (" & _
                                        "'0') end) as CreditAmount ," & _
                                        " gjd.Description from GeneralJournal gj,GeneralJournalDetail gjd where gj.GJNO= gjd.GJNO and " & _
                                        " gj.GJNO= '" & rpt.ToString & "' and (gj.status = 1 or gj.status = 10)"
            End Select

            dt = SQLExecuteQuery(sqlString).Tables(0)
            If dt.Rows.Count > 0 Then
                For j As Integer = 0 To dt.Rows.Count - 1
                    DR = RDT.NewRow()
                    With dt.Rows(j)
                        DR("Tanggal") = CDate(.Item("Tanggal")).ToString("dd MMMM yyyy")
                        DR("AccCode") = .Item("AccCode").ToString
                        DR("DebitAmount") = UbahKoma(.Item("DebitAmount").ToString)
                        DR("CreditAmount") = UbahKoma(.Item("CreditAmount").ToString)
                        DR("Description") = .Item("Description").ToString
                        DR("DorC") = .Item("DorC").ToString
                    End With
                    RDT.Rows.Add(DR)
                Next
            End If
           
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To (6 - dt.Rows.Count)
                    DR = RDT.NewRow()
                    DR("Tanggal") = ""
                    DR("AccCode") = ""
                    DR("DebitAmount") = ""
                    DR("CreditAmount") = ""
                    DR("Description") = ""
                    DR("DorC") = ""
                    RDT.Rows.Add(DR)
                Next
            End If


        Catch ex As Exception
            Throw New Exception("Error function setqueryreport :" & ex.ToString)
        End Try
        Return RDT
    End Function
End Class