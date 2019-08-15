using AspnetCoreSPATemplate.Services.Common;
using AspnetCoreSPATemplate.Utils;
using AspnetCoreSPATemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SqlServerDataAccess;
using AutoMapper;
using SqlServerDataAccess.EF;

namespace AspnetCoreSPATemplate.Services
{
    
    public class SqlServerContactRepository : RepositoryBase, IContactRepository, IContactModificationRepository
    {
        public SqlServerContactRepository(ContactsMgmtContext db, IMapper mapper)
        {
            Db = db;
            Mapper = mapper;
        }

        public async Task<List<ContactModel>> ListAsync(ContactListRequest rq)
        {
            // Create query
            IQueryable<Contact> query = Db.Contacts
                                          .Skip(rq.SkipCount)
                                          .Take(rq.TakeCount);
            // Retrieve data
            List<Contact> contacts = await query.ToListAsync();
            // Map to model
            List<ContactModel> dtoList = Mapper.Map<List<ContactModel>>(contacts);

            return dtoList;
        }

        public async Task<ContactModel> GetAsync(ContactGetRequest rq)
        {
            // Create query
            IQueryable<Contact> query = Db.Contacts
                                          .Where(c => c.ContactId == rq.Id);
            // Retrieve data
            Contact contact = await query.FirstOrDefaultAsync();
            // Map to model
            ContactModel dto = Mapper.Map<ContactModel>(contact);
            // Detach contact
            Db.Entry(contact).State = EntityState.Detached;

            return dto;
        }

        public async Task<int> ListRecordCountAsync()
        {
            // Create query
            IQueryable<Contact> query = Db.Contacts;
            // Retrieve data
            int recordCount = await query.CountAsync();

            return recordCount;
        }

        public async Task<List<ContactModel>> SearchAsync(ContactSearchRequest rq)
        {
            // Create query
            IQueryable<Contact> query = Db.Contacts
                                          .Where(c => c.FirstName.Contains(rq.Query)
                                                   || c.LastName.Contains(rq.Query)
                                                   || c.Email.Contains(rq.Query)
                                                   || c.Phone1.Contains(rq.Query))
                                          .Skip(rq.SkipCount)
                                          .Take(rq.TakeCount);
            // Retrieve data
            List<Contact> contacts = await query.ToListAsync();
            // Map to model
            List<ContactModel> dtoList = Mapper.Map<List<ContactModel>>(contacts);

            return dtoList;
        }

        public async Task<int> SearchRecordCountAsync(ContactSearchRequest rq)
        {
            // Create query
            IQueryable<Contact> query = Db.Contacts
                                          .Where(c => c.FirstName.Contains(rq.Query)
                                                   || c.LastName.Contains(rq.Query)
                                                   || c.Email.Contains(rq.Query)
                                                   || c.Phone1.Contains(rq.Query));
            // Retrieve data
            int recordCount = await query.CountAsync();

            return recordCount;
        }

        public async Task CreateAsync(ContactCreateRequest rq)
        {
            ContactModel dto = rq.Contact;
            if (IsEmailInUse(dto.Email))
            {
                throw new Exception("Email is in use.");
            }
            else
            {
                Contact result = Mapper.Map<Contact>(dto);
                Db.Contacts.Add(result);
                // Save data
                await Db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(ContactUpdateRequest rq)
        {
            ContactModel dto = rq.Contact;
            // Retrieve old email
            string oldEmail = Db.Contacts
                                .Where(c => c.ContactId == dto.Id)
                                .Select(c => c.Email)
                                .FirstOrDefault();
            // Check whether the new email is the same as the old one or it is being used by others
            if (IsEmailInUse(dto.Email, oldEmail))
            {
                throw new Exception("Email is in use.");
            }
            else
            {
                // It's not in use, then update the contact
                Contact result = Mapper.Map<Contact>(dto);
                Db.Contacts.Attach(result);
                Db.Entry(result).State = EntityState.Modified;
                // Save data
                await Db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(ContactDeleteRequest rq)
        {
            ContactModel dto = rq.Contact;
            Contact result = Mapper.Map<Contact>(dto);
            Db.Remove(result);
            // Save data
            await Db.SaveChangesAsync();
        }

        public bool IsEmailInUse(string email, string oldEmail = null)
        {
            // If new email is the same as old email
            if (oldEmail != null && oldEmail == email)
            {
                // Then it's OK
                return false;
            }
            else
            {
                // If not, check whether it is being used by other contacts
                // Create Query
                var query = Db.Contacts.Where(c => c.Email == email);
                // Retrieve data, do not use async method here
                bool isInUse = query.Any();

                return isInUse;
            }
        }
    }
}
