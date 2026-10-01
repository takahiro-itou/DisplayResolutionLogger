
Imports System


Namespace Global.ViewVb.Models

Public Class SampleModel


Private m_resultText As String

Public Property ResultText() As String
   Get
       Return  Me.m_resultText
   End Get
   Set(ByVal value As String)
       Me.m_resultText = value
   End Set
End Property


Public Overridable Function executeCommand(
        ByVal progress As IProgress(Of Integer) ) As Integer
''--------------------------------------------------------------------
''    モデルのタスクを実行する。
''--------------------------------------------------------------------

    Using process As New System.Diagnostics.Process()
        process.StartInfo.FileName = "ipconfig.exe"
        process.StartInfo.UseShellExecute = False
        process.StartInfo.RedirectStandardInput = False
        process.StartInfo.RedirectStandardOutput = True
        process.StartInfo.RedirectStandardError = False
        process.Start()

        Dim Reader As System.IO.StreamReader = process.StandardOutput
        Dim output As String = Reader.ReadToEnd()

        Me.ResultText = output
        process.WaitForExit()
        process.Close()
    End Using

    progress.Report(100)
    executeCommand = 0
End Function


End Class

End Namespace
