namespace Directum.PersonalMeeting.Interface
{
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Напоминающий.
    /// </summary>
    public interface IReminder
    {
        /// <summary>
        /// Добавить в встречу о которой нужно напоминать в список.
        /// </summary>
        /// <param name="meeting">Встреча.</param>
        public void AddListReminder(Meeting meeting);
    }
}
