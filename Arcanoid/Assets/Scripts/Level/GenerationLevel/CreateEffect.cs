using UnityEngine;

public class CreateEffect
{
    public void InstantiateEffect(ChapterObject chapterPreset, Transform target, int numberEffect)
    {
        Effect effect = MonoBehaviour.Instantiate(chapterPreset._effects[numberEffect - 1]._prefabEffect).GetComponent<Effect>();
        effect.Stats = target.GetComponent<Stats>();
        effect.StartEffect();
    }        
}