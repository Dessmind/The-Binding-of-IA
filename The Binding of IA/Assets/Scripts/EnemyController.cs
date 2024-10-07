using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    private float minX, maxX, minY, maxY; // Límites del área de aparición de enemigos
    [SerializeField] private Transform[] spawnPoints; // Puntos donde los enemigos pueden aparecer
    [SerializeField] private GameObject[] enemyPrefabs; // Prefabs de enemigos que se pueden generar
    [SerializeField] private float enemySpawnInterval; // Intervalo de tiempo para generar nuevos enemigos

    private float timeUntilNextSpawn; // Tiempo restante hasta que se genere el próximo enemigo

    private void Start()
    {
        // Establece los límites del área de aparición de enemigos utilizando los puntos de spawn
        maxX = spawnPoints.Max(point => point.position.x); // Obtiene la posición X máxima
        minX = spawnPoints.Min(point => point.position.x); // Obtiene la posición X mínima
        maxY = spawnPoints.Max(point => point.position.y); // Obtiene la posición Y máxima
        minY = spawnPoints.Min(point => point.position.y); // Obtiene la posición Y mínima
    }

    private void Update()
    {
        // Incrementa el tiempo hasta el siguiente spawn
        timeUntilNextSpawn += Time.deltaTime;

        // Si ha pasado el tiempo necesario, genera un nuevo enemigo
        if (timeUntilNextSpawn >= enemySpawnInterval)
        {
            CreateEnemy(); // Llama a la función para crear un enemigo
            timeUntilNextSpawn = 0; // Reinicia el temporizador
        }
    }

    private void CreateEnemy()
    {
        // Selecciona un prefab de enemigo aleatorio
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);

        // Genera una posición aleatoria dentro de los límites definidos
        Vector2 randomPosition = new Vector2(
            Random.Range(minX, maxX), // Posición X aleatoria
            Random.Range(minY, maxY)  // Posición Y aleatoria
        );

        // Instancia el enemigo en la posición aleatoria
        Instantiate(enemyPrefabs[randomEnemyIndex], randomPosition, Quaternion.identity);
    }
}
