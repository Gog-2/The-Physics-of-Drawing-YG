using UnityEngine;

public class TaskConnect : MonoBehaviour
{
    static public TaskConnect instance;

    private void Awake()
    {
        instance = this;
    }
}
