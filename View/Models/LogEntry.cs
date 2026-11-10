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


namespace  MonitorLogger.Models  {

public  enum  LogEntryType
{
    PowerSuspend,
    PowerResume,
    PowerStatusChange,
    DisplayChanged
}


//========================================================================
//
//    LogEntry  class
//

public  class  LogEntry
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public  LogEntry()
{
}


//========================================================================
//
//    Member Variables.
//


}   //  End class  LogEntry

}   //  End of namespace  MonitorLogger.Models
