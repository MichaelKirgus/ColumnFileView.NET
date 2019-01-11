'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports ColumnFileViewImportFileHandler

Public Class PostProcessingHandler
    Public LinesCnt As Int64 = 0
    Public CurrentLine As Int64 = 0
    Public CurrentPostItemCnt As Int64 = 0
    Public ProcessingItems As List(Of ProcessingItem)

    Public Function ProcessItems(ByVal DataX As DataSet, ByVal PostProcessingItems As List(Of ProcessingItem)) As DataSet
        Try
            Dim colcnt As Integer
            colcnt = DataX.Tables(0).Columns.Count

            For postindex = 0 To PostProcessingItems.Count - 1
                Select Case PostProcessingItems(postindex).ProcessingType
                    Case ProcessingItem.ProcessingTypeEnum.ReplaceChars
                        If PostProcessingItems(postindex).NewCharIsNothing Then
                            For rowindex = 0 To DataX.Tables(0).Rows.Count - 1
                                For colindex = 0 To colcnt - 1
                                    Dim newstr As String
                                    If Not DataX.Tables(0).Rows(rowindex).IsNull(colindex) Then
                                        newstr = DataX.Tables(0).Rows(rowindex).Item(colindex)
                                        newstr = newstr.Replace(PostProcessingItems(postindex).OldChar, "")
                                        DataX.Tables(0).Rows(rowindex).Item(colindex) = newstr
                                    End If
                                Next
                                CurrentLine += 1
                            Next
                        Else
                            For rowindex = 0 To DataX.Tables(0).Rows.Count - 1
                                For colindex = 0 To colcnt - 1
                                    Dim newstr As String
                                    If Not DataX.Tables(0).Rows(rowindex).IsNull(colindex) Then
                                        newstr = DataX.Tables(0).Rows(rowindex).Item(colindex)
                                        newstr = newstr.Replace(PostProcessingItems(postindex).OldChar, PostProcessingItems(postindex).NewChar)
                                        DataX.Tables(0).Rows(rowindex).Item(colindex) = newstr
                                    End If
                                Next
                                CurrentLine += 1
                            Next
                        End If

                    Case ProcessingItem.ProcessingTypeEnum.ReplaceDateTimeFormat
                        Dim typehandl As New ImportHandler
                        For rowindex = 0 To DataX.Tables(0).Rows.Count - 1
                            Dim newstr As String
                            Dim datex As DateTime
                            If Not DataX.Tables(0).Rows(rowindex).IsNull(PostProcessingItems(postindex).DateTimeColumnIndex) Then
                                newstr = DataX.Tables(0).Rows(rowindex).Item(PostProcessingItems(postindex).DateTimeColumnIndex)
                                datex = DateTime.Parse(PostProcessingItems(postindex).OldDateTimeFormat, CultureInfo.InvariantCulture)
                                If PostProcessingItems(postindex).ConvertDateToString Then
                                    DataX.Tables(0).Rows(rowindex).Item(PostProcessingItems(postindex).DateTimeColumnIndex) = datex.ToString(PostProcessingItems(postindex).NewDateTimeFormat, CultureInfo.InvariantCulture)
                                Else
                                    DataX.Tables(0).Rows(rowindex).Item(PostProcessingItems(postindex).DateTimeColumnIndex) = datex
                                End If
                            End If
                            CurrentLine += 1
                        Next
                        DataX.Tables(0).Columns(PostProcessingItems(postindex).DateTimeColumnIndex).DataType = typehandl.GetColumnType(ColumnDefinitionItem.DataTypeEnum.DateTimeValue)

                    Case ProcessingItem.ProcessingTypeEnum.ConvertFilesizeInReadableFormat
                        Dim calchandl As New FileSizeCalcHandler
                        For rowindex = 0 To DataX.Tables(0).Rows.Count - 1
                            Dim newstr As Int64
                            If Not DataX.Tables(0).Rows(rowindex).IsNull(PostProcessingItems(postindex).FilesizeColumnIndex) Then
                                newstr = DataX.Tables(0).Rows(rowindex).Item(PostProcessingItems(postindex).FilesizeColumnIndex)
                                DataX.Tables(0).Rows(rowindex).Item(PostProcessingItems(postindex).FilesizeColumnIndex) = calchandl.GetRightSizeFormat(newstr, PostProcessingItems(postindex).FilesizeDecimalPlaces, PostProcessingItems(postindex).FilesizeKBSize)
                            End If
                            CurrentLine += 1
                        Next

                    Case ProcessingItem.ProcessingTypeEnum.RexExReplacementInColumn
                        For rowindex = 0 To DataX.Tables(0).Rows.Count - 1
                            Dim newstr As Int64
                            If Not DataX.Tables(0).Rows(rowindex).IsNull(PostProcessingItems(postindex).FilesizeColumnIndex) Then
                                newstr = Regex.Replace(DataX.Tables(0).Rows(rowindex).Item(PostProcessingItems(postindex).FilesizeColumnIndex), PostProcessingItems(postindex).RegExPattern, PostProcessingItems(postindex).NewChar)
                                DataX.Tables(0).Rows(rowindex).Item(PostProcessingItems(postindex).FilesizeColumnIndex) = newstr
                            End If
                            CurrentLine += 1
                        Next

                    Case ProcessingItem.ProcessingTypeEnum.RexExExpressionEqualsInColumn
                        If PostProcessingItems(postindex).RegExEqualIndex = -1 Then
                            For rowindex = 0 To DataX.Tables(0).Rows.Count - 1
                                For colindex = 0 To colcnt - 1
                                    Dim newstr As String
                                    If Not DataX.Tables(0).Rows(rowindex).IsNull(colindex) Then
                                        newstr = DataX.Tables(0).Rows(rowindex).Item(colindex)
                                        Dim oo As New Regex(PostProcessingItems(postindex).RegExPattern)
                                        If Not oo.IsMatch(newstr) Then
                                            DataX.Tables(0).Rows.Remove(DataX.Tables(0).Rows(rowindex))
                                        End If
                                    End If
                                Next
                                CurrentLine += 1
                            Next
                        Else
                            For colindex = 0 To colcnt - 1
                                Dim newstr As String
                                If Not DataX.Tables(0).Rows(PostProcessingItems(postindex).RegExEqualIndex).IsNull(colindex) Then
                                    newstr = DataX.Tables(0).Rows(PostProcessingItems(postindex).RegExEqualIndex).Item(colindex)
                                    Dim oo As New Regex(PostProcessingItems(postindex).RegExPattern)
                                    If Not oo.IsMatch(newstr) Then
                                        DataX.Tables(0).Rows.Remove(DataX.Tables(0).Rows(PostProcessingItems(postindex).RegExEqualIndex))
                                    End If
                                End If
                            Next
                            CurrentLine += 1
                        End If
                End Select

                CurrentPostItemCnt += 1
                CurrentLine = 0
            Next

            Return DataX
        Catch ex As Exception
            Return DataX
        End Try
    End Function
End Class
