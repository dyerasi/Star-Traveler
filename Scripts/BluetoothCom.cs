using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechTweaking.Bluetooth;

public class BluetoothCom : MonoBehaviour
{
    // Start is called before the first frame update
    private BluetoothDevice device; //call bluetooth item Device
    public short ax; //datatype
    public short gas; //gas button
    void Awake()
    {
        ax = 0;
        gas = 0;
        BluetoothAdapter.enableBluetooth();
        device = new BluetoothDevice();
        device.Name = "RexPong";
        device.connect(); //conect to device
        device.setEndByte(10);
        device.ReadingCoroutine = ManageConnection;
    }

    public void send()
    {
        if(device != null)
        {
            device.send(new byte[] { 1 });
        }
    }

    IEnumerator ManageConnection(BluetoothDevice device)
    {
        send();
        while(device.IsReading)
        {
            send();
            BtPackets packets = device.readAllPackets(); 
            if(packets!= null)
            {
                send();
                for(int i = 0; i <packets.Count; i++)
                {
                    int indx = packets.get_packet_offset_index(i);
                    int size = packets.get_packet_size(i);
                    string content = System.Text.ASCIIEncoding.ASCII.GetString(packets.Buffer, indx, size);
                    string[] Numbers = content.Split(':');
                    short.TryParse(Numbers[0], out ax);
                    gas = short.Parse(Numbers[1]);
                    send();
                }
            }
            yield return null;
        }
    }
}

//in arduino, set up some variable to save BT.read (if BT.read = 1) it will send data to Arduino, anything 
