using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

// public class AnimalRoamingState : IStateMachine {
//     public Vector3 targetPosition;
//     public string ToDefault = "Idle";
//     public readonly AnimalMachine animal;
//     public AnimalRoamingState(StateMachine machine) : base(machine) => animal = machine as AnimalMachine;

//     public override void Enter() {
//         targetPosition = animal.movement.RandomTargetPosition();
//     }

//     public override void Exit() {
//         animal.movement.StopMoving();
//         animal.animator.SetFloat("MoveSpeed", 0);
//     }

//     public override void Trigger() {

//     }

//     public override void Update() {
//         bool complete = animal.movement.UpdateMove(targetPosition);
//         animal.animator.SetFloat("MoveSpeed", animal.movement.currentSpeed);
//         if (complete) { animal.ChangeState(ToDefault); }
//     }
// }
