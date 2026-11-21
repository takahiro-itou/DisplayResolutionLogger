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

using   Microsoft.Win32;
using   System;
using   System.Windows;
using   System.Windows.Media.Imaging;

using   MonitorLogger.Models;
using   MonitorLogger.Services;
using   MonitorLogger.ViewModels;


namespace  MonitorLogger.Views  {

//========================================================================
//
//    MainWindow  class
//

public  partial class  MainWindow : Window
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   デフォルトコンストラクタ。
**
**/
public
MainWindow(
        MainViewModel   viewModel)
{
    InitializeComponent();

    this.m_viewModel     = viewModel;
    lvwEvent.ItemsSource = viewModel.LogItems;

    this.Loaded += MainWindow_Loaded;
}


//========================================================================
//
//    Protected Member Functions.
//

protected  override  void
OnClosed(EventArgs e)
{
    base.OnClosed(e);

    this.m_viewModel?.CloseLogger();
}


//========================================================================
//
//    Event Handlers.
//

private  void
MainWindow_Loaded(object sender, RoutedEventArgs e)
{
    string  customIcon  = System.IO.Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Resources", "MainWindow.ico");
    if ( System.IO.File.Exists(customIcon) ) {
        try {
            this.Icon = new BitmapImage(
                    new Uri(customIcon, UriKind.Absolute));
        } catch ( Exception ex ) {
            System.Diagnostics.Debug.WriteLine(
                $"Failed custom icon: {ex.Message}");
        }
    }

    this.m_viewModel?.StartLogger();
}


//========================================================================
//
//    For Internal Use Only.
//

private  void
WriteDetailLog(
        ELogEventType   eventKind,
        System.String   eventTitle,
        System.String   localTitle)
{
    this.m_viewModel?.WriteDetailLog(eventKind, eventTitle, localTitle);
}

//========================================================================
//
//    Member Variables.
//

private   readonly  MainViewModel           m_viewModel;


}   //  End class  MainWindow

}   //  End of namespace  MonitorLogger.Views
