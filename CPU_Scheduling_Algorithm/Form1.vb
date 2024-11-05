Imports Microsoft.VisualBasic.Logging

Public Class Form1
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        MaterialProgressBar1.Increment(1)
        If MaterialProgressBar1.Value = 100 Then
            Timer1.Stop()
            Me.Hide()
            Menu.Show()
        End If
    End Sub

    Private Sub MaterialProgressBar1_Click(sender As Object, e As EventArgs) Handles MaterialProgressBar1.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
