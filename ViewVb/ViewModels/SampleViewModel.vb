
Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports System.Threading.Tasks
Imports System.Windows.Input

Imports ViewVb.Commands
Imports ViewVb.Models


Namespace Global.ViewVb.ViewModels

Public Class SampleViewModel
        Implements INotifyPropertyChanged

Private ReadOnly m_progress As System.IProgress(Of Integer)
Private ReadOnly m_trgModel As SampleModel

Private ReadOnly m_runModelTaskCommand As SimpleCommand
Private ReadOnly m_ClearTextCommand As SimpleCommand

Private m_returnCode As Integer
Private m_isRunning As Boolean


Public Sub New(ByVal model As SampleModel)
''--------------------------------------------------------------------
''    コンストラクタ
''--------------------------------------------------------------------

    Me.m_progress = New System.Progress(Of Integer)(AddressOf updateProgress)
    Me.m_trgModel = model

    Me.m_runModelTaskCommand = New SimpleCommand(
        Sub(ByVal parameter As Object)
            Me.runModelTaskAsync
        End Sub,
        Function(ByVal parameter As Object) As Boolean
            Return  Me.canRunTask()
        End Function
    )
    Me.m_clearTextCommand = New SimpleCommand(
        Sub(ByVal parameter As Object)
            Me.clearText()
        End Sub,
        Function(ByVal parameter As Object) As Boolean
            Return  Not Me.IsRunning
        End Function
    )

    Me.m_returnCode = 0
    Me.m_isRunning  = False
End Sub


''======================================================================
''
''    Properties.
''

Public Event PropertyChanged As PropertyChangedEventHandler _
        Implements INotifyPropertyChanged.PropertyChanged


Public Property IsRunning() As Boolean
    Get
        Return  Me.m_isRunning
    End Get
    Private Set(ByVal value As Boolean)
        Me.m_isRunning = value
        raisePropertyChanged()
        raiseCanExecuteChanged()
    End Set
End Property


Public Property ResultText() As String
    Get
        Return  Me.m_trgModel.ResultText
    End Get
    Set(ByVal value As String)
        Me.m_trgModel.ResultText = value
        raisePropertyChanged()
    End Set
End Property


Public Property ReturnCode() As Integer
    Get
        Return  Me.m_returnCode
    End Get
    Private Set(ByVal value As Integer)
        Me.m_returnCode = value
        raisePropertyChanged()
    End Set
End Property


Public Overridable ReadOnly Property ClearTextCommand() As ICommand
    Get
        Return  Me.m_clearTextCommand
    End Get
End Property


Public Overridable ReadOnly Property RunModelTaskCommand() As ICommand
    Get
        Return  Me.m_runModelTaskCommand
    End Get
End Property


''======================================================================
''
''    Public Member Functions.
''

Public Function canRunTask() As Boolean
    Return  Not Me.IsRunning
End Function


Public Overridable Sub clearText()
   Me.ResultText = ""
   Me.ReturnCode = 0
End Sub


Public Overridable Async Sub runModelTaskAsync()
''--------------------------------------------------------------------
''    モデルのタスクを非同期で実行する。
''--------------------------------------------------------------------
Dim result As Integer
Dim myTask As Task(Of Integer)

    Me.IsRunning  = True

    mytask = Task.Run(Of Integer)(
        Function() As Integer
            Return  Me.m_trgModel.executeCommand(Me.m_progress)
        End Function
    )
    result  = await mytask

    Me.ReturnCode = result
    Me.IsRunning  = False
End Sub


''======================================================================
''
''    Protected Member Functions.
''

Protected Overridable Sub raiseCanExecuteChanged()
    Me.m_runModelTaskCommand.RaiseCanExecuteChanged()
    Me.m_clearTextCommand.RaiseCanExecuteChanged()
End Sub


Protected Overridable Sub raisePropertyChanged(
        <CallerMemberName> Optional propertyName As String = Nothing)
    RaiseEvent  PropertyChanged(
            Me, New PropertyChangedEventArgs(propertyName)
    )
End Sub


Protected Overridable Sub updateProgress(
        ByVal progressValue As Integer)
    raisePropertyChanged(nameof(ResultText))
End Sub


End Class

End Namespace
