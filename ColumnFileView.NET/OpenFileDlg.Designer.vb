'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OpenFileDlg
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Vorlagenspezifische Nachbearbeitung", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Statische Elemente", System.Windows.Forms.HorizontalAlignment.Left)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.ProgressBar3 = New System.Windows.Forms.ProgressBar()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.AnalyseFileForProfileCtl = New System.Windows.Forms.CheckBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.ViewPreviewCtl = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ErrorPanelCtl = New System.Windows.Forms.Panel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NotificationPanelCtl = New System.Windows.Forms.Panel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New DataGridViewControls.ExtendedDataGridView()
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar()
        Me.errorlbl = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.JumpToEndCtl = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LastNLinesCtl = New System.Windows.Forms.NumericUpDown()
        Me.LastNLinesOptionCtl = New System.Windows.Forms.RadioButton()
        Me.FromLineOptionCtl = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LoadToEndCtl = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToLineCtl = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FromLineCtl = New System.Windows.Forms.NumericUpDown()
        Me.PreviewData = New System.Data.DataSet()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.EncodingAutoDetectCtl = New System.Windows.Forms.CheckBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.CountFileLinesThread = New System.ComponentModel.BackgroundWorker()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.ProcessMultipleFilesWithThreadingCtl = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.MultitreadingCountCtl = New System.Windows.Forms.NumericUpDown()
        Me.UseMultithreadingCtl = New System.Windows.Forms.CheckBox()
        Me.ProcessPostprocessingDirectCtl = New System.Windows.Forms.CheckBox()
        Me.FileRulesCheckThread = New System.ComponentModel.BackgroundWorker()
        Me.PreviewDataThread = New System.ComponentModel.BackgroundWorker()
        Me.LoadPostprocessingFilesThread = New System.ComponentModel.BackgroundWorker()
        Me.LoadAllEncodingsThread = New System.ComponentModel.BackgroundWorker()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.CalcLinesCountCtl = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ErrorPanelCtl.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.NotificationPanelCtl.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.LastNLinesCtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToLineCtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromLineCtl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PreviewData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.MultitreadingCountCtl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(321, 19)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(335, 20)
        Me.TextBox1.TabIndex = 2
        Me.TextBox1.Tag = "file"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ComboBox4)
        Me.GroupBox1.Controls.Add(Me.ProgressBar3)
        Me.GroupBox1.Controls.Add(Me.ComboBox3)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(699, 51)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Quelle"
        '
        'ComboBox4
        '
        Me.ComboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Items.AddRange(New Object() {"Plaintext"})
        Me.ComboBox4.Location = New System.Drawing.Point(162, 19)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(153, 21)
        Me.ComboBox4.TabIndex = 9
        '
        'ProgressBar3
        '
        Me.ProgressBar3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar3.Location = New System.Drawing.Point(7, 40)
        Me.ProgressBar3.MarqueeAnimationSpeed = 200
        Me.ProgressBar3.Name = "ProgressBar3"
        Me.ProgressBar3.Size = New System.Drawing.Size(649, 4)
        Me.ProgressBar3.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar3.TabIndex = 8
        Me.ProgressBar3.Visible = False
        '
        'ComboBox3
        '
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"Einzelne Datei", "Mehrere Dateien", "Ordner"})
        Me.ComboBox3.Location = New System.Drawing.Point(7, 19)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(153, 21)
        Me.ComboBox3.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Button1.Image = Global.ColumnFileView.NET.My.Resources.Resources.document_open_7
        Me.Button1.Location = New System.Drawing.Point(662, 14)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(32, 31)
        Me.Button1.TabIndex = 3
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.ProgressBar1)
        Me.GroupBox2.Controls.Add(Me.Button5)
        Me.GroupBox2.Controls.Add(Me.Button3)
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Controls.Add(Me.AnalyseFileForProfileCtl)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 123)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(699, 52)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Vorlage für die Darstellung"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(6, 15)
        Me.ProgressBar1.MarqueeAnimationSpeed = 200
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(574, 4)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 7
        Me.ProgressBar1.Visible = False
        '
        'Button5
        '
        Me.Button5.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Button5.Image = Global.ColumnFileView.NET.My.Resources.Resources.edit_6
        Me.Button5.Location = New System.Drawing.Point(586, 13)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(32, 31)
        Me.Button5.TabIndex = 6
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Button3.Image = Global.ColumnFileView.NET.My.Resources.Resources.document_new_8
        Me.Button3.Location = New System.Drawing.Point(624, 13)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(32, 31)
        Me.Button3.TabIndex = 5
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Button2.Image = Global.ColumnFileView.NET.My.Resources.Resources.document_open_7
        Me.Button2.Location = New System.Drawing.Point(662, 13)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(32, 31)
        Me.Button2.TabIndex = 4
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(6, 19)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(574, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'AnalyseFileForProfileCtl
        '
        Me.AnalyseFileForProfileCtl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AnalyseFileForProfileCtl.AutoSize = True
        Me.AnalyseFileForProfileCtl.Checked = True
        Me.AnalyseFileForProfileCtl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AnalyseFileForProfileCtl.Location = New System.Drawing.Point(587, -1)
        Me.AnalyseFileForProfileCtl.Name = "AnalyseFileForProfileCtl"
        Me.AnalyseFileForProfileCtl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AnalyseFileForProfileCtl.Size = New System.Drawing.Size(107, 17)
        Me.AnalyseFileForProfileCtl.TabIndex = 11
        Me.AnalyseFileForProfileCtl.Text = "Datei analysieren"
        Me.AnalyseFileForProfileCtl.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.Location = New System.Drawing.Point(579, 689)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(132, 32)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "&Datei öffnen"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Alle Dateien|*.*"
        Me.OpenFileDialog1.Multiselect = True
        Me.OpenFileDialog1.ReadOnlyChecked = True
        Me.OpenFileDialog1.RestoreDirectory = True
        Me.OpenFileDialog1.ShowReadOnly = True
        Me.OpenFileDialog1.SupportMultiDottedExtensions = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.LinkLabel2)
        Me.GroupBox3.Controls.Add(Me.ViewPreviewCtl)
        Me.GroupBox3.Controls.Add(Me.Panel1)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 507)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(699, 176)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Vorschau (Erste 5 Zeilen)"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(142, -1)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LinkLabel2.Size = New System.Drawing.Size(176, 13)
        Me.LinkLabel2.TabIndex = 12
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Erweiterte Ansicht in neuem Fenster"
        '
        'ViewPreviewCtl
        '
        Me.ViewPreviewCtl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ViewPreviewCtl.AutoSize = True
        Me.ViewPreviewCtl.Checked = True
        Me.ViewPreviewCtl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ViewPreviewCtl.Location = New System.Drawing.Point(577, -1)
        Me.ViewPreviewCtl.Name = "ViewPreviewCtl"
        Me.ViewPreviewCtl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ViewPreviewCtl.Size = New System.Drawing.Size(117, 17)
        Me.ViewPreviewCtl.TabIndex = 12
        Me.ViewPreviewCtl.Text = "Vorschau anzeigen"
        Me.ViewPreviewCtl.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ErrorPanelCtl)
        Me.Panel1.Controls.Add(Me.NotificationPanelCtl)
        Me.Panel1.Controls.Add(Me.DataGridView1)
        Me.Panel1.Controls.Add(Me.ProgressBar2)
        Me.Panel1.Controls.Add(Me.errorlbl)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(693, 157)
        Me.Panel1.TabIndex = 3
        '
        'ErrorPanelCtl
        '
        Me.ErrorPanelCtl.BackColor = System.Drawing.Color.Coral
        Me.ErrorPanelCtl.Controls.Add(Me.LinkLabel3)
        Me.ErrorPanelCtl.Controls.Add(Me.PictureBox2)
        Me.ErrorPanelCtl.Controls.Add(Me.Label7)
        Me.ErrorPanelCtl.Dock = System.Windows.Forms.DockStyle.Top
        Me.ErrorPanelCtl.Location = New System.Drawing.Point(0, 4)
        Me.ErrorPanelCtl.Name = "ErrorPanelCtl"
        Me.ErrorPanelCtl.Size = New System.Drawing.Size(693, 22)
        Me.ErrorPanelCtl.TabIndex = 14
        Me.ErrorPanelCtl.Visible = False
        '
        'LinkLabel3
        '
        Me.LinkLabel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Location = New System.Drawing.Point(568, 4)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LinkLabel3.Size = New System.Drawing.Size(119, 13)
        Me.LinkLabel3.TabIndex = 3
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Diagnoseansicht öffnen"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.ColumnFileView.NET.My.Resources.Resources.mail_mark_important_2
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(214, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Die Vorschau kann nicht angezeigt werden."
        '
        'NotificationPanelCtl
        '
        Me.NotificationPanelCtl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NotificationPanelCtl.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.NotificationPanelCtl.Controls.Add(Me.LinkLabel1)
        Me.NotificationPanelCtl.Controls.Add(Me.PictureBox1)
        Me.NotificationPanelCtl.Controls.Add(Me.Label6)
        Me.NotificationPanelCtl.Location = New System.Drawing.Point(0, 0)
        Me.NotificationPanelCtl.Name = "NotificationPanelCtl"
        Me.NotificationPanelCtl.Size = New System.Drawing.Size(693, 22)
        Me.NotificationPanelCtl.TabIndex = 13
        Me.NotificationPanelCtl.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(575, 4)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LinkLabel1.Size = New System.Drawing.Size(114, 13)
        Me.LinkLabel1.TabIndex = 2
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Vorschau aktualisieren"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ColumnFileView.NET.My.Resources.Resources.info_small
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(196, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Die Vorschau ist evtl. nicht mehr aktuell."
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(693, 153)
        Me.DataGridView1.TabIndex = 2
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Dock = System.Windows.Forms.DockStyle.Top
        Me.ProgressBar2.Location = New System.Drawing.Point(0, 0)
        Me.ProgressBar2.MarqueeAnimationSpeed = 200
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(693, 4)
        Me.ProgressBar2.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar2.TabIndex = 12
        Me.ProgressBar2.Visible = False
        '
        'errorlbl
        '
        Me.errorlbl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.errorlbl.Location = New System.Drawing.Point(0, 0)
        Me.errorlbl.Name = "errorlbl"
        Me.errorlbl.Size = New System.Drawing.Size(693, 157)
        Me.errorlbl.TabIndex = 12
        Me.errorlbl.Text = "Es ist ein Fehler bei der Verarbeitung aufgetreten." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Bitte überprüfen Sie die Ein" &
    "stellungen."
        Me.errorlbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.Button10)
        Me.GroupBox4.Controls.Add(Me.Button9)
        Me.GroupBox4.Controls.Add(Me.Button8)
        Me.GroupBox4.Controls.Add(Me.Button7)
        Me.GroupBox4.Controls.Add(Me.Button6)
        Me.GroupBox4.Controls.Add(Me.JumpToEndCtl)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.LastNLinesCtl)
        Me.GroupBox4.Controls.Add(Me.LastNLinesOptionCtl)
        Me.GroupBox4.Controls.Add(Me.FromLineOptionCtl)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.LoadToEndCtl)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.ToLineCtl)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.FromLineCtl)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 357)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(699, 71)
        Me.GroupBox4.TabIndex = 8
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Bereich"
        '
        'Button10
        '
        Me.Button10.Enabled = False
        Me.Button10.Location = New System.Drawing.Point(422, 36)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(52, 23)
        Me.Button10.TabIndex = 17
        Me.Button10.Text = "10000"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Enabled = False
        Me.Button9.Location = New System.Drawing.Point(373, 36)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(43, 23)
        Me.Button9.TabIndex = 16
        Me.Button9.Text = "5000"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Enabled = False
        Me.Button8.Location = New System.Drawing.Point(324, 36)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(43, 23)
        Me.Button8.TabIndex = 15
        Me.Button8.Text = "2500"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Enabled = False
        Me.Button7.Location = New System.Drawing.Point(275, 36)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(43, 23)
        Me.Button7.TabIndex = 14
        Me.Button7.Text = "1000"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Enabled = False
        Me.Button6.Location = New System.Drawing.Point(233, 36)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(36, 23)
        Me.Button6.TabIndex = 13
        Me.Button6.Text = "500"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'JumpToEndCtl
        '
        Me.JumpToEndCtl.AutoSize = True
        Me.JumpToEndCtl.Location = New System.Drawing.Point(554, 28)
        Me.JumpToEndCtl.Name = "JumpToEndCtl"
        Me.JumpToEndCtl.Size = New System.Drawing.Size(139, 17)
        Me.JumpToEndCtl.TabIndex = 12
        Me.JumpToEndCtl.Text = "Springe direkt ans Ende"
        Me.JumpToEndCtl.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(191, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Zeilen"
        '
        'LastNLinesCtl
        '
        Me.LastNLinesCtl.Enabled = False
        Me.LastNLinesCtl.Location = New System.Drawing.Point(78, 39)
        Me.LastNLinesCtl.Maximum = New Decimal(New Integer() {-727379968, 232, 0, 0})
        Me.LastNLinesCtl.Name = "LastNLinesCtl"
        Me.LastNLinesCtl.Size = New System.Drawing.Size(110, 20)
        Me.LastNLinesCtl.TabIndex = 10
        '
        'LastNLinesOptionCtl
        '
        Me.LastNLinesOptionCtl.AutoSize = True
        Me.LastNLinesOptionCtl.Location = New System.Drawing.Point(6, 39)
        Me.LastNLinesOptionCtl.Name = "LastNLinesOptionCtl"
        Me.LastNLinesOptionCtl.Size = New System.Drawing.Size(57, 17)
        Me.LastNLinesOptionCtl.TabIndex = 9
        Me.LastNLinesOptionCtl.Text = "Letzte "
        Me.LastNLinesOptionCtl.UseVisualStyleBackColor = True
        '
        'FromLineOptionCtl
        '
        Me.FromLineOptionCtl.AutoSize = True
        Me.FromLineOptionCtl.Checked = True
        Me.FromLineOptionCtl.Location = New System.Drawing.Point(6, 14)
        Me.FromLineOptionCtl.Name = "FromLineOptionCtl"
        Me.FromLineOptionCtl.Size = New System.Drawing.Size(70, 17)
        Me.FromLineOptionCtl.TabIndex = 8
        Me.FromLineOptionCtl.TabStop = True
        Me.FromLineOptionCtl.Text = "Von Zeile"
        Me.FromLineOptionCtl.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(431, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "(Nullbasierter Index)"
        '
        'LoadToEndCtl
        '
        Me.LoadToEndCtl.AutoSize = True
        Me.LoadToEndCtl.Checked = True
        Me.LoadToEndCtl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LoadToEndCtl.Location = New System.Drawing.Point(377, 15)
        Me.LoadToEndCtl.Name = "LoadToEndCtl"
        Me.LoadToEndCtl.Size = New System.Drawing.Size(51, 17)
        Me.LoadToEndCtl.TabIndex = 6
        Me.LoadToEndCtl.Text = "Ende"
        Me.LoadToEndCtl.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(333, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "laden"
        '
        'ToLineCtl
        '
        Me.ToLineCtl.Enabled = False
        Me.ToLineCtl.Location = New System.Drawing.Point(217, 14)
        Me.ToLineCtl.Maximum = New Decimal(New Integer() {-727379968, 232, 0, 0})
        Me.ToLineCtl.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.ToLineCtl.Name = "ToLineCtl"
        Me.ToLineCtl.Size = New System.Drawing.Size(110, 20)
        Me.ToLineCtl.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(191, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "bis"
        '
        'FromLineCtl
        '
        Me.FromLineCtl.Location = New System.Drawing.Point(78, 14)
        Me.FromLineCtl.Maximum = New Decimal(New Integer() {-727379968, 232, 0, 0})
        Me.FromLineCtl.Name = "FromLineCtl"
        Me.FromLineCtl.Size = New System.Drawing.Size(110, 20)
        Me.FromLineCtl.TabIndex = 1
        '
        'PreviewData
        '
        Me.PreviewData.CaseSensitive = True
        Me.PreviewData.DataSetName = "NewDataSet"
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.EncodingAutoDetectCtl)
        Me.GroupBox5.Controls.Add(Me.ComboBox2)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 69)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(699, 48)
        Me.GroupBox5.TabIndex = 9
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Codierung der Datei"
        '
        'EncodingAutoDetectCtl
        '
        Me.EncodingAutoDetectCtl.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.EncodingAutoDetectCtl.AutoSize = True
        Me.EncodingAutoDetectCtl.Checked = True
        Me.EncodingAutoDetectCtl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.EncodingAutoDetectCtl.Location = New System.Drawing.Point(511, 21)
        Me.EncodingAutoDetectCtl.Name = "EncodingAutoDetectCtl"
        Me.EncodingAutoDetectCtl.Size = New System.Drawing.Size(182, 17)
        Me.EncodingAutoDetectCtl.TabIndex = 2
        Me.EncodingAutoDetectCtl.Text = "Codierung automatisch erkennen"
        Me.EncodingAutoDetectCtl.UseVisualStyleBackColor = True
        '
        'ComboBox2
        '
        Me.ComboBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.Enabled = False
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(6, 19)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(499, 21)
        Me.ComboBox2.TabIndex = 1
        '
        'CountFileLinesThread
        '
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.Button13)
        Me.GroupBox6.Controls.Add(Me.Button12)
        Me.GroupBox6.Controls.Add(Me.Button11)
        Me.GroupBox6.Controls.Add(Me.ListView1)
        Me.GroupBox6.Location = New System.Drawing.Point(12, 181)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(699, 170)
        Me.GroupBox6.TabIndex = 10
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Nachbearbeitung"
        '
        'Button13
        '
        Me.Button13.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Button13.Image = Global.ColumnFileView.NET.My.Resources.Resources.document_open_7
        Me.Button13.Location = New System.Drawing.Point(662, 93)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(32, 31)
        Me.Button13.TabIndex = 9
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Button12.Image = Global.ColumnFileView.NET.My.Resources.Resources.document_new_8
        Me.Button12.Location = New System.Drawing.Point(662, 56)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(32, 31)
        Me.Button12.TabIndex = 8
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.Button11.Image = Global.ColumnFileView.NET.My.Resources.Resources.edit_6
        Me.Button11.Location = New System.Drawing.Point(662, 19)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(32, 31)
        Me.Button11.TabIndex = 7
        Me.Button11.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        ListViewGroup1.Header = "Vorlagenspezifische Nachbearbeitung"
        ListViewGroup1.Name = "ListViewGroup2"
        ListViewGroup2.Header = "Statische Elemente"
        ListViewGroup2.Name = "ListViewGroup1"
        Me.ListView1.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2})
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(6, 19)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(650, 145)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        Me.ColumnHeader1.Width = 200
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Aktion(en)"
        Me.ColumnHeader2.Width = 412
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.Controls.Add(Me.ProcessMultipleFilesWithThreadingCtl)
        Me.GroupBox7.Controls.Add(Me.Label5)
        Me.GroupBox7.Controls.Add(Me.MultitreadingCountCtl)
        Me.GroupBox7.Controls.Add(Me.UseMultithreadingCtl)
        Me.GroupBox7.Controls.Add(Me.ProcessPostprocessingDirectCtl)
        Me.GroupBox7.Location = New System.Drawing.Point(12, 434)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(699, 67)
        Me.GroupBox7.TabIndex = 11
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Performance"
        '
        'ProcessMultipleFilesWithThreadingCtl
        '
        Me.ProcessMultipleFilesWithThreadingCtl.AutoSize = True
        Me.ProcessMultipleFilesWithThreadingCtl.Checked = True
        Me.ProcessMultipleFilesWithThreadingCtl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ProcessMultipleFilesWithThreadingCtl.Location = New System.Drawing.Point(275, 42)
        Me.ProcessMultipleFilesWithThreadingCtl.Name = "ProcessMultipleFilesWithThreadingCtl"
        Me.ProcessMultipleFilesWithThreadingCtl.Size = New System.Drawing.Size(289, 17)
        Me.ProcessMultipleFilesWithThreadingCtl.TabIndex = 4
        Me.ProcessMultipleFilesWithThreadingCtl.Text = "Multiple Dateien in verschiedene Threads aufschlüsseln"
        Me.ProcessMultipleFilesWithThreadingCtl.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(206, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Threads"
        '
        'MultitreadingCountCtl
        '
        Me.MultitreadingCountCtl.Location = New System.Drawing.Point(143, 41)
        Me.MultitreadingCountCtl.Maximum = New Decimal(New Integer() {10000000, 0, 0, 0})
        Me.MultitreadingCountCtl.Name = "MultitreadingCountCtl"
        Me.MultitreadingCountCtl.Size = New System.Drawing.Size(62, 20)
        Me.MultitreadingCountCtl.TabIndex = 2
        Me.MultitreadingCountCtl.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'UseMultithreadingCtl
        '
        Me.UseMultithreadingCtl.AutoSize = True
        Me.UseMultithreadingCtl.Checked = True
        Me.UseMultithreadingCtl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.UseMultithreadingCtl.Location = New System.Drawing.Point(6, 42)
        Me.UseMultithreadingCtl.Name = "UseMultithreadingCtl"
        Me.UseMultithreadingCtl.Size = New System.Drawing.Size(137, 17)
        Me.UseMultithreadingCtl.TabIndex = 1
        Me.UseMultithreadingCtl.Text = "Multi-Threading nutzen:"
        Me.UseMultithreadingCtl.UseVisualStyleBackColor = True
        '
        'ProcessPostprocessingDirectCtl
        '
        Me.ProcessPostprocessingDirectCtl.AutoSize = True
        Me.ProcessPostprocessingDirectCtl.Checked = True
        Me.ProcessPostprocessingDirectCtl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ProcessPostprocessingDirectCtl.Location = New System.Drawing.Point(6, 19)
        Me.ProcessPostprocessingDirectCtl.Name = "ProcessPostprocessingDirectCtl"
        Me.ProcessPostprocessingDirectCtl.Size = New System.Drawing.Size(456, 17)
        Me.ProcessPostprocessingDirectCtl.TabIndex = 0
        Me.ProcessPostprocessingDirectCtl.Text = "Nachbearbeitung direkt durchführen (keine Anzeige des Inhalts während Nachbearbei" &
    "tung)"
        Me.ProcessPostprocessingDirectCtl.UseVisualStyleBackColor = True
        '
        'FileRulesCheckThread
        '
        '
        'PreviewDataThread
        '
        '
        'LoadPostprocessingFilesThread
        '
        '
        'LoadAllEncodingsThread
        '
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'CalcLinesCountCtl
        '
        Me.CalcLinesCountCtl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CalcLinesCountCtl.AutoSize = True
        Me.CalcLinesCountCtl.Checked = True
        Me.CalcLinesCountCtl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CalcLinesCountCtl.Location = New System.Drawing.Point(566, 10)
        Me.CalcLinesCountCtl.Name = "CalcLinesCountCtl"
        Me.CalcLinesCountCtl.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.CalcLinesCountCtl.Size = New System.Drawing.Size(140, 17)
        Me.CalcLinesCountCtl.TabIndex = 10
        Me.CalcLinesCountCtl.Text = "Zeilenanzahl berechnen"
        Me.CalcLinesCountCtl.UseVisualStyleBackColor = True
        '
        'OpenFileDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(723, 731)
        Me.Controls.Add(Me.CalcLinesCountCtl)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(592, 534)
        Me.Name = "OpenFileDlg"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Datei öffnen..."
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ErrorPanelCtl.ResumeLayout(False)
        Me.ErrorPanelCtl.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.NotificationPanelCtl.ResumeLayout(False)
        Me.NotificationPanelCtl.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.LastNLinesCtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToLineCtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromLineCtl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PreviewData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.MultitreadingCountCtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button4 As Button
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Button5 As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ToLineCtl As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents FromLineCtl As NumericUpDown
    Friend WithEvents LoadToEndCtl As CheckBox
    Friend WithEvents Label4 As Label
    Public WithEvents PreviewData As DataSet
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents EncodingAutoDetectCtl As CheckBox
    Friend WithEvents CountFileLinesThread As System.ComponentModel.BackgroundWorker
    Friend WithEvents FromLineOptionCtl As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents LastNLinesCtl As NumericUpDown
    Friend WithEvents LastNLinesOptionCtl As RadioButton
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents JumpToEndCtl As CheckBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents ProcessPostprocessingDirectCtl As CheckBox
    Friend WithEvents Button10 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents errorlbl As Label
    Friend WithEvents FileRulesCheckThread As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label5 As Label
    Friend WithEvents MultitreadingCountCtl As NumericUpDown
    Friend WithEvents UseMultithreadingCtl As CheckBox
    Friend WithEvents PreviewDataThread As System.ComponentModel.BackgroundWorker
    Friend WithEvents LoadPostprocessingFilesThread As System.ComponentModel.BackgroundWorker
    Friend WithEvents LoadAllEncodingsThread As System.ComponentModel.BackgroundWorker
    Friend WithEvents Button11 As Button
    Friend WithEvents Button13 As Button
    Friend WithEvents Button12 As Button
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents ProcessMultipleFilesWithThreadingCtl As CheckBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ProgressBar2 As ProgressBar
    Friend WithEvents ProgressBar3 As ProgressBar
    Friend WithEvents ComboBox4 As ComboBox
    Friend WithEvents CalcLinesCountCtl As CheckBox
    Friend WithEvents AnalyseFileForProfileCtl As CheckBox
    Friend WithEvents ViewPreviewCtl As CheckBox
    Friend WithEvents NotificationPanelCtl As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents LinkLabel1 As LinkLabel
    Friend WithEvents LinkLabel2 As LinkLabel
    Friend WithEvents ErrorPanelCtl As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Label7 As Label
    Friend WithEvents LinkLabel3 As LinkLabel
End Class
