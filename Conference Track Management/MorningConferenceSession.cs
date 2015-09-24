using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conference_Track_Management
{
    public class MorningConferenceSession : BaseConferenceSession
    {
        #region Contants
        private const int morningSessionEndTimeinHours = 12;
        #endregion

        #region Constructor
        public MorningConferenceSession(int currentHour, int currentMinutes)
            : base(currentHour, currentMinutes)
        {

        }
        #endregion

        #region Public Methods
        public override List<string> CreateSession()
        {
            //Using a random number generator to get the talk duration
            var random = new Random();
            string sessionDetails = string.Empty;
            try
            {
                while (CurrentHour < morningSessionEndTimeinHours)
                {
                    var talkDuration = random.Next(1, 13) * 5;
                    var talks = GetTalksWithSpecifiedDuration(talkDuration);

                    if (talks != null && talks.Any())
                    {
                        if (CheckNextTalkValidity(talkDuration))
                        {
                            var talkName = talks.First();
                            var conferenceTalksWithoutDuration=ConferenceTalk.Instance.GetAllTalksWithoutDuration();
                            sessionDetails = (conferenceTalksWithoutDuration.Any() && !conferenceTalksWithoutDuration.Contains(talkName)) ?
                                string.Format("{0}:{1}{2} {3} {4}{5}", CurrentHour.ToString("00"), CurrentMinutes.ToString("00"), amString, talkName, talkDuration, minuteString) :
                                string.Format("{0}:{1}{2} {3}", CurrentHour.ToString("00"), CurrentMinutes.ToString("00"), amString, talkName);
                            //Adding the talk info to the repository
                            SessionDetails.Add(sessionDetails);
                            //Updating the hours and minutes remiaing for the session
                            UpdateHoursAndMinutes(talkDuration);
                            //Removing the talk details from the main repository
                            ConferenceTalk.Instance.RemoveTalk(talkName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SessionDetails;
        }

        public override bool CheckNextTalkValidity(int duration)
        {
            var isValid = false;
            int updatedMinutes = CurrentMinutes + duration;
            int updatedHour = CurrentHour;
            if (updatedMinutes > 60)
            {
                updatedMinutes -= 60;
                updatedHour += 1;

            }
            else if (updatedMinutes == 60)
            {
                updatedMinutes = 0;
                updatedHour += 1;
            }

            var totalMinutes = (updatedHour * 60) + updatedMinutes;
            var remainingMinutes = (60 * morningSessionEndTimeinHours) - totalMinutes;
            if ((totalMinutes < (60 * morningSessionEndTimeinHours) && ((remainingMinutes % 30 == 0) || (remainingMinutes % 45 == 0) || (remainingMinutes % 60 == 0))) ||
                ((totalMinutes == (60 * morningSessionEndTimeinHours)) && remainingMinutes == 0))
                isValid = true;

            return isValid;
        }
        #endregion
    }
}
