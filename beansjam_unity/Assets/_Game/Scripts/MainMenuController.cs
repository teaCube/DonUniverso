using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

    public Transform playerMenuPosition;
    public Transform cameraMenuPosition;
    public Transform playerInGamePosition;
    public Transform cameraInGamePosition;

    public CanvasGroup mainMenuUIGroup;

    public Transform cameraObj;
    public Transform player;

    public GameObject gameManager;

    private Game gameScript;

    private bool inMenuMode = true;
    private bool inGameMode = false;
    private bool switchToInGameMode = false;

    private float timeStep = 0;
    private float startTime = 0;
    private float switchSpeed = 0.125f;
    private float duration = 3; // in seconds

	// Use this for initialization
	void Start () {
		if (player != null)
        {
            player.position = playerMenuPosition.position;
            player.localEulerAngles = playerMenuPosition.localEulerAngles;
            player.localScale = playerMenuPosition.localScale;
        }

        if (cameraObj != null)
        {
            cameraObj.position = cameraMenuPosition.position;
            cameraObj.localEulerAngles = cameraMenuPosition.localEulerAngles;
            cameraObj.localScale = cameraMenuPosition.localScale;
        }

        if (gameManager != null)
        {
            gameScript = gameManager.GetComponent<Game>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (switchToInGameMode && !inGameMode)
        {
            float progress = (Time.time - startTime) / duration;
            float easeInOutCubic;
            if (progress < 0.5f)
            {
                easeInOutCubic = 4 * progress * progress * progress;
            } 
            else
            {
                easeInOutCubic = (progress-1) * (2*progress-2) * (2*progress-2) + 1;
            }

            mainMenuUIGroup.alpha = 1 - (progress * 2f);

            player.position = Vector3.Lerp(playerMenuPosition.position, playerInGamePosition.position, easeInOutCubic);
            player.localEulerAngles = Vector3.Lerp(playerMenuPosition.localEulerAngles, playerInGamePosition.localEulerAngles, easeInOutCubic);
            player.localScale = Vector3.Lerp(playerMenuPosition.localScale, playerInGamePosition.localScale, easeInOutCubic);
            cameraObj.position = Vector3.Lerp(cameraMenuPosition.position, cameraInGamePosition.position, easeInOutCubic);
            cameraObj.localEulerAngles = Vector3.Lerp(cameraMenuPosition.localEulerAngles, cameraInGamePosition.localEulerAngles, easeInOutCubic);

            if (progress >= 1)
            {
                switchToInGameMode = false;
                inGameMode = true;
                inMenuMode = false;
                // Let the Games BEGIN!!!
                gameScript.StartGame();
            }
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
        startTime = Time.time;
    }
}
