using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Model;
using Connection;

namespace BufferService.State
{
    public class StateContext
    {
        private IState state;
        private DBRepository dbRepository;
        private SystemConfiguration sysConfig;
        public StateContext(IState state, DBRepository dbRepository, SystemConfiguration sysConfig)
        {
            this.state = state;
            this.dbRepository = dbRepository;
            this.sysConfig = sysConfig;
        }

        public IState State
        {
            get { return state; }
            set
            {
                state = value;
                Console.WriteLine("Novo stanje tabele je: {0}", state.GetType().ToString());
                dbRepository.ChangeServiceState(GetState());
                sysConfig.State = GetStates();
            }
        }

        private int GetState()
        {
            if (state is LocalState)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private States GetStates()
        {
            if (state is LocalState)
            {
                return States.LOCAL;
            }
            else
            {
                return States.REMOTE;
            }
        }

        public void Request()
        {
            state.Handle(this);
        }

    }
}
