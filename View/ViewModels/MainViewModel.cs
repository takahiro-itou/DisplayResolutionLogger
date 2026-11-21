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
using   System.Collections.ObjectModel;

using   MonitorLogger.Services;


namespace  MonitorLogger.ViewModels  {

//========================================================================
//
//    MainViewModel  class
//

public  class  MainViewModel
{

using   LogEntriesList  = ObservableCollection<LogEntry>;


//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**
**
**/
public
MainViewModel(
        LogStorageService   srvLogStorage)
{
    this.m_srvMonitor = new Services.MonitorService();
    this.m_srvStorage = new srvLogStorage;

    this.m_logItems = this.m_srvStorage.LoadLogs();
    this.m_logItems.CollectionChanged += (s, e) =>
    {
        this.m_srvStorage.SaveLogs(this.m_logItems);
    };
}


//========================================================================

//----------------------------------------------------------------
/**
**
**/
public  LogEntriesList  LogItems
{
    get { return  this.m_logItems; }
}


//========================================================================
//
//    Member Variables.
//

private   readonly  LogStorageService   m_srvStorage;
private             LogEntriesList      m_logItems;

}   //  End class  MainViewModel

}   //  End of namespace  MonitorLogger.ViewModels
