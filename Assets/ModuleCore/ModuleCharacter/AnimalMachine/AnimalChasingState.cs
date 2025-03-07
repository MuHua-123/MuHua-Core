using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class AnimalChasingState<T> : MachineState where T : Component {
    public T target;
    public string ToDefault = "Eating";
    public readonly AnimalMachine animal;
    public AnimalChasingState(Machine machine) : base(machine) => animal = machine as AnimalMachine;

    public override void Enter() {
        bool valid = animal.Find(out target);
        if (!valid) { animal.ChangeState("Idle"); }
    }

    public override void Exit() {
        animal.movement.StopMoving();
        animal.animator.SetFloat("MoveSpeed", 0);
    }

    public override void Trigger() {

    }

    public override void Update() {
        if (target == null) { animal.ChangeState(ToDefault); return; }
        bool complete = animal.movement.UpdateMove(target.transform.position);
        animal.animator.SetFloat("MoveSpeed", animal.movement.currentSpeed);
        if (complete) { animal.ChangeState(ToDefault); }
    }
}
