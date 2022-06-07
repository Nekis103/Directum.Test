namespace Directum.PersonalMeeting.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Directum.PersonalMeeting.Interface;
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Сервис встреч.
    /// </summary>
    public class MeetingService
    {
        private List<Meeting> meetingList;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingService"/> class.
        /// </summary>
        public MeetingService()
        {
            this.meetingList = new List<Meeting>();
        }

        /// <summary>
        /// Добавить встречу.
        /// </summary>
        /// <param name="meeting">Встреча.</param>
        /// <param name="validationMeeting">Валидатор.</param>
        /// <param name="remember">Оповещающий.</param>
        public void AddMeeting(Meeting meeting, IValidationMeeting validationMeeting, IReminder remember)
        {
            if (validationMeeting.CheckMeeting(meeting))
            {
                remember.AddListReminder(meeting);
                this.meetingList.Add(meeting);
            }
            else
            {
                Console.WriteLine("Встреча не была добавлена, так как время было занято.");
            }
        }

        /// <summary>
        /// Удалить встречу.
        /// </summary>
        /// <param name="date">Дата начала встречи.</param>
        /// <returns>Удалена или нет.</returns>
        public bool RemoveMeeting(DateTime date)
        {
            if (this.meetingList.Any(x => x.DateStart == date))
            {
                this.meetingList.Remove(this.meetingList.First(x => x.DateStart == date));
                return true;
            }

            return false;
        }

        /// <summary>
        /// Найти встречу по дате.
        /// </summary>
        /// <param name="date">Дата.</param>
        /// <returns>Встреча.</returns>
        public Meeting FindMeeting(DateTime date)
        {
            if (this.meetingList.Any(x => x.DateStart == date))
            {
                return this.meetingList.Find(x => x.DateStart == date);
            }

            return null;
        }

        /// <summary>
        /// Поулчит все встречи.
        /// </summary>
        /// <returns>Список встреч.</returns>
        public List<Meeting> GetAll()
        {
            return this.meetingList;
        }

        /// <summary>
        /// Создать расписание на день.
        /// </summary>
        /// <param name="strategyCreateTimetableOnDay">Стратегия создания расписания.</param>
        /// <param name="path">путь.</param>
        /// <param name="datetime">На какой день создавать.</param>
        public void CreateTimetable(IStrategyCreateTimetableOnDay strategyCreateTimetableOnDay, string path, DateTime datetime)
        {
            var dirictory = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirictory))
            {
                Directory.CreateDirectory(dirictory);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            strategyCreateTimetableOnDay.CreateTimetableOnDay(path, datetime);
        }
    }
}
