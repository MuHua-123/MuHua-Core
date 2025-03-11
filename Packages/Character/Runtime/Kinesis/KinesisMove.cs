using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class KinesisMove : Ikinesis {
		public Vector3 position;
		public IMovement movement;
		public IAnimator animator;
		public ICharacter character;

		public KinesisMove(ICharacter character, Vector3 position) {
			this.position = position;
			this.character = character;
			movement = character.GetComponent<IMovement>();
			animator = character.GetComponent<IAnimator>();
		}

		public override bool Transition(Ikinesis kinesis) => true;

		public override void Startkinesis() => animator.Transition("Move");

		public override void Update() {
			if (movement.UpdateMove(position, 5)) { character.Transitionkinesis(new KinesisIdle(character)); }

			animator.SetFloat("MoveSpeed", movement.CurrentSpeed);
			animator.SetFloat("MoveX", movement.Direction.x);
			animator.SetFloat("MoveZ", movement.Direction.z);
		}
	}
}
