using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace DancingAgents
{
    public class KeyboardAgent : Agent
    {
        [Header("Agent Settings")]
        [Range(0, 100)]
        [Tooltip("How many notes the agent will observe at once?")]
        public int lookAheadSteps = 4;

        public override void Initialize()
        {

        }
        public override void CollectObservations(VectorSensor sensor)
        {

        }

        public override void OnEpisodeBegin()
        {
            
        }
    }
}