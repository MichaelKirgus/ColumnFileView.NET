'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Reflection
Imports ColumnFileViewExportHandler
Imports ColumnFileViewImportFileHandler
Imports ColumnFileViewSettingsHandler
Imports ColumnFileViewThreadingHelper
Imports DataGridViewAutoFilter
Imports DataGridViewControls

Public Class Form1
    Public _ProfileFile As String = ""
    Public _ProfileDefinition As New FileHandler
    Public _Settings As New SettingsClass
    Public _ColumnDefPopulated As Boolean = False
    Public _ProcessingDlg As ProcessingCtl
    Public _LastGridColor As Color
    Public _IsChild As Boolean = False
    Public _UseMultiThreading As Boolean = False

    Public AppFunctionWrapper As New FunctionWrapper
    Private InstanceSettingsManager As New SettingsManager

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterExtendedDataGridView1.DoubleBufferOverride = True
        FilterExtendedDataGridView1._parent = Me
        FilterExtendedDataGridView1._ProfileDefinition = _ProfileDefinition

        _Settings = InstanceSettingsManager.LoadSettings(SettingsManager.SettingsFileDestination.AppDataCurrentUser, "", True, "")

        If _IsChild = False Then
            If My.Application.CommandLineArgs.Count = 0 Then
                Dim qq As New OpenFileDlg
                qq._parent = Me
                qq.ShowDialog()
            Else
                Try
                    Dim DEFINDEX As Integer = -1
                    For Each argument In My.Application.CommandLineArgs
                        If argument.StartsWith("/DEFINDEX") Then
                            Try
                                Dim jj As Array
                                jj = argument.Split(":")
                                DEFINDEX = argument(1).ToString
                            Catch ex As Exception
                            End Try
                        Else
                            If IO.File.Exists(argument) Then
                                Dim qq As New OpenFileDlg
                                qq._parent = Me
                                qq.InitFile = argument
                                qq.OpenFileDialog1.FileName = argument
                                qq.InitDefIndex = DEFINDEX
                                qq.HandleFileSelection()
                                qq.ShowDialog()
                            End If
                        End If
                    Next
                Catch ex As Exception
                End Try
            End If
        Else
            ResizeStatusPanel.Start()
            CheckProfileGUIModifications()
            FilterExtendedDataGridView1.AddFilterControls()
        End If
    End Sub

    Public Sub CheckProfileGUIModifications()
        If _ProfileDefinition.ArrangeColumnsAfterLoad Then
            PreSetViewSettings(FilterExtendedDataGridView1.DataGridViewCtl, _ProfileDefinition)

            ToolStripButton13.Checked = True

            Dim tryresize As Boolean
            tryresize = AutoArrangCollumnsHorizon()

            If tryresize = False Then
                If BackgroundWorker3.IsBusy = False Then
                    BackgroundWorker3.RunWorkerAsync()
                End If
            End If
        End If
    End Sub


    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        Dim qq As New OpenFileDlg
        qq._parent = Me
        qq.ShowDialog()
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs)
        If _IsChild Then
            Dim hh As New ExportHandler
            Dim pp As DataTable
            pp = BindingSource1.DataSource
            hh.ExportResults(pp.DataSet, "", 0)
        Else
            Dim hh As New ExportHandler
            Dim pp As DataTable
            pp = BindingSource1.DataSource
            hh.ExportResults(pp.DataSet, "", 0)
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs)
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        If SaveFileDialog1.FilterIndex = 1 Then
            Dim hh As New ExportHandler
            Dim pp As DataTable
            pp = BindingSource1.DataSource
            hh.ExportResults(pp.DataSet, SaveFileDialog1.FileName, 3)
        End If
        If SaveFileDialog1.FilterIndex = 2 Then
            Dim hh As New ExportHandler
            Dim pp As DataTable
            pp = BindingSource1.DataSource
            hh.ExportResults(pp.DataSet, SaveFileDialog1.FileName, 1)
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        If _UseMultiThreading Then
            'Warten bis alle Threads bearbeitet:
            If AppFunctionWrapper.UsedManagerID = 1 Then
                Dim doloop2 As Boolean = True
                Do While doloop2 = True
                    Threading.Thread.Sleep(10)
                    doloop2 = False
                    If Not AppFunctionWrapper.TMultipleFilesManager.CurrProcessingState = 2 Then
                        doloop2 = True
                    End If
                Loop
            End If
            If AppFunctionWrapper.UsedManagerID = 2 Then
                Dim doloop2 As Boolean = True
                Do While doloop2 = True
                    Threading.Thread.Sleep(10)
                    doloop2 = False
                    If Not AppFunctionWrapper.TManager.CurrProcessingState = 6 Then
                        doloop2 = True
                    End If
                Loop
            End If
        Else
            Dim jj As ThreadingWorkerItem
            jj = e.Argument
            jj.RunAction()

            If jj.Destination = ThreadingWorkerItem.AvDestinations.InternalDataset Then
                e.Result = jj.OutputData
            End If
        End If
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If _ProcessingDlg._ImportThread.PostProccessingDirectMode Then
        Else
            If _UseMultiThreading Then
                If AppFunctionWrapper.UsedManagerID = 1 Then
                    BindingSource1.DataSource = AppFunctionWrapper.TMultipleFilesManager.AllDataSet.Tables(0)
                End If
                If AppFunctionWrapper.UsedManagerID = 2 Then
                    BindingSource1.DataSource = AppFunctionWrapper.TManager.AllDataSet.Tables(0)
                End If
            Else
                BindingSource1.DataSource = e.Result.Tables(0)
            End If

            BindingSource1.ResumeBinding()
            FilterExtendedDataGridView1.DataGridViewCtl.DataSource = BindingSource1
            Me.UseWaitCursor = False

            LoadProfileDefinition()
            FlowLayoutPanel1.Controls.Remove(_ProcessingDlg)
        End If
        If Not _ProcessingDlg._ImportThread.PostProccessingItems.Count = 0 Then
            'PostProcessing muss nur noch ausgeführt werden, wenn keine verschiedenen Threads verwendet wurden
            'Im Multi-Thread-Mode werden die Nachbearbeitungen direkt in den jeweiligen Threads ausgeführt!

            If Not _UseMultiThreading Then
                'Postprocessing muss ausgeführt werden
                If BackgroundWorker2.IsBusy = False Then
                    Dim jj As New ThreadingPostProccessingHelper
                    If _UseMultiThreading Then
                        If AppFunctionWrapper.UsedManagerID = 1 Then
                            jj.Data = AppFunctionWrapper.TMultipleFilesManager.AllDataSet
                        End If
                        If AppFunctionWrapper.UsedManagerID = 2 Then
                            jj.Data = AppFunctionWrapper.TManager.AllDataSet
                        End If
                    Else
                        jj.Data = e.Result
                    End If

                    jj.ProcessingItems = _ProcessingDlg._ImportThread.PostProccessingItems.ToList

                    _ProcessingDlg._PostProccessingThread = jj.ProccessingHandler

                    BackgroundWorker2.RunWorkerAsync(jj)
                    _ProcessingDlg._Loadstate = 1
                End If
            End If

        Else
            'Keine Nachbearbeitung notwendig, direkt Datenquelle anzeigen
            If AppFunctionWrapper.UsedManagerID = 1 Then
                BindingSource1.DataSource = AppFunctionWrapper.TMultipleFilesManager.AllDataSet.Tables(0)
            End If
            If AppFunctionWrapper.UsedManagerID = 2 Then
                BindingSource1.DataSource = AppFunctionWrapper.TManager.AllDataSet.Tables(0)
            End If

            LoadProfileDefinition()
            BindingSource1.ResumeBinding()
            FilterExtendedDataGridView1.DataGridViewCtl.DataSource = BindingSource1
            PreSetViewSettings(FilterExtendedDataGridView1.DataGridViewCtl, _ProfileDefinition)
            PostSetViewSettings()
            FlowLayoutPanel1.Controls.Remove(_ProcessingDlg)
            Me.UseWaitCursor = False
        End If

        CheckProfileGUIModifications()
    End Sub

    Public Function PostSetViewSettings() As Boolean
        Try
            If _ProcessingDlg._ImportThread.JumpToEnd Then
                BindingNavigatorMoveLastItem.PerformClick()
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadProfileDefinition(Optional ByVal ProfileFilePath As String = "") As Boolean
        Dim hlp As New FileSerializer
        If ProfileFilePath = "" Then
            _ProfileDefinition = hlp.LoadDefFile(_ProfileFile)
        Else
            _ProfileDefinition = hlp.LoadDefFile(ProfileFilePath)
        End If

        Return True
    End Function

    Public Function PreSetViewSettings(ByVal DestDatagridViewCtl As ExtendedDataGridView, ByVal Profile As FileHandler) As Boolean
        Try
            If Not Profile.DisplayName = "" Then
                For index = 0 To Profile.ColumnDefinitions.Count - 1
                    DestDatagridViewCtl.Columns(index).HeaderText = Profile.ColumnDefinitions(index).ColumnText
                    DestDatagridViewCtl.Columns(index).AutoSizeMode = Profile.ColumnDefinitions(index).AutoSizeColumnContent
                    DestDatagridViewCtl.Columns(index).Visible = Profile.ColumnDefinitions(index).Visible
                    If _Settings.AlwaysUseColumnsSortFeature Then
                        DestDatagridViewCtl.Columns(index).SortMode = DataGridViewColumnSortMode.Programmatic
                    Else
                        DestDatagridViewCtl.Columns(index).SortMode = Profile.ColumnDefinitions(index).SortingMode
                    End If
                    If Not Profile.ColumnDefinitions(index).ColumnWidth = 0 Then
                        DestDatagridViewCtl.Columns(index).Width = Profile.ColumnDefinitions(index).ColumnWidth
                    End If
                    If Not Profile.ColumnDefinitions(index).ColumnDisplayIndex = -1 Then
                        DestDatagridViewCtl.Columns(index).DisplayIndex = Profile.ColumnDefinitions(index).ColumnDisplayIndex
                    End If
                    DestDatagridViewCtl.Columns(index).CellTemplate.Style.BackColor = Color.FromKnownColor(Profile.ColumnDefinitions(index).CellsBackColor)
                    DestDatagridViewCtl.Columns(index).CellTemplate.Style.ForeColor = Color.FromKnownColor(Profile.ColumnDefinitions(index).CellsTextColor)
                    DestDatagridViewCtl.Columns(index).CellTemplate.Style.Alignment = Profile.ColumnDefinitions(index).CellsTextAlignment
                    DestDatagridViewCtl.Columns(index).CellTemplate.Style.ApplyStyle(DestDatagridViewCtl.Columns(index).CellTemplate.Style)

                    DestDatagridViewCtl.Columns(index).DefaultCellStyle.BackColor = Color.FromKnownColor(Profile.ColumnDefinitions(index).CellsBackColor)
                    DestDatagridViewCtl.Columns(index).DefaultCellStyle.ForeColor = Color.FromKnownColor(Profile.ColumnDefinitions(index).CellsTextColor)
                    DestDatagridViewCtl.Columns(index).DefaultCellStyle.Alignment = Profile.ColumnDefinitions(index).CellsTextAlignment
                    DestDatagridViewCtl.Columns(index).DefaultCellStyle.ApplyStyle(DestDatagridViewCtl.Columns(index).DefaultCellStyle)
                Next
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function FillSearchColumnList() As Boolean
        Try
            ToolStripComboBox1.Items.Clear()
            ToolStripComboBox1.Items.Add("(Alle Spalten)")

            Dim pp As DataTable
            pp = BindingSource1.DataSource

            For index = 0 To pp.Columns.Count - 1
                ToolStripComboBox1.Items.Add(pp.Columns(index).ColumnName)
            Next

            ToolStripComboBox1.SelectedIndex = 0

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetSQLWhereClause(ByVal SearchText As String, ByVal OperatorStr As String, ByVal SearchIndex As Integer) As String
        Try
            Dim pp As DataTable
            pp = BindingSource1.DataSource

            'SearchText prüfen und ggf. anpassen
            SearchText = SearchText.Replace("'", "[']")
            SearchText = SearchText.Replace("`", "'`'")
            SearchText = SearchText.Replace("[", "'['")
            SearchText = SearchText.Replace("]", "']'")
            SearchText = SearchText.Replace("%", "'%'")
            SearchText = SearchText.Replace("*", "[*]")
            SearchText = SearchText.Replace("\", "''")

            If SearchIndex = 0 Then
                Dim SQL As String = ""
                For index = 0 To pp.Columns.Count - 1
                    SQL += "(" & pp.Columns(index).ColumnName & OperatorStr & "'" & SearchText & "')"
                    If Not index = pp.Columns.Count - 1 Then
                        SQL += " OR "
                    End If
                Next

                Return SQL
            Else
                Return pp.Columns(SearchIndex - 1).ColumnName & OperatorStr & "'" & SearchText & "'"
            End If

            Return ""
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        Try
            If ToolStripButton7.Checked Then
                CreateNewInstance(SearchQuery(GetSQLWhereClause(ToolStripTextBox1.Text, " = ", ToolStripComboBox1.SelectedIndex), BindingSource1.DataSource))
            Else
                CreateNewInstance(SearchQuery(GetSQLWhereClause("%" & ToolStripTextBox1.Text & "%", " LIKE ", ToolStripComboBox1.SelectedIndex), BindingSource1.DataSource))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function SearchQuery(ByVal QueryTxt As String, ByVal DataT As DataTable) As DataRow()
        Try
            Return DataT.Select(QueryTxt)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CreateNewInstance(ByVal DataSetX As DataRow()) As Boolean
        Try
            Dim nn As New Form1
            nn._IsChild = True
            nn._ProfileFile = _ProfileFile
            nn.FilterExtendedDataGridView1._ProfileFilename = FilterExtendedDataGridView1._ProfileFilename
            nn.LoadProfileDefinition(nn._ProfileFile)

            nn.Show()

            nn.BindingSource1.DataSource = DataSetX.CopyToDataTable
            nn.FilterExtendedDataGridView1.DataGridViewCtl.DataSource = nn.BindingSource1

            'nn.PreSetViewSettings(nn.FilterExtendedDataGridView1.DataGridViewCtl, nn._ProfileDefinition)
            nn.FilterExtendedDataGridView1.CheckForBindingAndPopulateCache()

            nn.BindingSource1.ResumeBinding()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub DataGridView1_DataSourceChanged(sender As Object, e As EventArgs)
        FillSearchColumnList()
    End Sub

    Public Sub RefreshSQLStatement()
        If ToolStripButton7.Checked Then
            ToolStripTextBox1.ToolTipText = GetSQLWhereClause(ToolStripTextBox1.Text, " = ", ToolStripComboBox1.SelectedIndex)
        Else
            ToolStripTextBox1.ToolTipText = GetSQLWhereClause("%" & ToolStripTextBox1.Text & "%", " LIKE ", ToolStripComboBox1.SelectedIndex)
        End If
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        RefreshSQLStatement()
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        Try
            CreateNewInstance(SearchQuery(ToolStripTextBox1.Text, BindingSource1.DataSource))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        RefreshSQLStatement()
    End Sub

    Private Sub ToolStripButton8_Click(sender As Object, e As EventArgs) Handles ToolStripButton8.Click
        If ToolStripButton8.Checked = True Then
            FilterExtendedDataGridView1.DataGridViewCtl.GridColor = _LastGridColor
        Else
            _LastGridColor = FilterExtendedDataGridView1.DataGridViewCtl.GridColor
            FilterExtendedDataGridView1.DataGridViewCtl.GridColor = Color.White
        End If
    End Sub

    Private Sub ToolStripButton9_Click(sender As Object, e As EventArgs) Handles ToolStripButton9.Click
        FilterExtendedDataGridView1.DataGridViewCtl.AutoResizeColumns()
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try
            Dim uu As ThreadingPostProccessingHelper
            uu = e.Argument

            'Führe Postprocessing aus:
            e.Result = uu.RunAction()
        Catch ex As Exception
            e.Result = e.Argument
        End Try
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        BindingSource1.DataSource = e.Result.Tables(0)
        BindingSource1.ResumeBinding()
        FilterExtendedDataGridView1.DataGridViewCtl.DataSource = BindingSource1

        PreSetViewSettings(FilterExtendedDataGridView1.DataGridViewCtl, _ProfileDefinition)
        PostSetViewSettings()

        FlowLayoutPanel1.Controls.Remove(_ProcessingDlg)
        SplitContainer1.Panel2Collapsed = True
        Me.UseWaitCursor = False
    End Sub

    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        RefreshSQLStatement()
    End Sub

    Private Sub ToolStripButton10_Click(sender As Object, e As EventArgs) Handles ToolStripButton10.Click
        ToolStripComboBox1.SelectedIndex = FilterExtendedDataGridView1.DataGridViewCtl.SelectedCells(0).ColumnIndex + 1
        ToolStripTextBox1.Text = FilterExtendedDataGridView1.DataGridViewCtl.SelectedCells(0).Value
        ToolStripButton5.PerformClick()
    End Sub

    Private Sub ToolStripButton11_Click(sender As Object, e As EventArgs) Handles ToolStripButton11.Click
        Dim jj As New AboutFrm
        jj.ShowDialog()
    End Sub

    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        e.ThrowException = False
    End Sub

    Public Function AutoArrangCollumnsHorizon() As Boolean
        Try
            If _ProfileDefinition.ColumnDefinitions.Count = FilterExtendedDataGridView1.DataGridViewCtl.Columns.Count Then
                If _ProfileDefinition.OptimalColumnSizeIndex = -1 Then
                    If FilterExtendedDataGridView1.DataGridViewCtl.Columns.Count = 1 Then
                        'Die Spalte wird auf die Größe des DataGridViews maximiert
                        FilterExtendedDataGridView1.DataGridViewCtl.Columns(0).Width = (FilterExtendedDataGridView1.DataGridViewCtl.Width - FilterExtendedDataGridView1.DataGridViewCtl.RowHeadersWidth - 18)
                    Else
                        'Es sind mehrere Spalten definiert

                        Dim columncnt As Integer
                        columncnt = FilterExtendedDataGridView1.DataGridViewCtl.Columns.Count

                        Dim hidecols As Integer = 0
                        'Sind Spalten ausgeblendet?
                        For index = 0 To columncnt - 1
                            If FilterExtendedDataGridView1.DataGridViewCtl.Columns(index).Visible = False Then
                                hidecols += 1
                            End If
                        Next

                        Dim ctlwidth As Integer
                        ctlwidth = FilterExtendedDataGridView1.DataGridViewCtl.Width
                        Dim divvalue As Integer
                        divvalue = Math.Round((ctlwidth - FilterExtendedDataGridView1.DataGridViewCtl.RowHeadersWidth - 18) / (columncnt - hidecols), 0)

                        For index = 0 To columncnt - 1
                            FilterExtendedDataGridView1.DataGridViewCtl.Columns(index).Width = divvalue
                        Next
                    End If
                Else
                    Dim columncnt As Integer
                    columncnt = FilterExtendedDataGridView1.DataGridViewCtl.Columns.Count

                    'Den restlichen Platz berechnen:
                    Dim withi As Integer = 0
                    For index = 0 To columncnt - 1
                        If Not index = _ProfileDefinition.OptimalColumnSizeIndex Then
                            If FilterExtendedDataGridView1.DataGridViewCtl.Columns(index).Visible Then
                                withi += FilterExtendedDataGridView1.DataGridViewCtl.Columns(index).Width
                            End If
                        End If
                    Next
                    Dim restw As Integer
                    restw = FilterExtendedDataGridView1.DataGridViewCtl.Width - FilterExtendedDataGridView1.DataGridViewCtl.RowHeadersWidth - withi - 18

                    If restw > 0 Then
                        FilterExtendedDataGridView1.DataGridViewCtl.Columns(_ProfileDefinition.OptimalColumnSizeIndex).Width = restw
                    End If
                End If

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ToolStripButton12_Click(sender As Object, e As EventArgs) Handles ToolStripButton12.Click
        AutoArrangCollumnsHorizon()
    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If ToolStripButton13.Checked Then
            AutoArrangCollumnsHorizon()
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim qq As New OpenFileDlg
        qq._parent = Me
        qq.ShowDialog()
    End Sub

    Private Sub ToolStripButton14_Click(sender As Object, e As EventArgs) Handles ToolStripButton14.Click
        If _IsChild Then
            Dim hh As New ExportHandler
            Dim pp As DataTable
            pp = BindingSource1.DataSource
            hh.ExportResults(pp.DataSet, "", 0)
        Else
            Dim hh As New ExportHandler
            Dim pp As DataTable
            pp = BindingSource1.DataSource
            hh.ExportResults(pp.DataSet, "", 0)
        End If
    End Sub

    Private Sub ToolStripButton15_Click(sender As Object, e As EventArgs) Handles ToolStripButton15.Click
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub ResizeStatusPanel_Tick(sender As Object, e As EventArgs) Handles ResizeStatusPanel.Tick
        ResizeStatusPanel.Stop()

        Dim besth As Integer = 0
        If Not FlowLayoutPanel1.Controls.Count = 0 Then
            'Es sind Elemente vorhanden
            For ind = 0 To FlowLayoutPanel1.Controls.Count - 1
                besth += FlowLayoutPanel1.Controls(ind).Height + 10
            Next
        End If

        If besth = 0 Then
            SplitContainer1.Panel2Collapsed = True
        Else
            SplitContainer1.Panel2Collapsed = False
            Dim maxwanted As Integer
            maxwanted = Math.Round(Me.Height / 3, 0)
            If besth > maxwanted Then
                Dim seth As Integer
                seth = SplitContainer1.Height - maxwanted
                FlowLayoutPanel1.AutoScroll = True
                SplitContainer1.SplitterDistance = seth
            Else
                Dim seth As Integer
                seth = SplitContainer1.Height - besth
                FlowLayoutPanel1.AutoScroll = False
                SplitContainer1.SplitterDistance = seth
            End If

            ResizeStatusPanel.Start()
        End If
    End Sub

    Private Sub BackgroundWorker3_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Dim doloop2 As Boolean = True
        Do While Not AutoArrangCollumnsHorizon()
            Threading.Thread.Sleep(100)
        Loop
    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        FilterExtendedDataGridView1.FilterPanel.Visible = ToolStripButton1.Checked
    End Sub

    Private Sub ToolStripButton2_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        FilterExtendedDataGridView1.DataGridViewCtl.ColumnHeadersVisible = ToolStripButton2.Checked
    End Sub

    Private Sub ToolStripButton3_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        FilterExtendedDataGridView1.DataGridViewCtl.RowHeadersVisible = ToolStripButton3.Checked
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If FilterExtendedDataGridView1._UnsavedProfileChanges Then
            FilterExtendedDataGridView1.SaveDefinitionFileChanges(FilterExtendedDataGridView1._ProfileFilename)
        End If

        InstanceSettingsManager.SaveSettings(_Settings, SettingsManager.SettingsFileDestination.AppDataCurrentUser, "", True, "")
    End Sub

    Private Sub ToolStripButton17_Click(sender As Object, e As EventArgs) Handles ToolStripButton17.Click
        Dim xx As New SettingsFrm
        xx._parent = Me

        xx.ShowDialog()
    End Sub
End Class
