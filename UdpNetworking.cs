using System.Net.Sockets;
using System.Text;

namespace NodeSysCore
{
    public class UdpNetworking
    {        
        private UdpClient udpClient;
        public enum DataType { Float = 0, ByteArray = 1, Color = 2, ColorArray = 3, Debug = 4 };
        public UdpNetworking(string RemoteIp, int RemotePort)
        {
            udpClient = new UdpClient(RemoteIp, RemotePort);
        }

        public void SendByteArray(string ID, byte[] data)
        {
            SendMessage(DataType.ByteArray, ID, data);
        }

        public void SendDebug(string ID, string msg)
        {
            byte[] data = Encoding.ASCII.GetBytes(msg);
            SendMessage(DataType.Debug, ID, data);
        }

        public void SendFloat(string ID, float num)
        {
            byte[] data = System.BitConverter.GetBytes(num);
            SendMessage(DataType.Float, ID, data);
        }

        private void SendMessage(DataType type, string ID, byte[] data)
        {
            byte[] finalData = new byte[2 + ID.Length + data.Length];
            finalData[0] = (byte)type;
            finalData[1] = (byte)ID.Length;
            byte[] IDarr = Encoding.ASCII.GetBytes(ID);
            for (int i = 0; i < IDarr.Length; i++)
            {
                finalData[i + 2] = IDarr[i];
            }
            for (int i = 0; i < data.Length; i++)
            {
                finalData[i + 2 + ID.Length] = data[i];
            }
            udpClient.Send(finalData, finalData.Length);
        }
    }
}
