using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Features;

namespace RuCollection.API.Subclasses
{
    public abstract class SubclassBase
    {
        public abstract string Name { get; }

        public abstract int Health { get; }

        public virtual void Assign(Player player)
        {

        }

        public virtual void Deassign(Player player)
        {

        }
}
}
