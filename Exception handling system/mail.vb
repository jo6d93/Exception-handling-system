Imports System.Net.Mail
Imports System.Net
Imports Microsoft.Exchange.WebServices.Data

Public Class mail

    Public Shared Sub send_mail(ByVal to_mail As String, ByVal subject As String, ByVal body As String)
        Try

            Dim html As String = "表單連結:</br><html><head><title></title></head><body> http://172.30.4.225/Exception_handle.aspx?id=" & body & "</body></html>"

            Send_Outlook(to_mail, "", subject, html)

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub

    Public Shared Sub send_mail1(ByVal to_mail As String, ByVal CC As String, ByVal subject As String, ByVal body As String)
        Try

            Dim html As String = "表單連結:</br><html><head><title></title></head><body> http://172.30.4.225/Exception_handle.aspx?id=" & body & "</body></html>"

            Send_Outlook(to_mail, "", subject, html)
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub

    Public Shared Sub send_mail2(ByVal to_mail As String, ByVal subject As String, ByVal body As String)
        Try
            Dim html As String = "表單連結:</br><html><head><title></title></head><body> http://172.30.4.225/Review.aspx?id=" & body & "</body></html>"

            Send_Outlook(to_mail, "", subject, html)

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub

    Public Shared Sub send_mail3(ByVal to_mail As String, ByVal subject As String, ByVal body As String)
        Try

            Dim html As String = "表單連結:</br><html><head><title></title></head><body> http://172.30.4.225/Exception_view.aspx </body></html>"

            Send_Outlook(to_mail, "", subject, html)

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Public Shared Sub send_mail4(ByVal to_mail As String, ByVal subject As String, ByVal body As String)
        Try

            Dim html As String = "<html><head><title></title></head><body>您新修改的密碼為:" & body & "</body><br/>如不是您自行修改的，請立即至 http://172.30.4.225/Password_change.aspx 更改密碼</html>"

            Send_Outlook(to_mail, "", subject, html)

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub

    Public Shared Sub Send_Outlook(to_mail As String, to_cc As String, subject As String, body As String)

        Dim mail_to As List(Of EmailAddress) = New List(Of EmailAddress)
        Dim mail_cc As List(Of EmailAddress) = New List(Of EmailAddress)
        Dim mail_bcc As List(Of EmailAddress) = Nothing
        Dim mail_file As List(Of String) = Nothing

        If to_mail <> "" Then
            Dim mail_list As String() = to_mail.Split(",")
            For Each eachmaill As String In mail_list
                mail_to.Add(eachmaill)
            Next
        End If

        If to_cc <> "" Then
            Dim mail_list As String() = to_cc.Split(",")
            For Each eachmaill As String In mail_list
                mail_cc.Add(eachmaill)
            Next
        End If

        Dim Sql As String = String.Format("Select * From MailConfig")
        Dim ds As DataSet = Db.Get_DataSet(Sql)
        Dim dt As DataTable = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            Dim dr As DataRow = dt.Rows(0)
            Dim Adress As String = dr("Account")
            Dim Password As String = dr("Password")
            Exchange.Main(Adress, Password)

            Exchange.Outbox.Prepare(subject, body, mail_to, mail_cc, mail_bcc, mail_file).Send()

        End If

    End Sub
End Class
