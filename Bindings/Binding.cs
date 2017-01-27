using Skogsaas.Legion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Skogsaas.Monolith.Bindings
{
    internal class Binding
    {
        public IBinding Config { get; private set; }

        private Channel fromChannel;
        private IObject fromObject;

        private Channel toChannel;
        private IObject toObject;

        private Channel bindingsChannel;

        public Binding(IBinding config, Channel channel)
        {
            this.Config = config;
            this.bindingsChannel = channel;

            this.fromChannel = Manager.Create(this.Config.FromChannel);
            this.fromChannel.SubscribePublishId(this.Config.FromObject, onFromObjectPublish);

            this.toChannel = Manager.Create(this.Config.ToChannel);
            this.toChannel.SubscribePublishId(this.Config.ToObject, onToObjectPublish);
        }

        private void onFromObjectPublish(Channel c, IObject obj)
        {
            this.fromObject = obj;
            bind();
        }

        private void onToObjectPublish(Channel c, IObject obj)
        {
            this.toObject = obj;
            bind();
        }

        private void bind()
        {
            if(this.fromObject != null && this.toObject != null)
            {
                mapValue();
                this.fromObject.PropertyChanged += onFromChanged;
            }
        }

        private void mapValue()
        {
            try
            {
                object fromValue = getPropertyValue(this.fromObject, this.Config.FromProperty);
                bool success = setPropertyValue(this.toObject, this.Config.ToProperty, fromValue);
            }
            catch (Exception ex)
            {
                Logging.Logger.Error("Could no set the property value.");
            }
        }

        private void onFromChanged(object caller, PropertyChangedEventArgs args)
        {
            string[] fromLevels = this.Config.FromProperty.Split('/');

            if(args.PropertyName != fromLevels.First()) // Something else has changed
            {
                return;
            }

            mapValue();
        }

        private static object getPropertyValue(IObject obj, string path)
        {
            string[] levels = path.Split('/');

            PropertyInfo currentProperty = null;
            object currentObj = obj;

            foreach (string level in levels)
            {
                currentProperty = currentObj.GetType().GetProperty(level);

                if(currentProperty != null)
                {
                    currentObj = currentProperty.GetValue(currentObj);
                }
                else
                {
                    return null;
                }

                if (currentObj == null)
                {
                    break;
                }
            }

            return currentObj;
        }

        private static bool setPropertyValue(IObject obj, string path, object value)
        {
            string[] levels = path.Split('/');

            PropertyInfo currentProperty = null;
            object currentObj = obj;

            for (int i = 0; i < levels.Length; i++)
            {
                currentProperty = currentObj.GetType().GetProperty(levels[i]);

                if(currentProperty == null)
                {
                    return false;
                }

                if (currentObj == null)
                {
                    return false;
                }

                if (i != (levels.Length - 1))
                {
                    // This is not the last level.
                    currentObj = currentProperty.GetValue(currentObj);
                }
                else
                {
                    // This is the last level, lets set the property value.
                    currentProperty.SetValue(currentObj, value);

                    return true;
                }
            }

            return false;
        }
    }
}
