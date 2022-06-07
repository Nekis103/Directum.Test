namespace Directum.PersonalMeeting.Extensions
{
    using System;
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Методы расширения.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Преобразование даты строки в дату в формате dd/MM/yyyy.
        /// </summary>
        /// <param name="strLine">Строка.</param>
        /// <returns>Преобразованная дата или если невозможно преобразовать то null.</returns>
        public static DateTime? ParseFormatDate(this string strLine)
        {
            DateTime result;
            if (DateTime.TryParseExact(strLine, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Преобразование даты строки в дату в формате HH/mm/dd/MM/yyyy.
        /// </summary>
        /// <param name="strLine">Строка.</param>
        /// <returns>Преобразованная дата или если невозможно преобразовать то null.</returns>
        public static DateTime? ParseFormatDateTime(this string strLine)
        {
            DateTime result;
            if (DateTime.TryParseExact(strLine, "HH/mm/dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Формирование строки, из встречи.
        /// </summary>
        /// <param name="meeting">Встреча.</param>
        /// <returns>Сформированная строка.</returns>
        public static string GetWriteString(this Meeting meeting)
        {
            return string.Format("{0,-20} {1,-20} {2,-20}", meeting.DateStart, meeting.DateEnd, meeting.Alert);
        }

        /// <summary>
        /// Формирование строки, из даты и времени.
        /// </summary>
        /// <param name="date">Дата и время.</param>
        /// <returns>Сформированная строка.</returns>
        public static string GetStringAlert(this DateTime date)
        {
            return string.Format(": {0,-20} месяцев {1,-20} дней {2,-20} часов {3,-20} минут", date.Month, date.Day, date.Hour, date.Minute);
        }
    }
}
