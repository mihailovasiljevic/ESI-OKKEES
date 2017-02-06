using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class DataSets
    {
        private List<DataSet> dataSets;

        public DataSets()
        {
            dataSets = new List<DataSet>(5);

            InitializeDataSets();
        }

        public void InitializeDataSets()
        {
            for (int i = 0; i < 5; i++)
            {
                dataSets.Add(new DataSet());
            }
            dataSets.ElementAt(0).AddPair(Codes.ANALOG, 1);
            dataSets.ElementAt(0).AddPair(Codes.DIGITAL, 1);

            dataSets.ElementAt(1).AddPair(Codes.CUSTOM, 2);
            dataSets.ElementAt(1).AddPair(Codes.LIMITSET, 2);

            dataSets.ElementAt(2).AddPair(Codes.SINGLENODE, 3);
            dataSets.ElementAt(2).AddPair(Codes.MULTIPLENODE, 3);

            dataSets.ElementAt(3).AddPair(Codes.CONSUMER, 4);
            dataSets.ElementAt(3).AddPair(Codes.SOURCE, 4);

            dataSets.ElementAt(4).AddPair(Codes.MOTION, 5);
            dataSets.ElementAt(4).AddPair(Codes.SENSOR, 5);
        }

        public DataSet GetDataSet(Codes code)
        {
            Util.ElementExistsInEnumeration(typeof(Codes), code);
            foreach (DataSet dataSet in dataSets)
            {
                foreach (KeyValuePair<Codes, int> pair in dataSet.GetDataSet)
                {
                    if (pair.Key == code)
                    {
                        return dataSet;
                    }
                }
            }

            return null;
        }

        public bool ExistInDataSet(Codes code)
        {
            foreach (DataSet dataSet in dataSets)
            {
                if (dataSet.ExistsInDataSet(code))
                {
                    return true;
                }
            }

            return false;
        }

        public DataSet GetDataSet(int index)
        {

            if (index < 0 || index > 4)
            {
                throw new ArgumentOutOfRangeException("Indeks mora biti ceo broj izmedju 0 i 4.");
            }
            return dataSets.ElementAt(index);
        }
    }
}
