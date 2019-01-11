'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Public Class FileSizeCalcHandler
    Public Function GetRightSizeFormat(ByVal size_bytes As Int64, Optional ByVal DecimalPlaces As Integer = 2, Optional ByVal KBSize As Integer = 1024) As String
        'Diese Funktion generiert aus einer Anzahl von Bytes eine lesbare Größenangabe in Bytes/KB/MB/GB/TB

        Try
            Dim newstr As String = ""
            Dim newdec As Double = 0
            Dim by As Decimal = size_bytes
            If by > 1024 Then
                Dim kb As Double = by / KBSize
                If kb > 1024 Then
                    Dim mb As Double = kb / KBSize
                    If mb > 1024 Then
                        Dim gb As Double = mb / KBSize
                        If gb > 1024 Then
                            Dim tb As Double = gb / KBSize
                            newdec = Math.Round(tb, DecimalPlaces)
                            newstr += newdec & " TB"
                        Else
                            newdec = Math.Round(gb, DecimalPlaces)
                            newstr += Math.Round(gb, DecimalPlaces).ToString & " GB"
                        End If
                    Else
                        newdec = Math.Round(mb, DecimalPlaces)
                        newstr += Math.Round(mb, DecimalPlaces).ToString & " MB"
                    End If
                Else
                    newdec = Math.Round(kb, DecimalPlaces)
                    newstr += Math.Round(kb, DecimalPlaces).ToString & " KB"
                End If
            Else
                newdec = Math.Round(by, DecimalPlaces)
                newstr += Math.Round(by, DecimalPlaces).ToString & " Bytes"
            End If

            Return newstr
        Catch ex As Exception
            Return "0 KB"
        End Try
    End Function
End Class
