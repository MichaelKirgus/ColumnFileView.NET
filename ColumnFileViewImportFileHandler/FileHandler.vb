'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
<Serializable> Public Class FileHandler
    Public Property DisplayName As String = ""
    Public Property TransformActions As New List(Of TransformItem)
    Public Property ColumnDefinitions As New List(Of ColumnDefinitionItem)
    Public Property OptimalColumnSizeIndex As Integer = -1
    Public Property ArrangeColumnsAfterLoad As Boolean = False
    Public Property AutoGenerateColumns As Boolean = False
    Public Property FirstRowColumns As Boolean = False
    Public Property SkipFirstRowAsContent As Boolean = False
    Public Property SkipNFirstLines As Integer = 0
    Public Property UseExtendedFilter As Boolean = False
    Public Property FileExtensionRecognitionRules As New List(Of FileExtension)
    Public Property FilenameRecognitionRules As New List(Of FileNameRecognition)
    Public Property FileContentRecognitionRules As New List(Of FileContentRecognition)
    Public Property ProfileSpecificPostActions As New List(Of ProcessingItem)
End Class
