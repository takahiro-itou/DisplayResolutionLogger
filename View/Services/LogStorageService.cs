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
using   System.Collections.ObjectModel;
using   System.IO;
using   System.Runtime.Serialization.Json;

using   MonitorLogger.Models;


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
    this.m_filePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, fileName);
    this.m_settings = new DataContractJsonSerializerSettings
    {
        DateTimeFormat = new System.Runtime.Serialization.DateTimeFormat("o")
    };
}


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**
**
**/
public  ObservableCollection<LogEntry>
LoadLogs()
{
    if ( ! File.Exists(this.m_filePath) ) {
        return  new ObservableCollection<LogEntry>();
    }

    try {
        var serializer = new DataContractJsonSerializer(
                typeof(List<LogEntry>), this.m_settings);
        using ( var stream = new FileStream(
                this.m_filePath, FileMode.Open,
                FileAccess.Read, FileShare.Read)
        ) {
            var list = serializer.ReadObject(stream) as List<LogEntry>;
            return  list != null
                ? new ObservableCollection<LogEntry>(list)
                : new ObservableCollection<LogEntry>();
        }
    } catch {
        return  new ObservableCollection<LogEntry>();
    }
}

//----------------------------------------------------------------
/**
**
**/
public  void
SaveLogs(
        IEnumerable<LogEntry>   logs)
{
    var list = new List<LogEntry>(logs);
    var serializer = new DataContractJsonSerializer(
            typeof(List<LogEntry>), this.m_settings);
    using (var stream = new FileStream(
            this.m_filePath, FileMode.Create,
            FileAccess.Write, FileShare.None)
    ) {
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
