'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.IO

Public Class FileRecognitionHandler
    Dim resultindex As Int64 = -1

    Public Function GetFileProfileFromFile(ByVal Filename As String, ByVal ProfileCollection As List(Of FileHandler), ByVal AutoDetectEncoding As Boolean, ByVal Encoding As System.Text.Encoding) As Int64
        Try
            Dim zz As FileInfo
            zz = New FileInfo(Filename)

            Dim SourceStream As FileStream = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim dd As New StreamReader(SourceStream, Encoding, AutoDetectEncoding)

            Dim First10Lines As New List(Of String)
            Do Until dd.EndOfStream
                First10Lines.Add(dd.ReadLine())
                If (First10Lines.Count = 10) Or (dd.EndOfStream) Then
                    Exit Do
                End If
            Loop

            If Not ProfileCollection.Count = 0 Then
                For index = 0 To ProfileCollection.Count - 1
                    If Not ProfileCollection(index).FileExtensionRecognitionRules.Count = 0 Then
                        For ruleindex1 = 0 To ProfileCollection(index).FileExtensionRecognitionRules.Count - 1
                            If ProfileCollection(index).FileExtensionRecognitionRules(ruleindex1).Extension.ToLower = zz.Extension.ToLower Then
                                resultindex = index
                                If ProfileCollection(index).FileExtensionRecognitionRules(ruleindex1).CancelRecognition Then
                                    Return resultindex
                                End If
                            End If
                        Next
                    End If

                    If Not ProfileCollection(index).FilenameRecognitionRules.Count = 0 Then
                        For ruleindex3 = 0 To ProfileCollection(index).FilenameRecognitionRules.Count - 1
                            Dim text As String = ""
                            Dim filenamex As String = ""
                            If ProfileCollection(index).FilenameRecognitionRules(ruleindex3).CaseSensitive Then
                                text = ProfileCollection(index).FilenameRecognitionRules(ruleindex3).Text.ToLower
                                filenamex = zz.Name.ToLower
                            Else
                                text = ProfileCollection(index).FilenameRecognitionRules(ruleindex3).Text
                                filenamex = zz.Name
                            End If
                            If ProfileCollection(index).FilenameRecognitionRules(ruleindex3).DirectMatching Then
                                If text = filenamex Then
                                    resultindex = index
                                    If ProfileCollection(index).FilenameRecognitionRules(ruleindex3).CancelRecognition Then
                                        Return resultindex
                                    End If
                                End If
                            Else
                                If filenamex.Contains(text) Then
                                    resultindex = index
                                    If ProfileCollection(index).FilenameRecognitionRules(ruleindex3).CancelRecognition Then
                                        Return resultindex
                                    End If
                                End If
                            End If
                        Next
                    End If

                    If Not ProfileCollection(index).FileContentRecognitionRules.Count = 0 Then
                        For ruleindex2 = 0 To ProfileCollection(index).FileContentRecognitionRules.Count - 1
                            If First10Lines.Count > ProfileCollection(index).FileContentRecognitionRules(ruleindex2).LineCount Then
                                If First10Lines(ProfileCollection(index).FileContentRecognitionRules(ruleindex2).LineCount).Contains(ProfileCollection(index).FileContentRecognitionRules(ruleindex2).ContainsText) Then
                                    resultindex = index
                                    If ProfileCollection(index).FileContentRecognitionRules(ruleindex2).CancelRecognition Then
                                        Return resultindex
                                    End If
                                End If
                            End If
                        Next
                    End If
                Next
            End If


            Return resultindex
        Catch ex As Exception
            Return resultindex
        End Try
    End Function
End Class
