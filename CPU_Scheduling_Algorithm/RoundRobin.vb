Public Class RoundRobin_Form
    Public numOfProcess As Integer = 5
    Dim totalFT As Integer = 0

    Dim myTool As Tool = New Tool()

    Dim buttonScale = 0.75F
    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        For i = 0 To numOfProcess - 1
            Controls($"P{i}Label").Visible = False
            Controls($"AT_P{i}").Visible = False
            Controls($"BT_P{i}").Visible = False
            Controls($"FT_P{i}").Visible = False
            Controls($"TAT_P{i}").Visible = False
            Controls($"WT_P{i}").Visible = False
            Controls($"GC_Label{i}").Visible = False
            Controls($"AT_P{i}").Text = ""
            Controls($"BT_P{i}").Text = ""
            Controls($"FT_P{i}").Text = 0
            Controls($"TAT_P{i}").Text = 0
            Controls($"WT_P{i}").Text = 0
        Next
        Controls("Quantum").Visible = False
        Controls("Quantum").Text = 0
        For j = 0 To 79
            Controls($"GC_{j}").BackColor = Color.FromArgb(255, 255, 255)
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
        Quantum.Text = 2
        AT_P0.Text = 3
        AT_P1.Text = 1
        AT_P2.Text = 2
        AT_P3.Text = 1
        AT_P4.Text = 5

        BT_P0.Text = 6
        BT_P1.Text = 3
        BT_P2.Text = 9
        BT_P3.Text = 6
        BT_P4.Text = 3
        For i = 0 To numOfProcess - 1
            Controls($"P{i}Label").Visible = True
            Controls($"AT_P{i}").Visible = True
            Controls($"BT_P{i}").Visible = True
            Controls($"FT_P{i}").Visible = True
            Controls($"TAT_P{i}").Visible = True
            Controls($"WT_P{i}").Visible = True
            Controls($"GC_Label{i}").Visible = False
        Next
        Quantum.Focus()

        Controls($"GC_{0}").Width = 5
        Controls($"GC_{0}").Location = New Point(62, 446)
        Controls($"GC_{0}").BackColor = Color.White
        For i = 1 To 80 - 1
            Controls($"GC_{i}").Width = 5
            Controls($"GC_{i}").Location = New Point(Controls($"GC_{i - 1}").Location.X + 12, 446)
            Controls($"GC_{i}").BackColor = Color.White
        Next
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

        Dim AT As New List(Of Integer)
        Dim ATIndex As New List(Of Integer)
        Dim BT As New List(Of Integer)
        Dim FT As New List(Of Integer)

        Dim processOrder As New List(Of Integer)
        Dim isProcessAdded As New List(Of Boolean)()

        ' Populate the lists from controls
        For i = 0 To numOfProcess - 1
            AT.Add(Controls($"AT_P{i}").Text)
            ATIndex.Add(i)
            BT.Add(Controls($"BT_P{i}").Text)
            isProcessAdded.Add(False)
        Next

        Dim copyAT As New List(Of Integer)
        copyAT.AddRange(AT)

        ' Sort the AT list
        myTool.sortList(AT, AT.Count - 1)

        ' Find original indices for sorted AT elements
        For i = 0 To AT.Count - 1
            For j = 0 To copyAT.Count - 1
                If AT(i) = copyAT(j) AndAlso Not isProcessAdded(j) Then
                    processOrder.Add(j)
                    isProcessAdded(j) = True
                    Exit For
                End If
            Next
        Next

        ' Debug output to verify process order
        Debug.WriteLine("Process Order (based on original indices):")
        For Each index In processOrder
            Debug.WriteLine($"{index}")
        Next


        For i = 0 To numOfProcess - 1
            While BT(processOrder(i)) > 0

            End While
        Next
        'For x = 0 To processOrder.Count - 1
        '    Debug.WriteLine($"Process {x}: {processOrder(x)}")
        'Next
        ' Display the finish times for verification or further processing
        'For i = 0 To numOfProcess - 1
        '    Debug.WriteLine($"Process {i}: Finish Time = {FT(i)}")
        'Next
    End Sub


    Private Sub BackButton_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Menu.Show()
        Me.Close()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Application.Exit()
    End Sub
End Class
