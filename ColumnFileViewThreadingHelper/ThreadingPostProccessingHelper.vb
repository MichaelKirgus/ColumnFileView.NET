'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.ComponentModel
Imports ColumnFileViewImportFileHandler

Public Class ThreadingPostProccessingHelper
    Public Data As New DataSet
    Public LinesCnt As Int64 = 0
    Public CurrentLineCnt As Int64 = 0
    Public ProcessingItems As List(Of ProcessingItem)
    Public ProccessingHandler As New PostProcessingHandler
    Public WithEvents ThreadingPostProcessWorker As New BackgroundWorker
    Public ProcessingThreadState As Integer = -1

    Private Sub ThreadingPostProcessWorker_DoWork() Handles ThreadingPostProcessWorker.DoWork
        ProcessingThreadState = 1
        RunAction()
        ProcessingThreadState = 2
    End Sub

    Public Function RunAction(Optional ByVal Async As Boolean = False) As DataSet
        If Async Then
            ThreadingPostProcessWorker.RunWorkerAsync()
        Else
            Try
                Dim newlinescnt As Int64
                newlinescnt = Data.Tables(0).Rows.Count
                LinesCnt = newlinescnt
                ProccessingHandler.LinesCnt = newlinescnt
                Return ProccessingHandler.ProcessItems(Data, ProcessingItems)
            Catch ex As Exception
                Return Data
            End Try
        End If
    End Function
End Class
