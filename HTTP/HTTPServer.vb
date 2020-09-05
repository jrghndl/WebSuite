Imports System.Security.Authentication.SslProtocols
Imports System.Security.Cryptography.X509Certificates

Public Class HTTPServer
    Dim Server As WebSocketSharp.Server.HttpServer
    Dim RedirectServer As WebSocketSharp.Server.HttpServer
    Dim _Port As Integer = 30000
    Dim _CertificatePath As String = ""
    Dim _RootPath As String
    Dim Redirects As New Dictionary(Of String, String)
    'Possible HTTP statuses.
    Public Enum HttpStatusCode
        [Continue] = 100
        SwitchingProtocols = 101
        NonAuthoritativeInformation = 203
        NoContent = 204
        ResetContent = 205
        PartialContent = 206
        MultipleChoices = 300
        Ambiguous = 300
        MovedPermanently = 301
        Moved = 301
        Found = 302
        Redirect = 302
        SeeOther = 303
        RedirectMethod = 303
        NotModified = 304
        UseProxy = 305
        Unused = 306
        TemporaryRedirect = 307
        RedirectKeepVerb = 307
        BadRequest = 400
        Unauthorized = 401
        PaymentRequired = 402
        Forbidden = 403
        NotFound = 404
        MethodNotAllowed = 405
        NotAcceptable = 406
        ProxyAuthenticationRequired = 407
        RequestTimeout = 408
        Conflict = 409
        Gone = 410
        LengthRequired = 411
        PreconditionFailed = 412
        RequestEntityTooLarge = 413
        RequestUriTooLong = 414
        UnsupportedMediaType = 415
        RequestedRangeNotSatisfiable = 416
        ExpectationFailed = 417
        InternalServerError = 500
        NotImplemented = 501
        BadGateway = 502
        ServiceUnavailable = 503
        GatewayTimeout = 504
        HttpVersionNotSupported = 505
    End Enum

#Region "Settings"

    'Verbose error reporting.
    Public Property Verbose As Boolean = False

    'Port property which clients will connect to.
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

    'Whether the server uses SSL.
    Public Property UseSSL As Boolean = False

    'Path to the pfx certificate.
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

    'Certificate's password.
    Public Property CertificatePassword As String = ""

    'Path for website's root folder.
    Public Property RootPath As String
        Get
            If My.Computer.FileSystem.DirectoryExists(_RootPath) Then
                Return _RootPath
            Else
                Throw New System.Exception("Root path not set.")
            End If
        End Get
        Set(value As String)
            If My.Computer.FileSystem.DirectoryExists(value) Then
                _RootPath = value
            Else
                Throw New System.Exception("Could not find folder at the given location.")
            End If
        End Set
    End Property

    Public Property UnderMaintenance As Boolean = False 'Whether the website is under maintenance. If true, it shows the page given at the maintenance path.
    Public Property MaintenancePath As String = "" 'Path for under maintenance page
    Public Property NotFoundPath As String = "" 'Path for 404 Error page
#End Region

    'Starts the HTTP Server
    Public Sub Start()
        Server = New WebSocketSharp.Server.HttpServer(Port, UseSSL)
        AddHandler Server.OnGet, AddressOf OnGet

        Server.DocumentRootPath = RootPath

        If UseSSL Then
            Server.SslConfiguration.EnabledSslProtocols = Tls12 Or Tls11 Or Tls
            Server.SslConfiguration.ServerCertificate = New X509Certificate2(CertificatePath, CertificatePassword)
            protocol = "https://"
        End If
        If Verbose Then
            Server.Log.Level = WebSocketSharp.LogLevel.Trace
        Else
            Server.Log.Level = WebSocketSharp.LogLevel.Error
        End If
        'Enable IP/webpage tracking using onconnect


        Server.Start()
    End Sub

    'Shuts the HTTP server down
    Public Sub Shutdown()
        Server.Stop()
    End Sub

    Dim protocol As String = "http://"

    'OnGet is called when a client sends a request.
    Private Sub OnGet(Sender As Object, e As WebSocketSharp.Server.HttpRequestEventArgs)
        Dim req = e.Request
        Dim res = e.Response
        Dim path = req.RawUrl
        If path = "/" Then path += "index.html"

        Dim contents As Byte()

        If UnderMaintenance Then
            If Not req.RawUrl = MaintenancePath Then
                res.StatusCode = HttpStatusCode.Redirect
                res.RedirectLocation = protocol + req.UserHostAddress + MaintenancePath
                Exit Sub
            End If
        End If

        If Redirects.ContainsKey(req.RawUrl) Then
            res.StatusCode = HttpStatusCode.Redirect
            res.RedirectLocation = protocol + req.Url.Host + ":" + Port + Redirects(req.RawUrl)

            Exit Sub
        End If

        If Not e.TryReadFile(path, contents) Then
            If NotFoundPath.Equals("") Then
                res.StatusCode = HttpStatusCode.NotFound
            Else
                res.StatusCode = HttpStatusCode.Redirect
                res.RedirectLocation = protocol + req.Url.Host + ":" + Port + NotFoundPath
            End If
            Exit Sub
        End If

        If path.EndsWith(".html") Then
            res.ContentType = "text/html"
            res.ContentEncoding = Text.Encoding.UTF8
        ElseIf path.EndsWith(".js") Then
            res.ContentType = "application/javascript"
            res.ContentEncoding = Text.Encoding.UTF8
        End If

        res.ContentLength64 = contents.LongLength
        res.Close(contents, True)

    End Sub

    'Adds a redirect.
    Public Sub AddRedirect(ByVal OldRootPath As String, ByVal NewRootPath As String)
        Redirects.Add(OldRootPath, NewRootPath)
    End Sub

    'Deleteds a redirect.
    Public Sub DeleteRedirect(ByVal OldRoothPath As String)
        Redirects.Remove(OldRoothPath)
    End Sub

    'Checks whether a redirect exists.
    Public Function RedirectExists(ByVal OldRootPath As String) As Boolean
        If Redirects.ContainsKey(OldRootPath) Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
