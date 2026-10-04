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


}   //  End class  Win32API

}   //  End of namespace  MonitorLogger.Models
