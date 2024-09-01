using Components.Objects;
using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.ExtraComponents
{
    public class HurtingComponent : ComponentBase
    {
        protected override void EnteredTrigger(Collider2D other)
        {
            //teporary, should've make the player lose if this grabable can hurt
            ClawMachineControl clawMachine = other.GetComponent<ClawMachineControl>();
            if (clawMachine != null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}