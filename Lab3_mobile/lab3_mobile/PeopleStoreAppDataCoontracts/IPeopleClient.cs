using RestEase;
using System.Threading.Tasks;

namespace PeopleStoreAppDataCoontracts
{
    public interface IPeopleClient
    {
        [Post("people")]
        Task AddPersonAsync([Body] Person p);
    }
}
