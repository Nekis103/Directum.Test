namespace Directum.PersonalMeeting.Interface
{
    using System;

    /// <summary>
    /// Контракт создания отчета.
    /// </summary>
    public interface IStrategyCreateTimetableOnDay
    {
        /// <summary>
        /// Метод создания рассписания.
        /// </summary>
        /// <param name="path">Путь файла.</param>
        /// <param name="datetime">Выбранная дата.</param>
        void CreateTimetableOnDay(string path, DateTime datetime);
    }
}
