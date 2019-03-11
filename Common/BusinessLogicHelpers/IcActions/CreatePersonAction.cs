using Common.DTO.Service1C;
using Domain.Models.Crm;
using Library1C;
using Library1C.DTO;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BusinessLogicHelpers.IcActions
{
    public class CreatePersonAction
    {
        UnitOfWork database;
        TypeAdapterConfig mapper;

        string guid;

        public CreatePersonAction(UnitOfWork database, TypeAdapterConfig mapper)
        {
            this.database = database;
        }

        public async Task<string> Create(Contact contact)
        {
            var dto1CContact = new SendPersonTo1CDTO();
            dto1CContact = contact.Adapt<SendPersonTo1CDTO>(mapper);

            if (dto1CContact.isValid == false) throw new ArgumentException(String.Join(" | ", dto1CContact.GetValidateErrors()));

            var guid = await database.Persons.Add2(dto1CContact.Adapt<AddPersonDTO>(mapper));

            return guid;
        }
    }
}
