'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ExFilterComboBox
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
        Me.ItemCtl = New System.Windows.Forms.ComboBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DefinitionBearbeitenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ItemCtl
        '
        Me.ItemCtl.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ItemCtl.Dock = System.Windows.Forms.DockStyle.Top
        Me.ItemCtl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ItemCtl.FormattingEnabled = True
        Me.ItemCtl.Items.AddRange(New Object() {"(All)"})
        Me.ItemCtl.Location = New System.Drawing.Point(0, 0)
        Me.ItemCtl.Name = "ItemCtl"
        Me.ItemCtl.Size = New System.Drawing.Size(130, 21)
        Me.ItemCtl.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem, Me.DefinitionBearbeitenToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(333, 48)
        '
        'SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem
        '
        Me.SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem.Name = "SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem"
        Me.SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem.Size = New System.Drawing.Size(332, 22)
        Me.SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem.Text = "Setze aktuelle Spaltenbreite als Standard im Profil"
        '
        'DefinitionBearbeitenToolStripMenuItem
        '
        Me.DefinitionBearbeitenToolStripMenuItem.Name = "DefinitionBearbeitenToolStripMenuItem"
        Me.DefinitionBearbeitenToolStripMenuItem.Size = New System.Drawing.Size(332, 22)
        Me.DefinitionBearbeitenToolStripMenuItem.Text = "Definition bearbeiten..."
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ItemCtl)
        Me.SplitContainer1.Panel1MinSize = 0
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel2MinSize = 20
        Me.SplitContainer1.Size = New System.Drawing.Size(153, 21)
        Me.SplitContainer1.SplitterDistance = 130
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Image = Global.ColumnFileView.NET.My.Resources.Resources.configure_toolbars
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 21)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ExFilterComboBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ContextMenuStrip = Me.ContextMenuStrip1
        Me.Controls.Add(Me.SplitContainer1)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "ExFilterComboBox"
        Me.Padding = New System.Windows.Forms.Padding(1, 0, 0, 0)
        Me.Size = New System.Drawing.Size(154, 21)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ItemCtl As ComboBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents SetzteAktuelleSpaltenbreiteAlsStandardImProfilToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DefinitionBearbeitenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Button1 As Button
End Class
