namespace Directum.PersonalMeeting.Reminder
{
    using System;
    using System.Text;
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Проектировщик оповещений.
    /// </summary>
    public class ReminderDesigner
    {
        private StringBuilder reminder = new StringBuilder();

        /// <summary>
        /// Конструирование записи оповещения.
        /// </summary>
        /// <param name="meeting">Встреча.</param>
        public void SetWriteString(Meeting meeting)
        {
            if (meeting == null)
            {
                throw new NullReferenceException("пустое значение");
            }

            this.GetAlert(meeting);
            this.reminder.AppendLine($"Встреча состоится " +
                $"{meeting.DateStart.Day}.{meeting.DateStart.Month}.{meeting.DateStart.Year} в " +
                $"{meeting.DateStart.Hour}:{meeting.DateStart.Minute}");
        }

        /// <summary>
        /// Получить оповещение.
        /// </summary>
        /// <returns>Строка оповещения.</returns>
        public string GetWriteString()
        {
            return this.reminder.ToString();
        }

        /// <summary>
        /// Удалить все записи оповещения.
        /// </summary>
        public void Clear()
        {
            this.reminder.Clear();
        }

        private void GetAlert(Meeting meeting)
        {
            var month = meeting.DateStart.Month - meeting.Alert.Month;
            var day = meeting.DateStart.Day - meeting.Alert.Day;
            var hour = meeting.DateStart.Hour - meeting.Alert.Hour;
            var minute = meeting.DateStart.Minute - meeting.Alert.Minute;

            this.reminder.Append("Внимание, до встречи ");
            if (month != 0) this.reminder.Append(string.Format("{0} месяц ", month));

            if (day != 0) this.reminder.Append(string.Format("{0} день ", day));

            if (hour != 0) this.reminder.Append(string.Format("{0} часов ", hour));

            if (minute != 0) this.reminder.AppendLine(string.Format("{0} минут", minute));
        }
    }
}
