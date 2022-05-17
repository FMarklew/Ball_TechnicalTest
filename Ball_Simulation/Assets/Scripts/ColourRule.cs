using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BallGame
{
    /// <summary>
    /// Class to hold a rule containing a colour to switch to and the multiples required
    /// </summary>
    [System.Serializable]
    public class ColourRule
    {
        [Tooltip("The colour to change to if the rule is satisfied")]
        public Color colourToChangeTo;
        [Tooltip("Every multiple required to satisfy the rule")]
        public int[] allRequiredMultiples = new int[0];
        public bool DoesRuleApply(int num)
        {
            foreach (int i in allRequiredMultiples)
            {
                if (num % i != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}