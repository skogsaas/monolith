using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class ObjectBase : IObject, IAttributeContainer, IActionContainer
    {
        private List<IAttribute> attributes;
        private List<IAction> actions;

        public string Identifier { get; private set; }

        public string Type { get; protected set; }

        public event ObjectEventHandler ObjectChanged;

        public ObjectBase(string identifier)
        {
            this.attributes = new List<IAttribute>();
            this.actions = new List<IAction>();

            this.Type = this.GetType().Name;

            this.Identifier = identifier;
        }

        private ObjectBase()
        {
            this.attributes = new List<IAttribute>();
            this.actions = new List<IAction>();

            this.Type = this.GetType().Name;
        }

        public void addAttribute(IAttribute a)
        {
            this.attributes.Add(a);
        }

        public void addAction(IAction a)
        {
            this.actions.Add(a);
        }

        public List<IAttribute> getAttributes()
        {
            return this.attributes;
        } 

        public List<IAction> getActions()
        {
            return this.actions;
        } 
    }
}