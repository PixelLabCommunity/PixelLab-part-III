// 2024-05-15 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product.
using System;
using Unity.Muse.Behavior;
using Action = Unity.Muse.Behavior.Action;
using UnityEngine;

[Serializable]
[NodeDescription(name: "Self move to Target", story: "Agent attack Agent", category: "Action", id: "4ad18d945476d5c098295999ff5784bc")]
public class SelfMoveToTarget : Action
{
    private BlackboardVariable<GameObject> self;
    private BlackboardVariable<GameObject> target;
    private BlackboardVariable<float> speed;

    protected override Status OnStart()
    {
        if (self.Value == null || target.Value == null)
        {
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        try
        {
            float step = speed.Value * Time.deltaTime;
            self.Value.transform.position = Vector3.MoveTowards(self.Value.transform.position, target.Value.transform.position, step);

            if (Vector3.Distance(self.Value.transform.position, target.Value.transform.position) < 0.001f)
            {
                return Status.Success;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        self.Value = null;
        target.Value = null;
    }
}