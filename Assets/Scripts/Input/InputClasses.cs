using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Input
{
    
    [Serializable]
    public class InputMapWrapper
    {
        public string mapName;
        public List<InputActionWrapper> actionList;
    }

    [Serializable]
    public class InputActionWrapper
    {
        public string actionName;
        public InputActionReference action;
    }
}