Public Class RoundRobin_Form
    Public numOfProcess As Integer
    Dim totalFT As Integer = 0

    Dim myTool As Tool = New Tool()

    Dim buttonScale = 0.75F

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        If GenerateButton.Visible = False Then
            GenerateButton.Visible = True
        End If
        For i = 0 To numOfProcess - 1
            Controls($"P{i}Label").Visible = False
            Controls($"AT_P{i}").Visible = False
            Controls($"BT_P{i}").Visible = False
            Controls($"FT_P{i}").Visible = False
            Controls($"TAT_P{i}").Visible = False
            Controls($"WT_P{i}").Visible = False

            Controls($"AT_P{i}").Text = ""
            Controls($"BT_P{i}").Text = ""
            Controls($"FT_P{i}").Text = 0
            Controls($"TAT_P{i}").Text = 0
            Controls($"WT_P{i}").Text = 0
        Next
        Controls("Quantum").Visible = False
        Controls("Quantum").Text = 0
        For j = 0 To 80
            Controls($"GC_{j}").BackColor = Color.FromArgb(255, 255, 255)
            Controls($"GC_Label{j}").Visible = False
        Next
        AVG_TAT.Text = ""
        AVG_WT.Text = ""
        Quantum.Text = 0
        ProcessDialog.CPU_Scheduling = 3
        ProcessDialog.Show()
    End Sub

    Private Sub Reset_Enter(sender As Object, e As EventArgs) Handles ResetButton.MouseEnter
        ResetButton.Width += 3
        ResetButton.Height += 3
    End Sub

    Private Sub Reset_Leave(sender As Object, e As EventArgs) Handles ResetButton.MouseLeave
        ResetButton.Width -= 3
        ResetButton.Height -= 3
    End Sub

    Private Sub Exit_Enter(sender As Object, e As EventArgs) Handles ExitButton.MouseEnter
        ExitButton.Width += 3
        ExitButton.Height += 3
    End Sub
    Private Sub Exit_Leave(sender As Object, e As EventArgs) Handles ExitButton.MouseLeave
        ExitButton.Width -= 3
        ExitButton.Height -= 3
    End Sub

    Private Sub Back_Enter(sender As Object, e As EventArgs) Handles BackButton.MouseEnter
        BackButton.Width += 3
        BackButton.Height += 3
    End Sub
    Private Sub Back_Exit(sender As Object, e As EventArgs) Handles BackButton.MouseLeave
        BackButton.Width -= 3
        BackButton.Height -= 3
    End Sub

    Private Sub Generate_Enter(sender As Object, e As EventArgs) Handles GenerateButton.MouseEnter
        GenerateButton.Width += 3
        GenerateButton.Height += 3
    End Sub


    Private Sub Generate_Leave(sender As Object, e As EventArgs) Handles GenerateButton.MouseLeave
        GenerateButton.Width -= 3
        GenerateButton.Height -= 3
    End Sub

    Private Sub RoundRobin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Quantum.Text = 2
        'AT_P0.Text = 2
        'AT_P1.Text = 0
        'AT_P2.Text = 1
        'AT_P3.Text = 3
        'AT_P4.Text = 2

        'BT_P0.Text = 3
        'BT_P1.Text = 5
        'BT_P2.Text = 4
        'BT_P3.Text = 2
        'BT_P4.Text = 1
        'For i = 0 To numOfProcess - 1
        '    Controls($"P{i}Label").Visible = True
        '    Controls($"AT_P{i}").Visible = True
        '    Controls($"BT_P{i}").Visible = True
        '    Controls($"FT_P{i}").Visible = True
        '    Controls($"TAT_P{i}").Visible = True
        '    Controls($"WT_P{i}").Visible = True

        'Next
        Quantum.Visible = True
        Quantum.Focus()
        For i = 0 To 80
            Controls($"GC_{i}").Visible = False
            Controls($"GC_Label{i}").Visible = False
        Next
        'Controls($"GC_{0}").Width = 5
        'Controls($"GC_{0}").Location = New Point(62, 446)
        'Controls($"GC_{0}").BackColor = Color.White
        'For i = 1 To 80 - 1
        '    Controls($"GC_{i}").Width = 5
        '    Controls($"GC_{i}").Location = New Point(Controls($"GC_{i - 1}").Location.X + 12, 446)
        '    Controls($"GC_{i}").BackColor = Color.White
        'Next
    End Sub

    Private Sub GenerateButton_Click(sender As Object, e As EventArgs) Handles GenerateButton.Click
        If Val(Quantum.Text) <= 0 Then
            MessageBox.Show("Quantum must be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Quantum.Focus()
            Return
        End If

        If (Not myTool.CheckIfProcessAreFilled(numOfProcess, Me)) Then
            Return
        End If

        For i = 0 To numOfProcess - 1
            If (Not myTool.checkIfProcessAreNumbers(Controls($"AT_P{i}").Text) Or
            Not myTool.checkIfProcessAreNumbers(Controls($"BT_P{i}").Text) Or
            Not myTool.checkIfProcessAreNumbers(Controls("Quantum").Text)) Then
                MessageBox.Show("Only numerical input values allowed", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        For i = 0 To numOfProcess - 1
            If (Not myTool.isNumLessThan10(Controls($"AT_P{i}").Text) Or
            Not myTool.isNumLessThan10(Controls($"BT_P{i}").Text) Or
            Not myTool.isNumLessThan10(Controls($"Quantum").Text)) Then
                MessageBox.Show("Allowed input is only 1-10", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next


         GenerateButton.Visible = False
        Dim AT As New List(Of Integer)
        Dim ATIndex As New List(Of Integer)
        Dim BT As New List(Of Integer)
        Dim FT As New List(Of Integer)
        Dim TAT As New List(Of Integer)(New Integer(numOfProcess - 1) {})
        Dim WT As New List(Of Integer)(New Integer(numOfProcess - 1) {})

        Dim processOrder As New List(Of Integer)
        Dim isProcessAdded As New List(Of Boolean)()

        For i = 0 To numOfProcess - 1
            AT.Add(Controls($"AT_P{i}").Text)
            ATIndex.Add(i)
            BT.Add(Controls($"BT_P{i}").Text)
            isProcessAdded.Add(False)
        Next

        Dim copyAT As New List(Of Integer)
        copyAT.AddRange(AT)

        For i = 0 To AT.Count - 2
            For j = 0 To AT.Count - i - 2
                If AT(j) > AT(j + 1) Then
                    Dim tempAT As Integer = AT(j)
                    AT(j) = AT(j + 1)
                    AT(j + 1) = tempAT

                    Dim tempIndex As Integer = ATIndex(j)
                    ATIndex(j) = ATIndex(j + 1)
                    ATIndex(j + 1) = tempIndex
                End If
            Next
        Next

        For Each ATs In AT
            Debug.WriteLine(ATs)
        Next

        For i = 0 To AT.Count - 1
            processOrder.Add(ATIndex(i))
        Next

        Dim quant As Integer = Val(Quantum.Text)
        Dim time As Integer = 0
        Dim remainingBT As New List(Of Integer)(BT)
        Dim cur As Integer = 0

        Dim allProcessesCompleted As Boolean = False
        Dim ganttIndex As Integer = 0

        While Not allProcessesCompleted
            allProcessesCompleted = True

            For i = 0 To numOfProcess - 1
                Dim processIndex As Integer = processOrder(i)

                If remainingBT(processIndex) > 0 Then
                    allProcessesCompleted = False

                    Dim timeSpent As Integer
                    If remainingBT(processIndex) < quant Then
                        timeSpent = remainingBT(processIndex)
                    Else
                        timeSpent = quant
                    End If

                    Dim startTime As Integer = time
                    time += timeSpent
                    Dim endTime As Integer = time

                    remainingBT(processIndex) -= timeSpent

                    For k = startTime + 1 To endTime
                        Controls($"GC_{k}").BackColor = myTool.getColor(processIndex)
                        Controls($"GC_{k}").Visible = True
                        Controls($"GC_Label{k}").Visible = True
                    Next


                    If remainingBT(processIndex) = 0 Then
                        FT(processIndex) = endTime
                    End If

                    ganttIndex += 1
                End If
            Next
        End While
        Dim totalGanttChart As Integer = 0

        totalGanttChart = FT(processOrder(numOfProcess - 1))

        If totalGanttChart > 80 Then
            MessageBox.Show("Burst Time Size is too big for the Gantt Chart. Please reduce the Burst Time size.", "")
            Return
        End If

        For i = 0 To FT.Count - 1
            Controls($"FT_P{i}").Text = FT(i)
        Next

        'calculation for TAT and WT
        Dim totalTAT As Double = 0
        Dim totalWT As Double = 0

        For k = 0 To numOfProcess - 1
            Controls($"TAT_P{k}").Text = $"{FT(k) - Controls($"AT_P{k}").Text}"
            TAT(k) = Controls($"TAT_P{k}").Text
            Controls($"WT_P{k}").Text = $"{TAT(k) - BT(k)}"
            WT(k) = Controls($"WT_P{k}").Text
        Next

        For l = 0 To numOfProcess - 1
            totalTAT += TAT(l)
            totalWT += WT(l)
        Next

        'calculation for avg TAT and WT
        AVG_TAT.Text = $"{(totalTAT / numOfProcess).ToString("0.00")} ms"
        AVG_WT.Text = $"{(totalWT / numOfProcess).ToString("0.00")} ms"
    End Sub


    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Menu.Show()
        Me.Close()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Application.Exit()
    End Sub
End Class
