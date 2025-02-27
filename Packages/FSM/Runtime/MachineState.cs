using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua
{
    public abstract class MachineState
    {
        protected Machine machine;

        public MachineState(Machine machine) => this.machine = machine;

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}