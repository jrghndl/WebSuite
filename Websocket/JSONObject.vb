Public Class JSONObject
    Dim JSON As Newtonsoft.Json.Linq.JObject

    'Creates a JSONObject from a JSON string.
    Public Sub New(ByVal JSON As String)
        Me.JSON = Newtonsoft.Json.Linq.JObject.Parse(JSON)
    End Sub

    'Gets an element from the JSONObject using Path or JSONPath.
    Public Function GetElement(ByVal Path As String)
        Return New JSONElement(JSON.SelectToken(Path))
    End Function

    'Gets elements from the JSONObject using Path or JSONPath.
    Public Function GetElements(ByVal Path As String)
        Dim elementList As New List(Of JSONElement)
        Dim elements As IEnumerable(Of Newtonsoft.Json.Linq.JToken) = JSON.SelectTokens(Path)
        For Each element As Newtonsoft.Json.Linq.JToken In elements
            elementList.Add(New JSONElement(element))
        Next
        Return elementList
    End Function

    'Returns the JSON string.
    Public Overrides Function ToString() As String
        Return JSON.ToString
    End Function
End Class
