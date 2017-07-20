Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Partial Public Class ReportItem
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DS As DataSet
    Private report As New ReportDocument
    Private reportPath As String
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim result As String
    Dim hasil As Integer

#Region "PAGE"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            If Not Session("ReportItem") Is Nothing Then 'utk next agar tidak postback
                Session("ReportItem") = Nothing
            End If

        Catch ex As Exception
            Response.Write("Page_Init Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            crv1.HasCrystalLogo = False
            crv1.HasToggleGroupTreeButton = False
            crv1.HasViewList = False
            crv1.HasZoomFactorList = True
            crv1.HasSearchButton = True
            crv1.DisplayGroupTree = False

            If Not Session("ReportItem") Is Nothing Then
                crv1.ReportSource = CType(Session("ReportItem"), ReportDocument)
            End If
        End If

    End Sub
#End Region

#Region "BUTTON"
    Protected Sub btView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btView.Click

        Try
            If validation() Then
                LoadReport()
            End If
        Catch ex As Exception
            Throw New Exception("Error Button view click : " & ex.ToString)
        End Try
    End Sub
#End Region
#Region "VALIDATION"
    Private Function validation() As Boolean
        Try
            If hfCID.Value = "" Then
                lblError.Visible = True
                lblError.Text = " Customer is not exist , please choose customer from the list "
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("Error function validation :" & ex.ToString)
        End Try
    End Function
#End Region

#Region "REPORT"
    Private Sub LoadReport()
        Try
            sqlstring = " Select mi.Nama_Barang,mc.Nama_Customer,mi.Unit,mi.Berat,mi.Panjang,mi.Lebar,mi.Tinggi from masteritem mi,mastercustomer mc where mi.Customer_Id = mc.ID and mi.Customer_Id = '" & hfCID.Value & "' and mi.status = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            'Response.Write(sqlString)
            reportPath = Server.MapPath("ReportItemT.rpt")
            report.Load(reportPath) ' buad ngeload reportnya
            report.SetDataSource(DT) ' untuk penggabungan report dan datatable
            crv1.ReportSource = report   ' nampilin ke viewernya
            Session("ReportItem") = report

        Catch ex As Exception
            Throw New Exception("Error Function LoadReport :" & ex.ToString)
        End Try
    End Sub
   
#End Region

#Region "METHOD"
    Private Sub clear()
        lblError.Text = ""
        TxtCust.Text = ""
        hfCID.Value = ""
    End Sub
#End Region
    

End Class