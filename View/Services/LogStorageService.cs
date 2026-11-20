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


namespace  MonitorLogger.Services  {

//========================================================================
//
//    LogStorageService  class
//

public  class  LogStorageService
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public
LogStorageService(
        System.String   fileName = "Logs.json")
{
    this.m_filePath = fileName;
    this.m_jsOption = new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };
}


//========================================================================
//
//    Public Member Functions.
//


//========================================================================
//
//    Member Variables.
//

private   readonly  System.String           m_filePath;
private   readonly  JsonSerializerOptions   m_jsOption;

}   //  End class  LogStorageService

}   //  End of namespace  MonitorLogger.Services
