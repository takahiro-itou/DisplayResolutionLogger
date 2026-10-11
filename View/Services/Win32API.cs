//  -*-  coding: utf-8-with-signature-unix     -*-  //
/*************************************************************************
**                                                                      **
**                  --  Display Resolution Logger.  --                  **
**                                                                      **
**          Copyright (C), 2026-2026, Takahiro Itou                     **
**          All Rights Reserved.                                        **
**                                                                      **
**          License: (See COPYING or LICENSE files)                     **
**          GNU Affero General Public License (AGPL) version 3,         **
**          or (at your option) any later version.                      **
**                                                                      **
*************************************************************************/

using   Microsoft.Win32;
using   System;
using   System.Runtime.InteropServices;


namespace  MonitorLogger.Services  {

//========================================================================
//
//    Win32API  class
//

public  class  Win32API
{

public  const   int     ERROR_SUCCESS           = 0;
public  const   uint    QDC_ONLY_ACTIVE_PATHS   = 2;

[StructLayout(LayoutKind.Sequential)]
public  struct  LUID
{
    public  uint    LowPart;
    public  int     HightPart;
}

[StructLayout(LayoutKind.Sequential)]
public  struct  DISPLAYCONFIG_PATH_SOURCE_INFO
{
    public  LUID    adapterId;
    public  uint    id;
    public  uint    modeInfoIdx;
    public  uint    statusFlags;
}

[StructLayout(LayoutKind.Sequential)]
public  struct  DISPLAYCONFIG_PATH_TARGET_INFO
{
    public  LUID    adapterId;
    public  uint    id;
    public  uint    modeInfoIdx;
    public  uint    outputTechnology;
    public  uint    rotation;
    public  uint    scaling;
    public  uint    refreshRate;
    public  uint    scanLineOrdering;

    [MarshalAs(UnmanagedType.Bool)]
    public  bool    targetAvailable;

    public  uint    statusFlags;
}

[StructLayout(LayoutKind.Sequential)]
public  struct  DISPLAYCONFIG_PATH_INFO
{
    public  DISPLAYCONFIG_PATH_SOURCE_INFO  sourceInfo;
    public  DISPLAYCONFIG_PATH_TARGET_INFO  targetInfo;
    public  uint                            flags;
}


[StructLayout(LayoutKind.Sequential)]
public  struct  DISPLAYCONFIG_MODE_INFO
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
    public  byte[]  dummy;
}

public  enum  DISPLAYCONFIG_DEVICE_INFO_TYPE : uint
{
    DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2
}

[StructLayout(LayoutKind.Sequential)]
public  struct  DISPLAYCONFIG_DEVICE_INFO_HEADER
{
    public  DISPLAYCONFIG_DEVICE_INFO_TYPE  type;
    public  uint    size;
    public  LUID    adapterId;
    public  uint    id;
}


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public  struct  DISPLAYCONFIG_TARGET_DEVICE_NAME
{
    public  DISPLAYCONFIG_DEVICE_INFO_HEADER    header;
    public  uint    flags;
    public  uint    outputTechnology;
    public  ushort  edidManufactureId;
    public  ushort  edidProductCodeId;
    public  uint    connectorInstance;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
    public  string  monitorFriendlyDeviceName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public  string  monitorDevicePath;
}


[DllImport("user32.dll")]
public  static  extern  int
GetDisplayConfigBufferSizes(
    uint        flags,
    out uint    numPathArrayElements,
    out uint    numModeInfoArrayElements);

[DllImport("user32.dll")]
public  static  extern  int
QueryDisplayConfig(
    uint        flags,
    ref uint    numPathArrayElements,
    [Out] DISPLAYCONFIG_PATH_INFO[] pathArray,
    ref uint    numModeInfoArrayElements,
    [Out] DISPLAYCONFIG_MODE_INFO[] modeInfoArray,
    IntPtr      currentTopologyId);

[DllImport("user32.dll")]
public  static  extern  int
DisplayConfigGetDeviceInfo(
    ref DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName);


}   //  End class  Win32API

}   //  End of namespace  MonitorLogger.Services
