using System;
using LiteDB;

namespace SAR.Libraries.Database.Objects
{
    public class AbstractDbObject
    {
        public AbstractDbObject()
        {
            Id = Guid.NewGuid();
        }

        [BsonId]
        public Guid Id { get; set; }
    }
}
