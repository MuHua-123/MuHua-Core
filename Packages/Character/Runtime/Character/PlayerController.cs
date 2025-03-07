using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
    public class PlayerController : Character {
        public override bool UpdateMove(Vector3 position) {
            return movement.UpdateMove(position);
        }

        public override void AnimationTrigger(string value) {
            throw new System.NotImplementedException();
        }
        public override void AnimationEnd() {
            throw new System.NotImplementedException();
        }
    }
}