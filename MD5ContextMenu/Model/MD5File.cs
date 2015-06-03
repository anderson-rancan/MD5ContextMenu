using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MD5ContextMenu.Model
{
    /// <summary>
    /// Contém informações MD5 do arquivo especificado
    /// </summary>
    internal class MD5File : IDataErrorInfo
    {

        const string FilePropertyName = "File";
        private string md5Hash = null;

        static readonly string[] ValidatedProperties = 
        { 
            FilePropertyName, 
        };

        #region Static methods

        /// <summary>
        /// Cria novo objeto com informações MD5 do arquivo
        /// </summary>
        /// <param name="file">Arquivo para criação das informações</param>
        /// <returns>Informações MD5 do arquivo</returns>
        public static MD5File CreateMD5File(FileInfo file)
        {
            return new MD5File
            {
                File = file
            };
        }

        /// <summary>
        /// Cria novo objeto com informações MD5 do arquivo
        /// </summary>
        /// <param name="file">Endereço completo do arquivo para criação das informações</param>
        /// <returns>Informações MD5 do arquivo</returns>
        public static MD5File CreateMD5File(string fileName)
        {
            return new MD5File
            {
                File = new FileInfo(fileName)
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Arquivo do qual será fornecida a chave MD5
        /// </summary>
        public FileInfo File { get; protected set; }

        /// <summary>
        /// Retorna a hash MD5 do arquivo
        /// </summary>
        public string MD5Hash
        {
            get { return this.GetMD5(); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Inicializa uma nova instância de MD5ContextMenu.Model.MD5File
        /// </summary>
        protected MD5File() { }

        #endregion

        #region IDataErrorInfo Members

        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        #endregion

        /// <summary>
        /// Retorna condição verdadeira caso a classe esteja válida
        /// </summary>
        public bool IsValid
        {
            get
            {
                foreach (string property in ValidatedProperties)
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        /// <summary>
        /// Verifica se a propriedade é válida
        /// </summary>
        /// <param name="propertyName">Nome da propriedade a ser verificada</param>
        /// <returns>Texto descrevendo ou erro, ou <c>null</c> caso seja válida</returns>
        string GetValidationError(string propertyName)
        {
            if (Array.IndexOf(ValidatedProperties, propertyName) < 0)
                return null;

            string error = null;

            switch (propertyName)
            {
                case FilePropertyName:
                    error = this.ValidateFile();
                    break;
                default:
                    Debug.Fail("Unexpected property being validated on MD5File: " + propertyName);
                    break;
            }

            return error;
        }

        /// <summary>
        /// Verifica se a propriedade File é valida
        /// </summary>
        /// <returns>Texto descrevendo ou erro, ou <c>null</c> caso seja válida</returns>
        string ValidateFile()
        {
            bool result = true;

            try
            {
                if (this.File != null && !string.IsNullOrWhiteSpace(this.File.FullName))
                    result = this.File.Exists;
            }
            catch
            {
                result = false;
            }

            if (!result)
                return MD5ContextMenu.Properties.Resources.FileNotFoundError;
            else
                return null;
        }

        /// <summary>
        /// Calcula e retorna o hash MD5 do arquivo
        /// </summary>
        /// <returns>Hash MD5 do arquivo atual</returns>
        public string GetMD5()
        {
            if (string.IsNullOrWhiteSpace(this.md5Hash))
            {
                if (this.IsValid)
                    using (var md5 = MD5.Create())
                    using (var stream = this.File.OpenRead())
                        this.md5Hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty).ToLowerInvariant();
            }

            return this.md5Hash;
        }

        /// <summary>
        /// Retorna representação em texto da instância atual
        /// </summary>
        /// <returns>Representação texto da instância atual</returns>
        public override string ToString()
        {
            if (this.File == null || string.IsNullOrWhiteSpace(this.File.FullName))
                return base.ToString();
            else
                return this.File.Name;
        }

    }
}
