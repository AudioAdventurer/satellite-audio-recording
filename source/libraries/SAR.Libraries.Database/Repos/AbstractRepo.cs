using System;
using LiteDB;
using SAR.Libraries.Database.Objects;

namespace SAR.Libraries.Database.Repos
{
    public abstract class AbstractRepo<T>
        where T:AbstractDbObject
    {
        protected readonly LiteCollection<T> Collection;
        private readonly string _collectionName;

        protected AbstractRepo(
            LiteDatabase db, 
            string collectionName)
        {
            _collectionName = collectionName;
            Collection = db.GetCollection<T>(_collectionName);
        }
        
        public T GetById(Guid id)
        {
            var obj = Collection.FindById(id);

            return (T) obj;
        }

        public void Save(T record)
        {
            var existing = GetById(record.Id);

            if (existing == null)
            {
                Collection.Insert(record);
            }
            else
            {
                Collection.Update(record);
            }
        }
    }
}
