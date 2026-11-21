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

using   BaseballScoreHelper.ViewModels;


namespace  MonitorLogger.Services  {

//========================================================================
//
//    IWindowService  interface.
//

public  interface  IWindowService
{


//----------------------------------------------------------------
/**   メインウィンドウを表示する。
**
**/
public  System.Boolean
showMainWindow(
        MainViewModel   viewModel);


}   //  End interface  IWindowService

}   //  End of namespace  MonitorLogger.Services
