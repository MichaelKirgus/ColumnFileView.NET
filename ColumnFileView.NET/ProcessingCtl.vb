Imports ColumnFileViewImportFileHandler
Imports ColumnFileViewThreadingHelper

Public Class ProcessingCtl
    Public _parent As Form1
    Public _ImportHandleObj As ImportHandler
    Public _ImportThread As ThreadingWorkerItem
    Public _PostProccessingThread As PostProcessingHandler
    Public _Loadstate As Integer = 0
    Public _InitMode As Boolean = True

    Public Sub InitGUI()
        If Not _ImportHandleObj.LineCount = 0 Then
            If _parent._UseMultiThreading Then
                ProgressBar1.Style = ProgressBarStyle.Blocks

                If _parent.AppFunctionWrapper.UsedManagerID = 2 Then
                    ProgressBar2.Style = ProgressBarStyle.Blocks
                    FileProgress.Text = "Datei (1/1): " & _parent.AppFunctionWrapper.TManager.TargetFilePath
                    ProgressBar1.Maximum = 100
                    ProgressBar1.Value = 50

                    _InitMode = False
                End If

                If _parent.AppFunctionWrapper.UsedManagerID = 1 Then
                    ProgressBar2.Style = ProgressBarStyle.Blocks
                    ProgressBar1.Maximum = _parent.AppFunctionWrapper.TMultipleFilesManager.TargetFileCollection.Count - 1

                    _InitMode = False
                End If
            Else
                ProgressBar2.Style = ProgressBarStyle.Blocks
                ProgressBar2.Maximum = _ImportHandleObj.LineCount
            End If
        Else
            ProgressBar1.Style = ProgressBarStyle.Marquee
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()

        Try
            If _Loadstate = 0 Then
                If _parent._UseMultiThreading Then
                    If (Not _parent.AppFunctionWrapper.UsedManagerID = -1) And (_InitMode = True) Then
                        InitGUI()
                    End If
                    If _parent.AppFunctionWrapper.UsedManagerID = 1 Then
                        FileProgress.Text = "Datei (" & (_parent.AppFunctionWrapper.TMultipleFilesManager.CurrFileInd + 1) & "/" & _parent.AppFunctionWrapper.TMultipleFilesManager.TargetFileCollection.Count & "): " & _parent.AppFunctionWrapper.TMultipleFilesManager.CurrFileName
                        ProgressBar1.Value = _parent.AppFunctionWrapper.TMultipleFilesManager.CurrFileInd

                        If Not _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.AllLinesCount = -1 Then
                            Dim allcnt As Int64
                            allcnt = _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.AllLinesCount

                            Dim currlines As Int64
                            currlines = _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.GetCurrentProcessedLinesCount

                            If currlines = -1 Then
                                ItemProgress.Text = "Berechne Threads..."
                            Else
                                If _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.CurrProcessingState = 3 Then
                                    If currlines > ProgressBar2.Maximum Then
                                        If allcnt < currlines Then
                                            ProgressBar2.Maximum = currlines * 2
                                        Else
                                            ProgressBar2.Maximum = allcnt
                                        End If
                                    Else
                                        ProgressBar2.Maximum = allcnt
                                    End If

                                    If Not ProgressBar2.Maximum < currlines Then
                                        ProgressBar2.Value = currlines
                                    End If

                                    ItemProgress.Text = "Lade Zeile " & currlines & " von " & allcnt
                                End If

                                If _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.CurrProcessingState = 4 Then
                                    ItemProgress.Text = "Postprocessing..."
                                End If

                                If _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.CurrProcessingState = 5 Then
                                    If Not _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.AllDataSet.Tables.Count = 0 Then
                                        ProgressBar2.Maximum = _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.AllLinesCount
                                        ProgressBar2.Value = _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.AllDataSet.Tables(0).Rows.Count

                                        ItemProgress.Text = "Füge Teile zusammen (" & _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.AllDataSet.Tables(0).Rows.Count & "/" & _parent.AppFunctionWrapper.TMultipleFilesManager.FileProcessSettings.AllLinesCount & ")"
                                    End If
                                End If
                            End If
                        End If
                    Else
                        Dim allcnt As Int64
                        allcnt = _parent.AppFunctionWrapper.TManager.AllLinesCount

                        Dim currlines As Int64
                        currlines = _parent.AppFunctionWrapper.TManager.GetCurrentProcessedLinesCount

                        If currlines = -1 Then
                            ItemProgress.Text = "Berechne Threads..."
                        Else
                            If currlines > ProgressBar2.Maximum Then
                                If allcnt < currlines Then
                                    ProgressBar2.Maximum = currlines * 2
                                Else
                                    ProgressBar2.Maximum = allcnt
                                End If
                            Else
                                ProgressBar2.Maximum = allcnt
                            End If

                            If Not ProgressBar2.Maximum < currlines Then
                                ProgressBar2.Value = currlines
                            End If

                            ItemProgress.Text = "Lade Zeile " & currlines & " von " & allcnt
                        End If
                    End If
                Else
                    If Not _ImportHandleObj.LineCount = 0 Then
                        ProgressBar2.Value = _ImportHandleObj.CurrentLine
                        ItemProgress.Text = "Lade Zeile " & _ImportHandleObj.CurrentLine & " von " & _ImportHandleObj.LineCount
                    Else
                        ItemProgress.Text = "Lade Zeile " & _ImportHandleObj.CurrentLine
                    End If
                End If
            Else
                ProgressBar2.Maximum = _PostProccessingThread.LinesCnt
                ProgressBar2.Value = _PostProccessingThread.CurrentLine
                ItemProgress.Text = "Nachbearbeitung Zeile " & _PostProccessingThread.CurrentLine
            End If
            If _Loadstate = 1 Then

            End If
        Catch ex As Exception
        End Try

        Timer1.Start()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Stop()

        InitGUI()
    End Sub

    Private Sub ProcessingCtl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = Me.Parent.Width - 10
    End Sub

    Private Sub ProcessingCtl_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Me.Width = Me.Parent.Width - 10
    End Sub
End Class
