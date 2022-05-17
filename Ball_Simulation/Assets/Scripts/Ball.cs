using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BallGame
{
    /// <summary>
    /// Class to handle all ball-specific logic, such as setting colour, text, and bounciness
    /// </summary>
    public class Ball : MonoBehaviour
    {
        private const float maxVelocity = 20f;

        [SerializeField]
        private MeshRenderer sphere_renderer;

        [SerializeField]
        private TextMeshPro text_mesh;

        [SerializeField]
        private Rigidbody sphereRigidBody;

        [SerializeField]
        [Range(0, maxVelocity)]
        private float minVelocityOnImpact;

        [SerializeField]
        [Range(0, maxVelocity)]
        private float maxVelocityOnImpact;

        void Start()
        {
            sphere_renderer ??= GetComponentInChildren<MeshRenderer>();
            text_mesh ??= GetComponentInChildren<TextMeshPro>();
            sphereRigidBody ??= GetComponentInChildren<Rigidbody>();
        }

        public void SetText(string s)
        {
            text_mesh.text = s;
        }

        public void SetMaterial(Material m)
        {
            sphere_renderer.material = m;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 6) // NOTE: could use a layermask if desired here - this works for convenience
            {
                float rVelocity = Random.Range(minVelocityOnImpact, maxVelocityOnImpact);
                sphereRigidBody.velocity = new Vector3(0, rVelocity, 0);
            }
        }
    }
}