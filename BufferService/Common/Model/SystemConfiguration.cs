using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{

    [DataContract]
    public class SystemConfiguration
    {
        private int deadband;
        private double pmin;
        private double pmax;
        private States state;

        public SystemConfiguration(int deadband, double pmin, double pmax, States state)
        {
            this.deadband = deadband;
            this.pmax = pmax;
            this.pmin = pmin;
            State = state;
        }

        [DataMember]
        public int Deadband
        {
            get { return deadband; }
            set { deadband = value; }
        }
        [DataMember]
        public double Pmin
        {
            get { return pmin; }
            set { pmin = value; }
        }
        [DataMember]
        public double Pmax
        {
            get { return pmax; }
            set { pmax = value; }
        }

        [DataMember]
        public States State
        {
            get { return state; }
            set
            {
                bool exists = Enum.IsDefined(typeof(States), value);
                if (!exists)
                {
                    throw new ArgumentException("Niste uneli adekvatan parametar. Mora pripadati States enumeraciji!");
                }
                this.state = value;
            }
        }
    }
}
