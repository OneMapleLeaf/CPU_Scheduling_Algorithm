<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcessDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Button1 = New Button()
        Dialogue_Textbox = New TextBox()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.Transparent
        Button1.BackgroundImage = My.Resources.Resources.B_800_x_450_px___40_x_40_px___2_
        Button1.BackgroundImageLayout = ImageLayout.Stretch
        Button1.Cursor = Cursors.Hand
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatAppearance.MouseDownBackColor = Color.Transparent
        Button1.FlatAppearance.MouseOverBackColor = Color.Transparent
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Location = New Point(174, 196)
        Button1.Name = "Button1"
        Button1.Size = New Size(49, 31)
        Button1.TabIndex = 0
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Dialogue_Textbox
        ' 
        Dialogue_Textbox.BorderStyle = BorderStyle.None
        Dialogue_Textbox.Location = New Point(171, 159)
        Dialogue_Textbox.Name = "Dialogue_Textbox"
        Dialogue_Textbox.Size = New Size(56, 16)
        Dialogue_Textbox.TabIndex = 1
        Dialogue_Textbox.Text = "0"
        Dialogue_Textbox.TextAlign = HorizontalAlignment.Center
        ' 
        ' ProcessDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackgroundImage = My.Resources.Resources.process__400_x_300_px_
        ClientSize = New Size(384, 261)
        Controls.Add(Dialogue_Textbox)
        Controls.Add(Button1)
        FormBorderStyle = FormBorderStyle.None
        Name = "ProcessDialog"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ProcessDialog"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Dialogue_Textbox As TextBox
End Class
