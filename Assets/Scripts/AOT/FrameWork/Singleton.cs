

public class Singleton<TInstance> where TInstance : Singleton<TInstance>, new()
{
    private static TInstance _mInstance;
    private static bool HasInstance => _mInstance != null;

    public static TInstance Instance
    {
        get
        {
            if (!HasInstance)
            {
                _mInstance = new TInstance();
            }

            return _mInstance;
        }
    }
}
