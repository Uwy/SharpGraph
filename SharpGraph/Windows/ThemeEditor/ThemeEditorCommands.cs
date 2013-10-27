using SharpGraph.ViewModelUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Windows.ThemeEditor
{
    public class ThemeEditorCommands
    {
        public event CommandHandler OnOk;
        public event CommandHandler OnSave;
        public event CommandHandler OnCancel;

        private readonly Command ok;
        public Command Ok
        {
            get
            {
                return this.ok;
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

        private readonly Command cancel;
        public Command Cancel
        {
            get
            {
                return this.cancel;
            }
        }

        public ThemeEditorCommands()
        {
            this.ok = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnOk != null)
                    {
                        this.OnOk(param);
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
            this.cancel = new Command(new Action<object>
            (
                param =>
                {
                    if (this.OnCancel != null)
                    {
                        this.OnCancel(param);
                    }

                }
            ));
        }
    }
}
