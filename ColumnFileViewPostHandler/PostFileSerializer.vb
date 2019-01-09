Imports System.IO
Imports System.Xml.Serialization

Public Class PostFileSerializer
    Public Function LoadDefFile(ByVal Filename As String) As PostFileHandler
        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New PostFileHandler
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New PostFileHandler
        End Try
    End Function

    Public Function SaveDefFile(ByVal Filename As String, ByVal Obj As PostFileHandler) As Boolean
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
