using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace DancingAgents
{
    public class DancingAgent : Agent
    {
        [SerializeField] private References m_references;

        public override void Initialize()
        {
            
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            
        }

        public struct References
        {
            public DancePadController controller;
        }
    }
}
