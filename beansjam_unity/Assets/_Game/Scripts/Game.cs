using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Game script initializes the game by spawning the planets and asteroids.
/// </summary>
public class Game : MonoBehaviour {

    public GameObject world;
    public GameObject HUD;
    public GameObject shopUI;
    public GameObject player;

    private Ship shipScript;
    private SpaceShipControll spaceShipControllScript;

    // The planet spawner component, used to spawn the planets and asteroids.
	[HideInInspector] 
    public PlanetSpawner planetSpawner;

	private void Awake() {
	}

	private void Start()
    {
        // Initialize the planet spawner
        Random.InitState("hdmksdfkasdfkjh".GetHashCode());
        planetSpawner = world.GetComponent<PlanetSpawner>();

        shipScript = player.GetComponent<Ship>();
        spaceShipControllScript = player.GetComponent<SpaceShipControll>();
        
        // disable HUD
        HUD.SetActive(false);
        shopUI.SetActive(false);

        // hide world
        world.SetActive(false);
    }

    public void StartGame()
    {
        // display HUD
        HUD.SetActive(true);
        shopUI.SetActive(true);

        // awake world
        world.SetActive(true);

        // spawn 100 planets
        for (int i = 0; i < 100; i++)
            planetSpawner.SpawnPlanet();

        // spawn 50 asteroids
        for (int i = 0; i < 50; i++)
            planetSpawner.SpawnAsteroid();

        // activate player scripts
        shipScript.enabled = true;
        spaceShipControllScript.enabled = true;
    }
}
