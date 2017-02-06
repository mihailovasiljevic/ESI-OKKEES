using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Common.Model
{
    [Serializable]
    public class DumpingPropertyCollection
    {
        private List<DumpingProperty> dumpingProperties;

        public DumpingPropertyCollection()
        {
            dumpingProperties = new List<DumpingProperty>();
        }


        public DumpingPropertyCollection(List<DumpingProperty> dumpingProperties)
        {
            this.dumpingProperties = dumpingProperties;
        }

        [DataMember]
        public IReadOnlyCollection<DumpingProperty> DumpingProperties
        {
            get { return dumpingProperties; }
        }

        //test za ovo!
        public void AddDumpingProperty(DumpingProperty dumpingProperty, CollectionDescription cd)
        {
            if (dumpingProperty == null || cd == null)
            {
                throw new ArgumentNullException("Uneli ste: " + dumpingProperty + " za dumping property i " + cd +
                                                " za collection description, a nijedna od te dve vrednosti ne sme biti null");
            }
            if (dumpingProperties.Count > 0)
            {
                if (!cd.DataSet.ExistsInDataSet(dumpingProperty.Code))
                {
                    throw new ArgumentException("Ne mozete ovo da uradite");
                }
            }

            dumpingProperties.Add(dumpingProperty);
        }
    }
}