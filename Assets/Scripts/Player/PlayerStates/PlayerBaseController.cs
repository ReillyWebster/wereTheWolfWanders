using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBaseController
{
    public abstract void EnterState(PlayerControllerStateManager player);
    public abstract void UpdateState(PlayerControllerStateManager player);
}
