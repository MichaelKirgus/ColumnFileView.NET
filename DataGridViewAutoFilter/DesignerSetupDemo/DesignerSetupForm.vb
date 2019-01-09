'---------------------------------------------------------------------
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Imports DataGridViewAutoFilter

Public Class DesignerSetupForm

    Private Sub DesignerSetupForm_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        ' Load the sample data and resize the columns based on their contents.
        Me.NewDataSet.ReadXml("..\\..\\..\\..\\..\\TestData.xml")
        Me.DataGridView1.AutoResizeColumns()

    End Sub

    ' Displays the drop-down list when the user presses 
    ' ALT+DOWN ARROW or ALT+UP ARROW.
    Private Sub dataGridView1_KeyDown(ByVal sender As Object, _
        ByVal e As KeyEventArgs) Handles DataGridView1.KeyDown

        If e.Alt AndAlso (e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Up) Then

            Dim filterCell As DataGridViewAutoFilterColumnHeaderCell = _
                TryCast(DataGridView1.CurrentCell.OwningColumn.HeaderCell, _
                DataGridViewAutoFilterColumnHeaderCell)
            If filterCell IsNot Nothing Then
                filterCell.ShowDropDownList()
                e.Handled = True
            End If

        End If

    End Sub

    ' Updates the filter status label. 
    Private Sub dataGridView1_DataBindingComplete(ByVal sender As Object, _
        ByVal e As DataGridViewBindingCompleteEventArgs) _
        Handles DataGridView1.DataBindingComplete

        Dim filterStatus As String = DataGridViewAutoFilterColumnHeaderCell _
            .GetFilterStatus(DataGridView1)
        If String.IsNullOrEmpty(filterStatus) Then
            showAllLabel.Visible = False
            filterStatusLabel.Visible = False
        Else
            showAllLabel.Visible = True
            filterStatusLabel.Visible = True
            filterStatusLabel.Text = filterStatus
        End If

    End Sub

    ' Clears the filter when the user clicks the "Show All" link
    ' or presses ALT+A. 
    Private Sub showAllLabel_Click(ByVal sender As Object, _
        ByVal e As EventArgs) Handles showAllLabel.Click

        DataGridViewAutoFilterColumnHeaderCell.RemoveFilter(DataGridView1)

    End Sub

End Class
