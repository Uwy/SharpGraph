using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph
{
    class ThemeEditorModel
    {
        private Theme theme;

        public ThemeEditorModel(Theme current)
        {
            this.theme = (current.Clone() as Theme).ThrowIfNull();
        }
    }
}
