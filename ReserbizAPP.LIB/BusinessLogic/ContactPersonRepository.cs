using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReserbizAPP.LIB.Interfaces;
using ReserbizAPP.LIB.Models;

namespace ReserbizAPP.LIB.BusinessLogic
{
    public class ContactPersonRepository
        : BaseRepository<ContactPerson>, IContactPersonRepository<ContactPerson>
    {

        public ContactPersonRepository(IReserbizRepository<ContactPerson> reserbizRepository)
            : base(reserbizRepository, reserbizRepository.ClientDbContext)
        {

        }
    }
}