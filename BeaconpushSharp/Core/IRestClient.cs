namespace BeaconpushSharp.Core
{
    public interface IRestClient
    {
        IResponse Execute(IRequest request);
    }
}