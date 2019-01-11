'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports ColumnFileViewImportFileHandler
Imports DataGridViewAutoFilter

Public Class ExFilterComboBox
    Public _FilterMode As Integer = -1
    Public _parentCtl As FilterExtendedDataGridView
    Public _parent As Form1
    Public _parentColumnIndex As Integer

    Private UpdateCtl As Boolean = False

    Private Sub ItemCtl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ItemCtl.SelectedIndexChanged
        Try
            If UpdateCtl = False Then
                If _FilterMode = 1 Then
                    Dim kk As DataGridViewAutoFilter.DataGridViewAutoFilterColumnHeaderCell
                    kk = _parentCtl.DataGridViewCtl.Columns(_parentColumnIndex).HeaderCell

                    kk.UpdateFilter(ItemCtl.Items(ItemCtl.SelectedIndex))

                    Dim tt As New MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)
                    Dim qq As New DataGridViewCellMouseEventArgs(_parentColumnIndex, 0, 0, 0, tt)

                    kk.SimulateMouseDown(qq)
                End If
                If _FilterMode = 0 Then
                    Dim qq As DataGridViewColumn
                    qq = _parentCtl.DataGridViewCtl.Columns(_parentColumnIndex)

                    _parent.ToolStripComboBox1.SelectedIndex = _parentColumnIndex + 1
                    _parent.ToolStripTextBox1.Text = ItemCtl.Items(ItemCtl.SelectedIndex)
                    _parent.ToolStripButton5.PerformClick()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Sub CheckForFilter()
        UpdateCtl = True

        If _FilterMode = 1 Then
            Dim kk As DataGridViewAutoFilter.DataGridViewAutoFilterColumnHeaderCell
            kk = _parentCtl.DataGridViewCtl.Columns(_parentColumnIndex).HeaderCell

            If kk.filtered Then
                If Not ItemCtl.Items.Contains(kk.selectedFilterValue) Then
                    ItemCtl.Items.Add(kk.selectedFilterValue)
                End If

                ItemCtl.SelectedItem = kk.selectedFilterValue
            End If
        End If

        UpdateCtl = False
    End Sub

    Private Sub ExFilterComboBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateCtl = False
        ItemCtl.SelectedIndex = 0
        UpdateCtl = True
    End Sub

    Private Sub SetColumnSizeInProfile() Handles SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem.Click
        Dim handl As FileHandler
        handl = _parentCtl._ProfileDefinition
        handl.ColumnDefinitions(_parentColumnIndex).ColumnWidth = _parentCtl.DataGridViewCtl.Columns(_parentColumnIndex).Width
        _parentCtl._UnsavedProfileChanges = True
    End Sub

    Private Sub ShowColumnDefPropertyWindow() Handles DefinitionBearbeitenToolStripMenuItem.Click
        Dim pp As New EditPropertyFrm
        pp._SelectedObj = _parentCtl._ProfileDefinition.ColumnDefinitions(_parentColumnIndex)
        _parentCtl._UnsavedProfileChanges = True
        pp.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
    End Sub
End Class
