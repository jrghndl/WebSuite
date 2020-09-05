Public Class XMLRecord
    Friend Record As DataRow
    Private Table As XMLTable

    'Initializes an XMLRecord bassed on the table its for.
    Friend Sub New(ByRef Table As XMLTable)
        Record = Table.Table.NewRow
        Me.Table = Table
    End Sub

    'Initializes a new empty XMLRecord.
    Friend Sub New(ByRef Row As DataRow)
        Record = Row
    End Sub

    'Sets a record's value in a specific column.
    Public Sub SetValue(ByVal ColumnName As String, ByVal Value As Object)
        Record(ColumnName) = Value
    End Sub

    'Inserts the record into the table.
    Public Sub Insert()
        Table.AddRow(Me)
    End Sub

    'Gets a specific value from a record.
    Public Function GetValue(ByVal ColumnName As String)
        Return Record(ColumnName)
    End Function

    'Gets the parents record associated with the current record based on an existing relation.
    Public Function GetParentRecord(ByVal Relation As String) As XMLRecord
        Return New XMLRecord(Record.GetParentRow(Relation))
    End Function

    'Gets the child records of the current record based on an existing relation.
    Public Function GetChildRecords(ByVal Relation As String) As XMLRecord()
        Dim RowList As New List(Of XMLRecord)
        For Each Row As DataRow In Record.GetChildRows(Relation)
            RowList.Add(New XMLRecord(Row))
        Next
        Return RowList.ToArray
    End Function

End Class
