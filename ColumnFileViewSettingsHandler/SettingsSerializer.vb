Imports System.IO
Imports System.Xml.Serialization

Public Class SettingsSerializer
    Public Function LoadSettingsFile(ByVal Filename As String) As SettingsClass
        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New SettingsClass
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New SettingsClass
        End Try
    End Function

    Public Function SaveSettingsFile(ByVal Filename As String, ByVal Obj As SettingsClass) As Boolean
        Try
            Dim XML As New XmlSerializer(Obj.GetType)
            Dim FS As New FileStream(Filename, FileMode.Create)
            XML.Serialize(FS, Obj)
            FS.Close()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
