Imports System.Drawing

Public Class ReView1
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim ds1 As DataSet
    Dim ds2 As DataSet
    Dim ds3 As DataSet
    Dim ds4 As DataSet
    Dim page_id As String = ""
    Dim time As DateTime
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Db.Set_Db("172.30.4.225", "DNTW_Warehouse")
            page_id = If((Request.QueryString("id") Is Nothing), "", Request.QueryString("id"))
            Session("Exception") = "http://172.30.4.225/Review.aspx?id=" + page_id
            Dim a As String = Session("Exception")

            If Session("user_id") = "" Then
                Response.Redirect("Default.aspx")
            End If


            Button4.Attributes.Add("onclick", "return confirm('請於下方表格中填寫駁回原因')")
            Button5.Attributes.Add("onclick", "return confirm('請於下方表格中填寫駁回原因')")
            Button6.Attributes.Add("onclick", "return confirm('是否要發回給上一位填寫者重寫?')")
            Button7.Attributes.Add("onclick", "return confirm('是否要發回給上一位填寫者重寫?')")

            If Session("異常編號") <> "" Then
                ImageButton3.Visible = True
            End If

            If Not Page.IsPostBack Then
                select_date()
                btn_select_Click(Me, Nothing)
                If page_id <> "" Then
                    select_review()
                End If
            End If
            user_name1()

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
            If dr("user_group") <> "承認者" Then
                Response.Redirect("Home_page.aspx")
            Else
                ImageButton4.Visible = True
            End If

            If dr("user_id") = "018051" Then
                Panel10.Visible = True
            Else
                Panel10.Visible = False
            End If

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub set_DropDownList(ByVal obj As DropDownList, ByVal kara As Integer, ByVal made As Integer)
        Try
            obj.Items.Clear()
            Dim item0 As ListItem = New ListItem()
            item0.Value = ""
            item0.Text = ""
            obj.Items.Add(item0)

            For k As Integer = kara To made
                Dim item As ListItem = New ListItem()
                item.Value = k.ToString()
                item.Text = k.ToString()
                obj.Items.Add(item)
            Next
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub select_date()
        Try
            Dim x As String = Today.ToString("yyyy")
            Dim y As String = x - 2
            Dim a As Integer = Today.ToString("MM")
            Dim b As Integer = Today.ToString("dd")
            set_DropDownList(DropDownList3, y, x)
            DropDownList3.Text = x
            Dim yy As Integer = Int32.Parse(DropDownList3.SelectedValue)
            set_DropDownList(DropDownList2, 1, 12)
            DropDownList2.Text = a
            Dim mm As Integer = Int32.Parse(DropDownList2.SelectedValue)
            Dim dt1 As DateTime = New DateTime(yy, mm, 1)
            dt1 = dt1.AddMonths(1).AddDays(-1)
            set_DropDownList(DropDownList1, 1, dt1.Day)
            DropDownList1.Text = b
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Try
            Dim mm As Integer = Int32.Parse(DropDownList2.SelectedValue)
            Dim yy As Integer = Int32.Parse(DropDownList3.SelectedValue)
            Dim dt1 As DateTime = New DateTime(yy, mm, 1)
            dt1 = dt1.AddMonths(1).AddDays(-1)
            set_DropDownList(DropDownList1, 1, dt1.Day)
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub btn_select_Click(sender As Object, e As EventArgs) Handles btn_select.Click
        Try
            Dim a As String = DropDownList2.Text.ToString()  '月
            Dim b As String = DropDownList1.Text.ToString()  '日
            Dim c As String = ""
            Dim d As String = ""
            Dim no As Integer = a.Length
            Dim no1 As Integer = b.Length
            If no = 1 Then
                c = "0" + "" & a & ""
            Else
                c = a
            End If
            If no1 = 1 Then
                d = "0" + "" & b & ""
            Else
                d = b
            End If
            Dim sql As String = "select  異常編號, 異常日期 ,異常種類,異常對象 ,計畫時間 ,實際時間 , CONVERT(varchar(20),(異常調查原因)) AS 異常調查原因,CONVERT(varchar(20),(再發防止)) AS 再發防止,CONVERT(varchar(20),(責任部屬)) AS 責任部屬 ,簽核狀態 ,受入場,簽核狀態 from Exception_view " &
                "where 異常日期='" & DropDownList3.Text.ToString() & "-" & c.ToString() & "-" & d.ToString() & "' and (簽核狀態='尚未簽核' or 簽核狀態='簽核中' or 簽核狀態='簽核通過')  ORDER BY 計畫時間 ASC"


            ds = Db.Get_DataSet(sql)
            ds.Tables(0).Columns.Add(New DataColumn("no", GetType(String)))

            If ds.Tables(0).Rows.Count > 0 Then
                For k As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim dr As DataRow
                    dr = ds.Tables(0).Rows(k)
                    dr("no") = k + 1
                Next
                Panel4.Visible = True
                Table2.Visible = False
                Label13.Visible = False
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()
            Else
                Panel4.Visible = False
                Label13.Visible = True
                Table2.Visible = False
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub select_review()
        Try
            Dim sql As String = "select  異常編號, 異常日期 ,異常種類,異常對象 ,計畫時間 ,實際時間 , CONVERT(varchar(20),(異常調查原因)) AS 異常調查原因,CONVERT(varchar(20),(再發防止)) AS 再發防止,CONVERT(varchar(20),(責任部屬)) AS 責任部屬 ,簽核狀態 ,受入場,簽核狀態 from Exception_view " &
              "where 異常編號='" & page_id.ToString().Trim() & "' and  (簽核狀態='尚未簽核' or 簽核狀態='簽核中' or 簽核狀態='簽核通過')  ORDER BY 計畫時間 ASC"

            ds = Db.Get_DataSet(sql)
            ds.Tables(0).Columns.Add(New DataColumn("no", GetType(String)))

            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr1 As DataRow = ds.Tables(0).Rows(0)
                For k As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = ds.Tables(0).Rows(k)
                    dr("no") = k + 1
                Next
                Panel4.Visible = True
                Label13.Visible = False
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()

                Dim a As Date = dr1("異常日期").ToString()
                Dim b As Integer = a.ToString("yyyy")
                Dim c As Integer = a.ToString("MM")
                Dim d As Integer = a.ToString("dd")
                DropDownList3.Text = b
                DropDownList2.Text = c
                DropDownList1.Text = d
                Dim debug As String = ""
            Else
                Panel4.Visible = False
                Label13.Visible = True
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub BindData() '單筆資料
        Try
            Dim sql As String = "select  異常編號, 異常日期 ,異常種類,異常對象 ,計畫時間 ,實際時間 , CONVERT(varchar(20),(異常調查原因)) AS 異常調查原因,CONVERT(varchar(20),(再發防止)) AS 再發防止,CONVERT(varchar(20),(責任部屬)) AS 責任部屬 ,簽核狀態 ,受入場,簽核狀態 from Exception_view " &
                "where 異常編號='" & Session("異常編號") & "' and (簽核狀態='尚未簽核' or 簽核狀態='簽核中' or 簽核狀態='簽核通過')  ORDER BY 計畫時間 ASC"
            ds4 = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds4.Tables(0).Rows(0)
            If ds4.Tables(0).Rows.Count > 0 Then
                Table2.Visible = True

                Button4.Visible = True
                Button5.Visible = True

                textbox_intext()  'button->修改

                history()
                history1()

            End If
            If dr("簽核狀態") = "簽核通過" Then
                Table2.Visible = True
             
                Button4.Visible = False
                Button5.Visible = False
            End If

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            If (CType(e.CommandSource, Button)).CommandName = "detail" Then
                Session("異常編號") = e.CommandArgument.ToString()
                review_detail()
                BindData()

            End If
            If (CType(e.CommandSource, Button)).CommandName = "check" Then
                Session("異常編號") = e.CommandArgument.ToString()
                review_check()
                BindData()
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub review_detail()
        Try
            Dim sql As String = "update Exception_view set 簽核狀態='簽核中' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' and 簽核狀態<>'簽核通過'"
            ds = Db.Get_DataSet(sql)

            btn_select_Click(Me, Nothing)
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub review_check()
        Try
            Dim sql1 As String = "select 異常編號,簽核狀態 from Exception_view where 簽核狀態='簽核通過' and 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds1 = Db.Get_DataSet(sql1)
            If ds1.Tables(0).Rows.Count = 0 Then

                Dim sql As String = "update Exception_view set 簽核狀態='簽核通過' ,judgment='1'  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                ds = Db.Get_DataSet(sql)
              
                send_all()
                btn_select_Click(Me, Nothing)
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub send_all()
        Try
            Dim sql As String = "select 異常對象,異常種類,異常編號 from Exception_view where 異常編號 ='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            Dim number As String = dr("異常編號")
            Dim company As String = dr("異常對象") + dr("異常種類") + "-已簽核通過"

            Dim sql1 As String = "select writer ,Email  from Exception_process left join Staff on writer =Code  where notice_all ='1' "
            ds1 = Db.Get_DataSet(sql1)

            Dim user_all As String = ""
            If ds1.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds1.Tables(0).Rows.Count() - 1
                    Dim dr1 As DataRow = ds1.Tables(0).Rows(i)
                    Dim user_mail1 As String = dr1("Email").ToString()
                    user_all = user_all & dr1("Email") & ","
                Next
            End If
            Dim user_all1 As String = user_all.Substring(0, user_all.Length() - 1)

            mail.send_mail(user_all1, company, number)

        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button12_Click(sender As Object, e As EventArgs)
        Dim a As String = TextBox7.Text.ToString.Trim()
        Dim sql1 As String = "select * from Exception_view where 異常編號 like '%" & a.ToString() & "' or 異常編號 like '%" & a.ToString() & "'"
        ds1 = Db.Get_DataSet(sql1)
        If ds1.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds1.Tables(0).Rows.Count - 1
                Dim dr As DataRow = ds1.Tables(0).Rows(i)
                Dim b As String = dr("異常編號")
                Dim sql2 As String = "update Exception_view set judgment ='1' where 異常編號 ='" & b.ToString.Trim() & "'"
                ds2 = Db.Get_DataSet(sql2)
            Next
            Label20.Text = "變更完成"
        End If
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Session("Exception") = ""
        Response.Redirect("Default.aspx")
    End Sub
    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        btn_select_Click(Me, Nothing)
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
    Private Sub SQL_time()
        Try
            Dim sql_time As String = "select getdate() AS SERVER_TIME"
            ds1 = Db.Get_DataSet(sql_time)
            time = ds1.Tables(0).Rows(0)("SERVER_TIME").ToString()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub textbox_intext()
        Try
            Dim sql As String = "select 異常調查原因,再發防止 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)


            TextBox2.Text = dr("異常調查原因").ToString()

            TextBox3.Text = dr("再發防止").ToString()
        
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) '異常調查原因
        Try
            Dim sql As String = "select 異常調查原因 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                If dr("異常調查原因").ToString() <> TextBox2.Text.ToString() Then
                    Dim sql1 As String = "update Exception_view set 異常調查原因='" & TextBox2.Text.ToString() & "'  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds1 = Db.Get_DataSet(sql1)
                    SQL_time()
                    Dim sql2 As String = "insert into Exception_history (異常編號,user_name,修改人員編號,修改時間, 修改後內容,欄位)values('" & Session("異常編號").ToString.Trim() & "','" & Session("user_name").ToString.Trim() & "','" & Session("user_id").ToString.Trim() & "','" & time.ToString("yyyy-MM-dd HH:mm:ss") & "','" & TextBox2.Text.ToString.Trim() & "','異常調查原因')"
                    ds2 = Db.Get_DataSet(sql2)
                    If TextBox2.Text.ToString() <> "" And TextBox3.Text.ToString() <> "" Then
                        Dim sql4 As String = "update Exception_view set 簽核狀態='尚未簽核'  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                        ds3 = Db.Get_DataSet(sql4)
                    End If
                Else
                    BindData()
                End If
                Dim sql3 As String = "select user_name AS 修改者,修改時間 from Exception_history where 欄位='異常調查原因' and  異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' ORDER BY 修改時間 DESC"
                ds1 = Db.Get_DataSet(sql3)
                If ds1.Tables(0).Rows.Count > 0 Then
                    GridView4.DataSource = ds1.Tables(0)
                    GridView4.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) '再發防止
        Try
            Dim sql As String = "select 再發防止 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                If dr("再發防止").ToString() <> TextBox3.Text.ToString() Then
                    Dim sql1 As String = "update Exception_view set 再發防止='" & TextBox3.Text.ToString() & "'where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds1 = Db.Get_DataSet(sql1)
                    SQL_time()
                    Dim sql2 As String = "insert into Exception_history (異常編號 ,user_name,修改人員編號 ,修改時間 , 修改後內容 ,欄位)values('" & Session("異常編號").ToString().ToString.Trim() & "','" & Session("user_name") & "','" & Session("user_id") & "', '" & time.ToString("yyyy-MM-dd HH:mm:ss") & "','" & TextBox2.Text.ToString() & "','再發防止')"
                    ds2 = Db.Get_DataSet(sql2)
                    If TextBox2.Text.ToString() <> "" And TextBox3.Text.ToString() <> "" Then
                        Dim sql4 As String = "update Exception_view set 簽核狀態='尚未簽核'  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                        ds3 = Db.Get_DataSet(sql4)
                    End If
                Else
                    BindData()
                End If
                Dim sql3 As String = "select user_name AS 修改者,修改時間 from Exception_history where 欄位='再發防止' and  異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' ORDER BY 修改時間 DESC"
                ds1 = Db.Get_DataSet(sql3)
                If ds1.Tables(0).Rows.Count > 0 Then
                    GridView3.DataSource = ds1.Tables(0)
                    GridView3.DataBind()
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub history()
        Try
            Dim sql As String = "select user_name AS 修改者,修改時間 from Exception_history where 欄位='異常調查原因' and  異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' ORDER BY 修改時間 DESC"
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                GridView4.DataSource = ds.Tables(0)
                GridView4.DataBind()
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub history1()
        Try
            Dim sql As String = "select user_name AS 修改者,修改時間 from Exception_history where 欄位='再發防止' and  異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' ORDER BY 修改時間 DESC"
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                GridView3.DataSource = ds.Tables(0)
                GridView3.DataBind()
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub GridView4_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            GridView4.PageIndex = e.NewPageIndex
            BindData()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub GridView3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            GridView3.PageIndex = e.NewPageIndex
            BindData()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs)
        Try
            Panel7.Visible = True
            Button11.Visible = True
            Button4.Visible = False
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '駁回(異常)
    Protected Sub Button5_Click(sender As Object, e As EventArgs)
       Try
            Panel8.Visible = True
            Button10.Visible = True
            Button5.Visible = False
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '駁回(再發)
    Protected Sub Button6_Click(sender As Object, e As EventArgs)
        Try
            Dim sql2 As String = "update Exception_view set modify_reason1='" & TextBox4.Text.ToString.Trim() & "', modify1='1' ,簽核狀態='修改中' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds2 = Db.Get_DataSet(sql2)

            Dim sql As String = "select 異常對象,異常種類,異常編號 from Exception_view where 異常編號 ='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            Dim number As String = dr("異常編號")
            Dim company As String = dr("異常對象") + dr("異常種類") + "-異常調查原因請重寫!"

            Dim sql1 As String = "select  EL.user_id,EL.user_name ,user_mail,MAX(Eh.修改時間) AS 最後修改時間  from Exception_Login EL INNER JOIN Exception_history Eh on EL.user_id =Eh.修改人員編號 where 異常編號 ='" & Session("異常編號").ToString().ToString.Trim() & "' and 欄位='異常調查原因' Group by 異常編號,欄位,user_id,EL.user_name,user_mail order by 最後修改時間 DESC"
            ds1 = Db.Get_DataSet(sql1)
            Dim dr1 As DataRow = ds1.Tables(0).Rows(0)
            Dim user_mail As String = dr1("user_mail")
            Dim user_name As String = dr1("user_name")
            mail.send_mail(user_mail, company, number)
            Button4.Visible = False
            Button11.Visible = False
            Label2.Visible = True
            Panel7.Visible = False
            Dim sql3 As String = "update Exception_view set  status='" & user_name & "' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds3 = Db.Get_DataSet(sql3)
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button7_Click(sender As Object, e As EventArgs)
        Try
            Dim sql2 As String = "update Exception_view set modify_reason2='" & TextBox6.Text.ToString.Trim() & "', modify2='1' ,簽核狀態='修改中' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds2 = Db.Get_DataSet(sql2)

            Dim sql As String = "select 異常對象,異常種類,異常編號 from Exception_view where 異常編號 ='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            Dim number As String = dr("異常編號")
            Dim company As String = dr("異常對象") + dr("異常種類") + "-再發防止請重寫!"

            Dim sql1 As String = "select  EL.user_id,EL.user_name ,user_mail,MAX(Eh.修改時間) AS 最後修改時間  from Exception_Login EL INNER JOIN Exception_history Eh on EL.user_id =Eh.修改人員編號 where 異常編號 ='" & Session("異常編號").ToString().ToString.Trim() & "' and 欄位='再發防止' Group by 異常編號,欄位,user_id,EL.user_name,user_mail order by 最後修改時間 DESC"
            ds1 = Db.Get_DataSet(sql1)
            Dim dr1 As DataRow = ds1.Tables(0).Rows(0)
            Dim user_mail As String = dr1("user_mail")
            Dim user_name As String = dr1("user_name")
            mail.send_mail(user_mail, company, number)
            Button5.Visible = False
            Button10.Visible = False
            Label14.Visible = True
            Panel8.Visible = False
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub Button10_Click(sender As Object, e As EventArgs)
        Try
            Panel8.Visible = False
            Button10.Visible = False
            Button5.Visible = True
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '取消駁回(再發)
    Protected Sub Button11_Click(sender As Object, e As EventArgs)
        Try
            Panel7.Visible = False
            Button11.Visible = False
            Button4.Visible = True
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '取消駁回(異常)
    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim btn_check As Button = e.Row.FindControl("check")
            btn_check.Attributes.Add("onclick", "return confirm('是否簽核通過，並發信給所有人?')")
        End If
    End Sub
End Class