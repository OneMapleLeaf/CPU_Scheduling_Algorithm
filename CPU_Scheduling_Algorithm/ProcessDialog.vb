Public Class ProcessDialog
    Public CPU_Scheduling As Integer
    Dim myForm As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Val(Dialogue_Textbox.Text) <= 0 Or Val(Dialogue_Textbox.Text) > 5 Then
            MessageBox.Show("Invalid Input", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        'FCFS
        If CPU_Scheduling = 1 Then
            For i = 0 To Val(Dialogue_Textbox.Text) - 1
                FCFS_Form.Controls($"P{i}Label").Visible = True
                FCFS_Form.Controls($"AT_P{i}").Visible = True
                FCFS_Form.Controls($"BT_P{i}").Visible = True
                FCFS_Form.Controls($"FT_P{i}").Visible = True
                FCFS_Form.Controls($"TAT_P{i}").Visible = True
                FCFS_Form.Controls($"WT_P{i}").Visible = True
            Next
            FCFS_Form.numOfProcess = Dialogue_Textbox.Text
            FCFS_Form.Enabled = True
            FCFS_Form.Show()
        End If

        'SJF
        If CPU_Scheduling = 2 Then
            For i = 0 To Val(Dialogue_Textbox.Text) - 1
                SJF_Form.Controls($"P{i}Label").Visible = True
                SJF_Form.Controls($"AT_P{i}").Visible = True
                SJF_Form.Controls($"BT_P{i}").Visible = True
                SJF_Form.Controls($"FT_P{i}").Visible = True
                SJF_Form.Controls($"TAT_P{i}").Visible = True
                SJF_Form.Controls($"WT_P{i}").Visible = True
            Next
            SJF_Form.numOfProcess = Dialogue_Textbox.Text
            SJF_Form.Enabled = True
            SJF_Form.Show()
        End If

        'Round Robin
        If CPU_Scheduling = 3 Then
            For i = 0 To Val(Dialogue_Textbox.Text) - 1
                RoundRobin_Form.Controls($"P{i}Label").Visible = True
                RoundRobin_Form.Controls($"AT_P{i}").Visible = True
                RoundRobin_Form.Controls($"BT_P{i}").Visible = True
                RoundRobin_Form.Controls($"FT_P{i}").Visible = True
                RoundRobin_Form.Controls($"TAT_P{i}").Visible = True
                RoundRobin_Form.Controls($"WT_P{i}").Visible = True
            Next
            RoundRobin_Form.Controls("Quantum").Visible = True
            RoundRobin_Form.numOfProcess = Dialogue_Textbox.Text
            RoundRobin_Form.Enabled = True
            RoundRobin_Form.Show()
        End If

        'Priority
        If CPU_Scheduling = 4 Then
            For i = 0 To Val(Dialogue_Textbox.Text) - 1
                Priority_Form.Controls($"P{i}Label").Visible = True
                Priority_Form.Controls($"AT_P{i}").Visible = True
                Priority_Form.Controls($"BT_P{i}").Visible = True
                Priority_Form.Controls($"PT_P{i}").Visible = True
                Priority_Form.Controls($"FT_P{i}").Visible = True
                Priority_Form.Controls($"TAT_P{i}").Visible = True
                Priority_Form.Controls($"WT_P{i}").Visible = True
            Next
            Priority_Form.numOfProcess = Dialogue_Textbox.Text
            Priority_Form.Enabled = True
            Priority_Form.Show()
        End If

        Dialogue_Textbox.Text = 0
        Me.Close()
    End Sub
    Private Sub Button1_Enter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.Width += 3
        Button1.Height += 3
    End Sub

    Private Sub Button1_Leave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.Width -= 3
        Button1.Height -= 3
    End Sub

    Private Sub ProcessDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FCFS_Form.Enabled = False
        SJF_Form.Enabled = False
        RoundRobin_Form.Enabled = False
        Priority_Form.Enabled = False
    End Sub

    Private Sub ProcessDialog_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave

    End Sub
End Class