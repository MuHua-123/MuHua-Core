using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    /// <summary>
    /// 状态接口
    /// </summary>
    public abstract class MachineState {
        protected readonly Machine machine;

        public MachineState(Machine machine) => this.machine = machine;

        /// <summary> 进入状态 </summary>
        public abstract void Enter();
        /// <summary> 更新状态 </summary>
        public abstract void Update();
        /// <summary> 退出状态 </summary>   
        public abstract void Exit();
        /// <summary> 触发状态 </summary> 
        public abstract void Trigger();
    }
}