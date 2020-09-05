Public Class JSONBuilder
    Private JSONDicitonary As New Dictionary(Of String, Object)

    'Gets the JSON string of the JSONBuilder object.
    Public ReadOnly Property Data
        Get
            Return Newtonsoft.Json.JsonConvert.SerializeObject(JSONDicitonary, Newtonsoft.Json.Formatting.Indented)
        End Get
    End Property

    'Adds an element with its associated key and value.
    Public Sub Add(ByVal Key As String, Value As Object)
        JSONDicitonary.Add(Key, Value)
    End Sub

    'Removes an element by key.
    Public Sub Remove(ByVal Key As String)
        JSONDicitonary.Remove(Key)
    End Sub

    'Clears all of the existing JSON from the JSONBuilder object.
    Public Sub ClearAll()
        JSONDicitonary.Clear()
    End Sub

    'Gets the JSON string of the JSONBuilder object. 
    Public Overrides Function ToString() As String
        Return Newtonsoft.Json.JsonConvert.SerializeObject(JSONDicitonary, Newtonsoft.Json.Formatting.Indented)
    End Function
End Class
