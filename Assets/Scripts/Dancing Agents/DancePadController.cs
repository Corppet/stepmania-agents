using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingAgents
{
    public class DancePadController : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("The parent of the agent's feet.")]
        [SerializeField] private Transform agentParent;

        private ArrowPanel[] m_panels;
        private AgentFoot[] m_agentFeet;

        private float m_initialY;
        private float m_agentInitialY;

        private void Awake()
        {
            m_panels = GetComponentsInChildren<ArrowPanel>();
            foreach (ArrowPanel panel in m_panels)
            {
                panel.Controller = this;
            }

            m_agentFeet = agentParent.GetComponentsInChildren<AgentFoot>();

            m_initialY = transform.position.y;
            m_agentInitialY = agentParent.position.y;
        }

        private void Start()
        {
            // Set the initial position of the pad

            switch (Modifications.Instance.ScrollDirection)
            {
                case ScrollDirection.down:
                    break;
                case ScrollDirection.up:
                    m_initialY *= -1;
                    m_agentInitialY *= -1;
                    break;
            }

            transform.position = new Vector3
            {
                x = transform.position.x,
                y = m_initialY + (Modifications.Instance.ReceptorHeight * 0.5f),
                z = transform.position.z
            };

            agentParent.position = new Vector3
            {
                x = agentParent.position.x,
                y = m_agentInitialY + (Modifications.Instance.ReceptorHeight * 0.5f),
                z = agentParent.position.z
            };
        }
    }
}
