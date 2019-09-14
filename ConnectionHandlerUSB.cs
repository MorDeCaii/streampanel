using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iMobileDevice;
using iMobileDevice.iDevice;
using iMobileDevice.Lockdown;
using iMobileDevice.MobileSync;
namespace Streampanel
{
    public static class ConnectionHandlerUSB
    {
        //public static bool isConnectedUsb = false;
        //public static PictureBox pictureUsbStatus;
        public static iDeviceHandle device;                  //Device with detected app
        public static iDeviceConnectionHandle connection;    //Device connection handler
        public static Label status;                          //Status label

        private const int PORT = 8834;

        public static void Init(Timer _timerLookForConnection, Timer _timerConnection, Label _status)
        {
            //pictureUsbStatus = _pictureUsbStatus;
            BeginListen();
            _timerLookForConnection.Start();
            _timerConnection.Start();
            status = _status;
        }

        private static void BeginListen()
        {
            var idevice = LibiMobileDevice.Instance.iDevice;
            idevice.idevice_event_subscribe(EventCallback(), new IntPtr());
        }

        public static List<string> GetIDs()
        {
            List<string> deviceNames = new List<string>();
            ReadOnlyCollection<string> udids;
            int count = 0;

            var idevice = LibiMobileDevice.Instance.iDevice;

            var ret = idevice.idevice_get_device_list(out udids, ref count);
            if (ret == iDeviceError.NoDevice)
            {
                // Not actually an error in our case
                return null;
            }

            ret.ThrowOnError();

            return udids.ToList();
        }

        private static iDeviceEventCallBack EventCallback()
        {
            return (ref iDeviceEvent devEvent, IntPtr data) =>
            {
                switch (devEvent.@event)
                {
                    case iDeviceEventType.DeviceAdd:
                        break;
                    case iDeviceEventType.DeviceRemove:
                        break;
                    default:
                        return;
                }
            };
        }

        public static void Connect(string newUdid)
        {
            var idevice = LibiMobileDevice.Instance.iDevice;
            idevice.idevice_new(out iDeviceHandle deviceHandle, newUdid).ThrowOnError();
            var error = idevice.idevice_connect(deviceHandle, PORT, out iDeviceConnectionHandle connectionHandle);
            if (error != iDeviceError.Success)
            {
                Console.WriteLine("Error");
                return;
            }

            device = deviceHandle;
            connection = connectionHandle;

            status.Text = "Connected via USB";

            //Clear app queue before recieving data
            byte[] cmd = { 2 };
            SendBytes(cmd);
        }

        public static void CheckConnection()
        {
            if (device == null)
            {
                connection = null;
                return;
            }

            var idevice = LibiMobileDevice.Instance.iDevice;
            var error = idevice.idevice_connect(device, PORT, out iDeviceConnectionHandle _connection);
            if (error != iDeviceError.Success)
            {
                device = null;
                connection = null;
                status.Text = "Lost connection";
            }
        }

        public static void SendBytes(byte[] msg)
        {
            if (device != null)
            {
                var idevice = LibiMobileDevice.Instance.iDevice;
                List<byte> res = new List<byte>(msg);
                res.Add(255);
                byte[] bytesPending = res.ToArray();
                uint sentBytes = 0;
                idevice.idevice_connect(device, PORT, out connection);
                var error = idevice.idevice_connection_send(connection, bytesPending, (uint)bytesPending.Length, ref sentBytes);
                if (error != iDeviceError.Success)
                    Console.WriteLine("Send error!");

                ReceiveResponse();
            }
            else
                status.Text = "No device detected";
        }

        private static void ReceiveResponse()
        {
            var idevice = LibiMobileDevice.Instance.iDevice;
            Task.Run(() =>
            {
                while (true)
                {
                    uint receivedBytes = 0;
                    byte[] response = new byte[64];
                    idevice.idevice_connection_receive(connection, response, (uint)response.Length, ref receivedBytes);

                    if (receivedBytes <= 0)
                    {
                        Console.WriteLine("No data recieved!");
                        device = null;
                        connection = null;
                        break;
                    }

                    // Handle received bytes
                    /*string res = "";
                    for (int i = 0; i < response.Length; i++)
                        res += " | " + response[i].ToString();
                    Console.WriteLine("Command: "+res);*/

                    if (response.Length > 1 && response[0] == 1)
                    {
                        if (response[1] != 0)
                            Console.WriteLine("Button "+response[1]+" was pressed!");
                        break;
                    }

                    break;
                }
            });
        }
    }
}
