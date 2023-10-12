using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.States
{
    public class StateMachine
    {
        private State _currentState;
        private Dictionary<Type, State> _states = new Dictionary<Type, State>();

        public void AddState(State state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>(Transform target) where T : State
        {
            var type = typeof(T);
            if (_currentState!=null &&_currentState.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newstate))
            {
                _currentState?.Exit();
                _currentState = newstate;
                _currentState.Enter(target);
            }
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}