using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class PlayerKinesisAttack : Ikinesis {

		public string animName = "Attack01";
		public bool animEnd = false;

		public override string AnimName => animName;
		public override bool Interrupt => animEnd;

		public override void AnimationEnd() => animEnd = true;
	}
}
