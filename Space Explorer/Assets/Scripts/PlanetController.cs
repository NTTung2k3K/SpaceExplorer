using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planets;// mang Planet prefabs

    //Queue to hold the planets
    Queue<GameObject> availablePlanets = new Queue<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // them hanh tinh vao queue 
        availablePlanets.Enqueue(Planets[0]);
        availablePlanets.Enqueue(Planets[1]);
        availablePlanets.Enqueue(Planets[2]);

        //goi ham MovePlanetDown moi 20 giay
        InvokeRepeating("MovePlanetDown",0,20f);

    }

    // Update is called once per frame


    //ham dequeue hanh tinh va dat gia tri isMoving cho true
    // vi the hanh tinh co the scroll down man hinh
    void MovePlanetDown()
    {
        EnqueuePlanets();

        // neu Queue rong thi tra ve
        if (availablePlanets.Count == 0)
            return;

        //lay hanh tinh trong queue
        GameObject aPlanet = availablePlanets.Dequeue();

        // dat isMoving la true
        aPlanet.GetComponent<Planet>().isMoving = true;
    }

    // ham enqueue hanh tinh below the screen va khong di chuyen
    void EnqueuePlanets()
    {
        foreach (GameObject aPlanet in Planets)
        {
            //neu planet duoi man hinh va hanh tinh khong di chuyen
            if((aPlanet.transform.position.y <0) && (!aPlanet.GetComponent<Planet>().isMoving))
            {
                // dat lai vi tri hanh tinh
                aPlanet.GetComponent<Planet>().ResetPosition();

                //Enqueue hanh tinh
                availablePlanets.Enqueue(aPlanet);
            }
        }
    }
}
