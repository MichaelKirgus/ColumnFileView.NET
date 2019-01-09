Imports System.Text

Public Class EncodingHelper
    Public Function GetAllEncodingsAsStringArray() As List(Of String)
        Try
            Dim arrstr As New List(Of String)

            Dim def As System.Text.Encoding
            def = System.Text.Encoding.Default

            For index = 0 To System.Text.Encoding.GetEncodings.Length - 1
                arrstr.Add(System.Text.Encoding.GetEncodings(index).CodePage & " (" & System.Text.Encoding.GetEncodings(index).DisplayName & ")")

                If System.Text.Encoding.GetEncodings(index).CodePage = def.CodePage Then
                    arrstr(index) = arrstr(index) & " (*)"
                End If
            Next

            Return arrstr
        Catch ex As Exception
            Return New List(Of String)
        End Try
    End Function

    Public Function GetDefaultEncodingIndex() As Integer
        Try
            For index = 0 To System.Text.Encoding.GetEncodings.Length - 1
                If System.Text.Encoding.GetEncodings(index).CodePage = System.Text.Encoding.Default.CodePage Then
                    Return index
                End If
            Next

            Return 0
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function GetEncodingFromIndex(ByVal Index As Integer) As EncodingInfo
        Try
            Return System.Text.Encoding.GetEncodings(Index)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetEncodingDescTextFromIndex(ByVal Index As Integer) As String
        Try
            Return GetAllEncodingsAsStringArray(Index)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
