Imports System.Reflection
Imports System.Runtime.InteropServices
Imports ColumnFileViewImportFileHandler
Imports DataGridViewAutoFilter

Public Class FilterExtendedDataGridView
    Public _parent As Form1
    Public _ProfileDefinition As FileHandler
    Public _ProfileFilename As String = ""
    Public _UnsavedProfileChanges As Boolean = False

    Public Property DoubleBufferOverride As Boolean = False

    Dim statusStrip1 As New StatusStrip()
    Dim filterStatusLabel As New ToolStripStatusLabel()
    Dim WithEvents showAllLabel As New ToolStripStatusLabel("Show &All")


    Private Sub DataGridViewCtl_BindingContextChanged(sender As Object, e As EventArgs) Handles DataGridViewCtl.BindingContextChanged
        CheckForBindingAndPopulateCache()
    End Sub

    Public Sub CheckForBindingAndPopulateCache()
        ' Continue only if the data source has been set.
        If Not IsNothing(_parent) Then
            If _parent._IsChild = False Then
                If DataGridViewCtl.DataSource Is Nothing Then
                    Return
                End If
            End If
            If (_parent._ProfileDefinition.UseExtendedFilter And _parent._ColumnDefPopulated = False) Or _parent._Settings.AlwaysUseExtendedFilters Then
                ' Add the AutoFilter header cell to each column.
                Dim cnt As Integer = 0
                For Each col As DataGridViewColumn In DataGridViewCtl.Columns
                    Dim tt As New DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell)
                    col.HeaderCell = tt

                    If _parent._ProfileDefinition.ColumnDefinitions(cnt).PreloadFilter Then
                        tt.PopulateFilters(True)

                        Dim qq As New ColumnDropdownCalcCtl
                        qq._parentColumn = tt
                        qq._parentFrm = _parent

                        _parent.FlowLayoutPanel1.Controls.Add(qq)
                    Else
                        tt.FilterPopulated = False
                    End If

                    _parent._ColumnDefPopulated = True

                    cnt += 1
                Next
            End If
        End If
    End Sub

    Public Sub AddFilterControls()
        'Erstelle Filter
        FilterPanel.Controls.Clear()


        If DataGridViewCtl.RowHeadersVisible Then
            Dim kk As New ExFilterDummyCtl
            kk.Width = DataGridViewCtl.RowHeadersWidth - 2
            FilterPanel.Controls.Add(kk)
        End If

        For ind = 0 To DataGridViewCtl.Columns.Count - 1
            If DataGridViewCtl.Columns(ind).Visible And Not DataGridViewCtl.Columns(ind).Width = 0 And Not DataGridViewCtl.Columns(ind).HeaderCell.Size.Width = 0 Then
                Dim uu As New ExFilterComboBox
                uu._parentCtl = Me
                uu._parentColumnIndex = ind
                If _parent._ProfileDefinition.UseExtendedFilter Then
                    uu._FilterMode = 1
                End If
                If Not _ProfileDefinition.ColumnDefinitions(ind).CommonFilters.Length = 0 Then
                    For ind2 = 0 To _ProfileDefinition.ColumnDefinitions(ind).CommonFilters.Length - 1
                        uu.ItemCtl.Items.Add(_ProfileDefinition.ColumnDefinitions(ind).CommonFilters(ind2))
                    Next
                End If

                If (ind = 0) Or (ind = DataGridViewCtl.Columns.Count - 1) Then
                    uu.Width = DataGridViewCtl.Columns(ind).Width + 1
                Else
                    uu.Width = DataGridViewCtl.Columns(ind).Width
                End If

                FilterPanel.Controls.Add(uu)
                uu.CheckForFilter()
            End If
        Next
    End Sub

    Public Function GetColumnsControlsCount() As Integer
        Dim cnt As Integer = 0
        For ind = 0 To FilterPanel.Controls.Count - 1
            Try
                Dim uu As ExFilterComboBox
                uu = FilterPanel.Controls(ind)

                cnt += 1
            Catch ex As Exception
            End Try
        Next

        Return cnt
    End Function

    Public Sub UpdateFilterControls()
        'Aktualisiere Elemente
        If Not FilterPanel.Controls.Count = 0 Then
            Dim startindex = 0
            If DataGridViewCtl.RowHeadersVisible Then
                startindex = 1

                Dim kk As ExFilterDummyCtl
                kk = FilterPanel.Controls(0)
                kk.Width = DataGridViewCtl.RowHeadersWidth - 2
            End If

            For ind = 0 To DataGridViewCtl.Columns.Count - 1
                Dim uu As ExFilterComboBox
                uu = FilterPanel.Controls(startindex)
                If (ind = 0) Or (ind = DataGridViewCtl.Columns.Count - 1) Then
                    uu.Width = DataGridViewCtl.Columns(ind).Width + 1
                Else
                    uu.Width = DataGridViewCtl.Columns(ind).Width
                End If

                If DataGridViewCtl.Columns(ind).Width > 25 Then
                    If Not uu.SplitContainer1.Visible Then
                        uu.SplitContainer1.Visible = True
                        uu.ItemCtl.Visible = True
                    End If
                Else
                    If uu.SplitContainer1.Visible Then
                        uu.SplitContainer1.Visible = False
                        uu.ItemCtl.Visible = False
                    End If
                End If

                uu.CheckForFilter()

                startindex += 1
            Next
        End If
    End Sub

    Private Sub DataGridViewCtl_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridViewCtl.DataBindingComplete
        If _parent._ProfileDefinition.UseExtendedFilter Then
            Dim filterStatus As String = DataGridViewAutoFilterColumnHeaderCell _
                        .GetFilterStatus(DataGridViewCtl)
            If String.IsNullOrEmpty(filterStatus) Then
                showAllLabel.Visible = False
                filterStatusLabel.Visible = False
            Else
                showAllLabel.Visible = True
                filterStatusLabel.Visible = True
                filterStatusLabel.Text = filterStatus
            End If
        End If

        _parent.PreSetViewSettings(Me.DataGridViewCtl, _parent._ProfileDefinition)
        AddFilterControls()

        _parent.FillSearchColumnList()
    End Sub

    Private Sub FilterExtendedDataGridView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With showAllLabel
            .Visible = False
            .IsLink = True
            .LinkBehavior = LinkBehavior.HoverUnderline
        End With

        With statusStrip1
            .Cursor = Cursors.Default
            .Items.AddRange(New ToolStripItem() {
                filterStatusLabel, showAllLabel})
        End With

        If DoubleBufferOverride Then
            If Not SystemInformation.TerminalServerSession Then
                Dim dgvType As Type = DataGridViewCtl.[GetType]()
                Dim pi As PropertyInfo = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
                pi.SetValue(DataGridViewCtl, True, Nothing)
            End If
        End If
    End Sub

    Private Sub DataGridViewCtl_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridViewCtl.DataError
        e.ThrowException = False
    End Sub

    Private Sub showAllLabel_Click(ByVal sender As Object,
    ByVal e As EventArgs) Handles showAllLabel.Click

        DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(DataGridViewCtl)
    End Sub

    Private Sub DataGridViewCtl_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridViewCtl.ColumnWidthChanged
        If Not GetColumnsControlsCount() = DataGridViewCtl.Columns.Count Then
            AddFilterControls()
        Else
            UpdateFilterControls()
        End If

        If _parent.ToolStripButton13.Checked Then
            _parent.AutoArrangCollumnsHorizon()
        End If
    End Sub

    Private Sub DataGridViewCtl_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridViewCtl.KeyDown
        If e.Alt AndAlso (e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Up) Then
            Dim filterCell As DataGridViewAutoFilterColumnHeaderCell =
                TryCast(DataGridViewCtl.CurrentCell.OwningColumn.HeaderCell,
                DataGridViewAutoFilterColumnHeaderCell)
            If filterCell IsNot Nothing Then
                filterCell.ShowDropDownList()
                e.Handled = True
            End If
        End If
    End Sub

    Public Function SaveDefinitionFileChanges(ByVal Filename As String) As Boolean
        Try
            Dim pp As New FileSerializer
            pp.SaveDefFile(Filename, _ProfileDefinition)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadDefinitionFileChanges(ByVal Filename As String) As FileHandler
        Try
            Dim pp As New FileSerializer
            Dim jj As FileHandler
            jj = pp.LoadDefFile(Filename)

            Return jj
        Catch ex As Exception
            Return New FileHandler
        End Try
    End Function

    Private Sub SucheWertToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SucheWertToolStripMenuItem.Click
        _parent.ToolStripButton7.Checked = False
        _parent.ToolStripButton10.PerformClick()
    End Sub

    Private Sub SucheWertexaktToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SucheWertexaktToolStripMenuItem.Click
        _parent.ToolStripButton7.Checked = True
        _parent.ToolStripButton10.PerformClick()
    End Sub

    Private Sub KopierenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KopierenToolStripMenuItem.Click
        My.Computer.Clipboard.SetText(DataGridViewCtl.SelectedCells(0).Value)
    End Sub

    Private Sub EinfügenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EinfügenToolStripMenuItem.Click
        DataGridViewCtl.SelectedCells(0).Value = My.Computer.Clipboard.GetText
    End Sub

    Private Sub DataGridViewCtl_ColumnStateChanged(sender As Object, e As DataGridViewColumnStateChangedEventArgs) Handles DataGridViewCtl.ColumnStateChanged
        If e.StateChanged = DataGridViewElementStates.Visible Then
            AddFilterControls()
        End If
    End Sub
End Class
