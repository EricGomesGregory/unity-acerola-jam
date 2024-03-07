

public interface ICharacter
{
    public IHealth Health { get; }
}

public interface IInteractor
{
    public void Initialize(ICharacter interactor);
    public void OnInteract();
}

public interface IHealth
{
    public float Current { get; }
    public float Total { get; }
    public void Setup(HealthData data);
    public void Reset();
    public void Damage(float value);
    public void Heal(float value);


    public struct HealthData
    {
        public float Total;
        public float Current;
    }
}