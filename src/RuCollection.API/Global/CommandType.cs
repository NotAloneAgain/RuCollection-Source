using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuCollection.API.Global
{
    public enum CommandType : byte
    {
        RemoteAdmin,
        PlayerConsole,
        ServerConsole
    }
}
