using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

// public class AnimalEatingState : IStateMachine {
//     public string ToDefault = "Idle";

//     private AnimalFood target;

//     public AnimalMachine animal;
//     public AnimalEatingState(StateMachine machine) : base(machine) => animal = machine as AnimalMachine;

//     public override void Enter() {
//         if (!animal.Find(out target)) { Exit(); return; }

//         // 判断target距离是否小于0.3f
//         float distance = Vector3.Distance(animal.transform.position, target.transform.position);
//         if (distance >= 0.1f) { Exit(); return; }

//         animal.animator.SetBool("Eating", true);
//     }

//     public override void Exit() {
//         animal.animator.SetBool("Eating", false);
//     }

//     public override void Trigger() {
//         animal.hunger += target.nutritionValue;
//         animal.hunger = Mathf.Clamp(animal.hunger, 0, animal.maxHunger);
//         GameObject.Destroy(target.gameObject);

//         animal.ChangeState(ToDefault);
//     }

//     public override void Update() {

//     }
// }
