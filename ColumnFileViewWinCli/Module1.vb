Imports ColumnFileViewCliHelper

Module Module1
    Private CliWrapper As New CliHelper

    Sub Main()
        CliWrapper.LunchCli(Environment.GetCommandLineArgs, True)
    End Sub
End Module
