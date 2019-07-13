using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientArmory
{
    public class ArmoryController : ControllerBase
    {
        [Header("---Armory Controller---")]
        [SerializeField]
        private MercController[] mercArray;
        public List<GameObject> Scavengers;
    }

}
