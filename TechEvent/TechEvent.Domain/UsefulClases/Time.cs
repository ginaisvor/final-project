using System;
using System.Collections.Generic;
using System.Text;

namespace TechEvent.Domain.UsefulClases
{
    public class Time
    {
        private int totalTime;

        public int TotalTime
        {
            get { return totalTime; }
            set { totalTime = (value >= 0 ? value % 1440 : 0); }
        }

        public Time(int totalTime=0)
        {
            TotalTime = totalTime;
        }

        public void Add(int minute)
        {
            TotalTime += minute;
        }

        public int M
        {
            get
            {
                return totalTime % 60;
            }
        }

        public int H
        {
            get
            {
                return (TotalTime - M) / 60;
            }
        }

        public override string ToString()
        {
            return String.Format($"{H:00}:{M:00}");
        }
    }
}
