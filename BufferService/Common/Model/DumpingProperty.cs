using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [Serializable]
    [DataContract]
    public class DumpingProperty
    {
        private Codes code;
        private double dumpingValue;

        public DumpingProperty()
        {
            code = Codes.ANALOG;
            dumpingValue = -9999;
        }

        public DumpingProperty(Codes code, double dumpingValue)
        {
            Code = code;
            this.dumpingValue = dumpingValue;
        }

        [DataMember]
        public Codes Code
        {
            get { return code; }
            set
            {
                bool exists = Enum.IsDefined(typeof(Codes), value);
                if (!exists)
                {
                    throw new ArgumentException("Niste uneli adekvatan parametar. Mora pripadati Codes enumeraciji!");
                }
                this.code = value;
            }
        }

        [DataMember]
        public double DumpingValue
        {
            get { return dumpingValue; }
            set { dumpingValue = value; }
        }

    }
}
