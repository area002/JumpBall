using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text currentScoreText;
    public Text bestScoreText;

    public Slider slider;
    public Text actualLevel;
    public Text nextLevel;

    public Transform topTransform;
    public Transform goalTransform;

    public Transform Ball;


    // Update is called once per frame
    void Update()
    {
        currentScoreText.text ="Score: "+GameManager.singleton.currentScore;
        bestScoreText.text = "Best: " + GameManager.singleton.bestScore;
        ChangeSliderLevelProgress();
    }

    public void ChangeSliderLevelProgress()
    {
        Debug.Log("Entro al metodo de cargar el Slider");
        actualLevel.text =""+(GameManager.singleton.currentLevel+1);
        nextLevel.text =""+(GameManager.singleton.currentLevel+2);

        float totalDistance = (topTransform.position.y - goalTransform.position.y);
        float distanceleft = totalDistance - (Ball.position.y - goalTransform.position.y);

        float value = (distanceleft / totalDistance);
        slider.value = Mathf.Lerp(slider.value,value,5);
    }
}
