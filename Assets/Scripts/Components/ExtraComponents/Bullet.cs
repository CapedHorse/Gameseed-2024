using Components.Objects;
using UnityEngine;

namespace Components.ExtraComponents
{
    public class Bullet : ObjectBase
    {
        protected override void EnteredTrigger(Collider2D other)
        {
            base.EnteredTrigger(other);
        }
    }
}