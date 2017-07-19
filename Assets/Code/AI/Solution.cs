using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Playground.AI
{
    public class Solution<Action> : IEnumerable<Action>
    {
        public static readonly Solution<Action> FAILURE = new Solution<Action>();

        private IList<Action> actions = new List<Action>();

        public void AddAction(Action action)
        {
            actions.Add(action);
        }

        public IEnumerator<Action> GetEnumerator()
        {
            return actions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return actions.GetEnumerator();
        }
        
        public static bool IsFailure(Solution<Action> solution)
        {
            return (solution == FAILURE);
        }
    }
}
