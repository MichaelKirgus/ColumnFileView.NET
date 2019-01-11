'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.ComponentModel

Public Class MultiFileImportManager
    Public FileProcessSettings As ThreadingManager
    Public TargetFileCollection As New List(Of String)
    Public TargetFileFolder As String = ""
    Public FExportMode As FileExportMode = FileExportMode.Combined
    Public WithEvents MultiFileThread As New BackgroundWorker
    Public FilesBatchThreadSpintime As Integer = 100
    Public CurrProcessingState As Integer = 0
    Public CurrFileInd As Integer = 0
    Public CurrFileName As String = ""

    Public AllDataSet As New DataSet

    Public Enum FileExportMode
        Combined = 0
        Individually = 1
    End Enum

    Public Function ProcessFileBatch() As Boolean
        AllDataSet.Clear()
        MultiFileThread.RunWorkerAsync()
        CurrProcessingState = 1

        Return True
    End Function

    Private Sub MultiFileThread_DoWork() Handles MultiFileThread.DoWork
        AllDataSet.Tables.Add("Main", "Data")

        If Not TargetFileFolder = "" Then
            For Each item As String In IO.Directory.GetFiles(TargetFileFolder)
                TargetFileCollection.Add(item)
            Next
        End If

        For fileind = 0 To TargetFileCollection.Count - 1
            CurrFileInd = fileind
            CurrFileName = TargetFileCollection(fileind)
            FileProcessSettings.TargetFilePath = TargetFileCollection(fileind)
            FileProcessSettings.CurrProcessingState = 0
            FileProcessSettings.AllLinesCount = -1
            FileProcessSettings.LastNLines = -1
            FileProcessSettings.AllDataSet = New DataSet
            FileProcessSettings.RunTaskAsync()

            Dim doloop2 As Boolean = True
            Do While doloop2 = True
                Threading.Thread.Sleep(FilesBatchThreadSpintime)
                doloop2 = False
                If Not FileProcessSettings.CurrProcessingState = 6 Then
                    doloop2 = True
                End If
            Loop

            If fileind = 0 Then
                For colindex = 0 To FileProcessSettings.AllDataSet.Tables(0).Columns.Count - 1
                    Dim jj As New DataColumn
                    jj.Caption = FileProcessSettings.AllDataSet.Tables(0).Columns(colindex).Caption
                    jj.ColumnName = FileProcessSettings.AllDataSet.Tables(0).Columns(colindex).ColumnName
                    jj.DataType = FileProcessSettings.AllDataSet.Tables(0).Columns(colindex).DataType
                    jj.DefaultValue = FileProcessSettings.AllDataSet.Tables(0).Columns(colindex).DefaultValue
                    AllDataSet.Tables(0).Columns.Add(jj)
                Next
            End If

            For rowindex = 0 To FileProcessSettings.AllDataSet.Tables(0).Rows.Count - 1
                Dim jj As DataRow
                jj = AllDataSet.Tables(0).NewRow
                jj.ItemArray = FileProcessSettings.AllDataSet.Tables(0).Rows(rowindex).ItemArray.Clone

                AllDataSet.Tables(0).Rows.Add(jj)
            Next
        Next
    End Sub

    Private Sub MultiFileThread_Complete() Handles MultiFileThread.RunWorkerCompleted
        CurrProcessingState = 2
    End Sub
End Class
