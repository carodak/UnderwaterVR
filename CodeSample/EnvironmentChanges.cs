using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class that will perform changes in the environment by pressing keys
public class EnvironmentChanges : MonoBehaviour {

    public GameObject music; //main music with properties to change
    public GameObject spirit; //gameobject that does spirit effects
    public GameObject light; //main light with properties to change
    public GameObject magic; //gameobject that does magic effects

    public float timer = 2f; // timer for each change (in sec)

	
	// Update is called once per frame
	void Update ()
    {
        CheckInput();

    }

    //Check which key was pressed
    public void CheckInput()
    {
        if (Input.GetKeyDown("t"))
        {
            IncreaseMusicVolume();
        }
        else if (Input.GetKeyDown("y"))
        {
            DecreaseMusicVolume();
        }
        else if(Input.GetKeyDown("u"))
        {
            EnableSpirits();
        }
        else if(Input.GetKeyDown("i"))
        {
            DisableSpirits();
        }
        else if(Input.GetKeyDown("o"))
        {
            SetBluerLight();
        }
        else if(Input.GetKeyDown("p"))
        {
            SetLigherLight();
        }
        else if(Input.GetKeyDown("j"))
        {
            EnableMagic();
        }
        else if(Input.GetKeyDown("k"))
        {
            DisableMagic();
        }
    }

    //Increase the music of the environment
    void IncreaseMusicVolume()
    {
        StartCoroutine(_IncreaseMusicVolume());
    }

    //Decrease the music of the environment
    void DecreaseMusicVolume()
    {
        StartCoroutine(_DecreaseMusicVolume());
    }

    //Make the light bluer
    void SetBluerLight()
    {
        StartCoroutine(_SetBluerLight());
    }

    //Make the light ligher
    void SetLigherLight()
    {
        StartCoroutine(_SetLigherLight());
    }

    //Enable spirits effect
    void EnableSpirits()
    {
        spirit.SetActive(true);
        ParticleSystem ps = spirit.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = true;
    }

    //Disable spirits effect
    void DisableSpirits()
    {
        ParticleSystem ps = spirit.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = false;
    }

    //Enable magic effects
    void EnableMagic()
    {
        ParticleSystem ps = magic.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = true;

        SetBluerLight();

        EnableSpirits();
    }

    //Disable magic effects
    void DisableMagic()
    {
        ParticleSystem ps = magic.GetComponent<ParticleSystem>();
        var emission = ps.emission;
        emission.enabled = false;

        SetLigherLight();

        DisableSpirits();
    }

    //Increase the Volume of the music by 0.1f
    IEnumerator _IncreaseMusicVolume() {
        float elapsedTime = 0f;
        float currentVolume = music.GetComponent<AudioSource>().volume;
        while (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;
            music.GetComponent<AudioSource>().volume = Mathf.Lerp(currentVolume, currentVolume + 0.3f, elapsedTime*0.4f*timer);
            yield return null;
        }
    }

    //Decrease the Volume of the music by 0.1f
    IEnumerator _DecreaseMusicVolume()
    {
        float elapsedTime = 0f;
        float currentVolume = music.GetComponent<AudioSource>().volume;
        while (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;
            music.GetComponent<AudioSource>().volume = Mathf.Lerp(currentVolume, currentVolume - 0.3f, elapsedTime * 1.6f / timer);
            yield return null;
        }
    }

    //Make the environment bluer
    IEnumerator _SetBluerLight()
    {
        float elapsedTime = 0f;
        Color currentColor = light.GetComponent<Light>().color;

        if (currentColor.r <= 0 || currentColor.g <= 0)
            yield break;

        while (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;

            float r, g;

            r = Mathf.Lerp(currentColor.r,currentColor.r-0.2f, elapsedTime * 0.4f * timer);
            g = Mathf.Lerp(currentColor.g, currentColor.g-0.2f, elapsedTime * 0.4f * timer);

            light.GetComponent<Light>().color = new Color(r, g, currentColor.b, currentColor.a);

            yield return null;
        }
    }

    //Make the light lighter
    IEnumerator _SetLigherLight()
    {
        float elapsedTime = 0f;
        Color currentColor = light.GetComponent<Light>().color;

        if (currentColor.r >= 1 || currentColor.g >= 1)
            yield break;

        while (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;

            float r, g;

            r = Mathf.Lerp(currentColor.r, currentColor.r + 0.2f, elapsedTime * 1.6f / timer);
            g = Mathf.Lerp(currentColor.g, currentColor.g + 0.2f, elapsedTime * 1.6f / timer);

            light.GetComponent<Light>().color = new Color(r, g, currentColor.b, currentColor.a);

            yield return null;
        }
    }
}
