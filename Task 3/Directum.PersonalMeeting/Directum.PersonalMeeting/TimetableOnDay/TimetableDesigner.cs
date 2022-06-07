namespace Directum.PersonalMeeting.TimetableOnDay
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Directum.PersonalMeeting.Extensions;
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Создание отчета.
    /// </summary>
    public class TimetableDesigner
    {
        private List<Meeting> libraryItems = new List<Meeting>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTimetable"/> class.
        /// </summary>
        /// <param name="timetable">Расписание, для формирования строки и дальнейшего экспорта в файл.</param>
        /// <returns>готовая строка для экспорта в файл.</returns>
        public string CreateTimetable(Timetable timetable)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("Рассписание на {0}", timetable.DateTimetable + Environment.NewLine));
            stringBuilder.AppendJoin(Environment.NewLine, timetable.List.Select(x => x.GetWriteString()));

            return stringBuilder.ToString();
        }
    }
}
