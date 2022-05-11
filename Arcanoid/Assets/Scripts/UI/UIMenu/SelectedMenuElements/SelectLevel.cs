using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : SelectedMenuItem
{
    [SerializeField]
    private List<GameObject> _stars;

    public override void SetStars(int countStars)
    {
        if (countStars <= _stars.Count) 
        {
            for (int i = 0; i < countStars; i++)
            {
                _stars[i].SetActive(true);
            } 
        }
    }

    public override void OnButtonDown()
    {
        GameManager.Instantiate().LevelSetting.LevelNumber = _number;
        Debug.Log(GameManager.Instantiate().LevelSetting.LevelNumber);
        SceneManager.LoadScene("GameScene");
    }
}
