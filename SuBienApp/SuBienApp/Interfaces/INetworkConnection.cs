namespace SuBienApp.Interfaces
{
  public  interface INetworkConnection
    {
        bool IsConnected { get; }
        void CheckNetworkConnection();
    }
}
