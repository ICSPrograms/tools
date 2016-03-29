Imports Microsoft.Win32
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Management
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Security.Cryptography
Imports System.Security.Principal
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Threading.Tasks
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock
Imports System.Xml



Public Class Form1
    'Hello there user!
    'You probably got the source code from my github (http://github.com/icsprograms)
    'Thank you for downloading! Create whatever updates you'd like, but DO NOT release it to the public without asking me first. This program is protected by the GNU General Public License.
    'E-mail me at icsprograms@hotmail.com if you have any errors, bugs, general questions or are asking for permissions.
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label2.Text = Application.ProductVersion
        TimeBro()
        TextBox1.Text = Date.Now & ": " & "Loading Tools.." & Environment.NewLine & Date.Now & ": " & "Tools successfully loaded!" & Environment.NewLine & Date.Now & ": " & "Please read the textboxes before using the tools."
        TextBox1.ForeColor = Color.Green()
    End Sub
    Private Sub EnableDHCP()
        'Sometimes doesn't work for Static IPs
        TextBox1.Text = Date.Now & ": " & "Enabling DHCP.." & Environment.NewLine
        TextBox1.ForeColor = Color.Green()
        Process.Start("netsh.exe", "interface ip set address \ source=dhcp")
        TextBox1.Text = Date.Now & ": " & "DHCP was successfully enabled!" & Environment.NewLine
        TextBox1.ForeColor = Color.Green()
    End Sub


    Public Shared Function IsAdministrator() As Boolean
        If Environment.OSVersion.Version.Major > 5 Then
            ' If newer than XP
            Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
            Dim principal As New WindowsPrincipal(identity)
            Return principal.IsInRole(WindowsBuiltInRole.Administrator)
        Else
            Return True
        End If
    End Function

    Private Sub ChkAdmn()
        'Checks if user has admin privileges. If the user does not have admin privileges a lot of the tools wont work.
        If IsAdministrator() = True Then
            TextBox1.Text = Date.Now & ": " & "Your current identity has admin privileges." & Environment.NewLine
            TextBox1.ForeColor = Color.Green()

        Else
            TextBox1.Text = Date.Now & ": " & "WARNING: Your user does not have admin privileges. Some functions may not work correctly." & Environment.NewLine
            TextBox1.ForeColor = Color.Red()
        End If
    End Sub

    Private Sub TimeBro()
        TextBox2.Text = Date.Now
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ChkAdmn()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FlushingDNS()
    End Sub

    Private Sub FlushingDNS()
        MsgBox("Are you sure you want to flush DNS Cache? Flushing your DNS Cache can temporarily disable your computers internet connection.", MsgBoxStyle.YesNo)
        If MsgBoxResult.No = True Then ' If the user clicks 'No' on the prompt
            Return 'Returns them to previous state (Cancels DNS Flush)
        End If


        If MsgBoxResult.Yes = True Then 'If the user clicks 'Yes' on the prompt
            TextBox1.Text = Date.Now & ": " & "Flushing DNS Cache.." & Environment.NewLine
            TextBox1.ForeColor = Color.Green()
            Process.Start("cmd", "/c ipconfig /flushdns") ' Prompts CMD and flushes DNS
            TextBox1.Text = Date.Now & ": " & "DNS Cache successfully flushed!" & Environment.NewLine
            TextBox1.ForeColor = Color.Green()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        MsgBox("Are you sure you want to reset TCP/IP?", MsgBoxStyle.YesNo)
        If MsgBoxResult.No = True Then ' If the user clicks 'No' on the prompt
            Return 'Returns them to previous state (Cancels TCP/IP reset)
        End If
        If MsgBoxResult.Yes = True Then
            TextBox1.Text = Date.Now & ": " & "Resetting TCP/IP.." & Environment.NewLine
            TextBox1.ForeColor = Color.Green()
        End If
        Process.Start("cmd", "/c netsh int reset all")
        TextBox1.Text = Date.Now & ": " & "TCP/IP successfully reset!." & Environment.NewLine
        TextBox1.ForeColor = Color.Green()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DisableProxy()
    End Sub

    Public Sub DisableProxy()
        TextBox1.Text = Date.Now & ": " & "Disabling proxies.." & Environment.NewLine
        TextBox1.ForeColor = Color.Green()

        Dim registry__1 As RegistryKey = Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Internet Settings", True)

        'Disable the proxy          
        registry__1.SetValue("ProxyServer", 1)

        'Remove tik

        registry__1.SetValue("ProxyEnable", 0)

        TextBox1.Text = Date.Now & ": " & "Proxies successfully disabled!" & Environment.NewLine
        TextBox1.ForeColor = Color.Green()
    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        CheckForUpdates.Show()
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox1.Text = Date.Now & ": " & "Resetting/repairing firewall.." & Environment.NewLine
        Process.Start("cmd", "/c netsh.exe advfirewall reset")
        TextBox1.Text = Date.Now & ": " & "Windows Firewall successfully reset/repaired!" & Environment.NewLine
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Process.Start("cmd", "/c netsh interface ip delete arpcache")
        Process.Start("cmd", "/c route.exe -f")
        MsgBox("A restart is required to finish the operation. Do you want to restart now? (You have 60 seconds to decide)", MsgBoxStyle.OkCancel)
        If MsgBoxResult.Yes = True Then
            Shell("Shutdown -r -t 60")
        Else
            Return
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox1.Text = Date.Now & ": " & "Clearing HOSTS file.." & Environment.NewLine
        Dim sHostsPath As String = Environment.GetEnvironmentVariable("windir") + "\System32\Drivers\Etc\hosts"
        TextBox1.Text = Date.Now & ": " & "HOSTS file successfully cleared! (A HOSTS file was placed if there wasn't already one)" & Environment.NewLine
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        EnableDHCP()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        TextBox1.Text = Date.Now & ": " & "Changing DNS to Googles.." & Environment.NewLine
        Process.Start("netsh.exe", "interface ip set dns static 8.8.4.4 primary")
        Process.Start("netsh.exe", "interface ip add dns 8.8.8.8 index=1")
        TextBox1.Text = Date.Now & ": " & "DNS successfully changed to Googles! (8.8.8.8, 8.8.4.4)" & Environment.NewLine
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        TextBox1.Text = Date.Now & ": " & "Refreshing current network adapter.." & Environment.NewLine
        Process.Start("netsh.exe", "interface set interface DISABLED")
        Process.Start("netsh.exe", "interface set interface ENABLED")
        TextBox1.Text = Date.Now & ": " & "Network adapter successfully refreshed!" & Environment.NewLine
    End Sub

    Private Sub ToolStripStatusLabel1_Click(sender As Object, e As EventArgs) Handles ToolStripStatusLabel1.Click
        Process.Start("http://icsprograms.x10.bz")
    End Sub
End Class
