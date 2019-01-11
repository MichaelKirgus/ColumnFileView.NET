'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

<Serializable> Public Class ColumnDefinitionItem
    Public Property ColumnText As String = ""
    Public Property ColumnSQLName As String = ""
    Public Property ColumnWidth As Integer = 0
    Public Property ColumnDisplayIndex As Integer = -1
    Public Property AutoSizeColumnContent As New DataGridViewAutoSizeColumnMode
    Public Property SortingMode As New DataGridViewColumnSortMode
    Public Property DataType As DataTypeEnum = DataTypeEnum.StringValue
    Public Property IsFileSizeField As Boolean = False
    Public Property IsDateTimeField As Boolean = False
    Public Property PreloadFilter As Boolean = True
    Public Property CommonFilters As String() = {""}

    Public Property DefaultValue As String = ""
    Public Property Visible As Boolean = True
    Public Property CellsBackColor As KnownColor = KnownColor.White
    Public Property CellsTextColor As KnownColor = KnownColor.Black
    Public Property CellsTextAlignment As New DataGridViewContentAlignment

    Public Enum DataTypeEnum
        StringValue = 0
        Int32Value = 1
        Int64Value = 2
        BooleanValue = 3
        DateTimeValue = 4
        DateValue = 5
    End Enum
End Class
