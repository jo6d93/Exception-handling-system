Public Class Account_creat
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim ds1 As DataSet
    Dim ds2 As DataSet
    Dim ds3 As DataSet
    Dim ds4 As DataSet
    Dim ds5 As DataSet
    Dim ds6 As DataSet
    Dim ds7 As DataSet
    Dim ds8 As DataSet
    Dim ds9 As DataSet


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Db.Set_Db("172.30.4.225", "DNTW_Warehouse")
            Session("Exception") = "Password_change.aspx"
            If Session("user_id") = "" Then
                Response.Redirect("Default.aspx")
            End If
            If Not Page.IsPostBack Then
                set_DropDownList()
                LinkButton2.Visible = True
                LinkButton3.Visible = False
                Login()
            End If
            user_name1()
            GV()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub GV()
        Try
            Dim sql As String = "select 填寫者,user_name AS 檢討者 from(select user_name AS 填寫者,reviewer from Exception_process inner join Exception_Login  on  writer =user_id) AS TEMP inner join Exception_Login on TEMP.reviewer= Exception_Login.user_id"
            ds = Db.Get_DataSet(sql)

            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
          
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub user_name1() '權限
        Try
            Dim sql As String = "select user_id ,user_name ,user_dept ,user_group  from Exception_Login  where user_id = '" & Session("user_id").Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            If ds.Tables(0).Rows.Count > 0 Then
                user_name.Text = Session("user_name").ToString()
            End If
            If dr("user_group") = "承認者" Then
                ImageButton4.Visible = True
            Else
                ImageButton4.Visible = False
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Try
            Session("Exception") = ""
            Response.Redirect("Default.aspx")
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub set_DropDownList()
        Try
            DropDownList1.Items.Add("")
            DropDownList1.Items.Add("填寫者")
            DropDownList1.Items.Add("檢討者")
            DropDownList1.Items.Add("承認者")
            DropDownList1.Items.Add("通知")

            DropDownList3.Items.Add("")
            DropDownList3.Items.Add("是")
            DropDownList3.Items.Add("否")

            DropDownList4.Items.Add("")
            DropDownList4.Items.Add("是")
            DropDownList4.Items.Add("否")

            DropDownList7.Items.Add("")
            DropDownList7.Items.Add("是")
            DropDownList7.Items.Add("否")



            Dim sql As String = "select Name from (select reviewer from Exception_process group by reviewer ) AS TEMP inner join staff on reviewer =code order by reviewer ASC"
            ds = Db.Get_DataSet(sql)
            DropDownList2.Items.Clear()
            DropDownList2.Items.Add("")
            For k As Integer = 0 To ds.Tables(0).Rows.Count() - 1
                Dim dr As DataRow = ds.Tables(0).Rows(k)
                If ds.Tables(0).Rows.Count > 0 Then
                    DropDownList2.Items.Add(dr("Name"))
                End If
            Next

            Dim sql3 As String = "select recipient from Exception_process group by recipient order by recipient ASC"
            ds = Db.Get_DataSet(sql3)
            DropDownList5.Items.Clear()
            For k As Integer = 0 To ds.Tables(0).Rows.Count() - 1
                Dim dr As DataRow = ds.Tables(0).Rows(k)
                If ds.Tables(0).Rows.Count > 0 Then
                    DropDownList5.Items.Add(dr("recipient"))
                End If
            Next

            Dim sql4 As String = "select CC from Exception_process group by CC order by CC ASC"
            ds = Db.Get_DataSet(sql4)
            DropDownList6.Items.Clear()
            For k As Integer = 0 To ds.Tables(0).Rows.Count() - 1
                Dim dr As DataRow = ds.Tables(0).Rows(k)
                If ds.Tables(0).Rows.Count > 0 Then
                    DropDownList6.Items.Add(dr("CC"))
                End If
            Next
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        Response.Redirect("Home_page.aspx")
    End Sub
    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs)
        Response.Redirect("Exception_view.aspx")
    End Sub
    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        Response.Redirect("Exception_handle.aspx")
    End Sub
    Protected Sub ImageButton4_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton4.Click
        Response.Redirect("Review.aspx")
    End Sub
    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        user_select()
        LinkButton2.Visible = False
        LinkButton3.Visible = True
    End Sub
    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        Login()
        LinkButton2.Visible = True
        LinkButton3.Visible = False
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            If TextBox1.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "" And TextBox4.Text <> "" And TextBox6.Text <> "" Then
                Dim sql As String = "select user_id from Exception_Login where user_id='" & TextBox1.Text.ToString.Trim() & "'"
                ds = Db.Get_DataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Label9.Visible = True
                    Label9.Text = "此帳號已存在"
                Else
                    Dim sql1 As String = "insert into Exception_Login(user_id,user_position,user_name,user_dept,user_group,user_mail)values('" & TextBox1.Text.ToString.Trim() & "','" & TextBox3.Text.ToString.Trim() & "','" & TextBox2.Text.ToString.Trim() & "','" & TextBox4.Text.ToString.Trim() & "','" & DropDownList1.Text.ToString.Trim() & "','" & TextBox6.Text.ToString.Trim() & "')"
                    ds1 = Db.Get_DataSet(sql1)

                    Dim a As String = DropDownList2.Text.ToString.Trim()
                    If DropDownList2.Text.ToString.Trim() = "" Then
                        a = ""
                    End If
                    Dim d As String = DropDownList5.Text.ToString.Trim()
                    If DropDownList5.Text.ToString.Trim() = "否" Then
                        d = ""
                    End If
                    Dim f As String = DropDownList6.Text.ToString.Trim()
                    If DropDownList6.Text.ToString.Trim() = "否" Then
                        f = ""
                    End If
                    Dim g As String = DropDownList6.Text.ToString.Trim()
                    If DropDownList7.Text.ToString.Trim() = "否" Then
                        g = ""
                    End If
                   
                    Dim sql2 As String = "insert into Exception_process (writer ,reviewer ,notice,notice_all,recipient,CC)values ('" & TextBox1.Text.ToString.Trim() & "','" & a.ToString.Trim() & "','','" & g.ToString.Trim() & "','" & d.ToString.Trim() & "','" & f.ToString.Trim() & "')"
                    ds2 = Db.Get_DataSet(sql2)

                    Label9.Text = "帳號已新增完成"
                    Label9.Visible = True
                    TextBox1.Text = "" '帳號
                    TextBox2.Text = "" '名稱
                    TextBox3.Text = "" '職稱
                    TextBox4.Text = "" '部門
                    DropDownList1.SelectedItem.Text = "" '權限
                    TextBox6.Text = "" 'mail
                    Panel7.Visible = False
                    Panel6.Visible = False
                End If
            Else
                Label9.Visible = True
                Label9.Text = "欄位不可為空白"
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If TextBox5.Text <> "" Then
                Dim sql As String = "select * from Exception_Login where user_id='" & TextBox5.Text.ToString.Trim() & "'"
                ds = Db.Get_DataSet(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim dr As DataRow = ds.Tables(0).Rows(0)
                    TextBox1.Text = dr("user_id")
                    TextBox3.Text = dr("user_position")
                    TextBox2.Text = dr("user_name")
                    TextBox4.Text = dr("user_dept")
                    TextBox6.Text = dr("user_mail")
                    DropDownList1.SelectedValue = dr("user_group")
                    Panel6.Visible = True
                    DDL_select()
                    '每一個dropdownlist都要輸入資料

                    Dim sql1 As String = "select user_id ,user_name,permission from Exception_LxP  where permission like '承認者%' and user_id ='" & TextBox1.Text.ToString.Trim() & "' "
                    ds1 = Db.Get_DataSet(sql1)
                    If ds1.Tables(0).Rows.Count > 0 Then
                        Dim dr1 As DataRow = ds1.Tables(0).Rows(0)
                        If dr1("permission") = "" Then
                            DropDownList2.SelectedItem.Text = ""
                        Else
                            DropDownList2.SelectedItem.Text = dr1("permission")
                        End If
                    End If

                    Dim sql4 As String = "select user_id ,user_name,permission from Exception_LxP  where (permission like '第一接收者%' or permission like 'CC%') and user_id ='" & TextBox1.Text.ToString.Trim() & "' "
                    ds4 = Db.Get_DataSet(sql4)
                    If ds4.Tables(0).Rows.Count > 0 Then
                        Dim dr1 As DataRow = ds4.Tables(0).Rows(0)
                        If dr1("permission") = "" Then
                            DropDownList5.SelectedItem.Text = ""
                        Else
                            DropDownList5.SelectedItem.Text = dr1("permission")
                        End If
                    End If

                    Dim sql5 As String = "select user_id ,user_name,permission from Exception_LxP  where permission like '通知%' and user_id ='" & TextBox1.Text.ToString.Trim() & "' "
                    ds5 = Db.Get_DataSet(sql5)
                    If ds5.Tables(0).Rows.Count > 0 Then
                        Dim dr1 As DataRow = ds5.Tables(0).Rows(0)
                        If dr1("permission") = "" Then
                            DropDownList6.SelectedItem.Text = ""
                        Else
                            DropDownList6.SelectedItem.Text = dr1("permission")
                        End If
                    End If

                    user_update()
                    LinkButton2.Visible = False
                    LinkButton3.Visible = True
                    TextBox5.Text = ""
                    Button4.Visible = True
                Else
                    Label12.Visible = True
                    Label12.Text = "查無此帳號"

                End If
            Else
                Label12.Visible = True
                Label12.Text = "請輸入帳號"
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs)
        Try
            Dim sql7 As String = "select user_id from Exception_Login where user_id='" & TextBox1.Text.ToString.Trim() & "'"
            ds7 = Db.Get_DataSet(sql7)
            If ds7.Tables(0).Rows.Count > 0 Then
                Dim sql8 As String = "select * from Exception_LxP where user_id='" & TextBox1.Text.ToString.Trim() & "'"
                ds8 = Db.Get_DataSet(sql8)
                If ds8.Tables(0).Rows.Count > 0 Then
                    Dim sql9 As String = "delete Exception_LxP where user_id='" & TextBox1.Text.ToString.Trim() & "'"
                    ds9 = Db.Get_DataSet(sql9)

                    Dim a As String = DropDownList2.Text.ToString.Trim()
                    If DropDownList2.Text.ToString.Trim() = "請選擇發送對象" Then
                        a = ""
                    End If
                    Dim sql2 As String = "insert into Exception_LxP (user_id,user_name,permission )values('" & TextBox1.Text.ToString.Trim() & "','" & TextBox2.Text.ToString.Trim() & "','" & a.ToString.Trim() & "')"
                    ds2 = Db.Get_DataSet(sql2)

                    Dim d As String = DropDownList5.Text.ToString.Trim()
                    If DropDownList5.Text.ToString.Trim() = "請選擇接收者" Then
                        d = ""
                    End If
                    Dim sql5 As String = "insert into Exception_LxP (user_id,user_name,permission )values('" & TextBox1.Text.ToString.Trim() & "','" & TextBox2.Text.ToString.Trim() & "','" & d.ToString.Trim() & "')"
                    ds5 = Db.Get_DataSet(sql5)

                    Dim f As String = DropDownList6.Text.ToString.Trim()
                    If DropDownList6.Text.ToString.Trim() = "請選擇通知者" Then
                        f = ""
                    End If
                    Dim sql6 As String = "insert into Exception_LxP (user_id,user_name,permission )values('" & TextBox1.Text.ToString.Trim() & "','" & TextBox2.Text.ToString.Trim() & "','" & f.ToString.Trim() & "')"
                    ds6 = Db.Get_DataSet(sql6)

                End If


                Dim sql As String = "update Exception_Login set user_dept ='" & TextBox4.Text.ToString.Trim() & "',user_name ='" & TextBox2.Text.ToString.Trim() & "',user_position ='" & TextBox3.Text.ToString.Trim() & "',user_mail ='" & TextBox6.Text.ToString.Trim() & "',user_group ='" & DropDownList1.SelectedItem.Text.ToString.Trim() & "' where user_id ='" & TextBox1.Text.ToString.Trim() & "'"
                ds = Db.Get_DataSet(sql)

                Label9.Visible = True
                Label9.Text = "已修改完成"

                TextBox1.Text = "" '帳號
                TextBox2.Text = "" '名稱
                TextBox3.Text = "" '職稱
                TextBox4.Text = "" '部門
                DropDownList1.SelectedItem.Text = "" '權限
                TextBox6.Text = "" 'mail
                TextBox5.Text = ""
                Button3.Visible = False
                Button4.Visible = True
                Panel7.Visible = False
            Else
                Label9.Visible = True
                Label9.Text = "無此帳號"
            End If

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub Login()
        Panel1.Visible = True
        Panel4.Visible = False
        Panel6.Visible = True
        Panel7.Visible = False
        Panel8.Visible = True
        Label11.Text = "新增使用者"

        Button1.Visible = True
        Button3.Visible = False
        Button4.Visible = False

        Label9.Visible = False
        TextBox1.Text = "" '帳號
        TextBox2.Text = "" '名稱
        TextBox3.Text = "" '職稱
        TextBox4.Text = "" '部門
        DropDownList1.SelectedItem.Text = "" '權限
        TextBox6.Text = "" 'mail
    End Sub
    Private Sub user_select()
        Panel1.Visible = False
        Panel4.Visible = True
        Panel6.Visible = False
        Panel7.Visible = False

        Label11.Text = "使用者資料查詢"
        Label2.Visible = True
        TextBox5.Visible = True
        Button2.Visible = True
        Label12.Visible = False
    End Sub
    Private Sub user_update()
        Panel1.Visible = True
        Panel4.Visible = False
        Panel6.Visible = True
        Panel7.Visible = False
        Panel8.Visible = True
        Label11.Text = "使用者資料修改"
        Button1.Visible = False
        Button3.Visible = True
        Button4.Visible = False

        Label9.Visible = True
    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs)
        user_select()
        LinkButton2.Visible = False
        LinkButton3.Visible = True
        TextBox5.Text = ""
        Button4.Visible = False
    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs)
        DDL_select()
    End Sub
    Private Sub DDL_select()
        Try

            If DropDownList1.SelectedValue = "承認者" Then
               
                Label16.Visible = True
                DropDownList5.Visible = True
                Label13.Visible = True
                DropDownList2.Visible = True
                Panel7.Visible = True

            ElseIf DropDownList1.SelectedValue = "檢討者" Then
               
                Label16.Visible = True
                DropDownList5.Visible = True
                Label13.Visible = False
                DropDownList2.Visible = False
                Panel7.Visible = True
            ElseIf DropDownList1.SelectedValue = "填寫者" Then
               
                Label16.Visible = True
                DropDownList5.Visible = True
                Label13.Visible = False
                DropDownList2.Visible = False
                Panel7.Visible = True
            ElseIf DropDownList1.SelectedValue = "通知" Then
                
                Label16.Visible = False
                DropDownList5.Visible = False
                Label13.Visible = False
                DropDownList2.Visible = False
                Panel7.Visible = True
            ElseIf DropDownList1.SelectedValue = "" Then
              
                Label16.Visible = False
                DropDownList5.Visible = False
                Label13.Visible = False
                DropDownList2.Visible = False
                Panel7.Visible = False
            End If


        Catch ex As Exception
            Dim str As String = ex.Message
        End Try

    End Sub
End Class