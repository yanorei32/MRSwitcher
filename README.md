# MR Switcher

Enable / Disable tool for MixedReality devices.

![image](https://user-images.githubusercontent.com/11992915/74040279-4d21e380-49bb-11ea-8c85-3872bb749a28.png)

## Download latest release build
[From GitHub](https://github.com/Yanorei32/MRSwitcher/releases/latest)

## Quick Launch
Run `mr-enable.exe` / `mr-disable.exe`

## GUI Tool
Run `mr-ctl-gui.exe`

![image](https://user-images.githubusercontent.com/11992915/74040108-fa482c00-49ba-11ea-9010-3b8824595412.png)

\* MEDIA device class is displayed in this image, but in fact Holographic device class.

## CUI Tool (Admin required)
```bat
mr-ctl [enable|disable] <device id>
mr-ctl ls
```

## Build

 - Windows 10
 - Cygwin / GNU Make

```bash
git clone https://github.com/yanorei32/MRSwitcher
cd MRSwitcher
make
make genzip # if you need zip file.
```

## Install CUI Tool
Copy `mr-ctl.exe` to `C:\Windows\System32`

