Living‑off‑the‑land Remote Access Trojan (RAT) BluePony program overview

BluePony is a Windows desktop implant that installs itself quietly, persists across logons, and orchestrates a modular surveillance and control framework using only native Windows features and .NET libraries. It captures keystrokes, mouse clicks, screenshots, microphone audio, process usage, file system contents, recent user activity, local network details, WAN IP and geolocation, and compiles periodic HTML reports. It can fetch and run remote commands via cmd.exe and PowerShell, download and stage additional payloads, propagate files to removable drives and network shares, and ultimately generate Microsoft Remote Assistance invitations to enable interactive remote access.

Installation and persistence

- Stealth start: On load, the form hides itself (`Visible = False`, `Hide()`), running headless.
- Self-relocation: The executable copies itself to a disguised path under the current user profile:
  - AppData\Roaming\Microsoft\Windows\Host\System.exe
  - The directory name mimics Microsoft/Windows components to blend with normal system folders.
- Original cleanup: A temporary batch file (`delete_me.bat`) is written and launched to:
  - Loop killing the original process (`taskkill /f /im <original>`).
  - Delete the original executable, retrying until removed.
  - Self-delete the batch script.
- Registry-based persistence: Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run` “System” to the relocated `System.exe`, ensuring execution at user logon.
- Handoff: Starts the relocated binary and exits the original process.

This sequence ensures survival across reboots, conceals the implant in a plausible folder, and eliminates initial on-disk clues.

Modular orchestration via timers

The program uses many timers to schedule surveillance and control tasks at different cadences, creating a consistent “heartbeat” of activity:

- High-frequency input capture:
  - KeysT: 100 ms, counts keystrokes and mouse clicks.
  - LogT: 1 ms, translates specific keypresses into readable characters.
- Visual monitoring:
  - ScreenT: 30 s, captures full-screen JPEG screenshots to “BPD” with timestamped filenames.
- System activity profiling:
  - AppsT: 3 min, lists visible-window processes and their runtimes.
  - DeviceT: 3 min, computes device uptime from `Environment.TickCount`.
- Network profiling:
  - LANT: 3 min, enumerates NICs, IPv4 addresses, DNS and DHCP servers.
  - WANT: 30 min, queries ip-api.com for WAN IP and geolocation (lat/lon, city, region, country).
- File system reconnaissance:
  - DirT: 3 min, inventories files and folders in Desktop, Downloads, Documents, Pictures, Music, Videos, OneDrive; enumerates installed applications from HKLM Uninstall registry keys.
  - RecentT: 3 min, lists the 10 most recent `.lnk` files (Recent folder).
- Audio surveillance:
  - MicT: 3 min, continuously records waveaudio via `mciSendString`; rotates to new timestamped WAV files in “BPD”.
- Data collation and staging:
  - ReportT: 3 min, composes HTML reports containing user/device info, input logs, app activity, drive stats, geolocation, OS details, LAN/WAN IPs, recent activity, and directory/app inventory. Writes to “BPD” and clears the keystroke buffer.
- Payload delivery and propagation:
  - LoadT: 5 min, fetches a list of URLs; downloads ZIPs or files into “BPL”, extracts ZIPs, and launches `System.exe` if present.
  - CopyT: 30 s, copies files from “BPD” to a `BPDO` folder on removable drives (USB) or a configured network path, then deletes the originals in “BPD”.
- Remote access enablement:
  - RemoteT: 15 min, one-shot trigger to create Remote Assistance invitation.

This orchestration provides a continuous flow of capture, enrichment, packaging, and optional delivery/propagation.

Keylogging and input translation

BluePony implements two complementary keystroke capture streams:

- Counters (KeysT):
  - Iterates virtual key codes (8–255); increments `_totalKeys` when pressed.
  - Tracks left/right mouse buttons (`1`, `2`) to increment `_totalClicks`.
  - Updates UI labels with totals.

- Readable text reconstruction (LogT):
  - Polls specific key codes and maps them to human-readable tokens:
    - Control keys: `[Enter]`, `[Backspace]`, `[Space]`, `[Tab]`, `[Ctrl]`, `[Alt]`, `[Caps Lock]`, `[Esc]`, arrows, `[Delete]`.
    - Punctuation and symbols: `; :`, `= +`, `, <`, `- _`, `. >`, `/ ?`, `` ` ~ ``, `\ |`, `' "`, `[ {`, `] }`.
    - Numbers with Shift-mapping to symbols: `1 !`, `2 @`, `3 #`, `4 $`, `5 %`, `6 ^`, `7 &`, `8 *`, `9 (`, `0 )`.
    - Numpad operators and textual tags: `[Multiply]`, `[Add]`, `[Subtract]`, `/`.
  - Alphabetic characters (A–Z):
    - Uses `Shift()` and `CapsLock()` to decide case:
      - Shift off + Caps on → uppercase.
      - Shift off + Caps off → lowercase.
      - Shift on + Caps off → uppercase.
      - Shift on + Caps on → lowercase.
  - Appends each detected character/token to `KeysAndClicksLog`, inserting line breaks every 69 characters for readability.

Together, these routines produce both quantitative activity metrics and high-fidelity textual reconstructions of user input, enabling reconstruction of credentials, commands, and prose.

Screen capture and process monitoring

- Screenshot capture (ScreenT):
  - Grabs primary screen dimensions, copies full screen to a bitmap with `Graphics.CopyFromScreen`.
  - Saves JPEGs to “BPD” named as `<username>_Image_<timestamp>.jpg`.
  - Updates a status label (“Screen Capture Ready”).

- Application usage (AppsT):
  - Enumerates all processes.
  - Filters by `MainWindowTitle` to focus on user-facing apps.
  - Computes duration since `StartTime`, formats as `hh:mm:ss`.
  - Appends process names and runtimes to `ApplicationList`.

These modules give visual context to the keystrokes and track what applications were actively used over time.

File system and environment inventory

- Directory inventory (DirT):
  - Scans common user directories (Desktop, Downloads, Documents, Pictures, Music, Videos, OneDrive).
  - Lists full paths of files and subfolders.
  - Queries `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall` to list installed applications’ `DisplayName`.
  - Outputs a consolidated listing into `DirList`.

- Recent activity (RecentT):
  - Reads the shell “Recent” folder.
  - Orders `.lnk` files by creation time and takes the latest 10.
  - Logs base filenames (without `.lnk`).

- Drive enumeration (DrivesT):
  - Iterates drives and, if ready, records:
    - Drive letter/name.
    - Free space (MB).
    - Used space (MB).
    - Total space (MB).
  - Updates “Drives Ready” status.

These inventories help build a dossier of user content, software, and storage context.

Network profiling and geolocation

- LAN details (LANT):
  - Enumerates all NICs and collects:
    - IPv4 addresses from unicast address sets.
    - IPv4 DNS servers.
    - IPv4 DHCP servers.
  - Joins and displays them as a single string.

- WAN IP and location (WANT):
  - Calls `http://ip-api.com/json`.
  - Parses JSON: `lat`, `lon`, `query` (WAN IPv4), `city`, `regionName`, `country`.
  - Updates labels for geolocation and WAN IP.

This dual profiling provides both internal network context and external internet footprint and location.

Reporting and data staging

- HTML report generation (ReportT):
  - Uses `StringBuilder` to compose a simple HTML document containing:
    - Username, device name, domain.
    - Key and click counts.
    - Keystroke log contents.
    - Device uptime.
    - Applications running.
    - Drive usage block.
    - Geolocation (name and coordinates).
    - OS name, platform, version.
    - LAN addresses; WAN address label.
    - Recent activity entries.
    - Directory and installed applications inventory.
  - Ensures “BPD” exists.
  - Writes the report as `<UID>_Log_<timestamp>.html` in “BPD”.
  - Clears the keystroke buffer after write.

- Propagation (CopyT):
  - Scans removable drives for a `BPDO` directory:
    - Copies files from “BPD” into `BPDO` if not already present, then deletes originals in “BPD`.
  - Fallback to a `NetworkPath` share if `BPDO` not found:
    - Same copy-then-delete pattern.

This packaging and movement of artifacts suggests a workflow where collected data and captures are moved off the local staging folder to removable or network locations for pickup or later exfiltration.

Payload delivery and execution

- Loader (LoadT):
  - Downloads a text manifest from a remote URL (placeholder).
  - Splits by newline into file URLs.
  - For each URL:
    - If `.zip`, downloads to `%TEMP%`, extracts to “BPL”, deletes ZIP.
    - Else, downloads directly to “BPL”.
  - If `System.exe` exists in “BPL”, launches it.

This enables the operator to push updates, modules, or secondary implants, using standard web and compression libraries.

Audio surveillance

- Microphone capture (MicT):
  - Every 3 minutes:
    - Saves current recording to `<username>Audio<timestamp>.wav` in “BPD” (or `_Audio_` variant in other blocks).
    - Closes the current `recsound` session.
    - Creates a new alias and resumes recording.
  - Uses `winmm.dll`’s `mciSendString` with `waveaudio` to record without external tooling.

This produces a series of periodic audio files, maintaining continuous microphone surveillance with rolling segmentation.

Remote command execution

- Command-line (CLT):
  - Fetches a plaintext command from a remote URL (first line).
  - Tracks the last executed command; only runs new commands.
  - Executes via hidden `cmd.exe /c <command>`.

- PowerShell (PST):
  - Same pattern but executes via hidden `powershell.exe -Command <script>`.

These functions implement remote control using Windows’ built-in interpreters, allowing arbitrary actions with operator-provided commands or scripts.

Remote access enablement

- Invitation generation (CreateInvitationFile):
  - Assembles path for `Invitation.msrcincident` in `srcDir`.
  - Deletes any pre-existing invitation.
  - Generates a 10-character password with `RNGCryptoServiceProvider` using uppercase, lowercase, digits, and symbols.
  - Writes the password to `External.txt` alongside.
  - Launches `Msra.exe /saveasfile <file> <password>` as a hidden process to create a Remote Assistance invitation file.
  - Loops until the file appears, then signals readiness.

- Trigger (RemoteT):
  - One-time activation: stops timer, removes handler, calls `CreateInvitationFile`.

This creates a legitimate Microsoft Remote Assistance invitation using only native tools, furnishing a ready path for interactive remote sessions guarded by a generated password.

Data storage and movement

- Local staging directories:
  - “BPD” (captures and reports): screenshots, audio recordings, HTML reports.
  - “BPL” (loader staging): downloaded payloads and extracted ZIP contents.

- File naming conventions:
  - Include `Environment.UserName` and timestamps (`yyyy-MM-dd-HH-mm`) to organize artifacts per user and period.

- Movement to external locations:
  - CopyT attempts moving staged files to `BPDO` on USB or to a designated network path, then deletes local copies.

This structure supports modular collection, organized storage, and periodic offloading of artifacts to removable or shared locations.

Living-off-the-land techniques summarized

- Native APIs and binaries:
  - user32.dll (`GetAsyncKeyState`, `GetKeyState`), winmm.dll (`mciSendString`), Msra.exe, cmd.exe, powershell.exe.
- .NET libraries:
  - System.IO, System.Net `WebClient`, System.IO.Compression `ZipFile`, Microsoft.Win32 registry access, System.Diagnostics `Process`.
- Windows features and folders:
  - AppData\Roaming paths, Recent folder, HKCU Run key, HKLM Uninstall keys, `%TEMP%`.
- Public services:
  - ip-api.com for WAN IP and geolocation.

By relying on trusted components already present on Windows systems, BluePony minimizes the need for custom tooling and blends its behavior with legitimate system activity while delivering a comprehensive surveillance and control capability.

© Copyright 2025 Elliot Monteverde. All Rights Reserved. For educational use only.
