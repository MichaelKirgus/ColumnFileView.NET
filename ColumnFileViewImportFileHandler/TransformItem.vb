'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
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
