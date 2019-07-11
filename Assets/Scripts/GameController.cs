using UnityEngine;
using System.Collections;
using RpgDB;

namespace AncientArmory
{
    public sealed class GameController : MonoBehaviour
    {
        [HideInInspector] public GameDatabase GameDatabase;

        void Awake() {
            // GameDatabase = GameObject.Find("GameDatabase").GetComponent<GameDatabase>();
        }

        void Start() {
            // 
        }
    }
}