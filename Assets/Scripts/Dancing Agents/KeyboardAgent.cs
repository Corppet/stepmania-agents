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
            GameManager.Instance.RestartGame();
        }

        protected NoteData[] LookAheadArrows
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

        protected struct NoteData
        {
            public float position;
            public Directions direction;
            public NoteTypes type;
        }
    }
}