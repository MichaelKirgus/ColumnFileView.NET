Imports ColumnFileViewEncodingHelper

Public Class SettingsFrm
    Public _parent As Form1

    Private Sub SettingsFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0

        LoadAllEncodings()
        LoadSettingsToGUI()
    End Sub

    Public Sub LoadAllEncodings(Optional ByVal SelectSystemDefault As Boolean = True)
        Dim pp As New EncodingHelper

        ComboBox2.Items.AddRange(pp.GetAllEncodingsAsStringArray().ToArray)

        If SelectSystemDefault Then
            ComboBox2.SelectedIndex = pp.GetDefaultEncodingIndex
        End If
    End Sub

    Public Sub LoadSettingsToGUI()
        PropertyGrid1.SelectedObject = _parent._Settings

        ComboBox1.SelectedIndex = _parent._Settings.LanguageID
        CheckBox1.Checked = _parent._Settings.AutoDetectEncoding
        CheckBox2.Checked = _parent._Settings.DefaultLoadFileLinesCount
        CheckBox3.Checked = _parent._Settings.DefaultAnalyseFile
        CheckBox4.Checked = _parent._Settings.DefaultLoadPreview
        CheckBox5.Checked = _parent._Settings.AlwaysUseExtendedFilters
        CheckBox6.Checked = _parent._Settings.AlwaysUseColumnsSortFeature
    End Sub

    Public Sub SaveSettingsFromGUI()
        _parent._Settings.LanguageID = ComboBox1.SelectedIndex
        _parent._Settings.AutoDetectEncoding = CheckBox1.Checked
        _parent._Settings.DefaultLoadFileLinesCount = CheckBox2.Checked
        _parent._Settings.DefaultAnalyseFile = CheckBox3.Checked
        _parent._Settings.DefaultLoadPreview = CheckBox4.Checked
        _parent._Settings.AlwaysUseExtendedFilters = CheckBox5.Checked
        _parent._Settings.AlwaysUseColumnsSortFeature = CheckBox6.Checked
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveSettingsFromGUI()
        Me.Close()
    End Sub
End Class