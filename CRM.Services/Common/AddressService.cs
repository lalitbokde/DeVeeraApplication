using CRM.Core.Domain.Common;
using CRM.Data.Interfaces;
using CRM.Services.Directory;
using CRM.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Services.Common
{
    public partial class AddressService : IAddressService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : address ID
        /// </remarks>
        private const string ADDRESSES_BY_ID_KEY = "Nop.address.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ADDRESSES_PATTERN_KEY = "Nop.address.";

        #endregion

        #region Fields

        private readonly IRepository<Address> _addressRepository;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
      
       

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="addressRepository">Address repository</param>
        /// <param name="countryService">Country service</param>
        /// <param name="stateProvinceService">State/province service</param>
        /// <param name="addressAttributeService">Address attribute service</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="addressSettings">Address settings</param>
        public AddressService(
            IRepository<Address> addressRepository,
            ICountryService countryService,
            IStateProvinceService stateProvinceService)
        {
           
            this._addressRepository = addressRepository;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes an address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void DeleteAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            _addressRepository.Delete(address);

            //cache
       
        }

        /// <summary>
        /// Gets all Address
        /// </summary>
        /// <returns>Warehouses</returns>
        public virtual IList<Address> GetAllAddresses()
        {
            var query = from wh in _addressRepository.Table
                        orderby wh.FirstName
                        select wh;
            var warehouses = query.ToList();
            return warehouses;
        }

        ///// <summary>
        ///// Gets all Address
        ///// </summary>
        ///// <returns>Warehouses</returns>
        //public virtual IList<Address> GetAllAddress()
        //{
        //    var query = from wh in _addressRepository.Table
        //                orderby wh.FirstName
        //                select wh;
        //    var warehouses = query.ToList();
        //    return warehouses;
        //}


        /// <summary>
        /// Gets total number of addresses by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <returns>Number of addresses</returns>
        public virtual int GetAddressTotalByCountryId(int countryId)
        {
            if (countryId == 0)
                return 0;

            var query = from a in _addressRepository.Table
                        where a.CountryId == countryId
                        select a;
            return query.Count();
        }

        /// <summary>
        /// Gets total number of addresses by state/province identifier
        /// </summary>
        /// <param name="stateProvinceId">State/province identifier</param>
        /// <returns>Number of addresses</returns>
        public virtual int GetAddressTotalByStateProvinceId(int stateProvinceId)
        {
            if (stateProvinceId == 0)
                return 0;

            var query = from a in _addressRepository.Table
                        where a.StateProvinceId == stateProvinceId
                        select a;
            return query.Count();
        }

        /// <summary>
        /// Gets an address by address identifier
        /// </summary>
        /// <param name="addressId">Address identifier</param>
        /// <returns>Address</returns>
        public virtual Address GetAddressById(int addressId)
        {
            if (addressId == 0)
                return null;

           
           return _addressRepository.GetById(addressId);
        }

        /// <summary>
        /// Get Users by identifiers
        /// </summary>
        /// <param name="UserIds">User identifiers</param>
        /// <returns>Users</returns>
        public virtual IList<Address> GetAddressByIds(int[] addressIds)
        {
            if (addressIds == null || addressIds.Length == 0)
                return new List<Address>();

            var query = from c in _addressRepository.Table
                        where addressIds.Contains(c.Id) 
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<Address>();
            foreach (var id in addressIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedUsers.Add(User);
            }
            return sortedUsers;
        }



        /// <summary>
        /// Inserts an address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void InsertAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            address.CreatedOnUtc = DateTime.UtcNow;

            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            _addressRepository.Insert(address);

           
        }

        /// <summary>
        /// Updates the address
        /// </summary>
        /// <param name="address">Address</param>
        public virtual void UpdateAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            //some validation
            if (address.CountryId == 0)
                address.CountryId = null;
            if (address.StateProvinceId == 0)
                address.StateProvinceId = null;

            _addressRepository.Update(address);

           
        }

        /// <summary>
        /// Gets a value indicating whether address is valid (can be saved)
        /// </summary>
        /// <param name="address">Address to validate</param>
        /// <returns>Result</returns>
        public virtual bool IsAddressValid(Address address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            if (string.IsNullOrWhiteSpace(address.FirstName))
                return false;

            if (string.IsNullOrWhiteSpace(address.LastName))
                return false;

            if (string.IsNullOrWhiteSpace(address.Email))
                return false;

            if (string.IsNullOrWhiteSpace(address.Address1))
                return false;

            if (string.IsNullOrWhiteSpace(address.Address2))
                return false;

            if (string.IsNullOrWhiteSpace(address.ZipPostalCode))
                return false;

            
                if (address.CountryId == null || address.CountryId.Value == 0)
                    return false;

                var country = _countryService.GetCountryById(address.CountryId.Value);
                if (country == null)
                    return false;

              
                    var states = _stateProvinceService.GetStateProvincesByCountryId(country.Id);
                    if (states.Any())
                    {
                        if (address.StateProvinceId == null || address.StateProvinceId.Value == 0)
                            return false;

                        var state = states.FirstOrDefault(x => x.Id == address.StateProvinceId.Value);
                        if (state == null)
                            return false;
                    }

            if (string.IsNullOrWhiteSpace(address.City))
                return false;

            if (string.IsNullOrWhiteSpace(address.PhoneNumber))
                return false;

            if (string.IsNullOrWhiteSpace(address.FaxNumber))
                return false;


            return true;
        }

        #endregion
    }
}
