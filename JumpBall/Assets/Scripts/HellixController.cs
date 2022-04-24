using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 starPosition;
    public Transform topTransform;
    public Transform goalTransform;

    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();


    public float helixDistance;
    private List<GameObject> spawnedLevels = new List<GameObject>();

    private void Awake()
    {   //Saber posicion inicial del Helix
        starPosition = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
        LoadStage(0);
    }

   

    // Update is called once per frame
    void Update()
    {
        //Si hacemos click
        if (Input.GetMouseButton(0))
        {
            Vector2 currentTapPosition = Input.mousePosition;
            //si no hemos tocado la pantalla
            if (lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
            }
            //saber la distancia que se ha movido
            float distance = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;

            //rotar el helix
            transform.Rotate(Vector3.up * distance);

            //saber si dejamos de pulsar la pantalla
            if (Input.GetMouseButtonUp(0))
            {
                lastTapPosition = Vector2.zero;
            }

        }
    }
    //Cargar el nivel
    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];
        if (stage == null)
        {
            Debug.Log("No hay mundo creado");
            return;
        }

       Camera.main.backgroundColor = allStages[stageNumber].stageBackground;
       FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;
        transform.localEulerAngles = starPosition;

        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        float levelDistance = helixDistance / stage.levels.Count;
        float spawnPosy = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosy -= levelDistance;
            GameObject level = Instantiate(helixLevelPrefab, transform);
            level.transform.localPosition = new Vector3(0, spawnPosy, 0);
            spawnedLevels.Add(level);

            int partToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count < partToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            List<GameObject> leftParts = new List<GameObject>();
            foreach(Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].StageLevelPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }

            List<GameObject> deathParts = new List<GameObject>();
            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject randomPart =  leftParts[(Random.Range(0, leftParts.Count))];
                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }

        }

    }


}
