Imports System
Imports ColumnFileViewCliHelper

Module CliManager
    Private CliWrapper As New CliHelper

    Sub Main(args As String())
        CliWrapper.LunchCli(args, True)
    End Sub
End Module
