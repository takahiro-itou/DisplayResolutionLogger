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

using   MonitorLogger.Services;


namespace  MonitorLogger.ViewModels  {

//========================================================================
//
//    MainViewModel  class
//

public  class  MainViewModel
{

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
    m_srvLogStorage = srvLogStorage;
}


//========================================================================
//
//    Member Variables.
//

private   readonly  LogStorageService   m_srvLogStorage;


}   //  End class  MainViewModel

}   //  End of namespace  MonitorLogger.ViewModels
