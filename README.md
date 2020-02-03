# MR Switcher

Enable / Disable tool for MixedReality devices.

## Quick Launch
`mr-enable.exe` and `mr-disable.exe`


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

