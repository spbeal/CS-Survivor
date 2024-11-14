using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSoundEffect()
    {
        /*
        lightGameObject = new GameObject("SingleLight");
        lightComp = lightGameObject.AddComponent<Light>();
        */
    }
}

// incomplete factory pattern to finish later... should be useful for spawning sound effects but I don't want to worry about whether it fits a pattern described on a website
// so I'll be implementing it in the best way possible without worrying about the "correctness" of the pattern
/*
public class AudioFactory
{

}

public class SoundEffect
{
    private GameObject soundEffect;

}

public class GunFiringSoundEffect : SoundEffect
{

}

public class DyingSoundEffect : SoundEffect
{

}

public class GettingHurtSoundEffect : SoundEffect
{

}
*/