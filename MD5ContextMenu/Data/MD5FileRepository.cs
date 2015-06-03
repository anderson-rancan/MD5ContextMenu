using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MD5ContextMenu.Model;

namespace MD5ContextMenu.Data
{
    /// <summary>
    /// Representa o repositório de arquivos
    /// </summary>
    internal class MD5FileRepository
    {

        /// <summary>
        /// Evento liberado quando um arquivo é acrescentado no repositório
        /// </summary>
        public event EventHandler<MD5FileAddedEventArgs> FileAdded;

        readonly List<MD5File> files;

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância de MD5ContextMenu.Data.MD5FileRepository
        /// </summary>
        public MD5FileRepository()
        {
            this.files = new List<MD5File>();
        }

        #endregion

        /// <summary>
        /// Acrescenta o arquivo ao repositório
        /// </summary>
        public void AddFile(MD5File file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            if (!this.files.Contains(file))
            {
                this.files.Add(file);

                if (this.FileAdded != null)
                    this.FileAdded(this, new MD5FileAddedEventArgs(file));
            }
        }

        /// <summary>
        /// Verifica se o arquivo já existe no repositório
        /// </summary>
        /// <returns><c>true</c> caso o arquivo exista</returns>
        public bool ContainsFile(MD5File file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            return this.files.Contains(file);
        }

        /// <summary>
        /// Retorna uma cópia da lista de arquivos do repositório
        /// </summary>
        public List<MD5File> GetFiles()
        {
            return new List<MD5File>(this.files);
        }

        /// <summary>
        /// Acrescenta todos arquivos do diretório especificado
        /// </summary>
        /// <param name="directory">Diretório a ser acrescido</param>
        public void AddFolder(System.IO.DirectoryInfo directory)
        {
            this.AddFolder(directory, true);
        }

        /// <summary>
        /// Acrescenta todos arquivos do diretório especificado
        /// </summary>
        /// <param name="directory">Diretório a ser acrescido</param>
        /// <param name="subFolders"><c>true</c> para acrescentar arquivos dos sub-diretórios</param>
        public void AddFolder(System.IO.DirectoryInfo directory, bool subFolders)
        {
            if (directory.Exists)
                foreach (var item in directory.EnumerateFiles("*", subFolders ? System.IO.SearchOption.AllDirectories : System.IO.SearchOption.TopDirectoryOnly))
                    this.AddFile(MD5File.CreateMD5File(item));
        }

        /// <summary>
        /// Retorna representação em texto da instância atual
        /// </summary>
        /// <returns>Representação texto da instância atual</returns>
        public override string ToString()
        {
            return string.Format("Repository with {0} items", this.files.Count);
        }

    }
}
