using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpringbrook
{
    public class BellSchedule
    {
        public static TimeSpan time;
        public static readonly TimeSpan transitionTime = new TimeSpan(0, 5, 0);

        public static Period lunch = new Period("Lunch", 40);
        public static Period advisory = new Period("Advisory", 10);
        public static Period pepRally = new Period("Pep Rally", 55);

        public static int announcements = 0;

        public static List<Period> periods = new List<Period>();

        //Name, class length, and other changes to the schedule for every day
        public static Dictionary<string, SchoolDay> days = new Dictionary<string, SchoolDay>()
        {
            { "Normal", new SchoolDay (47,  delegate
                {
                    periods.Insert(4, lunch);
                })
            },
            { "Advisory", new SchoolDay (43, delegate
                {
                    advisory.Duration = new TimeSpan(0, 25, 0);
                    periods.Insert(3, advisory);
                    lunch.Duration = new TimeSpan(0, 38, 0);
                    periods.Insert(5, lunch);
                })
            },
            { "Half", new SchoolDay (28, delegate
                {
                    lunch.Duration = new TimeSpan(0, 25, 0);
                    periods.Insert(7, lunch);
                    announcements = 6;
                })
            },
            { "Two-Hour Delay", new SchoolDay (30, delegate
                {
                    periods.Insert(4, lunch);
                    time = new TimeSpan(9, 45, 0);
                })
            },
            { "Advisory at the Beginning", new SchoolDay (45, delegate
                {
                    periods.Insert(0, advisory);
                    periods.Insert(5, lunch);
                })
            },
            { "Advisory at the End", new SchoolDay (45, delegate
                {
                    periods.Insert(4, lunch);
                    periods.Insert(8, advisory);
                })
            },
            { "Assembly", new SchoolDay (35, delegate
                {
                    periods[3].Duration = new TimeSpan(0, 60, 0);
                    periods.Insert(4, new Period("Assembly", 60));
                    periods.Insert(5, lunch);
                    announcements = -1;
                })
            },
            { "Pep Rally Outside", new SchoolDay (39, delegate
                {
                    lunch.Duration = new TimeSpan(0, 38, 0);
                    periods.Insert(5, lunch);
                    periods.Insert(8, pepRally);
                    announcements = -1;
                })
            },
            { "Pep Rally Gymnasium", new SchoolDay (37, delegate
                {
                    periods[3].Duration = new TimeSpan(0, 35, 0);
                    periods[6].Duration = new TimeSpan(0, 55, 0);
                    periods.Insert(5, lunch);
                    periods.Insert(8, pepRally);
                    announcements = -1;
                })
            }
        };

        public static List<Period> getSchedule(string dayName)
        {
            SchoolDay day = days[dayName];
            
            //Set defaults
            lunch.Duration = new TimeSpan(0, 40, 0);
            advisory.Duration = new TimeSpan(0, 10, 0);
            time = new TimeSpan(7, 45, 0);
            announcements = 0;

            //Reset list to periods 1-7
            periods.Clear();
            for (var i = 1; i < 8; i++)
                periods.Add(new Period("Period " + i.ToString(), days[dayName].ClassLength));

            //Make changes that need to be made to other periods based on the schedule
            days[dayName].Changes();

            for (int i = 0; i < periods.Count; i++)
            {
                //Deal with inconsistencies in schedule
                if (periods[i].Name == "Lunch")
                    time = time.Subtract(transitionTime);
                else if ((dayName == "Normal" || dayName == "Advisory") && periods[i].Name == "Period 5")
                    time = time.Add(new TimeSpan(0, 1, 0));
                else if (dayName == "Half" && periods[i].Name == "Period 6")
                    time = time.Subtract(new TimeSpan(0, 1, 0));
                else if (dayName == "Pep Rally Outside" && periods[i].Name == "Pep Rally")
                    time = time.Add(new TimeSpan(0, 4, 0));

                //Set period start time
                periods[i].startTime = new DateTime(time.Ticks).ToString("h:mm"); //"h':'m"

                //Add 5 minutes to a period if it has announcements
                if (announcements == i)
                    time = time.Add(transitionTime);

                //Set period end time
                periods[i].endTime = new DateTime(time.Add(periods[i].Duration).Ticks).ToString("h:mm");
                //Add class length time and 5 minute in between period
                time = time.Add(periods[i].Duration).Add(transitionTime);
            }

            return periods;
        }
    }

    //Defines a school day based on its schedule
    public class SchoolDay
    {
        public int ClassLength;
        //Changes that need to be made to the other parts of the schedule
        public Action Changes;

        public SchoolDay(int classLength, Action changes)
        {
            ClassLength = classLength;
            Changes = changes;
        }
    }

    public class Period
    {
        public string Name;
        public string startTime;
        public string endTime;
        public TimeSpan Duration;

        public Period(string name, int duration)
        {
            Name = name;
            Duration = new TimeSpan(0, duration, 0);
        }
    }
}
