'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.IO
Imports System.Windows.Forms

Public Class ExportHandler
    Public Function ExportResults(ByVal DataSetObj As DataSet, ByVal Filename As String, ByVal Type As Integer) As Boolean
        Try
            If Type = 0 Then
                'Export im XML-Format + Excel starten

                DataSetObj.WriteXml(My.Computer.FileSystem.SpecialDirectories.Temp & "\export.xml", XmlWriteMode.WriteSchema)
                Dim excelproc As New Process
                excelproc.StartInfo.FileName = "EXCEL"
                excelproc.StartInfo.Arguments = My.Computer.FileSystem.SpecialDirectories.Temp & "\export.xml"
                excelproc.Start()
            End If
            If Type = 1 Then
                'Export im XML-Format

                DataSetObj.WriteXml(Filename)
            End If
            If Type = 2 Then
                'Export im CSV-Format mit Komma getrennt

                writeCSV(DataSetObj, Filename)
            End If
            If Type = 3 Then
                'Export im CSV-Format mit Semelikon getrennt

                writeCSV(DataSetObj, Filename, ";")
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function


    Public Function writeCSV(gridIn As DataSet, outputFile As String, Optional ByVal Delimiter As String = ",") As Boolean
        'test to see if the DataGridView has any rows
        Dim swOut As StreamWriter = Nothing
        Try
            If gridIn.Tables(0).Rows.Count > 0 Then
                Try
                    Dim value As String = ""
                    swOut = New StreamWriter(outputFile)

                    'write header rows to csv
                    For i As Integer = 0 To gridIn.Tables(0).Columns.Count - 1
                        Try
                            If i > 0 Then
                                swOut.Write(Delimiter)
                            End If
                            swOut.Write(gridIn.Tables(0).Columns(i).Caption)
                        Catch ex As Exception
                        End Try
                    Next

                    swOut.WriteLine()

                    'write DataGridView rows to csv
                    For j As Integer = 0 To gridIn.Tables(0).Rows.Count - 1
                        If j > 0 Then
                            swOut.WriteLine()
                        End If

                        For i As Integer = 0 To gridIn.Tables(0).Columns.Count - 1
                            Try
                                If i > 0 Then
                                    swOut.Write(Delimiter)
                                End If

                                value = gridIn.Tables(0).Rows(j).Item(i).ToString
                                'replace comma's with spaces
                                value = value.Replace(","c, "."c)
                                value = value.Replace(";"c, " "c)
                                'replace embedded newlines with spaces
                                value = value.Replace(Environment.NewLine, " ")
                                value = value.Replace(vbNewLine, "")

                                swOut.Write(value)
                            Catch ex As Exception
                            End Try
                        Next
                    Next
                    swOut.Close()
                Catch ex As Exception
                End Try
            End If

            Return True
        Catch ex As Exception
            swOut.Close()
            Return False
        End Try
    End Function

    Public Function DatagridviewToDataset(ByVal dgv As DataGridView) As System.Data.DataSet
        Dim ds As New System.Data.DataSet

        'Take the data and structure from the datagridview and return it as a dataset.  You can use 
        '"Imports System.Data" declaration at the top of your project/class and remove the system.data 
        'from the various parts of this function.

        Try
            'Add a new table to the dataset
            ds.Tables.Add("Main", "Data")

            'Add the columns
            Dim col As System.Data.DataColumn

            'For each colum in the datagridveiw add a new column to your table
            For Each dgvCol As DataGridViewColumn In dgv.Columns
                col = New System.Data.DataColumn(dgvCol.HeaderText)
                ds.Tables("Main").Columns.Add(col)
            Next

            'Add the rows from the datagridview
            Dim row As System.Data.DataRow
            Dim colcount As Integer = dgv.Columns.Count - 1

            For i As Integer = 0 To dgv.Rows.Count - 1
                row = ds.Tables("Main").Rows.Add

                For Each column As DataGridViewColumn In dgv.Columns
                    row.Item(column.Index) = dgv.Rows.Item(i).Cells(column.Index).Value
                Next

            Next

            Return ds
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
