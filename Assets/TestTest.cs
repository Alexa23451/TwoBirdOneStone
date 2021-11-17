using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestTest : MonoBehaviour
{
    private float XLimit = 8f, YLimit = 5f;
    Queue<AIState> aIStates = new Queue<AIState>();
    AIState curState;

    private void Start()
    {
        aIStates.Enqueue(new MoveState(new Vector3(XLimit, YLimit, 0)));
        aIStates.Enqueue(new MoveState(new Vector3(-XLimit, -YLimit, 0)));
    }

    void ChangeState()
    {
        if (aIStates.Count != 0)
        {
            curState = aIStates.Dequeue();
            aIStates.Enqueue(new MoveState(new Vector3(Random.value * XLimit, Random.value * YLimit, 0)));
        }
    }

    

    private void Update()
    {
        if (curState)
        {
            curState.Doing();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState();
        }
    }

}

public abstract class AIState : MonoBehaviour
{
    public abstract void Doing();
}

public class MoveState : AIState
{
    Vector3 destination;


    public MoveState(Vector3 des)
    {
        destination = des;
    }

    public override void Doing()
    {
        transform.DOMove(destination, 2f);
    }
}



