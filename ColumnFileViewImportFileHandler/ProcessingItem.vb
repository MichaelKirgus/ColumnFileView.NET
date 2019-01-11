'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
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
