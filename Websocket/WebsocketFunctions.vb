Public Class WebsocketFunctions
    Friend _OnOpenFunc As OnOpenDelegate
    Friend _OnMessageFunc As OnMessageDelegate
    Friend _OnCloseFunc As OnCloseDelegate
    Friend _OnErrorFunc As OnErrorDelegate
    Friend SessionSendDelegates As New Dictionary(Of String, SendDelegate)
    'Public Send As SendDelegate
    Delegate Sub OnOpenDelegate(ByVal id As String)
    Delegate Sub OnMessageDelegate(ByVal id As String, ByVal data As String)
    Delegate Sub OnCloseDelegate(ByVal id As String, ByVal code As String, ByVal reason As String)
    Delegate Sub OnErrorDelegate(ByVal id As String, ByVal message As String, ByVal exception As Exception)
    Delegate Sub SendDelegate(ByVal message As String)

    'Delegate for OnOpen.
    Public WriteOnly Property OnOpenFunc As OnOpenDelegate
        Set(value As OnOpenDelegate)
            _OnOpenFunc = CType(value, OnOpenDelegate)
        End Set
    End Property

    'Delegate for websocket OnMessage.
    Public WriteOnly Property OnMessageFunc As OnMessageDelegate
        Set(value As OnMessageDelegate)
            _OnMessageFunc = CType(value, OnMessageDelegate)
        End Set
    End Property

    'Delegate for websocket OnClose.
    Public WriteOnly Property OnCloseFunc As OnCloseDelegate
        Set(value As OnCloseDelegate)
            _OnCloseFunc = CType(value, OnCloseDelegate)
        End Set
    End Property

    'Delegate for websocket OnError.
    Public WriteOnly Property OnErrorFunc As OnErrorDelegate
        Set(value As OnErrorDelegate)
            _OnErrorFunc = CType(value, OnErrorDelegate)
        End Set
    End Property

    'Not used.
    Friend WriteOnly Property SendFunc As SendDelegate
        Set(value As SendDelegate)
            'Send = CType(value, SendDelegate)
        End Set
    End Property

    'Adds the send func delegate for a specific client that has connected.
    Friend Sub AddSendFunc(ByVal SessionID As String, ByVal Send As SendDelegate)
        SessionSendDelegates.Add(SessionID, Send)
    End Sub

    'Removes the send func delegate for a specific client that has disconnected.
    Friend Sub RemoveSendFunc(ByVal SessionID As String)
        SessionSendDelegates.Remove(SessionID)
    End Sub
End Class

