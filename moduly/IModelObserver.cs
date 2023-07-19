using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Z tej klasy powstają wszystkie moduły oprócz komunikacji. Komunikacja jest w stanie wywołać funkcję MainChannel
// Każdy moduł, który powstał z tej klasy będzie powiadomiony o użyciu tej funkcji i odbierze wiadomość
//
namespace HAL062app.moduly
{
   
    public interface MainChannelObserver
    {
        void MainChannel(Message message);
        
    }

}
