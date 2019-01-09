<Serializable> Public Class ProcessingItem
    Public Property Name As String = ""

    Public Property ProcessingType As ProcessingTypeEnum = ProcessingTypeEnum.ReplaceChars

    Public Property NewCharIsNothing As Boolean = False
    Public Property OldChar As Char
    Public Property NewChar As Char
    Public Property OldDateTimeFormat As String = ""
    Public Property ConvertDateToString As Boolean = False
    Public Property NewDateTimeFormat As String = ""
    Public Property DateTimeColumnIndex As Integer = 0
    Public Property FilesizeColumnIndex As Integer = 0
    Public Property FilesizeDecimalPlaces As Integer = 2
    Public Property FilesizeKBSize As Integer = 1024
    Public Property RegExPattern As String = ""
    Public Property RegExEqualIndex As Integer = -1

    Public Property Activated As Boolean = False

    Enum ProcessingTypeEnum
        ReplaceChars = 0
        ReplaceDateTimeFormat = 1
        ConvertFilesizeInReadableFormat = 2
        RexExReplacementInColumn = 3
        RexExExpressionEqualsInColumn = 4
    End Enum
End Class
