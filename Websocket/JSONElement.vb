Public Class JSONElement
    Dim Element As Newtonsoft.Json.Linq.JToken

    'Creates a new JSONElement object
    Public Sub New(ByVal Element As Newtonsoft.Json.Linq.JToken)
        Me.Element = Element
    End Sub

    'Determines whether the JSONElement contains JSON
    Public Function IsJSON()
        Try
            Newtonsoft.Json.Linq.JToken.Parse("")
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    'Returns JSONElement as a string.
    Public Overrides Function ToString() As String
        Return Element.ToString
    End Function
End Class
