Imports Microsoft.Exchange.WebServices.Data
Imports MSExchange

Public Class InboxFolder

    Private Shared _default_max_count As Integer = 1000
    Public Property DEFAULT_MAX_COUNT() As Integer
        Get
            Return _default_max_count
        End Get
        Set()
            If Value < 0 Then
                Throw New IndexOutOfRangeException()
            Else
                _default_max_count = Value
            End If
        End Set
    End Property

    Public Shared Function GetFolder()
        Return Folder.Bind(Exchange.Service, WellKnownFolderName.Inbox)
    End Function

    Public Function Search(filter As SearchFilter, count As Integer) As FindItemsResults(Of Item)
        Dim folder As Folder = GetFolder()
        Dim Items As FindItemsResults(Of Item) = folder.FindItems(filter, New ItemView(count))
        Return Items
    End Function

    Public Function Suject(pattern As String) As FindItemsResults(Of Item)
        Dim filter As SearchFilter.ContainsSubstring = New SearchFilter.ContainsSubstring()
        filter.Value = pattern
        filter.PropertyDefinition = EmailMessageSchema.Subject
        Return Search(filter, DEFAULT_MAX_COUNT)
    End Function

    Public Function After(time As DateTime) As FindItemsResults(Of Item)
        Dim filter As SearchFilter.IsGreaterThanOrEqualTo = New SearchFilter.IsGreaterThanOrEqualTo()
        filter.Value = time
        filter.PropertyDefinition = EmailMessageSchema.DateTimeReceived
        Return Search(filter, DEFAULT_MAX_COUNT)
    End Function

    Public Function Before(time As DateTime) As FindItemsResults(Of Item)
        Dim filter As SearchFilter.IsLessThanOrEqualTo = New SearchFilter.IsLessThanOrEqualTo()
        filter.Value = time
        filter.PropertyDefinition = EmailMessageSchema.DateTimeReceived
        Return Search(filter, DEFAULT_MAX_COUNT)
    End Function

    Public Function All() As FindItemsResults(Of Item)
        Dim folder As Folder = GetFolder()
        Dim items As FindItemsResults(Of Item) = GetFolder().FindItems(New ItemView(folder.TotalCount))
        Return items
    End Function

    Public Function Unread() As FindItemsResults(Of Item)
        Dim filter As SearchFilter = New SearchFilter.SearchFilterCollection(LogicalOperator.And, New SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, False))
        Return Search(filter, DEFAULT_MAX_COUNT)
    End Function
End Class




