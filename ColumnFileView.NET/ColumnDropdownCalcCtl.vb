Imports DataGridViewAutoFilter

Public Class ColumnDropdownCalcCtl

    Public _parentColumn As DataGridViewAutoFilterColumnHeaderCell
    Public _parentFrm As Form1

    Private Sub ColumnDropdownCalcCtl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Width = Me.Parent.Width - 20
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        CheckForIllegalCrossThreadCalls = False

        Try
            Dim kk As DataTable

            kk = _parentFrm.BindingSource1.DataSource

            Do Until _parentColumn.FilterPopulated
                If Not kk.DataSet.Tables.Count = 0 Then
                    If _parentColumn.CurrItemsLoadIndex = 0 Then
                        ProgressBar1.Maximum = (kk.DataSet.Tables(0).Rows.Count - _parentColumn.CurrDupPropertiesLoadCount)
                        If ProgressBar1.Maximum >= _parentColumn.CurrPropertiesLoadIndex Then
                            ProgressBar1.Value = _parentColumn.CurrPropertiesLoadIndex - _parentColumn.CurrDupPropertiesLoadCount
                        End If
                        CalcProgress.Text = "Erstelle Index für Spalte '" & _parentColumn.Value & "' (Suche Duplikate) | Aktuelle Position: " & _parentColumn.CurrPropertiesLoadIndex & "/" & kk.DataSet.Tables(0).Rows.Count & " | Duplikate: " & _parentColumn.CurrDupPropertiesLoadCount & " | Eindeutige Werte: " & _parentColumn.CurrNonDupPropertiesLoadCount & ")"
                    Else
                        ProgressBar1.Maximum = _parentColumn.CurrNonDupPropertiesLoadCount
                        ProgressBar1.Value = _parentColumn.CurrItemsLoadIndex
                        CalcProgress.Text = "Erstelle Index für Spalte '" & _parentColumn.Value & "' (Erstelle Index) | Aktuelle Position: " & _parentColumn.CurrItemsLoadIndex & "/" & kk.DataSet.Tables(0).Rows.Count & ")"
                    End If
                End If

                Threading.Thread.Sleep(250)
            Loop
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Parent.Controls.Remove(Me)
    End Sub

    Private Sub ColumnDropdownCalcCtl_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Me.Width = Me.Parent.Width - 20
    End Sub
End Class
