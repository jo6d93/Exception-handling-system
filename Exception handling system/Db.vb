Imports System.Data.SqlClient

Public Class Db
    Public Shared server_ip As String = ""
    Public Shared database_name As String = ""
    Public Shared connection_string As String = ""
    Public Shared conn As SqlConnection
    Public Shared da As SqlDataAdapter = New SqlDataAdapter()
    Public Shared cmd As SqlCommand = New SqlCommand()

    Public Shared Sub Renew_Connection()
        conn = New SqlConnection(connection_string)
    End Sub

    Public Shared Function NEW_Connection() As SqlConnection
        Return New SqlConnection(connection_string)
    End Function

    Public Shared Sub Open_Connection()
        Try
            If conn Is Nothing Then
                conn = NEW_Connection()
            End If
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            Dim err As String = ex.Message
        End Try
    End Sub

    Public Shared Sub Close_Connection()
        conn.Close()
    End Sub

    Public Shared Sub Set_Db(ByVal server_ip As String, ByVal database_name As String)
        Db.server_ip = server_ip
        Db.database_name = database_name
        connection_string = "Data Source=" & server_ip & " ;Initial Catalog=" & database_name & ";Integrated Security=False;User Id=0013;Password=0013"
        Renew_Connection()
    End Sub

    Public Shared Function Get_DataSet(ByVal sql As String) As DataSet
        Try
            da = New SqlDataAdapter(sql, conn)
            Dim ds As DataSet = New DataSet()
            Open_Connection()
            da.Fill(ds)
            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function ExecuteNonQuery(ByVal sql As String) As Boolean
        Try
            cmd = New SqlCommand(sql, conn)
            Open_Connection()
            cmd.ExecuteNonQuery()
            Return True
        Catch
            Return False
        End Try
    End Function
End Class
