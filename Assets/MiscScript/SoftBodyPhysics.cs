using System;
using UnityEngine;

namespace MiscScript
{
    [System.Serializable]
    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public class SoftBodyPhysics : MonoBehaviour
    {
        [Header("Settings")]
        [Range(0f, 2f)]
        public float softness = 1f;
        [Range(0.01f, 1f)]
        public float damping = 0.1f;
        public float stiffness = 1f;
        public float collisionDistance = 0.01f;

        private void Start()
        {
            CreateSoftBody();
        }

        private void CreateSoftBody()
        {
            SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
            
            Cloth cloth = gameObject.AddComponent<Cloth>();
            cloth.damping = damping;
            cloth.bendingStiffness = stiffness;

            cloth.coefficients = GenerateClothesCoefficients(skinnedMeshRenderer.sharedMesh.vertices.Length);
        }

        private ClothSkinningCoefficient[] GenerateClothesCoefficients(int vertexCount)
        {
            ClothSkinningCoefficient[] coefficients = new ClothSkinningCoefficient[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                coefficients[i].maxDistance = softness;
                coefficients[i].collisionSphereDistance = collisionDistance;
            }
            
            return coefficients;
        }
    }
}