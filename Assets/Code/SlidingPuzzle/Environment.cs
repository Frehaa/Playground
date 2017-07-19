using Playground.AI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playground.SlidingPuzzle
{
    public class Environment : AI.Environment
    {
        private PuzzleBoard board;
        private Agent agent;

        public enum Attributes {
            PuzzleBoard,
            Problem
        }

        public enum Action
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            None
        }

        public Environment(PuzzleBoard board)
        {
            this.board = board;
            agent = new Agent();
        }

        public override void AddAgent()
        {
            throw new NotImplementedException();
        }
        
        public override void Step()
        {
            Percept percept = new Percept();
            
            percept.AddAttribute(Attributes.Problem, new PuzzleBoardProblem(board));
            try
            {
                Solution<Action> solution = agent.Act(percept);

            }catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            //if (Solution<Action>.IsFailure(solution))
            //{
            //    Debug.Log("Failed to find solution");
            //}
            //else
            //{
            //    Debug.Log("Found solution: ");
            //    foreach (Action action in solution)
            //    {
            //        Debug.Log(action);
            //    }
            //}
            
        }

        public class PuzzleBoardProblem : AI.Problem<PuzzleBoard, Action>
        {
            private PuzzleBoard initialState;

            public PuzzleBoardProblem(PuzzleBoard initialState)
            {
                this.initialState = initialState;
            }

            public override ICollection<Action> GetActions()
            {
                IList<Action> actions = new List<Action>()
                {
                    Action.MoveUp,
                    Action.MoveDown,
                    Action.MoveLeft,
                    Action.MoveRight
                };

                return actions;
            }

            public override PuzzleBoard GetInitialState()
            {
                return initialState;
            }

            public override bool GoalTest(PuzzleBoard state)
            {
                return state.InGoalState;
            }

            public override float PathCost(IList<Action> path)
            {
                return path.Count;
            }

            public override PuzzleBoard Transition(PuzzleBoard state, Action action)
            {
                PuzzleBoard newState = new PuzzleBoard(state);
                switch (action)
                {
                    case Action.MoveUp:
                        newState.MoveUp();
                        break;
                    case Action.MoveDown:
                        newState.MoveDown();
                        break;
                    case Action.MoveLeft:
                        newState.MoveLeft();
                        break;
                    case Action.MoveRight:
                        newState.MoveRight();
                        break;
                    default:
                        throw new Exception("Undhandled action");
                }
                return newState;
            }
        }
    }
}
