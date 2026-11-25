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
/**   インスタンスを初期化する
**  （デフォルトコンストラクタ）。
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
/**   イベントが発生した日時。
**
**/
public  DateTime
EventDateTime { get; set; }

//----------------------------------------------------------------
/**   イベントの種類。
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
/**   モニター製品名。
**
**/
public  System.String
MonitorName { get; set; }

//----------------------------------------------------------------
/**   イベント発生時点の解像度（高さ）。
**
**/
public  int
ScreenHeight { get; set; }

//----------------------------------------------------------------
/**   イベント発生時点の解像度（幅）。
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
