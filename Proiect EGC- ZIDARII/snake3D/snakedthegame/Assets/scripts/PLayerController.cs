using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tags;


public class PLayerController : MonoBehaviour
{
    [HideInInspector] //vizibile doar in cod
    public Comenzile comanda;

    [HideInInspector]
    public float lungimea = 0.2f;

    [HideInInspector]
    public float miscare_permanenta = 0.1f;

    private float counter;
    private bool misc;

    [SerializeField] ///se foloseste doar aici si e ascunsa
    private GameObject CoadaPrefab;

    private List<Vector3> pozitia;
    private List<Rigidbody> bucati;

    private Rigidbody corpul;
    private Rigidbody capul;
    private Transform trans;

    private bool adauga;

    void Awake()
    {
        //pozitia initiala a sarpelui folosita la misc

        trans = transform;
        corpul = GetComponent<Rigidbody>();

        InitSnakeNodes();
        InitPlayer();

        pozitia = new List<Vector3>()
        {
            new Vector3(-lungimea,0f),
            new Vector3(0f, lungimea),
            new Vector3(lungimea,0f),
            new Vector3(0f, -lungimea)
        };
    }


    void Update()
    {
        CheckMovementFrequency();
       //verifici miscarea constanta
    }

   void FixedUpdate()
    {
        if(misc)
        {
            //variabile pt msicarea fortata
            misc = false;
            Misc();
        }
    }

    void InitSnakeNodes()
    {
        //componentele sarpelui
        bucati = new List<Rigidbody>();

        bucati.Add(trans.GetChild(0).GetComponent<Rigidbody>());
        bucati.Add(trans.GetChild(1).GetComponent<Rigidbody>());
        bucati.Add(trans.GetChild(2).GetComponent<Rigidbody>());

        capul = bucati[0];
    }

    void SetDirectionRandom()
    {
        int dirRandom = UnityEngine.Random.Range(0, (int)Comenzile.COUNT);
        comanda = (Comenzile)dirRandom;
    }

    void InitPlayer()
    {
        SetDirectionRandom();
        switch(comanda)
        {
            //pozitie de start
            case Comenzile.RIGHT:

                bucati[1].position = bucati[0].position - new Vector3(Dimensiuni.bucata, 0f, 0f);
                bucati[2].position = bucati[0].position - new Vector3(Dimensiuni.bucata * 2f, 0f, 0f);

                break;

                case Comenzile.LEFT:

                bucati[1].position = bucati[0].position + new Vector3(Dimensiuni.bucata, 0f, 0f);
                bucati[2].position = bucati[0].position + new Vector3(Dimensiuni.bucata * 2f, 0f, 0f);

                break;

                case Comenzile.UP:

                bucati[1].position = bucati[0].position - new Vector3(0f, Dimensiuni.bucata, 0f);
                bucati[2].position = bucati[0].position - new Vector3(0f, Dimensiuni.bucata * 2f, 0f);

                break;

                case Comenzile.DOWN:

                bucati[1].position = bucati[0].position + new Vector3(0f, Dimensiuni.bucata, 0f);
                bucati[2].position = bucati[0].position + new Vector3(0f, Dimensiuni.bucata * 2f, 0f);

                break;
                
        }
    }

    void Misc ()
    {

     //miscare si adauga la corp
        Vector3 dpozitia = pozitia[(int)comanda];

        Vector3 pozitiainitiala = capul.position;
        Vector3 pozitiaurmatoare;

        corpul.position = corpul.position + dpozitia;
        capul.position = capul.position + dpozitia;

        for(int i = 1; i < bucati.Count; i++)
        {

            pozitiaurmatoare = bucati[i].position;
            bucati[i].position = pozitiainitiala;
            pozitiainitiala = pozitiaurmatoare;
        }

        if(adauga)
        {
            adauga = false;

            GameObject bucatanoua = Instantiate(CoadaPrefab, bucati[bucati.Count - 1].position, Quaternion.identity);

            bucatanoua.transform.SetParent(transform, true);
            bucati.Add(bucatanoua.GetComponent<Rigidbody>());
        }
        //adauga frucrul in coada
    }

    void CheckMovementFrequency()
    {

        counter += Time.deltaTime;

        if(counter >= miscare_permanenta)
        {
            counter = 0f;
            misc = true;
        }
    }

    public void SetInputDirection(Comenzile com)
    {
        if(com == Comenzile.UP && comanda == Comenzile.DOWN ||
           com == Comenzile.DOWN && comanda == Comenzile.UP ||
           com == Comenzile.RIGHT && comanda == Comenzile.LEFT ||
           com == Comenzile.LEFT && comanda == Comenzile.RIGHT)
        {
            return;
        }

        comanda = com;
        ForceMove();

    }

    void ForceMove()
    {//nu merg miscarile la 180
        counter = 0;
        misc = false;
        Misc();
    }

    void OnTriggerEnter(Collider target)
    {
        if( target.tag == Denumiri.Fruit)
        {
            target.gameObject.SetActive(false);
            adauga = true;

            GameplayController.instance.score();
            AudioManager.instance.pickup_sounds();
        }

        if(target.tag == Denumiri.Wall || target.tag == Denumiri.Tail)
        {
            Time.timeScale = 0f;
            AudioManager.instance.dead_sounds();
            //izbirea de zid si fructul luat
        }
    }

}//clas
