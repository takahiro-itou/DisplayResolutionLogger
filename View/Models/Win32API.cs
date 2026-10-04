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
private  struct  LUID
{
    public  uint    LowPart;
    public  int     HightPart;
};

}   //  End class  Win32API

}   //  End of namespace  MonitorLogger.Models
