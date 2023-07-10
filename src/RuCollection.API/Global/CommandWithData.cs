using CommandSystem;
using System;

namespace RuCollection.API.Global
{
    public abstract class CommandWithData : CustomCommand, IHasData
    {
        public abstract void Reset();
    }
}
