using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AncientArmory
{
    public class ArmoryController : MonoBehaviour
    {
        void OffenseMovement(GameObject characterObject)
        {
            // 
        }

        void ResolveAttack(Character attacker, Character defender)
        {
            int defender_health = defender.Hit_Points() - defender.Damage_Taken;
            if (defender_health > 0)
            {
                int damage = resolveSingleOrDualWeildAttack(attacker, defender.KAC());
                Debug.Log(String.Format("Attack causes {1} damage!", attacker.Armor.Name, damage.ToString()));
                defender.Damage_Taken += damage;
                defender_health = defender.Hit_Points() - defender.Damage_Taken;
                Debug.Log(String.Format("Defender has {0} health remaining!", defender_health.ToString()));
            }
        }
    }

}
