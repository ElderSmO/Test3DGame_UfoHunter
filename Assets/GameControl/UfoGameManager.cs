using Assets.GameControl;
using Assets.UFO_Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoGameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> portalList;
    [SerializeField] List<GameObject> ufoList;

    List<GameObject> ufoBuffer;
    bool gameStarted;
    int countLoseUfo;

    [Header("Game Settings")]
    [SerializeField] private int maxMissedUfo = 5; // Максимум пропущенных UFO до проигрыша

    [Header("Spawn Settings")]
    [SerializeField] private float minSpawnDelay = 2f; // Минимальная задержка спавна
    [SerializeField] private float maxSpawnDelay = 0.3f; // Максимальная задержка спавна (максимальная сложность)
    [SerializeField] private float timeToMaxDifficulty = 120f; // Время за которое достигается максимальная сложность (секунды)

    private float gameTimer; 
    private Coroutine spawnCoroutine;

    void Start()
    {
        ufoBuffer = new List<GameObject>();
        UfoGameEvents.EnemyInDeadZone += OnEnemyInDeadZone;
        UfoGameEvents.StartGameAction += StartGame;
    }

    void OnDestroy()
    {
        UfoGameEvents.EnemyInDeadZone -= OnEnemyInDeadZone;
    }

    void StartGame()
    {
        if (gameStarted) return;
        UfoGameEvents.onLoseGame();
        gameStarted = true;
        countLoseUfo = 0;
        gameTimer = 0f; 

        
        ClearAllUfos();

        
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(ContinuousSpawnCoroutine());

    }

    IEnumerator ContinuousSpawnCoroutine()
    {
        while (gameStarted)
        {
            // Вычисляем текущую задержку спавна на основе времени игры
            float currentSpawnDelay = GetCurrentSpawnDelay();
            yield return new WaitForSeconds(currentSpawnDelay);

            // Создаем случайный UFO из случайного портала
            CreateRandomUfo();
        }
    }

    void Update()
    {
        if (gameStarted)
        {
            gameTimer += Time.deltaTime;

            if (Mathf.FloorToInt(gameTimer) > Mathf.FloorToInt(gameTimer - Time.deltaTime))
            {
               UfoGameEvents.OnUpdateTime();
            }
        }
    }

    float GetCurrentSpawnDelay()
    {
        // Чем больше времени прошло, тем меньше задержка (больше UFO)
        float difficultyProgress = Mathf.Clamp01(gameTimer / timeToMaxDifficulty);
        
        float currentDelay = Mathf.Lerp(minSpawnDelay, maxSpawnDelay, difficultyProgress);

        return currentDelay;
    }

    void CreateRandomUfo()
    {
        if (portalList == null || portalList.Count == 0 || ufoList == null || ufoList.Count == 0)
        {
            Debug.LogError("Поля параметров игры пусты");
            return;
        }

        // Выбираем случайный портал
        int randomPortalIndex = Random.Range(0, portalList.Count);
        GameObject selectedPortal = portalList[randomPortalIndex];

        // Выбираем случайный UFO префаб
        int randomUfoIndex = Random.Range(0, ufoList.Count);
        GameObject randomUfoPrefab = ufoList[randomUfoIndex];

        // Создаем UFO в позиции портала
        GameObject newUfo = Instantiate(randomUfoPrefab, selectedPortal.transform.position, selectedPortal.transform.rotation);
        UFO ufo = newUfo.GetComponent<UFO>();
        // Настраиваем случайные параметры UFO
        SetupRandomUfoParameters(ufo);

        // Добавляем в буфер для отслеживания
        ufoBuffer.Add(newUfo);

    }

    void SetupRandomUfoParameters(IEnemy enemy)
    {
        if (enemy == null) return;

        // ИСПОЛЬЗОВАНИЕ ВРЕМЕНИ для увеличения скорости UFO
        float speedMultiplier = 1f + (gameTimer / timeToMaxDifficulty) * 0.5f; // +50% к скорости на максимуме
        enemy.Speed = Random.Range(1f, 1.5f) * speedMultiplier;

       
        enemy.StartMovingRight = Random.Range(0, 2) == 0;

        
        float changeTimeMultiplier = Mathf.Lerp(1f, 0.7f, gameTimer / timeToMaxDifficulty); // -30% времени на максимуме
        enemy.ChangeVectorTime = Random.Range(1f, 2f) * changeTimeMultiplier;

        
        int maxHp = Mathf.Min(3, 1 + Mathf.FloorToInt(gameTimer / 30f)); // +1 HP каждые 30 секунд, максимум 3
        enemy.Hp = Random.Range(1, maxHp + 1);
    }

    void OnEnemyInDeadZone(GameObject ufo)
    {
        if (ufoBuffer != null && ufoBuffer.Contains(ufo))
        {
            ufoBuffer.Remove(ufo);
        }

        countLoseUfo++;
        

        
        if (countLoseUfo >= maxMissedUfo)
        {
            LoseGame();
        }
    }

    void LoseGame()
    {
        if (!gameStarted) return;

        gameStarted = false;

        // Останавливаем спавн
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);

        // Очищаем все UFO с экрана
        ClearAllUfos();
        UfoGameEvents.onLoseGame();
    }

    void ClearAllUfos()
    {
        if (ufoBuffer != null)
        {
            foreach (GameObject ufo in ufoBuffer.ToArray())
            {
                if (ufo != null)
                    Destroy(ufo);
            }
            ufoBuffer.Clear();
        }
    }

    

    
    //public float GetSurvivalTime()
    //{
    //    return gameTimer;
    //}

   
    //public float GetDifficultyProgress()
    //{
    //    return Mathf.Clamp01(gameTimer / timeToMaxDifficulty);
    //}
}