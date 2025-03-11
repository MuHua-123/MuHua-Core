using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class CharacterPlayer : ICharacter {

		private void Awake() => Transitionkinesis(new KinesisIdle(this));

		public override void Transitionkinesis(Ikinesis kinesis) {
			if (currentKinesis != null && !currentKinesis.Transition(kinesis)) { return; }
			currentKinesis = kinesis;
			currentKinesis.Startkinesis();
		}
	}
}