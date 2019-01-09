Imports System.Globalization
Imports System.Resources
Imports System.Windows.Forms
Imports ColumnFileViewEncodingHelper
Imports ColumnFileViewImportFileHandler
Imports ColumnFileViewLanguageHelper
Imports ColumnFileViewThreadingHelper

Public Class OpenFileDlg

    Public DefFileList As New List(Of String)
    Public PostProcessingList As New List(Of String)
    Public DefFileCollection As New List(Of FileHandler)
    Public AnalyseDataInstance As New FunctionWrapper
    Public CountLinesInstance As New FunctionWrapper
    Public FilePreviewInstance As New FunctionWrapper
    Public _parent As Form1
    Public DefaultEncIndex As Int64 = 0
    Public AllLinesCnt As Int64 = 0
    Public InitFile As String = ""
    Public InitDefIndex As Integer = -1
    Public CurrentImportFile As String = ""
    Public TargetFolder As String = ""
    Public LoadItems As Boolean = True
    Public MultiTargetFileCollection As New List(Of String)

    Private EncHelper As New EncodingHelper
    Private CultureInf As CultureInfo

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox4.SelectedIndex = 0 Then
            'Plain text

            If ComboBox3.SelectedIndex = 2 Then
                FolderBrowserDialog1.ShowDialog()

                If Not FolderBrowserDialog1.SelectedPath = "" Then
                    TextBox1.Text = FolderBrowserDialog1.SelectedPath
                    TargetFolder = TextBox1.Text
                    CurrentImportFile = IO.Directory.GetFiles(TextBox1.Text)(0)
                Else
                    TargetFolder = ""
                End If

                MultiTargetFileCollection.Clear()
                MultiTargetFileCollection.Add(CurrentImportFile)

                LoadFilePreview(False, True, False)
            Else
                OpenFileDialog1.ShowDialog()
            End If
        End If
    End Sub

    Public Sub AnalyseFile()
        If Not MultiTargetFileCollection.Count = 0 Then
            If AnalyseFileForProfileCtl.Checked Then
                If Not FileRulesCheckThread.IsBusy Then
                    ErrorPanelCtl.Visible = False
                    ProgressBar1.Visible = True
                    FileRulesCheckThread.RunWorkerAsync()
                End If
            End If
        End If
    End Sub

    Public Sub CountFileLines()
        If Not MultiTargetFileCollection.Count = 0 Then
            If CalcLinesCountCtl.Checked Then
                If Not CountFileLinesThread.IsBusy Then
                    Button4.Enabled = False
                    ProgressBar3.Visible = True
                    CountFileLinesThread.RunWorkerAsync()
                End If
            Else
                AllLinesCnt = 0
                SetLinesCountToGUI()
            End If
        End If
    End Sub

    Public Sub GenerateFilePreview(Optional ByVal Override As Boolean = False)
        If Not MultiTargetFileCollection.Count = 0 Then
            If ViewPreviewCtl.Checked Or Override Then
                NotificationPanelCtl.Visible = False
                If Not PreviewDataThread.IsBusy Then
                    DataGridView1.Visible = False
                    ProgressBar2.Visible = True
                    PreviewDataThread.RunWorkerAsync()
                End If
            Else
                NotificationPanelCtl.Visible = True
            End If
        End If
    End Sub

    Public Sub LoadFilePreview(ByVal PostProcessingChange As Boolean, ByVal FileNameChange As Boolean, ByVal ProfileChange As Boolean, Optional ByVal DoAnalyseFile As Boolean = True, Optional ByVal Override As Boolean = False)
        Dim singlefile As New List(Of String)
        If MultiTargetFileCollection.Count = 0 Then
            singlefile.Add("")
        Else
            singlefile.Add(MultiTargetFileCollection(0))
        End If

        If FileNameChange Then
            CountFileLines()
        End If
        If PostProcessingChange = False And FileNameChange = False And ProfileChange = False Then
            If DoAnalyseFile = True Then
                AnalyseFile()
            End If
        Else
            If (PostProcessingChange Or ProfileChange) And (Not singlefile.Count = 0 Or Not singlefile(0) = "") Then
                If DoAnalyseFile Then
                    AnalyseFile()
                End If
                GenerateFilePreview(Override)
            End If
        End If
    End Sub

    Public Sub HandleFileSelection()
        MultiTargetFileCollection.Clear()

        If Not InitFile = "" Then
            CurrentImportFile = InitFile
            MultiTargetFileCollection.Add(CurrentImportFile)
        End If

        Try
            For ind = 0 To OpenFileDialog1.FileNames.Count - 1
                MultiTargetFileCollection.Add(OpenFileDialog1.FileNames(ind))
            Next

            If InitFile = "" Then
                CurrentImportFile = OpenFileDialog1.FileNames(0)
            End If

            If OpenFileDialog1.FileNames.Count > 1 Then
                TextBox1.Text = "(" & (OpenFileDialog1.FileNames.Count) & " Dateien)"
                CurrentImportFile = MultiTargetFileCollection(0)
                ComboBox3.SelectedIndex = 1
            Else
                TextBox1.Text = OpenFileDialog1.FileName
                CurrentImportFile = TextBox1.Text
                ComboBox3.SelectedIndex = 0
            End If
        Catch ex As Exception
        End Try

        StartGUIDataPreview()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        HandleFileSelection()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim gg As New AddModifyDefFileDlg
        gg.DefObj = New FileHandler
        gg.ShowDialog()

        Dim ss As New FileSerializer
        ss.SaveDefFile(My.Application.Info.DirectoryPath & "\Templates\" & gg.TextBox1.Text, gg.DefObj)

        LoadTemplateDefinitions()
    End Sub

    Public Function LoadDefFiles(ByVal Path As String) As Boolean
        Try

            For Each item As String In IO.Directory.GetFiles(Path)
                Try
                    Dim aa As New FileSerializer
                    Dim ww As FileHandler
                    ww = aa.LoadDefFile(item)

                    ComboBox1.Items.Add(ww.DisplayName)
                    DefFileList.Add(item)
                    DefFileCollection.Add(ww)
                Catch ex As Exception
                End Try
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadDefaultDefFile(ByVal DefaultFile As String) As Boolean
        Try
            Dim aa As New FileSerializer
            Dim ww As FileHandler
            ww = aa.LoadDefFile(DefaultFile)

            ComboBox1.Items.Add(ww.DisplayName)
            DefFileList.Add(DefaultFile)
            DefFileCollection.Add(ww)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub LoadAllEncodings()
        Dim pp As New EncodingHelper

        ComboBox2.Items.AddRange(pp.GetAllEncodingsAsStringArray().ToArray)
        ComboBox2.SelectedIndex = pp.GetDefaultEncodingIndex
    End Sub

    Private Sub OpenFileDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CultureInf = CultureInfo.CurrentUICulture

        Dim ll As New LanguageApplyHelper
        ll.ApplyCultureToGUI("ColumnFileView.NET.AppStrings", GetType(OpenFileDlg), CultureInf, Me)

        SetInitState()
        StartLoadEncodingItems()
        StartLoadGUIPostprocessingItems()

        LoadTemplateDefinitions()

        MultitreadingCountCtl.Value = _parent.AppFunctionWrapper.TManager.GetProcessorThreadCount()
        If MultitreadingCountCtl.Value = 1 Then
            UseMultithreadingCtl.Checked = False
        End If

        If Not InitFile = "" Then
            TextBox1.Text = InitFile
            HandleFileSelection()
        End If
    End Sub

    Public Sub LoadTemplateDefinitions()
        LoadItems = True

        ComboBox1.Items.Clear()
        DefFileList.Clear()
        DefFileCollection.Clear()

        LoadDefaultDefFile(My.Application.Info.DirectoryPath & "\Default.xml")
        LoadDefFiles(My.Application.Info.DirectoryPath & "\Templates")

        ComboBox1.SelectedIndex = 0

        LoadItems = False
    End Sub

    Public Sub SetInitState()
        ComboBox3.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
    End Sub

    Public Sub StartGUIDataPreview()
        If Not CurrentImportFile = "" Or ComboBox3.SelectedIndex = 2 Then
            If CalcLinesCountCtl.Checked Then
                CountFileLines()
            Else
                AllLinesCnt = -1
                ToLineCtl.Value = -1
            End If
        End If
    End Sub

    Public Sub StartLoadEncodingItems()
        ComboBox2.Enabled = False
        ComboBox2.Items.Clear()
        LoadAllEncodingsThread.RunWorkerAsync()
    End Sub

    Public Sub StartLoadGUIPostprocessingItems()
        Button11.Enabled = False
        Button12.Enabled = False
        Button13.Enabled = False
        ListView1.BeginUpdate()
        ListView1.Items.Clear()
        PostProcessingList.Clear()
        LoadPostprocessingFilesThread.RunWorkerAsync(My.Application.Info.DirectoryPath & "\Postprocessing")
    End Sub

    Public Sub EndLoadEncodingItems()
        If EncodingAutoDetectCtl.Checked = False Then
            ComboBox2.Enabled = True
        End If
    End Sub

    Public Sub EndLoadGUIPostprocessingItems()
        ListView1.EndUpdate()
        Button11.Enabled = True
        Button12.Enabled = True
        Button13.Enabled = True
    End Sub

    Public Function LoadProfileSpecProcessingActions(ByVal ProfileDef As FileHandler) As Boolean
        Try
            LoadItems = True

            Dim speca As List(Of ProcessingItem)
            speca = ProfileDef.ProfileSpecificPostActions

            If speca.Count = 0 Then
                Return False
            Else
                Return FillListViewWithPostProcItems(speca, ListView1, 0)
            End If

            LoadItems = False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ClearProfileSpecProcessingActions(ByVal ListViewCtl As ListView) As Boolean
        Try
            For index = 0 To ListViewCtl.Items.Count - 1
                If ListViewCtl.Items(index).Group.Name = ListViewCtl.Groups.Item(0).Name Then
                    ListViewCtl.Items.RemoveAt(index)
                End If
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ClearProfileSpecProcessingActions(ListView1)
        LoadProfileSpecProcessingActions(DefFileCollection(ComboBox1.SelectedIndex))

        If LoadItems = False Then
            LoadFilePreview(False, False, True, False)
        Else
            LoadFilePreview(False, False, True, False)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim aa As New FileSerializer
        Dim ww As FileHandler
        ww = aa.LoadDefFile(DefFileList(ComboBox1.SelectedIndex))
        Dim jj As New AddModifyDefFileDlg
        jj.DefObj = ww
        jj.FileNamePath = DefFileList(ComboBox1.SelectedIndex)
        jj.TextBox1.Enabled = False

        jj.ShowDialog()

        aa.SaveDefFile(DefFileList(ComboBox1.SelectedIndex), jj.DefObj)
    End Sub

    Public Function GetProcessingListFromListView(ByVal ListViewCtl As ListView) As List(Of ProcessingItem)
        Try
            Dim processingitems As New List(Of ProcessingItem)
            If Not ListViewCtl.CheckedItems.Count = 0 Then
                For index = 0 To ListViewCtl.CheckedItems.Count - 1
                    processingitems.Add(ListViewCtl.CheckedItems(index).Tag)
                Next
            End If

            Return processingitems
        Catch ex As Exception
            Return New List(Of ProcessingItem)
        End Try
    End Function

    Public Function GetProcessingItemsFromFiles(ByVal PostProcessingDefPath As String) As List(Of ProcessingItem)
        Try
            Dim processingitems As New List(Of ProcessingItem)
            Dim postdeser As New ProcessingFileSerializer
            For Each item As String In IO.Directory.GetFiles(PostProcessingDefPath)
                PostProcessingList.Add(item)
                processingitems.Add(postdeser.LoadDefFile(item))
            Next

            Return processingitems
        Catch ex As Exception
            Return New List(Of ProcessingItem)
        End Try
    End Function

    Public Function FillListViewWithPostProcItems(ByVal itms As List(Of ProcessingItem), ByVal Ctl As ListView, Optional ByVal GroupIndex As Integer = 0) As Boolean
        Try
            For index = 0 To itms.Count - 1
                Dim jj As New ListViewItem
                jj.Text = itms(index).Name

                If itms(index).ProcessingType = ProcessingItem.ProcessingTypeEnum.ReplaceChars Then
                    If itms(index).NewCharIsNothing Then
                        jj.SubItems.Add("Entferne Zeichen " & itms(index).OldChar)
                    Else
                        jj.SubItems.Add("Ersetze Zeichen " & itms(index).OldChar & " mit Zeichen " & itms(index).NewChar)
                    End If
                End If
                If itms(index).ProcessingType = ProcessingItem.ProcessingTypeEnum.ReplaceDateTimeFormat Then
                    jj.SubItems.Add("Ersetze Datum/Uhrzeit im Format " & itms(index).OldDateTimeFormat & " durch Format " & itms(index).NewDateTimeFormat)
                End If
                If itms(index).ProcessingType = ProcessingItem.ProcessingTypeEnum.ConvertFilesizeInReadableFormat Then
                    jj.SubItems.Add("Konvertiere Byteangaben in lesbares Format")
                End If

                jj.Tag = itms(index)
                jj.Checked = itms(index).Activated
                jj.Group = Ctl.Groups(GroupIndex)

                Ctl.Items.Add(jj)
            Next

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        _parent.ResizeStatusPanel.Start()

        Dim uu As New EncodingHelper

        If UseMultithreadingCtl.Checked Then
            Dim ll As FileHandler
            Dim oo As New FileSerializer
            ll = oo.LoadDefFile(DefFileList(ComboBox1.SelectedIndex))
            _parent._ProfileFile = DefFileList(ComboBox1.SelectedIndex)
            _parent._ProfileDefinition = ll
            _parent.FilterExtendedDataGridView1._ProfileFilename = DefFileList(ComboBox1.SelectedIndex)
            _parent.FilterExtendedDataGridView1._ProfileDefinition = ll

            _parent.AppFunctionWrapper.StartAction(ThreadingWorkerItem.AvActions.TransformData,
                                                   ThreadingWorkerItem.AvDestinations.InternalDataset,
                                                   MultiFileImportManager.FileExportMode.Combined,
                                                   New DataSet,
                                                   MultiTargetFileCollection,
                                                   TargetFolder,
                                                   DefFileList(ComboBox1.SelectedIndex),
                                                   DefFileCollection,
                                                   ll,
                                                   "",
                                                   uu.GetEncodingFromIndex(ComboBox2.SelectedIndex),
                                                   EncodingAutoDetectCtl.Checked,
                                                   FromLineCtl.Value,
                                                   LastNLinesCtl.Value,
                                                   FromLineOptionCtl.Checked,
                                                   ToLineCtl.Value,
                                                   -1,
                                                   MultitreadingCountCtl.Value,
                                                   GetProcessingListFromListView(ListView1),
                                                   ProcessMultipleFilesWithThreadingCtl.Checked,
                                                   1,
                                                   1)

            'Alle Threads sind gestartet, jetzt warten, bis alle Threads beendet sind:
            _parent._UseMultiThreading = True
            _parent._ProfileFile = DefFileList(ComboBox1.SelectedIndex)

            _parent.UseWaitCursor = True
            Dim qq As New ProcessingCtl
            _parent._ProcessingDlg = qq
            _parent._ProfileFile = DefFileList(ComboBox1.SelectedIndex)
            Dim zz As New ImportHandler

            zz.LineCount = -1
            qq._ImportHandleObj = zz

            Dim gg As New ThreadingWorkerItem
            gg.PostProccessingItems = GetProcessingListFromListView(ListView1)
            gg.PostProccessingDirectMode = False
            qq._ImportThread = gg
            qq._parent = _parent

            _parent.FlowLayoutPanel1.Controls.Add(qq)

            _parent.BackgroundWorker1.RunWorkerAsync()
        Else
            Dim aa As New ThreadingWorkerItem
            aa.Action = ThreadingWorkerItem.AvActions.TransformData
            aa.TargetFilePath = TextBox1.Text
            aa.Destination = ThreadingWorkerItem.AvDestinations.InternalDataset
            aa.LoadToEnd = LoadToEndCtl.Checked
            If FromLineOptionCtl.Checked = True Then
                aa.StartAt = FromLineCtl.Value
                aa.MaxLines = ToLineCtl.Value
            Else
                aa.LoadToEnd = False
                aa.StartAt = (AllLinesCnt - LastNLinesCtl.Value)
                aa.MaxLines = (AllLinesCnt - 1)
            End If

            aa.TargetFileEncoding = uu.GetEncodingFromIndex(ComboBox2.SelectedIndex)
            aa.TargetFileEncodingAutoDiscover = EncodingAutoDetectCtl.Checked

            aa.PostProccessingDirectMode = ProcessPostprocessingDirectCtl.Checked
            aa.JumpToEnd = JumpToEndCtl.Checked

            aa.PostProccessingItems = GetProcessingListFromListView(ListView1)

            If _parent.BackgroundWorker1.IsBusy = False Then
                _parent.BindingSource1.SuspendBinding()
                _parent.ViewData.Tables.Clear()

                _parent.BackgroundWorker1.RunWorkerAsync(aa)
            End If

            _parent.UseWaitCursor = True
            Dim qq As New ProcessingCtl
            _parent._ProcessingDlg = qq
            _parent._ProfileFile = DefFileList(ComboBox1.SelectedIndex)
            aa.ImportHandle.LineCount = AllLinesCnt
            qq._ImportHandleObj = aa.ImportHandle
            qq._ImportThread = aa
            _parent.FlowLayoutPanel1.Controls.Add(qq)
        End If

        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles LoadToEndCtl.CheckedChanged
        If LoadItems = False Then
            If LoadToEndCtl.Checked = True Then
                ToLineCtl.Enabled = False
            Else
                ToLineCtl.Enabled = True
            End If
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles FromLineCtl.ValueChanged
        LoadFilePreview(False, False, True)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If LoadAllEncodingsThread.IsBusy = False Then
            LoadFilePreview(True, False, False)
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles EncodingAutoDetectCtl.CheckedChanged
        If EncodingAutoDetectCtl.Checked Then
            ComboBox2.Enabled = False
        Else
            ComboBox2.Enabled = True
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles CountFileLinesThread.DoWork
        Dim singlefile As New List(Of String)
        singlefile.Add(MultiTargetFileCollection(0))

        CountLinesInstance.StartAction(ThreadingWorkerItem.AvActions.CountFileLines, ThreadingWorkerItem.AvDestinations.InternalDataset, MultiFileImportManager.FileExportMode.Combined, New DataSet, singlefile, TargetFolder, "", New List(Of FileHandler), New FileHandler,
"", EncHelper.GetEncodingFromIndex(EncHelper.GetDefaultEncodingIndex), EncodingAutoDetectCtl.Checked, FromLineCtl.Value, LastNLinesCtl.Value, FromLineOptionCtl.Checked, ToLineCtl.Value, -1, MultitreadingCountCtl.Value, New List(Of ProcessingItem), ProcessMultipleFilesWithThreadingCtl.Checked, 1, 1)

        Dim doloop2 As Boolean = True
        Do While doloop2 = True
            Threading.Thread.Sleep(1)
            doloop2 = False
            If Not CountLinesInstance.TManager.CurrProcessingState = 6 Then
                doloop2 = True
            End If
        Loop
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles CountFileLinesThread.RunWorkerCompleted
        Dim oldcnt As Int64 = 0
        oldcnt = AllLinesCnt

        SetLinesCountToGUI()

        ProgressBar3.Visible = False
        Button4.Enabled = True

        If Not oldcnt = AllLinesCnt Then
            LoadFilePreview(False, False, False)
        End If
    End Sub

    Public Sub SetLinesCountToGUI()
        LoadItems = True

        ToLineCtl.Value = CountLinesInstance.TManager.Output64
        AllLinesCnt = CountLinesInstance.TManager.Output64

        LoadItems = False
    End Sub


    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles LastNLinesOptionCtl.CheckedChanged
        If LastNLinesOptionCtl.Checked Then
            FromLineCtl.Enabled = False
            LoadToEndCtl.Enabled = False
            LastNLinesCtl.Enabled = True
            Button6.Enabled = True
            Button7.Enabled = True
            Button8.Enabled = True
            Button9.Enabled = True
            Button10.Enabled = True
        Else
            FromLineCtl.Enabled = True
            LoadToEndCtl.Enabled = True
            LastNLinesCtl.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
        End If
    End Sub

    Private Sub ListView1_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles ListView1.ItemChecked
        If LoadItems = False Then
            LoadFilePreview(True, False, False)
        Else
            LoadFilePreview(True, False, False, False)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        LastNLinesCtl.Value = 500
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        LastNLinesCtl.Value = 1000
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        LastNLinesCtl.Value = 2500
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        LastNLinesCtl.Value = 5000
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        LastNLinesCtl.Value = 10000
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles FileRulesCheckThread.DoWork
        If LoadAllEncodingsThread.IsBusy Then
            'Warten, bis alle Encodings des OS geladen wurden
            Dim doloop3 As Boolean = True
            Do While doloop3 = True
                Threading.Thread.Sleep(1)
                doloop3 = False
                If LoadAllEncodingsThread.IsBusy Then
                    doloop3 = True
                End If
            Loop
        End If

        If CountFileLinesThread.IsBusy Then
            'Warten, bis die Anzahl der Zeilen berechnet ist
            Dim doloop1 As Boolean = True
            Do While doloop1 = True
                Threading.Thread.Sleep(1)
                doloop1 = False
                If CountFileLinesThread.IsBusy Then
                    doloop1 = True
                End If
            Loop

        End If

        Dim singlefile As New List(Of String)
        singlefile.Add(MultiTargetFileCollection(0))

        AnalyseDataInstance.StartAction(ThreadingWorkerItem.AvActions.CheckForFileProfileRules,
                                        ThreadingWorkerItem.AvDestinations.InternalDataset,
                                        MultiFileImportManager.FileExportMode.Combined,
                                        New DataSet,
                                        singlefile,
                                        "",
                                        "",
                                        DefFileCollection,
                                        New FileHandler,
                                        "",
                                        System.Text.Encoding.GetEncodings(ComboBox2.SelectedIndex),
                                        EncodingAutoDetectCtl.Checked,
                                        0,
                                        10,
                                        False,
                                        -1,
                                        -1,
                                        1,
                                        New List(Of ProcessingItem),
                                        False,
                                        1,
                                        1,
                                        False)

        Dim doloop2 As Boolean = True
        Do While doloop2 = True
            Threading.Thread.Sleep(1)
            doloop2 = False
            If Not AnalyseDataInstance.TManager.CurrProcessingState = 6 Then
                doloop2 = True
            End If
        Loop
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles FileRulesCheckThread.RunWorkerCompleted
        Dim oldint As Integer
        oldint = ComboBox1.SelectedIndex

        If Not AnalyseDataInstance.TManager.Output64 = -1 Then
            If Not oldint = AnalyseDataInstance.TManager.Output64 Then
                ComboBox1.SelectedIndex = AnalyseDataInstance.TManager.Output64

                LoadFilePreview(False, False, True, False)
            End If
        Else
            'Es passt keine Profildatei für die Datei, Default-Profil wählen
            If Not ComboBox1.SelectedIndex = 0 Then
                ComboBox1.SelectedIndex = 0
            End If
        End If

        ProgressBar1.Visible = False
    End Sub

    Private Sub BackgroundWorker4_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadPostprocessingFilesThread.DoWork
        e.Result = GetProcessingItemsFromFiles(e.Argument)
    End Sub

    Private Sub BackgroundWorker4_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadPostprocessingFilesThread.RunWorkerCompleted
        FillListViewWithPostProcItems(e.Result, ListView1, 1)
        EndLoadGUIPostprocessingItems()
    End Sub

    Private Sub BackgroundWorker5_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles LoadAllEncodingsThread.DoWork
        CheckForIllegalCrossThreadCalls = False

        LoadAllEncodings()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim gg As New AddModifyDefFileDlg
        gg.DefObj = New ProcessingItem
        gg.ShowDialog()

        Dim ss As New ProcessingFileSerializer
        ss.SaveDefFile(My.Application.Info.DirectoryPath & "\Postprocessing\" & gg.TextBox1.Text, gg.DefObj)

        StartLoadGUIPostprocessingItems()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim aa As New ProcessingFileSerializer
        Dim ww As ProcessingItem
        ww = aa.LoadDefFile(PostProcessingList(ListView1.SelectedItems(0).Index))
        Dim jj As New AddModifyDefFileDlg
        jj.DefObj = ww
        jj.FileNamePath = PostProcessingList(ListView1.SelectedItems(0).Index)
        jj.TextBox1.Enabled = False

        jj.ShowDialog()

        aa.SaveDefFile(PostProcessingList(ListView1.SelectedItems(0).Index), jj.DefObj)
    End Sub

    Private Sub BackgroundWorker5_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadAllEncodingsThread.RunWorkerCompleted
        EndLoadEncodingItems()
    End Sub

    Private Sub BackgroundWorker3_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles PreviewDataThread.DoWork
        PreviewData.Tables.Clear()

        Dim doloop1 As Boolean = True
        Do While doloop1 = True
            Threading.Thread.Sleep(1)
            doloop1 = False
            If Not DefFileCollection.Count = DefFileList.Count Then
                doloop1 = True
            End If
        Loop

        Dim prehandl As FileHandler
        prehandl = DefFileCollection(ComboBox1.SelectedIndex)

        Dim singlefile As New List(Of String)
        singlefile.Add(MultiTargetFileCollection(0))

        If FromLineOptionCtl.Checked Then
            FilePreviewInstance.StartAction(ThreadingWorkerItem.AvActions.TransformData,
                                                                ThreadingWorkerItem.AvDestinations.InternalDataset,
                                                                MultiFileImportManager.FileExportMode.Combined,
                                                                New DataSet,
                                                                singlefile,
                                                                "",
                                                                "",
                                                                DefFileCollection,
                                                                prehandl,
                                                                "",
                                                                System.Text.Encoding.GetEncodings(ComboBox2.SelectedIndex),
                                                               EncodingAutoDetectCtl.Checked,
                                                               FromLineCtl.Value,
                                                               0,
                                                               True,
                                                               5,
                                                               -1,
                                                               MultitreadingCountCtl.Value,
                                                               GetProcessingListFromListView(ListView1),
                                                               ProcessMultipleFilesWithThreadingCtl.Checked,
                                                               1,
                                                               1,
                                                               False)
        Else
            FilePreviewInstance.StartAction(ThreadingWorkerItem.AvActions.TransformData,
                                                                ThreadingWorkerItem.AvDestinations.InternalDataset,
                                                                MultiFileImportManager.FileExportMode.Combined,
                                                                New DataSet,
                                                                singlefile,
                                                                "",
                                                                "",
                                                                DefFileCollection,
                                                                prehandl,
                                                                "",
                                                                System.Text.Encoding.GetEncodings(ComboBox2.SelectedIndex),
                                                               EncodingAutoDetectCtl.Checked,
                                                               FromLineCtl.Value,
                                                               LastNLinesCtl.Value,
                                                               FromLineOptionCtl.Checked,
                                                               AllLinesCnt,
                                                               -1,
                                                               MultitreadingCountCtl.Value,
                                                               GetProcessingListFromListView(ListView1),
                                                               ProcessMultipleFilesWithThreadingCtl.Checked,
                                                               1,
                                                               1,
                                                               False)
        End If


        Dim doloop2 As Boolean = True
        Do While doloop2 = True
            Threading.Thread.Sleep(1)
            doloop2 = False
            If Not FilePreviewInstance.TManager.CurrProcessingState = 6 Then
                doloop2 = True
            End If
        Loop
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex = 0 Then
            'OpenFileDialog1.Multiselect = False
        End If
        If ComboBox3.SelectedIndex = 1 Then
            OpenFileDialog1.Multiselect = True
        End If
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        Button1.PerformClick()
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles PreviewDataThread.RunWorkerCompleted
        PreviewData = FilePreviewInstance.TManager.AllDataSet

        If PreviewData.Tables.Count = 0 Then
            ErrorPanelCtl.Visible = True
        Else
            ErrorPanelCtl.Visible = False
            DataGridView1.DataSource = PreviewData.Tables(0)

            'Formatierung zurücksetzen
            _parent.PreSetViewSettings(DataGridView1, New FileHandler)

            'Formatierung übernehmen
            _parent.PreSetViewSettings(DataGridView1, DefFileCollection(ComboBox1.SelectedIndex))

            'Spalten automatisch anpassen
            DataGridView1.AutoResizeColumns()
        End If

        ProgressBar2.Visible = False
        DataGridView1.Visible = True
    End Sub

    Private Sub NumericUpDown3_ValueChanged(sender As Object, e As EventArgs) Handles LastNLinesCtl.ValueChanged
        If LoadItems = False Then
            LoadFilePreview(False, False, True)
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles FromLineOptionCtl.CheckedChanged
        If LoadItems = False Then
            LoadFilePreview(False, False, True)
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        LoadFilePreview(False, False, True, True, True)
    End Sub

    Private Sub ViewPreviewCtl_CheckedChanged(sender As Object, e As EventArgs) Handles ViewPreviewCtl.CheckedChanged
        If ViewPreviewCtl.Checked And LoadItems = False Then
            LoadFilePreview(False, False, True)
        End If
    End Sub

    Private Sub AnalyseFileForProfileCtl_CheckedChanged(sender As Object, e As EventArgs) Handles AnalyseFileForProfileCtl.CheckedChanged
        If AnalyseFileForProfileCtl.Checked And LoadItems = False Then
            LoadFilePreview(False, False, True)
        End If
    End Sub

    Private Sub CalcLinesCountCtl_CheckedChanged(sender As Object, e As EventArgs) Handles CalcLinesCountCtl.CheckedChanged
        If CalcLinesCountCtl.Checked And LoadItems = False Then
            LoadFilePreview(False, True, False)
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim ii As New DataExWindowFrm
        ii.Show()
        ii.DataGridView1.DataSource = PreviewData.Tables(0)

        'Formatierung übernehmen
        _parent.PreSetViewSettings(ii.DataGridView1, DefFileCollection(ComboBox1.SelectedIndex))

        'Spalten automatisch anpassen
        ii.DataGridView1.AutoResizeColumns()
    End Sub
End Class
