using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Upgradebehaviours
{
    public class UpgradeBehaviour : MonoBehaviour
    {
        protected int level { get; private set; }

        public void LevelUp()
        {
            level++;
        }
    }
}