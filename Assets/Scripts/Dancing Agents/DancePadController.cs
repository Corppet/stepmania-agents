using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingAgents
{
    public class DancePadController : MonoBehaviour
    {
        [SerializeField] private AgentFoot[] m_feet;

        private ArrowPanel[] m_panels;

        private float m_initialY;

        private void Awake()
        {
            m_panels = GetComponentsInChildren<ArrowPanel>();
            foreach (ArrowPanel panel in m_panels)
            {
                panel.Controller = this;
            }

            m_initialY = transform.position.y;
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
                    break;
            }

            transform.position = new Vector3
            {
                x = transform.position.x,
                y = m_initialY + (Modifications.Instance.ReceptorHeight * 0.5f),
                z = transform.position.z
            };
        }
    }
}
