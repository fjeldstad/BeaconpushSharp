namespace BeaconpushSharp.Core
{
    public interface IJsonSerializer
    {
        string Serialize(object data);
        T Deserialize<T>(string data);
    }
}