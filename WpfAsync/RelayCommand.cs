using System.Windows.Input;

namespace WpfAsync
{
    internal class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;


        /// <summary>
        /// wird ausgeloest wenn RaiseCanExecuteChanged 
        /// aufgerufen wird
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Erstellt einen neuen Command
        /// </summary>
        /// <param name="execute">Action die ausgefuehrt wird</param>
        /// <param name="canExecute">Delegatelogik die prueft ob ausgefuehrt wird</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute null");
            _execute = execute;
            _canExecute = canExecute;
        }
        public RelayCommand(Action execute) : this(execute, null)
        { }


        /// <summary>
        /// legt fest ob relaycommand im aktuellen zustand ausgefuehrt  werden kann
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true darf ausgefuehrt werden, andernfalls false</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
        /// <summary>
        /// zum Aufrufen des Ereeignisses
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);

            }
        }
    }
}
