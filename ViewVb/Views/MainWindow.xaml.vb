
Namespace Global.ViewVb.Views

Public Class MainWindow

Private m_taskModel As Models.SampleModel
Private m_viewModel As ViewModels.SampleViewModel


Public Sub New()
''--------------------------------------------------------------------
''    コンストラクタ
''--------------------------------------------------------------------
    InitializeComponent()

    Me.m_taskModel = New Models.SampleModel()
    Me.m_viewModel = New ViewModels.SampleViewModel(Me.m_taskModel)

    Me.DataContext = Me.m_viewModel
End Sub


Private Sub mnuFileExit_Click(ByVal sender As Object, ByVal e As EventArgs)
''--------------------------------------------------------------------
''    メニュー「ファイル」－「終了」
''--------------------------------------------------------------------
    System.Windows.Application.Current.Shutdown()
End Sub


End Class

End Namespace
