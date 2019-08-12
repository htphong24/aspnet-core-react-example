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
using SqlServerDataAccess.Entities;
using AutoMapper;

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
                //dto.Id = result.Id;
            }
        }

        public async Task UpdateAsync(ContactUpdateRequest rq)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(ContactDeleteRequest rq)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailInUse(string email)
        {
            // Create Query
            var query = Db.Contacts.Where(c => c.Email == email);
            // Retrieve data, do not use async method here
            bool isInUse = query.Any();

            return isInUse;
        }
    }
}
