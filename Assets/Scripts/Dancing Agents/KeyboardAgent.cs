using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace DancingAgents
{
    public class KeyboardAgent : Agent
    {
        public static KeyboardAgent Instance { get; private set; }

        [Header("Agent Settings")]
        public bool isTraining;
        [Range(0, 100)]
        [Tooltip("How many notes the agent will observe at once?")]
        public int lookAheadSteps = 4;

        [Space(5)]

        [Header("Reward Settings")]
        public float arrowReward = 1f; // reward for pressing an arrow correctly
        public float missPenalty = -1f; // penalty for missing an arrow
        public float scoreReward = 0.1f; // reward for increasing the score
        public float perfectReward = 0.5f; // reward for achieving a perfect score
        public float timeReward = 1f; // reward for completing the song

        private ActionSegment<int> prevDiscretes;

        private AudioSource[] audioSources;

        public override void Initialize()
        {
            if (isTraining)
            {
                audioSources = FindObjectsOfType<AudioSource>();
                Debug.Log("Found " + audioSources.Length + " audio sources");
                foreach (AudioSource source in audioSources)
                {
                    source.pitch = Time.timeScale;
                }
            }
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            foreach (NoteData data in LookAheadArrows)
            {
                sensor.AddObservation(data.distance);
                sensor.AddObservation((int)data.direction);
                sensor.AddObservation((int)data.type);
            }
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            InputHandler input = InputHandler.Instance;
            ActionSegment<int> keysPressed = actions.DiscreteActions;
            
            // Determine type of input for each direction
            for (int i = 0; i < 4 && i < keysPressed.Length;  i++)
            {
                switch (keysPressed[i])
                {
                    case 0:
                        switch (prevDiscretes[i])
                        {
                            case 0: // key not pressed
                                break;
                            default: // key pressed
                                input.InputPress((Directions)i);
                                break;
                        }
                        break;
                    default:
                        switch (prevDiscretes[i])
                        {
                            case 0: // key released
                                input.InputRelease((Directions)i); 
                                break;
                            case 1: // key held
                                input.InputHeld((Directions)i);
                                break;
                        }
                        break;
                }
            }
        }

        public override void OnEpisodeBegin()
        {
            int[] startActions = { 0, 0, 0, 0 };
            prevDiscretes = new(startActions);

            GameManager.Instance.RestartGame();
        }

        public void AddJudgement(float noteScore, Judgements judgement)
        {
            switch (judgement)
            {
                case Judgements.miss:
                    AddReward(-noteScore);
                    break;
                default:
                    AddReward(noteScore); 
                    break;
            }
        }

        public void FinishSong()
        {
            EndEpisode();
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

                    float dist = Mathf.Abs(note.transform.position.y
                        - GameManager.Instance.Receptors[note.Direction].transform.position.y);
                    result[activeArrowsFound] = new NoteData
                    {
                        distance = dist,
                        direction = note.Direction,
                        type = note.Type
                    };
                    activeArrowsFound++;
                }
                Debug.Log("Read ahead " + result.Length + " notes.");

                return result;
            }
        }

        protected struct NoteData
        {
            /// <summary>
            /// Distance from the respective receptor.
            /// </summary>
            public float distance;

            /// <summary>
            /// The direction of the note's arrow.
            /// </summary>
            public Directions direction;

            /// <summary>
            /// The type of note.
            /// </summary>
            public NoteTypes type;
        }
    }
}