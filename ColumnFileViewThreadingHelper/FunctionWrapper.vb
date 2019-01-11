'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.ComponentModel
Imports System.Text
Imports ColumnFileViewImportFileHandler
Imports ColumnFileViewThreadingHelper.MultiFileImportManager
Imports ColumnFileViewThreadingHelper.ThreadingWorkerItem

Public Class FunctionWrapper
    Public TManager As New ThreadingManager
    Public TMultipleFilesManager As New MultiFileImportManager
    Public UsedManagerID As Integer = -1
    Public CurrProcessingState As Integer = -1

    Public Function StartAction(ByVal Action As AvActions,
                                ByVal Destination As AvDestinations,
                                ByVal FExportMode As FileExportMode,
                                ByVal InputData As DataSet,
                                ByVal Filenames As List(Of String),
                                ByVal TargetFolder As String,
                                ByVal ProfileFilename As String,
                                ByVal ProfileCollection As List(Of FileHandler),
                                ByVal ProfileHandler As FileHandler,
                                ByVal OutputFilePath As String,
                                ByVal TargetFileEncoding As EncodingInfo,
                                ByVal TargetFileEncodingAutoDiscover As Boolean,
                                ByVal StartAt As Int64,
                                ByVal LastNLines As Int64,
                                ByVal LoadToEnd As Boolean,
                                ByVal AllLinesCount As Int64,
                                ByVal LoadMaxLines As Int64,
                                ByVal ThreadCount As Integer,
                                ByVal PostProccessingItems As List(Of ProcessingItem),
                                ByVal UseThreadingWithMultipleFiles As Boolean,
                                ByVal FilesBatchThreadSpintime As Integer,
                                ByVal FilesThreadSpintime As Integer,
                                Optional ByVal Wait As Boolean = False) As Boolean

        CurrProcessingState = 1

        If (Filenames.Count = 0) And (TargetFolder = "") And Action = AvActions.TransformData Then
            Return False
        End If

        TManager.PostProccessingItems = PostProccessingItems
        TManager.ProfilesCollection = ProfileCollection

        Select Case Action
            Case AvActions.TransformData
                TManager.Action = Action

                If Not Filenames.Count = 0 Then
                    TManager.TargetFilePath = Filenames(0)
                End If

                TManager.Destination = Destination
                TManager.OutputFilePath = OutputFilePath
                TManager.StartAt = StartAt

                If LoadToEnd = False Then
                    TManager.LoadToEnd = False
                    TManager.LastNLines = LastNLines
                End If

                TManager.TargetFileEncoding = TargetFileEncoding
                TManager.TargetFileEncodingAutoDiscover = TargetFileEncodingAutoDiscover

                TManager.PostProccessingDirectMode = True

                TManager.ThreadCount = ThreadCount

                If Not LoadMaxLines = -1 Then
                    TManager.AllLinesCount = StartAt - LoadMaxLines
                    'TManager.LastNLines = 0 'Prüfen
                    'TManager.AllLinesCount = StartAt - LoadMaxLines + AllLinesCount
                Else
                    If Not AllLinesCount = -1 Then
                        TManager.AllLinesCount = AllLinesCount
                    End If
                End If

                TManager.FilesThreadSpintime = FilesThreadSpintime

                If Not ProfileHandler.DisplayName = "" Then
                    TManager.ProfileHandler = ProfileHandler
                Else
                    Dim oo As FileHandler
                    Dim qq As New FileSerializer
                    oo = qq.LoadDefFile(ProfileFilename)

                    TManager.ProfileHandler = oo
                End If

                If (Filenames.Count > 1) Or (Not TargetFolder = "") Then
                    'Es wurden mehrere Dateien übergeben:

                    TMultipleFilesManager.FileProcessSettings = TManager

                    If TargetFolder = "" Then
                        TMultipleFilesManager.TargetFileCollection = Filenames
                    Else
                        TMultipleFilesManager.TargetFileFolder = TargetFolder
                    End If

                    TMultipleFilesManager.FilesBatchThreadSpintime = FilesBatchThreadSpintime

                    If UseThreadingWithMultipleFiles = False Then
                        TMultipleFilesManager.FileProcessSettings.ThreadCount = 1
                    End If

                    TMultipleFilesManager.FileProcessSettings.AllLinesCount = -1
                    TMultipleFilesManager.ProcessFileBatch()

                    UsedManagerID = 1
                    CurrProcessingState = 2

                    If Wait Then
                        Dim doloop1 As Boolean = True
                        Do While doloop1 = True
                            Threading.Thread.Sleep(FilesBatchThreadSpintime)
                            doloop1 = False

                            If Not TMultipleFilesManager.CurrProcessingState = 2 Then
                                doloop1 = True
                            End If
                        Loop
                    End If
                Else
                    If Not AllLinesCount = -1 Then
                        TManager.AllLinesCount = AllLinesCount
                    End If

                    TManager.RunTaskAsync()

                    UsedManagerID = 2
                    CurrProcessingState = 2

                    If Wait Then
                        Dim doloop1 As Boolean = True
                        Do While doloop1 = True
                            Threading.Thread.Sleep(FilesBatchThreadSpintime)
                            doloop1 = False

                            If Not TManager.CurrProcessingState = 6 Then
                                doloop1 = True
                            End If
                        Loop
                    End If
                End If

            Case AvActions.CheckForFileProfileRules
                TManager.Action = Action
                TManager.TargetFilePath = Filenames(0)
                TManager.TargetFileEncoding = TargetFileEncoding
                TManager.TargetFileEncodingAutoDiscover = TargetFileEncodingAutoDiscover
                TManager.FilesThreadSpintime = FilesThreadSpintime
                TManager.ProfileHandler = New FileHandler
                TManager.RunTaskAsync()

                CurrProcessingState = 2

                If Wait Then
                    Dim doloop1 As Boolean = True
                    Do While doloop1 = True
                        Threading.Thread.Sleep(FilesBatchThreadSpintime)
                        doloop1 = False

                        If Not TManager.CurrProcessingState = 6 Then
                            doloop1 = True
                        End If
                    Loop
                End If

            Case AvActions.CountFileLines
                TManager.Action = Action
                TManager.TargetFilePath = Filenames(0)
                TManager.TargetFileEncoding = TargetFileEncoding
                TManager.TargetFileEncodingAutoDiscover = TargetFileEncodingAutoDiscover
                TManager.FilesThreadSpintime = FilesThreadSpintime
                TManager.ProfileHandler = New FileHandler
                TManager.RunTaskAsync()

                CurrProcessingState = 2

                If Wait Then
                    Dim doloop1 As Boolean = True
                    Do While doloop1 = True
                        Threading.Thread.Sleep(FilesBatchThreadSpintime)
                        doloop1 = False

                        If Not TManager.CurrProcessingState = 6 Then
                            doloop1 = True
                        End If
                    Loop
                End If
        End Select

        CurrProcessingState = 3

        Return True
    End Function
End Class
