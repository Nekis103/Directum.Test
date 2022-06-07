namespace Directum.PersonalMeeting.Model
{
    using System;

    /// <summary>
    /// Встреча.
    /// </summary>
    public class Meeting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Meeting"/> class.
        /// </summary>
        /// <param name="dateStart">Дата начала встречи.</param>
        /// <param name="dateEnd">Дата окончания встречи.</param>
        public Meeting(DateTime dateStart, DateTime dateEnd)
        {
            this.DateStart = dateStart;
            this.DateEnd = dateEnd;
        }

        /// <summary>
        /// Gets or sets дата начала встречи.
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Gets or sets дата окончания встречи.
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Gets or sets оповещение.
        /// </summary>
        public DateTime Alert { get; set; }
    }
}
