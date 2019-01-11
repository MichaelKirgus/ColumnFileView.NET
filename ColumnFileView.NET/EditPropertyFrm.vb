'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Public Class EditPropertyFrm
    Public Property _SelectedObj As Object
    Public Property _SelectedObjs As Object()

    Private Sub EditPropertyFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsNothing(_SelectedObjs) Then
            PropertyGrid1.SelectedObjects = _SelectedObjs
        End If

        If Not IsNothing(_SelectedObj) Then
            PropertyGrid1.SelectedObject = _SelectedObj
        End If
    End Sub
End Class