using Coelsa.Common;
using Coelsa.Models;
using Coelsa.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coelsa.Services
{
    public class ContactService : IContactService
    {

        private readonly IContactRepository _ContactRepository;
        private readonly NLogger _NLogger;
        public ContactService(IContactRepository contactRepository, NLogger NLogger)
        {
            this._ContactRepository = contactRepository;
            this._NLogger = NLogger;
        }


        public async Task<int> InsertContact(ContactModel contact)
        {
            try
            {
                ContactDataValidation(contact);
                var result = await _ContactRepository.InsertAsync(contact);
                if (result == 0) throw new FunctionalException(Constants.MSG_CONTACT_NOT_INSERTED);

                return result;
            }
            catch (Exception ex)
            {
                this._NLogger.Write.Error(ex);
                throw;
            }
        }
        public async Task<int> UpdateContact(int id, ContactModel contact)
        {
            try
            {
                if (id == contact.Id)
                {
                    ContactDataValidation(contact);
                    var contactExist = await _ContactRepository.ExistsAsync(contact.Id);
                    if (contactExist)
                    {
                        return await _ContactRepository.UpdateAsync(contact);
                    }
                    else
                    {
                        throw new FunctionalException(Constants.MSG_CONTACT_NOT_EXISTS);
                    }
                }
                else
                {
                    throw new FunctionalException(Constants.MSG_CONTACT_NOT_MATCH);
                }
            }
            catch (Exception ex)
            {
                this._NLogger.Write.Error(ex);
                throw;
            }
        }

        public async Task<int> DeleteContact(int id)
        {
            try
            {
                var result = await _ContactRepository.DeleteAsync(id);
                if (result == 0) throw new FunctionalException(Constants.MSG_CONTACT_NOT_DELETED);

                return result;
            }
            catch (Exception ex)
            {
                this._NLogger.Write.Error(ex);
                throw;
            }
        }

        public async Task<ContactModel> GetContact(int id)
        {
            try
            {
                var result = await _ContactRepository.GetAsync(id);
                if (result == null) throw new FunctionalException(Constants.MSG_CONTACT_NOT_FOUND);

                return result;
            }
            catch (Exception ex)
            {
                this._NLogger.Write.Error(ex);
                throw;
            }
        }

        public async Task<IEnumerable<ContactModel>> GetAllContact()
        {
            try
            {
                var result = await _ContactRepository.GetAllAsync();
                if (result == null) throw new FunctionalException(Constants.MSG_NOT_RESULTS);

                return result;
            }
            catch (Exception ex)
            {
                this._NLogger.Write.Error(ex);
                throw;
            }
        }
        public async Task<IEnumerable<ContactModel>> GetContactsByCompanyAsync(string company, int pageNumber, int pageSize)
        {
            try
            {
                var result = await _ContactRepository.GetContactsByCompanyAsync(company, pageNumber, pageSize);
                if (result == null) throw new FunctionalException(Constants.MSG_NOT_RESULTS);

                return result;
            }
            catch (Exception ex)
            {
                this._NLogger.Write.Error(ex);
                throw;
            }
        }

        public void ContactDataValidation(ContactModel contact)
        {
            if (string.IsNullOrEmpty(contact.FirstName))
            {
                throw new FunctionalException(string.Format(Constants.MSG_PARAM_REQUIRED,"First Name"));
            }
            if (string.IsNullOrEmpty(contact.LastName))
            {
                throw new FunctionalException(string.Format(Constants.MSG_PARAM_REQUIRED, "Last Name"));
            }
            if (string.IsNullOrEmpty(contact.Company))
            {
                throw new FunctionalException(string.Format(Constants.MSG_PARAM_REQUIRED, "Company"));
            }
            if (string.IsNullOrEmpty(contact.PhoneNumber))
            {
                throw new FunctionalException(string.Format(Constants.MSG_PARAM_REQUIRED, "Phone Number"));
            }
        }

    }
}
