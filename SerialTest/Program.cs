using System;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace SerialTest
{
    public class Program
    {
        const int BUFFER_SIZE = 1024;
        private static OutputPort led = new OutputPort(Pins.GPIO_PIN_D8, false);
        public static void Main()
        {
            SerialPort sp = new SerialPort("COM1", 57600, Parity.None, 8, StopBits.One);
            //sp.DataReceived += sp_DataReceived;
            sp.Open();
            Debug.Print("+-----------------------------------+");
            Debug.Print("+  Hello World                       +");
            Debug.Print("+  Hello Netduino                    +");
            Debug.Print("+  Copyright 2016 (c) AES.           +");
            Debug.Print("+-----------------------------------+");

            Thread.Sleep(Timeout.Infinite);
        }
        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str;
            str = "";
            if (e.EventType == SerialData.Chars)
            {
                int amount;
                byte[] buffer;

                buffer = new byte[BUFFER_SIZE];
                amount = ((SerialPort)sender).Read(buffer, 0, BUFFER_SIZE);
                if (amount > 0)
                {
                    for (int index = 0; index < amount; index++)
                    {
                        str += buffer[index].ToString();
                        str += "";
                    }
                    if (buffer[0] == 1)
                    {
                        led.Write(true);
                    }
                    else
                    {
                        led.Write(false);
                    }
                    Debug.Print("Data: " + str);
                }
            }
        }


    }
}
