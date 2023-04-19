using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingAgents
{
    public class KeyPanel : MonoBehaviour
    {
        [HideInInspector] public KeyboardPadController Controller { private get; set; }

        public Directions direction;

        private bool m_isPressed;
        public bool IsPressed
        {
            get => m_isPressed;
            set
            {
                m_isPressed = value;
                m_renderer.sprite = value ? m_litSprite : m_unlitSprite;
            }
        }

        [Header("References")]
        [SerializeField] private Sprite m_litSprite;
        [SerializeField] private Sprite m_unlitSprite;

        private SpriteRenderer m_renderer;

        private void Awake()
        {
            m_renderer = GetComponent<SpriteRenderer>();
        }
    }
}
