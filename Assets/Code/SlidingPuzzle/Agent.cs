using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Playground.AI;
using UnityEngine;

namespace Playground.SlidingPuzzle
{
    public class Agent
    {
        private Problem<PuzzleBoard, Environment.Action> problem;
        private IList<Environment.Action> seq;

        /*
         * persistent: seq, an action sequence, initially empty
         *      state, some description of the current world state
         *        goal, a goal, initially null
         *        problem, a problem formulation
         *    state ←UPDATE-STATE (state,percept)
         *    if seq is empty then
         *        goal ←FORMULATE -GOAL (state)
         *        problem ←FORMULATE-PROBLEM (state,goal)
         *        seq ←SEARCH (problem)
         *        if seq = failure then return a null action
         *     action ←FIRST (seq)  
         *     seq ←REST (seq)  
         *     return action  
         */

        public Solution<Environment.Action> Act(Percept percept)
        {   
            problem = (Problem<PuzzleBoard, Environment.Action>)  percept.GetAttribute(Environment.Attributes.Problem);

            Debug.Log("Begin search");
            return BreadthFirstSearch();
        }
        
        
        private Solution<Environment.Action> BreadthFirstSearch()
        {
            Node node = new Node()
            {
                pathCost = 0,
                state = problem.GetInitialState(),
                action = Environment.Action.None,
                parent = null
            };

            Queue<Node> frontier = new Queue<Node>();
            HashSet<PuzzleBoard> explored = new HashSet<PuzzleBoard>();

            frontier.Enqueue(node);
            while (frontier.Count != 0)
            {
                node = frontier.Dequeue();
                explored.Add(node.state);

                ICollection<Environment.Action> actions = problem.GetActions();
                foreach (var action in actions)
                {
                    Node child = CreateChildNode(action, node);
                    if (!(frontier.Contains(child) || explored.Contains(child.state)))
                    {
                        if (problem.GoalTest(child.state))
                        {
                            return CreateSolution(child);
                        }
                        frontier.Enqueue(child);
                    }
                }

            }

            return Solution<Environment.Action>.FAILURE;
        }

        private Solution<Environment.Action> CreateSolution(Node node)
        {
            Solution<Environment.Action> solution = new Solution<Environment.Action>();
            
            Stack<Environment.Action> stack = new Stack<Environment.Action>();
            Node currentNode = node;
            while (currentNode != null)
            {
                stack.Push(currentNode.action);
                currentNode = currentNode.parent;
            }

            while (stack.Count != 0)
            {
                solution.AddAction(stack.Pop());
            }

            return solution;
        }

        private Node CreateChildNode(Environment.Action action, Node parent)
        {
            Node child = new Node()
            {
                state = problem.Transition(parent.state, action),
                parent = parent,
                action = action,
                pathCost = parent.pathCost + 1
            };
            return child;
        }

        private class Node
        {
            public PuzzleBoard state;
            public Node parent;
            public Environment.Action action;
            public int pathCost;
        }
    }
}
