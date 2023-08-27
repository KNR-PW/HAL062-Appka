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
 *  Czemu sterowanie jest w WPF, a nie w zwykłym form? Nie wiem XD
 *  A tak na serio, to posiada chyba bardziej nowoczesny widok i pozwala oddzielić wszystkie funkcje sterowania od form. Z mojego punktu widzenia czytelniejsze było programowanie w ten sposób ~Chmielak
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

    }
}
