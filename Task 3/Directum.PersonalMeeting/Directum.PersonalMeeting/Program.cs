namespace Directum.PersonalMeeting
{
    using System;
    using Directum.PersonalMeeting.Services;
    using Directum.PersonalMeeting.TimetableOnDay;
    using Directum.PersonalMeeting.Validation;

    class Program
    {
        static void Main(string[] args)
        {
            var meetingService = new MeetingService();
            var uI = new UserInteraction(meetingService);
            var stratageCreateTimetable = new TimetableOnDayToTXT(meetingService.GetAll());
            var reminderService = new ReminderMeetingServicecs();
            reminderService.ReminderMeetingAsync();
            ConsoleKeyInfo key;
            do
            {
                uI.PrintMainMenu();
                key = Console.ReadKey();
                reminderService.PauseOutputRemider();
                switch (key.KeyChar.ToString())
                {
                    case "1":
                        Console.Clear();
                        uI.PrintAllForDate();
                        break;
                    case "2":
                        Console.Clear();
                        meetingService.CreateTimetable(stratageCreateTimetable, uI.GetPath(), uI.GetDateTimetable());
                        Console.WriteLine("Расписание записано в файл");
                        break;
                    case "3":
                        Console.Clear();
                        meetingService.AddMeeting(uI.Add(), new ValidatorMeeting(meetingService.GetAll()), reminderService);
                        break;
                    case "4":
                        Console.Clear();
                        uI.Edit(meetingService.FindMeeting(uI.FindForEdit()));
                        break;
                    case "5":
                        Console.Clear();
                        if (meetingService.RemoveMeeting(uI.Remove()))
                        {
                            Console.WriteLine("Встреча была удалена.");
                        }
                        else
                        {
                            Console.WriteLine("Такой встречи не существует.");
                        }

                        break;
                    default:
                        Console.Clear();
                        break;
                }

                reminderService.StartOutputRemider();
                reminderService.PrintAllReminder();
            }
            while (key.Key != ConsoleKey.Escape);
            reminderService.SetCanselToken();
            Console.Read();
        }
    }
}
