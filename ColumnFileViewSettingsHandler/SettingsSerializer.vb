'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.IO
Imports System.Xml.Serialization

Public Class SettingsSerializer
    Public Function LoadSettingsFile(ByVal Filename As String) As SettingsClass
        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New SettingsClass
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New SettingsClass
        End Try
    End Function

    Public Function SaveSettingsFile(ByVal Filename As String, ByVal Obj As SettingsClass) As Boolean
        Try
            Dim XML As New XmlSerializer(Obj.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, Obj)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
