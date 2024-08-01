using HAL062app.moduly.komunikacja;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*  Sterowanie jest dosyć zawiłe w przypadku manipulatora (jeśli chodzi o program) 
 *  Posiadamy 5 plików:  
 *  Model - tutaj jesteśmy - wytłumaczenie niżej
 *  Controller - przesyłanie danych pomiędzy Modelem, a widokiem(Form). Oddzielenie logiki od widoku
 *  Form - ten plik posiada w sobie dwie kontrolki WPF i komunikuje je między sobą oraz między controller
 *  ManipulatorWPF - tutaj jest tylko wizualizacja 3D, ten plik docelowo ma odbierać tylko informację w postaci kątów 
 *  SterowanieWPF - Tutaj jest cały panel sterowania i on docelowo wysyła informacje do modelu (przez controller i form) oraz wysyła aktualnie ustawione kąty do wizualizacji (za pomocą form)  
 * ... No w sumie to posiadamy 7 plików XD
 * klasa position oraz sequence, z której korzystać ma kilka plików
 * 
 *  Czemu sterowanie jest w WPF, a nie w zwykłym form? 
 *  Nowoczesny widok i pozwala oddzielić wszystkie funkcje sterowania od form. Z mojego punktu widzenia czytelniejsze było programowanie w ten sposób 
 *  
 * Model odbiera kinematykę (wszystkie kąty i xyz dla inverse kinematic, a także sekwencje, którą ma odtworzyć)
 * Oblicza wszystko na kąty, które przesyła do wizualizacji oraz do komunikacji w postaci ramek
 * 
 * Ramki też zostały tutaj popierdolone, a w czasie pisania tej aplikacji nikt nie miał kodu, który jest wgrany na manipulator
 * Wiedziałeś, że manipulator posiada policzoną kinematykę, która jest spierdolona i nikt nie może jej poznać (ponieważ nie da się ściągnąć kodu z płytek) 
 * Dlatego zdecydowałem się na wysyłanie ramek tylko z kątami, a kinematyka będzie liczona tutaj.
 * 
 * 
 */

namespace HAL062app.moduly.manipulator
{
    public class manipulatorModel : MainChannelObserver
    {
        komunikacja.komunikacjaModel komunikacjaModel;
        private ConcurrentQueue<Message> receivedQueue;

        public manipulatorModel(komunikacjaModel komunikacja)
        {
            receivedQueue = new ConcurrentQueue<Message>();
            this.komunikacjaModel = komunikacja;
        }
        public void MainChannel(Message message)
        {
            receivedQueue.Enqueue(message);

        }

        public void SendMessageToKomunikacja(Message message)
        {
            message.author = 4;
            komunikacjaModel.SendMMessageToHALService(message);

        }

        async public void SendAnglesToManipulator(Position position)
        {
            Message[] frames = new Message[3];
            frames = angleFrames(position);

            SendMessageToKomunikacja(frames[0]);
            await Task.Delay(50);
            SendMessageToKomunikacja(frames[1]);
            await Task.Delay(50);
            SendMessageToKomunikacja(frames[2]);
            await Task.Delay(50);
        }

        Message[] angleFrames(Position position)
        {
            Message[] frames = new Message[3];
            for (int i = 0; i < 3; i++)
            {
                frames[i] = new Message();
                frames[i].author = 4;
                frames[i].ID = i+129;
            }
            byte[] x1 = BitConverter.GetBytes(0);
            byte[] x2 = BitConverter.GetBytes(0);
            byte[] x3 = BitConverter.GetBytes(0);
            byte[] x4 = BitConverter.GetBytes(0);
            byte[] x5 = BitConverter.GetBytes(0);
            byte[] x6 = BitConverter.GetBytes(0);
            frames[0].buffer[0] = (byte)('#');
            frames[1].buffer[0] = (byte)('#');
            frames[2].buffer[0] = (byte)('#');

            frames[0].buffer[1] = (byte)(129);
            frames[1].buffer[1] = (byte)(130);
            frames[2].buffer[1] = (byte)(131);

            x1 = BitConverter.GetBytes((float)((position.joints[0] -position.relative0[0])* Math.PI / 180.0));
            x2 = BitConverter.GetBytes((float)((position.joints[1] -position.relative0[1])* Math.PI / 180.0));
            x3 = BitConverter.GetBytes((float)((position.joints[2] -position.relative0[2])* Math.PI / 180.0));
            x4 = BitConverter.GetBytes((float)((position.joints[3] -position.relative0[3])* Math.PI / 180.0));
            x5 = BitConverter.GetBytes((float)((position.joints[4] -position.relative0[4])* Math.PI / 180.0));
            x6 = BitConverter.GetBytes((float)((position.joints[5] -position.relative0[5])* Math.PI / 180.0));

            for (int j = 0; j < 4; j++)
            {
                frames[0].buffer[2+j] = x1[3 - j];
                frames[0].buffer[2 + 4 + j] = x2[3 - j];
                frames[1].buffer[2 + j] = x3[3 - j];
                frames[1].buffer[2 + 4 + j] = x4[3 - j];
                frames[2].buffer[2 + j] = x5[3 - j];
                frames[2].buffer[2 + 4 + j] = x6[3 - j];
            }

            frames[0].text = new string(frames[0].encodeMessage());
            frames[1].text = new string(frames[1].encodeMessage());
            frames[2].text = new string(frames[2].encodeMessage());
            return frames;
        }

    }
}
