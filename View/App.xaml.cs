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

using   System.Windows;

using   MonitorLogger.Models;
using   MonitorLogger.Services;
using   MonitorLogger.ViewModels;
using   MonitorLogger.Views;


namespace  MonitorLogger  {

public  partial  class  App : Application
{

//----------------------------------------------------------------
/**   通常のアプリ終了時。
**
**/
protected  override  void
OnExit(ExitEventArgs e)
{
    this.m_vmMain?.CloseLogger();
    base.OnExit(e);
}

//----------------------------------------------------------------
/**   システムのシャットダウン・ログオフ時
**
**/
protected  override  void
OnSessionEnding(SessionEndingCancelEventArgs e)
{
    this.m_vmMain?.OnSessionEnding(e);
    base.OnSessionEnding(e);
}

//----------------------------------------------------------------
/**
**
**/
protected  override  void
OnStartup(StartupEventArgs e)
{
    base.OnStartup(e);

    WindowService       wndSrv  = new WindowService();
    LogStorageService   storage = new LogStorageService();

    //  メインウィンドウを表示する。    //
    this.m_vmMain   = new MainViewModel(storage);
    this.m_vmMain.StartLogger();
    wndSrv.showMainWindow(this.m_vmMain);
}


//========================================================================
//
//    Member Variables.
//

private             MainViewModel?  m_vmMain;


}   //  End class  App

}   //  End of namespace  MonitorLogger
