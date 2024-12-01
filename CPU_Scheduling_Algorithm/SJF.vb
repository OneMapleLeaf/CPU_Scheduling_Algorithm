

Imports System.Diagnostics.Metrics
Imports System.Reflection
Imports System.Runtime.ConstrainedExecution

Public Class SJF_Form
    Public numOfProcess As Integer = 5
    Dim totalFT As Integer = 0

    Dim myTool As Tool = New Tool()

    'Dim buttonScale = 0.75F
    Private Sub Generate_Enter(sender As Object, e As EventArgs) Handles GenerateButton.MouseEnter
        GenerateButton.Width += 3
        GenerateButton.Height += 3
    End Sub

    Private Sub Generate_Leave(sender As Object, e As EventArgs) Handles GenerateButton.MouseLeave
        GenerateButton.Width -= 3
        GenerateButton.Height -= 3
    End Sub

    Private Sub Reset_Enter(sender As Object, e As EventArgs) Handles Button4.MouseEnter
        Button4.Width += 3
        Button4.Height += 3
    End Sub

    Private Sub Reset_Leave(sender As Object, e As EventArgs) Handles Button4.MouseLeave
        Button4.Width -= 3
        Button4.Height -= 3
    End Sub

    Private Sub Back_Enter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        Button2.Width += 3
        Button2.Height += 3
    End Sub

    Private Sub Back_Leave(sender As Object, e As EventArgs) Handles Button2.MouseLeave
        Button2.Width -= 3
        Button2.Height -= 3
    End Sub

    Private Sub Exit_Enter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        Button1.Width += 3
        Button1.Height += 3
    End Sub

    Private Sub Exit_Leave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.Width -= 3
        Button1.Height -= 3
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Menu.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub SJF_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Controls($"AT_P{0}").Text = 3
        'Controls($"AT_P{1}").Text = 0
        'Controls($"AT_P{2}").Text = 2
        'Controls($"AT_P{3}").Text = 0
        'Controls($"AT_P{4}").Text = 1

        'Controls($"BT_P{0}").Text = 5
        'Controls($"BT_P{1}").Text = 4
        'Controls($"BT_P{2}").Text = 1
        'Controls($"BT_P{3}").Text = 2
        'Controls($"BT_P{4}").Text = 6

        'For i = 0 To numOfProcess - 1
        '    Controls($"P{i}Label").Visible = True
        '    Controls($"AT_P{i}").Visible = True
        '    Controls($"BT_P{i}").Visible = True
        '    Controls($"FT_P{i}").Visible = True
        '    Controls($"TAT_P{i}").Visible = True
        '    Controls($"WT_P{i}").Visible = True
        'Next

        For i = 0 To 80
            Controls($"GC_{i}").Visible = False
        Next

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
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
        For j = 0 To 80
            Controls($"GC_{j}").BackColor = Color.FromArgb(255, 255, 255)
            Controls($"GC_Label{j}").Visible = False
        Next
        AVG_TAT.Text = ""
        AVG_WT.Text = ""
        ProcessDialog.CPU_Scheduling = 2
        ProcessDialog.Show()
    End Sub

    Private Sub GenerateButton_Click(sender As Object, e As EventArgs) Handles GenerateButton.Click
        'checks if all processes are filled
        If (Not myTool.CheckIfProcessAreFilled(numOfProcess, Me)) Then
            Return
        End If

        ' Check if input are numbers
        For i = 0 To numOfProcess - 1
            If (Not myTool.checkIfProcessAreNumbers(Controls($"AT_P{i}").Text) Or
                Not myTool.checkIfProcessAreNumbers(Controls($"BT_P{i}").Text)) Then
                MessageBox.Show($"only numerical input value allowed", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        ' Checks if all the input are less than 10
        For i = 0 To numOfProcess - 1
            If Not myTool.isNumLessThan10(Controls($"AT_P{i}").Text) Or Not myTool.isNumLessThan10(Controls($"BT_P{i}").Text) Then
                MessageBox.Show($"Allowed input is only 1-10", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next


        GenerateButton.Visible = False
        Dim AT As New List(Of Integer)
        Dim BT As New List(Of Integer)

        Dim ZeroBT As New List(Of Integer)
        Dim ZeroBTIndex As New List(Of Integer)

        Dim NonZeroBT As New List(Of Integer)
        Dim NonZeroBTIndex As New List(Of Integer)
        Dim NonZeroAT As New List(Of Integer)

        Dim processOrder As New List(Of Integer)

        Dim FT(numOfProcess - 1) As Integer
        Dim TAT(numOfProcess - 1) As Integer
        Dim WT(numOfProcess - 1) As Integer

        Dim isProcessAdded(numOfProcess - 1) As Boolean

        For i = 0 To numOfProcess - 1
            AT.Add(Controls($"AT_P{i}").Text)
            BT.Add(Controls($"BT_P{i}").Text)
            isProcessAdded(i) = False
        Next

        Dim zeroCounter As Integer = 0

        For i = 0 To numOfProcess - 1
            If AT(i) = 0 Then
                ZeroBT.Add(BT(i))
                ZeroBTIndex.Add(i)
                zeroCounter += 1
            Else
                NonZeroBT.Add(BT(i))
                NonZeroBTIndex.Add(i)
                NonZeroAT.Add(AT(i))
            End If
        Next

        If zeroCounter > 0 Then
            For i = 0 To ZeroBT.Count - 2
                For j = i + 1 To ZeroBT.Count - 1
                    Dim shouldSwap As Boolean = False
                    If ZeroBT(i) > ZeroBT(j) Then
                        shouldSwap = True
                    ElseIf ZeroBT(i) = ZeroBT(j) Then
                        Dim idx1 As Integer = ZeroBTIndex(i)
                        Dim idx2 As Integer = ZeroBTIndex(j)
                        If AT(idx1) < AT(idx2) Then
                            shouldSwap = True
                        End If
                    End If

                    If shouldSwap Then
                        Dim temp As Integer = ZeroBT(i)
                        ZeroBT(i) = ZeroBT(j)
                        ZeroBT(j) = temp

                        Dim tempIndex As Integer = ZeroBTIndex(i)
                        ZeroBTIndex(i) = ZeroBTIndex(j)
                        ZeroBTIndex(j) = tempIndex
                    End If
                Next
            Next

            Dim ZeroBTSorted As New List(Of Integer)
            For Each process In ZeroBTIndex
                ZeroBTSorted.Add(process)
            Next

            For Each process In ZeroBTSorted
                processOrder.Add(process)
                isProcessAdded(process) = True
            Next
        End If

        For i = 0 To NonZeroBT.Count - 2
            For j = i + 1 To NonZeroBT.Count - 1
                Dim shouldSwap As Boolean = False

                If NonZeroBT(i) > NonZeroBT(j) Then
                    shouldSwap = True
                ElseIf NonZeroBT(i) = NonZeroBT(j) Then
                    If NonZeroAT(i) < NonZeroAT(j) Then
                        shouldSwap = True
                    End If
                End If

                If shouldSwap Then
                    Dim temp As Integer = NonZeroBT(i)
                    NonZeroBT(i) = NonZeroBT(j)
                    NonZeroBT(j) = temp

                    Dim tempIndex As Integer = NonZeroBTIndex(i)
                    NonZeroBTIndex(i) = NonZeroBTIndex(j)
                    NonZeroBTIndex(j) = tempIndex

                    Dim tempAT As Integer = NonZeroAT(i)
                    NonZeroAT(i) = NonZeroAT(j)
                    NonZeroAT(j) = tempAT
                End If
            Next
        Next

        For Each process In NonZeroBTIndex
            processOrder.Add(process)
            isProcessAdded(process) = True
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
End Class