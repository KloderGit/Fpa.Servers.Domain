using Domain.Models.Crm;
using Library1C;
using LibraryAmoCRM.Interfaces;
using LibraryAmoCRM.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions.ContactDomain
{
    public static class UpdateInCRMExtension
    {
        public static async System.Threading.Tasks.Task UpdateInCRM(this Contact contact, IDataManager crm, TypeAdapterConfig mapper)
        {
            var updateItem = contact.GetChanges();
            await crm.Contacts.Update(updateItem.Adapt<ContactDTO>(mapper));
        }
    }
}
