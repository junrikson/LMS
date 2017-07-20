Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

'***********************************************************************************************************************************************
' TAG               Programmer      Purpose
' EH00_20111014_01  Eddy Handaya    Fix bug on Inventory Report
'***********************************************************************************************************************************************

Partial Public Class ReportAcc
    Inherits System.Web.UI.Page
    Dim reportPath As String
    Dim report As New ReportDocument
    Dim dt As DataTable
    Dim dt2 As New DataTable
    Dim sqlString As String
    Dim sqlstr As String
#Region "PAGE"
    Private Sub ReportAcc_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not Session("reportDoc") Is Nothing Then
            report = New ReportDocument
            report = CType(Session("reportDoc"), ReportDocument)
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

                If Not Request("rpt") Is Nothing Then
                    Select Case Request("rpt").ToString
                        Case "variance_analysis"

                        Case Else
                            openReport(Request("rpt").ToString & ".rpt")
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
            dt = setqueryreport(Request("rpt"))
            report.SetDataSource(dt)
            setparameter()
            CrystalReportViewer1.ReportSource = report
            Session("reportDoc") = report

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
        Dim lastmonth As String = ""


        Select Case Request("rpt")
            Case "balance"

                lastmonth = Request("lastmonth")

                If lastmonth.ToString.Contains("undefined") Then
                    lastmonth = Request("firstdayofyear")
                End If

                report.SetParameterValue("lastmonth", lastmonth)
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

            Case "rptAR"
            Case "LapPembayaran"
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
        Dim datefrom As String
        Dim dateto As String
        Dim AccountNoStart As String
        Dim AccountNoEnd As String
        Dim startdate As String
        Dim enddate As String
        Dim firstdayofyear As String
        Dim lastdayofyear As String
        Dim lastmonth As String
        Dim currentmonth As String
        Dim Tipe As String
        Tipe = Request("tipe")
        Dim TipeKapal As String
        TipeKapal = Request("tipekapal")

        Try
            Select Case Request("rpt").ToString
                Case "rptPendapatan"
                    Dim statbyr As String = Request("statbyr")
                    datefrom = Request("transdatefrom")
                    dateto = Request("transdateto")
                    firstdayofyear = Request("firstdayofyear")

                    If Tipe = "Semua" Then
                        If statbyr = "Semua" Then
                            sqlString = " (select ih.No_Invoice,ih.Total,mc.Nama_Customer,mc.Kode_Customer,ih.InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, ih.ditujukan " & _
                                        " from InvoiceHeader ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where ih.status <> 0 and ih.InvoiceDate between '" & datefrom & "' AND '" & dateto & "' " & _
                                        " UNION ALL " & _
                                        " select ih.No_Asuransi As No_Invoice,ih.Harga as Total,mc.Nama_Customer,mc.Kode_Customer,ih.TglAsuransi as InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, mc.Nama_Customer as ditujukan " & _
                                        " from Asuransi ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where ih.status <> 0 and ih.TglAsuransi between '" & datefrom & "' AND '" & dateto & "')ORDER BY invoicedate  "
                        ElseIf statbyr = "Lunas" Then
                            sqlString = " (select ih.No_Invoice,ih.Total,mc.Nama_Customer,mc.Kode_Customer,ih.InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, ih.ditujukan " & _
                                        " from InvoiceHeader ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where ih.status <> 0 and ih.[statuspembayaran] = 'Lunas' and ih.InvoiceDate between '" & datefrom & "' AND '" & dateto & "' " & _
                                        " UNION ALL " & _
                                        " select ih.No_Asuransi As No_Invoice,ih.Harga as Total,mc.Nama_Customer,mc.Kode_Customer,ih.TglAsuransi as InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, mc.Nama_Customer as ditujukan " & _
                                        " from Asuransi ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where ih.status <> 0 AND ih.[statuspembayaran] = 'Lunas' and ih.TglAsuransi between '" & datefrom & "' AND '" & dateto & "')ORDER BY invoicedate  "
                        ElseIf statbyr = "BelumLunas" Then
                            sqlString = " (select ih.No_Invoice,ih.Total,mc.Nama_Customer,mc.Kode_Customer,ih.InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, ih.ditujukan " & _
                                        " from InvoiceHeader ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where ih.status <> 0 and ih.[statuspembayaran] = 'Belum bayar' and ih.InvoiceDate between '" & datefrom & "' AND '" & dateto & "' " & _
                                        " UNION ALL " & _
                                        " select ih.No_Asuransi As No_Invoice,ih.Harga as Total,mc.Nama_Customer,mc.Kode_Customer,ih.TglAsuransi as InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, mc.Nama_Customer as ditujukan " & _
                                        " from Asuransi ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where ih.status <> 0 AND ih.[statuspembayaran] = 'Belum bayar' and ih.TglAsuransi between '" & datefrom & "' AND '" & dateto & "')ORDER BY invoicedate  "

                        End If


                    Else
                        If statbyr = "Semua" Then
                            sqlString = " (select ih.No_Invoice,ih.Total,mc.Nama_Customer,mc.Kode_Customer,ih.InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, ih.ditujukan " & _
                                            " from InvoiceHeader ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                            " left join Kapal k on ih.KapalID = k.id " & _
                                            " where LEFT(ih.No_Invoice,1) <> 'B' and ih.status <> 0 and ih.InvoiceDate between '" & datefrom & "' AND '" & dateto & "' " & _
                                            " UNION ALL " & _
                                            " select ih.No_Asuransi As No_Invoice,ih.Harga as Total,mc.Nama_Customer,mc.Kode_Customer,ih.TglAsuransi as InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, mc.Nama_Customer as ditujukan " & _
                                            " from Asuransi ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                            " left join Kapal k on ih.KapalID = k.id " & _
                                            " where LEFT(ih.No_Asuransi,1) <> 'B' and ih.status <> 0 and ih.TglAsuransi between '" & datefrom & "' AND '" & dateto & "')ORDER BY invoicedate  "
                        ElseIf statbyr = "Lunas" Then
                            sqlString = " (select ih.No_Invoice,ih.Total,mc.Nama_Customer,mc.Kode_Customer,ih.InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, ih.ditujukan " & _
                                        " from InvoiceHeader ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where LEFT(ih.No_Invoice,1) <> 'B' and ih.status <> 0 and ih.[statuspembayaran] = 'Lunas' and ih.InvoiceDate between '" & datefrom & "' AND '" & dateto & "' " & _
                                        " UNION ALL " & _
                                        " select ih.No_Asuransi As No_Invoice,ih.Harga as Total,mc.Nama_Customer,mc.Kode_Customer,ih.TglAsuransi as InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, mc.Nama_Customer as ditujukan " & _
                                        " from Asuransi ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where LEFT(ih.No_Asuransi,1) <> 'B' and ih.status <> 0 AND ih.[statuspembayaran] = 'Lunas' and ih.TglAsuransi between '" & datefrom & "' AND '" & dateto & "')ORDER BY invoicedate  "
                        ElseIf statbyr = "BelumLunas" Then
                            sqlString = " (select ih.No_Invoice,ih.Total,mc.Nama_Customer,mc.Kode_Customer,ih.InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, ih.ditujukan " & _
                                        " from InvoiceHeader ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where LEFT(ih.No_Invoice,1) <> 'B' and ih.status <> 0 and ih.[statuspembayaran] = 'Belum bayar' and ih.InvoiceDate between '" & datefrom & "' AND '" & dateto & "' " & _
                                        " UNION ALL " & _
                                        " select ih.No_Asuransi As No_Invoice,ih.Harga as Total,mc.Nama_Customer,mc.Kode_Customer,ih.TglAsuransi as InvoiceDate ,k.Alias_Kapal, ih.statuspembayaran, mc.Nama_Customer as ditujukan " & _
                                        " from Asuransi ih left join MasterCustomer mc on IH.CodeCust = mc.Kode_Customer  " & _
                                        " left join Kapal k on ih.KapalID = k.id " & _
                                        " where LEFT(ih.No_Asuransi,1) <> 'B' and ih.status <> 0 AND ih.[statuspembayaran] = 'Belum bayar' and ih.TglAsuransi between '" & datefrom & "' AND '" & dateto & "')ORDER BY invoicedate "


                        End If

                    End If

                    RDT = SQLExecuteQuery(sqlString).Tables(0)

                Case "rptSubLedgerPiutang"
                    Dim KodeCust As String = Request("KodeCust")
                    datefrom = Request("transdatefrom")
                    dateto = Request("transdateto")
                    firstdayofyear = Request("firstdayofyear")


                    If Tipe = "Semua" Then

                        If KodeCust = "kosong" Then

                            sqlString = "(SELECT  No_Invoice, InvoiceDate, Ditujukan,No_Invoice as Description, Total, 0 as Kredit, " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD  " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate < '" & datefrom & "' ),0) as SaldoKredit, " & _
                                        "(select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ) - " & _
                                        "(select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate BETWEEN '" & datefrom & "' AND '" & dateto & "') as Selisih " & _
                                        "FROM InvoiceHeader WHere [status] <> 0  " & _
                                        "AND InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' " & _
                                        "UNION ALL " & _
                                        "select jsh.RefNo, JSH.TransDate, IH.Ditujukan,JSD.Description, 0 as total, Amount as kredit,   " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate < '" & datefrom & "'),0) as SaldoKredit,  " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) - " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) as Selisih " & _
                                        "FROM JournalSalesDetail JSD    " & _
                                        "join JournalSalesHeader JSH ON JSH.RunNo=JSD.RunNoHeader   " & _
                                        "JOIN InvoiceHeader ih ON ih.No_Invoice = JSH.RefNo   " & _
                                        "WHERE JSD.Type = 'Kredit' AND JSH.RunNo = JSD.RunNoHeader   " & _
                                        "AND JSD.status <> 0 and JSH.status <> 0   " & _
                                        "AND ih.status <> 0  " & _
                                        "AND JSH.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "') " & _
                                        "UNION ALL " & _
                                        "(SELECT No_Asuransi as No_Invoice,TglAsuransi as InvoiceDate, Ditujukan, No_Asuransi as Description,Harga as Total, 0 as Kredit, " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD  " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate < '" & datefrom & "' ),0) as SaldoKredit, " & _
                                        "(select SUM(Harga) from asuransi where status <> 0 and TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' ) - " & _
                                        "(select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate BETWEEN '" & datefrom & "' AND '" & dateto & "') as Selisih " & _
                                        "FROM asuransi WHere [status] <> 0  " & _
                                        "AND TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' " & _
                                        "UNION ALL " & _
                                        "select jsh.RefNo, JSH.TransDate, IH.Ditujukan,JSD.Description, 0 as total, Amount as kredit,   " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate < '" & datefrom & "'),0) as SaldoKredit,  " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) - " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) as Selisih " & _
                                        "FROM JournalSalesDetail JSD    " & _
                                        "join JournalSalesHeader JSH ON JSH.RunNo=JSD.RunNoHeader   " & _
                                        "JOIN asuransi ih ON ih.No_Asuransi = JSH.RefNo     " & _
                                        "WHERE JSD.Type = 'Kredit' AND JSH.RunNo = JSD.RunNoHeader   " & _
                                        "AND JSD.status <> 0 and JSH.status <> 0   " & _
                                        "AND ih.status <> 0  " & _
                                        "AND JSH.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ) " & _
                                        "ORDER BY No_Invoice, InvoiceDate"
                        Else

                            sqlString = "(SELECT  No_Invoice, InvoiceDate, MD.Nama_Customer as Ditujukan,No_Invoice as Description, Total, 0 as Kredit, " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD  " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate < '" & datefrom & "' ),0) as SaldoKredit, " & _
                                        "(select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ) - " & _
                                        "(select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate BETWEEN '" & datefrom & "' AND '" & dateto & "') as Selisih " & _
                                        "FROM InvoiceHeader " & _
                                        "JOIN MasterCustomer MD ON MD.Kode_Customer = InvoiceHeader.CodeCust " & _
                                        " WHere InvoiceHeader.[status] <> 0  " & _
                                        "AND InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' AND CodeCust = '" & KodeCust & "' " & _
                                        "UNION ALL " & _
                                        "select jsh.RefNo, JSH.TransDate, MD.Nama_Customer as Ditujukan,JSD.Description, 0 as total, Amount as kredit,   " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate < '" & datefrom & "'),0) as SaldoKredit,  " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) - " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) as Selisih " & _
                                        "FROM JournalSalesDetail JSD    " & _
                                        "join JournalSalesHeader JSH ON JSH.RunNo=JSD.RunNoHeader   " & _
                                        "JOIN InvoiceHeader ih ON ih.No_Invoice = JSH.RefNo   " & _
                                        "JOIN MasterCustomer MD ON MD.Kode_Customer = IH.CodeCust " & _
                                        "WHERE JSD.Type = 'Kredit' AND JSH.RunNo = JSD.RunNoHeader   " & _
                                        "AND JSD.status <> 0 and JSH.status <> 0   " & _
                                        "AND ih.status <> 0  " & _
                                        "AND JSH.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' AND IH.CodeCust = '" & KodeCust & "'  ) " & _
                                        "UNION ALL " & _
                                        "(SELECT No_Asuransi as No_Invoice,TglAsuransi as InvoiceDate, MD.Nama_Customer as Ditujukan, No_Asuransi as Description,Harga as Total, 0 as Kredit, " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD  " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate < '" & datefrom & "' ),0) as SaldoKredit, " & _
                                        "(select SUM(Harga) from asuransi where status <> 0 and TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' ) - " & _
                                        "(select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate BETWEEN '" & datefrom & "' AND '" & dateto & "') as Selisih " & _
                                        "FROM asuransi " & _
                                        "JOIN MasterCustomer MD ON MD.Kode_Customer = Asuransi.CodeCust " & _
                                        "WHere asuransi.[status] <> 0  " & _
                                        "AND TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' AND CodeCust = '" & KodeCust & "' " & _
                                        "UNION ALL " & _
                                        "select jsh.RefNo, JSH.TransDate, MD.Nama_Customer as Ditujukan,JSD.Description, 0 as total, Amount as kredit,   " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate < '" & datefrom & "'),0) as SaldoKredit,  " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) - " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) as Selisih " & _
                                        "FROM JournalSalesDetail JSD    " & _
                                        "join JournalSalesHeader JSH ON JSH.RunNo=JSD.RunNoHeader   " & _
                                        "JOIN asuransi ih ON ih.No_Asuransi = JSH.RefNo     " & _
                                        "JOIN MasterCustomer MD ON MD.Kode_Customer = IH.CodeCust " & _
                                        "WHERE JSD.Type = 'Kredit' AND JSH.RunNo = JSD.RunNoHeader   " & _
                                        "AND JSD.status <> 0 and JSH.status <> 0   " & _
                                        "AND ih.status <> 0  " & _
                                        "AND JSH.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' AND IH.CodeCust = '" & KodeCust & "' ) " & _
                                        "ORDER BY No_Invoice, InvoiceDate"


                        End If

                    Else
                        If KodeCust = "kosong" Then


                            sqlString = "(SELECT  No_Invoice, InvoiceDate, Ditujukan,No_Invoice as Description, Total, 0 as Kredit, " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD  " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate < '" & datefrom & "' ),0) as SaldoKredit, " & _
                                        "(select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ) - " & _
                                        "(select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate BETWEEN '" & datefrom & "' AND '" & dateto & "') as Selisih " & _
                                        "FROM InvoiceHeader WHere [status] <> 0  " & _
                                        "AND InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' " & _
                                        " AND LEFT(No_Invoice, 1) <> 'B' " & _
                                        "UNION ALL " & _
                                        "select jsh.RefNo, JSH.TransDate, IH.Ditujukan,JSD.Description, 0 as total, Amount as kredit,   " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate < '" & datefrom & "'),0) as SaldoKredit,  " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) - " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) as Selisih " & _
                                        "FROM JournalSalesDetail JSD    " & _
                                        "join JournalSalesHeader JSH ON JSH.RunNo=JSD.RunNoHeader   " & _
                                        "JOIN InvoiceHeader ih ON ih.No_Invoice = JSH.RefNo   " & _
                                        "WHERE JSD.Type = 'Kredit' AND JSH.RunNo = JSD.RunNoHeader   " & _
                                        "AND JSD.status <> 0 and JSH.status <> 0   " & _
                                        "AND ih.status <> 0  " & _
                                        "AND JSH.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' " & _
                                        "AND LEFT(JSH.refno, 1) <> 'B' )" & _
                                        "UNION ALL " & _
                                        "(SELECT No_Asuransi as No_Invoice,TglAsuransi as InvoiceDate, Ditujukan, No_Asuransi as Description,Harga as Total, 0 as Kredit, " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD  " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate < '" & datefrom & "' ),0) as SaldoKredit, " & _
                                        "(select SUM(Harga) from asuransi where status <> 0 and TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' ) - " & _
                                        "(select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate BETWEEN '" & datefrom & "' AND '" & dateto & "') as Selisih " & _
                                        "FROM asuransi WHere [status] <> 0  " & _
                                        "AND TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' " & _
                                        "AND LEFT(No_Asuransi, 1) <> 'B' " & _
                                        "UNION ALL " & _
                                        "select jsh.RefNo, JSH.TransDate, IH.Ditujukan,JSD.Description, 0 as total, Amount as kredit,   " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate < '" & datefrom & "'),0) as SaldoKredit,  " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) - " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) as Selisih " & _
                                        "FROM JournalSalesDetail JSD    " & _
                                        "join JournalSalesHeader JSH ON JSH.RunNo=JSD.RunNoHeader   " & _
                                        "JOIN asuransi ih ON ih.No_Asuransi = JSH.RefNo     " & _
                                        "WHERE JSD.Type = 'Kredit' AND JSH.RunNo = JSD.RunNoHeader   " & _
                                        "AND JSD.status <> 0 and JSH.status <> 0   " & _
                                        "AND ih.status <> 0  " & _
                                        "AND JSH.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ) " & _
                                        "AND LEFT(JSH.refno, 1) <> 'B' " & _
                                        "ORDER BY No_Invoice, InvoiceDate"

                        Else

                            sqlString = "(SELECT  No_Invoice, InvoiceDate, Ditujukan,No_Invoice as Description, Total, 0 as Kredit, " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD  " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate < '" & datefrom & "' ),0) as SaldoKredit, " & _
                                        "(select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ) - " & _
                                        "(select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate BETWEEN '" & datefrom & "' AND '" & dateto & "') as Selisih " & _
                                        "FROM InvoiceHeader WHere [status] <> 0  " & _
                                        "AND InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' AND CodeCust = '" & KodeCust & "' " & _
                                        "AND LEFT(No_Invoice, 1) <> 'B' " & _
                                        "UNION ALL " & _
                                        "select jsh.RefNo, JSH.TransDate, IH.Ditujukan,JSD.Description, 0 as total, Amount as kredit,   " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate < '" & datefrom & "'),0) as SaldoKredit,  " & _
                                        "ISNULL((select SUM(total) from InvoiceHeader where status <> 0 and InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) - " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) as Selisih " & _
                                        "FROM JournalSalesDetail JSD    " & _
                                        "join JournalSalesHeader JSH ON JSH.RunNo=JSD.RunNoHeader   " & _
                                        "JOIN InvoiceHeader ih ON ih.No_Invoice = JSH.RefNo   " & _
                                        "WHERE JSD.Type = 'Kredit' AND JSH.RunNo = JSD.RunNoHeader   " & _
                                        "AND JSD.status <> 0 and JSH.status <> 0   " & _
                                        "AND ih.status <> 0  " & _
                                        "AND LEFT(JSH.refno, 1) <> 'B' " & _
                                        "AND JSH.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' AND IH.CodeCust = '" & KodeCust & "'  ) " & _
                                        "UNION ALL " & _
                                        "(SELECT No_Asuransi as No_Invoice,TglAsuransi as InvoiceDate, Ditujukan, No_Asuransi as Description,Harga as Total, 0 as Kredit, " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD  " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate < '" & datefrom & "' ),0) as SaldoKredit, " & _
                                        "(select SUM(Harga) from asuransi where status <> 0 and TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' ) - " & _
                                        "(select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail JSD " & _
                                        "JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                        "where JSH.status <> 0 AND JSD.[status] <> 0 and jsd.Type = 'kredit' AND JSH.Transdate BETWEEN '" & datefrom & "' AND '" & dateto & "') as Selisih " & _
                                        "FROM asuransi WHere [status] <> 0  " & _
                                        "AND TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' AND CodeCust = '" & KodeCust & "' " & _
                                        "AND LEFT(No_Asuransi, 1) <> 'B' " & _
                                        "UNION ALL " & _
                                        "select jsh.RefNo, JSH.TransDate, IH.Ditujukan,JSD.Description, 0 as total, Amount as kredit,   " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi < '" & datefrom & "'),0) as SaldoDebet, " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate < '" & datefrom & "'),0) as SaldoKredit,  " & _
                                        "ISNULL((select SUM(Harga) from asuransi where status <> 0 and TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) - " & _
                                        "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(Amount, 'Infinity', '0'), 'NaN', '0'), 0))) from JournalSalesDetail where status <> 0 and Type = 'kredit' and JSH.transdate BETWEEN '" & datefrom & "' AND '" & dateto & "'),0) as Selisih " & _
                                        "FROM JournalSalesDetail JSD    " & _
                                        "join JournalSalesHeader JSH ON JSH.RunNo=JSD.RunNoHeader   " & _
                                        "JOIN asuransi ih ON ih.No_Asuransi = JSH.RefNo     " & _
                                        "WHERE JSD.Type = 'Kredit' AND JSH.RunNo = JSD.RunNoHeader   " & _
                                        "AND JSD.status <> 0 and JSH.status <> 0   " & _
                                        "AND ih.status <> 0  " & _
                                        "AND LEFT(JSH.refno, 1) <> 'B' " & _
                                        "AND JSH.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' AND IH.CodeCust = '" & KodeCust & "' ) " & _
                                        "ORDER BY No_Invoice, InvoiceDate"

                        End If
                    End If



                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                Case "rptPiutang"
                    datefrom = Request("transdatefrom")
                    dateto = Request("transdateto")
                    firstdayofyear = Request("firstdayofyear")

                    If Tipe = "Semua" Then
                        sqlString = "SELECT  IH.No_Invoice, IH.InvoiceDate, IH.Ditujukan, IH.Total, " & _
                                   "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(JSD.Amount, 'Infinity', '0'), 'NaN', '0'), 0)))   " & _
                                   "FROM JournalSalesDetail JSD JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                   "WHERE(JSH.RunNo = JSD.RunNoHeader And IH.No_Invoice = JSH.RefNo) " & _
                                   "AND JSD.[status] <> 0 AND JSH.status <> 0 AND IH.[status] <> 0 " & _
                                   "AND JSD.Type = 'Debet'),0) as Kredit " & _
                                   "FROM InvoiceHeader IH  " & _
                                   "WHERE(status <> 0) " & _
                                   "AND IH.InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' AND ih.[statuspembayaran] <> 'Lunas'  " & _
                                   "UNION ALL " & _
                                   "SELECT  IH.No_Asuransi as No_Invoice, IH.TglAsuransi as InvoiceDate, IH.Ditujukan, IH.Harga as Total, " & _
                                    "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(JSD.Amount, 'Infinity', '0'), 'NaN', '0'), 0))) " & _
                                    "FROM JournalSalesDetail JSD JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo     " & _
                                    "WHERE(JSH.RunNo = JSD.RunNoHeader And IH.No_Asuransi = JSH.RefNo) " & _
                                    "AND JSD.[status] <> 0 AND JSH.status <> 0 AND IH.[status] <> 0   " & _
                                    "AND JSD.Type = 'Debet'),0) as Kredit   " & _
                                    "FROM asuransi IH    " & _
                                    "WHERE status <> 0 " & _
                                    "AND IH.TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' AND ih.[statuspembayaran] <> 'Lunas' "


                    Else
                        sqlString = "SELECT  IH.No_Invoice, IH.InvoiceDate, IH.Ditujukan, IH.Total, " & _
                                   "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(JSD.Amount, 'Infinity', '0'), 'NaN', '0'), 0)))   " & _
                                   "FROM JournalSalesDetail JSD JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo  " & _
                                   "WHERE(JSH.RunNo = JSD.RunNoHeader And IH.No_Invoice = JSH.RefNo) " & _
                                   "AND JSD.[status] <> 0 AND JSH.status <> 0 AND IH.[status] <> 0 " & _
                                   "AND JSD.Type = 'Debet'),0) as Kredit " & _
                                   "FROM InvoiceHeader IH  " & _
                                   "WHERE(status <> 0) " & _
                                   "AND IH.InvoiceDate BETWEEN '" & datefrom & "' AND '" & dateto & "' AND ih.[statuspembayaran] <> 'Lunas' AND LEFT(ih.No_Invoice,1) <> 'B'  " & _
                                   "UNION ALL " & _
                                   "SELECT  IH.No_Asuransi as No_Invoice, IH.TglAsuransi as InvoiceDate, IH.Ditujukan, IH.Harga as Total, " & _
                                    "ISNULL((select SUM(CONVERT(FLOAT, ISNULL(REPLACE(REPLACE(JSD.Amount, 'Infinity', '0'), 'NaN', '0'), 0))) " & _
                                    "FROM JournalSalesDetail JSD JOIN JournalSalesHeader JSH ON JSD.RunNoHeader = JSH.RunNo     " & _
                                    "WHERE(JSH.RunNo = JSD.RunNoHeader And IH.No_Asuransi = JSH.RefNo) " & _
                                    "AND JSD.[status] <> 0 AND JSH.status <> 0 AND IH.[status] <> 0   " & _
                                    "AND JSD.Type = 'Debet'),0) as Kredit   " & _
                                    "FROM asuransi IH    " & _
                                    "WHERE status <> 0 " & _
                                    "AND IH.TglAsuransi BETWEEN '" & datefrom & "' AND '" & dateto & "' AND ih.[statuspembayaran] <> 'Lunas' and LEFT(ih.No_Asuransi,1) <> 'B' "

                    End If

                    RDT = SQLExecuteQuery(sqlString).Tables(0)

                Case "rptJournal"
                    datefrom = Request("transdatefrom")
                    dateto = Request("transdateto")
                    firstdayofyear = Request("firstdayofyear")

                    Select Case Session("namaroles")
                        Case "Accounting Pangkal Pinang"
                            If Tipe = "Semua" Then
                                sqlString = "SELECT " & _
                                                " V.* " & _
                                                "     FROM V_Journal_View V " & _
                                                "WHERE " & _
                                                "(LEFT(V.refno, 2) = 'BP' OR LEFT(V.refno, 1) = 'P' OR RefNo = 'ARP') AND " & _
                                                "    V.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ORDER BY V.IDDetail ASC "
                            Else
                                sqlString = "SELECT " & _
                                                " V.* " & _
                                                "     FROM V_Journal_View V " & _
                                                "WHERE " & _
                                                "(LEFT(V.refno, 2) <> 'BP' OR LEFT(V.refno, 1) = 'P' OR RefNo = 'ARP') AND " & _
                                                "    V.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ORDER BY V.IDDetail ASC "
                            End If
                        Case "Accounting Tanjung Pandan"
                            If Tipe = "Semua" Then
                                sqlString = "SELECT " & _
                                                " V.* " & _
                                                "     FROM V_Journal_View V " & _
                                                "WHERE " & _
                                                "(LEFT(V.refno, 2) = 'BT' OR LEFT(V.refno, 1) = 'T' OR RefNo = 'ART') AND  " & _
                                                "    V.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ORDER BY V.IDDetail ASC "
                            Else
                                sqlString = "SELECT " & _
                                                " V.* " & _
                                                "     FROM V_Journal_View V " & _
                                                "WHERE " & _
                                                "(LEFT(V.refno, 2) <> 'BT' OR LEFT(V.refno, 1) = 'T' OR RefNo = 'ART') AND " & _
                                                "    V.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ORDER BY V.IDDetail ASC "
                            End If
                        Case Else
                            If Tipe = "Semua" Then
                                sqlString = "SELECT " & _
                                                " V.* " & _
                                                "     FROM V_Journal_View V " & _
                                                "WHERE " & _
                                                "    V.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ORDER BY V.IDDetail ASC "
                            Else
                                sqlString = "SELECT " & _
                                                " V.* " & _
                                                "     FROM V_Journal_View V " & _
                                                "WHERE " & _
                                                "LEFT(V.refno, 1) <> 'B' AND " & _
                                                "    V.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' ORDER BY V.IDDetail ASC "
                            End If
                    End Select





                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                Case "ledger"
                    'Dim AccPiutang As String
                    'Dim str As String
                    datefrom = Request("transdatefrom")
                    dateto = Request("transdateto")
                    AccountNoStart = Request("accountnostart")
                    AccountNoEnd = Request("accountnoend")
                    firstdayofyear = Request("firstdayofyear")

                    'Str = "SELECT Code from ChartOfAccount Where Name = 'Piutang Dagang Jakarta' and [status] <> 0"
                    'AccPiutang = SQLExecuteScalar(Str)

                    If Tipe = "Semua" Then
                        sqlString = "SELECT " & _
                                   " V.*," & _
                                   "        ISNULL((SELECT SUM(X.Amount) FROM V_JournalGL X " & _
                                   "WHERE X.AccCode=V.AccCode AND " & _
                                   "X.TransDate BETWEEN '" & firstdayofyear.ToString & "' AND DATEADD(DAY,-1, '" & datefrom.ToString & "')),0) AS BeginningBalance" & _
                                   "     FROM V_JournalGL V " & _
                                   "WHERE " & _
                                   "    V.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' " & _
                                   "                         AND (V.AccCode = '" & AccountNoStart & "' OR V.AccCode =  '" & AccountNoEnd & "') " & _
                                   "               ORDER BY V.TransDate"





                    Else
                        sqlString = "SELECT " & _
                                   " V.*," & _
                                   "        ISNULL((SELECT SUM(X.Amount) FROM V_JournalGL X " & _
                                   "WHERE X.AccCode=V.AccCode AND " & _
                                   "X.TransDate BETWEEN '" & firstdayofyear.ToString & "' AND DATEADD(DAY,-1, '" & datefrom.ToString & "')),0) AS BeginningBalance" & _
                                   "     FROM V_JournalGL V " & _
                                   "WHERE " & _
                                   "LEFT(V.refno, 1) <> 'B' AND " & _
                                   "    V.TransDate BETWEEN '" & datefrom & "' AND '" & dateto & "' " & _
                                   "                         AND (V.AccCode = '" & AccountNoStart & "' OR V.AccCode =  '" & AccountNoEnd & "') " & _
                                   "               ORDER BY V.TransDate"

                    End If


                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT
                Case "balance"
                    startdate = Request("startdate")
                    enddate = Request("enddate")
                    firstdayofyear = Request("firstdayofyear")
                    lastmonth = Request("lastmonth")

                    If lastmonth.Contains("undefined") Then
                        lastmonth = firstdayofyear
                    End If

                    currentmonth = Request("currentmonth")

                    If Tipe = "Semua" Then

                        sqlString = " SELECT " & _
                                         "A.Code AS AccCode," & _
                                         "UPPER(A.Name) AS AccountName," & _
                                        "	ISNULL((SELECT SUM(Amount) FROM V_Journal WHERE AccCode = A.Code AND TransDate BETWEEN '" & firstdayofyear.ToString & "' AND'" & lastmonth.ToString & "'),0) AS StartingBalance,  " & _
                                        "	ISNULL((SELECT SUM(Debit) FROM V_Journal WHERE AccCode = A.Code AND TransDate BETWEEN '" & startdate.ToString & "' AND '" & enddate.ToString & "'),0) AS DebitCurrentMonth, " & _
                                        "	ISNULL((SELECT SUM(Credit) FROM V_Journal WHERE AccCode = A.Code AND TransDate BETWEEN '" & startdate.ToString & "' AND '" & enddate.ToString & "'),0) AS CreditCurrentMonth, " & _
                                        "	ISNULL((SELECT SUM(Amount) FROM V_Journal WHERE AccCode = A.Code AND TransDate BETWEEN '" & firstdayofyear.ToString & "' AND '" & enddate.ToString & "'),0) AS EndingBalance " & _
                                        "FROM ChartOfAccount A where status = 1 and Levels <> 1 and Parent <> 'TOP' " & _
                                        "ORDER BY A.Code "

                    Else
                        sqlString = " SELECT " & _
                                         "A.Code AS AccCode," & _
                                         "UPPER(A.Name) AS AccountName," & _
                                        "	ISNULL((SELECT SUM(Amount) FROM V_Journal WHERE AccCode = A.Code AND LEFT(X.refno, 1) <> 'B' AND TransDate BETWEEN '" & firstdayofyear.ToString & "' AND'" & lastmonth.ToString & "'),0) AS StartingBalance,  " & _
                                        "	ISNULL((SELECT SUM(Debit)  FROM V_Journal WHERE AccCode = A.Code AND LEFT(X.refno, 1) <> 'B' AND TransDate BETWEEN '" & startdate.ToString & "' AND '" & enddate.ToString & "'),0) AS DebitCurrentMonth, " & _
                                        "	ISNULL((SELECT SUM(Credit) FROM V_Journal WHERE AccCode = A.Code AND LEFT(X.refno, 1) <> 'B' AND TransDate BETWEEN '" & startdate.ToString & "' AND '" & enddate.ToString & "'),0) AS CreditCurrentMonth, " & _
                                        "	ISNULL((SELECT SUM(Amount) FROM V_Journal WHERE AccCode = A.Code AND LEFT(X.refno, 1) <> 'B' AND TransDate BETWEEN '" & firstdayofyear.ToString & "' AND '" & enddate.ToString & "'),0) AS EndingBalance " & _
                                        "FROM ChartOfAccount A where status = 1 and Levels <> 1 and Parent <> 'TOP' " & _
                                        "ORDER BY A.Code "
                    End If


                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT
                Case "LabaRugi"
                    startdate = Request("startdate")
                    enddate = Request("enddate")
                    firstdayofyear = Request("firstdayofyear")
                    lastdayofyear = Request("lastdayofyear")
                    lastmonth = Request("lastmonth")
                    currentmonth = Request("currentmonth")

                    If Tipe = "Semua" Then
                        'sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                        '                "ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE (Parent = A.Code or AccCode = A.Code) " & _
                        '                "  AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) as Total ," & _
                        '                "(abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE Class='0005' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) ) " & _
                        '                "-" & _
                        '                "abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE Class='0004' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) )) " & _
                        '                "as LabaRugi  " & _
                        '                "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0004' and '0005') and A.Levels=2 Order by A.Code "

                        sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName , " & _
                                    "(Select case A.Code WHEN '0004.411.001.00' " & _
                                    "then  " & _
                                    "abs(ISNULL((SELECT SUM(Amount) FROM V_JournalNeraca WHERE Parent = '0001.114.000.00' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) * -1 )  " & _
                                                        "Else " & _
                                    "ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE (Parent = A.Code or AccCode = A.Code)   " & _
                                    "AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) " & _
                                                            "End " & _
                                    ")as Total ,  " & _
                                    "abs(ISNULL((SELECT SUM(Amount) FROM V_JournalNeraca WHERE Parent = '0001.114.000.00'  AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'),0)) " & _
                                    "- " & _
                                    "abs(ISNULL((SELECT SUM(Amount) FROM V_JournalNeraca WHERE Class='0005' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) )  " & _
                                    "as LabaRugi " & _
                                    "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0004' and '0005')  " & _
                                    "and A.Levels=2 Order by A.Code  "

                    Else
                        sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName , " & _
                                    "(Select case A.Code WHEN '0004.411.001.00' " & _
                                    "then  " & _
                                    "abs(ISNULL((SELECT SUM(Amount) FROM V_JournalNeraca WHERE LEFT(refno, 1) <> 'B' AND Parent = '0001.114.000.00' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) * -1 )  " & _
                                                        "Else " & _
                                    "ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE LEFT(refno, 1) <> 'B' AND (Parent = A.Code or AccCode = A.Code)   " & _
                                    "AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) " & _
                                                            "End " & _
                                    ")as Total ,  " & _
                                    "abs(ISNULL((SELECT SUM(Amount) FROM V_JournalNeraca WHERE LEFT(refno, 1) <> 'B' AND Parent = '0001.114.000.00'  AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'),0)) " & _
                                    "- " & _
                                    "abs(ISNULL((SELECT SUM(Amount) FROM V_JournalNeraca WHERE LEFT(refno, 1) <> 'B' AND Class='0005' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) )  " & _
                                    "as LabaRugi " & _
                                    "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0004' and '0005')  " & _
                                    "and A.Levels=2 Order by A.Code  "

                        'sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                        '                "ISNULL(( SELECT SUM(Amount) FROM V_LabaRugi WHERE LEFT(refno, 1) <> 'B' AND (Parent = A.Code or AccCode = A.Code)  AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) as Total ," & _
                        '                "(abs(ISNULL(( SELECT SUM(Amount) FROM V_LabaRugi WHERE LEFT(refno, 1) <> 'B' AND Class='0004' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) ) " & _
                        '                "-" & _
                        '                "abs(ISNULL(( SELECT SUM(Amount) FROM V_LabaRugi WHERE LEFT(refno, 1) <> 'B' AND Class='0005' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) )) " & _
                        '                "as LabaRugi  " & _
                        '                "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0004' and '0005') and A.Levels=2 Order by A.Code "
                    End If


                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT

                Case "ReportNeraca"
                    startdate = Request("startdate")
                    enddate = Request("enddate")
                    firstdayofyear = Request("firstdayofyear")
                    lastdayofyear = Request("lastdayofyear")
                    lastmonth = Request("lastmonth")
                    currentmonth = Request("currentmonth")

                    If Tipe = "Semua" Then
                        'sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                        '            "ISNULL((select case A.Code " & _
                        '            "when '0003.311.002.00'  " & _
                        '            "then ( " & _
                        '              "(abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE Class='0005' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) )  " & _
                        '            "-" & _
                        '            "abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE Parent = '0001.114.000.00' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) ) ) ) " & _
                        '            "when '0001.114.001.00' " & _
                        '            "then( " & _
                        '            "0 " & _
                        '            ") " & _
                        '            "else ( SELECT SUM(Amount) FROM V_JournalNeraca WHERE (Parent = A.Code or AccCode = A.Code) and Parent <> '0001.114.000.00'  AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "')" & _
                        '            "end ),0) as Total ," & _
                        '            "ISNULL((select case A.[Types] " & _
                        '            "		When '0001' then ('Assets')" & _
                        '            "		When '0002' then ('modal&equity')	" & _
                        '            "		When '0003' then ('modal&equity') end" & _
                        '            "		),0) as jenis " & _
                        '            "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0001' and '0003')" & _
                        '            "and A.Levels=2 AND A.[status] <> 0 Order by A.Code "

                        sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                                        "ISNULL((select case A.Code " & _
                                        "when '0003.311.003.00'  " & _
                                        "then ( " & _
                                          "(abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE Parent = '0001.114.000.00'   AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'), 0) )  " & _
                                        "-" & _
                                        "abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE Class='0005'   AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) ) ) ) " & _
                                        "else ( SELECT SUM(Amount) FROM V_JournalNeraca WHERE (Parent = A.Code or AccCode = A.Code)   AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "')" & _
                                        "end ),0) as Total ," & _
                                        "ISNULL((select case A.[Types] " & _
                                        "		When '0001' then ('Assets')" & _
                                        "		When '0002' then ('modal&equity')	" & _
                                        "		When '0003' then ('modal&equity') end" & _
                                        "		),0) as jenis " & _
                                        "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0001' and '0003')" & _
                                        "and A.Levels=2 AND A.[status] <> 0 Order by A.Code "
                    Else
                        'sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                        '            "ISNULL((select case A.Code " & _
                        '            "when '0003.311.002.00'  " & _
                        '            "then ( " & _
                        '              "(abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE  Class='0005' AND LEFT(refno, 1) <> 'B' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) )  " & _
                        '            "-" & _
                        '            "abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE Parent = '0001.114.000.00' AND LEFT(refno, 1) <> 'B' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) ) ) ) " & _
                        '            "when '0001.114.001.00' " & _
                        '            "then( " & _
                        '            "0 " & _
                        '            ") " & _
                        '            "else ( SELECT SUM(Amount) FROM V_JournalNeraca WHERE (Parent = A.Code or AccCode = A.Code) and Parent <> '0001.114.000.00'  AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "')" & _
                        '            "end ),0) as Total ," & _
                        '            "ISNULL((select case A.[Types] " & _
                        '            "		When '0001' then ('Assets')" & _
                        '            "		When '0002' then ('modal&equity')	" & _
                        '            "		When '0003' then ('modal&equity') end" & _
                        '            "		),0) as jenis " & _
                        '            "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0001' and '0003')" & _
                        '            "and A.Levels=2 AND A.[status] <> 0 Order by A.Code "

                        sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                                       "ISNULL((select case A.Code " & _
                                       "when '0003.311.003.00'  " & _
                                       "then ( " & _
                                         "(abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca  WHERE LEFT(refno, 1) <> 'B' AND Class='0005'  AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) )  " & _
                                       "-" & _
                                       "abs(ISNULL(( SELECT SUM(Amount) FROM V_JournalNeraca WHERE LEFT(refno, 1) <> 'B' AND Class='0004'   AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis ='Normal'), 0) ) ) ) " & _
                                       "else ( SELECT SUM(Amount) FROM V_JournalNeraca WHERE LEFT(refno, 1) <> 'B' AND (Parent = A.Code or AccCode = A.Code)   AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "')" & _
                                       "end ),0) as Total ," & _
                                       "ISNULL((select case A.[Types] " & _
                                       "		When '0001' then ('Assets')" & _
                                       "		When '0002' then ('modal&equity')	" & _
                                       "		When '0003' then ('modal&equity') end" & _
                                       "		),0) as jenis " & _
                                       "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0001' and '0003')" & _
                                       "and A.Levels=2 AND A.[status] <> 0 Order by A.Code "
                    End If


                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT

                Case "NeracaLajur"

                    startdate = Request("startdate")
                    enddate = Request("enddate")
                    firstdayofyear = Request("firstdayofyear")
                    lastdayofyear = Request("lastdayofyear")
                    lastmonth = Request("lastmonth")
                    currentmonth = Request("currentmonth")

                    sqlString = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                                        "ISNULL((select case A.Code " & _
                                        "when '0003.311.002.00'  " & _
                                        "then ( " & _
                                          "(abs(ISNULL(( SELECT SUM(Amount) FROM V_LabaRugi WHERE Class='0004' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis = 'Normal' ), 0))  " & _
                                        "-" & _
                                        "abs(ISNULL(( SELECT SUM(Amount) FROM V_LabaRugi WHERE Class='0005' AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "' and Jenis = 'Normal'), 0)))  " & _
                                        " * - 1) " & _
                                        "else ( SELECT SUM(Amount) FROM V_LabaRugi WHERE (Parent = A.Code or AccCode = A.Code) AND TransDate Between '" & firstdayofyear & "' and '" & lastdayofyear & "'  and Jenis = 'Normal')" & _
                                        "end ),0) as Total ," & _
                                        "ISNULL((Select SUM(Amount) from V_LabaRugi where TransDate = '" & firstdayofyear & "' and (Parent = A.Code or AccCode = A.Code)  and Jenis = 'Tutup'),0) as BeginningBalance," & _
                                        "ISNULL((select case A.[Types] " & _
                                        "		When '0001' then ('Assets')" & _
                                        "		When '0002' then ('modal&equity')	" & _
                                        "		When '0003' then ('modal&equity') end " & _
                                        "		),0) as jenis " & _
                                        "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types]= '0001' or A.[Types] = '0002' or A.[Types] ='0003')" & _
                                        "and A.Levels=2 and A.[status] <> 0 Order by A.Code "

                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT


                Case "rptStock"
                    Dim gdg As String = Request("Gudang")
                    Dim tujuan As String = Request("Tujuan")
                    startdate = Request("transdatefrom")
                    enddate = Request("transdateto")

                    '***** EH00_20111014_01 START *****
                    'sqlString = "select row_number() OVER(ORDER BY C.Time_Itemcome asc) AS 'RowNumber', " & _
                    '            "c.namaquo, c.harga, C.namaquo,C.Harga, C.JenisPembayaran, C.TotalKubik, " & _
                    '            "C.Nama_Barang, C.pengirim, C.Penerima, C.WarehouseItem_Code, " & _
                    '            "C.Tujuan, C.Warehouse_Name, C.Nama_Kapal, C.Time_Itemcome, C.Tanggal, C.Unit, " & _
                    '            "C.TotalBerat, C.TotalQty " & _
                    '            "FROM " & _
                    '            "(select A.namaquo, A.Harga,  " & _
                    '            "A.JenisTarif As JenisPembayaran, SUM(A.TotalQty) as TotalQty,  " & _
                    '            "SUM(A.TotalBerat) as TotalBerat  , SUM(A.TotalKubik) as TotalKubik , " & _
                    '            "A.Nama_Barang, A.pengirim, A.Penerima, A.WarehouseItem_Code, " & _
                    '            "A.Tujuan, A.Warehouse_Name, A.Nama_Kapal, A.Time_Itemcome, COnvert(varchar,A.Tanggal ,106) as Tanggal, A.Unit   " & _
                    '            "FROM   " & _
                    '            "(select wd.Container,qd.Nama_Barang as namaquo, k.Nama_Kapal,  " & _
                    '            " mh.NamaHarga as JenisTarif, qd.Harga, wd.QtyMsk as TotalQty,  " & _
                    '            " cast(wd.Panjang * wd.Lebar * wd.Tinggi * wd.QtyMsk AS decimal(30,3)) as TotalKubik, " & _
                    '            " wd.WarehouseItem_Code , MW.Warehouse_Name, mq.Tujuan  , " & _
                    '            "(SELECT CASE     " & _
                    '            "When wd.Container='true' then   " & _
                    '            "	(    " & _
                    '            "	    '20'   " & _
                    '            "	)     " & _
                    '            "else   " & _
                    '            "	(    " & _
                    '            "	(SELECT case   " & _
                    '            "		when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then    " & _
                    '            "			(    " & _
                    '            "				(wd.QtyMsk * wd.Berat) /1000 " & _
                    '            "			)   " & _
                    '            "		when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then    " & _
                    '            "			(    " & _
                    '            "				(wd.QtyMsk * wd.Berat) /1000  " & _
                    '            "			)    " & _
                    '            "		when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then    " & _
                    '            "			(    " & _
                    '            "				(wd.QtyMsk * wd.Berat) / 1000 " & _
                    '            "			)   " & _
                    '            "		else(   " & _
                    '            "				(wd.QtyMsk * wd.Berat) / 1000 " & _
                    '            "			)   " & _
                    '            "		end)    " & _
                    '            "	)    " & _
                    '            "end) as TotalBerat, qd.Nama_Barang, wd.NamaSupplier as pengirim, mq.Penerima, wd.Time_Itemcome, MBR.Depart_Date as Tanggal,    " & _
                    '            "(SELECT CASE      " & _
                    '            "When wd.Container='true' then " & _
                    '               "( " & _
                    '                               " 'Container'     " & _
                    '               ")       " & _
                    '            "else    " & _
                    '              "(      " & _
                    '                                "'Colli'     " & _
                    '             ")      " & _
                    '            "end) as Unit " & _
                    '            "from V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh,  " & _
                    '            "MasterCustomer mc, MasterQuotation mq, MasterWarehouse MW, MuatBarang MB, Kapal K, " & _
                    '            "MuatBarangReport MBR, mbrDetail MBRD " & _
                    '            "where   " & _
                    '            "(wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)    " & _
                    '            "and (qd.Quotation_No = mq.Quotation_No and mq.Customer_Id = mc.Kode_Customer) " & _
                    '            "and qd.SatuanID = mh.ID and MW.Warehouse_Code = wd.Warehouse_Code    " & _
                    '            "and wd.WarehouseItem_Code = MB.WarehouseHeaderID and MB.Kapal = K.IDDetail " & _
                    '            "and MBRD.Mb_No = MB.Mb_No and MBRD.Mbr_No = MBR.Mbr_No " & _
                    '            "and  wd.status = 1 and qd.status <>0 and mh.status = 1 and wd.[statuspengirimandetail] <> 0    " & _
                    '            "and mq.Tujuan = '" & tujuan & "' " & _
                    '            "and wd.Warehouse_Code = '" & gdg & "' " & _
                    '            "and wd.Time_Itemcome between '" & startdate & "' AND '" & enddate & "' " & _
                    '            "and MBR.status <> 0 and MBRD.status <> 0  " & _
                    '            "and MBR.Depart_Date >= dateadd(MONTH,-1,GETDATE()) " & _
                    '            "GROUP BY mh.NamaHarga,wd.Container, qd.Nama_Barang,qd.harga,wd.Paid, qd.Harga,   " & _
                    '            "wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat,  qd.Nama_Barang,  " & _
                    '            "wd.NamaSupplier, mq.Penerima,wd.berat, wd.WarehouseItem_Code, MW.Warehouse_Name, mq.Tujuan, " & _
                    '            "wd.Time_Itemcome, MBR.Depart_Date, k.Nama_Kapal, wd.QtyMsk ) as A   " & _
                    '            "GROUP BY A.namaquo, A.Harga, " & _
                    '            "A.JenisTarif,A.Nama_Barang, A.pengirim, A.Penerima,A.WarehouseItem_Code, " & _
                    '            "A.Tujuan, A.Warehouse_Name, A.Nama_Kapal, A.Time_Itemcome, A.Tanggal, A.Unit  " & _
                    '            "UNION ALL " & _
                    '            "select A.namaquo, A.Harga,  " & _
                    '            "A.JenisTarif As JenisPembayaran, SUM(A.TotalQty) as TotalQty,  " & _
                    '            "SUM(A.TotalBerat) as TotalBerat  , SUM(A.TotalKubik) as TotalKubik , " & _
                    '            "A.Nama_Barang, A.pengirim, A.Penerima, A.WarehouseItem_Code, " & _
                    '            "A.Tujuan, A.Warehouse_Name, A.Nama_Kapal, A.Time_Itemcome, A.Tanggal, A.Unit   " & _
                    '            "FROM   " & _
                    '            "(select wd.Container,qd.Nama_Barang as namaquo, '-' as Nama_Kapal,  " & _
                    '            " mh.NamaHarga as JenisTarif, qd.Harga, wd.QtyMsk as TotalQty,  " & _
                    '            " cast(wd.Panjang * wd.Lebar * wd.Tinggi * wd.QtyMsk AS decimal(30,3)) as TotalKubik, " & _
                    '            " wd.WarehouseItem_Code , MW.Warehouse_Name, mq.Tujuan  , " & _
                    '            "(SELECT CASE     " & _
                    '            "When wd.Container='true' then   " & _
                    '            "	(    " & _
                    '            "	    '20'   " & _
                    '            "	)     " & _
                    '            "else   " & _
                    '            "	(    " & _
                    '            "	(SELECT case   " & _
                    '            "		when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then    " & _
                    '            "			(    " & _
                    '            "				(wd.QtyMsk * wd.Berat) /1000 " & _
                    '            "			)   " & _
                    '            "		when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then    " & _
                    '            "			(    " & _
                    '            "				(wd.QtyMsk * wd.Berat) /1000  " & _
                    '            "			)    " & _
                    '            "		when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then    " & _
                    '            "			(    " & _
                    '            "				(wd.QtyMsk * wd.Berat) / 1000 " & _
                    '            "			)   " & _
                    '            "		else(   " & _
                    '            "				(wd.QtyMsk * wd.Berat) / 1000 " & _
                    '            "			)   " & _
                    '            "		end)    " & _
                    '            "	)    " & _
                    '            "end) as TotalBerat, qd.Nama_Barang, wd.NamaSupplier as pengirim, mq.Penerima, wd.Time_Itemcome, '-' as Tanggal,    " & _
                    '            "(SELECT CASE      " & _
                    '            "When wd.Container='true' then " & _
                    '               "( " & _
                    '                               " 'Container'     " & _
                    '               ")       " & _
                    '            "else    " & _
                    '              "(      " & _
                    '                                "'Colli'     " & _
                    '             ")      " & _
                    '            "end) as Unit " & _
                    '            "from V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh,  " & _
                    '            "MasterCustomer mc, MasterQuotation mq, MasterWarehouse MW " & _
                    '            "where   " & _
                    '            "(wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)    " & _
                    '            "and (qd.Quotation_No = mq.Quotation_No and mq.Customer_Id = mc.Kode_Customer) " & _
                    '            "and qd.SatuanID = mh.ID and MW.Warehouse_Code = wd.Warehouse_Code    " & _
                    '            "and  wd.status = 1 and qd.status <>0 and mh.status = 1   " & _
                    '            "and wd.[statuspengirimandetail] = 0 " & _
                    '            "and mq.Tujuan = '" & tujuan & "' " & _
                    '            "and wd.Warehouse_Code = '" & gdg & "' " & _
                    '            "and wd.Time_Itemcome between '" & startdate & "' AND '" & enddate & "' " & _
                    '            "GROUP BY mh.NamaHarga,wd.Container, qd.Nama_Barang,qd.harga,wd.Paid, qd.Harga,   " & _
                    '            "wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat,  qd.Nama_Barang,  " & _
                    '            "wd.NamaSupplier, mq.Penerima,wd.berat, wd.WarehouseItem_Code, MW.Warehouse_Name, mq.Tujuan, " & _
                    '            "wd.Time_Itemcome, wd.QtyMsk ) as A   " & _
                    '            "GROUP BY A.namaquo, A.Harga, " & _
                    '            "A.JenisTarif,A.Nama_Barang, A.pengirim, A.Penerima,A.WarehouseItem_Code, " & _
                    '            "A.Tujuan, A.Warehouse_Name, A.Nama_Kapal, A.Time_Itemcome, A.Tanggal, A.Unit) As C ORDER BY C.Time_Itemcome  "
                    sqlString = "select row_number() OVER(ORDER BY C.Time_Itemcome asc) AS 'RowNumber', " & _
                                "c.namaquo, c.harga, C.namaquo,C.Harga, C.JenisPembayaran, C.TotalKubik, " & _
                                "C.Nama_Barang, C.pengirim, C.Penerima, C.WarehouseItem_Code, " & _
                                "C.Tujuan, C.Warehouse_Name, C.Nama_Kapal, C.Time_Itemcome, C.Tanggal, C.Unit, " & _
                                "C.TotalBerat, C.TotalQty " & _
                                "FROM " & _
                                "(select A.namaquo, A.Harga,  " & _
                                "A.JenisTarif As JenisPembayaran, SUM(A.TotalQty) as TotalQty,  " & _
                                "SUM(A.TotalBerat) as TotalBerat  , SUM(A.TotalKubik) as TotalKubik , " & _
                                "A.Nama_Barang, A.pengirim, A.Penerima, A.WarehouseItem_Code, " & _
                                "A.Tujuan, A.Warehouse_Name, A.Nama_Kapal, A.Time_Itemcome, COnvert(varchar,A.Tanggal ,106) as Tanggal, A.Unit   " & _
                                "FROM   " & _
                                "( select wd.Container,qd.Nama_Barang as namaquo, k.Nama_Kapal, mh.NamaHarga as JenisTarif, qd.Harga, wd.QtyMsk as TotalQty " & _
                                ", cast(wd.Panjang * wd.Lebar * wd.Tinggi * wd.QtyMsk AS decimal(30,3)) as TotalKubik, wd.WarehouseItem_Code , MW.Warehouse_Name, mq.Tujuan  " & _
                                ", (SELECT CASE When wd.Container='true'  " & _
                                "   then ( '20' )  " & _
                                "	else (  " & _
                                "		(SELECT case  " & _
                                "		    when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then ( (wd.QtyMsk * wd.Berat) /1000 )  " & _
                                "		    when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then ( (wd.QtyMsk * wd.Berat) /1000 )  " & _
                                "		    when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then ( (wd.QtyMsk * wd.Berat) / 1000 ) else( (wd.QtyMsk * wd.Berat) / 1000 )  " & _
                                "	    end)  " & _
                                "   )  " & _
                                "   end) as TotalBerat, qd.Nama_Barang, wd.NamaSupplier as pengirim, mq.Penerima, wd.Time_Itemcome, MBR.Depart_Date as Tanggal " & _
                                ",(SELECT CASE When wd.Container='true' then ( 'Container' ) else ( 'Colli' ) end) as Unit  " & _
                                "from V_Warehouse_Satuan WD  , MuatBarang MB , MBRDetail MBRD , MuatBarangDetail MBD , MuatBarangReport MBR , Kapal K " & _
                                "	,QuotationDetail QD ,MasterHargaDefault MH, MasterCustomer MC, MasterQuotation MQ, MasterWarehouse MW " & _
                                "WHERE WD.Warehouse_Code = '" & gdg & "' and WD.Time_Itemcome between '" & startdate & "' AND '" & enddate & "' " & _
                                "AND MQ.Tujuan = '" & tujuan & "'  " & _
                                "AND WD.status = 1  " & _
                                "AND WD.[statuspengirimandetail] <> 0  " & _
                                "AND	MB.status <> '0' " & _
                                "AND	MB.WarehouseHeaderID = WD.WarehouseItem_Code " & _
                                "and MBRD.status <> 0 " & _
                                "AND	MBRD.Mb_No = MB.Mb_No " & _
                                "AND MBD.status <> '0' " & _
                                "AND MBRD.Mb_No = MBD.Mb_No " & _
                                "AND MBRD.Mbd_ID = MBD.IDDetail " & _
                                "AND WD.IDDetail = MBD.Warehouse_Id " & _
                                "and MBR.status <> 0  " & _
                                "AND MBR.Mbr_No = MBRD.Mbr_No " & _
                                "AND	K.status <> 0	 " & _
                                "AND	K.IDDetail = MBR.Kapal " & _
                                "AND QD.status <>0 and MH.status = 1  " & _
                                "and (wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)  " & _
                                "and (qd.Quotation_No = mq.Quotation_No and mq.Customer_Id = mc.Kode_Customer) " & _
                                "and qd.SatuanID = mh.ID and MW.Warehouse_Code = wd.Warehouse_Code  ) as A   " & _
                                "GROUP BY A.namaquo, A.Harga, " & _
                                "A.JenisTarif,A.Nama_Barang, A.pengirim, A.Penerima,A.WarehouseItem_Code, " & _
                                "A.Tujuan, A.Warehouse_Name, A.Nama_Kapal, A.Time_Itemcome, A.Tanggal, A.Unit  " & _
                                "UNION ALL " & _
                                "select A.namaquo, A.Harga,  " & _
                                "A.JenisTarif As JenisPembayaran, SUM(A.TotalQty) as TotalQty,  " & _
                                "SUM(A.TotalBerat) as TotalBerat  , SUM(A.TotalKubik) as TotalKubik , " & _
                                "A.Nama_Barang, A.pengirim, A.Penerima, A.WarehouseItem_Code, " & _
                                "A.Tujuan, A.Warehouse_Name, A.Nama_Kapal, A.Time_Itemcome, A.Tanggal, A.Unit   " & _
                                "FROM   " & _
                                "(select wd.Container,qd.Nama_Barang as namaquo, '-' as Nama_Kapal,  " & _
                                " mh.NamaHarga as JenisTarif, qd.Harga, wd.QtyMsk as TotalQty,  " & _
                                " cast(wd.Panjang * wd.Lebar * wd.Tinggi * wd.QtyMsk AS decimal(30,3)) as TotalKubik, " & _
                                " wd.WarehouseItem_Code , MW.Warehouse_Name, mq.Tujuan  , " & _
                                "(SELECT CASE     " & _
                                "When wd.Container='true' then   " & _
                                "	(    " & _
                                "	    '20'   " & _
                                "	)     " & _
                                "else   " & _
                                "	(    " & _
                                "	(SELECT case   " & _
                                "		when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then    " & _
                                "			(    " & _
                                "				(wd.QtyMsk * wd.Berat) /1000 " & _
                                "			)   " & _
                                "		when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then    " & _
                                "			(    " & _
                                "				(wd.QtyMsk * wd.Berat) /1000  " & _
                                "			)    " & _
                                "		when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then    " & _
                                "			(    " & _
                                "				(wd.QtyMsk * wd.Berat) / 1000 " & _
                                "			)   " & _
                                "		else(   " & _
                                "				(wd.QtyMsk * wd.Berat) / 1000 " & _
                                "			)   " & _
                                "		end)    " & _
                                "	)    " & _
                                "end) as TotalBerat, qd.Nama_Barang, wd.NamaSupplier as pengirim, mq.Penerima, wd.Time_Itemcome, '-' as Tanggal,    " & _
                                "(SELECT CASE      " & _
                                "When wd.Container='true' then " & _
                                   "( " & _
                                                   " 'Container'     " & _
                                   ")       " & _
                                "else    " & _
                                  "(      " & _
                                                    "'Colli'     " & _
                                 ")      " & _
                                "end) as Unit " & _
                                "from V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh,  " & _
                                "MasterCustomer mc, MasterQuotation mq, MasterWarehouse MW " & _
                                "where   " & _
                                "(wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)    " & _
                                "and (qd.Quotation_No = mq.Quotation_No and mq.Customer_Id = mc.Kode_Customer) " & _
                                "and qd.SatuanID = mh.ID and MW.Warehouse_Code = wd.Warehouse_Code    " & _
                                "and  wd.status = 1 and qd.status <>0 and mh.status = 1   " & _
                                "and wd.[statuspengirimandetail] = 0 " & _
                                "and mq.Tujuan = '" & tujuan & "' " & _
                                "and wd.Warehouse_Code = '" & gdg & "' " & _
                                "and wd.Time_Itemcome between '" & startdate & "' AND '" & enddate & "' " & _
                                "GROUP BY mh.NamaHarga,wd.Container, qd.Nama_Barang,qd.harga,wd.Paid, qd.Harga,   " & _
                                "wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat,  qd.Nama_Barang,  " & _
                                "wd.NamaSupplier, mq.Penerima,wd.berat, wd.WarehouseItem_Code, MW.Warehouse_Name, mq.Tujuan, " & _
                                "wd.Time_Itemcome, wd.QtyMsk ) as A   " & _
                                "GROUP BY A.namaquo, A.Harga, " & _
                                "A.JenisTarif,A.Nama_Barang, A.pengirim, A.Penerima,A.WarehouseItem_Code, " & _
                                "A.Tujuan, A.Warehouse_Name, A.Nama_Kapal, A.Time_Itemcome, A.Tanggal, A.Unit) As C ORDER BY C.Time_Itemcome  "

                    '***** EH00_20111014_01 END   *****

                    'Throw New Exception(sqlString)

                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT

                Case "rptNotBalance"
                    startdate = Request("transdatefrom")
                    enddate = Request("transdateto")

                    sqlString = "select RefNo,TransDate, SUM(debit) as Debit, SUM(Credit)as Credit , SUM(Debit) - SUM(Credit) as Selisih  " & _
                                "FROM V_JournalOther " & _
                                "WHERE TransDate BETWEEN '" & startdate & "' AND '" & enddate & "' " & _
                                "GROup BY RefNo, TransDate " & _
                                "Having(SUM(Debit) - SUM(Credit) <> 0) " & _
                                "ORDER BY TransDate"

                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT


                Case "rptAR"
                    startdate = Request("transdatefrom")
                    enddate = Request("transdateto")

                    If TipeKapal = "Semua" Then
                        If Tipe = "semua" Then
                            sqlString = "SELECT JARH.RefNo, JARD.AccountNo, JARD.Type, JARD.Description, JARD.Amount FROM JournalARHeader JARH" & _
                                        " INNER JOIN JournalARDetail JARD ON JARH.RunNo=JARD.RunNoHeader" & _
                                        " WHERE TransDate between '" & startdate & "' and '" & enddate & "' ORDER BY RefNo,IDDetail"
                        Else
                            sqlString = "SELECT JARH.RefNo, JARD.AccountNo, JARD.Type, JARD.Description, JARD.Amount FROM JournalARHeader JARH" & _
                                        " INNER JOIN JournalARDetail JARD ON JARH.RunNo=JARD.RunNoHeader" & _
                                        " WHERE SUBSTRING(JARH.REFNO,1,1)<>'B' and TransDate between '" & startdate & "' and '" & enddate & "' ORDER BY RefNo,IDDetail"
                        End If
                    Else
                        If Tipe = "semua" Then
                            sqlString = "SELECT JARH.RefNo, JARD.AccountNo, JARD.Type, JARD.Description, JARD.Amount FROM JournalARHeader JARH" & _
                                        " INNER JOIN JournalARDetail JARD ON JARH.RunNo=JARD.RunNoHeader" & _
                                        " WHERE TransDate between '" & startdate & "' and '" & enddate & "' and (SUBSTRING(JARH.REFNO,10,3)='" & TipeKapal.ToString & "' OR SUBSTRING(JARH.REFNO,11,3)='" & TipeKapal.ToString & "') ORDER BY RefNo,IDDetail"
                        Else
                            sqlString = "SELECT JARH.RefNo, JARD.AccountNo, JARD.Type, JARD.Description, JARD.Amount FROM JournalARHeader JARH" & _
                                        " INNER JOIN JournalARDetail JARD ON JARH.RunNo=JARD.RunNoHeader" & _
                                        " WHERE TransDate between '" & startdate & "' and '" & enddate & "' and SUBSTRING(JARH.REFNO,1,1)<>'B' AND (SUBSTRING(JARH.REFNO,10,3)='" & TipeKapal.ToString & "' OR SUBSTRING(JARH.REFNO,11,3)='" & TipeKapal.ToString & "') ORDER BY RefNo,IDDetail"
                        End If
                    End If
                    'Throw New Exception(sqlString.ToString)
                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT

                Case "LapPembayaran"
                    startdate = Request("transdatefrom")
                    enddate = Request("transdateto")

                    If Tipe = "semua" Then
                        sqlString = "select Ke," & _
                                    "TransDate,RefNo,accountno,journalsalesdetail.Description,case when Type='Debet' then cast(amount as float) " & _
                                    "else 0 end Debet ,case when Type='Kredit' then cast(amount as float) else 0 end Kredit  " & _
                                    ",type from ( " & _
                                    "select " & _
                                    "row_number() over(partition by RefNo order by RunNo) Ke," & _
                                    "* " & _
                                    "from JournalSalesHeader " & _
                                    "where JournalSalesHeader.Mode ='sales' " & _
                                    "and JournalSalesHeader.status = 1" & _
                                    ")JournalSalesHeader " & _
                                    "inner join JournalSalesDetail " & _
                                    "on JournalSalesDetail.RunNoHeader = JournalSalesHeader.RunNo " & _
                                    "and JournalSalesHeader.status = 1 " & _
                                    "order by RunNo,RefNo,case when Type='Debet' then cast(amount as float) else 0 end desc"
                    Else
                        sqlString = "select Ke," & _
                                    "TransDate,RefNo,accountno,journalsalesdetail.Description,case when Type='Debet' then cast(amount as float) " & _
                                    "else 0 end Debet ,case when Type='Kredit' then cast(amount as float) else 0 end Kredit  " & _
                                    ",type from ( " & _
                                    "select " & _
                                    "row_number() over(partition by RefNo order by RunNo) Ke," & _
                                    "* " & _
                                    "from JournalSalesHeader " & _
                                    "where JournalSalesHeader.Mode ='sales' " & _
                                    "and JournalSalesHeader.status = 1 and Left(JournalSalesHeader.RefNo,1) <>'B'" & _
                                    ")JournalSalesHeader " & _
                                    "inner join JournalSalesDetail " & _
                                    "on JournalSalesDetail.RunNoHeader = JournalSalesHeader.RunNo " & _
                                    "and JournalSalesHeader.status = 1 " & _
                                    "order by RunNo,RefNo,case when Type='Debet' then cast(amount as float) else 0 end desc"
                    End If

                    RDT = SQLExecuteQuery(sqlString).Tables(0)
                    Return RDT

            End Select


        Catch ex As Exception
            Throw New Exception("Error function setqueryreport :" & ex.ToString)
        End Try
        Return RDT
    End Function


    Private Sub setRecordSelectionFormula()

        Try

            If Not IsNothing(Request("transdatefrom")) Then
                If Request("transdatefrom") <> "" Then
                    report.SetParameterValue("transdatefrom", Request("transdatefrom") & " 00:00:00")
                Else
                    report.SetParameterValue("transdatefrom", "*")
                End If
            End If

            If Not IsNothing(Request("transdateto")) Then
                If Request("transdateto") <> "" Then
                    report.SetParameterValue("transdateto", Request("transdateto") & " 23:59:59")
                Else
                    report.SetParameterValue("transdateto", "*")
                End If
            End If

            If Not IsNothing(Request("accountnostart")) Then
                If Request("accountnostart") <> "" Then
                    report.SetParameterValue("accountnostart", Request("accountnostart"))
                Else
                    report.SetParameterValue("accountnostart", "%")
                End If
            End If

            If Not IsNothing(Request("accountnoend")) Then
                If Request("accountnoend") <> "" Then
                    report.SetParameterValue("accountnoend", Request("accountnoend"))
                Else
                    report.SetParameterValue("accountnoend", "%")
                End If
            End If

            If Not IsNothing(Request("str")) Then
                If Request("str") <> "" Then
                    report.SetParameterValue("str", Request("str"))
                Else
                    report.SetParameterValue("str", "*")
                End If
            End If

        Catch ex As Exception
            Response.Write("setRecordSelectionFormula Exception :<br>" & ex.ToString)
        End Try

    End Sub

#End Region
End Class