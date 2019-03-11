using Common.Extensions;
using Common.Extensions.ContactDomain;
using Domain.Models.Crm;
using Library1C;
using ServiceReference1C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Common.BusinessLogicHelpers.IcActions
{
    public class FindGuidAction
    {
        UnitOfWork database;

        string guid;

        public FindGuidAction(UnitOfWork database)
        {
            this.database = database;
        }

        public async Task<string> Find(Contact contact)
        {
            if (contact.Phones() != null) await FindByPhones(contact.Phones().Select(x => x.Value));

            if (contact.Email() != null) await FindByEmails(contact.Email().Select(x => x.Value));

            return guid;
        }

        private async Task FindByPhones(IEnumerable<string> phones)
        {
            foreach (var phone in phones)
            {
                if (String.IsNullOrEmpty(guid))
                {
                    guid = await GetGuid(phone.PhoneWithoutCode(), "");
                }
            }
        }
        private async Task FindByEmails(IEnumerable<string> emails)
        {
            foreach (var email in emails)
            {
                if (String.IsNullOrEmpty(guid))
                {
                    guid = await GetGuid("", email.ClearEmail());
                }
            }
        }

        private async Task<string> GetGuid(string phone, string email)
        {
            flGUIDs query = await database.Persons.GetGuidByPhoneOrEmail(phone, email);

            return query?.GUID;
        }
    }
}
