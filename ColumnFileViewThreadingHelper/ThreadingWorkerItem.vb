Imports System.ComponentModel
Imports System.Text
Imports ColumnFileViewExportHandler
Imports ColumnFileViewImportFileHandler

Public Class ThreadingWorkerItem
    Public TargetFilePath As String = ""
    Public OutputFilePath As String = ""
    Public TargetFileEncoding As EncodingInfo
    Public TargetFileEncodingAutoDiscover As Boolean = True
    Public ProfileHandler As FileHandler
    Public ProfilesCollection As New List(Of FileHandler)
    Public StartAt As Int64 = 0
    Public MaxLines As Int64 = 0
    Public LoadToEnd As Boolean = True
    Public JumpToEnd As Boolean = False

    Public ImportHandle As New ImportHandler
    Public FileRulesHandler As New FileRecognitionHandler
    Public PostProccessingItems As List(Of ProcessingItem)
    Public PostProccessingDirectMode As Boolean = True

    Public WithEvents ProcessingThread As New BackgroundWorker
    Public ProcessingThreadState As Integer = -1

    Public OutputData As New DataSet
    Public OutputInt64 As Int64

    Public Action As AvActions = AvActions.CountFileLines
    Public Destination As AvDestinations = AvDestinations.InternalDataset
    Public PostActions As PostActionsEnum = PostActionsEnum.None

    Public Enum AvActions
        CountFileLines = 0
        TransformData = 1
        CheckForFileProfileRules = 2
    End Enum

    Public Enum AvDestinations
        InternalDataset = 0
        XMLFile = 1
        CSVFile = 2
    End Enum

    Public Enum PostActionsEnum
        None = 0
        ProccessActions = 1
    End Enum

    Public Function RunAction(Optional ByVal Async As Boolean = False) As Boolean
        Try
            If Async = False Then
                Select Case Action
                    Case AvActions.CountFileLines
                        OutputInt64 = ImportHandle.CountFileLines(TargetFilePath, TargetFileEncodingAutoDiscover, TargetFileEncoding.GetEncoding)
                        Return True
                    Case AvActions.CheckForFileProfileRules
                        OutputInt64 = FileRulesHandler.GetFileProfileFromFile(TargetFilePath, ProfilesCollection, TargetFileEncodingAutoDiscover, TargetFileEncoding.GetEncoding)
                    Case AvActions.TransformData
                        OutputData = ImportHandle.LoadFile(TargetFilePath, TargetFileEncodingAutoDiscover, TargetFileEncoding.GetEncoding, ProfileHandler, StartAt, MaxLines)
                        If Destination = AvDestinations.InternalDataset Then
                            Return True
                        End If
                        Dim jj As New ExportHandler
                        If Destination = AvDestinations.XMLFile Then
                            jj.ExportResults(OutputData, OutputFilePath, 1)
                            Return True
                        End If
                        If Destination = AvDestinations.CSVFile Then
                            jj.ExportResults(OutputData, OutputFilePath, 3)
                            Return True
                        End If
                End Select
            Else
                If ProcessingThread.IsBusy = False Then
                    ProcessingThread.RunWorkerAsync()
                End If
            End If

            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ProcessingThread_DoWork() Handles ProcessingThread.DoWork
        ProcessingThreadState = 1
        RunAction(False)
    End Sub

    Private Sub ProcessingThread_Complete() Handles ProcessingThread.RunWorkerCompleted
        ProcessingThreadState = 2
    End Sub
End Class
