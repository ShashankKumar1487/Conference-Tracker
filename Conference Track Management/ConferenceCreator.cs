using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Conference_Track_Management
{
    public class ConferenceCreator
    {
        #region Constants
        private const string lunchEventInfo = "12:00PM Lunch";
        private const string networkingEventInfo = "05:00PM Networking Event";
        private const string trackString = "Track";
        private const int currentHourForMorningSession = 9;
        private const int currentMinutesForAnySession = 0;
        private const int currentHourForAfterNoonSession = 1;
        #endregion

        #region Variables
        private static int trackCounter = 1;
        #endregion

        #region Public Methods
        //Method to create conference schedule
        public static List<string> CreateConference()
        {
            var conferenceDetails = new List<string>();
            try
            {
                while (ConferenceTalk.Instance.GetAllConferenceTalks().Count() > 0)
                {
                    conferenceDetails.Add(string.Format("{0} {1:}", trackString, trackCounter));
                    //Create Morning Session
                    var morningSession = new MorningConferenceSession(currentHourForMorningSession, currentMinutesForAnySession);
                    var morningSessionDetails = morningSession.CreateSession();
                    if (morningSessionDetails != null && morningSessionDetails.Any())
                        morningSessionDetails.ForEach(item => conferenceDetails.Add(item));
                    //Add Lunch Event
                    conferenceDetails.Add(lunchEventInfo);
                    //Create AfternoonSession
                    var afterNoonSession = new AfternoonConferenceSession(currentHourForAfterNoonSession, currentMinutesForAnySession);
                    var afterNoonSessionDetails = afterNoonSession.CreateSession();
                    if (afterNoonSessionDetails != null && afterNoonSessionDetails.Any())
                        afterNoonSessionDetails.ForEach(item => conferenceDetails.Add(item));
                    //Add Networking Event
                    conferenceDetails.Add(networkingEventInfo);
                    conferenceDetails.Add("");
                    trackCounter++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conferenceDetails;
        }

        //Method to add all conference talk items to a repository
        public static void AddAllConferenceTalks(string inputPath)
        {
            int talkDuration;
            string talkName;

            try
            {
                using (StreamReader sr = new StreamReader(inputPath))
                {
                    while (sr.Peek() >= 0)
                    {
                        var talkInfo = sr.ReadLine().Split(' ');
                        var talkInfoLength = talkInfo.Length;
                        var talkDurationresult = Int32.TryParse(talkInfo[talkInfoLength - 1].Substring(0, 2), out talkDuration);

                        if (talkDurationresult)
                        {
                            talkName = talkInfo.ToList().TakeWhile(x => talkInfo.ToList().IndexOf(x) != talkInfoLength - 1).Aggregate((i, j) => i + " " + j);
                        }
                        else
                        {
                            talkName = talkInfo.ToList().Aggregate((i, j) => i + " " + j);
                        }
                        ConferenceTalk.Instance.AddTalk(talkName, talkDuration);
                    }
                }
            }
            catch (FileNotFoundException fex)
            {
                throw fex;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
