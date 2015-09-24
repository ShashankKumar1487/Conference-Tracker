using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conference_Track_Management
{
    public abstract class BaseConferenceSession
    {
        #region Constants
        public const string amString = "AM";
        public const string pmString = "PM";
        public const string minuteString = "min";
        #endregion

        #region Properties
        public int CurrentHour { get; set; }
        public int CurrentMinutes { get; set; }
        public List<string> SessionDetails { get; set; }
        #endregion

        #region Constructor
        public BaseConferenceSession(int currentHourValue,int currentMinutesValue)
        {
            CurrentHour = currentHourValue;
            CurrentMinutes = currentMinutesValue;
            SessionDetails = new List<string>();
        }
        #endregion

        #region Public Methods
        public IEnumerable<string> GetTalksWithSpecifiedDuration(int duration)
        {
            return ConferenceTalk.Instance.GetAllConferenceTalks().Where(talk => talk.Value == duration).Select(talk => talk.Key).ToList();
        }

        public void UpdateHoursAndMinutes(int duration)
        {
            var updatedMinutes = CurrentMinutes + duration;
            if (updatedMinutes > 60)
            {
                CurrentMinutes = updatedMinutes - 60;
                CurrentHour += 1;
            }
            else if (updatedMinutes == 60)
            {
                CurrentHour += 1;
                CurrentMinutes = 0;
            }
            else
            {
                CurrentMinutes = updatedMinutes;
            }
        }

        public abstract List<string> CreateSession();

        public abstract bool CheckNextTalkValidity(int duration);
        #endregion
    }
}
