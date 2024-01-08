using DataLayer;
using System;
using System.Collections.Generic;

namespace ServiceLayer
{
    public class DbManager<T, K>
    {
        IDb<T, K> context;

        public DbManager(IDb<T, K> context)
        {
            this.context = context;
        }

        public void Create(T item)
        {
            // Validation + Logging + Authorization + Authentication
            // + Error handling + Transformation from ViewModel to Model ...
            // Wrapper of Data Layer!
            context.Create(item);
        }

        public T Read(K key)
        {
            return context.Read(key);
        }

        public IEnumerable<T> ReadAll()
        {
            return context.ReadAll();
        }

        public void Update(T item)
        {
            context.Update(item);
        }

        public void Delete(K key)
        {
            context.Delete(key);
        }

    }
}
