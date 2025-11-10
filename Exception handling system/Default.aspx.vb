Public Class _Default
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim ds1 As DataSet
    Dim ds2 As DataSet
    Dim ds3 As DataSet
    Dim ds4 As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Db.Set_Db("172.30.4.225", "DNTW_Warehouse")
            Session("user_id") = ""
            Session("異常編號") = ""
            msg_error.Visible = False
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If user_id.Text.Trim() = "" Then  '如果帳號密碼空白
                msg_error.Visible = True
                msg_error.Text = ("帳號不可為空白")
                msg_error.ForeColor = Drawing.Color.Red
            Else
                Dim id As String = user_id.Text.ToString.Trim()
                Dim pass As String = user_pass.Text.ToString.Trim()

                Dim sql As String = "select Code,Name,Department ,Email,PWD from Staff  where Code ='" & id & "' and PWDCOMPARE ('',PWD)=1 and Quit=0 "
                ds = Db.Get_DataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dr As DataRow = ds.Tables(0).Rows(0)
                    Session("user_id") = dr("Code")  '讓登入時的帳號可以在整個程式通用
                    Session("user_name") = dr("Name")
                    Session("user_pass") = ""

                    Dim a As String = ""
                    a = Request.ServerVariables("REMOTE_ADDR")

                    Dim sql3 As String = "insert into Exception_Loginremenber (user_id ,user_name ,login_time ,login_ip ) values ('" & Session("user_id") & "','" & Session("user_name") & "',GETDATE(),'" & a.ToString() & "')"
                    ds3 = Db.Get_DataSet(sql3)
                    Response.Redirect("Password_change.aspx")
                End If

                Dim sql1 As String = "select Code,Name,Department ,Email,PWD from Staff  where Code ='" & id & "' and PWDCOMPARE ('" & pass & "',PWD)=1 and Quit=0 "
                ds1 = Db.Get_DataSet(sql1)
                'select * from Exception_Login  where PWDCOMPARE ('denso1',user_pw)=1
                If ds1.Tables(0).Rows.Count > 0 Then
                    Dim dr As DataRow = ds1.Tables(0).Rows(0)
                    Session("user_id") = dr("Code")  '讓登入時的帳號可以在整個程式通用
                    Session("user_name") = dr("Name")
                    Session("user_pass") = pass
                    '記IP
                    Dim a As String = ""
                    a = Request.ServerVariables("REMOTE_ADDR")

                    Dim sql3 As String = "insert into Exception_Loginremenber (user_id ,user_name ,login_time ,login_ip ) values ('" & Session("user_id") & "','" & Session("user_name") & "',GETDATE(),'" & a.ToString() & "')"
                    ds3 = Db.Get_DataSet(sql3)

                    '權限

                    If Session("Exception") <> "" Then
                        Dim sql2 As String = " select user_id,user_dept,user_name,user_position ,user_mail ,user_group  from Exception_Login left join staff on user_id =code  where  code='" & id & "'  and PWDCOMPARE('" & pass & "',PWD)=1 and Quit=0"
                        ds2 = Db.Get_DataSet(sql2)
                        If ds2.Tables(0).Rows.Count > 0 Then
                            Response.Redirect(Session("Exception"))
                        Else
                            Response.Redirect("Home_page.aspx")
                        End If
                    Else
                        Response.Redirect("Home_page.aspx")
                    End If
                Else

                    msg_error.Visible = True
                    msg_error.Text = ("你的帳號或密碼錯誤!")
                    msg_error.ForeColor = Drawing.Color.Red

                End If
            End If

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
End Class