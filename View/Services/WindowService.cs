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

using   MonitorLogger.ViewModels;


namespace  MonitorLogger.Services  {

//========================================================================
//
//    WindowService  class
//

public  class  WindowService : IWindowService
{

//========================================================================
//
//    Constructor(s) and Destructor.
//


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   メインウィンドウを表示する。
**
**/
public  System.Boolean
showMainWindow(
        MainViewModel   viewModel)
{
    MainWindow  frmMain = new MainWindow();

    frmMain.DataContext = viewModel;
    frmMain.Show();

    return ( true );
}


}   //  End class  WindowService

}   //  End of namespace  MonitorLogger.Services
