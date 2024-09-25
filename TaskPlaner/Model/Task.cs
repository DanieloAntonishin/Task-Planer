using System;
using System.Collections.Generic;

namespace TaskPlaner.Model
{
    [Serializable]
    public class Task
    {
        [NonSerialized]
        private List<TaskState> States;

        // Properties for Task. Setters call Save() method to save the task state
        #region Properties
        private string _Title = string.Empty;
        public string Title
        {
            get { return _Title; }
            set
            {
                Save();
                _Title = value;
            }
        }

        private string _Priority = string.Empty;
        public string Priority
        {
            get { return _Priority; }
            set
            {
                Save();
                _Priority = value;
            }
        }

        private string _Status = string.Empty;
        public string Status
        {
            get { return _Status; }
            set
            {
                Save();
                _Status = value;
            }
        }

        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set
            {
                Save();
                _Description = value;
            }
        }

        //private DateTime _DueDate = DateTime.Now;
        //public DateTime DueDate
        //{
        //    get { return _DueDate; }
        //    set
        //    {
        //        _DueDate = value;
        //        Save();
        //    }
        //}

        #endregion


        public Task() => States = new List<TaskState>();

        // Save Task state to list. If list is overflown delete first 30 records
        public void Save()
        {
            States.Add(new TaskState(this));
            if (States.Count > TaskState.MaxSize)
            {
                States.RemoveRange(0, 30);
            }
        }

        // Turn back last state and remove it
        public void Undo()
        {
            States[^1].ReturnState(this);
            States.Remove(States[^1]);
        }

        // look if Undo is possible
        public bool CanUndo() => States.Count != 0;


        record TaskState
        {
            public static readonly int MaxSize = 100;

            public string Title { get; init; }
            public string Priority { get; init; }
            public string Status { get; init; }
            public string Description { get; init; }
            public DateTime DueDate { get; init; }

            public TaskState(Task Momento)
            {
                Title = Momento._Title;
                Priority = Momento._Priority;
                Status = Momento._Status;
                Description = Momento._Description;
                //DueDate = Momento._DueDate;
            }

            // Return state to Task
            public void ReturnState(Task ReturnTo)
            {
                ReturnTo._Title = Title;
                ReturnTo._Priority = Priority;
                ReturnTo._Status = Status;
                ReturnTo._Description = Description;
                //ReturnTo._DueDate = DueDate;
            }
        }

    }
}
