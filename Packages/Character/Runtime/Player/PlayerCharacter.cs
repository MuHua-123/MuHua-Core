using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class PlayerCharacter : ICharacter {
		public Animator animator;
		public Movement movement;

		private void Awake() => kinesis = new PlayerKinesisIdle();

		public override void Updatekinesis(Ikinesis kinesis) {
			if (!this.kinesis.Interrupt) { return; }
			this.kinesis = kinesis;

			//播放动画
			animator.Play(this.kinesis.AnimName);
		}
	}
}