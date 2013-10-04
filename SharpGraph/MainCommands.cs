using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SharpGraph
{
    class MainCommands
    {
        private readonly ICommand save;
        public ICommand Save
        {
            get
            {
                return this.save;
            }
        }
        public MainCommands()
        {
            this.save = new Command(new Action<object>
            (
                param => Console.WriteLine("It Works !")
            ));
        }
    }
}
