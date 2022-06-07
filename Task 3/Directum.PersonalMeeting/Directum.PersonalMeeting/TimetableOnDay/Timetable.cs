namespace Directum.PersonalMeeting.TimetableOnDay
{
    using System;
    using System.Collections.Generic;
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Отчет.
    /// </summary>
    public class Timetable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timetable"/> class.
        /// </summary>
        /// <param name="meetingItems">Списко объектов для отчета.</param>
        public Timetable(List<Meeting> meetingItems)
        {
            this.List = meetingItems ?? throw new ArgumentNullException(nameof(meetingItems));
        }

        /// <summary>
        /// Gets or sets список встреч.
        /// </summary>
        public List<Meeting> List { get; set; }

        /// <summary>
        /// Gets or sets Дата рассписания.
        /// </summary>
        public string DateTimetable { get; set; }
    }
}
