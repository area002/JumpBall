using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]


public class Level
{
    //Crear objeto para los niveles
    [Range(1, 11)]
    public int partCount = 11;

    [Range(0, 11)]
    public int deathPartCount = 1;
}

[CreateAssetMenu(fileName="Nivel")]
public class Stage : ScriptableObject
{
   public Color stageBackground = Color.white;

   public Color StageLevelPartColor = Color.white;

    public Color stageBallColor = Color.white;


    public List<Level> levels =new List<Level>();

}
