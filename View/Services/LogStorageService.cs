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
using   System.Runtime.Serialization.Json;


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
    this.m_settings = new DataContractJsonSerializerSettings
    {
        DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("o")
    };
}


//========================================================================
//
//    Public Member Functions.
//

public  void
SaveLogs(
        IEnumerable<LogEntry>   logs)
{
    var list = new List<LogEntry>(logs);
    var serializer = new DataContractJsonSerializer(
            typeof(List<LogEntry>), this.m_settings);
    using (var stream = new FileStream(
            this._filePath, FileMode.Create,
            FileAccess.Write, FileShare.None))
    {
        serializer.WriteObject(stream, list);
    }
}


//========================================================================
//
//    Member Variables.
//

private   readonly  System.String           m_filePath;
private   readonly  DataContractJsonSerializerSettings  m_settings;


}   //  End class  LogStorageService

}   //  End of namespace  MonitorLogger.Services
