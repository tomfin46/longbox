using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace longbox.ViewModels
{
    public class NotificationBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // SetField (Name, value); // where there is a data member

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] String property = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(property);
            return true;
        }

        // SetField(()=> somewhere.Name = value; somewhere.Name, value) 
        // Advanced case where you rely on another property
        protected bool SetProperty<T>(T currentValue, T newValue, Action DoSet, [CallerMemberName] String property = null)
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
            DoSet.Invoke();
            RaisePropertyChanged(property);
            return true;
        }

        protected void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class NotificationBase<T> : NotificationBase where T : class
    {
        protected T This;

        public static implicit operator T(NotificationBase<T> thing) { return thing.This; }

        public NotificationBase(T thing)
        {
            This = thing;
        }
    }
}