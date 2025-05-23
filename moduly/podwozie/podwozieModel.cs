﻿using HAL062app.moduly.komunikacja;
using HAL062app.moduly.manipulator;
using HAL062app.moduly;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;


/*

## Sterowniki silnikow (ID 20-39)
1.Predkosci referencyjne.
Wysylane jako signed int8 w zakresie +/- 100, gdzie 100 to max do przodu, 0 bez ruchu
* ID = 20
* data[0] -speed prawy przod
* data[1] -speed prawy srodek
* data[2] -speed prawy tyl
* data[3] -speed lewy przod 
* data[4] -speed lewy srodek
* data[5] -speed lewy tyl

2. START/STOP.
Wszytskie sterowniki odbieraja ta sama ramke i ustawiaja flagi w
oparciu o dane z niej 
* ID = 21
* data[0] -start / stop(start = 0x01, stop - wszystko inne(domyslnie 0x00) - powtorzone 3 razy dla bezpieczenstwa)
* data[1] - start / stop
* data[2] - start / stop
* data[3] - prad maksymalny(100 - 100 % czyli 50A, 0 - 0 % -0A)
* data[4] - predkosc maksymalna(analogicznie jak z pradem)

4.Polecenie resetu sterowników
* ID - 28
* data[0]
data = 0 - nic nie robi
		data = 1  - resetuje statusy błędów
		data = 2  - przełącza w tryb pracy bez sprzężenie od prędkości (PWM MODE)
		data = 3 - resetuje cały sterownik

*/



namespace HAL062app.moduly.podwozie
{
    public class podwozieModel : MainChannelObserver
    {
        komunikacja.komunikacjaModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;

        public podwozieModel(komunikacjaModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
        }

         public void sendSpeed(motorData data)
        {
            SendMessageToKomunikacja(speedFrame(data));
        }

        Message speedFrame(motorData motorData)
        {
            Message frame = new Message();
            frame.author = 3;
            frame.ID = 20;
            
            byte[] x1 = BitConverter.GetBytes(0);
            byte[] x2 = BitConverter.GetBytes(0);
            byte[] x3 = BitConverter.GetBytes(0);
            byte[] x4 = BitConverter.GetBytes(0);
            byte[] x5 = BitConverter.GetBytes(0);
            byte[] x6 = BitConverter.GetBytes(0);
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(20);

            frame.buffer[2] = (byte)motorData.RF;
            frame.buffer[3] = (byte)motorData.RM;  
            frame.buffer[4] = (byte)motorData.RB;  
            frame.buffer[5] = (byte)motorData.LF; 
            frame.buffer[6] = (byte)motorData.LM;
            frame.buffer[7] = (byte)motorData.LB;
            frame.buffer[8] = (byte)('x');
            frame.buffer[9] = (byte)('x');
            frame.text = new string(frame.encodeMessage());
          
            return frame;
        }



        public void MainChannel(Message message)
        {
            receivedQueue.Enqueue(message); // to powoduje, ze wiadomosc z komunikacji trafia do tej queue

        }


        

        public void SendMessageToKomunikacja(Message message)
        {
            message.author = 3;
            komunikacjaModel.SendMMessageToHALService(message);

        }
        
       

    }
}
