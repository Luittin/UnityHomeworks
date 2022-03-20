using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : SelectedMenuItem
{

    public override void OnButtonDown()
    {
        LevelSetting.Instantiate().LevelNumber = _number;
        SceneManager.LoadScene("GameScene");
    }
}
