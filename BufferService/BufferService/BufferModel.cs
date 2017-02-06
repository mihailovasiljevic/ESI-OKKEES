using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BufferService.Duplex;
using BufferService.State;
using Common;
using Common.Model;
using Connection;

namespace BufferService
{
    public class BufferModel
    {
        private DeltaCD deltaCd;
        private DataSets dataSets;
        private SystemConfiguration systemConfiguration;
        private DBRepository dbRepository;
        private StateContext stateContext;
        

        public BufferModel()
        {
            this.dataSets = new DataSets();
            this.deltaCd = new DeltaCD(dataSets);

            systemConfiguration = new SystemConfiguration(-1, -1, -1, States.LOCAL);
            dbRepository = new DBRepository("localhost", "esi-oikkes", "root", "root");
            this.stateContext = new StateContext(new LocalState(this, null), DbRepository, systemConfiguration);

        }

        public BufferModel(DataSets dataSets, DeltaCD deltaCd, SystemConfiguration systemConfiguration,
            DBRepository dbRepository, StateContext stateContext)
        {
            this.dataSets = dataSets;
            this.deltaCd = deltaCd;

            this.systemConfiguration = systemConfiguration;
            this.dbRepository = dbRepository;
            this.stateContext = stateContext;
        }

        public SystemConfiguration GetSystemystemConfiguration
        {
            get { return systemConfiguration; }
            set { systemConfiguration = value; }
        }

        public StateContext GetStateContext
        {
            get { return stateContext; }
        }

        public DataSets GetDataSets
        {
            get { return dataSets; }
        }

        public DeltaCD GetDeltaCd
        {
            get { return deltaCd; }
            set { deltaCd = value; }
        }

        public DBRepository DbRepository
        {
            get { return dbRepository; }
            set { dbRepository = value; }
        }
    }
}
