Public Class XMLColumn
    Private _Name As String
    Private _AutoIncrement As Boolean = False
    Private _Unique As Boolean = False

    'Creates a new XMLColumn object.
    Public Sub New(ByVal Name As String)
        If Name.Length = 0 Then
            Throw New System.Exception("Column must have a name.")
        Else
            _Name = Name
        End If
    End Sub

    'Gets the Name property of the column.
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property

    'Gets/Sets whether the column should auto increment.
    Public Property AutoIncrement As Boolean
        Get
            Return _AutoIncrement
        End Get
        Set(value As Boolean)
            _AutoIncrement = value
        End Set
    End Property

    'Gets/Sets whether the column should only contain unique values.
    Public Property Unique As Boolean
        Get
            Return _Unique
        End Get
        Set(value As Boolean)
            _Unique = value
        End Set
    End Property
End Class
