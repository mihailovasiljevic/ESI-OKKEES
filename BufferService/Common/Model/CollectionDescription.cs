using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [Serializable]
    public class CollectionDescription
    {
        private int id;
        private DataSet dataSet;
        private DumpingPropertyCollection dumpingPropertyCollection;

        public CollectionDescription(int id, DataSet dataSet, DumpingPropertyCollection dumpingPropertyCollection)
        {
            this.id = id;
            this.dataSet = dataSet;
            this.dumpingPropertyCollection = dumpingPropertyCollection;
        }
        [DataMember]
        public DumpingPropertyCollection DumpingPropertyCollection
        {
            get { return dumpingPropertyCollection; }
            set { dumpingPropertyCollection = value; }
        }

        [DataMember]
        public DataSet DataSet
        {
            get { return dataSet; }
            set { dataSet = value; }
        }


    }
}
