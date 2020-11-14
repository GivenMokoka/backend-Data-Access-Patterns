using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Domain.Lazy
{
    /*The Domain project doesn´t know anythings about the profile picture service
     * The benefif of this approach is that we were no longer coupled to the way that we were loading 
     * our data before the data is passed back to the consumer oof the repository, we´re goona hood on one of 
     * these value holders so that one of the properties on our entity can make use of that value holder and lazy 
     * loda the data as the consumer  request that
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
