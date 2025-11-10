Imports System.Drawing

Public Class Home_page
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim ds1 As DataSet
    Dim ds2 As DataSet
    Dim ds3 As DataSet

    Dim Dateline As DateTime
    Dim LastArrivalTime As DateTime '最後到貨時間
    Dim BoxCount As Integer '箱數
    Dim RealTime As String '實際時間
    Dim RealCheck As Integer '檢品

    Dim time As DateTime  'SQL系統時間

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Db.Set_Db("172.30.4.225", "DNTW_Warehouse")
            Session("Exception") = "Home_page.aspx"
            If Session("user_id") = "" Then
                Response.Redirect("Default.aspx")
            End If

            If Not Page.IsPostBack Then
                DDL()
                user_name1()
            End If
            SQL_time()
            'after_4oclock()
            'dataview()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub DDL()  '顯示器的下拉選單
        Try
            Dim sql As String = "select monitor from Contol_monitor where monitor ='3號受入場' ORDER BY monitor ASC"
            ds1 = Db.Get_DataSet(sql)
            DropDownList1.Items.Clear()
            DropDownList1.Items.Add("請選擇受入場")
            For k As Integer = 0 To ds1.Tables(0).Rows.Count() - 1
                Dim dr As DataRow = ds1.Tables(0).Rows(k)
                If ds1.Tables(0).Rows.Count > 0 Then
                    DropDownList1.Items.Add(dr("monitor"))
                End If
            Next
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
    Private Sub user_name1() '權限
        Try
            user_name.Text = Session("user_name").ToString()
            Dim sql As String = "select user_id,user_dept,user_name,user_position ,user_mail ,user_group  from Exception_Login left join staff on user_id =code  where code = '" & Session("user_id").Trim() & "'"
            ds1 = Db.Get_DataSet(sql)
            If ds1.Tables(0).Rows.Count > 0 Then
                Dim dr As DataRow = ds1.Tables(0).Rows(0)
                If dr("user_group") = "承認者" Then
                    ImageButton1.Visible = True
                    ImageButton2.Visible = True
                    ImageButton3.Visible = True
                    ImageButton4.Visible = True
                    LinkButton6.Visible = True
                    LinkButton7.Visible = False
                ElseIf dr("user_group") = "檢討者" Then
                    ImageButton1.Visible = True
                    ImageButton2.Visible = True
                    ImageButton3.Visible = True
                    ImageButton4.Visible = False
                    LinkButton6.Visible = False
                    LinkButton7.Visible = False
                ElseIf dr("user_group") = "填寫者" Then
                    ImageButton1.Visible = True
                    ImageButton2.Visible = True
                    ImageButton3.Visible = True
                    ImageButton4.Visible = False
                    LinkButton6.Visible = False
                    LinkButton7.Visible = False
                Else
                    ImageButton1.Visible = True
                    ImageButton2.Visible = False
                    ImageButton3.Visible = False
                    ImageButton4.Visible = False
                    LinkButton6.Visible = False
                    LinkButton7.Visible = False
                End If
            Else
                ImageButton1.Visible = True
                ImageButton2.Visible = False
                ImageButton3.Visible = False
                ImageButton4.Visible = False
                LinkButton6.Visible = False
                LinkButton7.Visible = False
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub dataview()
        Try
            Dim date_time As String
            Dim date_time1 As String
            Dim ddl1 As String
            date_time = time.ToString("yyMMdd")
            date_time1 = time.ToString("yyyy/MM/dd")
            ddl1 = DropDownList1.Text()

            Dim sql As String =
"select TEMP2.*,Exception_view .status AS status2,Exception_view .judgment AS judgment2 from (select TEMP1 .*,handle_time,status  AS status1 ,judgment AS judgment1 from ((select * from (select  CASE WHEN LEN(納入時間)=1 then CONVERT(varchar(5),'00:00') WHEN  LEN(納入時間) = 3 THEN CONVERT(varchar(5),'0'+SUBSTRING(納入時間,1,1)+':'+ SUBSTRING(納入時間 ,2,2))WHEN LEN(納入時間)=4 THEN CONVERT(varchar(5),SUBSTRING(納入時間,1,2)+':' + SUBSTRING(納入時間 ,3,2))END  AS 預定時間 ,convert(varchar(16),Contol_V_m.仕入れ先名) AS 供應商,COUNT(Box.[箱数])As 箱數   ,CONVERT (varchar(5),SUBSTRING (着荷,7,2)+':'+SUBSTRING (着荷,9,2)) AS 到貨時間 ,CONVERT (nvarchar,COUNT(Box.[検品]))+ CONVERT (nvarchar, ('/')) + CONVERT(nvarchar,COUNT(Box.[箱数])) AS　點收數,Box.[納入日],CONVERT (varchar (10),'20'+SUBSTRING(納入日,1,2)+'/'+SUBSTRING(納入日,3,2)+'/'+SUBSTRING(納入日,5,2))  As 預定日期,CONVERT (varchar (5),SUBSTRING(納入日,3,2)+'/'+SUBSTRING(納入日,5,2)) As 日期,CONVERT (varchar (19),'20'+SUBSTRING(納入日,1,2)+'-'+SUBSTRING(納入日,3,2)+'-'+SUBSTRING(納入日,5,2))+' '+CASE WHEN LEN(納入時間)=1 then CONVERT(varchar(5),'00:00') WHEN  LEN(納入時間) = 3 THEN CONVERT(varchar(19),'0'+SUBSTRING(納入時間,1,1)+':'+ SUBSTRING(納入時間 ,2,2)+':00')WHEN LEN(納入時間)=4 THEN CONVERT(varchar(19),SUBSTRING(納入時間,1,2)+':' + SUBSTRING(納入時間 ,3,2)+':00')END  AS 預定到貨時間,CONVERT (varchar(19),'20'+SUBSTRING (着荷,1,2)+'-'+SUBSTRING (着荷,3,2)+'-'+SUBSTRING (着荷,5,2)+' '+SUBSTRING (着荷,7,2)+':'+SUBSTRING (着荷,9,2))+':00' AS 實際到貨時間, CASE WHEN LEN(納入時間)=1 then CONVERT(varchar(5),'00:00') WHEN  LEN(納入時間) = 3 THEN CONVERT(varchar(5),'0'+SUBSTRING(納入時間,1,1)+':'+ SUBSTRING(納入時間 ,2,2))WHEN LEN(納入時間)=4 THEN CONVERT(varchar(5),SUBSTRING(納入時間,1,2)+':' + SUBSTRING(納入時間 ,3,2))END  AS 計畫時間,CONVERT (varchar(5),SUBSTRING (着荷,7,2)+':'+SUBSTRING (着荷,9,2)) AS 實際時間,Box.[仕入れ先コード] AS 供應商編號,COUNT(Box.[検品])As 已查收,CONVERT(varchar(19),SUBSTRING(MAX(Box.検品),1,4)+'-'+SUBSTRING(MAX(Box.検品),5,2)+'-'+SUBSTRING(MAX(Box.検品),7,2)+' '+SUBSTRING(MAX(Box.検品),9,2)+':'+SUBSTRING(MAX(Box.検品),11,2)+':'+SUBSTRING(MAX(Box.検品),13,2)) As 最後到貨時間,Contol_V_m.顯示器 AS 受入場 From BOX_Status_Table Box INNER JOIN Contol_V_m ON Box.仕入れ先コード=Contol_V_m.仕入れ先コード group by Box.[納入日],Box.[納入時間],Box.[仕入れ先コード],Contol_V_m.[仕入れ先名],Box.[着荷],Contol_V_m.顯示器) As TEMP " &
"where 預定日期 >= '2025/07/01' and  預定日期 < '" & date_time1.ToString() & "' and 受入場='" & ddl1.ToString() & "' and (實際到貨時間 is null or 箱數<>已查收 or (箱數=已查收 and DATEDIFF(mi,最後到貨時間,getdate())< 10))) AS TEMP1 left join Exception_handle_time on TEMP1.供應商編號 =Exception_handle_time .供應商編號) left join Exception_view  on Exception_view .異常編號 ='00'+TEMP1.預定日期 +TEMP1 .預定時間 +convert(varchar,TEMP1.箱數)+temp1.供應商編號 " &
"where Exception_view .judgment is null)AS TEMP2 left join Exception_view on  Exception_view .異常編號 ='01'+TEMP2.預定日期 +TEMP2 .預定時間 +convert(varchar,TEMP2.箱數)+temp2.供應商編號 where judgment is null " &
"union select TEMP2.*,Exception_view .status AS status2 ,Exception_view .judgment AS judgment2 from (select TEMP.*,handle_time ,status  AS status1 ,judgment AS judgment1 from ((select CASE WHEN LEN(納入時間)=1 then CONVERT(varchar(5),'00:00') WHEN  LEN(納入時間) = 3 THEN CONVERT(varchar(5),'0'+SUBSTRING(納入時間,1,1)+':'+ SUBSTRING(納入時間 ,2,2))WHEN LEN(納入時間)=4 THEN CONVERT(varchar(5),SUBSTRING(納入時間,1,2)+':' + SUBSTRING(納入時間 ,3,2))END  AS 預定時間 ,convert(varchar(16),Contol_V_m.仕入れ先名) AS 供應商,COUNT(Box.[箱数])As 箱數   ,CONVERT (varchar(5),SUBSTRING (着荷,7,2)+':'+SUBSTRING (着荷,9,2)) AS 到貨時間 ,CONVERT (nvarchar,COUNT(Box.[検品]))+ CONVERT (nvarchar, ('/')) + CONVERT(nvarchar,COUNT(Box.[箱数])) AS　點收數,Box.[納入日],CONVERT (varchar (10),'20'+SUBSTRING(納入日,1,2)+'/'+SUBSTRING(納入日,3,2)+'/'+SUBSTRING(納入日,5,2))  As 預定日期,CONVERT (varchar (5),SUBSTRING(納入日,3,2)+'/'+SUBSTRING(納入日,5,2)) As 日期,CONVERT (varchar (19),'20'+SUBSTRING(納入日,1,2)+'-'+SUBSTRING(納入日,3,2)+'-'+SUBSTRING(納入日,5,2))+' '+CASE WHEN LEN(納入時間)=1 then CONVERT(varchar(5),'00:00') WHEN  LEN(納入時間) = 3 THEN CONVERT(varchar(19),'0'+SUBSTRING(納入時間,1,1)+':'+ SUBSTRING(納入時間 ,2,2)+':00')WHEN LEN(納入時間)=4 THEN CONVERT(varchar(19),SUBSTRING(納入時間,1,2)+':' + SUBSTRING(納入時間 ,3,2)+':00')END  AS 預定到貨時間,CONVERT (varchar(19),'20'+SUBSTRING (着荷,1,2)+'-'+SUBSTRING (着荷,3,2)+'-'+SUBSTRING (着荷,5,2)+' '+SUBSTRING (着荷,7,2)+':'+SUBSTRING (着荷,9,2))+':00' AS 實際到貨時間, CASE WHEN LEN(納入時間)=1 then CONVERT(varchar(5),'00:00') WHEN  LEN(納入時間) = 3 THEN CONVERT(varchar(5),'0'+SUBSTRING(納入時間,1,1)+':'+ SUBSTRING(納入時間 ,2,2))WHEN LEN(納入時間)=4 THEN CONVERT(varchar(5),SUBSTRING(納入時間,1,2)+':' + SUBSTRING(納入時間 ,3,2))END  AS 計畫時間,CONVERT (varchar(5),SUBSTRING (着荷,7,2)+':'+SUBSTRING (着荷,9,2)) AS 實際時間,Box.[仕入れ先コード] AS 供應商編號,COUNT(Box.[検品])As 已查收,CONVERT(varchar(19),SUBSTRING(MAX(Box.検品),1,4)+'-'+SUBSTRING(MAX(Box.検品),5,2)+'-'+SUBSTRING(MAX(Box.検品),7,2)+' '+SUBSTRING(MAX(Box.検品),9,2)+':'+SUBSTRING(MAX(Box.検品),11,2)+':'+SUBSTRING(MAX(Box.検品),13,2)) As 最後到貨時間,Contol_V_m.顯示器 AS 受入場 From BOX_Status_Table Box INNER JOIN Contol_V_m ON Box.仕入れ先コード=Contol_V_m.仕入れ先コード " &
"where Box.[納入日] =  '" & date_time.ToString() & "' and Contol_V_m.顯示器='" & ddl1.ToString() & "' group by Box.[納入日],Box.[納入時間],Box.[仕入れ先コード],Contol_V_m.[仕入れ先名],Box.[着荷],Contol_V_m.顯示器 ) AS TEMP  left join Exception_handle_time on TEMP.供應商編號 =Exception_handle_time.供應商編號)  left join Exception_view  on Exception_view .異常編號 ='00'+TEMP.預定日期 +TEMP .預定時間 +convert(varchar,TEMP.箱數)+TEMP.供應商編號  " &
"where Exception_view .judgment is null) AS TEMP2 left join Exception_view on  Exception_view .異常編號 ='01'+TEMP2.預定日期 +TEMP2 .預定時間 +convert(varchar,TEMP2.箱數)+temp2.供應商編號 " &
"order by 預定日期 DESC,預定時間,TEMP2.供應商編號 ASC"


            ds = Db.Get_DataSet(sql)
            ds.Tables(0).Columns.Add(New DataColumn("no", GetType(String)))
            ds.Tables(0).Columns.Add(New DataColumn("作業預定時間", GetType(String)))
            ds.Tables(0).Columns.Add(New DataColumn("作業完成時間", GetType(String)))
            ds.Tables(0).Columns.Add(New DataColumn("到貨異常", GetType(String)))
            ds.Tables(0).Columns.Add(New DataColumn("時間異常", GetType(String)))
            Dim dr As DataRow

            For k As Integer = 0 To ds.Tables(0).Rows.Count() - 1
                dr = ds.Tables(0).Rows(k)
                If Not dr("實際到貨時間") Is DBNull.Value Then
                    Dim time1 As DateTime = time.ToString("yyyy-MM-dd HH:mm:ss")
                    Dim time2 As DateTime = dr("實際到貨時間").ToString()
                    Dim thing As Integer = dr("箱數").ToString()         '箱數
                    Dim thing1 As Integer = (thing * 3 * 1.5) / 60
                    Dim thing_time As Integer = 0
                    Dim handle_time As Integer = dr("handle_time")
                    thing_time = handle_time + ((thing * 3 * 1.5) / 60) '卸貨時間(分)
                    If ((thing * 3 * 1.5) / 60) > thing1 Then
                        thing_time = thing_time + 1
                    End If
                    Dim dateline As DateTime = time2.AddMinutes(thing_time)       '預計應點收完時間
                    Dim dateline1 As String = dateline.ToString("yyyy-MM-dd HH:mm:ss")

                    If dr("箱數") = dr("已查收") Then
                        Dim a As DateTime = dr("最後到貨時間").ToString()
                        dr("作業完成時間") = a.ToString("HH:mm")

                    ElseIf dr("箱數") <> dr("已查收") Then
                        Dim a As String = ""
                        dr("作業完成時間") = a.ToString()
                    End If
                    dr("作業預定時間") = dateline.ToString("HH:mm")
                End If
            Next

            For k As Integer = 0 To ds.Tables(0).Rows.Count - 1

                dr = ds.Tables(0).Rows(k)

                dr("no") = k + 1
                dr("到貨異常") = dr("status1")
                dr("時間異常") = dr("status2")
            Next
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
            change_color()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Private Sub change_color() '到貨時間異常 反橘
        Try
            Dim Nowtime As DateTime '現在時間
            Nowtime = time.ToString("yyyy-MM-dd HH:mm:ss") '現在時間

            Dim PlanArrivalTime As DateTime = Nothing '預計到貨時間
            Dim RealArrivalTime As DateTime '實際到貨時間
            Dim DelayTime As Integer '延遲時間

            '上午休息時間
            Dim AMRestStartTime As DateTime = Nowtime.ToString("yyyy-MM-dd 10:00:00")
            Dim AMRestEndTime As DateTime = Nowtime.ToString(" yyyy-MM-dd 10:10:00")
            '下午休息時間
            Dim PMRestStartTime As DateTime = Nowtime.ToString("yyyy-MM-dd 15:00:00")
            Dim PMRestEndTime As DateTime = Nowtime.ToString("yyyy-MM-dd 15:10:00")


            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1        '判斷沒被刪除的資料

                Dim dr As DataRow = ds.Tables(0).Rows(i)

                PlanArrivalTime = Convert.ToDateTime(dr("預定到貨時間").ToString()) '預計到貨時間

                '如果預計到貨時間+30分鐘落在休息時間，且休息時間之前未到貨的話，則異常時間+10分鐘

                If dr("實際到貨時間") Is DBNull.Value Or dr("已查收") = 0 Then '如果沒到貨

                    '如果預計到貨時間+30分鐘，剛好落在休息時間，則緩衝10分鐘
                    If (PlanArrivalTime.AddMinutes(30).ToString("HH:mm:ss") >= AMRestStartTime.ToString("HH:mm:ss") And PlanArrivalTime.AddMinutes(30).ToString("HH:mm:ss") <= AMRestEndTime.ToString("HH:mm:ss")) Or (PlanArrivalTime.AddMinutes(30).ToString("HH:mm:ss") >= PMRestStartTime.ToString("HH:mm:ss") And PlanArrivalTime.AddMinutes(30).ToString("HH:mm:ss") <= PMRestEndTime.ToString("HH:mm:ss")) Then
                        PlanArrivalTime = PlanArrivalTime.AddMinutes(10)
                    End If

                    If Nowtime > PlanArrivalTime.AddMinutes(30) Then
                        '到貨時間異常
                        Me.GridView1.Rows(i).BackColor = Color.FromArgb(255, 192, 128)
                    End If
                ElseIf Not dr("實際到貨時間") Is DBNull.Value Then '如果到貨的話
                    RealArrivalTime = dr("實際到貨時間").ToString() '實際到貨時間

                    If RealArrivalTime < PlanArrivalTime.AddMinutes(-30) Then
                        '到貨時間異常
                        Me.GridView1.Rows(i).BackColor = Color.FromArgb(255, 192, 128)
                    End If

                    If (PlanArrivalTime.AddMinutes(30).ToString("HH:mm:ss") >= AMRestStartTime.ToString("HH:mm:ss") And PlanArrivalTime.AddMinutes(30).ToString("HH:mm:ss") <= AMRestEndTime.ToString("HH:mm:ss")) Or (PlanArrivalTime.AddMinutes(30).ToString("HH:mm:ss") >= PMRestStartTime.ToString("HH:mm:ss") And PlanArrivalTime.AddMinutes(30).ToString("HH:mm:ss") <= PMRestEndTime.ToString("HH:mm:ss")) Then
                        PlanArrivalTime = PlanArrivalTime.AddMinutes(10)
                    End If

                    '如果到貨時間>預計時間+-30分鐘,到貨時間異常
                    If RealArrivalTime > PlanArrivalTime.AddMinutes(30) Then
                        '到貨時間異常
                        Me.GridView1.Rows(i).BackColor = Color.FromArgb(255, 192, 128)
                    End If

                    '判斷作業時間是否異常
                    BoxCount = dr("箱數").ToString() '箱數
                    RealCheck = dr("已查收").ToString() '檢品
                    DelayTime = dr("handle_time") '延遲時間

                    Dateline = WorkeTime(dr, DelayTime, RealArrivalTime) '計算工時

                    If Dateline.ToString("HH:mm:ss") > AMRestEndTime.ToString("HH:mm:ss") And RealArrivalTime.ToString("HH:mm:ss") < AMRestStartTime.ToString("HH:mm:ss") Then
                        Dateline = Dateline.AddMinutes(10)
                    ElseIf Dateline.ToString("HH:mm:ss") > PMRestEndTime.ToString("HH:mm:ss") And RealArrivalTime.ToString("HH:mm:ss") < PMRestStartTime.ToString("HH:mm:ss") Then
                        Dateline = Dateline.AddMinutes(10)
                    End If

                    LastArrivalTime = dr("最後到貨時間").ToString()
                    '進貨速度過慢或缺貨 (作業時間異常)
                    If Nowtime > Dateline And BoxCount <> RealCheck Or (LastArrivalTime > Dateline And BoxCount = RealCheck) Then
                        '現在時間>預計收完時間 貨還沒收完(紀錄錯誤時機)  OR  貨收完了最後到貨時間超過預計收完時間(顯示狀態)
                        '作業時間異常
                        Me.GridView1.Rows(i).BackColor = Color.LightCoral
                    End If
                End If
            Next
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub

    Function WorkeTime(dr As DataRow, DelayTime As Integer, RealArrivalTime As DateTime) As DateTime
        Try
            Dim Result As DateTime
            Dim WorkingTime As Integer
            Dim WorkingMin As Integer

            WorkingMin = (BoxCount * 3 * 1.5) / 60 '整數
            If ((BoxCount * 3 * 1.5) / 60) > WorkingMin Then '無條件進位
                WorkingMin = WorkingMin + 1
            End If

            WorkingTime = DelayTime + WorkingMin '卸貨時間(分)

            Result = RealArrivalTime.AddMinutes(WorkingTime).ToString() '預計應點收完時間

            Return Result
        Catch ex As Exception
            Dim str As String = ex.Message
            Return Nothing
        End Try
    End Function '計算工時

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Try
            dataview()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs)
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
    End Sub
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click  '登出
        Try
            Session("Exception") = ""
            Response.Redirect("Default.aspx")
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub LinkButton6_Click(sender As Object, e As EventArgs) Handles LinkButton6.Click
        Response.Redirect("special_case.aspx")
    End Sub
    Protected Sub LinkButton7_Click(sender As Object, e As EventArgs) Handles LinkButton7.Click
        Response.Redirect("Account_creat.aspx")
    End Sub
    Protected Sub LinkButton8_Click(sender As Object, e As EventArgs) Handles LinkButton8.Click
        Response.Redirect("Password_change.aspx")
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
    Private Sub after_4oclock()
        Try
            Dim now_time As DateTime = time
            If now_time.Hour >= 16 Then
                Dim sql1 As String = "select send from Exception_sendmail where date='" & time.ToString("yyyy-MM-dd") & "' and send='1'"
                ds1 = Db.Get_DataSet(sql1)
                If ds1.Tables(0).Rows.Count = 0 Then
                    Dim sql2 As String = "insert into Exception_sendmail (date,send) values('" & time.ToString("yyyy-MM-dd") & "','0')"
                    ds2 = Db.Get_DataSet(sql2)

                    Dim number As String = ""
                    Dim company As String = "當日部品到貨業務異常通知"
                    Dim sql As String = "select staff.name,staff.Email  from (Exception_Login inner join Exception_process  on Exception_Login .user_id =Exception_process .writer)inner join staff on Exception_Login.user_id =staff.code  where Exception_process.notice_all='1'"
                    ds = Db.Get_DataSet(sql)
                    Dim user_all As String = ""
                    If ds.Tables(0).Rows.Count > 0 Then
                        For i As Integer = 0 To ds.Tables(0).Rows.Count() - 1
                            Dim dr As DataRow = ds.Tables(0).Rows(i)
                            Dim user_mail1 As String = dr("Email").ToString()
                            user_all = user_all & dr("Email") & ","
                        Next
                    End If
                    Dim user_all1 As String = user_all.Substring(0, user_all.Length() - 1)
                    mail.send_mail3("" & user_all1 & "", company, number)

                    Dim sql3 As String = "update Exception_sendmail set send='1'  where date='" & time.ToString("yyyy-MM-dd") & "'"
                    ds3 = Db.Get_DataSet(sql3)
                End If
            End If
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
End Class