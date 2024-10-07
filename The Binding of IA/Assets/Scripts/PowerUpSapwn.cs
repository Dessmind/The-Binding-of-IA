using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUpController : MonoBehaviour
{
    private float minX, maxX, minY, maxY; // Límites del área de aparición de power-ups
    [SerializeField] private Transform[] spawnPoints; // Puntos donde los power-ups pueden aparecer
    [SerializeField] private GameObject[] powerUpPrefabs; // Prefabs de power-ups que se pueden generar
    [SerializeField] private float powerUpSpawnInterval; // Intervalo de tiempo para generar nuevos power-ups

    private float timeUntilNextSpawn; // Tiempo restante hasta que se genere el próximo power-up

    private void Start()
    {
        // Establece los límites del área de aparición de power-ups utilizando los puntos de spawn
        maxX = spawnPoints.Max(point => point.position.x); // Obtiene la posición X máxima
        minX = spawnPoints.Min(point => point.position.x); // Obtiene la posición X mínima
        maxY = spawnPoints.Max(point => point.position.y); // Obtiene la posición Y máxima
        minY = spawnPoints.Min(point => point.position.y); // Obtiene la posición Y mínima
    }

    private void Update()
    {
        // Incrementa el tiempo hasta el siguiente spawn
        timeUntilNextSpawn += Time.deltaTime;

        // Si ha pasado el tiempo necesario, genera un nuevo power-up
        if (timeUntilNextSpawn >= powerUpSpawnInterval)
        {
            CreatePowerUp(); // Llama a la función para crear un power-up
            timeUntilNextSpawn = 0; // Reinicia el temporizador
        }
    }

    private void CreatePowerUp()
    {
        // Selecciona un prefab de power-up aleatorio
        int randomPowerUpIndex = Random.Range(0, powerUpPrefabs.Length);

        // Genera una posición aleatoria dentro de los límites definidos
        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX), // Posición X aleatoria
            Random.Range(minY, maxY)  // Posición Y aleatoria
        );

        // Instancia el power-up en la posición aleatoria
        Instantiate(powerUpPrefabs[randomPowerUpIndex], randomPosition, Quaternion.identity);
    }
}
