Imports System.Drawing.Imaging

Public Class Tool

    Public Function sort(num As Integer(), numOfProcess As Integer)
        For i = 0 To numOfProcess - 1
            For j = 0 To numOfProcess - 1
                If num(i) < num(j) Then
                    Dim temp As Integer = num(i)
                    num(i) = num(j)
                    num(j) = temp
                End If
            Next
        Next

        Return num
    End Function

    Public Function sortList(num As List(Of Integer), numOfProcess As Integer)
        For i = 0 To numOfProcess - 1
            For j = 0 To numOfProcess - 1
                If num(i) < num(j) Then
                    Dim temp As Integer = num(i)
                    num(i) = num(j)
                    num(j) = temp
                End If
            Next
        Next
        Return num
    End Function
    Public Function CheckIfProcessAreFilled(NoP As Integer, myForm As Form)
        Debug.WriteLine(myForm.Tag)
        For i = 0 To NoP - 1
            If myForm.Controls($"AT_P{i}").Text = "" Then
                fillProcessDialogue(myForm, "Arrival Time")
                Return False
            End If
            If myForm.Controls($"BT_P{i}").Text = "" Then
                fillProcessDialogue(myForm, "Burst Time")
                Return False
            End If
        Next
        If myForm.Tag = "prioritySched" Then
            For i = 0 To NoP - 1
                If myForm.Controls($"PT_P{i}").Text = "" Then
                    fillProcessDialogue(myForm, "Priority")
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Sub fillProcessDialogue(myForm As Form, errType As String)
        MessageBox.Show($"Please fill all the {errType} for all process", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        myForm.Controls($"AT_P{1}").Focus()
    End Sub

    Public Function isNumLessThan10(input As Integer)
        If input > 10 Then
            Return False
        End If
        Return True
    End Function

    Public Function getColor(processSequence As Integer)

        Select Case processSequence
            Case 0
                Return Color.FromArgb(0, 74, 173)
            Case 1
                Return Color.FromArgb(255, 49, 49)
            Case 2
                Return Color.FromArgb(227, 239, 3)
            Case 3
                Return Color.FromArgb(8, 237, 72)
            Case 4
                Return Color.FromArgb(255, 125, 5)
            Case Else
                Return Color.FromArgb(255, 255, 255)
        End Select
    End Function

    Public Function checkIfProcessAreNumbers(input As String)
        If Not IsNumeric(input) Then
            Return False
        End If
        Return True
    End Function

    Public Function generateLabel(myForm As Form, counter As Integer) As Label
        If counter > 0 Then
            counter -= 1
        End If
        Dim num_lbl As New Label With {
        .Width = 19,
        .Height = 15,
        .ForeColor = Color.Black,
        .Location = New Point(myForm.Controls($"GC_{counter}").Location.X - 5, .Location.Y + 470),
        .BackColor = Color.Transparent,
        .Font = New Font("Sylfaen", 9, FontStyle.Regular)
    }
        If counter > 0 Then
            num_lbl.Text = $"{counter + 1}"
        Else
            num_lbl.Text = $"{counter}"
        End If

        Return num_lbl
    End Function

    Public Function CheckForZeroes(num As Integer)
        If num = 0 Then
            Return True
        End If
        Return False
    End Function



End Class
