Imports System.Net
Imports System.ComponentModel
Imports System.Reflection.Emit
Imports System.Reflection
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Web.Services


Public Class LookUp
    Inherits UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtLookUpDescription.Attributes.Add("readonly", "readonly")
    End Sub
    Private mParamCollection As ParameterCollection = New ParameterCollection
    <MergableProperty(False)> _
    <PersistenceMode(PersistenceMode.InnerProperty)> _
    Public Property LookupParameter As ParameterCollection
        Get
            Return mParamCollection
        End Get
        Set(value As ParameterCollection)
            mParamCollection = value
        End Set
    End Property
    Private Function FindControlRecursive(
    ByVal rootControl As Control, ByVal controlID As String) As Control

        If rootControl.ID = controlID Then
            Return rootControl
        End If

        For Each controlToSearch As Control In rootControl.Controls
            Dim controlToReturn As Control =
                FindControlRecursive(controlToSearch, controlID)
            If controlToReturn IsNot Nothing Then
                Return controlToReturn
            End If
        Next
        Return Nothing
    End Function

    Private Function GetFilters() As String
        Dim mFilterString As String = String.Empty
        For Each mLookupParameter As Parameter In Me.LookupParameter
            Dim mControlParamter As ControlParameter = mLookupParameter
            Dim mFilterControl As TextBox = FindControlRecursive(Me.Parent, mControlParamter.ControlID)
            If mFilterControl.Text.Trim.Length <> 0 Then
                If mFilterString.Equals(String.Empty) Then mFilterString = " Where "
                If mControlParamter.Type = TypeCode.String Then
                    mFilterString = mFilterString & mControlParamter.Name & " = " & mFilterControl.Text
                End If
            End If
        Next
        Return mFilterString
    End Function

    Public Function GetLookUpDetails() As DataTable
        Dim mDataTable As DataTable = New DataTable
        Using mEkycContext As LookupContext = New LookupContext
            Using mDataAdapter As DbDataAdapter = New SqlDataAdapter
                mDataAdapter.SelectCommand = mEkycContext.Database.Connection.CreateCommand
                mDataAdapter.SelectCommand.CommandText = "Select " & Me.KeyColumnName & ", " & Me.DescriptionColumnName & " From " & Me.RecordTypeName
                mDataTable = New DataTable
                mDataAdapter.Fill(mDataTable)
            End Using
        End Using
        Return mDataTable
    End Function

    Public Property AutoSearch As Boolean
        Get
            Return CBool(ViewState("AutoSearch"))
        End Get
        Set(value As Boolean)
            ViewState("AutoSearch") = value
        End Set
    End Property

    Private Sub FillLookUpDetails()
        Dim LookUpDetails As DataTable = GetLookUpDetails()
        Dim mLookupTableRows As Integer = LookUpDetails.Rows.Count
        Dim DescCols As String() = Me.DescriptionColumnName.Split(",")
        Dim mLookupTableColumns As Integer = DescCols.Length + 1

        Dim mLookupHeaderRow As TableHeaderRow = New TableHeaderRow
        Dim mLookupHeaderColumn As TableHeaderCell = New TableHeaderCell
        mLookupHeaderColumn.Text = Me.KeyColumnName
        mLookupHeaderRow.Cells.Add(mLookupHeaderColumn)
        For Each mColName As String In DescCols
            mLookupHeaderColumn = New TableHeaderCell
            mLookupHeaderColumn.Text = mColName
            mLookupHeaderRow.Cells.Add(mLookupHeaderColumn)
        Next
        LookUpData.Rows.Add(mLookupHeaderRow)
        For Each mDataRow As DataRow In LookUpDetails.Rows
            Dim mTableRow As TableRow = New TableRow()
            Dim mTableCell As TableCell = New TableCell()
            'To Find Key Column
            For mTableColumn As Integer = 0 To LookUpDetails.Columns.Count - 1
                mTableCell = New TableCell()
                mTableCell.Text = mDataRow(mTableColumn)
                mTableRow.Cells.Add(mTableCell)
            Next
            LookUpData.Rows.Add(mTableRow)
        Next
    End Sub

    Public Property Text As String
        Get
            Return Me.txtLookUpKey.Text
        End Get
        Set(value As String)
            ViewState("Text") = value
            Me.txtLookUpKey.Text = value
        End Set
    End Property

    Public Property DescriptionControlId As String
        Get
            Return CStr(ViewState("DescriptionControlId"))
        End Get
        Set(value As String)
            ViewState("DescriptionControlId") = value
        End Set
    End Property


    Public Function GetDescriptionControl() As Control
        If Not Me.DescriptionControlId Is Nothing Then
            Return FindControlRecursive(Me.Parent, Me.DescriptionControlId)
        Else
            Return Me.txtLookUpDescription
        End If
    End Function



    Public Property Width As Unit
        Get
            Return CType(ViewState("Width"), Unit)
        End Get
        Set(value As Unit)
            ViewState("Width") = value
        End Set
    End Property

    Public Property DescriptionWidth As Unit
        Get
            Return CType(ViewState("DescriptionWidth"), Unit)
        End Get
        Set(value As Unit)
            ViewState("DescriptionWidth") = value
        End Set
    End Property

    Public Property PageSize As Integer
        Get
            If CInt(ViewState("PageSize")) = 0 Then
                Return 10
            Else
                Return CInt(ViewState("PageSize"))
            End If
        End Get
        Set(value As Integer)
            ViewState("PageSize") = value
        End Set
    End Property

    Public Property RecordTypeName As String
        Get
            Return CStr(ViewState("RecordTypeName"))
        End Get
        Set(value As String)
            ViewState("RecordTypeName") = value
        End Set
    End Property

    Public Property DescriptionColumnName As String
        Get
            Return CStr(ViewState("DescriptionColumnName"))
        End Get
        Set(value As String)
            ViewState("DescriptionColumnName") = value
        End Set
    End Property

    Public Property KeyColumnName As String
        Get
            Return CStr(ViewState("KeyColumnName"))
        End Get
        Set(value As String)
            ViewState("KeyColumnName") = value
        End Set
    End Property

    Public Property ShowDescription As Boolean
        Get
            Return CBool(ViewState("ShowDescription"))
        End Get
        Set(value As Boolean)
            ViewState("ShowDescription") = value
        End Set
    End Property

    Public Property PopUpTitle As String
        Get
            Return CStr(ViewState("PopUpTitle"))
        End Get
        Set(value As String)
            ViewState("PopUpTitle") = value
        End Set
    End Property

    Protected Overrides Sub OnInit(e As EventArgs)
        Me.AutoSearch = True
        FillLookUpDetails()
        MyBase.OnInit(e)
    End Sub

    Protected Overrides Sub OnPreRender(e As EventArgs)
        If Not Me.Width.IsEmpty Then Me.txtLookUpKey.Width = Width
        If Not Me.DescriptionWidth.IsEmpty Then Me.txtLookUpDescription.Width = DescriptionWidth
        Me.txtLookUpDescription.Visible = ShowDescription
        If Not Me.DescriptionControlId Is Nothing Then
            Me.txtLookUpDescription.Visible = False
        End If
        MyBase.OnPreRender(e)
    End Sub

    Protected Overrides Sub Render(writer As HtmlTextWriter)
        MyBase.Render(writer)
    End Sub

End Class