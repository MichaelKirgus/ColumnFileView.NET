<Serializable> Public Class FileHandler
    Public Property DisplayName As String = ""
    Public Property TransformActions As New List(Of TransformItem)
    Public Property ColumnDefinitions As New List(Of ColumnDefinitionItem)
    Public Property OptimalColumnSizeIndex As Integer = -1
    Public Property ArrangeColumnsAfterLoad As Boolean = False
    Public Property AutoGenerateColumns As Boolean = False
    Public Property FirstRowColumns As Boolean = False
    Public Property SkipFirstRowAsContent As Boolean = False
    Public Property UseExtendedFilter As Boolean = False
    Public Property FileExtensionRecognitionRules As New List(Of FileExtension)
    Public Property FilenameRecognitionRules As New List(Of FileNameRecognition)
    Public Property FileContentRecognitionRules As New List(Of FileContentRecognition)
    Public Property ProfileSpecificPostActions As New List(Of ProcessingItem)
End Class
