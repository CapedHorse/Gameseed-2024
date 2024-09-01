using UnityEngine;

namespace Components.Objects
{
    public class TargeterComponent : ComponentBase
    {
        [SerializeField] private string targeterID;

        public string TargeterID => targeterID;
    }
}