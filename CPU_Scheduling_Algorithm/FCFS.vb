Imports System.Diagnostics.Metrics
Imports System.Drawing.Imaging
Imports System.Reflection
Imports System.Security.Cryptography.Xml

Public Class FCFS_Form
    Public numOfProcess As Integer = 5
    Dim totalFT As Integer = 0

    Dim myTool As Tool = New Tool()

    Dim buttonScale = 0.75F
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles BackButton.Click
        Menu.Show()
        Me.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Application.Exit()
    End Sub

    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles BackButton.MouseEnter
        BackButton.Width += buttonScale
        BackButton.Height += buttonScale
    End Sub

    Private Sub Button2_MouseLeave(sender As Object, e As EventArgs) Handles BackButton.MouseLeave
        BackButton.Width -= buttonScale
        BackButton.Height -= buttonScale
    End Sub

    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles ExitButton.MouseEnter
        ExitButton.Width += buttonScale
        ExitButton.Height += buttonScale
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles ExitButton.MouseLeave
        ExitButton.Width -= buttonScale
        ExitButton.Height -= buttonScale
    End Sub

    Private Sub Generate_Enter(sender As Object, e As EventArgs) Handles GenerateButton.MouseEnter
        GenerateButton.Width += buttonScale
        GenerateButton.Height += buttonScale
    End Sub

    Private Sub Generate_Leave(sender As Object, e As EventArgs) Handles GenerateButton.MouseLeave
        GenerateButton.Width -= buttonScale
        GenerateButton.Height -= buttonScale
    End Sub

    Private Sub Button(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Enter(sender As Object, e As EventArgs) Handles ResetButton.MouseEnter
        ResetButton.Width += 3
        ResetButton.Height += 3
    End Sub

    Private Sub Button4_Leave(sender As Object, e As EventArgs) Handles ResetButton.MouseLeave
        ResetButton.Width -= 3
        ResetButton.Height -= 3
    End Sub
    Private Sub FCFS_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Controls($"AT_P{0}").Text = 3
        'Controls($"AT_P{1}").Text = 0
        'Controls($"AT_P{2}").Text = 2
        'Controls($"AT_P{3}").Text = 0
        'Controls($"AT_P{4}").Text = 2

        'Controls($"AT_P{0}").Text = 0
        'Controls($"AT_P{1}").Text = 2
        'Controls($"AT_P{2}").Text = 3
        'Controls($"AT_P{3}").Text = 4
        'Controls($"AT_P{4}").Text = 5

        'Controls($"BT_P{0}").Text = 3
        'Controls($"BT_P{1}").Text = 1
        'Controls($"BT_P{2}").Text = 3
        'Controls($"BT_P{3}").Text = 1
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

    Private Sub GenerateButton_Click(sender As Object, e As EventArgs) Handles GenerateButton.Click
        ' Checks if all processes are filled
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
            If (Not myTool.isNumLessThan10(Controls($"AT_P{i}").Text)) Then
                MessageBox.Show($"Allowed input is only 1-10", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        Dim AT(numOfProcess - 1) As Integer
        Dim FT(numOfProcess - 1) As Integer
        Dim BT(numOfProcess - 1) As Integer
        Dim TAT(numOfProcess - 1) As Integer
        Dim WT(numOfProcess - 1) As Integer
        Dim SortedAT(numOfProcess - 1) As Integer
        Dim processOrder As New List(Of Integer)
        Dim isProcessAdded(numOfProcess - 1) As Boolean

        For i = 0 To numOfProcess - 1
            AT(i) = Controls($"AT_P{i}").Text
            BT(i) = Controls($"BT_P{i}").Text
            WT(i) = Controls($"WT_P{i}").Text
            SortedAT(i) = AT(i)
            isProcessAdded(i) = False
        Next

        ' Sort the AT array
        ' myTool.sort(SortedAT, numOfProcess)

        ' Prioritize processes with AT = 0
        For i = 0 To numOfProcess - 1
            If AT(i) = 0 Then
                processOrder.Add(i)
                isProcessAdded(i) = True
                'Debug.WriteLine(i)
            End If

        Next

        GenerateButton.Visible = False
        ' Add remaining processes in sorted order
        Dim NonZeroAT As New List(Of Integer)
        Dim NonZeroATCopy As New List(Of Integer)
        Dim NonZeroATIndex As New List(Of Integer)
        Dim isNZATAdded(numOfProcess - 1) As Integer

        For i = 0 To numOfProcess - 1
            isNZATAdded(i) = False
            If SortedAT(i) <> 0 Then
                For j = 0 To numOfProcess - 1
                    If SortedAT(i) = AT(j) AndAlso Not isProcessAdded(j) Then
                        NonZeroAT.Add(AT(i))
                        NonZeroATCopy.Add(AT(i))
                        NonZeroATIndex.Add(j)
                        isProcessAdded(j) = True
                    End If
                Next
            End If
        Next

        myTool.sortList(NonZeroAT, NonZeroAT.Count)

        For i = 0 To NonZeroAT.Count - 1
            For j = 0 To NonZeroATCopy.Count - 1
                If NonZeroAT(i) = NonZeroATCopy(j) AndAlso Not isNZATAdded(j) Then
                    processOrder.Add(NonZeroATIndex(j))
                    'Debug.WriteLine(NonZeroATIndex(j))
                    isNZATAdded(j) = True
                    Exit For
                End If
            Next
        Next


        For Each orders In processOrder
            Debug.WriteLine(orders)
        Next
        For Each items In NonZeroAT
            Debug.WriteLine($"Non Zero AT: {items}")
        Next

        For Each items In NonZeroATCopy
            Debug.WriteLine($"Non Zero AT Copy: {items}")
        Next

        For Each items In NonZeroATIndex
            Debug.WriteLine($"Non Zero AT Index: {items}")
        Next
        ' Debugging to see all values
        For i = 0 To numOfProcess - 1
            Debug.WriteLine($"ProcessOrder: {processOrder(i)} || AT: {AT(i)} || BT: {BT(i)} || Sorted AT: {SortedAT(i)}")
        Next

        Dim cur As Integer = 0
        Dim finishTime As Integer = 0
        Dim totalGanttChart As Integer = 0

        For i = 0 To numOfProcess - 1
            Dim processIndex = processOrder(i)
            finishTime += BT(processIndex)
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

        'For j = numOfProcess To 79
        '    Controls($"GC_{j}").Visible = False
        '    Controls($"GC_Label{j}").Visible = False
        'Next
        ' Calculation for TAT and WT
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
        AVG_TAT.Text = $"{(totalTAT / numOfProcess).ToString("0.00")} ms"
        AVG_WT.Text = $"{(totalWT / numOfProcess).ToString("0.00")} ms"
    End Sub



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
        For j = 0 To 80
            Controls($"GC_{j}").BackColor = Color.FromArgb(255, 255, 255)
            Controls($"GC_Label{j}").Visible = False
        Next
        AVG_TAT.Text = ""
        AVG_WT.Text = ""
        ProcessDialog.CPU_Scheduling = 1
        ProcessDialog.Show()
    End Sub
End Class