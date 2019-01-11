'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.ComponentModel
Imports System.Text
Imports ColumnFileViewExportHandler
Imports ColumnFileViewImportFileHandler
Imports ColumnFileViewThreadingHelper.ThreadingWorkerItem

Public Class ThreadingManager
    Public TargetFilePath As String = ""
    Public OutputFilePath As String = ""
    Public TargetFileEncoding As EncodingInfo
    Public TargetFileEncodingAutoDiscover As Boolean = True
    Public ProfileHandler As New FileHandler
    Public ProfilesCollection As New List(Of FileHandler)
    Public StartAt As Int64 = 0
    Public LastNLines As Int64 = -1
    Public LoadToEnd As Boolean = True
    Public JumpToEnd As Boolean = False

    Public WithEvents ThreadManagerThread As New BackgroundWorker
    Public WithEvents LinesCalcThread As New BackgroundWorker
    Public ThreadManagerState As Integer = -1
    Public AllLinesCount As Integer = -1
    Public ThreadCount As Integer = -1
    Public FilesThreadSpintime As Integer = 100
    Public ThreadList As New List(Of ThreadingWorkerItem)
    Public ThreadPostProcessingItems As New List(Of ThreadingPostProccessingHelper)
    Public AllDataSet As New DataSet
    Public Output64 As Int64 = -1

    Public ImportHandle As New ImportHandler
    Public FileRulesHandler As New FileRecognitionHandler
    Public PostProccessingItems As List(Of ProcessingItem)
    Public PostProccessingDirectMode As Boolean = True

    Public Action As AvActions = AvActions.CountFileLines
    Public Destination As AvDestinations = AvDestinations.InternalDataset
    Public PostActions As PostActionsEnum = PostActionsEnum.None

    Public CurrProcessingState As Integer = -1
    Public CancelRequest As Boolean = False

    Public Function GetProcessorThreadCount() As Integer
        Try
            Return Environment.ProcessorCount
        Catch ex As Exception
            Return 1
        End Try
    End Function

    Public Function GetCurrentProcessedLinesCount() As Int64
        If ThreadList.Count = ThreadCount Then
            Dim currlines As Int64 = 0
            For threadcnt = 0 To ThreadCount - 1
                currlines += ThreadList(threadcnt).ImportHandle.CurrentLineCount
            Next

            Return currlines
        Else
            Return -1
        End If
    End Function

    Public Function RunTaskAsync() As Boolean
        CurrProcessingState = 0

        ThreadList.Clear()
        ThreadPostProcessingItems.Clear()

        CurrProcessingState = 1

        Try
            If AllLinesCount = -1 Then
                If LastNLines = -1 Then
                    'Die Anzahl der Zeilen in der Datei ist noch nicht berechnet worden...

                    LinesCalcThread.RunWorkerAsync()

                    Dim doloop As Boolean = True
                    Do While doloop = True
                        Threading.Thread.Sleep(FilesThreadSpintime)
                        doloop = False
                        If LinesCalcThread.IsBusy Then
                            doloop = True
                        End If
                    Loop

                    'Die Anzahl der Zeilen ist nun bekannt
                End If
            End If

            'Gesamtanzahl zu importierender Zeilen berechnen
            Dim allcnt As Int64
            allcnt = AllLinesCount - 1

            'Wurde die Anzahl der zu nutzenden Threads angegeben?
            If ThreadCount = -1 Then
                ThreadCount = GetProcessorThreadCount()
            End If

            Dim threads As Int64 = 1
            threads = ThreadCount

            Dim currstartpos As Int64 = 0
            currstartpos = StartAt

            'Beginnen wir am nicht am Anfang?
            If Not currstartpos = 0 Then
                'Wir müssen die Anzahl der übersprungenden Zeilen abziehen
                allcnt = allcnt - currstartpos
            End If

            'Laden wir nur die letzten N Zeilen?
            If Not LastNLines = -1 Then
                'Wir müssen die Anzahl der noch zu ladenden Zeilen berechnen
                currstartpos = allcnt - LastNLines + 1
                'Die Anzahl der Zeilen ist gleich N:
                allcnt = LastNLines
            End If

            'Benötigen wir mehrere Threads?
            If threads = 1 Then
                'Nein

                CurrProcessingState = 2

                Dim aa As New ThreadingWorkerItem
                aa.Action = Action
                aa.TargetFilePath = TargetFilePath
                aa.Destination = AvDestinations.InternalDataset
                aa.ProfileHandler = ProfileHandler
                aa.ProfilesCollection = ProfilesCollection
                aa.TargetFileEncoding = TargetFileEncoding
                aa.TargetFileEncodingAutoDiscover = TargetFileEncodingAutoDiscover
                aa.LoadToEnd = True
                aa.StartAt = currstartpos
                aa.MaxLines = -2

                aa.RunAction(True)
                ThreadList.Add(aa)
            Else
                'Ja
                Dim currthreadlines As Int64 = 0
                Dim restcalc As Boolean = False

                Dim modcheck As Int64
                modcheck = allcnt Mod threads
                If Not modcheck = 0 Then
                    'Der Letzte Thread macht den Rest
                    restcalc = True
                End If
                currthreadlines = allcnt / threads
                currthreadlines = Math.Round(currthreadlines, 0)

                CurrProcessingState = 2

                'Daten laden (und Task starten)
                For threadcnt = 1 To threads
                    Dim aa As New ThreadingWorkerItem
                    aa.Action = Action
                    aa.TargetFilePath = TargetFilePath
                    aa.Destination = AvDestinations.InternalDataset
                    aa.ProfileHandler = ProfileHandler
                    aa.ProfilesCollection = ProfilesCollection
                    aa.TargetFileEncoding = TargetFileEncoding
                    aa.TargetFileEncodingAutoDiscover = TargetFileEncodingAutoDiscover
                    aa.LoadToEnd = False
                    aa.StartAt = currstartpos

                    If (threadcnt = threads) And restcalc Then
                        aa.MaxLines = currstartpos + currthreadlines + modcheck
                    Else
                        aa.MaxLines = currstartpos + currthreadlines - 1
                    End If

                    aa.RunAction(True)
                    ThreadList.Add(aa)

                    currstartpos += currthreadlines
                Next
            End If

            ThreadManagerThread.RunWorkerAsync()

            CurrProcessingState = 3

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub CancelAction()
        ThreadManagerThread.CancelAsync()
        CancelRequest = True
    End Sub

    Private Sub ThreadManager_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ThreadManagerThread.DoWork

        'Warten, bis alle Threads Ihre Daten eingelesen haben
        Dim doloop1 As Boolean = True
        Do While doloop1 = True
            Threading.Thread.Sleep(FilesThreadSpintime)
            doloop1 = False
            If Not ThreadList.Count = 0 Then
                For index = 0 To ThreadList.Count - 1
                    If Not ThreadList(index).ProcessingThreadState = 2 Then
                        doloop1 = True
                    End If
                Next
            End If
        Loop

        CurrProcessingState = 4

        If Not PostProccessingItems.Count = 0 Then
            'Postprocessing-Tasks vorbereiten + starten
            For threadcnt = 0 To ThreadCount - 1
                Dim ii As New ThreadingPostProccessingHelper
                ii.ProcessingItems = PostProccessingItems
                ii.Data = ThreadList(threadcnt).OutputData
                ii.RunAction(True)

                ThreadPostProcessingItems.Add(ii)
            Next

            'Warten bis alle Threads das Postprocessing ausgeführt haben:
            If Not ThreadPostProcessingItems.Count = 0 Then
                Dim doloop2 As Boolean = True
                Do While doloop2 = True
                    Threading.Thread.Sleep(FilesThreadSpintime)
                    doloop2 = False
                    If Not ThreadList.Count = 0 Then
                        For index = 0 To ThreadList.Count - 1
                            If Not ThreadPostProcessingItems(index).ProcessingThreadState = 2 Then
                                doloop2 = True
                            End If
                        Next
                    End If
                Loop
            End If
        End If

        CurrProcessingState = 5

        Select Case Action
            Case AvActions.TransformData
                'Nun alle Daten zusammenfügen:
                'Alle Threads abgeschlossen, jetzt Resultat zusammensetzen:
                AllDataSet.Tables.Add("Main", "Data")

                For index = 0 To ThreadList.Count - 1
                    If index = 0 Then
                        For colindex = 0 To ThreadList(index).OutputData.Tables(0).Columns.Count - 1
                            Dim jj As New DataColumn
                            jj.Caption = ThreadList(index).OutputData.Tables(0).Columns(colindex).Caption
                            jj.ColumnName = ThreadList(index).OutputData.Tables(0).Columns(colindex).ColumnName
                            jj.DataType = ThreadList(index).OutputData.Tables(0).Columns(colindex).DataType
                            jj.DefaultValue = ThreadList(index).OutputData.Tables(0).Columns(colindex).DefaultValue
                            AllDataSet.Tables(0).Columns.Add(jj)
                        Next
                    End If

                    For rowindex = 0 To ThreadList(index).OutputData.Tables(0).Rows.Count - 1
                        Dim jj As DataRow
                        jj = AllDataSet.Tables(0).NewRow
                        jj.ItemArray = ThreadList(index).OutputData.Tables(0).Rows(rowindex).ItemArray.Clone

                        AllDataSet.Tables(0).Rows.Add(jj)
                    Next

                    ThreadList(index).OutputData.Tables(0).Clear()
                Next

                'Prüfen, ob das Resultat als Datei ausgegeben werden soll:
                If Not Destination = AvDestinations.InternalDataset Then
                    Dim jj As New ExportHandler
                    If Destination = AvDestinations.XMLFile Then
                        jj.ExportResults(AllDataSet, OutputFilePath, 1)
                    End If
                    If Destination = AvDestinations.CSVFile Then
                        jj.ExportResults(AllDataSet, OutputFilePath, 3)
                    End If
                End If
            Case AvActions.CheckForFileProfileRules
                'Int64-Wert zurückgeben

                Output64 = ThreadList(0).OutputInt64

            Case AvActions.CountFileLines
                Output64 = ThreadList(0).OutputInt64
        End Select

        CurrProcessingState = 6
    End Sub

    Private Sub LinesCalcThread_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LinesCalcThread.DoWork
        'Ermittle die Zeilen der Datei

        Dim aa As New ThreadingWorkerItem
        aa.Action = ThreadingWorkerItem.AvActions.CountFileLines
        aa.TargetFilePath = TargetFilePath
        aa.TargetFileEncoding = TargetFileEncoding
        aa.TargetFileEncodingAutoDiscover = TargetFileEncodingAutoDiscover

        aa.RunAction(False)

        'Warten, bis die Zeilenanzahl ermittelt wurde

        'Dim doloop As Boolean = True
        'Do While doloop = True
        '    Threading.Thread.Sleep(FilesThreadSpintime)
        '    doloop = False
        '    If aa.ProcessingThreadState = 1 Then
        '        doloop = True
        '    End If
        'Loop

        'Alle Threads abgeschlossen

        AllLinesCount = aa.OutputInt64
    End Sub
End Class
