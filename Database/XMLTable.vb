Public Class XMLTable
    Friend Table As DataTable

    'Initializes a new XMLTable object.
    Public Sub New(ByVal Name As String)
        Table = New DataTable(Name)
    End Sub

    'Initializes a XMLTable from an existing datatable.
    Public Sub New(ByRef Table As DataTable)
        Me.Table = Table
    End Sub

    'Adds a new column to the table.
    Public Sub AddColumn(ByVal Name As String)
        Table.Columns.Add(Name)
    End Sub

    'Adds a new column to the table from an existing XMLColumn object.
    Public Sub AddColumn(ByVal Column As XMLColumn)
        Dim _Column As New DataColumn
        _Column.ColumnName = Column.Name
        _Column.AutoIncrement = Column.AutoIncrement
        _Column.Unique = Column.Unique
        Table.Columns.Add(_Column)
    End Sub

    'Adds a row to the table.
    Public Sub AddRow(ByVal Record As XMLRecord)
        Table.Rows.Add(Record.Record)
    End Sub

    'Adds a row to the table from an XMLRecord
    Public Function NewRecord() As XMLRecord
        Return New XMLRecord(Me)
    End Function

    'Sets the primary key of  the table.
    Public Sub SetPrimaryKey(ParamArray columns() As String)
        Dim ColumnList As New List(Of DataColumn)
        For Each str As String In columns
            ColumnList.Add(Table.Columns(str))
        Next
        Table.PrimaryKey = ColumnList.ToArray
    End Sub

    'Gets the table's primary key.
    Public Function GetPrimaryKey() As String()
        Dim Keys() As DataColumn
        Keys = Table.PrimaryKey
        Dim PrimaryKeys As New List(Of String)
        For Each key As DataColumn In Keys
            PrimaryKeys.Add(key.ColumnName)
        Next
        Return PrimaryKeys.ToArray
    End Function

    'Gets a column from the table by name.
    Public Function Column(ByVal Name As String) As DataColumn
        Return Table.Columns(Name)
    End Function

    'Returns an array of XMLRecord that contains every record in the table.
    Public Function GetRecords() As XMLRecord()
        Dim RowList As New List(Of XMLRecord)
        For Each Row As DataRow In Table.Select()
            RowList.Add(New XMLRecord(Row))
        Next
        Return RowList.ToArray
    End Function

    'Returns an array of XMLRecord that matches a filter.
    Public Function GetRecords(ByVal Filter As String) As XMLRecord()
        Dim RowList As New List(Of XMLRecord)
        For Each Row As DataRow In Table.Select(Filter)
            RowList.Add(New XMLRecord(Row))
        Next
        Return RowList.ToArray
    End Function

    'Returns a sorted array of XMLRecord that matches a filter.
    Public Function GetRecords(ByVal Filter As String, ByVal Sort As String) As XMLRecord()
        Dim RowList As New List(Of XMLRecord)
        For Each Row As DataRow In Table.Select(Filter, Sort)
            RowList.Add(New XMLRecord(Row))
        Next
        Return RowList.ToArray
    End Function

    'Gets a specific XMLRecord from a given index.
    Public Function GetRecord(ByVal Index As Integer) As XMLRecord
        Return New XMLRecord(Table.Rows.Item(Index))
    End Function

    'Returns a value based on an expression and filter.
    Public Function Compute(ByVal Expression As String, ByVal Filter As String) As Object
        Return Table.Compute(Expression, Filter)
    End Function

    'Renames the table.
    Public Sub Rename(ByVal Name As String)
        Table.TableName = Name
    End Sub
End Class
