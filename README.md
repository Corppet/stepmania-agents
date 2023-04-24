# StepMania Agents

StepMania Agents is a machine learning project focused on training agents to play [StepMania](https://github.com/stepmania/stepmania), 
an open-source rhythm game where players need to make keyboard inputs in a specific sequence. The project utilizes a clone of 
StepMania recreated in Unity and utilizes Unity ML Agents to train machine learning agents in the editor. Work for the project was 
done as part of the final project for the COGS-4420 Game AI course at Rensselaer Polytechnic Institute.

# Credits
 - Stepmania clone environment (a.k.a. UnityMania) by [Kaj Rumpff](https://github.com/rumpff/unitymania)
 - Machine Learning implementation and training by [Ivan Ho](https://github.com/Corppet)

# Installation

StepMania Agents requires Unity [2021.3.11f1](https://unity.com/releases/editor/whats-new/2021.3.11) and utilizes Unity 
[ML Agents](https://github.com/Unity-Technologies/ml-agents) for training.

# Running the Project

ML Agents configuation files and results can be found in `./ML Agents/config`. 
More infomation about using Unity ML Agents can be found on the package's [official documentation](https://github.com/Unity-Technologies/ml-agents/blob/develop/docs/Training-ML-Agents.md).

**Training** 
  1. Navigate to `gameScene` in the Unity editor (located in `./Assets/Scenes`).
  2. Select the `Keyboard Pad` game object, and under the `Keyboard Agent` component in the inspector, enable the `Is Training` option.
  3. Navigate to `songSelectScene` and run ML Agents with your Python environment in `./ML Agents/config` with the following command:
```sh
mlagents-learn keyboard.yaml --run-id=<run id>
```
  4. Play the `songSelectScene` and select a song and difficulty to train the agent on.

**Running an Existing Model**
  1. Navigate to `gameScene` in the Unity editor (located in `./Assets/Scenes`).
  2. Select the `Keyboard Pad` game object...
     a. Under the `Keyboard Agent` component in the inspector, disable the `Is Training` option.
     b. Under the `Behavior Parameters` component in the inspector, select the `Model` option and select the model you wish to run.
        Some models are provided in `./Assets/ML-Agents/Models`.
  3. Navigate to `songSelectScene` and play the scene to select a song and difficulty to run the agent on.
