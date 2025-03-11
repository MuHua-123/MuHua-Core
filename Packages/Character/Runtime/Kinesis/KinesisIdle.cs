using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class KinesisIdle : Ikinesis {
		public IAnimator animator;

		public KinesisIdle(ICharacter character) => animator = character.GetComponent<IAnimator>();

		public override bool Transition(Ikinesis kinesis) => true;

		public override void Startkinesis() => animator.Transition("Idle");
	}
}