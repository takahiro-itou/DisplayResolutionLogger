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
    SystemEvents.PowerModeChanged       -= OnPowerModeChanged;
    SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;

    base.OnClosed(e);
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

    WriteDetailLog(
        ELogEventType.ProgramStarted, "ProgramStart",
        "【プログラム起動】現在のディスプレイ構成");

    SystemEvents.PowerModeChanged       += OnPowerModeChanged;
    SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
}


private  async  void
OnDisplaySettingsChanged(object? sender, EventArgs e)
{
    await System.Threading.Tasks.Task.Delay(200);
    WriteDetailLog(
        ELogEventType.DisplayChanged, "DisplayChange",
        "【イベント検知】画面構成または解像度の変更が発生しました"
    );
}


private  void
OnPowerModeChanged(object? sender, PowerModeChangedEventArgs e)
{
      switch ( e.Mode ) {
      case  PowerModes.Suspend:
          WriteDetailLog(
              ELogEventType.PowerSuspend, "Suspend",
              "システムがスリープ/休止状態に移行します。");
          break;
      case  PowerModes.Resume:
          WriteDetailLog(
              ELogEventType.PowerResume, "Resume",
              "システムがスリープ/休止状態から復帰しました。");
          break;
      case  PowerModes.StatusChange:
          WriteDetailLog(
              ELogEventType.PowerStatusChange, "PowerStatus",
              "電源ステータスが変更されました。");
          break;
      }
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

private   static  readonly  string  LogFilePath =
    System.IO.Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Resolution.log");

private   readonly  MainViewModel           m_viewModel;
private   readonly  LogStorageService       m_srvStorage;
private   readonly  MonitorService          m_srvMonitor;


}   //  End class  MainWindow

}   //  End of namespace  MonitorLogger.Views
