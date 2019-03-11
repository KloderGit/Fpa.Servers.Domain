using Domain.Models.Crm;
using Library1C;
using ServiceReference1C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions.ContactDomain
{
    public static class FindIn1CExtension
    {
        public static async System.Threading.Tasks.Task<string> FindIn1C(this Contact contact, UnitOfWork database)
        {
            string guid = null;

            if (contact.Phones() != null) await FindByPhones(contact.Phones().Select(x => x.Value));

            if (contact.Email() != null) await FindByEmails(contact.Email().Select(x => x.Value));

            return guid;

            async System.Threading.Tasks.Task FindByPhones(IEnumerable<string> phones)
            {
                foreach (var phone in phones)
                {
                    if (String.IsNullOrEmpty(guid))
                    {
                        guid = await GetGuid(phone.PhoneWithoutCode(), "");
                    }
                }
            }
            async System.Threading.Tasks.Task FindByEmails(IEnumerable<string> emails)
            {
                foreach (var email in emails)
                {
                    if (String.IsNullOrEmpty(guid))
                    {
                        guid = await GetGuid("", email.ClearEmail());
                    }
                }
            }

            async Task<string> GetGuid(string phone, string email)
            {
                flGUIDs query = await database.Persons.GetGuidByPhoneOrEmail(phone, email);

                return query?.GUID;
            }
        }
    }
}
