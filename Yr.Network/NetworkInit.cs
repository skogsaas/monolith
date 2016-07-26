using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yr.Network
{
    public class NetworkInit
    {


        private static NetworkSettings settings = null; 

        internal static NetworkSettings Settings
        {
            get
            {
                if(settings == null)
                {

                }
                return settings; 
            }
            set
            {
                settings = value; 
            }
        }

        public NetworkInit(NetworkSettings network) 
        {
            if (network == null)
                throw new ArgumentNullException("The network object cannot be null");

            NetworkInit.Settings = network; 

        }

       
    }
}
