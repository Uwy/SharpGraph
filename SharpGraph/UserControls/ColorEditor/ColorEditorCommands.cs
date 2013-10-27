using SharpGraph.ViewModelUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.UserControls.ColorEditor
{
    public class ColorEditorCommands
    {
        public event CommandHandler OnDelete;
        public event CanBeExecutedHandler CanDelete;
        public event CommandHandler OnUp;
        public event CanBeExecutedHandler CanUp;
        public event CommandHandler OnDown;
        public event CanBeExecutedHandler CanDown;

        private readonly Command delete;
        private readonly Command up;
        private readonly Command down;

        public Command Delete
        {
            get
            {
                return this.delete;
            }
        }

        public Command Up
        {
            get
            {
                return this.up;
            }
        }

        public Command Down
        {
            get
            {
                return this.down;
            }
        }

        public ColorEditorCommands()
        {
            this.delete = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnDelete != null)
                    {
                        this.OnDelete(param);
                    }
                }

            ),
            new Predicate<object>
            (
                param =>
                {
                    if (this.CanDelete != null)
                    {
                        return this.CanDelete(param);
                    }
                    return true;
                }
            ));
            this.up = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnUp != null)
                    {
                        this.OnUp(param);
                    }
                }

            ),
            new Predicate<object>
            (
                param =>
                {
                    if (this.CanUp != null)
                    {
                        return this.CanUp(param);
                    }
                    return true;
                }
            ));
            this.down = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnDown != null)
                    {
                        this.OnDown(param);
                    }
                }

            ),
            new Predicate<object>
            (
                param =>
                {
                    if (this.CanDown != null)
                    {
                        return this.CanDown(param);
                    }
                    return true;
                }
            ));
        }
    }
}
