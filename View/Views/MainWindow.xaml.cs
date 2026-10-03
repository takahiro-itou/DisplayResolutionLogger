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
using   System.Windows;
using   System.Windows.Media.Imaging;

using   MonitorLogger;


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
public  MainWindow()
{
    InitializeComponent();

    this.Loaded += MainWindow_Loaded;

    this.m_taskModel = new Models.SampleModel();
    this.m_viewModel = new ViewModels.SampleViewModel(this.m_taskModel);

    this.DataContext = this.m_viewModel;
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
}

//========================================================================
//
//    Member Variables.
//

private   Models.SampleModel            m_taskModel;
private   ViewModels.SampleViewModel    m_viewModel;


}   //  End class  MainWindow

}   //  End of namespace  MonitorLogger.Views
