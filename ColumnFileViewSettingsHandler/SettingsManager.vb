'Copyright (C) 2018-2019 Michael Kirgus
'This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
'This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
'You should have received a copy of the GNU General Public License along with this program; if not, see <https://www.gnu.org/licenses>.
Public Class SettingsManager
    Public Function CheckSettingsFileExists(ByVal Destination As SettingsFileDestination, Optional ByVal Appendix As String = "", Optional ByVal SettingsPath As String = "") As Boolean
        If SettingsPath = "" Then
            Return IO.File.Exists(GetSettingsFilename(Destination, Appendix))
        Else
            Return IO.File.Exists(SettingsPath)
        End If
    End Function

    Public Function CreateSettingsFolderIfNotExists(ByVal Destination As SettingsFileDestination, Optional ByVal SettingsPath As String = "") As Boolean
        Try
            If Not Destination = SettingsFileDestination.AppRootPath Then
                Dim settingsfolder As String
                settingsfolder = GetSettingsFolder(Destination)

                If Not IO.Directory.Exists(settingsfolder) Then
                    IO.Directory.CreateDirectory(settingsfolder)
                End If
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function LoadSettings(ByVal Destination As SettingsFileDestination, Optional ByVal Appendix As String = "", Optional CreateNewIfNotExists As Boolean = True, Optional ByVal SettingsPath As String = "") As SettingsClass
        Try
            Dim kk As New SettingsSerializer
            CreateSettingsFolderIfNotExists(Destination, SettingsPath)

            Dim sfile As String
            sfile = GetSettingsFilename(Destination, Appendix, SettingsPath)

            If CreateNewIfNotExists Then
                If Not CheckSettingsFileExists(Destination, Appendix, SettingsPath) Then
                    kk.SaveSettingsFile(sfile, New SettingsClass)
                End If
            End If

            Dim obj As SettingsClass
            obj = kk.LoadSettingsFile(sfile)

            Return obj
        Catch ex As Exception
            Return New SettingsClass
        End Try
    End Function

    Public Function SaveSettings(ByVal SettingsObj As SettingsClass, ByVal Destination As SettingsFileDestination, Optional ByVal Appendix As String = "", Optional OverrideFile As Boolean = True, Optional ByVal SettingsPath As String = "") As Boolean
        Try
            Dim result As Boolean = False

            Dim kk As New SettingsSerializer
            CreateSettingsFolderIfNotExists(Destination, SettingsPath)

            Dim sfile As String
            sfile = GetSettingsFilename(Destination, Appendix, SettingsPath)

            Dim fileexists As Boolean
            fileexists = CheckSettingsFileExists(Destination, Appendix, SettingsPath)

            If OverrideFile And fileexists Then
                result = kk.SaveSettingsFile(sfile, SettingsObj)
            Else
                If fileexists = False Then
                    result = kk.SaveSettingsFile(sfile, SettingsObj)
                End If
            End If

            Return result
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function GetSettingsFolder(ByVal Destination As SettingsFileDestination, Optional ByVal Appendix As String = "", Optional ByVal SettingsPath As String = "") As String
        If SettingsPath = "" Then
            If Destination = SettingsFileDestination.AppRootPath Then
                Return "AppSettings" & Appendix & ".xml"
            End If
            If Destination = SettingsFileDestination.AppDataCurrentUser Then
                Return My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData
            End If
            If Destination = SettingsFileDestination.AppDataAllUsers Then
                Return My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData
            End If

            Return "AppSettings" & Appendix & ".xml"
        Else
            Return SettingsPath
        End If
    End Function

    Public Function GetSettingsFilename(ByVal Destination As SettingsFileDestination, Optional ByVal Appendix As String = "", Optional ByVal SettingsPath As String = "") As String
        If SettingsPath = "" Then
            If Destination = SettingsFileDestination.AppRootPath Then
                Return "AppSettings" & Appendix & ".xml"
            End If
            If Destination = SettingsFileDestination.AppDataCurrentUser Then
                Return My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\AppSettings" & Appendix & ".xml"
            End If
            If Destination = SettingsFileDestination.AppDataAllUsers Then
                Return My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\AppSettings" & Appendix & ".xml"
            End If

            Return "AppSettings" & Appendix & ".xml"
        Else
            Return SettingsPath & "\AppSettings" & Appendix & ".xml"
        End If
    End Function

    Public Enum SettingsFileDestination
        AppRootPath = 0
        AppDataCurrentUser = 1
        AppDataAllUsers = 2
    End Enum
End Class
