using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DancingAgents
{
    public class ArrowPanel : MonoBehaviour
    {
        [HideInInspector] public DancePadController Controller { private get; set; }

        public Directions direction;

        [Header("References")]
        [SerializeField] private Sprite m_litSprite;
        [SerializeField] private Sprite m_unlitSprite;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Foot"))
            {
                InputHandler.Instance.InputPress(direction);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Foot"))
            {
                InputHandler.Instance.InputRelease(direction);
            }
        }
    }
}
