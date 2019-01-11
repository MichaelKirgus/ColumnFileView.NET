﻿'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
<Serializable> Public Class FileNameRecognition
    Public Property DisplayName As String = ""
    Public Property Text As String = ""
    Public Property CaseSensitive As Boolean = False
    Public Property DirectMatching As Boolean = False
    Public Property CancelRecognition As Boolean = False
End Class
