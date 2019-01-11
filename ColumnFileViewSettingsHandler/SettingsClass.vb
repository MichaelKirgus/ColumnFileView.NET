'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.

<Serializable> Public Class SettingsClass
    Public Property LanguageID As Integer = 0
    Public Property DefaultEncodingID As Integer = 0
    Public Property AutoDetectEncoding As Boolean = True
    Public Property DefaultLoadFileLinesCount As Boolean = True
    Public Property DefaultAnalyseFile As Boolean = True
    Public Property DefaultLoadPreview As Boolean = True
    Public Property AlwaysUseExtendedFilters As Boolean = False
    Public Property AlwaysUseColumnsSortFeature As Boolean = True
End Class
