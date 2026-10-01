
using System.Windows;

using ViewCs;


namespace  ViewCs.Views  {

public  partial class  MainWindow : Window
{

    //----------------------------------------------------------------
    /**   デフォルトコンストラクタ。
    **
    **/
    public  MainWindow()
    {
        InitializeComponent();

        this.m_taskModel = new Models.SampleModel();
        this.m_viewModel = new ViewModels.SampleViewModel(this.m_taskModel);

        this.DataContext = this.m_viewModel;
    }


    private Models.SampleModel          m_taskModel;
    private ViewModels.SampleViewModel  m_viewModel;

}   //  End class  MainWindow

}   //  End of namespace  ViewCs.Views
