Imports System.Security.Authentication.SslProtocols
Imports System.Security.Cryptography.X509Certificates

Public Class WebsocketServer
    Private WebsocketServer As WebSocketSharp.Server.WebSocketServer
    Public Functions As New WebsocketFunctions

#Region "Settings"
    Private _Port As Integer = 80 '80 for WS, 443 for WSS
    Private _CertificatePath As String = ""
    Private _URLPath As String = "/websocket" 'Default location is ws://127.0.0.1:80/websocket
    Public Property Verbose As Boolean = False

    'The port which clients connect to.
    Public Property Port As String
        Get
            Return _Port
        End Get
        Set(value As String)
            If (value >= 1 And value <= 65535) Then
                _Port = value
            Else
                Throw New System.Exception("Port must be between 1 and 65535.")
            End If
        End Set
    End Property

    'Whether to use SSL for the connection.
    Public Property UseSSL As Boolean = False

    'Path to a pfx certificate.
    Public Property CertificatePath As String
        Get
            Return _CertificatePath
        End Get
        Set(value As String)
            If My.Computer.FileSystem.FileExists(value) Then
                _CertificatePath = value
            Else
                Throw New System.Exception("Could not find certificate at the given location.")
            End If
        End Set
    End Property

    'Password for the certificate.
    Public Property CertificatePassword As String = ""

    'URL Path that a client uses to connect to the server.
    Public Property URLPath As String
        Get
            Return _URLPath
        End Get
        Set(value As String)
            If value = "" Then
                Throw New System.Exception("Cannot have an empty path.")
            Else
                _URLPath = value
            End If
        End Set
    End Property

#End Region

#Region "Server Functions"
    'Starts the server.
    Public Sub Start()
        WebsocketServer = New WebSocketSharp.Server.WebSocketServer(Port, UseSSL)
        If UseSSL Then
            WebsocketServer.SslConfiguration.EnabledSslProtocols = Tls12 Or Tls11 Or Tls
            WebsocketServer.SslConfiguration.ServerCertificate = New X509Certificate2(CertificatePath, CertificatePassword)
        End If
        If Verbose Then
            WebsocketServer.Log.Level = WebSocketSharp.LogLevel.Trace
        Else
            WebsocketServer.Log.Level = WebSocketSharp.LogLevel.Error
        End If

        WebsocketServer.AddWebSocketService(URLPath, Function() New Websocket(Functions))
        WebsocketServer.Start()
    End Sub

    'Sends a message to a specific client.
    Public Sub SendMessage(ByVal id As String, ByVal Message As String)
        Functions.SessionSendDelegates(id)(Message)
    End Sub

    'Shuts down the websocket server.
    Public Sub Shutdown()
        WebsocketServer.Stop()
    End Sub

    'Broadcasts a message to every connected client.
    Public Sub Broadcast(ByVal message As String)
        WebsocketServer.WebSocketServices.Broadcast(message)
    End Sub

    'Closes a specific session by ID.
    Public Sub CloseSession(ByVal ID As String)
        Functions.RemoveSendFunc(ID)
    End Sub
#End Region

End Class
