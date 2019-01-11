'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FilterExtendedDataGridView
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.FilterPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.DataGridViewCtl = New DataGridViewControls.ExtendedDataGridView()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.KopierenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EinfügenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.SucheWertexaktToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SucheWertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DataGridViewCtl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FilterPanel
        '
        Me.FilterPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.FilterPanel.Location = New System.Drawing.Point(0, 0)
        Me.FilterPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.FilterPanel.Name = "FilterPanel"
        Me.FilterPanel.Size = New System.Drawing.Size(850, 21)
        Me.FilterPanel.TabIndex = 0
        Me.FilterPanel.WrapContents = False
        '
        'DataGridViewCtl
        '
        Me.DataGridViewCtl.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridViewCtl.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DataGridViewCtl.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewCtl.ColumnHeadersHeight = 28
        Me.DataGridViewCtl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridViewCtl.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridViewCtl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewCtl.Location = New System.Drawing.Point(0, 21)
        Me.DataGridViewCtl.Margin = New System.Windows.Forms.Padding(0)
        Me.DataGridViewCtl.Name = "DataGridViewCtl"
        Me.DataGridViewCtl.RowHeadersWidth = 15
        Me.DataGridViewCtl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridViewCtl.RowTemplate.Height = 20
        Me.DataGridViewCtl.ShowRowErrors = False
        Me.DataGridViewCtl.Size = New System.Drawing.Size(850, 387)
        Me.DataGridViewCtl.TabIndex = 2
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.KopierenToolStripMenuItem, Me.EinfügenToolStripMenuItem, Me.ToolStripSeparator5, Me.SucheWertexaktToolStripMenuItem, Me.SucheWertToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(173, 98)
        '
        'KopierenToolStripMenuItem
        '
        Me.KopierenToolStripMenuItem.Name = "KopierenToolStripMenuItem"
        Me.KopierenToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.KopierenToolStripMenuItem.Text = "Kopieren"
        '
        'EinfügenToolStripMenuItem
        '
        Me.EinfügenToolStripMenuItem.Name = "EinfügenToolStripMenuItem"
        Me.EinfügenToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.EinfügenToolStripMenuItem.Text = "Einfügen"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(169, 6)
        '
        'SucheWertexaktToolStripMenuItem
        '
        Me.SucheWertexaktToolStripMenuItem.Name = "SucheWertexaktToolStripMenuItem"
        Me.SucheWertexaktToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.SucheWertexaktToolStripMenuItem.Text = "Suche Wert (exakt)"
        '
        'SucheWertToolStripMenuItem
        '
        Me.SucheWertToolStripMenuItem.Name = "SucheWertToolStripMenuItem"
        Me.SucheWertToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.SucheWertToolStripMenuItem.Text = "Suche Wert"
        '
        'FilterExtendedDataGridView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.DataGridViewCtl)
        Me.Controls.Add(Me.FilterPanel)
        Me.Name = "FilterExtendedDataGridView"
        Me.Size = New System.Drawing.Size(850, 408)
        CType(Me.DataGridViewCtl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewCtl As DataGridViewControls.ExtendedDataGridView
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents KopierenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EinfügenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents SucheWertexaktToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SucheWertToolStripMenuItem As ToolStripMenuItem
    Public WithEvents FilterPanel As FlowLayoutPanel
End Class
