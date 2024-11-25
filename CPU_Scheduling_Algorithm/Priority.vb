Public Class Priority_Form
    Public numOfProcess As Integer = 5
    Dim totalFT As Integer = 0

    Dim myTool As Tool = New Tool()

    Dim buttonScale = 0.75F
    Private Sub Priority_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'AT_P0.Text = 1
        'AT_P1.Text = 3
        'AT_P2.Text = 0
        'AT_P3.Text = 5
        'AT_P4.Text = 4

        'BT_P0.Text = 3
        'BT_P1.Text = 1
        'BT_P2.Text = 2
        'BT_P3.Text = 5
        'BT_P4.Text = 6

        'PT_P0.Text = 3
        'PT_P1.Text = 1
        'PT_P2.Text = 2
        'PT_P3.Text = 5
        'PT_P4.Text = 6

        'For i = 0 To numOfProcess - 1
        '    Controls($"P{i}Label").Visible = True
        '    Controls($"AT_P{i}").Visible = True
        '    Controls($"BT_P{i}").Visible = True
        '    Controls($"PT_P{i}").Visible = True
        '    Controls($"FT_P{i}").Visible = True
        '    Controls($"TAT_P{i}").Visible = True
        '    Controls($"WT_P{i}").Visible = True
        '    Controls($"GC_Label{i}").Visible = False
        'Next


        For i = 0 To 80
            Controls($"GC_{i}").Visible = False
        Next
    End Sub

    Private Sub Reset_Enter(sender As Object, e As EventArgs) Handles ResetButton.MouseEnter
        ResetButton.Width += 3
        ResetButton.Height += 3
    End Sub

    Private Sub Reset_Leave(sender As Object, e As EventArgs) Handles ResetButton.MouseLeave
        ResetButton.Width -= 3
        ResetButton.Height -= 3
    End Sub

    Private Sub Back_Leave(sender As Object, e As EventArgs) Handles BackButton.MouseLeave
        BackButton.Width += 3
        BackButton.Height += 3
    End Sub

    Private Sub Back_Enter(sender As Object, e As EventArgs) Handles BackButton.MouseEnter
        BackButton.Width -= 3
        BackButton.Height -= 3
    End Sub

    Private Sub Exit_Enter(sender As Object, e As EventArgs) Handles ExitButton.MouseEnter
        ExitButton.Width += 3
        ExitButton.Height += 3
    End Sub

    Private Sub Exit_Leave(sender As Object, e As EventArgs) Handles ExitButton.MouseLeave
        ExitButton.Width -= 3
        ExitButton.Height -= 3
    End Sub

    Private Sub Generte_Enter(sender As Object, e As EventArgs) Handles GenerateButton.MouseEnter
        GenerateButton.Width += 3
        GenerateButton.Height += 3
    End Sub

    Private Sub Generte_Leave(sender As Object, e As EventArgs) Handles GenerateButton.MouseLeave
        GenerateButton.Width -= 3
        GenerateButton.Height -= 3
    End Sub

    Private Sub GenerateButton_Click(sender As Object, e As EventArgs) Handles GenerateButton.Click
        'checks if all processes are filled
        If (Not myTool.CheckIfProcessAreFilled(numOfProcess, Me)) Then
            Return
        End If

        'check if input are numerical values
        For i = 0 To numOfProcess - 1
            If (Not myTool.checkIfProcessAreNumbers(Controls($"AT_P{i}").Text) Or
                Not myTool.checkIfProcessAreNumbers(Controls($"BT_P{i}").Text) Or
                Not myTool.checkIfProcessAreNumbers(Controls($"PT_P{i}").Text)) Then
                MessageBox.Show($"only numerical input value allowed", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        'check if all the input are less than 10
        For i = 0 To numOfProcess - 1
            If (Not myTool.isNumLessThan10(Controls($"AT_P{i}").Text) Or
                Not myTool.isNumLessThan10(Controls($"BT_P{i}").Text) Or
                Not myTool.isNumLessThan10(Controls($"PT_P{i}").Text)) Then
                MessageBox.Show($"Allowed input is only 1-10", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        Dim AT As New List(Of Integer)
        Dim PT As New List(Of Integer)
        Dim BT As New List(Of Integer)

        Dim ZeroPT As New List(Of Integer)
        Dim ZeroPTIndex As New List(Of Integer)

        Dim NonZeroPT As New List(Of Integer)
        Dim NonZeroPTIndex As New List(Of Integer)
        Dim NonZeroAT As New List(Of Integer)

        Dim processOrder As New List(Of Integer)



        Dim FT(numOfProcess - 1) As Integer
        Dim TAT(numOfProcess - 1) As Integer
        Dim WT(numOfProcess - 1) As Integer

        Dim isProcessAdded(numOfProcess - 1) As Boolean

        For i = 0 To numOfProcess - 1
            AT.Add(Controls($"AT_P{i}").Text)
            PT.Add(Controls($"PT_P{i}").Text)
            BT.Add(Controls($"BT_P{i}").Text)
            isProcessAdded(i) = False
        Next

        ' Process arrival time zero separately
        Dim zeroCounter As Integer = 0
        For i = 0 To numOfProcess - 1
            If AT(i) = 0 Then
                ZeroPT.Add(PT(i))
                ZeroPTIndex.Add(i)
                zeroCounter += 1
            End If
        Next

        If zeroCounter > 0 Then
            For i = 0 To ZeroPT.Count - 2
                For j = i + 1 To ZeroPT.Count - 1
                    Dim shouldSwap As Boolean = False

                    If ZeroPT(i) > ZeroPT(j) Then
                        shouldSwap = True
                    ElseIf ZeroPT(i) = ZeroPT(j) Then
                        Dim idx1 As Integer = ZeroPTIndex(i)
                        Dim idx2 As Integer = ZeroPTIndex(j)
                        If AT(idx1) > AT(idx2) Then
                            shouldSwap = True
                        End If
                    End If

                    If shouldSwap Then
                        Dim tempPriority As Integer = ZeroPT(i)
                        ZeroPT(i) = ZeroPT(j)
                        ZeroPT(j) = tempPriority

                        Dim tempIndex As Integer = ZeroPTIndex(i)
                        ZeroPTIndex(i) = ZeroPTIndex(j)
                        ZeroPTIndex(j) = tempIndex
                    End If
                Next
            Next

            Dim zeroPTSorted As New List(Of Integer)
            For Each priority In ZeroPTIndex
                zeroPTSorted.Add(priority)
            Next

            For Each priority In zeroPTSorted
                processOrder.Add(priority)
                isProcessAdded(priority) = True
            Next
        End If

        ' Process arrival time non-zero separately
        For i = 0 To numOfProcess - 1
            If AT(i) <> 0 Then
                NonZeroPT.Add(PT(i))
                NonZeroPTIndex.Add(i)
                NonZeroAT.Add(AT(i))
            End If
        Next

        For i = 0 To NonZeroPT.Count - 2
            For j = i + 1 To NonZeroPT.Count - 1
                Dim shouldSwap As Boolean = False

                If NonZeroPT(i) > NonZeroPT(j) Then
                    shouldSwap = True
                ElseIf NonZeroPT(i) = NonZeroPT(j) Then
                    If NonZeroAT(i) > NonZeroAT(j) Then
                        shouldSwap = True
                    End If
                End If

                If shouldSwap Then
                    Dim tmepPriority As Integer = NonZeroPT(i)
                    NonZeroPT(i) = NonZeroPT(j)
                    NonZeroPT(j) = tmepPriority

                    Dim tempAT As Integer = NonZeroAT(i)
                    NonZeroAT(i) = NonZeroAT(j)
                    NonZeroAT(j) = tempAT

                    Dim tempIndex As Integer = NonZeroPTIndex(i)
                    NonZeroPTIndex(i) = NonZeroPTIndex(j)
                    NonZeroPTIndex(j) = tempIndex
                End If
            Next
        Next

        For Each priority In NonZeroPTIndex
            processOrder.Add(priority)
            isProcessAdded(priority) = True
        Next

        'generate gantt chart
        Dim cur As Integer = 0
        Dim finishTime As Integer = 0
        Dim totalGanttChart As Integer = 0

        For i = 0 To numOfProcess - 1
            Dim processIndex = processOrder(i)
            finishTime += BT(processOrder(i))
            totalFT += finishTime
            FT(processIndex) = finishTime
            Controls($"FT_P{processIndex}").Text = $"{finishTime}"
        Next

        totalGanttChart = FT(processOrder(numOfProcess - 1))

        If totalGanttChart > 80 Then
            MessageBox.Show("Burst Time Size is too big for the Gantt Chart. Please reduce the Burst Time size.", "")
            Return
        End If

        Controls($"GC_Label{0}").Visible = True
        Dim indexNum = 1
        For j = 0 To numOfProcess - 1
            Debug.WriteLine($"J: {j}")
            Debug.WriteLine($"BT: {BT(j)}")
            For k = 0 To BT(j) - 1
                Controls($"GC_{indexNum}").BackColor = myTool.getColor(processOrder(j))
                indexNum += 1
            Next
        Next
        For i = 1 To totalGanttChart
            Controls($"GC_Label{i}").Visible = True
            Controls($"GC_{i}").Visible = True
        Next

        'calculation for TAT and WT
        Dim totalTAT As Double = 0
        Dim totalWT As Double = 0

        For k = 0 To numOfProcess - 1
            Controls($"TAT_P{k}").Text = $"{FT(k) - AT(k)}"
            TAT(k) = Controls($"TAT_P{k}").Text
            Controls($"WT_P{k}").Text = $"{TAT(k) - PT(k)}"
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

    Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click
        For i = 0 To numOfProcess - 1
            Controls($"P{i}Label").Visible = False
            Controls($"AT_P{i}").Visible = False
            Controls($"BT_P{i}").Visible = False
            Controls($"PT_P{i}").Visible = False
            Controls($"FT_P{i}").Visible = False
            Controls($"TAT_P{i}").Visible = False
            Controls($"WT_P{i}").Visible = False

            Controls($"AT_P{i}").Text = ""
            Controls($"BT_P{i}").Text = ""
            Controls($"FT_P{i}").Text = 0
            Controls($"TAT_P{i}").Text = 0
            Controls($"WT_P{i}").Text = 0
        Next
        For j = 0 To 80
            Controls($"GC_{j}").BackColor = Color.FromArgb(255, 255, 255)
            Controls($"GC_Label{j}").Visible = False
        Next
        AVG_TAT.Text = ""
        AVG_WT.Text = ""
        ProcessDialog.CPU_Scheduling = 4
        ProcessDialog.Show()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Application.Exit()
    End Sub

    Private Sub GC_Label2_Click(sender As Object, e As EventArgs) Handles GC_Label2.Click

    End Sub

    Private Sub MaterialLabel11_Click(sender As Object, e As EventArgs) Handles GC_Label46.Click

    End Sub

    Private Sub MaterialLabel8_Click(sender As Object, e As EventArgs) Handles GC_Label75.Click

    End Sub
End Class