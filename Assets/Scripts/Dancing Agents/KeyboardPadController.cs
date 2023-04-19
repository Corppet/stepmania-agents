using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingAgents
{
    public class KeyboardPadController : MonoBehaviour
    {
        private Dictionary<Directions, KeyPanel> m_panels;

        private float m_initialY;

        public void PressKey(Directions direction)
        {
            InputHandler input = InputHandler.Instance;

            if (m_panels[direction].IsPressed) // key already pressed, trigger hold
            {
                input.InputHeld(direction);
            }
            else // key not pressed, trigger press
            {
                input.InputPress(direction);
                m_panels[direction].IsPressed = true;
            }
        }

        public void ReleaseKey(Directions direction)
        {
            m_panels[direction].IsPressed = false;
            InputHandler.Instance.InputRelease(direction);
        }

        private void Awake()
        {
            KeyPanel[] panels = GetComponentsInChildren<KeyPanel>();
            m_panels = new();
            foreach (KeyPanel panel in panels)
            {
                panel.Controller = this;
                m_panels.Add(panel.direction, panel);
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
