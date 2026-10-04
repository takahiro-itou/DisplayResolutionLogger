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

    WriteDetailLog("【プログラム起動】現在のディスプレイ構成");
    SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
}


private  void
OnDisplaySettingsChanged(object? sender, EventArgs e)
{
    WriteDetailLog("【イベント検知】画面構成または解像度の変更が発生しました");
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
        System.String   eventTitle)
{
    try {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("==================================================");
        sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {eventTitle}");
        sb.AppendLine("==================================================");

        // 全モニター数の取得
        var allScreens = System.Windows.Forms.Screen.AllScreens;
        sb.AppendLine($"認識されているモニター数: {allScreens.Length}");

        for (int i = 0; i < allScreens.Length; i++)
        {
            var scr = allScreens[i];
            sb.AppendLine($"--- モニター [{i}] --------------------");
            sb.AppendLine($"  ・デバイス名  : {scr.DeviceName}");
            sb.AppendLine($"  ・メイン画面  : {(scr.Primary ? "はい (Primary)" : "いいえ")}");
            sb.AppendLine($"  ・解像度(全体): {scr.Bounds.Width} x {scr.Bounds.Height}");
            sb.AppendLine($"  ・配置座標    : X={scr.Bounds.X}, Y={scr.Bounds.Y}");
            sb.AppendLine($"  ・作業領域    : {scr.WorkingArea.Width} x {scr.WorkingArea.Height} (タスクバー等を除く)");
        }

        // RegistryからDPIの目安を取得 (C# 8.0の switch 式を活用)
        var registryValue = Registry.GetValue(@"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics", "AppliedDPI", 96);
        int dpi = registryValue is int val ? val : 96;

        sb.AppendLine("--------------------------------------------------");
        sb.AppendLine($"システムDPIの目安: {dpi} DPI");
        sb.AppendLine(); // 空行

        // ファイルに追記
        System.IO.File.AppendAllText(_logFilePath, sb.ToString(), Encoding.UTF8);
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

private   Models.SampleModel            m_taskModel;
private   ViewModels.SampleViewModel    m_viewModel;


}   //  End class  MainWindow

}   //  End of namespace  MonitorLogger.Views
