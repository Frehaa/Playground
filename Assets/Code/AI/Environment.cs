using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Playground.AI
{
    public abstract class Environment
    {
        public abstract void AddAgent();
        public abstract void Step();
    }
}
