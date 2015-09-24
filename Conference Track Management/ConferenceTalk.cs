using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conference_Track_Management
{
    //Singleton class to add all the conference talks to a dictionary and make it available across the project via a single instance
    public sealed class ConferenceTalk
    {
        #region Private Variables
        private static readonly ConferenceTalk instance = new ConferenceTalk();
        private static Dictionary<string, int> dicConferenceTalks = new Dictionary<string, int>();
        private static List<string> listOfTalksWithoutDuration = new List<string>();
        #endregion

        #region Constructor
        private ConferenceTalk() { }
        #endregion

        #region Public Methods
        public static ConferenceTalk Instance
        {
            get
            {
                return instance;
            }
        }

        //Adding each talk item in to the dictionary
        public void AddTalk(string name, int duration)
        {
            if (duration == default(int))
            {
                //Adding the talk items with no duration specified and setting the duration as 60 min
                listOfTalksWithoutDuration.Add(name);
                duration = 60;
            }
            dicConferenceTalks.Add(name, duration);
        }

        //Removing talk item from the dictionary once added to the conference schedule
        public void RemoveTalk(string key)
        {
            dicConferenceTalks.Remove(key);
        }

        public Dictionary<string, int> GetAllConferenceTalks()
        {
            return dicConferenceTalks;
        }

        public List<string> GetAllTalksWithoutDuration()
        {
            return listOfTalksWithoutDuration;
        }
        #endregion
    }
}
