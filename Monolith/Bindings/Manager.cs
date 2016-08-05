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
                return Manager.Instance.types;
            }
        }

        public static Dictionary<string, IBinding> Bindings
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

        public static void Create(Type t, string identifier, Signals.Signal<IConvertible> first, Signals.Signal<IConvertible> second)
        {
            Manager.Instance.create(t, identifier, first, second);
        }

        #endregion

        #region Implementation

        private List<Type> types;
        private Dictionary<string, IBinding> bindings;

		private Manager()
		{
			this.types = new List<Type>();
            this.bindings = new Dictionary<string, IBinding>();
		}

        private void register(Type t)
        {
            this.types.Add(t);
        }

        private void create(Type t, string identifier, Signals.Signal<IConvertible> first, Signals.Signal<IConvertible> second)
        {
            if(typeof(IBinding).IsAssignableFrom(t))
            {
                IBinding b = (IBinding)Activator.CreateInstance(t, new object[] { identifier, first, second });

                this.bindings[identifier] = b;
            }
        }

        #endregion
    }
}
