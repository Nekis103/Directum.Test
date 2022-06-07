namespace Directum.PersonalMeeting.Interface
{
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Валидирующий встречи.
    /// </summary>
    public interface IValidationMeeting
    {
        /// <summary>
        /// Проверка встречи на валидность.
        /// </summary>
        /// <param name="meeting">Встреча.</param>
        /// <returns>True/false.</returns>
        public bool CheckMeeting(Meeting meeting);
    }
}
