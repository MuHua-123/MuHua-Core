using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua
{
    public abstract class Machine : MonoBehaviour
    {
        protected MachineState currentState;
        protected Dictionary<string, MachineState> states = new Dictionary<string, MachineState>();

        protected virtual void Start() => InitializeStates();

        protected virtual void Update() => currentState?.Update();

        protected abstract void InitializeStates();

        protected virtual void RegisterState(string stateType, MachineState state)
        {
            if (!states.ContainsKey(stateType)) { states.Add(stateType, state); }
        }

        public virtual void ChangeState(string stateType)
        {
            if (states.ContainsKey(stateType))
            {
                currentState?.Exit();
                currentState = states[stateType];
                currentState.Enter();
            }
            else
            {
                Debug.LogWarning($"State {stateType} is not registered.");
            }
        }
    }
}
