using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD5ContextMenu.ViewModel
{
    /// <summary>
    /// Base para toda ViewModel
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {

        #region Constructors

        protected ViewModelBase() { }

        #endregion // Constructor

        #region Properties

        /// <summary>
        /// Retorna o nome deste objeto que será exibido ao usuário final
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion

        /// <summary>
        /// Avisa o desenvolvedor se este objeto não possui uma propriedade pública com o nome especificado
        /// </summary>
        /// <param name="propertyName">Propriedade a ser verificada</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = string.Format("Invalid property name: {0}", propertyName);

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Retorna condição para liberação de exceção ou Debug.Fail
        /// <remarks>
        /// O valor padrão é <c>false</c>, mas pode ser trocada em teste unitário
        /// </remarks>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Libera evento sobre alteração de propriedades
        /// </summary>
        /// <param name="propertyName">Nome da propriedade que foi alterada</param>
        /// <remarks>
        /// Não utilizei métodos anonimos pois compromentem a performance
        /// </remarks>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose() { }

#if DEBUG

        /// <summary>
        /// Útil para ter certeza que a ViewModel foi descartada pelo GC
        /// </summary>
        /// <remarks>
        /// Só ocorre em DEBUG
        /// </remarks>
        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }

#endif

        #endregion
    }
}
