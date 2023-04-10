using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingAgents
{
    public enum FootType { left, right }

    public class AgentFoot : MonoBehaviour
    {
        #region Delegates and Events

        [HideInInspector] public delegate void FootDown();
        [HideInInspector] public delegate void FootHold();
        [HideInInspector] public delegate void FootUp();

        [HideInInspector] public event Action DropFoot;
        [HideInInspector] public event Action HoldFoot;
        [HideInInspector] public event Action LiftFoot;

        #endregion

        [Header("Foot Settings")]
        public float maxSpeed = 50f;
        [Range(0f, 720f)]
        public float maxAngularSpeed = 50f;
        public FootType footType { get; private set;}

        /// <summary>
        /// Tracks which arrow panels the foot is currently hovering over.
        /// </summary>
        private List<ArrowPanel> m_hoveringPanels;

        /// <summary>
        /// Tracks whether the foot is currently in the air.
        /// </summary>
        private bool m_isInAir;

        private Rigidbody2D m_rigidbody;

        public void TransformFoot(Vector2 destination, Quaternion rotation)
        {
            
        }

        #region Event Raisers

        public void OnDropFoot()
        {
            if (!m_isInAir)
                return;

            DropFoot?.Invoke();
            m_isInAir = false;
        }
        private void OnHoldFoot()
        {
            if (m_isInAir)
                return;

            HoldFoot?.Invoke();
        }

        public void OnLiftFoot()
        {
            if (m_isInAir)
                return;

            LiftFoot?.Invoke();
            m_isInAir = true;
        }

        #endregion

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();

            m_hoveringPanels = new();
            m_isInAir = true;
        }

        private void FixedUpdate()
        {
            OnHoldFoot();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Arrow Panel"))
            {
                ArrowPanel arrow = collision.GetComponent<ArrowPanel>();
                arrow.HoverEnter(this);
                m_hoveringPanels.Add(arrow);

                Debug.Log("Hovering over " + arrow.direction);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Arrow Panel"))
            {
                ArrowPanel arrow = collision.GetComponent<ArrowPanel>();
                arrow.HoverExit(this);
                m_hoveringPanels.Remove(arrow);

                Debug.Log("No longer hovering over " + arrow.direction);
            }
        }
    }
}
