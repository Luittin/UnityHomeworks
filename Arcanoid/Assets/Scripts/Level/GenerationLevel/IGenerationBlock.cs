public interface IGenerationBlock
{
    public void StartGeneration(LevelManager levelManager, CreateBonus createBonus, CreateEffect createEffect);

    public void OnDestroyBlockFromBonus(BlockStats blockStats);
}
