using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Class for executing the commands
    /// </summary>
    public static class CommandExecuter
    {
        private static Canvas _canvas;

        /// <summary>
        /// Inits the commandexecuter class
        /// </summary>
        /// <param name="canvas">the current canvas</param>
        public static void Init(Canvas canvas)
        {
            _canvas = canvas;
        }

        private static Stack<ICommand> _history = new Stack<ICommand>();

        private static Stack<ICommand> _redoHistory = new Stack<ICommand>();

        /// <summary>
        /// Executes a command and adds it to the history
        /// </summary>
        /// <param name="command">to be executed command</param>
        public static void Execute(ICommand command)
        {
            CheckCanvas();

            if (command != null)
            {
                command.Execute(_canvas);
                _history.Push(command);
            }
            _redoHistory.Clear();
        }

        /// <summary>
        /// Redos a command
        /// </summary>
        public static void Redo()
        {
            CheckCanvas();

            if (_redoHistory.Count > 0)
            {
                ICommand command = _redoHistory.Pop();
                command.Execute(_canvas);
                _history.Push(command);
            }
        }

        /// <summary>
        /// Undos a command
        /// </summary>
        public static void Undo()
        {
            CheckCanvas();

            ICommand command = PopHistory();

            if (command != null)
            {
                _redoHistory.Push(command);
            }
        }

        /// <summary>
        /// Gets the current history and redo state
        /// </summary>
        /// <param name="history">commands history</param>
        /// <param name="redoHistory">redo commands history</param>
        public static void GetExecutionState(out Stack<ICommand> history, out Stack<ICommand> redoHistory)
        {
            history = new Stack<ICommand>(_history);
            redoHistory = new Stack<ICommand>(_redoHistory);
        }

        /// <summary>
        /// Sets the history and redo state
        /// </summary>
        /// <param name="history">commands history</param>
        /// <param name="redoHistory">redo commands history</param>
        public static void SetExecutionState(Stack<ICommand> history, Stack<ICommand> redoHistory)
        {
            _history = history;
            _redoHistory = redoHistory;
        }

        /// <summary>
        /// Clears the history
        /// </summary>
        public static void Clear()
        {
            _history.Clear();
            _redoHistory.Clear();
        }

        /// <summary>
        /// Pops command from history and undos it
        /// </summary>
        /// <returns>command</returns>
        private static ICommand PopHistory()
        {
            if (_history.Count == 0)
            {
                return null;
            }

            ICommand command = _history.Pop();
            command.Undo(_canvas);

            return command;
        }

        /// <summary>
        /// Check if the class is initialized
        /// </summary>
        private static void CheckCanvas()
        {
            if(_canvas == null)
            {
                throw new Exception("CommandExecuter not initialized!");
            }
        }
    }
}
