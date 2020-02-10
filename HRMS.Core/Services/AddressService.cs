using HRMS.Core.Common;
using HRMS.Core.Model;
using HRMS.Core.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HRMS.Core.Services
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IUniOfWork work;

        public AddressService(IUniOfWork work)
        {
            this.work = work;
        }
        public async Task<Address> GetAddressAsync(Address address)
        {
            var result = (await work.Address.WhereAsync(
                        a =>
                            a.StreetName.ToLower() == address.StreetName.ToLower() &&
                            a.PostalCode.ToLower() == address.PostalCode.ToLower() &&
                            a.IsValid &&
                            a.CityId == address.CityId
                            )).FirstOrDefault();
            return result;

        }
        public async Task<Address> InsertOrUpdate(Address address)
        {

            if (address is null || string.IsNullOrEmpty(address.StreetName) || string.IsNullOrEmpty(address.PostalCode) || address.CityId == 0)
            {
                throw new HRMSException("Please enter correct address(City,street Name)");
            }
            var city = await work.City.GetByIdAsync(address.CityId);

            if (city.RegionId != address.RegionId || city.CountryId != address.CountryId)
            {
                throw new HRMSException("Invalid data for region or Country");
            }

            var currentaddress = await GetAddressAsync(address);

            if (currentaddress is null)
            {
                address.Id = Guid.NewGuid();
                currentaddress = address;
                await work.Address.InsertAsync(currentaddress);
            }
            return currentaddress;
        }

    }
}
