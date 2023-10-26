using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public GameManager gameManager;
    public CinemachineCollisionImpulseSource cinemahcine;
    public GameObject bulletPrefab;
    public GameObject explosionShip;
    public GameObject explosionShip2;
    public GameObject sssLaser;
    public GameObject shield;
    public GameObject targetPrefab;
    public SelectObj currentSelect;
    Quaternion q;
    private Rigidbody rb;
    private Collider colliderShip;
    // Ограничение по осям х и у
    float xLimit;
    float yLimit;
    
    //Угол наклона
    public float angle;
    // Скорость
    public float speed = 40f;

    // Перезарядка
    public float reloadTime = 0.5f;

    private float maxDistTarget;
    private float elapsedTime = 0f;

    private float xInput;
    private float yInput;

    private bool mouseControl;

    Ray target;
    Color oldTargetColor;

    void Shot()
    {
        //Функуия выстрела
        Vector3 spawnPos = transform.position;
        spawnPos += new Vector3(-0.5f, 0f, 3.3f);
        Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Vector3 spawnPos2 = transform.position;
        spawnPos2 += new Vector3(0.5f, 0f, 3.3f);
        Instantiate(bulletPrefab, spawnPos2, Quaternion.identity);
        elapsedTime = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
        cinemahcine = GetComponent<CinemachineCollisionImpulseSource>();
        colliderShip = GetComponent<BoxCollider>();
        mouseControl = gameManager.mouseControlGame;
        maxDistTarget = gameManager.maxDistTarget;
        
    }

    // Update is called once per frame
    void Update()
    {
        colliderShip.enabled = !shield.gameObject.activeSelf;
        xLimit = gameManager.shipXlimit;
        yLimit = gameManager.shipYlimit;
        // отсёт времени после выстрела
        elapsedTime += Time.deltaTime;

        // Перемещение игрока влево и вправо
        if (mouseControl)
        {
            // Если включено управление мышью
            xInput = Input.GetAxis("Mouse X");
            yInput = Input.GetAxis("Mouse Y");
            // Выстрел левой кнопкой мыши
            if (Input.GetMouseButtonDown(0) && elapsedTime > reloadTime)
            {
                Shot();
            }
        }

        if (!mouseControl)
        {
            
            // Если велючено упрвление клавиатурой
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");
            // if (Input.GetKey(KeyCode.UpArrow))
            // {
            //     transform.Translate(0f, -xInput * speed * Time.deltaTime, 1f * speed* Time.deltaTime);
            // }
            //
            // if (Input.GetKey(KeyCode.DownArrow))
            // {
            //     transform.Translate(0f, -xInput * speed * Time.deltaTime, -1f * speed* Time.deltaTime);
            // }
            // Выстрел клавишей Пробел
            if (Input.GetButtonDown("Jump"))
            {
                Shot();
            }
            

        }

        transform.Translate(0f, -xInput * speed * Time.deltaTime, yInput * speed * Time.deltaTime);
        
        //Фиксируем положение коробля по оси х
        var position = transform.position;
        position.x = Mathf.Clamp(position.x, -xLimit, xLimit); // ограничение по оси х
        position.y = Mathf.Clamp(position.y, -yLimit, yLimit); // ограничение по оси у
        transform.position = position;
        

        //Наклоны при движениии
        
        
        if (Input.GetKey(KeyCode.B))
        {
            // Включает лазер
            sssLaser.gameObject.SetActive(true);
        }
        else
        {
            sssLaser.gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.M))
        {
            // Активирует щит
            shield.gameObject.SetActive(true);
        }
        

        // Цель
        target = new Ray(transform.position, -transform.right);
        RaycastHit hit;
        Debug.DrawRay(transform.position, -transform.right * maxDistTarget, Color.green);
        
        if (Physics.Raycast(target, out hit, maxDistTarget))
        {
            
            if (hit.collider.gameObject.CompareTag("Meteor1") || hit.collider.gameObject.CompareTag("Meteor2")
                                                              || hit.collider.gameObject.CompareTag("Allien"))
            {
                targetPrefab.SetActive(true);
                targetPrefab.transform.position = hit.point;
                var select = hit.collider.gameObject.GetComponent<SelectObj>();
                if (select != null)
                {
                    if (currentSelect != null && currentSelect != select)
                    {
                        currentSelect.Deselect();
                        currentSelect = null;
                    }
                    currentSelect = select;
                    select.Select();
                }
                else
                {
                    if (currentSelect != null)
                    {
                        currentSelect.Deselect();
                        currentSelect = null;
                    }
                }
            }
        }
        else
        {
            targetPrefab.SetActive(false);
            if (currentSelect != null)
            {
                currentSelect.Deselect();
                currentSelect = null;
            }

        }

        maxDistTarget = gameManager.maxDistTarget;
        mouseControl = gameManager.mouseControlGame;
    }


    //Столкновение метеора с игроком
    private void OnTriggerEnter(Collider other)
    {
        //Если сталкивается с противником
        if (other.gameObject.tag == "Meteor1" || other.gameObject.tag == "Meteor2" || other.gameObject.tag == "Allien")
        {
            var crashPos = gameObject.transform.position;
            var expl1 = Instantiate(explosionShip, crashPos, Quaternion.identity);
            var expl2 = Instantiate(explosionShip2, crashPos, Quaternion.identity);
            Destroy(expl1, 1);
            Destroy(expl2, 1);
            gameManager.lives -= 1;
            cinemahcine.GenerateImpulse();
            
            
            //if (gameManager.lives < 0)
            //{
            // gameManager.PlayerDied();
            //}
        }

        
    }
}