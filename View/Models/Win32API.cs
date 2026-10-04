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
using   System.Runtime.InteropServices;


namespace  MonitorLogger.Models  {

//========================================================================
//
//    Win32API  class
//

public  class  Win32API
{

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
    public  uint    infoType;
    public  uint    id;
    public  LUID    adapterId;
    public  uint    dummy1;
    public  uint    dummy2;
    public  uint    dummy3;
    public  uint    dummy4;
    public  uint    dummy5;
    public  uint    dummy6;
    public  uint    dummy7;
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


}   //  End class  Win32API

}   //  End of namespace  MonitorLogger.Models
