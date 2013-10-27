using SharpGraph.ViewModelUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Windows.MainWindow
{
    class MainCommands
    {
        public event CommandHandler OnReset;
        public event CommandHandler OnSave;
        public event CommandHandler OnStart;
        public event CanBeExecutedHandler CanStart;
        public event CommandHandler OnStop;
        public event CanBeExecutedHandler CanStop;
        public event CommandHandler OnEditTheme;

        private readonly Command editTheme;
        public Command EditTheme
        {
            get
            {
                return this.editTheme;
            }
        }

        private readonly Command save;
        public Command Save
        {
            get
            {
                return this.save;
            }
        }

        private readonly Command start;
        public Command Start
        {
            get
            {
                return this.start;
            }
        }

        private readonly Command stop;
        public Command Stop
        {
            get
            {
                return this.stop;
            }
        }

        private readonly Command reset;
        public Command Reset
        {
            get
            {
                return this.reset;
            }
        }

        public MainCommands()
        {
            this.editTheme = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnEditTheme != null)
                    {
                        this.OnEditTheme(param);
                    }
                }

            ));
            this.reset = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnReset != null)
                    {
                        this.OnReset(param);
                    }
                }

            ));
            this.save = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnSave != null)
                    {
                        this.OnSave(param);
                    }
                }
            ));
            this.start = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnStart != null)
                    {
                        this.OnStart(param);
                    }
                }
            ),
            new Predicate<object>
            (
                param =>
                {
                    if (this.CanStart != null)
                    {
                        return this.CanStart(param);
                    }
                    return true;
                }
            ));
            this.stop = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnStop != null)
                    {
                        this.OnStop(param);
                    }
                }
            ),
            new Predicate<object>
            (
                param =>
                {
                    if (this.CanStop != null)
                    {
                        return this.CanStop(param);
                    }
                    return true;
                }
            ));
        }
    }
}
