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
        [Header("Foot Settings")]
        public float maxSpeed = 50f;
        [Range(0f, 720f)]
        public float maxAngularSpeed = 50f;

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

        public override void Heuristic(in ActionBuffers actionsOut)
        {
            
        }
    }
}
