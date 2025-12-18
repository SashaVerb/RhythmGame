using UnityEngine.InputSystem;

namespace StateManaging
{
    public class PressKeyCondition : ICondition
    {
        private readonly InputAction inputAction;

        public PressKeyCondition(InputAction inputAction)
        {
            this.inputAction = inputAction;
        }

        public bool Check()
        {
            return inputAction.WasPressedThisFrame();
        }
    }
}
