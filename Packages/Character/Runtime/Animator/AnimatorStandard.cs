using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class AnimatorStandard : IAnimator {
		private string layer;
		private string current;
		private Animator animator;

		private void Awake() => animator = GetComponent<Animator>();

		public override void Transition(string name) {
			if (current == name) { animator.Play(name); }
			else { animator.CrossFade(name, 0.1f); }
			current = name;

			// 判断当前动画，如果不是相同动画就增加过渡
			// AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
			// Debug.Log($"{currentState.normalizedTime} , {this.kinesis.AnimName}");
		}

		public override void SetFloat(string name, float value) => animator.SetFloat(name, value);
	}
}