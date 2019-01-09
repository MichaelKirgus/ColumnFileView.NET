Imports System.Windows.Forms

Public Class AddModifyDefFileDlg

    Public DefObj As Object
    Public FileNamePath As String = ""

    Private Sub AddModifyDefFileDlg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = FileNamePath
        PropertyGrid1.SelectedObject = DefObj
    End Sub
End Class
