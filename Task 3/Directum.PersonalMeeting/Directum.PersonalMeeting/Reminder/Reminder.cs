namespace Directum.PersonalMeeting.Reminder
{
    using System.Text;

    /// <summary>
    /// Оповещение.
    /// </summary>
    public class Reminder
    {
        private StringBuilder remiders;

        /// <summary>
        /// Добавляет
        /// </summary>
        /// <param name="str"></param>
        public void Add(string str)
        {
            remiders.AppendLine(str);
        }
        public void GetAll()
        {

        }
    }
}
