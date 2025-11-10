Public Class Password_change
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim ds1 As DataSet
    Dim ds2 As DataSet
    Dim ds3 As DataSet
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Db.Set_Db("172.30.4.225", "DNTW_Warehouse")
            Session("Exception") = "Password_change.aspx"
            If Session("user_id") = "" Then
                Response.Redirect("Default.aspx")
            End If
            If Session("異常編號") <> "" Then
                ImageButton3.Visible = True
            End If
            user_name1()

           
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub user_name1() '權限
        Try
            Dim sql As String = "select Staff.Code,Staff.Name,Staff.Email,Exception_Login.user_position ,Exception_Login.user_dept ,Exception_Login.user_group  from Staff left join Exception_Login on Staff.Code =Exception_Login.user_id  where Code = '" & Session("user_id").Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            If ds.Tables(0).Rows.Count > 0 Then
                user_name.Text = dr("Name").ToString()
                Session("user_name") = dr("Name").ToString()
                Session("user_id") = dr("Code").ToString()
            End If

            If Session("user_pass") = "" Then
                ImageButton1.Visible = False
                ImageButton2.Visible = False
                ImageButton3.Visible = False
                ImageButton4.Visible = False
                Label4.Visible = False
                TextBox1.Visible = False
                Label5.Visible = True
                Label5.Text = "您為第一次登入，請設定您的登入密碼"

            Else
                If dr("user_group") = "承認者" Then
                    ImageButton4.Visible = True
                Else
                    ImageButton4.Visible = False
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Dim pass As String = TextBox1.Text.ToString.Trim()

            Dim sql As String = "select Code from Staff  where Code ='" & Session("user_id").Trim() & "' and PWDCOMPARE ('" & pass & "',PWD)=1 and Quit=0 "
            ds = Db.Get_DataSet(sql)

            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                If TextBox2.Text.ToString.Trim() <> "" And TextBox3.Text.ToString.Trim() <> "" Then
                    If TextBox2.Text.ToString() = TextBox3.Text.ToString() Then
                        Dim new_pass As String = TextBox3.Text.ToString.Trim()
                        Dim sql1 As String = "update Staff set PWD = PWDENCRYPT ('" & new_pass.ToString.Trim() & "') where Code='" & Session("user_id").Trim() & "'"
                        ds1 = Db.Get_DataSet(sql1)

                        If Session("user_pass") = "" Then

                            Session("user_pass") = new_pass.ToString.Trim()
                            '寄信
                            set_mail()
                            Session("Exception") = ""
                            Response.Redirect("Default.aspx")
                        End If
                        '寄信
                        Session("user_pass") = new_pass.ToString.Trim()
                        set_mail()
                        Label5.Visible = True
                        Label5.Text = "密碼已變更完成!!，請按到貨進度一覽回首頁"
                        TextBox1.Text = ""
                        TextBox2.Text = ""
                        TextBox3.Text = ""

                    Else
                        Label5.Visible = True
                        Label5.Text = "兩次密碼不一樣，請確認!!"
                    End If
                Else
                    Label5.Visible = True
                    Label5.Text = "密碼不可為空白!!"
                End If
            Else
                Label5.Visible = True
                Label5.Text = "您輸入的舊密碼錯誤"
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
            End If

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub set_mail()
        Try
            Dim sql As String = "select Code,Name,Department ,Email,PWD from Staff  where Code ='" & Session("user_id").Trim() & "'  and PWDCOMPARE ('" & Session("user_pass") & "',PWD)=1 and Quit=0 "
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                If Not dr("Email") Is DBNull.Value Then
                    Dim user_mail As String = dr("Email")
                    Dim number As String = Session("user_pass")
                    Dim company As String = Session("user_name") + "密碼已變更"
                    mail.send_mail4(user_mail, company, number)
                Else
                    Label5.Visible = True
                    Label5.Text = "搜尋不到您的Mail，如有疑問請聯絡人事課陳美惠"
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub  '寄信
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            Session("Exception") = ""
            Response.Redirect("Default.aspx")
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs)
        Response.Redirect("Home_page.aspx")
    End Sub
    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs)
        Response.Redirect("Exception_view.aspx")
    End Sub
    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs)
        Response.Redirect("Exception_handle.aspx")
    End Sub
    Protected Sub ImageButton4_Click(sender As Object, e As ImageClickEventArgs)
        Response.Redirect("Review.aspx")
    End Sub
End Class