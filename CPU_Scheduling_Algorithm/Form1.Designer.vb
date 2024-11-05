<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Timer1 = New Timer(components)
        MaterialProgressBar1 = New MaterialSkin.Controls.MaterialProgressBar()
        SuspendLayout()
        ' 
        ' Timer1
        ' 
        Timer1.Enabled = True
        Timer1.Interval = 30
        ' 
        ' MaterialProgressBar1
        ' 
        MaterialProgressBar1.BackColor = Color.Wheat
        MaterialProgressBar1.Depth = 0
        MaterialProgressBar1.ForeColor = Color.YellowGreen
        MaterialProgressBar1.Location = New Point(215, 289)
        MaterialProgressBar1.MouseState = MaterialSkin.MouseState.HOVER
        MaterialProgressBar1.Name = "MaterialProgressBar1"
        MaterialProgressBar1.Size = New Size(367, 5)
        MaterialProgressBar1.TabIndex = 2
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ButtonHighlight
        BackgroundImage = My.Resources.Resources.menu11
        BackgroundImageLayout = ImageLayout.Stretch
        ClientSize = New Size(800, 450)
        Controls.Add(MaterialProgressBar1)
        DoubleBuffered = True
        FormBorderStyle = FormBorderStyle.None
        Name = "Form1"
        Padding = New Padding(3, 0, 3, 3)
        StartPosition = FormStartPosition.CenterScreen
        Text = "Form1"
        ResumeLayout(False)
    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents MaterialProgressBar1 As MaterialSkin.Controls.MaterialProgressBar

End Class
