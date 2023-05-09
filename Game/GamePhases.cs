using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TohoGame
{
    internal class GamePhases
    {
        private class Phase
        {
            double time;
            bool isCompleted;
            public Phase(double Time) 
            { 
                this.time = Time; 
                this.isCompleted = false; 
            }
            public double GetTime()
            {
                return this.time;
            }
            public bool IsCompleted()
            {
                return this.isCompleted;
            }
            public void markCompleted()
            {
                this.isCompleted = true;
            }
        }

        private List<Phase> phases;
        public GamePhases(int time1, int time2, int time3, int time4)
        {
            // grunts
            //phaseTimes.Add(45);

            // mid bosses
            //phaseTimes.Add(50);

            // grunts
            //phaseTimes.Add(45);

            // final boss
            //phaseTimes.Add(90);

            this.phases = new List<Phase>();
            this.phases.Add(new Phase(time1));
            this.phases.Add(new Phase(time2));
            this.phases.Add(new Phase(time3));
            this.phases.Add(new Phase(time4));
        }
        public List<double> getTimes()
        {
            List<double> l = new List<double>();
            foreach (Phase phase in this.phases)
            {
                l.Add(phase.GetTime());
            }
            return l;
        }

        public double FirstPhaseTime { get { return phases[0].GetTime(); } }
        public double SecondPhaseTime { get { return phases[1].GetTime(); } }
        public double ThirdPhaseTime { get { return phases[2].GetTime(); } }
        public double FourthPhaseTime { get { return phases[3].GetTime(); } }
        public bool Is1PComplete { get { return phases[0].IsCompleted(); } set { phases[0].markCompleted(); } }
        public bool Is2PComplete { get { return phases[1].IsCompleted(); } set { phases[1].markCompleted(); } }
        public bool Is3PComplete { get { return phases[2].IsCompleted(); } set { phases[2].markCompleted(); } }
        public bool Is4PComplete { get { return phases[3].IsCompleted(); } set { phases[3].markCompleted(); } }

    }
}
