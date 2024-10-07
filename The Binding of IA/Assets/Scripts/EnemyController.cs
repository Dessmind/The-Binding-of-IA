using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    private float minX, maxX, minY, maxY; // L�mites del �rea de aparici�n de enemigos
    [SerializeField] private Transform[] spawnPoints; // Puntos donde los enemigos pueden aparecer
    [SerializeField] private GameObject[] enemyPrefabs; // Prefabs de enemigos que se pueden generar
    [SerializeField] private float enemySpawnInterval; // Intervalo de tiempo para generar nuevos enemigos

    private float timeUntilNextSpawn; // Tiempo restante hasta que se genere el pr�ximo enemigo

    private void Start()
    {
        // Establece los l�mites del �rea de aparici�n de enemigos utilizando los puntos de spawn
        maxX = spawnPoints.Max(point => point.position.x); // Obtiene la posici�n X m�xima
        minX = spawnPoints.Min(point => point.position.x); // Obtiene la posici�n X m�nima
        maxY = spawnPoints.Max(point => point.position.y); // Obtiene la posici�n Y m�xima
        minY = spawnPoints.Min(point => point.position.y); // Obtiene la posici�n Y m�nima
    }

    private void Update()
    {
        // Incrementa el tiempo hasta el siguiente spawn
        timeUntilNextSpawn += Time.deltaTime;

        // Si ha pasado el tiempo necesario, genera un nuevo enemigo
        if (timeUntilNextSpawn >= enemySpawnInterval)
        {
            CreateEnemy(); // Llama a la funci�n para crear un enemigo
            timeUntilNextSpawn = 0; // Reinicia el temporizador
        }
    }

    private void CreateEnemy()
    {
        // Selecciona un prefab de enemigo aleatorio
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);

        // Genera una posici�n aleatoria dentro de los l�mites definidos
        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX), // Posici�n X aleatoria
            Random.Range(minY, maxY)  // Posici�n Y aleatoria
        );

        // Instancia el enemigo en la posici�n aleatoria
        Instantiate(enemyPrefabs[randomEnemyIndex], randomPosition, Quaternion.identity);
    }
}
