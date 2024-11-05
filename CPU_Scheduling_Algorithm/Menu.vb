Public Class Menu
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Application.Exit()

    End Sub
    Private Sub Menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub FCFSbtn_Click(sender As Object, e As EventArgs) Handles FCFSbtn.Click
        ProcessDialog.CPU_Scheduling = 1
        FCFS_Form.Show()
        ProcessDialog.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        Button2.Width += 3
        Button2.Height += 3
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        Button2.Width -= 3
        Button2.Height -= 3
    End Sub

    Private Sub FCFSbtn_Enter(sender As Object, e As EventArgs) Handles FCFSbtn.MouseEnter
        FCFSbtn.Width += 3
        FCFSbtn.Height += 3
    End Sub

    Private Sub FCFSbtn_Leave(sender As Object, e As EventArgs) Handles FCFSbtn.MouseLeave
        FCFSbtn.Width -= 3
        FCFSbtn.Height -= 3
    End Sub

    Private Sub SJFbtn_Enter(sender As Object, e As EventArgs) Handles SJFbtn.MouseEnter
        SJFbtn.Width += 3
        SJFbtn.Height += 3
    End Sub

    Private Sub SJFbtn_Leave(sender As Object, e As EventArgs) Handles SJFbtn.MouseLeave
        SJFbtn.Width -= 3
        SJFbtn.Height -= 3
    End Sub

    Private Sub RRSbtn_Enter(sender As Object, e As EventArgs) Handles RRSbtn.MouseEnter
        RRSbtn.Width += 3
        RRSbtn.Height += 3
    End Sub

    Private Sub RRSbtn_Leave(sender As Object, e As EventArgs) Handles RRSbtn.MouseLeave
        RRSbtn.Width -= 3
        RRSbtn.Height -= 3
    End Sub

    Private Sub Prioritybtn_Enter(sender As Object, e As EventArgs) Handles Prioritybtn.MouseEnter
        Prioritybtn.Width += 3
        Prioritybtn.Height += 3
    End Sub

    Private Sub Prioritybtn_Leave(sender As Object, e As EventArgs) Handles Prioritybtn.MouseLeave
        Prioritybtn.Width -= 3
        Prioritybtn.Height -= 3
    End Sub

    Private Sub SRCSbtn_Enter(sender As Object, e As EventArgs) Handles SRCSbtn.MouseEnter
        SRCSbtn.Width += 3
        SRCSbtn.Height += 3
    End Sub

    Private Sub SRCSbtn_Leave(sender As Object, e As EventArgs) Handles SRCSbtn.MouseLeave
        SRCSbtn.Width -= 3
        SRCSbtn.Height -= 3
    End Sub

    Private Sub SJFbtn_Click(sender As Object, e As EventArgs) Handles SJFbtn.Click
        ProcessDialog.CPU_Scheduling = 2
        SJF_Form.Show()
        ProcessDialog.Show()
        Me.Hide()

    End Sub

    Private Sub SRCSbtn_Click(sender As Object, e As EventArgs) Handles SRCSbtn.Click

    End Sub

    Private Sub RRSbtn_Click(sender As Object, e As EventArgs) Handles RRSbtn.Click
        ProcessDialog.CPU_Scheduling = 3
        RoundRobin_Form.Show()
        ProcessDialog.Show()
        Me.Hide()
    End Sub

    Private Sub Prioritybtn_Click(sender As Object, e As EventArgs) Handles Prioritybtn.Click
        ProcessDialog.CPU_Scheduling = 4
        Priority_Form.Show()
        ProcessDialog.Show()
        Me.Hide()
    End Sub
End Class