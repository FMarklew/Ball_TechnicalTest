using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BallGame
{
    /// <summary>
    /// Class to handle the ball spawning and logic around rules
    /// </summary>
    public class BallSpawner : MonoBehaviour
    {
        //TODO: allow for the balls to be staggered over seconds if performance becomes an issue (Switch to Coroutine)

        [Tooltip("Base ball prefab")]
        public Ball ballPrefab; // prefab for the ball

        [Tooltip("Amount of balls to spawn")]
        public int amtBallsToSpawn;

        [Tooltip("This number will be used as the negative min and positive max for the x axis randomization")]
        public float maxSpawnDistX;
        [Tooltip("This number will be used as the negative min and positive max for the z axis randomization")]
        public float maxSpawnDistZ;

        [Tooltip("These rules should be ordered, the first which is true will be accepted")]
        public List<ColourRule> allRules = new List<ColourRule>();

        [Tooltip("Material for balls which do not satisfy any rules")]
        public Material baseMaterial;
        private List<Material> allMats = new List<Material>(); // caching mats so we don't make a new material per ball

        void Start()
        {
            if (baseMaterial == null)
            {
                Debug.LogError("Base material must be populated");
                return;
            }

            for (int i = 0; i < amtBallsToSpawn; i++)
            {
                bool colourSet = false; // for the case where a ball satisfies no rules
                float randX = Random.Range(-maxSpawnDistX, maxSpawnDistX);
                float randZ = Random.Range(-maxSpawnDistZ, maxSpawnDistZ);

                Ball b = Instantiate(ballPrefab, new Vector3(randX, transform.position.y, randZ), Quaternion.identity, transform); // create and child a ball to this object
                int num = i + 1;
                b.gameObject.name = num.ToString();
                b.SetText(num.ToString());

                foreach (ColourRule rule in allRules) // check all rules in order
                {
                    if (rule.DoesRuleApply(num)) // check the rule's multiples against num
                    {
                        Material m = allMats.Find(x => x.color == rule.colourToChangeTo);
                        if (m == null)
                        {
                            m = new Material(baseMaterial);
                            m.color = rule.colourToChangeTo;
                            allMats.Add(m);
                        }
                        b.SetMaterial(m);
                        colourSet = true;
                        break; // if a rule is satisfied, break the loop
                    }
                }

                if (!colourSet)
                {
                    b.SetMaterial(baseMaterial);
                }
            }
        }


        private void OnDrawGizmos()
        {
            // this is to display the spawn bounding box for balls
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(2f * maxSpawnDistX, 0.15f, 2f * maxSpawnDistZ));
        }
    }
}