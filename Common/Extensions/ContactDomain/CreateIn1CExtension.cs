using Common.DTO.Service1C;
using Domain.Models.Crm;
using Library1C;
using Library1C.DTO;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Extensions.ContactDomain
{
    public static class CreateIn1CExtension
    {
        public static async System.Threading.Tasks.Task<string> CreateIn1C(this Contact contact, UnitOfWork database, TypeAdapterConfig mapper)
        {
            var dto1CContact = new SendPersonTo1CDTO();
            dto1CContact = contact.Adapt<SendPersonTo1CDTO>(mapper);

            if (dto1CContact.isValid == false) throw new ArgumentException(String.Join(" | ", dto1CContact.GetValidateErrors()));

            var guid = await database.Persons.Add2(dto1CContact.Adapt<AddPersonDTO>(mapper));

            return String.IsNullOrEmpty(guid) ? null : guid;
        }
    }
}
