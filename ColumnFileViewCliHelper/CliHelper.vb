'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Imports ColumnFileViewImportFileHandler
Imports ColumnFileViewThreadingHelper.MultiFileImportManager
Imports ColumnFileViewThreadingHelper.ThreadingWorkerItem

Public Class CliHelper
    Public _FunctionW As New ColumnFileViewThreadingHelper.FunctionWrapper
    Public _EncFunc As New ColumnFileViewEncodingHelper.EncodingHelper

    Public _OutputType As AvDestinations = AvDestinations.CSVFile
    Public _OutputFileMode As FileExportMode = FileExportMode.Combined
    Public _InputType As Integer = 0
    Public _InputFiles As New List(Of String)
    Public _InputEncodingIndex = -1
    Public _InputEncodingAutoDetect As Boolean = True
    Public _InputFolder As String = ""
    Public _InputTransformFileFile As String = ""
    Public _InputTransformFileObj As FileHandler
    Public _InputTransformFilesPostprocessingFiles As New List(Of String)
    Public _InputTransformFilesPostprocessing As New List(Of ProcessingItem)
    Public _OutputDest As String = ""
    Public _ThreadCount As Integer = -1
    Public _FilesBatchThreadSpintime As Integer = 1
    Public _FilesThreadSpintime As Integer = 1
    Public _StartAt As Int64 = -1
    Public _LastNLines As Int64 = -1
    Public _LoadMaxLines As Int64 = -1
    Public _LoadToEnd As Boolean = True
    Public _UseThreadingWithMultipleFiles As Boolean = True

    Public Function LunchCli(args As String(), Optional ByVal OutputErrors As Boolean = True) As Boolean
        Try
            If args IsNot Nothing Then
                If args.Length = 0 Then
                    If OutputErrors Then
                        ShowHelp()
                    End If
                Else
                    If args.Length > 1 Then
                        LoadDefaultCodepage()
                        If ReadAllCommandSwitchesAndSetSettings(args) Then
                            If ReadTransformFiles() Then
                                If ProcessFiles() Then
                                    Return True
                                Else
                                    If OutputErrors Then
                                        ShowHelp("", "Processing file failed.", True)
                                    End If
                                    Return False
                                End If
                            Else
                                ShowHelp("", "Parsing transform files failed. Please check syntax.", True)
                            End If
                        Else
                            ShowHelp("", "Parsing command line arguments failed. Please check syntax.", True)
                        End If
                    Else
                        If OutputErrors Then
                            ShowHelp("", "No valid command-line arguments.")
                        End If
                    End If
                End If
            Else
                If OutputErrors Then
                    ShowHelp("", "No valid command-line arguments.")
                End If
            End If
        Catch ex As Exception
            If OutputErrors Then
                ShowHelp(ex.Message)
            End If
        End Try

        Return False
    End Function

    Public Sub LoadDefaultCodepage()
        _InputEncodingIndex = _EncFunc.GetDefaultEncodingIndex
    End Sub

    Public Function ReadTransformFiles() As Boolean
        Try
            Dim ProcessItemS As New ProcessingFileSerializer

            For index = 0 To _InputTransformFilesPostprocessingFiles.Count - 1
                Try
                    _InputTransformFilesPostprocessing.Add(ProcessItemS.LoadDefFile(_InputTransformFilesPostprocessingFiles(index)))
                Catch ex As Exception
                    ShowHelp(ex.Message, "")
                    Return False
                End Try
            Next

            Return True
        Catch ex As Exception
            ShowHelp(ex.Message, "")
            Return False
        End Try
    End Function

    Public Function ProcessFiles() As Boolean
        Try
            _FunctionW.StartAction(AvActions.TransformData, _OutputType, _OutputFileMode, New Data.DataSet, _InputFiles, _InputFolder, _InputTransformFileFile, New List(Of FileHandler), New FileHandler,
                                   _OutputDest, _EncFunc.GetEncodingFromIndex(_InputEncodingIndex), _InputEncodingAutoDetect, _StartAt, _LastNLines, _LoadToEnd, -1, _LoadMaxLines, _ThreadCount, _InputTransformFilesPostprocessing,
                                   _UseThreadingWithMultipleFiles, _FilesBatchThreadSpintime, _FilesThreadSpintime, True)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ReadAllCommandSwitchesAndSetSettings(ByVal CmdArgs As String()) As Boolean
        Try
            Dim arglist As List(Of String)
            arglist = CmdArgs.ToList

            For ind = 0 To arglist.Count - 1
                Try
                    If arglist(ind).StartsWith("-I") Then
                        _InputFiles.Add(arglist(ind + 1))
                    End If
                    If arglist(ind).StartsWith("-FL") Then
                        _InputFolder = (arglist(ind + 1))
                    End If
                    If arglist(ind).StartsWith("-FPO") Then
                        _InputTransformFilesPostprocessingFiles.Add(arglist(ind + 1))
                    End If
                    If arglist(ind).StartsWith("-TF") Then
                        _InputTransformFileFile = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-OT") Then
                        If arglist(ind + 1).ToLower = "csvfile" Then
                            _OutputType = AvDestinations.CSVFile
                        End If
                        If arglist(ind + 1).ToLower = "xmlfile" Then
                            _OutputType = AvDestinations.XMLFile
                        End If
                        If arglist(ind + 1).ToLower = "string" Then
                            _OutputType = AvDestinations.InternalDataset
                        End If
                    End If
                    If arglist(ind).StartsWith("-OF") Then
                        _OutputDest = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-EI") Then
                        _InputEncodingIndex = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-EN") Then
                        _InputEncodingAutoDetect = False
                    End If
                    If arglist(ind).StartsWith("-TC") Then
                        _ThreadCount = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-FBTST") Then
                        _FilesBatchThreadSpintime = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-FTST") Then
                        _FilesThreadSpintime = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-SA") Then
                        _StartAt = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-LNL") Then
                        _LastNLines = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-LML") Then
                        _LoadMaxLines = arglist(ind + 1)
                    End If
                    If arglist(ind).StartsWith("-LNTE") Then
                        _LoadToEnd = False
                    End If
                    If arglist(ind).StartsWith("-NTWMF") Then
                        _UseThreadingWithMultipleFiles = False
                    End If
                Catch ex As Exception
                    ShowHelp(ex.Message, "Unknown cli parameter or parameter list is not complete.")
                    Return False
                End Try
            Next

            Return True
        Catch ex As Exception
            ShowHelp(ex.Message, "Unknown error.")
            Return False
        End Try
    End Function

    Public Sub ShowHelp(Optional ByVal IntErrorText As String = "", Optional ByVal UserErrorText As String = "", Optional ByVal ShowOnlyError As Boolean = False)
        Console.WriteLine("ColumnFileView.NET CLI")
        If Not UserErrorText = "" Then
            Console.WriteLine("Error: " & UserErrorText)
            Console.WriteLine("Error (internal): " & IntErrorText)
        End If

        If ShowOnlyError = False Then
            Console.WriteLine("Help:")
            Console.WriteLine("-I" & vbTab & vbTab & "<InputFile>...")
            Console.WriteLine("-TF" & vbTab & vbTab & "<Tranformation profile file>")
            Console.WriteLine("-OF" & vbTab & vbTab & "<Output folder or filename>")
            Console.WriteLine("[-FL]" & vbTab & vbTab & "<InputFolder> (If folder is used, input file will be ignored)")
            Console.WriteLine("[-FPO]" & vbTab & vbTab & "<Postprocessing file>...")
            Console.WriteLine("[-OT]" & vbTab & vbTab & "<OutputType> (csvfile, xmlfile, string {console default output})")
            Console.WriteLine("[-EI]" & vbTab & vbTab & "<Encoding index from 0-based index from system codepage>")
            Console.WriteLine("[-EN]" & vbTab & vbTab & "Disable encoding auto-detect")
            Console.WriteLine("[-TC]" & vbTab & vbTab & "<Thread count> (Only for overwriting best performance value)")
            Console.WriteLine("[-FBTST]" & vbTab & "<Spintime in ms between multiple files processing>")
            Console.WriteLine("[-FTST]" & vbTab & vbTab & "<Spintime in ms between thread status checks>")
            Console.WriteLine("[-SA]" & vbTab & vbTab & "<Start at nullbased index position>")
            Console.WriteLine("[-LNL]" & vbTab & vbTab & "<Process only the last lines from beginning at nullbased index position>")
            Console.WriteLine("[-LML]" & vbTab & vbTab & "<Only process file to this position>")
            Console.WriteLine("[-LNTE]" & vbTab & vbTab & "Do not load file to end")
            Console.WriteLine("[-NTWMF]" & vbTab & "Do not use multiple threads if process multiple files")
        End If
    End Sub
End Class
