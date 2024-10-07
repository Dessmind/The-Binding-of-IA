using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUpController : MonoBehaviour
{
    private float minX, maxX, minY, maxY; // L�mites del �rea de aparici�n de power-ups
    [SerializeField] private Transform[] spawnPoints; // Puntos donde los power-ups pueden aparecer
    [SerializeField] private GameObject[] powerUpPrefabs; // Prefabs de power-ups que se pueden generar
    [SerializeField] private float powerUpSpawnInterval; // Intervalo de tiempo para generar nuevos power-ups

    private float timeUntilNextSpawn; // Tiempo restante hasta que se genere el pr�ximo power-up

    private void Start()
    {
        // Establece los l�mites del �rea de aparici�n de power-ups utilizando los puntos de spawn
        maxX = spawnPoints.Max(point => point.position.x); // Obtiene la posici�n X m�xima
        minX = spawnPoints.Min(point => point.position.x); // Obtiene la posici�n X m�nima
        maxY = spawnPoints.Max(point => point.position.y); // Obtiene la posici�n Y m�xima
        minY = spawnPoints.Min(point => point.position.y); // Obtiene la posici�n Y m�nima
    }

    private void Update()
    {
        // Incrementa el tiempo hasta el siguiente spawn
        timeUntilNextSpawn += Time.deltaTime;

        // Si ha pasado el tiempo necesario, genera un nuevo power-up
        if (timeUntilNextSpawn >= powerUpSpawnInterval)
        {
            CreatePowerUp(); // Llama a la funci�n para crear un power-up
            timeUntilNextSpawn = 0; // Reinicia el temporizador
        }
    }

    private void CreatePowerUp()
    {
        // Selecciona un prefab de power-up aleatorio
        int randomPowerUpIndex = Random.Range(0, powerUpPrefabs.Length);

        // Genera una posici�n aleatoria dentro de los l�mites definidos
        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX), // Posici�n X aleatoria
            Random.Range(minY, maxY)  // Posici�n Y aleatoria
        );

        // Instancia el power-up en la posici�n aleatoria
        Instantiate(powerUpPrefabs[randomPowerUpIndex], randomPosition, Quaternion.identity);
    }
}
