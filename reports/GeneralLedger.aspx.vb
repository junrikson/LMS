Imports System.Data.SqlClient
Partial Public Class GeneralLedger
    Inherits System.Web.UI.Page
    Private sqlString As String
    Private sDT As DataTable
    Private sDR As SqlDataReader
    Private con As SqlConnection
#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                tbStartDate.Date = Today
                tbEndDate.Date = Today
                LoadGrid()

            End If

            If Not Session("GridAccountGL") Is Nothing Then
                grid_Account.DataSource = CType(Session("GridAccountGL"), DataTable)
                grid_Account.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try
    End Sub
#End Region


#Region "DDL"
    'Private Sub Load_DDL()
    '    Try

    '        sqlString = "	SELECT " & _
    '              "		Code, " & _
    '              "		(Code + ' - ' + Name) AS AccountName " & _
    '              "	FROM ChartOfAccount " & _
    '              "	WHERE Levels > 0 and status =1 " & _
    '              "	ORDER BY Code "
    '        sDT = SQLExecuteQuery(sqlString).Tables(0)
    '        With DDLCOAFrom
    '            .DataSource = sDT
    '            .DataValueField = "Code"
    '            .DataTextField = "AccountName"
    '            .DataBind()
    '            '.Items.Insert(0, "- All -")
    '            '.Items.Item(0).Value = "%"
    '        End With
    '        sqlString = "	SELECT " & _
    '              "		Code, " & _
    '              "		(Code + ' - ' + Name) AS AccountName " & _
    '              "	FROM ChartOfAccount " & _
    '              "	WHERE Levels > 0 and status =1 " & _
    '              "	ORDER BY Code "
    '        sDT = SQLExecuteQuery(sqlString).Tables(0)
    '        With DDLCOATo
    '            .DataSource = sDT
    '            .DataValueField = "Code"
    '            .DataTextField = "AccountName"
    '            .DataBind()
    '            '.Items.Insert(0, "- All -")
    '            '.Items.Item(0).Value = "%"
    '        End With

    '    Catch ex As Exception
    '        Response.Write("Load_DDL Exception :<br>" & ex.ToString)
    '    End Try
    'End Sub

#End Region

    Private Sub LoadGrid()
        Try
            Select Case Session("namaroles")
                Case "Accounting Pangkal Pinang"
                    sqlString = "SELECT ID, [Types], Code, Name, Levels " & _
                                "FROM ChartOfAccount  WHERE Parent <> 'TOP' and " & _
                                "status = 1 and Levels <> 1 and (LEFT(Parent, 8) = '0001.111' " & _
                                "OR LEFT(Parent, 8) = '0001.112' OR LEFT(Parent, 8) = '0001.113' " & _
                                "OR LEFT(Parent, 8) = '0001.114') and Lokasi = '2' ORDER BY Code"
                Case "Accounting Tanjung Pandan"
                    sqlString = "SELECT ID, [Types], Code, Name, Levels " & _
                                "FROM ChartOfAccount  WHERE Parent <> 'TOP' and " & _
                                "status = 1 and Levels <> 1 and (LEFT(Parent, 8) = '0001.111' " & _
                                "OR LEFT(Parent, 8) = '0001.112' OR LEFT(Parent, 8) = '0001.113' " & _
                                "OR LEFT(Parent, 8) = '0001.114') and Lokasi = '3' ORDER BY Code"
                Case Else
                    sqlString = "SELECT ID, [Types], Code, Name, Levels FROM ChartOfAccount WHERE Parent <> 'TOP' and status = 1 and Levels <> 1 ORDER BY Code"
            End Select

            sDT = SQLExecuteQuery(sqlString).Tables(0)

            Session("GridAccountGL") = sDT
            grid_Account.DataSource = sDT
            grid_Account.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
End Class