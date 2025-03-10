using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class PlayerKinesisMove : Ikinesis {
		public Vector3 position;
		public Movement movement;
		public Animator animator;
		public PlayerCharacter character;
		public Ikinesis transition = new PlayerKinesisIdle();

		public override string AnimName => "Move";
		public override bool Interrupt => true;

		public PlayerKinesisMove(Vector3 position, PlayerCharacter character) {
			this.position = position;
			this.movement = character.movement;
			this.animator = character.animator;
			this.character = character;
		}

		public override void Update() {
			if (movement.UpdateMove(position)) { character.Updatekinesis(transition); }

			animator.SetFloat("MoveSpeed", movement.CurrentSpeed);
			animator.SetFloat("MoveX", movement.Direction.x);
			animator.SetFloat("MoveZ", movement.Direction.z);
		}
	}
}
