using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [Serializable]
    public class DataSet
    {
        private Dictionary<Codes, int> dataSet;

        public DataSet()
        {
            dataSet = new Dictionary<Codes, int>();
        }

        public void AddPair(Codes code, int dataSetCode)
        {

            Util.ElementExistsInEnumeration(typeof(Codes), code);

            if (dataSet.Count > 0)
            {
                if (dataSet.ElementAt(0).Value != dataSetCode)
                {
                    throw new ArgumentException("Niste uneli adekvatan parametar. dataSetCode mora biti: " +
                                                dataSet.ElementAt(0).Value + "!");
                }
            }

            dataSet.Add(code, dataSetCode);
        }
        [DataMember]
        public IReadOnlyDictionary<Codes, int> GetDataSet
        {
            get { return dataSet; }
        }
        [DataMember]
        public Dictionary<Codes, int> SetDataSet
        {

            set { dataSet = value; }
        }

        public bool ExistsInDataSet(Codes code)
        {
            Util.ElementExistsInEnumeration(typeof(Codes), code);

            foreach (KeyValuePair<Codes, int> pair in GetDataSet)
            {
                if (pair.Key == code)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
