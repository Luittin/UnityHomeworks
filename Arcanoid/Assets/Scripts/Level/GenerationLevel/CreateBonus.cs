using UnityEngine;

public class CreateBonus
{
    public void InstantiateBonus(ChapterObject chapterPreset, int numberPresetEffect, Transform transformBlock)
    {
        GameObject prefabBonus = chapterPreset._effects[numberPresetEffect - 1]._prefabBonus;
        Bonus bonus = MonoBehaviour.Instantiate(prefabBonus, transformBlock.position, Quaternion.identity).GetComponent<Bonus>();
        bonus.TargetEffect = chapterPreset._effects[numberPresetEffect - 1]._target;
        bonus.NumberEffect = numberPresetEffect;
    }
}