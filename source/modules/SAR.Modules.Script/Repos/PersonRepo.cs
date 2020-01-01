using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using LiteDB;
using SAR.Libraries.Database.Repos;
using SAR.Modules.Script.Objects;

namespace SAR.Modules.Script.Repos
{
    public class PersonRepo : AbstractRepo<Person>
    {
        public PersonRepo(LiteDatabase db)
            : base(db, "Persons")
        {
        }

        public IEnumerable<Person> GetAll()
        {
            return Collection.FindAll()
                .OrderBy(p => p.FamilyName)
                .ThenBy(p => p.GivenName);
        }
        
    }
}
