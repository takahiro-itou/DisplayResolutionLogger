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
    var vmMain  = new MainViewModel(storage);
    wndSrv.showMainWindow(vmMain);
}


}   //  End class  App

}   //  End of namespace  MonitorLogger
