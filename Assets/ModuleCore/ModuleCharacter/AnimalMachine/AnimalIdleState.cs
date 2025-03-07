using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class AnimalIdleState : MachineState {
    public string ToDefault = "Roaming";

    private float idleTime;

    public AnimalMachine animal;
    public AnimalIdleState(Machine machine) : base(machine) => animal = machine as AnimalMachine;

    public override void Enter() {
        idleTime = Random.Range(3.0f, 5.0f);
    }

    public override void Exit() {

    }

    public override void Trigger() {

    }

    public override void Update() {
        idleTime -= Time.deltaTime;
        if (idleTime <= 0) { animal.ChangeState(ToDefault); }
    }
}
