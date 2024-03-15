using UnityEngine;

public class Brain : MonoBehaviour
{

    [field: SerializeField]
    public SceneHandler SceneHandler { get; set; } = null;

    [field: SerializeField]
    private EventManager EventManager { get; set; } = null;
}
