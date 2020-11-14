using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain.Lazy
{
    /*The Domain project doesn´t know anythings about the profile picture service
     */
    public interface IValueHolder<T>
    {
        T GetValue(object parameter);
    }
    public class ValueHolder<T> : IValueHolder<T>
    {
        private readonly Func<object, T> getValue;
        //A value holder uses a value loader to retrieve its data in a lazy manner.
        private T value;
        public ValueHolder(Func<object, T> getValue)
        {
            this.getValue = getValue;
        }
        public T GetValue(object parameter)
        {
            if (value == null) 
            {
                value = getValue(parameter);
            }
            return value;
        }
    }
}
