﻿using System;
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

        public static void Register(BindingBase b)
        {
            Manager.Instance.register(b);
        }

        #endregion

        private List<Type> bindings;

        private void register(BindingBase b)
        {
            this.bindings.Add(b.GetType());
        }
    }
}
