using System.ServiceModel;
using BufferService;
using Common.Interfaces;

namespace ServiceContract
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples", SessionMode = SessionMode.Required,
        CallbackContract = typeof(IHistoricalBufferServiceCallback))]
    public interface IHistoricalBufferService
    {
        [OperationContract(IsOneWay = true)]
        void Connect(string address, string state);

        [OperationContract(IsOneWay = true)]
        void TransferDelta(DeltaCD delta);

        [OperationContract(IsOneWay = true)]
        void ChangeState(string address, string state);
    }
}