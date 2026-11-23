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

using   MonitorLogger.Models;
using   MonitorLogger.Services;


namespace  MonitorLogger.ViewModels  {

using   LogEntriesList  = ObservableCollection<LogEntry>;


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
/**   インスタンスを初期化する
**  （コンストラクタ）。
**
**  @param [in] srvLogStorage   ストレージサービス。
**/
public
MainViewModel(
        LogStorageService   srvLogStorage)
{
    this.m_srvMonitor = new Services.MonitorService();
    this.m_srvStorage = srvLogStorage;

    this.m_logItems = this.m_srvStorage.LoadLogs();

    this.m_isStart  = false;
    this.m_isSaved  = false;
    this.m_isEnable = false;
}


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   ログをクローズする。
**
**  ディスプレイと電源の監視を終了する。
**/
public  virtual  void
CloseLogger()
{
    WriteDetailLog(
        ELogEventType.ProgramExit, "ProgramExit",
        "【プログラム終了】",
        true);

    if ( ! this.m_isEnable ) { return; }

    SystemEvents.PowerModeChanged       -= OnPowerModeChanged;
    SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;

    this.m_isEnable = false;
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  void
OnSessionEnding(SessionEndingCancelEventArgs e)
{
    WriteDetailLog(
        ELogEventType.SessionEnding, "SessionEnding",
        "【シャットダウン等】",
        true);

    if ( ! this.m_isEnable ) { return; }

    SystemEvents.PowerModeChanged       -= OnPowerModeChanged;
    SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;

    this.m_isEnable = false;
}


//----------------------------------------------------------------
/**
**
**/
public  virtual  void
SaveLogs()
{
    if ( this.m_isSaved || ! this.m_isStart ) { return; }

    this.m_srvStorage.SaveLogs(this.m_logItems);
    this.m_isSaved  = true;
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  void
StartLogger()
{
    this.m_isStart  = true;
    this.m_isSaved  = false;

    WriteDetailLog(
        ELogEventType.ProgramStart, "ProgramStart",
        "【プログラム起動】現在のディスプレイ構成",
        true);

    if ( this.m_isEnable ) { return; }

    SystemEvents.PowerModeChanged       += OnPowerModeChanged;
    SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;

    this.m_isEnable = true;
}


//----------------------------------------------------------------
/**
**
**/
public  virtual  void
WriteDetailLog(
        ELogEventType   eventKind,
        System.String   eventTitle,
        System.String   localTitle,
        System.Boolean  flagSave = true)
{
    int sW  = 0;
    int sH  = 0;
    System.String   monitorName = "?";

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

        for ( int i = 0; i < allScreens.Length; ++i )
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

            monitorName = this.m_srvMonitor.FindMonitorFriendlyNames(
                    scr.DeviceName);
            sb.AppendLine($"  ・モニター製品名: {monitorName}");
        }

        this.m_logItems.Add(new LogEntry {
            MonitorName     = monitorName,
            EventDateTime   = curTime,
            EventKind       = eventKind,
            EventTitle      = eventTitle,
            ScreenHeight    = sH,
            ScreenWidth     = sW
        });
        this.m_isSaved  = false;

        if ( flagSave ) {
            SaveLogs();
        }

        // RegistryからDPIの目安を取得 (C# 8.0の switch 式を活用)
        var registryValue = Registry.GetValue(
                @"HKEY_CURRENT_USER\Control Panel\Desktop\WindowMetrics",
                "AppliedDPI", 96);
        int dpi = registryValue is int val ? val : 96;

        sb.AppendLine("--------------------------------------------------");
        sb.AppendLine($"システムDPIの目安: {dpi} DPI");
        sb.AppendLine(); // 空行

        //  ファイルに追記。    //
        System.IO.File.AppendAllText(
            LogFilePath, sb.ToString(), System.Text.Encoding.UTF8);
    } catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine($"ログ出力エラー: {ex.Message}");
    }
}


//========================================================================

//----------------------------------------------------------------
/**
**
**/
public  LogEntriesList  LogItems
{
    get { return  this.m_logItems; }
}


//========================================================================
//
//    For Internal Use Only.
//

//----------------------------------------------------------------
/**
**
**/
private  async  void
OnDisplaySettingsChanged(object? sender, EventArgs e)
{
    await System.Threading.Tasks.Task.Delay(200);
    WriteDetailLog(
        ELogEventType.DisplayChange, "DisplayChange",
        "【イベント検知】画面構成または解像度の変更が発生しました",
        true
    );
}


//----------------------------------------------------------------
/**
**
**/
private  void
OnPowerModeChanged(object? sender, PowerModeChangedEventArgs e)
{
    switch ( e.Mode ) {
    case  PowerModes.Suspend:
        WriteDetailLog(
            ELogEventType.PowerSuspend, "Suspend",
            "システムがスリープ/休止状態に移行します。",
            false);
        break;
    case  PowerModes.Resume:
        WriteDetailLog(
            ELogEventType.PowerResume, "Resume",
            "システムがスリープ/休止状態から復帰しました。",
            true);
        break;
    case  PowerModes.StatusChange:
        WriteDetailLog(
            ELogEventType.PowerChange, "PowerStatus",
            "電源ステータスが変更されました。",
            true);
        break;
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

private   readonly  LogStorageService   m_srvStorage;
private   readonly  MonitorService      m_srvMonitor;

private             LogEntriesList      m_logItems;

private             System.Boolean      m_isStart;
private             System.Boolean      m_isSaved;
private             System.Boolean      m_isEnable;


}   //  End class  MainViewModel

}   //  End of namespace  MonitorLogger.ViewModels
