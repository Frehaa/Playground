using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Playground.AI
{
    public abstract class Agent
    {
        public abstract void Act(Percept percept);
    }
}
