using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SuBienApp.Models
{
    public abstract  class BaseEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] String propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public DateTime Created { get; set; }

        public string CreatedById { get; set; }

        public DateTime Updated { get; set; }

        public string UpdatedById { get; set; }

    }
}
