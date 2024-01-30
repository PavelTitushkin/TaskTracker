namespace TaskTracker.Logic.Exceptions
{
    public class TaskNameIsNullException : Exception
    {
        public TaskNameIsNullException(string? message) : base(message)
        {
        }
    }
}
