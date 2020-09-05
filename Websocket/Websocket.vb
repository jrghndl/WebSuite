Imports WebSocketSharp

Public Class Websocket
    Inherits WebSocketSharp.Server.WebSocketBehavior

    Dim Functions As WebsocketFunctions

    'Initializes object that describes websocket behavior.
    Public Sub New(ByVal _Functions As WebsocketFunctions)
        Functions = _Functions
    End Sub

    'Sets OnOpen behavior based on WebsocketFunctions object.
    Protected Overrides Sub OnOpen()
        Functions.AddSendFunc(ID, AddressOf SendMessage)
        Functions._OnOpenFunc(ID)
    End Sub

    'OnMessage behavior based on WebsocketFunctions object.
    Protected Overrides Sub OnMessage(e As MessageEventArgs)
        Functions._OnMessageFunc(ID, e.Data)
    End Sub

    'OnClose behavior based on WebsocketFunctions object.
    Protected Overrides Sub OnClose(e As CloseEventArgs)
        Functions._OnCloseFunc(ID, e.Code, e.Reason)
    End Sub

    'OnError behavior based on WebsocketFunctions object.
    Protected Overrides Sub OnError(e As ErrorEventArgs)
        Functions._OnErrorFunc(ID, e.Message, e.Exception)
    End Sub

    'Sends a message.
    Sub SendMessage(ByVal message As String)
        Send(message)
    End Sub

End Class
