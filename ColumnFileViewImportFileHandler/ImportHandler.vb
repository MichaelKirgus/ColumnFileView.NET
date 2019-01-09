Imports System.IO
Imports System.Windows.Forms

Public Class ImportHandler

    Public CurrentLine As Int64 = 0
    Public CurrentLineCount As Int64 = 0
    Public LineCount As Int64 = 0

    Public Function CountFileLines(ByVal Filename As String, ByVal AutoDetectEncoding As Boolean, ByVal Encoding As System.Text.Encoding) As Int64
        Try
            Dim SourceStream As FileStream = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim dd As New StreamReader(SourceStream, Encoding, AutoDetectEncoding)

            Dim linescnt As Int64 = 0
            Do Until dd.EndOfStream
                dd.ReadLine()
                linescnt += 1
            Loop

            LineCount = linescnt

            dd.Close()
            SourceStream.Close()

            Return linescnt
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function GetValidColumnName(ByVal ColumnTxt As String) As String
        Try
            Dim tmpstr As String
            tmpstr = ColumnTxt
            tmpstr = tmpstr.Replace(" ", "_")
            tmpstr = tmpstr.Replace(":", "_")
            tmpstr = tmpstr.Replace("|", "_")
            tmpstr = tmpstr.Replace(My.Resources.trenn, "_")
            tmpstr = tmpstr.Replace("'", "_")
            tmpstr = tmpstr.Replace("/", "_")
            tmpstr = tmpstr.Replace("\", "_")
            tmpstr = tmpstr.Replace("§", "_")
            tmpstr = tmpstr.Replace("$", "_")
            tmpstr = tmpstr.Replace("%", "_")
            tmpstr = tmpstr.Replace("&", "_")
            tmpstr = tmpstr.Replace("(", "_")
            tmpstr = tmpstr.Replace(")", "_")
            tmpstr = tmpstr.Replace("=", "_")
            tmpstr = tmpstr.Replace("?", "_")
            tmpstr = tmpstr.Replace("´", "_")
            tmpstr = tmpstr.Replace("*", "_")
            tmpstr = tmpstr.Replace("+", "_")
            tmpstr = tmpstr.Replace("~", "_")
            tmpstr = tmpstr.Replace(";", "_")
            tmpstr = tmpstr.Replace("@", "_")
            tmpstr = tmpstr.Replace("<", "_")
            tmpstr = tmpstr.Replace(">", "_")
            tmpstr = tmpstr.Replace("^", "_")

            Return tmpstr
        Catch ex As Exception
            Return ColumnTxt
        End Try
    End Function

    Public Function GetColumnType(ByVal ColumnEnum As ColumnDefinitionItem.DataTypeEnum) As Type
        Select Case ColumnEnum
            Case ColumnDefinitionItem.DataTypeEnum.StringValue
                Return Type.GetType("System.String")
            Case ColumnDefinitionItem.DataTypeEnum.Int32Value
                Return Type.GetType("System.Int32")
            Case ColumnDefinitionItem.DataTypeEnum.Int64Value
                Return Type.GetType("System.Int64")
            Case ColumnDefinitionItem.DataTypeEnum.BooleanValue
                Return Type.GetType("System.Boolean")
            Case ColumnDefinitionItem.DataTypeEnum.DateTimeValue
                Return Type.GetType("System.DateTime")
            Case ColumnDefinitionItem.DataTypeEnum.DateValue
                Return Type.GetType("System.Date")
        End Select

        Return Type.GetType("System.String")
    End Function

    Public Function LoadFile(ByVal Filename As String, ByVal AutoDetectEncoding As Boolean, ByVal Encoding As System.Text.Encoding, ByVal ProfileHandler As FileHandler, Optional StartIndex As Integer = 0, Optional ByVal MaxIndex As Integer = -2) As DataSet
        Try
            Dim DataSetCtl As New DataSet

            DataSetCtl.Tables.Add("Main", "Data")

            Dim SourceStream As FileStream = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim dd As New StreamReader(SourceStream, Encoding, AutoDetectEncoding)

            'Spalten hinzufügen
            Dim col As System.Data.DataColumn

            If ProfileHandler.AutoGenerateColumns = False Then
                For indexc = 0 To ProfileHandler.ColumnDefinitions.Count - 1
                    col = New System.Data.DataColumn()
                    If ProfileHandler.ColumnDefinitions(indexc).ColumnSQLName = "" Then
                        col.ColumnName = GetValidColumnName(ProfileHandler.ColumnDefinitions(indexc).ColumnText)
                    Else
                        col.ColumnName = ProfileHandler.ColumnDefinitions(indexc).ColumnSQLName
                    End If
                    col.Caption = ProfileHandler.ColumnDefinitions(indexc).ColumnText
                    col.DataType = GetColumnType(ProfileHandler.ColumnDefinitions(indexc).DataType)
                    col.DefaultValue = ProfileHandler.ColumnDefinitions(indexc).DefaultValue

                    DataSetCtl.Tables("Main").Columns.Add(col)
                Next
            Else
                Dim firstrow As String
                firstrow = dd.ReadLine()

                Dim splicols As Array
                splicols = firstrow.Split(ProfileHandler.TransformActions(0).SplitChar)

                If ProfileHandler.FirstRowColumns Then
                    For indexc = 0 To splicols.Length - 1
                        col = New System.Data.DataColumn()
                        col.ColumnName = GetValidColumnName(splicols(indexc))
                        col.Caption = splicols(indexc)

                        DataSetCtl.Tables("Main").Columns.Add(col)
                    Next
                Else
                    For indexc = 0 To splicols.Length - 1
                        col = New System.Data.DataColumn()
                        col.ColumnName = "C" & indexc
                        col.Caption = "C" & indexc

                        DataSetCtl.Tables("Main").Columns.Add(col)
                    Next
                End If

                If Not ProfileHandler.SkipFirstRowAsContent Then
                    SourceStream.Position = 0
                End If
            End If

            Dim row As System.Data.DataRow

            Dim index As Int64 = -1
            Dim intindex As Int64 = -1
            Do Until dd.EndOfStream
                index += 1
                CurrentLine += 1
                Dim newarr As New List(Of String)
                Dim currline As String
                currline = dd.ReadLine()
                If MaxIndex = (index - 1) Then
                    Exit Do
                End If
                If StartIndex <= index Then
                    intindex += 1
                    CurrentLineCount += 1
                    For actionind = 0 To ProfileHandler.TransformActions.Count - 1
                        Dim transformact As TransformItem
                        transformact = ProfileHandler.TransformActions(actionind)
                        Try
                            If Not (transformact.BeforeSplitRemoveStartIndex = 0 And transformact.BeforeSplitRemoveCount) Then
                                currline = currline.Remove(transformact.BeforeSplitRemoveStartIndex, transformact.BeforeSplitRemoveCount)
                            End If
                            If Not (transformact.BeforeSplitReplaceOldChar = "" And transformact.BeforeSplitReplaceNewChar = "") Then
                                currline = currline.Replace(transformact.BeforeSplitReplaceOldChar, transformact.BeforeSplitReplaceNewChar)
                            End If

                            'If transformact.BeforeSplitRemoveStartIndex = 0 And transformact.BeforeSplitRemoveCount Then
                            'Else
                            '    currline = currline.Remove(transformact.BeforeSplitRemoveStartIndex, transformact.BeforeSplitRemoveCount)
                            'End If
                            'If transformact.BeforeSplitReplaceOldChar = "" And transformact.BeforeSplitReplaceNewChar = "" Then
                            'Else
                            '    currline = currline.Replace(transformact.BeforeSplitReplaceOldChar, transformact.BeforeSplitReplaceNewChar)
                            'End If
                        Catch ex As Exception
                        End Try
                        Try
                            Dim returnarr As Array
                            returnarr = currline.Split(transformact.SplitChar)
                            If Not transformact.AddSplitChar = "" Then
                                For addi = 0 To returnarr.Length - 1
                                    returnarr(addi) = returnarr(addi) & transformact.AddSplitChar
                                Next
                            End If
                            If transformact.SplitReturnIndex = -1 Then
                                If transformact.AddToList Then
                                    newarr.AddRange(returnarr)
                                Else
                                    If transformact.ClearList Then
                                        newarr.Clear()
                                    End If
                                    newarr.AddRange(returnarr)
                                End If
                            Else
                                If transformact.AddToList Then
                                    If transformact.SplitReturnIndex <= returnarr.Length - 1 Then
                                        newarr.Add(returnarr(transformact.SplitReturnIndex))
                                    End If
                                Else
                                    If transformact.ClearList Then
                                        newarr.Clear()
                                    End If
                                    If transformact.SplitReturnIndex <= returnarr.Length - 1 Then
                                        newarr.Add(returnarr(transformact.SplitReturnIndex))
                                    End If
                                End If
                            End If

                            If transformact.AfterSplitReplaceOldChar = "" And transformact.AfterSplitReplaceNewChar = "" Then
                            Else
                                For replaci = 0 To newarr.Count - 1
                                    newarr(replaci) = newarr(replaci).Replace(transformact.AfterSplitReplaceOldChar, transformact.AfterSplitReplaceNewChar)
                                Next
                            End If
                        Catch ex As Exception
                        End Try
                    Next

                    'Ausgeben
                    row = DataSetCtl.Tables("Main").Rows.Add
                    For colcnt = 0 To newarr.Count - 1
                        If colcnt <= DataSetCtl.Tables(0).Columns.Count - 1 Then
                            row.Item(colcnt) = newarr(colcnt)
                        End If
                    Next
                End If
            Loop

            dd.Dispose()
            SourceStream.Close()

            Return DataSetCtl
        Catch ex As Exception
            Return New DataSet
        End Try
    End Function
End Class
