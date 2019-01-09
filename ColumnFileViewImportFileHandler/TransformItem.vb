<Serializable> Public Class TransformItem
    Public Property SplitChar As Char
    Public Property SplitReturnIndex As Integer = -1
    Public Property AddToList As Boolean = False
    Public Property ClearList As Boolean = True
    Public Property BeforeSplitRemoveStartIndex As Integer = 0
    Public Property BeforeSplitRemoveCount As Integer = 0
    Public Property AfterSplitRemoveStartIndex As Integer = 0
    Public Property AfterSplitRemoveCount As Integer = 0
    Public Property BeforeSplitReplaceOldChar As Char
    Public Property BeforeSplitReplaceNewChar As Char
    Public Property AfterSplitReplaceOldChar As Char
    Public Property AfterSplitReplaceNewChar As Char
    Public Property AddSplitChar As Char
End Class
