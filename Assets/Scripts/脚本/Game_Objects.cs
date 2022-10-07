using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Objects : MonoBehaviour
{
   public static Game_Objects instance;
    [SerializeField]
    public  GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
