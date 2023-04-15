using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace DancingAgents
{
    public class DancingAgent : Agent
    {
        [Header("Agent Settings")]
        [Range(0, 100)]
        [Tooltip("How many notes the agent will observe at once?")]
        public int lookAheadSteps = 4;

        [Space(5)]

        [Header("Foot Settings")]
        public float maxSpeed = 50f;
        [Range(0f, 720f)]
        public float maxAngularSpeed = 50f;

        [Space(10)]

        [Header("References")]
        [SerializeField] private DancePadController m_controller;
        [SerializeField] private AgentFoot m_leftFoot;
        [SerializeField] private AgentFoot m_rightFoot;

        // Reset Parameters
        private Vector3 m_initPos;
        private Vector3 m_leftFootInitPos;
        private Vector3 m_rightFootInitPos;
        private Quaternion m_initRot;
        private Quaternion m_leftFootInitRot;
        private Quaternion m_rightFootInitRot;

        public override void Initialize()
        {
            m_initPos = transform.position;
            m_initRot = transform.rotation;
            m_leftFootInitPos = m_leftFoot.transform.position;
            m_leftFootInitRot = m_leftFoot.transform.rotation;
            m_rightFootInitPos = m_rightFoot.transform.position;
            m_rightFootInitRot = m_rightFoot.transform.rotation;
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            // Left foot position and rotation
            sensor.AddObservation(new Vector2
            {
                x = m_leftFoot.transform.position.x - m_initPos.x,
                y = m_leftFoot.transform.position.y - m_initPos.y
            });
            sensor.AddObservation(m_leftFoot.transform.eulerAngles.z);

            // Right foot position and rotation
            sensor.AddObservation(new Vector2
            {
                x = m_rightFoot.transform.rotation.x - m_initPos.x,
                y = m_rightFoot.transform.rotation.y - m_initPos.y
            });
            sensor.AddObservation(m_rightFoot.transform.eulerAngles.z);

            // Look-ahead arrow position
            foreach (NoteData arrow in LookAheadArrows)
            {
                
            }
        }

        public override void OnEpisodeBegin()
        {
            transform.position = m_initPos;
            transform.rotation = m_initRot;
            m_leftFoot.transform.position = m_leftFootInitPos;
            m_leftFoot.transform.rotation = m_leftFootInitRot;
            m_rightFoot.transform.position = m_rightFootInitPos;
            m_rightFoot.transform.rotation = m_rightFootInitRot;
        }

        private NoteData[] LookAheadArrows
        {
            get
            {
                int activeArrowsFound = 0;
                List<Note> noteList = GameManager.Instance.AllNotes;
                NoteData[] result = new NoteData[lookAheadSteps];
                for (int i = 0; i < noteList.Count && activeArrowsFound < lookAheadSteps; i++)
                {
                    Note note = noteList[i];
                    if (!note.IsActive)
                        continue;

                    float notePos = Mathf.Abs(note.transform.position.y 
                        - GameManager.Instance.Receptors[note.Direction].transform.position.y);
                    result[activeArrowsFound] = new NoteData
                    {
                        position = notePos,
                        direction = note.Direction,
                        type = note.Type
                    };
                    activeArrowsFound++;
                }

                return result;
            }
        }

        public struct NoteData
        {
            public float position;
            public Directions direction;
            public NoteTypes type;
        }
    }
}
