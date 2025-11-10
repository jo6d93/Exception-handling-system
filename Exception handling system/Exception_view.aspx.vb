Imports System.Drawing

Public Class Exception_view
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim ds1 As DataSet
    Dim time As DateTime  'SQL系統時間
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Db.Set_Db("172.30.4.225", "DNTW_Warehouse")
            Session("Exception") = "Exception_view.aspx"
            If Session("user_id") = "" Then
                Response.Redirect("Default.aspx")
            End If

            If Session("異常編號") <> "" Then
                ImageButton3.Visible = True
            End If
            If Not Page.IsPostBack Then
                select_date()
                btn_select_Click(Me, Nothing)
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
                user_name.Text = dr("user_name").ToString()
                Session("user_name") = dr("user_name").ToString()
                Session("user_id") = dr("user_id").ToString()
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
    Protected Sub btn_select_Click(sender As Object, e As EventArgs) Handles btn_select.Click  '查詢
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
            Dim sql As String = "select  異常編號, 異常日期 ,異常種類, CONVERT(varchar(16),(異常對象)) AS 異常對象 ,計畫時間 ,實際時間 , CONVERT(varchar(20),(異常調查原因)) AS 異常調查原因,CONVERT(varchar(20),(再發防止)) AS 再發防止,CONVERT(varchar(20),(責任部屬)) AS 責任部屬 ,簽核狀態 ,受入場,status from Exception_view " &
                "where 異常日期='" & DropDownList3.Text.ToString() & "-" & c.ToString() & "-" & d.ToString() & "'  ORDER BY 計畫時間 ASC"
            ds = Db.Get_DataSet(sql)
            ds.Tables(0).Columns.Add(New DataColumn("no", GetType(String)))

            For k As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow
                dr = ds.Tables(0).Rows(k)
                dr("no") = k + 1
            Next
            GridView1.DataSource = ds.Tables(0)
            GridView1.DataBind()
        Catch ex As Exception
            Dim str As String = ex.Message
        End Try
    End Sub
    Protected Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            If (CType(e.CommandSource, Button)).CommandName = "detail" Then
                Session("異常編號") = e.CommandArgument.ToString()
                Response.Redirect("Exception_handle.aspx")
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
    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        btn_select_Click(Me, Nothing)
    End Sub
End Class