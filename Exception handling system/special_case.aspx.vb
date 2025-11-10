Public Class special_case
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim ds1 As DataSet
    Dim ds2 As DataSet
    Dim ds3 As DataSet
    Dim ds4 As DataSet
    Dim time As DateTime
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Db.Set_Db("172.30.4.225", "DNTW_Warehouse")
            Session("Exception") = "Home_page.aspx"
            If Session("user_id") = "" Then
                Response.Redirect("Default.aspx")
            End If
            If Not Page.IsPostBack Then
                Label5.Visible = False
            End If
            user_name1()
            dataview()
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

            ElseIf dr("user_group") = "檢討者" Then
                ImageButton4.Visible = False
            Else
                ImageButton4.Visible = False
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
    Private Sub dataview()
        Try
            Dim sql As String = "select * from VenderCode_Name full join Exception_handle_time  on VenderCode_Name .仕入れ先コード = Exception_handle_time .供應商編號  where Exception_handle_time.供應商編號 is null"
            ds = Db.Get_DataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = ds.Tables(0).Rows(i)
                    Dim a As String = dr("仕入れ先コード")
                    Dim b As String = dr("仕入れ先名")
                    Dim sql1 As String = "insert into  Exception_handle_time (供應商編號 ,供應商 ,handle_time )values('" & a.ToString.Trim() & "','" & b.ToString.Trim() & "','0')"
                    ds1 = Db.Get_DataSet(sql1)
                Next
            End If

            Dim sql2 As String = "select 供應商編號,供應商,handle_time  from Exception_handle_time"
            ds2 = Db.Get_DataSet(sql2)
            ds2.Tables(0).Columns.Add(New DataColumn("no", GetType(String)))
            For k As Integer = 0 To ds2.Tables(0).Rows.Count - 1
                Dim dr As DataRow = ds2.Tables(0).Rows(k)
                dr("no") = k + 1
            Next

            GridView1.DataSource = ds2.Tables(0)
            GridView1.DataBind()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text.ToString.Trim() <> "" And TextBox2.Text.ToString.Trim() <> "" Then

                Dim sql3 As String = "select * from Exception_handle_time where 供應商編號 ='" & TextBox1.Text.ToString.Trim() & "'"
                ds3 = Db.Get_DataSet(sql3)
                If ds3.Tables(0).Rows.Count > 0 Then
                    Dim sql2 As String = "update Exception_handle_time set handle_time ='" & TextBox2.Text.ToString.Trim() & "' where 供應商編號 ='" & TextBox1.Text.ToString.Trim() & "'"
                    ds2 = Db.Get_DataSet(sql2)
                    SQL_time()
                    Dim sql4 As String = "insert into Exception_special (user_id,user_name,供應商編號,修改內容,update_time )values ('" & Session("user_id").Trim() & "','" & Session("user_name").Trim() & "','" & TextBox1.Text.ToString.Trim() & "','" & TextBox2.Text.ToString.Trim() & "','" & time.ToString("yyyy-MM-dd HH:mm:ss") & "')"
                    ds4 = Db.Get_DataSet(sql4)

                    Label5.Visible = False
                    dataview()
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    Label5.Visible = True
                    Label5.Text = "修改完成"
                Else
                    Label5.Visible = True
                    Label5.Text = "供應商編號錯誤"
                End If
            Else
                Label5.Visible = True
                Label5.Text = "欄位不可為空白"
            End If
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
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        Try
            Session("Exception") = ""
            Response.Redirect("Default.aspx")
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
End Class