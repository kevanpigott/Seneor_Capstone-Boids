using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    public Slider cohesionSlider;
    public Slider targetSlider;
    public Slider allignmentSlider;
    public Slider seperationSlider;
    public InputField newFlockSize;
    public GameObject Spawner;
    public Text FPS;
    public float deltaTime = 0.0f;

    void Start()
    {
        cohesionSlider.value = 0.1f;
        targetSlider.value = 1.6f;
        seperationSlider.value = 100f;
        allignmentSlider.value = .6f;
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        //float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        FPS.text = "fps = " + fps.ToString();
    }

    public void SubmitSliderSetting()
    {
        //Displays the value of the slider in the console.
        Spawner.GetComponent<BoidSpawner>().setCohesion(cohesionSlider.value);
        Spawner.GetComponent<BoidSpawner>().setTarget(targetSlider.value);

    }

    public void SubmitCohesion()
    {
        //Displays the value of the slider in the console.
        Spawner.GetComponent<BoidSpawner>().setCohesion(cohesionSlider.value);
    }

    public void SubmitTarget()
    {
        //Displays the value of the slider in the console.
        Spawner.GetComponent<BoidSpawner>().setTarget(targetSlider.value);
    }

    public void SubmitAllignment()
    {
        //Displays the value of the slider in the console.
        Spawner.GetComponent<BoidSpawner>().setAllignment(allignmentSlider.value);
    }

    public void SubmitSeperation()
    {
        //Displays the value of the slider in the console.
        Spawner.GetComponent<BoidSpawner>().setSeperation(seperationSlider.value);
    }

    public void ResetSpawner()
    {


        //Spawner.GetComponent<BoidSpawner>().enabled = false; //toggle this script to re-invoke it
        int number;
        if (int.TryParse(newFlockSize.text, out number))
        {
            Spawner.GetComponent<BoidSpawner>().flockSize = (System.Convert.ToInt32(newFlockSize.text));
        }
        cohesionSlider.value = 0.1f;
        targetSlider.value = 1.6f;
        seperationSlider.value = 100f;
        allignmentSlider.value = .6f;
        Spawner.GetComponent<BoidSpawner>().Restart();
    }

    public void exitGame()
    {
        Application.Quit();
        //Debug.Log("Game is exiting");
    }

}
