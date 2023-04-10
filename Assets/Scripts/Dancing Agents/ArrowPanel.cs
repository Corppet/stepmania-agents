using System;
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

        public void HoverEnter(AgentFoot foot)
        {
            foot.DropFoot += Press;
            foot.HoldFoot += Hold;
            foot.LiftFoot += Release;
        }

        public void HoverExit(AgentFoot foot)
        {
            foot.DropFoot -= Press;
            foot.HoldFoot -= Hold;
            foot.LiftFoot -= Release;
        }

        private void Press()
        {
            InputHandler.Instance.InputPress(direction);
        }

        private void Hold()
        {
            InputHandler.Instance.InputHeld(direction);
        }

        private void Release()
        {
            InputHandler.Instance.InputRelease(direction);
        }
    }
}
