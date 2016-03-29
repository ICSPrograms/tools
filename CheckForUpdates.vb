Imports System.Net
Public Class CheckForUpdates

    Private Sub CheckForUpdates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Minimum = 1
        ProgressBar1.Maximum = 100
        ProgressBar1.Step = 1
        ProgressBar1.Value = 1


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label1.Text = "Please wait.. scanning for updates."
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(+1)
        If ProgressBar1.Value = 50 Then
            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("http://icsprograms.x10.bz/Version.txt")
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            Dim newestversion As String = sr.ReadToEnd()
            Dim currentversion As String = Application.ProductVersion
            If newestversion.Contains(currentversion) Then
                Label1.Text = "You are up to date."
                Label1.ForeColor = Color.Green

            Else
                PictureBox1.Visible = True
                LinkLabel1.Visible = True
                LinkLabel2.Visible = True
                End If
            End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim wc As New WebClient()
        wc.DownloadFile("http://icsprograms.x10.bz/Tools.exe", "Tools by ICS Programs")
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("http://icsprograms.x10.bz")
    End Sub
End Class