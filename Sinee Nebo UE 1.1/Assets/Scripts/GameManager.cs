using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;
using Cinemachine;
using Unity.VisualScripting;
using Vector3 = UnityEngine.Vector3;


public class GameManager : MonoBehaviour
{
    //Счет
    public Text scoreText;
    public Text gameOverText;
    public Text levelText;
    public Text levelUpText;
    public Sky sky;
    public MeteorSpawn meteorSpawn;
    public Meteor2Spawn meteor2Spawn;
    public AlienSpawn alienSpawn;
    public ShipControl shipControl;
    public UnityEngine.UI.Button restartButton;
    public MicroFunc MicroFunc;
    //Список прицельцев
    public HashSet<GameObject> aliens = new HashSet<GameObject>();
    // Список метеоров 1
    public HashSet<GameObject> meteors1 = new HashSet<GameObject>();
    // Список метеоров 2
    public HashSet<GameObject> meteors2 = new HashSet<GameObject>();
    // Список жизней
    public List<GameObject> livesList = new List<GameObject>();

    // Количество жизней
    public int lives;
    // Количество пришельцев
    public int countAliens;
    // Количество метеоров 1
    public int countMeteor1;
    // Количество метеоров 1
    public int countMeteor2;
    // Максимальное количество пришельцев
    [Header("Максимальное количество")]
    [Space(5)]
    public int maxAliens;
    // Максимальное количество метеоров 1
    public int maxMeteor1;
    // Максимальное количество метеоров 2
    public int maxMeteor2;
    // Дистанция до цели
    public float maxDistTarget;
    [Header("Скорость")]
    [Space(5)]
    // Скорость
    public float speedMeteor1;
    public float speedMeteor2;
    public float speedAliens;
    [Header("Время респавна")]
    [Space(5)]
    // Время через которое спавнятся объекты
    public float meteorSpawnDeley = 2f;
    public float meteor2SpawnDeley = 3f;
    public float alienSpawnDeley = 1.5f;
    // Включение спавна
    public bool alienSpawnOn;
    public bool meteor1SpawnOn;
    public bool meteor2SpawnOn;
    // Включение управления мышкой
    [Header("Управление мышкой")]
    [Space(5)]
    public bool mouseControlGame;
    // Ограничение предвижения корабля
    [Header("Ограничение движ корабля")]
    [Space(5)]
    public float shipXlimit;
    public float shipYlimit;
    // Шаг начальной позиции пришельцев
    public float spawnStep = 1;
    // для запоминания старых ограничений
    private float shipXlimOld;
    private float shipYlimOld;
    // для запоминания позиции спавна по оси Z
    private float spawnZold;
    // Словарь спавна позиций метеоритов 1
    public IDictionary<Vector3, bool> spawnPosDicMeteor1 = new Dictionary<Vector3, bool>();
    public float spawnStepMeteor1;
    // Словарь спавна позиций метеоритов 2
    public IDictionary<Vector3, bool> spawnPosDicMeteor2 = new Dictionary<Vector3, bool>();
    public float spawnStepMeteor2;
    // Словарь спавна позиций пришельцев
    public IDictionary<Vector3, bool> spawnPosDicAliens = new Dictionary<Vector3, bool>();
    public float spawnStepAliens;
    
    //  Позиция спавна
    private Vector3 spawnPos;
    public Vector3 GridSize;
    public float sizeSell;
    public float spawnZ;
    public float timeActivSpawnPos = 1f;
    
    int playerScore = 0;
    float LevUpTextTime = 0;
    int level = 1;
    int nextlevel = 10;

    //Для групповых позиций пришельцев
    [Header("Групповые позиции пришельцев")]
    [Space(5)]
    // Список начальных позиций пришельцев
    public List<List<Vector3>> groupPosAliensList = new List<List<Vector3>>();
    public bool goAlienOn;
    public Vector2 centerGroupPosAliens;
    public float step;
    public float posZ;
    public float radius;
    public bool runCorutine;
    Vector3 groupPos;
    public float timeStopAlien;
    public int directionAliens;
    public float timeDirection;
    private float stepTimeDirection;
    private List<Vector3> row = new List<Vector3>();
    public int maxRows;
    public int rowLong;
    public float stepRadius;

    
    private void Awake()
    {
        
        lives = 3;
        
    }
    
    public void AddScore()
    {
        //Добавление счета ///////////////////
        
        playerScore++;
        scoreText.text = playerScore.ToString();

        if (playerScore <= nextlevel) return;
        LevelUp();
        nextlevel += 10;
    }

    public void LevelUp()
    {
        // Поднятие уровня ///////////////////
        
        level++;
        levelText.text = level.ToString();
        levelUpText.enabled = true;
        IncresedSpeeed();
        
    }

    private void IncresedSpeeed()
    {
        //Увеличение скорости игры ///////////////////
        
        sky.speed -= 0.01f;
        speedMeteor1 -= 0.02f;
        speedMeteor2 -= 0.02f;
        speedAliens += 0.005f;

        maxAliens += 1;
        maxMeteor1 += 1;
        maxMeteor2 += 1;
        
        meteorSpawnDeley -= 0.01f;
        meteor2SpawnDeley -= 0.01f;
        alienSpawnDeley -= 0.01f;

        shipControl.reloadTime -= 0.5f;
        shipControl.speed += 0.2f;

    }
    public void PlayerDied()
    {
        // Смерть игрока  ///////////////////
        //Приостанавливает игру
        gameOverText.enabled = true;
        Time.timeScale = 0;
        restartButton.gameObject.SetActive(true);
    }


    public List<List<Vector3>> CreateGroupPosAliens(List<List<Vector3>> groupPosList,float radius, float stepRadius,
        int rowLong )
    {
        // Создает список позиций для группового движения пришельцев ///////////////////
        groupPosList.Clear();
        
        // Создает групповые позиции пришельцев
        for (int rowIndex = 0; rowIndex < maxRows; rowIndex++)
        {
            var posList = new List<Vector3>();
            if (rowIndex == maxRows - 1)
            {
                rowLong = 30;
                radius = 25 ;

            }
            for (int i = 0; i < rowLong ; i++)
            {
                
                posList.Add(MicroFunc.CreateCircleGroupPosAlien(centerGroupPosAliens, posZ, rowLong, i, radius));
                
            }
            groupPosList.Add(posList);
            rowLong /= 2;
            radius -= stepRadius;
            posZ += 4;
        }
        return groupPosList;


    }
    
    //Для вывода сетки настройки спавна ///////////////////
    
    /*private void OnDrawGizmosSelected()
    {
        for(float x = -GridSize.x; x <= GridSize.x; x++)
        {
            for (float y = -GridSize.y ; y <= GridSize.y; y++)
            {
                Gizmos.color = new Color(0.88f, 0f, 1f, 0.4f);
                Gizmos.DrawCube(new Vector3( x *spawnStep , y * spawnStep, spawnZ), 
                    new Vector3(sizeSell, sizeSell, sizeSell));
            }
        }
    }*/

    private IDictionary<Vector3,bool> updateSpawnPosDictionary(IDictionary<Vector3,bool> spawnDic,
        float  spawnZ, float spawnStep)
    {
        // Обновляет позиции спавна пришельцев ///////////////////
        
        spawnDic.Clear();
        for(float x = -shipXlimit; x <= shipXlimit; x = x + spawnStep)
        {
            for (float y = -shipYlimit; y <= shipYlimit; y = y + spawnStep)
            {
                spawnPos = new(x , y, spawnZ);
                spawnDic.Add(spawnPos,true);
            }
        }
        return spawnDic;
    }
    
    public IEnumerator resetRandomPos(IDictionary<Vector3,bool> spawnDic , Vector3 randomPos, float time)
    {
        // Через заданное время разрешает спавн на позиции ///////////////////
        
        yield return new WaitForSeconds(time);
        spawnDic[randomPos] = true;
    }
    
    public IEnumerator goAlien(float timeStopAlien)
    {
        //Запускает движение пришельцев по позициям ///////////////////
        
        runCorutine = true;
        while (true)
        {
            yield return new WaitForSeconds(timeStopAlien);
            UpdatePos();
        }
    }
    
    public void UpdatePos()
    {
        // Обновляет позиции пришельцев ///////////////////
        
        foreach (var alien in aliens)
        {
            var alienMover = alien.GetComponent<AllienMover>();
            if (directionAliens == 1)
            {
                var indexPos = groupPosAliensList[alienMover.rowAlien].IndexOf(alienMover.groupPos);
                if (indexPos < groupPosAliensList[alienMover.rowAlien].Count - 1)
                {
                    alienMover.groupPos = groupPosAliensList[alienMover.rowAlien][indexPos + 1];
                }
                if (indexPos >= groupPosAliensList[alienMover.rowAlien].Count - 1)
                {
                    alienMover.groupPos = groupPosAliensList[alienMover.rowAlien][0];
                }
                    
            }
            if (directionAliens == -1)
            {
                var indexPos = groupPosAliensList[alienMover.rowAlien].IndexOf(alienMover.groupPos);
                if (indexPos > 0)
                {
                    alienMover.groupPos = groupPosAliensList[alienMover.rowAlien][indexPos - 1];
                }
                if (indexPos <= 0)
                {
                    alienMover.groupPos = groupPosAliensList[alienMover.rowAlien][groupPosAliensList[alienMover.rowAlien].Count - 1];
                }
            }
            
        }
    }


    // START /////////////////// /////////////////// /////////////////// /////////////////// /////////////////// ///////////////////
    void Start()
    {
        groupPosAliensList = CreateGroupPosAliens(groupPosAliensList, radius, stepRadius, rowLong);
        spawnPosDicMeteor1 = updateSpawnPosDictionary(spawnPosDicMeteor1, spawnZ, spawnStepMeteor1);
        spawnPosDicMeteor2 = updateSpawnPosDictionary(spawnPosDicMeteor2, spawnZ + 5, spawnStepMeteor2);
        spawnPosDicAliens = updateSpawnPosDictionary(spawnPosDicAliens, spawnZ + 10,spawnStepAliens);
        shipControl = shipControl.GetComponent<ShipControl>();
        shipXlimOld = shipXlimit;
        shipYlimOld = shipYlimit;
        spawnZold = spawnZ;
        directionAliens = 1;
        GridSize.x = shipXlimit;
        GridSize.y = shipYlimit;
        sizeSell = spawnStep;
        spawnStep = 1;
        restartButton.gameObject.SetActive(false);
        Time.timeScale = 1;
        
    }

    // UPDATE /////////////////// /////////////////// /////////////////// /////////////////// /////////////////// ///////////////////
    void FixedUpdate()
    {
        
        if (shipXlimOld != shipXlimit || shipYlimOld != shipYlimit || spawnZold != spawnZ)
        {
            //Если пределы движения корабля или Z координата спавна изменилась ///////////////////
            
            spawnPosDicMeteor1 = updateSpawnPosDictionary(spawnPosDicMeteor1, spawnZ, spawnStepMeteor1);
            spawnPosDicMeteor2 = updateSpawnPosDictionary(spawnPosDicMeteor2, spawnZ + 5, spawnStepMeteor2);
            spawnPosDicAliens = updateSpawnPosDictionary(spawnPosDicAliens, spawnZ + 10 ,spawnStepAliens);
            shipXlimOld = shipXlimit;
            shipYlimOld = shipYlimit;
            spawnZold = spawnZ;
        }
        
        if (levelUpText.enabled)
        {
            LevUpTextTime += Time.deltaTime;
            if (LevUpTextTime >= 0.5f)
            {
                levelUpText.enabled = false;
                LevUpTextTime = 0;
            }
        }
        
        //Для отображения в инспекторе
        
        countAliens = aliens.Count;
        countMeteor1 = meteors1.Count;
        countMeteor2 = meteors2.Count;

        if (aliens.Count > 0 )
        {
            if (MicroFunc.ifReadyAllAliens(aliens))
            {
                if (!runCorutine)
                {
                    StartCoroutine(goAlien(timeStopAlien));
                }
            }

            if (runCorutine)
            {
                stepTimeDirection += Time.deltaTime;
                if (stepTimeDirection >= timeDirection)
                {
                    directionAliens *= -1;
                    stepTimeDirection = 0;
                }
            }
        }
        
    }
}

