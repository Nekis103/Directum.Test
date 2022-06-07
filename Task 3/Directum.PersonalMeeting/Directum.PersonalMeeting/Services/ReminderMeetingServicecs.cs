namespace Directum.PersonalMeeting.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Directum.PersonalMeeting.Interface;
    using Directum.PersonalMeeting.Model;
    using Directum.PersonalMeeting.Reminder;

    /// <summary>
    /// Сервис оповещений.
    /// </summary>
    public class ReminderMeetingServicecs : IReminder
    {
        private List<Meeting> meetingList = new List<Meeting>();
        private CancellationTokenSource cancelTokenSource;
        private CancellationToken token;
        private ReminderDesigner reminderDesigner = new ReminderDesigner();
        private bool trigger = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReminderMeetingServicecs"/> class.
        /// </summary>
        public ReminderMeetingServicecs()
        {
            this.cancelTokenSource = new CancellationTokenSource();
            this.token = this.cancelTokenSource.Token;
        }

        /// <summary>
        /// Возобнавляет вывод на консоль напоминаний.
        /// </summary>
        public void StartOutputRemider()
        {
            this.trigger = true;
        }

        /// <summary>
        /// Приостанавливает вывод на консоль напоминаний.
        /// </summary>
        public void PauseOutputRemider()
        {
            this.trigger = true;
        }

        /// <summary>
        /// Добавить в встречу о которой нужно напоминать в список.
        /// </summary>
        /// <param name="meeting">Встреча.</param>
        public void AddListReminder(Meeting meeting)
        {
            this.meetingList.Add(meeting);
        }

        /// <summary>
        /// Запустить задачу напоминания о встречах.
        /// </summary>
        /// <returns>Задача завершена.</returns>
        public async Task ReminderMeetingAsync()
        {
            await Task.Run(() => this.ReminderMeeting());
        }

        /// <summary>
        /// Установить токен отмены.
        /// </summary>
        public void SetCanselToken()
        {
            this.cancelTokenSource.Cancel();
        }

        /// <summary>
        /// Распечатать в консоль все напоминания.
        /// </summary>
        public void PrintAllReminder()
        {
            if (this.reminderDesigner.GetWriteString() != string.Empty)
            {
                Console.WriteLine(this.reminderDesigner.GetWriteString());
                this.reminderDesigner.Clear();
            }
        }

        private void ReminderMeeting()
        {
            while (true)
            {
                if (!this.token.IsCancellationRequested)
                {
                    foreach (var item in this.meetingList)
                    {
                        if (item.Alert.ToShortDateString() == DateTime.Now.ToShortDateString() &&
                            item.Alert.Hour == DateTime.Now.Hour &&
                            item.Alert.Minute == DateTime.Now.Minute)
                        {
                            this.reminderDesigner.SetWriteString(item);
                            if (this.trigger)
                            {
                                this.PrintAllReminder();
                            }
                        }
                    }
                }
                else
                {
                    break;
                }

                Thread.Sleep(55000);
            }
        }
    }
}
