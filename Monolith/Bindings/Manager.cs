using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class Manager
    {
        #region Static interface

        private static Manager instance = null;
        private static Manager Instance
        {
            get
            {
                if (instance == null)
                    instance = new Manager();

                return instance;
            }
        }

        public static List<Type> Types
        {
            get
            {
                return Manager.Instance.bindings;
            }
        }

        public static void Register(Type t)
        {
            Manager.Instance.register(t);
        }

        #endregion

        #region Implementation

        private List<Type> bindings;

		private Manager()
		{
			this.bindings = new List<Type>();
		}

        private void register(Type t)
        {
            this.bindings.Add(t);
        }

        #endregion
    }
}
