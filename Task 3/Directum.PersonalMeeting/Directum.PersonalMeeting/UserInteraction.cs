namespace Directum.PersonalMeeting.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Directum.PersonalMeeting.Extensions;
    using Directum.PersonalMeeting.Model;

    /// <summary>
    /// Взаимодествие с пользователем в консоли.
    /// </summary>
    public class UserInteraction
    {
        private readonly string formatDateTime = "формат ввода: час/минуты/день/месяц/год Пример 01/02/03/04/2022";
        private readonly string formatDate = "формат ввода: день/месяц/год Пример 03/04/2022";
        private MeetingService meetingServices;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInteraction"/> class.
        /// </summary>
        /// <param name="meetingService">Сервис работы со встречами.</param>
        public UserInteraction(MeetingService meetingService)
        {
            this.meetingServices = meetingService;
        }

        /// <summary>
        /// Распечатать в консоли всё за выбраннуб дату.
        /// </summary>
        public void PrintAllForDate()
        {
            Console.WriteLine("Введите Дату {0}", this.formatDate);
            var dateTime = this.GetDate();
            Console.WriteLine("{1} Рассписание встреч на {0,-20:dd/MM/yy}{1}", dateTime, Environment.NewLine);
            var meetingList = this.meetingServices.GetAll().Select(x => x).Where(x => x.DateStart.Day == dateTime.Day);
            this.PrintAll(meetingList);
            this.ExitFromResult();
        }

        /// <summary>
        /// Распечатать на консоли главное меню.
        /// </summary>
        public void PrintMainMenu()
        {
            Console.WriteLine("_  Выберите пункт:" + Environment.NewLine +
                " 1    Посмотреть рассписание на определённый день" + Environment.NewLine +
                " 2    Экспорт рассписания в файл" + Environment.NewLine +
                " 3    Добавить встречу" + Environment.NewLine +
                " 4    Изменить данные о встрече" + Environment.NewLine +
                " 5    Удалить встречу" + Environment.NewLine +
                " Esc  Выйти из программы");
        }

        /// <summary>
        /// Добавить новую встречу.
        /// </summary>
        /// <returns>Встреча.</returns>
        public Meeting Add()
        {
            Console.WriteLine("Введите дату начала встречи в формате: {0}", this.formatDateTime);
            var dateStart = this.GetDateTime();
            while (!this.CheckDateStart(dateStart))
            {
                dateStart = this.GetDateTime();
            }

            Console.WriteLine("Введите дату окончания встречи в формате: {0}", this.formatDateTime);
            var dateEnd = this.GetDateTime();
            while (!this.CheckDateEnd(dateEnd, dateStart))
            {
                dateEnd = this.GetDateTime();
            }

            var meeting = new Meeting((DateTime)dateStart, (DateTime)dateEnd);
            Console.Clear();
            Console.WriteLine("Хотите назначить напомнинание?");
            Console.WriteLine("1 - да" + Environment.NewLine + "2 - нет");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.KeyChar.ToString())
            {
                case "1":
                    Console.WriteLine("Введите время за которое нужно предупредить: {0}", this.formatDateTime);
                    meeting.Alert = this.AddAlert(dateStart);
                    break;
                default:
                    break;
            }

            Console.Clear();
            Console.WriteLine("Новая встреча добавлена." + Environment.NewLine);
            return meeting;
        }

        /// <summary>
        /// Поиск выбранной встречи в сервисе встреч для изменения.
        /// </summary>
        /// <returns>Выбранная дата.</returns>
        public DateTime FindForEdit()
        {
            Console.WriteLine("Введите дату начала встречи которую хотите изменить. {0}", this.formatDateTime);
            return this.GetDateTime();
        }

        /// <summary>
        /// Изменение данный выбранной встречи.
        /// </summary>
        /// <param name="meeting">Выбранная встреча.</param>
        /// <returns>Обновленная встреча.</returns>
        public Meeting Edit(Meeting meeting)
        {
            Console.WriteLine("Выберите то что хотите изменить.");
            this.EditMenu(meeting);
            return meeting;
        }

        /// <summary>
        /// Удалить встречу.
        /// </summary>
        /// <returns>Дату встречи которую необходимо удалить.</returns>
        public DateTime Remove()
        {
            Console.WriteLine("Введите дату начала встречи которую хотите удалить. {0}", this.formatDateTime);
            return this.GetDateTime();
        }

        /// <summary>
        /// Поулчить путь.
        /// </summary>
        /// <returns>Путь файла.</returns>
        public string GetPath()
        {
            string path = string.Empty;
            while (path == string.Empty)
            {
                Console.WriteLine(@"Введите полный путь файла. в формате C:\Users\User\имяфайла.расширение");
                path = Console.ReadLine();
                Console.Clear();
            }

            return path;
        }

        /// <summary>
        /// Получить день на которуй нужно составить расписание.
        /// </summary>
        /// <returns>Дата.</returns>
        public DateTime GetDateTimetable()
        {
            Console.WriteLine("Введите Дату на которую хотите получить рассписание {0}", this.formatDate);
            return this.GetDate();
        }

        private DateTime GetDateTime()
        {
            DateTime? date = null;
            while (date == null)
            {
                date = Console.ReadLine().ParseFormatDateTime();
                Console.Clear();
                if (date == null)
                {
                    Console.WriteLine("Не верный формат даты, попробуйте ещё раз,{0}{1}", Environment.NewLine, this.formatDateTime);
                }
            }

            Console.Clear();
            return (DateTime)date;
        }

        private DateTime GetDate()
        {
            DateTime? date = null;
            while (date == null)
            {
                date = Console.ReadLine().ParseFormatDate();
                Console.Clear();
                if (date == null)
                {
                    Console.WriteLine("Не верный формат даты, попробуйте ещё раз,{0}{1}", Environment.NewLine, this.formatDate);
                }
            }

            Console.Clear();
            return (DateTime)date;
        }

        private void ExitFromResult()
        {
            Console.WriteLine(" Esc Для возврата в меню");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
            }
            while (key.Key != ConsoleKey.Escape);
            Console.Clear();
        }

        private void PrintAll(IEnumerable<Meeting> meetingList)
        {
            var listMeeting = this.meetingServices.GetAll();
            var number = 0;
            Console.WriteLine("   {0,-7} {1,-20} {2,-20}", "Номер", "Начало встречи", "Окончание встречи");
            foreach (var item in meetingList)
            {
                Console.WriteLine("   {0,-7} {1,-20:H:mm:ss} {2,-20:H:mm:ss}{3}", ++number, item.DateStart, item.DateEnd, Environment.NewLine);
            }
        }

        private DateTime AddAlert(DateTime dateStart)
        {
            var dateAlert = dateStart;
            int month, day, hours, minutes;
            Console.WriteLine("Введете количество месяцев");
            while (!int.TryParse(Console.ReadLine(), out month))
            {
                Console.WriteLine("Не верный ввод, введите количество часов");
            }

            Console.WriteLine("Введете количество дней");
            while (!int.TryParse(Console.ReadLine(), out day))
            {
                Console.WriteLine("Не верный ввод, введите количество часов");
            }

            Console.WriteLine("Введете количество часов");
            while (!int.TryParse(Console.ReadLine(), out hours))
            {
                Console.WriteLine("Не верный ввод, введите количество часов");
            }

            Console.WriteLine("Введете количество минут");
            while (!int.TryParse(Console.ReadLine(), out minutes))
            {
                Console.WriteLine("Не верный ввод, введите количество минут");
            }

            dateAlert = dateAlert.AddMonths(-month);
            dateAlert = dateAlert.AddMonths(-day);
            dateAlert = dateAlert.AddMonths(-hours);
            dateAlert = dateAlert.AddMinutes(-minutes);
            return dateAlert;
        }

        private Meeting EditMenu(Meeting meeting)
        {
            ConsoleKeyInfo key;
            do
            {
                this.PrintEditMenu();
                key = Console.ReadKey();
                switch (key.KeyChar.ToString())
                {
                    case "1":
                        this.EditDateStart(meeting);
                        break;
                    case "2":
                        this.EditDateEnd(meeting);
                        break;
                    case "3":
                        this.EditDateAlert(meeting);
                        break;
                    case "4":
                        this.EditMeeting(meeting);
                        break;
                    default:
                        break;
                }
            }
            while (key.Key != ConsoleKey.Escape);
            return meeting;
        }

        private void PrintEditMenu()
        {
            Console.WriteLine("_  Выберите пункт:" + Environment.NewLine +
                " 1    Изменить дату начала встречи" + Environment.NewLine +
                " 2    Изменить дату окончания встречи" + Environment.NewLine +
                " 3    Изменить время оповещения о начале встречи" + Environment.NewLine +
                " 4    Изменить всё" + Environment.NewLine +
                " Esc  Выйти из режима редактирования");
        }

        private Meeting EditDateStart(Meeting meeting)
        {
            Console.WriteLine("Введите дату начала встречи в формате: {0}", this.formatDateTime);
            meeting.DateStart = this.GetDateTime();
            Console.Clear();
            Console.WriteLine("Дата начала встречи изменена" + Environment.NewLine);
            return meeting;
        }

        private Meeting EditDateEnd(Meeting meeting)
        {
            Console.WriteLine("Введите дату окончания встречи в формате: {0}", this.formatDateTime);
            meeting.DateEnd = this.GetDateTime();
            Console.Clear();
            Console.WriteLine("Дата окончания встречи изменена" + Environment.NewLine);
            return meeting;
        }

        private Meeting EditDateAlert(Meeting meeting)
        {
            Console.WriteLine("Введите время за которое нужно предупредить: {0}", this.formatDateTime);
            meeting.Alert = this.AddAlert(meeting.DateStart);
            Console.Clear();
            Console.WriteLine("Время оповещения изменено изменена" + Environment.NewLine);
            return meeting;
        }

        private Meeting EditMeeting(Meeting meeting)
        {
            meeting = this.EditDateStart(meeting);
            meeting = this.EditDateEnd(meeting);
            meeting = this.EditDateAlert(meeting);
            Console.Clear();
            Console.WriteLine("Данные полностью заменены" + Environment.NewLine);
            return meeting;
        }

        private bool CheckDateStart(DateTime dateTime)
        {
            if (DateTime.Now < dateTime)
            {
                return true;
            }

            Console.WriteLine("Встреча не может начинаться раньше текущего времени");
            return false;
        }

        private bool CheckDateEnd(DateTime dateTime, DateTime startDateTime)
        {
            if (startDateTime < dateTime)
            {
                return true;
            }

            Console.WriteLine("Встреча не может заканчиваться раньше того, как она начнется");
            return false;
        }
    }
}
