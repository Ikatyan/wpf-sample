using System;
using System.ComponentModel;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfApp1.Annotations;

namespace WpfApp1
{

    /// <summary>
    /// MainWindowクラスのViewModel (ViewModel)
    /// </summary>
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private string _text = "Hello World!";

        /// <summary>
        /// Textに値がセットされたら、値が変更されたことをViewに通知
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// ボタンがクリックされたときに実行するコマンド
        /// </summary>
        public ChangeTextCommand ChangeTextCommand { get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            ChangeTextCommand = new ChangeTextCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// プロパティの値が変更したことをViewに伝えるメソッド
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// ViewModelのTextを"Hello hogehoge!"にかえるコマンド (Model)
    /// </summary>
    public class ChangeTextCommand: ICommand
    {
        private readonly MainWindowViewModel _viewModel;
        private bool _canExec = true;

        private bool CanExec
        {
            get => _canExec;
            set
            {
                if (_canExec == value) return;
                _canExec = value;
                RaiseCanExecuteChanged();
            }
        }

        public ChangeTextCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return CanExec;
        }

        public void Execute(object parameter)
        {
            CanExec = false;
            _viewModel.Text = "Hello hogehoge!";
            CanExec = true;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}