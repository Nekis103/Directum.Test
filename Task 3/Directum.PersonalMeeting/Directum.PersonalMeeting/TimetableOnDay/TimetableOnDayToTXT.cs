namespace Directum.PersonalMeeting.TimetableOnDay
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Directum.PersonalMeeting.Interface;
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Создание рассписания в файле ТХТ.
    /// </summary>
    public class TimetableOnDayToTXT : IStrategyCreateTimetableOnDay
    {
        private List<Meeting> meetingList = new List<Meeting>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TimetableOnDayToTXT"/> class.
        /// </summary>
        /// <param name="list">Список встреч.</param>
        public TimetableOnDayToTXT(List<Meeting> list)
        {
            this.meetingList = list;
        }

        /// <summary>
        /// Метод создания рассписания.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <param name="datetime">Выбранная дата.</param>
        public void CreateTimetableOnDay(string path, DateTime datetime)
        {
            if (this.meetingList.Count == 0)
            {
                return;
            }

            var timetable = new Timetable(this.meetingList.FindAll(x => x.DateStart.ToShortDateString() == datetime.ToShortDateString()));
            timetable.DateTimetable = datetime.ToShortDateString();
            var reportDesigner = new TimetableDesigner();
            var resultString = reportDesigner.CreateTimetable(timetable);
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(resultString);
            }
        }
    }
}
