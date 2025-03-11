using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class KinesisAttack : Ikinesis {
		public bool animEnd = false;
		public string animName;
		public IAnimator animator;

		public KinesisAttack(ICharacter character, string name = "Attack01") {
			animName = name;
			animator = character.GetComponent<IAnimator>();
		}

		public override bool Transition(Ikinesis kinesis) => animEnd;

		public override void Startkinesis() => animator.Transition(animName);

		public override void AnimationEnd() => animEnd = true;
	}
}
