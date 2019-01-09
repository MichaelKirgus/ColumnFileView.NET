Imports System.IO
Imports System.Xml.Serialization

Public Class ProcessingFileSerializer
    Public Function LoadDefFile(ByVal Filename As String) As ProcessingItem
        Try
            Dim objStreamReader As New StreamReader(Filename)
            Dim p2 As New ProcessingItem
            Dim x As New XmlSerializer(p2.GetType)
            p2 = x.Deserialize(objStreamReader)
            objStreamReader.Close()

            Return p2
        Catch ex As Exception
            Return New ProcessingItem
        End Try
    End Function

    Public Function SaveDefFile(ByVal Filename As String, ByVal Obj As ProcessingItem) As Boolean
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
