using HRMS.Core.Model;
using System;
using System.Threading.Tasks;

namespace HRMS.Core.Services.Interfaces
{
    public interface IAddressService
    {
        Task<Address> InsertOrUpdate(Address address);
    }
}