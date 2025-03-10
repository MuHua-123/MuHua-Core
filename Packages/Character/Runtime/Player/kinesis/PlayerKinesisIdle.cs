using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	public class PlayerKinesisIdle : Ikinesis {
		public override string AnimName => "Idle";
		public override bool Interrupt => true;
	}
}