namespace Directum.PersonalMeeting.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using Directum.PersonalMeeting.Interface;
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Валидатор встреч.
    /// </summary>
    public class ValidatorMeeting : IValidationMeeting
    {
        private List<Meeting> meetings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorMeeting"/> class.
        /// </summary>
        /// <param name="meetingList">Список встреч.</param>
        public ValidatorMeeting(List<Meeting> meetingList)
        {
            this.meetings = meetingList;
        }

        /// <summary>
        /// Проверка встречи занято ли время.
        /// </summary>
        /// <param name="meeting">Встреча.</param>
        /// <returns>true/false.</returns>
        public bool CheckMeeting(Meeting meeting)
        {
            if (this.meetings.All(x => x.DateStart > meeting.DateEnd || x.DateEnd < meeting.DateStart))
            {
                return true;
            }

            return false;
        }
    }
}
