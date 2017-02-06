using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Enumeration for avaliable codes.
    /// </summary>

    public enum Codes : int
    {
        ANALOG = 1,
        DIGITAL = 2,
        CUSTOM = 3,
        LIMITSET = 4,
        SINGLENODE = 5,
        MULTIPLENODE = 6,
        CONSUMER = 7,
        SOURCE = 8,
        MOTION = 9,
        SENSOR = 10
    }

    public enum States
    {
        LOCAL = 0,
        REMOTE = 1
    }
}
