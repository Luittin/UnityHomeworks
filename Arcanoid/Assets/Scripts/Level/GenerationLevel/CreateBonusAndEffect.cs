using UnityEngine;

public class CreateBonusAndEffect
{
    
    public void CreateBonus(ChapterObject chapterPreset, int numberPresetEffect, Transform transformBlock)
    {
        GameObject prefabBonus = chapterPreset._effects[numberPresetEffect - 1]._prefabBonus;
        Bonus bonus = MonoBehaviour.Instantiate(prefabBonus, transformBlock.position, Quaternion.identity).GetComponent<Bonus>();
        bonus.NumberEffect = numberPresetEffect;
    }

    public void CreateEffect(ChapterObject chapterPreset, Transform target, int numberEffect)
    {
        Effect effect = MonoBehaviour.Instantiate(chapterPreset._effects[numberEffect - 1]._prefabEffect).GetComponent<Effect>();
        effect.Stats = target.GetComponent<Stats>();
        effect.StartEffect();
    }
}