Public Class EditPropertyFrm
    Public Property _SelectedObj As Object
    Public Property _SelectedObjs As Object()

    Private Sub EditPropertyFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsNothing(_SelectedObjs) Then
            PropertyGrid1.SelectedObjects = _SelectedObjs
        End If

        If Not IsNothing(_SelectedObj) Then
            PropertyGrid1.SelectedObject = _SelectedObj
        End If
    End Sub
End Class