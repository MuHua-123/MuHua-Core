using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    /// <summary>
    /// 有限状态机
    /// </summary>
    public abstract class Machine : Character {
        protected MachineState currentState;
        protected Dictionary<string, MachineState> states = new Dictionary<string, MachineState>();

        protected virtual void Start() => InitializeStates();
        protected virtual void Update() => currentState?.Update();

        #region 状态机功能
        protected abstract void InitializeStates();
        protected virtual void RegisterState(string stateType, MachineState state) {
            if (!states.ContainsKey(stateType)) { states.Add(stateType, state); }
        }
        public virtual void ChangeState(string stateType) {
            if (states.ContainsKey(stateType)) {
                currentState?.Exit();
                currentState = states[stateType];
                currentState.Enter();
            }
            else {
                Debug.LogWarning($"State {stateType} is not registered.");
            }
        }
        #endregion
    }
}