using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace BufferService
{
    [DataContract]
    public class DeltaCD
    {
        private int transactionId;
        private List<CollectionDescription> add;
        private List<CollectionDescription> update;
        private DataSets dataSets;
        //private CollectionDescription remove;

        public DeltaCD(DataSets dataSets)
        {
            transactionId = -1;
            SetDataSet = dataSets;
            InitializeCollectionDescriptions();
        }

        public DataSets SetDataSet
        {
            get { return dataSets; }
            set
            {
                if (value == null)
                {
                    this.dataSets = new DataSets();
                }
                else
                {
                    this.dataSets = value;
                }
            }
        }

        public DeltaCD(int transactionId, List<CollectionDescription> add, List<CollectionDescription> update)
        {
            this.transactionId = transactionId;
            SetDataSet = new DataSets();
            Add = add;
            Update = update;
        }

        private void InitializeCollectionDescriptions()
        {
            this.add = new List<CollectionDescription>(5);
            this.update = new List<CollectionDescription>(5);

            ClearCollectionDescriptions(add);
            ClearCollectionDescriptions(update);
        }

        public void ClearCollectionDescriptions(List<CollectionDescription> listCollectionDescriptions)
        {
            if (listCollectionDescriptions == null)
            {
                throw new ArgumentException("Lista ne sme biti prazna!");
            }
            listCollectionDescriptions.Clear();
            for (int i = 0; i < 5; i++)
            {
                listCollectionDescriptions.Add(new CollectionDescription(i, dataSets.GetDataSet(i), new DumpingPropertyCollection()));
            }
        }
        [DataMember]
        public List<CollectionDescription> Add
        {
            get { return add; }
            set
            {
                if (value == null)
                {
                    this.add = new List<CollectionDescription>(5);
                    ClearCollectionDescriptions(this.add);
                }
                else
                {
                    this.add = value;
                }
            }
        }
        [DataMember]
        public List<CollectionDescription> Update
        {
            get { return update; }
            set
            {
                if (value == null)
                {
                    this.update = new List<CollectionDescription>(5);
                    ClearCollectionDescriptions(this.update);
                }
                else
                {
                    this.update = value;
                }
            }
        }
        [DataMember]
        public int TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }
    }
}
