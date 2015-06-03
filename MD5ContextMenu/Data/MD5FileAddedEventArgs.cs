using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MD5ContextMenu.Model;

namespace MD5ContextMenu.Data
{
    /// <summary>
    /// Argumentos do evento de acréscimo de arquivo
    /// </summary>
    internal class MD5FileAddedEventArgs : EventArgs
    {

        public MD5File NewFile { get; private set; }

        public MD5FileAddedEventArgs(MD5File file)
        {
            this.NewFile = file;
        }
        
    }
}
