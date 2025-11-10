
Imports System.IO
Imports Microsoft.Exchange.WebServices.Data

Public Class OutboxFolder
    Public Function Prepare(subject As String, body As String, to_mail As List(Of EmailAddress), cc As List(Of EmailAddress), bcc As List(Of EmailAddress), mail_file As List(Of String)) As EmailMessage
        Try
            Dim ex As Exchange = New Exchange()

            Dim mail As EmailMessage = New EmailMessage(ex.Service)
            mail.Subject = subject
            mail.Body = body
            mail.ToRecipients.AddRange(to_mail)
            If Not cc Is Nothing Then
                mail.CcRecipients.AddRange(cc)
            End If

            If Not bcc Is Nothing Then
                mail.BccRecipients.AddRange(bcc)
            End If

            If Not mail_file Is Nothing Then
                For Each f As String In mail_file
                    If File.Exists(f) = False Then
                        Throw New FileNotFoundException(f)
                    End If
                    Dim stream As FileStream = New FileStream(f, FileMode.Open)
                    mail.Attachments.AddFileAttachment(Path.GetFileName(f), stream)
                Next
            End If
            Return mail
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class

