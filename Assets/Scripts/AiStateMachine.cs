using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateMachine : MonoBehaviour
{
    public AiState[] states;
    public AiAgent agent;
    public AiStateId currentState;
    public AiStateMachine(AiAgent agent)
    {
        this.agent = agent;
        int numState = System.Enum.GetNames(typeof(AiStateId)).Length;
        states = new AiState[numState];
    }
    public void RegisterState(AiState state)
    {
        int index = (int)state.GetId();
        states[index] = state; 
    }
  
    public AiState GetState(AiStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }
    // Update is called once per frame
    public void Update()
    {
        GetState(currentState)?.Update(agent);
    }
    public void ChangeState(AiStateId newState)
    {
        GetState(currentState)?.Exit(agent);
        currentState = newState;
        GetState(currentState)?.Enter(agent);
    }
}
