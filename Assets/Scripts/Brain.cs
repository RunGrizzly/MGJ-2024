using UnityEngine;

public class Brain : MonoBehaviour
{


    public static Brain ins = null;


    [field: SerializeField]
    public SceneHandler SceneHandler { get; set; } = null;

    [field: SerializeField]
    private EventHandler EventHandler { get; set; } = null;



    private void Awake()
    {
        ins = this;
    }

}
