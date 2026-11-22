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

public  enum  ELogEventType
{
    ProgramStart,
    ProgramExit,
    PowerSuspend,
    PowerResume,
    PowerChange,
    DisplayChange,
    SessionEnding,
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
    this.EventTitle  = "";
    this.MonitorName = "";
}


//========================================================================
//
//    Public Properties.
//

//----------------------------------------------------------------
/**
**
**/
public  DateTime
EventDateTime { get; set; }

//----------------------------------------------------------------
/**
**
**/
public  ELogEventType
EventKind { get; set; }

//----------------------------------------------------------------
/**
**
**/
public  System.String
EventTitle { get; set; }

//----------------------------------------------------------------
/**
**
**/
public  System.String
MonitorName { get; set; }

//----------------------------------------------------------------
/**
**
**/
public  int
ScreenHeight { get; set; }

//----------------------------------------------------------------
/**
**
**/
public  int
ScreenWidth { get; set; }


//========================================================================
//
//    Member Variables.
//


}   //  End class  LogEntry

}   //  End of namespace  MonitorLogger.Models
