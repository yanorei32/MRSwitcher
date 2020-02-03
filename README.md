# MR Switcher

Enable / Disable tool for MixedReality devices.

## Quick Launch
`mr-enable.exe` and `mr-disable.exe`


## CUI Tool (Admin required)
```bat
mr-ctl [Enable|Disable|Toggle]
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

## Install
Copy `MR.exe` to `C:\Windows\System32`

