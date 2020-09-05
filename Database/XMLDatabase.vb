Public Class XMLDatabase
    Dim Database As DataSet

    'Creates a new empty XMLDatabase object.
    Public Sub New()
    End Sub

    'Creates a new XMLDatabase object with a name.
    Public Sub New(ByVal Name As String)
        Database = New DataSet(Name)
        Database.EnforceConstraints = True
    End Sub

    'Loads a XMLDatabase from a XML string.
    Public Sub LoadFromString(ByVal XML As String)
        Database = New DataSet()
        Dim XMLReader As System.IO.StringReader = New System.IO.StringReader(XML)
        Database.ReadXml(XMLReader, XmlReadMode.Auto)
        Database.EnforceConstraints = True
    End Sub

    'Loads a XMLDatabase from a XML file.
    Public Sub LoadFromFile(ByVal FilePath As String)
        Database = New DataSet()
        Database.ReadXml(FilePath, XmlReadMode.Auto)
        Database.EnforceConstraints = True
    End Sub

    'Adds an XMLTable to the XMLDatabase.
    Public Sub AddTable(ByVal Table As XMLTable)
        Database.Tables.Add(Table.Table)
    End Sub

    'Gets an XMLTable from the XMLDatabase by name.
    Public Function Table(ByVal Name As String) As XMLTable
        Return New XMLTable(Database.Tables(Name))
    End Function

    'Gets the XMLDatabase's XML
    Public Function GetXML()
        Return Database.GetXml
    End Function

    'Renames the XMLDatabase
    Public Sub Rename(ByVal Name As String)
        Database.DataSetName = Name
    End Sub

    'Clears the XMLDatabase
    Public Sub Clear()
        Database.Clear()
    End Sub

    'Returns the names of all of the XMLTables in the XMLDatabase as an array of string.
    Public Function GetTables()
        Dim collection As DataTableCollection = Database.Tables
        Dim TableList As New List(Of String)
        For i As Integer = 0 To collection.Count - 1
            TableList.Add(collection(i).TableName)
        Next
        Return TableList.ToArray
    End Function

    'Saves the XMLDatabase's XML to an XML File at the given location.
    Public Sub SaveDatabase(ByVal FilePath As String)
        Database.WriteXml(FilePath, XmlWriteMode.WriteSchema)
    End Sub

    'Creates a relation between tables.
    Public Sub CreateRelation(ByVal RelationName As String, ByVal ParentColumn As DataColumn, ByVal ChildColumn As DataColumn)
        Database.Relations.Add(RelationName, ParentColumn, ChildColumn)
    End Sub

End Class
