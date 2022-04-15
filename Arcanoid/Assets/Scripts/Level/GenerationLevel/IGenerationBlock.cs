using UnityEngine;

public interface IGenerationBlock
{
    public void StartGeneration(LevelManager levelManager, CreateBonusAndEffect createBonus);

    public void OnDestroyBlockFromBonus(BlockStats blockStats);
}
