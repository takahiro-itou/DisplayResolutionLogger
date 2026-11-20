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
using   System.Collections.ObjectModel;
using   System.Windows;
using   System.Windows.Media.Imaging;

using   MonitorLogger.Models;
using   MonitorLogger.Services;


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

    this.m_srvMonitor = new Services.MonitorService();
    this.m_srvStorage = new Services.LogStorageService();
//    this.m_taskModel  = new Models.SampleModel();
//    this.m_viewModel  = new ViewModels.SampleViewModel(this.m_taskModel);

//    this.DataContext = this.m_viewModel;

    //  テストデータ  //
    this.m_logItems = new ObservableCollection<LogEntry>();
    this.m_logItems.CollectionChaged += (s, e) =>
    {
        this.m_srvStorage.SaveLogs(this.m_logItems);
    }
    lvwEvent.ItemsSource = this.m_logItems;
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

//----------------------------------------------------------------
/**
**
**/
private  void
WriteDetailLog(
        ELogEventType   eventKind,
        System.String   eventTitle,
        System.String   localTitle)
{
    int sW  = 0;
    int sH  = 0;

    try {
        var sb = new System.Text.StringBuilder();
        DateTime curTime = DateTime.Now;
        sb.AppendLine("==================================================");
        sb.AppendLine($"[{curTime:yyyy-MM-dd HH:mm:ss.fff}] {eventTitle}");
        sb.AppendLine($"{localTitle}");
        sb.AppendLine("==================================================");

        // 全モニター数の取得
        sb.AppendLine("MonitorFriendlyNames:");
        var friendlyNames = this.m_srvMonitor.GetMonitorFriendlyNames();
        foreach ( string s in friendlyNames.Keys ) {
            string value = friendlyNames[s];
            sb.AppendLine($"Key = {s} Value = {value}");
        }

        var allScreens = System.Windows.Forms.Screen.AllScreens;
        sb.AppendLine($"認識されているモニター数: {allScreens.Length}");

        for (int i = 0; i < allScreens.Length; i++)
        {
            var scr = allScreens[i];
            sW  = scr.Bounds.Width;
            sH  = scr.Bounds.Height;

            sb.AppendLine($"--- モニター [{i}] --------------------");
            sb.AppendLine($"  ・デバイス名  : {scr.DeviceName}");
            sb.AppendLine($"  ・メイン画面  : {(scr.Primary ? "はい (Primary)" : "いいえ")}");
            sb.AppendLine($"  ・解像度(全体): {sW} x {sH}");
            sb.AppendLine($"  ・配置座標    : X={scr.Bounds.X}, Y={scr.Bounds.Y}");
            sb.AppendLine($"  ・作業領域    : {scr.WorkingArea.Width} x {scr.WorkingArea.Height} (タスクバー等を除く)");

            string  friendlyName = this.m_srvMonitor.FindMonitorFriendlyNames(
                    scr.DeviceName);
            sb.AppendLine($"  ・モニター製品名: {friendlyName}");
        }

        // RegistryからDPIの目安を取得 (C# 8.0の switch 式を活用)
        var registryValue = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", 96);
        int dpi = registryValue is int val ? val : 96;

        sb.AppendLine("--------------------------------------------------");
        sb.AppendLine($"システムDPIの目安: {dpi} DPI");
        sb.AppendLine(); // 空行

        this.m_logItems.Add(new LogEntry {
            EventDateTime = curTime,
            EventKind = eventKind,
            EventTitle = eventTitle,
            ScreenHeight = sH,
            ScreenWidth = sW
        });

        // ファイルに追記
        System.IO.File.AppendAllText(
            LogFilePath, sb.ToString(), System.Text.Encoding.UTF8);
    } catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine($"ログ出力エラー: {ex.Message}");
    }
}


//========================================================================
//
//    Member Variables.
//

private   static  readonly  string  LogFilePath =
    System.IO.Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Resolution.log");

private   readonlu  LogStorageService       m_srvStorage;
private   readonly  MonitorService          m_srvMonitor;

//  private   Models.SampleModel            m_taskModel;
//  private   ViewModels.SampleViewModel    m_viewModel;
private   ObservableCollection<LogEntry>    m_logItems;


}   //  End class  MainWindow

}   //  End of namespace  MonitorLogger.Views
