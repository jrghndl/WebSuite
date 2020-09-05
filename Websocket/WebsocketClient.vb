Public Class WebsocketClient
    Dim _WebsocketClient As WebSocketSharp.WebSocket

    Friend _OnOpenFunc As OnOpenDelegate
    Friend _OnMessageFunc As OnMessageDelegate
    Friend _OnCloseFunc As OnCloseDelegate
    Friend _OnErrorFunc As OnErrorDelegate
    Delegate Sub OnOpenDelegate()
    Delegate Sub OnMessageDelegate(ByVal data As String)
    Delegate Sub OnCloseDelegate(ByVal code As String, ByVal reason As String)
    Delegate Sub OnErrorDelegate(ByVal message As String, ByVal exception As Exception)

    'Sets the OnOpen function.
    Public WriteOnly Property OnOpenFunc As OnOpenDelegate
        Set(value As OnOpenDelegate)
            _OnOpenFunc = CType(value, OnOpenDelegate)
        End Set
    End Property

    'Sets the OnMessage function.
    Public WriteOnly Property OnMessageFunc As OnMessageDelegate
        Set(value As OnMessageDelegate)
            _OnMessageFunc = CType(value, OnMessageDelegate)
        End Set
    End Property

    'Sets the OnClose function.
    Public WriteOnly Property OnCloseFunc As OnCloseDelegate
        Set(value As OnCloseDelegate)
            _OnCloseFunc = CType(value, OnCloseDelegate)
        End Set
    End Property

    'Sets the OnError function.
    Public WriteOnly Property OnErrorFunc As OnErrorDelegate
        Set(value As OnErrorDelegate)
            _OnErrorFunc = CType(value, OnErrorDelegate)
        End Set
    End Property

    'Initializes a new websocket client at a given URL.
    Public Sub New(ByVal URL As String)
        _WebsocketClient = New WebSocketSharp.WebSocket(URL)
        AddHandler _WebsocketClient.OnOpen, AddressOf OnOpen
        AddHandler _WebsocketClient.OnClose, AddressOf OnClose
        AddHandler _WebsocketClient.OnError, AddressOf OnError
        AddHandler _WebsocketClient.OnMessage, AddressOf OnMessage
    End Sub

    'Establishes a connection.
    Sub Connect()
        _WebsocketClient.Connect()
    End Sub

    'Disconnects from server.
    Sub Disconnect()
        _WebsocketClient.Close()
    End Sub

    'Called when connection is establised.
    Sub OnOpen()
        _OnOpenFunc()
    End Sub

    'Called when message is received.
    Sub OnMessage(sender As Object, e As WebSocketSharp.MessageEventArgs)
        _OnMessageFunc(e.Data)
    End Sub

    'Called when connection is closed.
    Sub OnClose(sender As Object, e As WebSocketSharp.CloseEventArgs)
        _OnCloseFunc(e.Code, e.Reason)
    End Sub

    'Called on websocket error.
    Sub OnError(sender As Object, e As WebSocketSharp.ErrorEventArgs)
        _OnErrorFunc(e.Message, e.Exception)
    End Sub

    'Sends a message to the server.
    Sub SendMessage(ByVal Message As String)
        _WebsocketClient.Send(Message)
    End Sub

End Class
