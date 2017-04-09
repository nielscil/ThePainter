using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    public static class CommandExecuter
    {
        private static Canvas _canvas;

        public static void Init(Canvas canvas)
        {
            _canvas = canvas;
        }

        private static Stack<ICommand> _history = new Stack<ICommand>();

        private static Stack<ICommand> _redoHistory = new Stack<ICommand>();

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

        public static void Undo()
        {
            CheckCanvas();

            ICommand command = PopHistory();

            if (command != null)
            {
                _redoHistory.Push(command);
            }
        }

        public static void GetExecutionState(out Stack<ICommand> history, out Stack<ICommand> redoHistory)
        {
            history = new Stack<ICommand>(_history);
            redoHistory = new Stack<ICommand>(_redoHistory);
        }

        public static void SetExecutionState(Stack<ICommand> history, Stack<ICommand> redoHistory)
        {
            _history = history;
            _redoHistory = redoHistory;
        }

        public static void Clear()
        {
            _history.Clear();
            _redoHistory.Clear();
        }

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

        private static void CheckCanvas()
        {
            if(_canvas == null)
            {
                throw new Exception("CommandExecuter not initialized!");
            }
        }
    }
}
