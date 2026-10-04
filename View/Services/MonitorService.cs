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

using   System;
using   System.Collections.Generic;
using   System.Runtime.InteropServices;


namespace  MonitorLogger.Services  {

//========================================================================
//
//    MonitorService  class
//

public  class  MonitorService
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   デフォルトコンストラクタ。
**
**/
public  MonitorService()
{
    this.m_friendlyNames = new Dictionary<string, string>(
            StringComparer.OrdinalIgnoreCase);
}


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   アクティブなモニター（製品名）を取得する。
**
**/
public  static  Dictionary<string, string>
GetMonitorFriendlyNames()
{
    Dictionary<string, string>  friendlyNames = this.m_friendlyNames;
    friendlyNames.Clear();

    if ( Win32API.GetDisplayConfigBufferSizes(
            Win32API.QDC_ONLY_ACTIVE_PATHS,
            out uint pathCount,
            out uint modeCount) != Win32API.ERROR_SUCCESS )
    {
        return ( friendlyNames );
    }

    var paths = new Win32API.DISPLAYCONFIG_PATH_INFO[pathCount];
    var modes = new Win32API.DISPLAYCONFIG_MODE_INFO[modeCount];

    if ( Win32API.QueryDisplayConfig(
            Win32API.QDC_ONLY_ACTIVE_PATHS,
            ref pathCount, paths,
            ref modeCount, modes,
            IntPtr.Zero) != Win32API.ERROR_SUCCESS )
    {
        return ( friendlyNames );
    }

    for ( int i = 0; i < pathCount; ++ i ) {
        var deviceName = new Win32API.DISPLAYCONFIG_TARGET_DEVICE_NAME();
        deviceName.header.type = Win32API.DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME;
         deviceName.header.size = (uint)Marshal.SizeOf(typeof(Win32API.DISPLAYCONFIG_TARGET_DEVICE_NAME));
        deviceName.header.adapterId = paths[i].targetInfo.adapterId;
        deviceName.header.id = paths[i].targetInfo.id;

        if ( Win32API.DisplayConfigGetDeviceInfo(
                ref deviceName) != Win32API.ERROR_SUCCESS )
        {
            continue;
        }
        if ( !string.IsNullOrEmpty(deviceName.monitorDevicePath) ) {
            friendlyNames[deviceName.monitorDevicePath] = deviceName.monitorFriendlyDeviceName;
        }
    }

    return ( friendlyNames );
}


//========================================================================
//
//    Member Variables.
//

private   Dictionary<string, string>    m_friendlyNames;


}   //  End class  MonitorService

}   //  End of namespace  MonitorLogger.Services
