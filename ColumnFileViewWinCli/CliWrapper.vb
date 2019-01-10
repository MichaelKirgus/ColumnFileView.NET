Imports ColumnFileViewCliHelper

Module CliWrapper
    'This Module uses the CliHelper class to serve a command-line application with .net framework

    Private CliWrapper As New CliHelper

    Sub Main()
        'Start Cli
        CliWrapper.LunchCli(Environment.GetCommandLineArgs, True)
    End Sub
End Module
