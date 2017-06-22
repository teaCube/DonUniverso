using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public Transform playerMenuPosition;
    public Transform cameraMenuPosition;
    public Transform playerInGamePosition;
    public Transform cameraInGamePosition;

    public CanvasGroup mainMenuUIGroup;

    public Transform camera;
    public Transform player;

    private bool inMenuMode = true;
    private bool inGameMode = false;
    private bool switchToInGameMode = false;

    private float timeStep = 0;
    private float switchDuration = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (switchToInGameMode && !inGameMode)
        {
            float delta = Time.deltaTime;
            timeStep += delta;
            float progress = timeStep / switchDuration;

            LerpTransform(player, playerInGamePosition, delta);
            LerpTransform(camera, cameraInGamePosition, delta);

            // progress does not syncronize with the lerp.... have to check this.
            if (progress >= 1)
            {
                switchToInGameMode = false;
                inGameMode = true;
                inMenuMode = false;
            }

            Debug.Log(progress);

            mainMenuUIGroup.alpha = 1- progress;
        }
	}

    private void LerpTransform(Transform t1, Transform t2, float t)
    {
        t1.position = Vector3.Lerp(t1.position, t2.position, t);
        t1.rotation = Quaternion.Lerp(t1.rotation, t2.rotation, t);
        t1.localScale = Vector3.Lerp(t1.localScale, t2.localScale, t);
    }

    public void startPlay()
    {
        switchToInGameMode = true;
    }
}
