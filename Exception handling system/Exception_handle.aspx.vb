Public Class Exception_handle
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim ds1 As DataSet
    Dim ds2 As DataSet
    Dim ds3 As DataSet
    Dim ds4 As DataSet
    Dim ds5 As DataSet
    Dim page_id As String = ""
    Dim time As DateTime
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Db.Set_Db("172.30.4.225", "DNTW_Warehouse")
            page_id = If((Request.QueryString("id") Is Nothing), "", Request.QueryString("id"))
            Session("Exception") = "http://172.30.4.225/Exception_handle.aspx?id=" + page_id
            Dim a As String = Session("Exception")

            If Session("user_id") = "" Then
                Response.Redirect("Default.aspx")
            End If

            '新增/更新
            Button1.Attributes.Add("onclick", "return confirm('是否要更新，並發送給主管?')")
            Button2.Attributes.Add("onclick", "return confirm('是否要更新，並發送給主管?')")
            Button3.Attributes.Add("onclick", "return confirm('是否要送出，並發送給課長簽核?')")
            Button8.Attributes.Add("onclick", "return confirm('責任部屬送出後不可再更改，是否要新增內容並發送給責任部屬?')")
            Button9.Attributes.Add("onclick", "return confirm('是否要送出，並發送給主管?')")

            '駁回
            Button4.Attributes.Add("onclick", "return confirm('請於下方表格中填寫駁回原因')")
            Button5.Attributes.Add("onclick", "return confirm('請於下方表格中填寫駁回原因')")
            Button6.Attributes.Add("onclick", "return confirm('是否要發回給上一位填寫者重寫?')")
            Button7.Attributes.Add("onclick", "return confirm('是否要發回給上一位填寫者重寫?')")

            '權限
            user_name1()
            If Not Page.IsPostBack Then
                If Session("異常編號") = "" And page_id = "" Then
                    Panel1.Visible = False '填寫表單
                    Panel4.Visible = True '請先選擇要查詢的資料
                Else
                    Panel1.Visible = True '填寫表單
                    Panel4.Visible = False '請先選擇要查詢的資料
                End If
                DDL()
                BindData()
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub DDL()
        Try '是生管課及物流課的，股長級以下的人員
            Dim sql5 As String = "select user_id,user_name,user_mail from Exception_Login  where( user_dept ='生管課' or user_dept ='物流課' ) and (user_position <>'部長' and user_position <>'課長' and user_position <>'股長' and user_position <>'組長')"
            ds5 = Db.Get_DataSet(sql5)
            If ds5.Tables(0).Rows.Count > 0 Then
                DropDownList1.Items.Add("")
                For i As Integer = 0 To ds5.Tables(0).Rows.Count - 1
                    Dim dr5 As DataRow = ds5.Tables(0).Rows(i)
                    DropDownList1.Items.Add(dr5("user_name"))
                Next
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '責任部屬人員名單
    Private Sub user_name1() '權限
        Try
            Dim sql1 As String = "select user_id ,user_name ,user_dept ,user_group,user_position,user_mail  from Exception_Login  where user_id = '" & Session("user_id").Trim() & "'"
            ds1 = Db.Get_DataSet(sql1)
            Dim dr1 As DataRow = ds1.Tables(0).Rows(0)
            If ds1.Tables(0).Rows.Count > 0 Then
                user_name.Text = dr1("user_name").ToString()
                Session("user_name") = dr1("user_name").ToString()
                Session("user_id") = dr1("user_id").ToString()
            Else
                Response.Redirect("Home_page.aspx")
            End If

            If dr1("user_group") = "承認者" Then
                ImageButton4.Visible = True '簽核頁面
            Else
                ImageButton4.Visible = False
            End If
           
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub BindData()
        Try
            Dim sql As String = "select  異常編號, 異常日期 ,異常種類,異常對象 ,計畫時間 ,實際時間 , CONVERT(varchar(20),(異常調查原因)) AS 異常調查原因,CONVERT(varchar(20),(再發防止)) AS 再發防止,CONVERT(varchar(20),(責任部屬)) AS 責任部屬 ,簽核狀態 ,受入場,send_mail,status,modify1,modify2,modify_reason1,modify_reason2 from Exception_view  " &
                                 "where 1=1 and "
            If page_id = "" Then
                sql += "異常編號='" & Session("異常編號").ToString().Trim() & "'"
            Else
                sql += "異常編號='" & page_id.ToString().Trim() & "'"
            End If
            sql += "ORDER BY 計畫時間 ASC"

            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            If ds.Tables(0).Rows.Count > 0 Then
                ds.Tables(0).Columns.Add(New DataColumn("no", GetType(String)))

                For k As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim dr1 As DataRow = ds.Tables(0).Rows(k)
                    dr1("no") = k + 1
                    Session("異常編號") = dr1("異常編號")
                Next
                GridView1.DataSource = ds.Tables(0)
                GridView1.DataBind()

                '下方框
                textbox_intext() '依據簽核狀態顯示的物件

                history()  '歷史紀錄
                history1() '歷史紀錄           
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
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
            Dim sql As String = "select 異常調查原因,再發防止,責任部屬,簽核狀態,status,modify1,modify2,modify_reason1,modify_reason2 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            Dim sql1 As String = "select user_id ,user_name,user_group from Exception_Login where user_id = '" & Session("user_id").Trim() & "'"
            ds1 = Db.Get_DataSet(sql1)
            Dim dr1 As DataRow = ds1.Tables(0).Rows(0)

            Label15.Text = dr("簽核狀態").ToString.Trim()


            If dr("簽核狀態") = "簽核通過" Then
                Button1.Visible = False
                Button2.Visible = False
                Button3.Visible = False
                Button4.Visible = False
                Button5.Visible = False
                Button8.Visible = False
                Button9.Visible = False
                Label6.Visible = True
                Label7.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label11.Text = dr("責任部屬").ToString()
                Label11.Visible = True
                Label13.Visible = False
                DropDownList1.Visible = False
                Panel5.Visible = False
                Panel6.Visible = False
                TextBox2.Text = dr("異常調查原因").ToString()
                TextBox3.Text = dr("再發防止").ToString()

            ElseIf dr("簽核狀態") = "尚未簽核" Or dr("簽核狀態") = "簽核中" Then
                Button1.Visible = False
                Button2.Visible = False
                Button3.Visible = False
                Button4.Visible = False
                Button5.Visible = False
                Button8.Visible = False
                Button9.Visible = False
                Label6.Visible = True
                Label7.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label11.Text = dr("責任部屬").ToString()
                Label11.Visible = True
                Label13.Visible = False
                DropDownList1.Visible = False
                Panel5.Visible = False
                Panel6.Visible = False
                TextBox2.Text = dr("異常調查原因").ToString()
                TextBox3.Text = dr("再發防止").ToString()

            ElseIf dr("簽核狀態") = "修改中" Then
                Button1.Visible = True
                Button2.Visible = True
                Button3.Visible = False
                Button4.Visible = False
                Button5.Visible = False
                Button8.Visible = False
                Button9.Visible = False
                Label6.Visible = True
                Label7.Visible = False
                Label8.Visible = False
                Label9.Visible = False
                Label11.Text = dr("責任部屬").ToString()
                Label11.Visible = True
                Label13.Visible = False
                DropDownList1.Visible = False
                TextBox2.Text = dr("異常調查原因").ToString()
                TextBox3.Text = dr("再發防止").ToString()


                If dr("modify1") = 1 Then '有需修改
                    Panel5.Visible = True
                    Button6.Visible = False
                    TextBox4.Text = dr("modify_reason1")
                    Button4.Visible = False
                    Button11.Visible = False
                    Label7.Visible = True
                Else
                    If dr1("user_group") = "承認者" Or dr1("user_group") = "檢討者" Then
                        Button4.Visible = True
                    End If
                    Panel5.Visible = False
                End If

                If dr("modify2") = 1 Then
                    Panel6.Visible = True
                    Button7.Visible = False
                    TextBox6.Text = dr("modify_reason2")
                    Button5.Visible = False
                    Button10.Visible = False
                    Label8.Visible = True
                Else
                    If dr1("user_group") = "承認者" Or dr1("user_group") = "檢討者" Then
                        Button5.Visible = True
                    End If
                    Panel6.Visible = False
                End If

                If (dr("異常調查原因") <> "" And dr("再發防止") <> "") And (dr1("user_group") = "承認者" Or dr1("user_group") = "檢討者") Then
                    Button3.Visible = True
                End If

            ElseIf dr("簽核狀態") = "填寫中" Then
                Dim ds1 As DataSet
                Dim sql3 As String = "select * from Exception_history  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' and 欄位='再發防止'"
                ds1 = Db.Get_DataSet(sql3)


                If ds1.Tables(0).Rows.Count > 0 Then
                    Button2.Visible = True  '修改
                    Button9.Visible = False '新增
                    TextBox3.Text = dr("再發防止").ToString()

                    If dr1("user_group") = "承認者" Or dr1("user_group") = "檢討者" Then
                        Button5.Visible = True
                        Button10.Visible = False
                        Label8.Visible = False
                    End If
                ElseIf dr("再發防止") = "" Then
                    Button2.Visible = False '修改
                    Button9.Visible = True  '新增
                    TextBox3.Text = dr("再發防止").ToString()
                End If

                If dr("責任部屬") = "" Then
                    Button1.Visible = False '修改
                    Button2.Visible = False
                    Button3.Visible = False
                    Button4.Visible = False
                    Button5.Visible = False
                    Button8.Visible = True  '新增
                    Button9.Visible = False
                    Button10.Visible = False
                    Button11.Visible = False
                    Label6.Visible = True
                    Label7.Visible = False
                    Label8.Visible = False
                    Label9.Visible = False
                    Label11.Visible = False
                    DropDownList1.Visible = True
                    Panel5.Visible = False
                    Panel6.Visible = False
                ElseIf dr("責任部屬") <> "" Then
                    Button1.Visible = True '修改
                    Button8.Visible = False '新增
                    Label6.Visible = True
                    Label9.Visible = False
                    DropDownList1.Visible = False
                    Label11.Text = dr("責任部屬").ToString()
                    Label11.Visible = True
                    TextBox2.Text = dr("異常調查原因").ToString()
                    If dr1("user_group") = "承認者" Or dr1("user_group") = "檢討者" Then
                        Button4.Visible = True
                        Button11.Visible = False
                        Label7.Visible = False
                    End If
                End If

                If (dr("異常調查原因") <> "" And ds1.Tables(0).Rows.Count) And (dr1("user_group") = "承認者" Or dr1("user_group") = "檢討者") Then
                    Button3.Visible = True
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '起始樣版
    Private Sub set_mail1()
        Try
            Dim sql As String = "select user_id,user_name,user_mail,EV.責任部屬  from Exception_Login EL inner join Exception_view EV  on EL.user_name = EV.責任部屬  where user_name='" & Label11.Text.ToString.Trim() & "' and 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                Dim user_mail As String = dr("user_mail")
                Dim sql1 As String = "select 異常對象,異常種類,異常編號 from Exception_view where 異常編號 ='" & Session("異常編號").ToString().ToString.Trim() & "'"
                ds1 = Db.Get_DataSet(sql1)
                If ds1.Tables(0).Rows.Count > 0 Then
                    Dim dr1 As DataRow = ds1.Tables(0).Rows(0)
                    Dim number As String = dr1("異常編號")
                    Dim company As String = Session("user_name") + "對於" + dr1("異常對象") + dr1("異常種類") + "-異常調查原因已填寫"
                    mail.send_mail(user_mail, company, number)

                    Dim status As String = dr("責任部屬")
                    Dim sql3 As String = "update Exception_view set  status='" & status & "' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds3 = Db.Get_DataSet(sql3)
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub  '寄信(給責任部屬)
    Private Sub set_mail2()
        Try
            '查詢傳給誰
            Dim sql As String = "select writer,reviewer ,notice,notice_all ,user_name,user_mail,user_group  from Exception_process left join Exception_Login on Exception_process.writer=Exception_Login.user_id where user_id='" & Session("user_id").ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                Dim reviewer As String = dr("reviewer")
                Dim company As String = ""
                Dim number As String = ""
                Dim sql1 As String = "select 異常對象,異常種類,異常編號 from Exception_view where 異常編號 ='" & Session("異常編號").ToString().ToString.Trim() & "'"
                ds1 = Db.Get_DataSet(sql1)
                If ds1.Tables(0).Rows.Count > 0 Then
                    Dim dr1 As DataRow = ds1.Tables(0).Rows(0)
                    number = dr1("異常編號")

                    If Session("button").ToString() = "異常修改" Then  'button1
                        company = dr("user_name") + "對於" + dr1("異常對象") + dr1("異常種類") + "-異常調查原因已修改"

                    ElseIf Session("button").ToString() = "再發修改" Then 'button2
                        company = dr("user_name") + "對於" + dr1("異常對象") + dr1("異常種類") + "-再發防止已修改"

                    ElseIf Session("button").ToString() = "再發新增" Then 'button9
                        company = dr("user_name") + "對於" + dr1("異常對象") + dr1("異常種類") + "-再發防止已填寫"

                    ElseIf Session("button").ToString() = "送出簽核" Then 'button3
                        company = dr("user_name") + "對於" + dr1("異常對象") + dr1("異常種類") + "的問題已全部確認完畢，請簽核"

                    End If

                    '查詢被傳送者的MAIL
                    Dim sql2 As String = "select user_name,user_mail,user_group  from Exception_process left join Exception_Login on Exception_process.writer=Exception_Login.user_id where user_id='" & dr("reviewer").ToString.Trim() & "'"
                    ds2 = Db.Get_DataSet(sql2)
                    Dim send_to As String = ""
                    Dim status As String = ""
                    If ds2.Tables(0).Rows.Count > 0 Then
                        Dim dr2 As DataRow = ds2.Tables(0).Rows(0)
                        Dim user_mail As String = dr2("user_mail").ToString()
                        send_to = dr2("user_mail")
                        status = dr2("user_name")

                        If Session("button").ToString() = "異常修改" Or Session("button").ToString() = "再發修改" Or Session("button").ToString() = "再發新增" Then
                            mail.send_mail(send_to, company, number)
                        End If
                        If Session("button").ToString() = "送出簽核" Then
                            mail.send_mail2(send_to, company, number)
                            Label13.Text = "已傳送給" + "" & dr2("user_name") & ""
                        End If
                    End If

                    Dim sql3 As String = "update Exception_view set  status='" & status & "' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds3 = Db.Get_DataSet(sql3)
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub  '寄信(修改後->檢討者)
    Protected Sub Button1_Click(sender As Object, e As EventArgs) '異常調查原因
        Try
            Dim sql As String = "select 異常調查原因 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            If ds.Tables(0).Rows.Count > 0 Then
                If dr("異常調查原因").ToString() <> TextBox2.Text.ToString() Then
                    Dim sql1 As String = "update Exception_view set 異常調查原因='" & TextBox2.Text.ToString() & "' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds1 = Db.Get_DataSet(sql1)
                    SQL_time()
                    Dim sql2 As String = "insert into Exception_history (異常編號,user_name,修改人員編號,修改時間, 修改後內容,欄位)values('" & Session("異常編號").ToString.Trim() & "','" & Session("user_name").ToString.Trim() & "','" & Session("user_id").ToString.Trim() & "','" & time.ToString("yyyy-MM-dd HH:mm:ss") & "','" & TextBox2.Text.ToString.Trim() & "','異常調查原因')"
                    ds2 = Db.Get_DataSet(sql2)
                    Session("button") = "異常修改"
                    set_mail2()

                    Dim sql3 As String = "update Exception_view set modify1='0' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds3 = Db.Get_DataSet(sql3)
                    history()
                    BindData()
                    Button1.Visible = True '修改
                    'Button2.Visible = False '修改
                    'Button3.Visible = False '送出簽核
                    'Button4.Visible = False '駁回
                    'Button5.Visible = False '駁回
                    'Button8.Visible = False '新增
                    'Button9.Visible = False '新增
                    'Label7.Visible = False '已通知重寫
                    'Label8.Visible = False '已通知重寫
                    Label9.Visible = False '請選擇責任部屬
                    Label11.Visible = True '責任部屬
                    DropDownList1.Visible = False
                    'Panel5.Visible = False '駁回原因
                    'Panel6.Visible = False '駁回原因
                Else
                    BindData()
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '修改(異常)
    Protected Sub Button2_Click(sender As Object, e As EventArgs) '再發防止
        Try
            Dim sql As String = "select 再發防止 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            If ds.Tables(0).Rows.Count > 0 Then
                If dr("再發防止").ToString() <> TextBox3.Text.ToString() Then
                    Dim sql1 As String = "update Exception_view set 再發防止='" & TextBox3.Text.ToString() & "' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds1 = Db.Get_DataSet(sql1)
                    SQL_time()
                    Dim sql2 As String = "insert into Exception_history (異常編號,user_name,修改人員編號,修改時間, 修改後內容,欄位)values('" & Session("異常編號").ToString.Trim() & "','" & Session("user_name").ToString.Trim() & "','" & Session("user_id").ToString.Trim() & "','" & time.ToString("yyyy-MM-dd HH:mm:ss") & "','" & TextBox3.Text.ToString.Trim() & "','再發防止')"
                    ds2 = Db.Get_DataSet(sql2)

                    Session("button") = "再發修改"
                    set_mail2()
                    Dim sql3 As String = "update Exception_view set modify2='0' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds3 = Db.Get_DataSet(sql3)

                    history1()
                    BindData()

                    'Button1.Visible = True '修改
                    Button2.Visible = True '修改
                    'Button3.Visible = False '送出簽核
                    'Button4.Visible = False '駁回
                    'Button5.Visible = False '駁回
                    'Button8.Visible = False '新增
                    'Button9.Visible = False '新增
                    'Label7.Visible = False '已通知重寫
                    'Label8.Visible = False '已通知重寫
                    'Label9.Visible = False '請選擇責任部屬
                    Label11.Visible = True '責任部屬
                    'DropDownList1.Visible = False
                    'Panel5.Visible = False '駁回原因
                    ' Panel6.Visible = False '駁回原因
                Else
                    BindData()
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '修改(再發)
    Protected Sub Button3_Click(sender As Object, e As EventArgs)
        Try
            Dim ds1 As DataSet
            Dim sql3 As String = "select * from Exception_history  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' and 欄位='再發防止'"
            ds1 = Db.Get_DataSet(sql3)

            If TextBox2.Text.ToString() <> "" And ds1.Tables(0).Rows.Count > 0 Then
                Dim sql As String = "update Exception_view set 簽核狀態='尚未簽核'  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                ds = Db.Get_DataSet(sql)
                Session("button") = "送出簽核"
                set_mail2() '4->2

                Button1.Visible = False '修改
                Button2.Visible = False '修改
                Button3.Visible = False '送出簽核
                Button4.Visible = False '駁回
                Button5.Visible = False '駁回
                Button8.Visible = False '新增
                Button9.Visible = False '新增
                Label7.Visible = False '已通知重寫
                Label8.Visible = False '已通知重寫
                Label9.Visible = False '請選擇責任部屬
                Label11.Visible = True '責任部屬
                Label13.Visible = True
                DropDownList1.Visible = False
                Panel5.Visible = False '駁回原因
                Panel6.Visible = False '駁回原因
                BindData()
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '送出簽核
    Protected Sub Button4_Click(sender As Object, e As EventArgs)
        Try
            Panel5.Visible = True
            Dim sql As String = "select modify_reason1 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            TextBox4.Text = dr("modify_reason1")
            Button4.Visible = False
            Button11.Visible = True
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '駁回(異常)
    Protected Sub Button5_Click(sender As Object, e As EventArgs)
        Try
            Panel6.Visible = True
            Dim sql As String = "select modify_reason2 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            TextBox6.Text = dr("modify_reason2")
            Button5.Visible = False
            Button10.Visible = True
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
            Label7.Visible = True
            Panel5.Visible = False
            Dim sql3 As String = "update Exception_view set  status='" & user_name & "' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds3 = Db.Get_DataSet(sql3)
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '送出(異常駁回原因)
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
            Label8.Visible = True
            Panel6.Visible = False

            Dim sql3 As String = "update Exception_view set  status='" & user_name & "' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds3 = Db.Get_DataSet(sql3)
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '送出(再發駁回原因)
    Protected Sub Button8_Click(sender As Object, e As EventArgs)
        Try
            If DropDownList1.Text.ToString.Trim() = "" Then
                Label9.Visible = True
            Else
                Label9.Visible = False

                Dim sql As String = "select 異常調查原因 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                ds = Db.Get_DataSet(sql)
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                If ds.Tables(0).Rows.Count > 0 Then
                    If dr("異常調查原因").ToString() <> TextBox2.Text.ToString() Then
                        Dim sql1 As String = "update Exception_view set 異常調查原因='" & TextBox2.Text.ToString() & "'  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                        ds1 = Db.Get_DataSet(sql1)
                        SQL_time()
                        Dim sql2 As String = "insert into Exception_history (異常編號,user_name,修改人員編號,修改時間, 修改後內容,欄位)values('" & Session("異常編號").ToString.Trim() & "','" & Session("user_name").ToString.Trim() & "','" & Session("user_id").ToString.Trim() & "','" & time.ToString("yyyy-MM-dd HH:mm:ss") & "','" & TextBox2.Text.ToString.Trim() & "','異常調查原因')"
                        ds2 = Db.Get_DataSet(sql2)

                        Dim sql3 As String = "select 責任部屬 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                        ds3 = Db.Get_DataSet(sql3)
                        If ds3.Tables(0).Rows.Count > 0 Then
                            Dim dr1 As DataRow = ds3.Tables(0).Rows(0)
                            If dr1("責任部屬") = "" Then
                                Dim sql4 As String = "update Exception_view set 責任部屬='" & DropDownList1.Text.ToString.Trim() & "',status='" & DropDownList1.Text.ToString.Trim() & "' where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                                ds4 = Db.Get_DataSet(sql4)
                                DropDownList1.Visible = False
                                Label11.Text = "" & DropDownList1.Text.ToString.Trim() & ""
                                Label11.Visible = True
                                set_mail1()

                            ElseIf dr1("責任部屬") <> "" Then
                                DropDownList1.Visible = False
                                Label11.Text = dr1("責任部屬")
                                Label11.Visible = True
                                Button1.Visible = True '修改
                                'Button2.Visible = False '修改
                                'Button3.Visible = False '送出簽核
                                'Button4.Visible = False '駁回
                                'Button5.Visible = False '駁回
                                Button8.Visible = False '新增
                                'Button9.Visible = False '新增
                                'Label7.Visible = False '已通知重寫
                                'Label8.Visible = False '已通知重寫
                                'Label9.Visible = False '請選擇責任部屬
                                'Label11.Visible = True '責任部屬
                                'DropDownList1.Visible = False
                                Panel5.Visible = False '駁回原因
                                Panel6.Visible = False '駁回原因
                            Else
                                BindData()
                            End If
                        Else
                            BindData()
                        End If
                    Else
                        BindData()
                    End If
                    history()
                    BindData()
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '新增(異常)
    Protected Sub Button9_Click(sender As Object, e As EventArgs)
        Try
            Dim sql As String = "select 再發防止 from Exception_view where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
            ds = Db.Get_DataSet(sql)
            Dim dr As DataRow = ds.Tables(0).Rows(0)
            If ds.Tables(0).Rows.Count > 0 Then
                If dr("再發防止").ToString() <> TextBox2.Text.ToString() Then
                    Dim sql1 As String = "update Exception_view set 再發防止='" & TextBox3.Text.ToString() & "'  where 異常編號='" & Session("異常編號").ToString().ToString.Trim() & "'"
                    ds1 = Db.Get_DataSet(sql1)
                    SQL_time()
                    Dim sql2 As String = "insert into Exception_history (異常編號,user_name,修改人員編號,修改時間, 修改後內容,欄位)values('" & Session("異常編號").ToString.Trim() & "','" & Session("user_name").ToString.Trim() & "','" & Session("user_id").ToString.Trim() & "','" & time.ToString("yyyy-MM-dd HH:mm:ss") & "','" & TextBox3.Text.ToString.Trim() & "','再發防止')"
                    ds2 = Db.Get_DataSet(sql2)

                    Session("button") = "再發新增"
                    set_mail2()
                Else
                    BindData()
                End If
                history1()
                BindData()

                'Button1.Visible = True '修改
                Button2.Visible = True '修改
                'Button3.Visible = False '送出簽核
                'Button4.Visible = False '駁回
                'Button5.Visible = False '駁回
                'Button8.Visible = False '新增
                'Button9.Visible = False '新增
                'Label7.Visible = False '已通知重寫
                'Label8.Visible = False '已通知重寫
                'Label9.Visible = False '請選擇責任部屬
                'Label11.Visible = True '責任部屬
                'DropDownList1.Visible = False
                Panel5.Visible = False '駁回原因
                Panel6.Visible = False '駁回原因
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '新增(再發)
    Protected Sub Button10_Click(sender As Object, e As EventArgs)
        Try
            Panel6.Visible = False
            Button5.Visible = True
            Button10.Visible = False
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '再發取消駁回
    Protected Sub Button11_Click(sender As Object, e As EventArgs)
        Try
            Panel5.Visible = False
            Button4.Visible = True
            Button11.Visible = False
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub '異常取消駁回
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
    Private Sub history()
        Try
            Dim sql As String = "select user_name AS 修改者, CONVERT(varchar(19), 修改時間, 20) AS 修改時間 from Exception_history where 欄位='異常調查原因' and  異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' ORDER BY 修改時間 DESC"
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
            Dim sql As String = "select user_name AS 修改者, CONVERT(varchar(19), 修改時間, 20) AS 修改時間 from Exception_history where 欄位='再發防止' and  異常編號='" & Session("異常編號").ToString().ToString.Trim() & "' ORDER BY 修改時間 DESC"
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                GridView3.DataSource = ds.Tables(0)
                GridView3.DataBind()
            End If
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
    Protected Sub GridView4_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            GridView4.PageIndex = e.NewPageIndex
            BindData()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
End Class