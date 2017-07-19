using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Playground.AI
{
    public abstract class Problem<StateType, Action>
    {
        public abstract StateType GetInitialState();
        public abstract ICollection<Action> GetActions();
        public abstract StateType Transition(StateType state, Action action);
        public abstract bool GoalTest(StateType state);
        public abstract float PathCost(IList<Action> path);
    }
}
