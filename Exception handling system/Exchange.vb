
Imports System.Data.SqlClient
Imports System.Net
Imports System.Security
Imports Microsoft.Exchange.WebServices.Data
Public Class Exchange

    Public Shared Inbox As InboxFolder = New InboxFolder()
    Public Shared Outbox As OutboxFolder = New OutboxFolder()

    Private _User As String

    Public Property User() As String
        Get
            Return _User
        End Get
        Set(ByVal value As String)
            _User = value
        End Set
    End Property

    Private _Password As SecureString

    Public Property Password() As SecureString
        Get
            Return _Password
        End Get
        Set(ByVal value As SecureString)
            _Password = value
        End Set
    End Property

    Public Shared _Service As ExchangeService

    Public Shared Property Service() As ExchangeService
        Get
            Return _Service
        End Get
        Set(ByVal value As ExchangeService)
            _Service = value
        End Set
    End Property

    Public Shared Sub Main(User As String, password As String)

        Dim ex As Exchange = New Exchange()

        ex.User = User
        ex.Password = (New NetworkCredential("", password)).SecurePassword
        ex.Service = New ExchangeService()
        ex.Service.Credentials = New NetworkCredential(ex.User, ex.Password)
        ex.Service.Url = New Uri("https://outlook.office365.com/EWS/Exchange.asmx")

        Inbox.GetFolder()
    End Sub

    Public Shared Sub Main()

        Dim default_account As String = "mail.dntw.a9u@tw.denso.com"

        Dim connection_string As String = String.Format("Data Source={0},{1};Initial Catalog={2};User ID={3};Password={4}", "172.30.4.225", 1433, "DNTW_Warehouse", "0011", "0011")

        Dim Sql As String = String.Format("select * from MailConfig where Account='{0}'", default_account)

        Dim dt As DataTable = New DataTable()


        Using sqlCon As SqlConnection = New SqlConnection(connection_string)
            sqlCon.Open()
            Dim sqlCmd As SqlCommand
            sqlCmd = New SqlCommand(Sql, sqlCon)
            Dim sqlAdp As SqlDataAdapter = New SqlDataAdapter(sqlCmd)
            sqlAdp.Fill(dt)
            sqlCon.Close()
        End Using   '-- 處置DataReader****（巢狀 Using）****


        If dt.Rows.Count = 1 Then
            Dim ac As String = dt.Rows(0)("Account").ToString()
            Dim pw As String = dt.Rows(0)("Password").ToString()
            Dim ex As Exchange = New Exchange()
            ex.User = ac
            ex.Password = (New NetworkCredential("", pw)).SecurePassword
            ex.Service = New ExchangeService()
            ex.Service.Credentials = New NetworkCredential(ex.User, ex.Password)
            ex.Service.Url = New Uri("https://outlook.office365.com/EWS/Exchange.asmx")

            Inbox.GetFolder()
        Else
            Throw New Exception("Cannot find mail ac/pw in table MailConfig")
        End If
    End Sub

    Public Shared Function GetDefaultPropertySet() As PropertySet
        Dim prop As PropertySet = New PropertySet(BasePropertySet.IdOnly)
        prop.Add(ItemSchema.Subject)
        prop.Add(ItemSchema.TextBody)
        prop.Add(ItemSchema.Attachments)
        prop.Add(ItemSchema.HasAttachments)
        Return prop
    End Function

    Public Shared Sub DownloadAttachment(mail As EmailMessage, storage As String)

        If mail.HasAttachments Then
            For Each attachment As Attachment In mail.Attachments

                Try
                    Dim file As FileAttachment = attachment
                    file.Load(System.IO.Path.Combine(storage, file.Name))

                Catch ex As Exception

                End Try
            Next
        End If
    End Sub
End Class


