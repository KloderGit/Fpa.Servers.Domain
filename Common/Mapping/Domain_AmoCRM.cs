﻿using Domain.Models.Crm;
using LibraryAmoCRM.Models;
using LibraryAmoCRM.Models.Fields;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Mapping
{
    public class Domain_AmoCRM
    {
        public Domain_AmoCRM(TypeAdapterConfig config)
        {
            config.NewConfig<ContactDTO, Contact>()
                .Ignore(dest => dest.ChangeValueDelegate)
                .Map(dest => dest.Leads, src => src.Leads != null ? src.Leads.IDs.Select(c => new Lead { Id = c }) : null)
                .Map(dest => dest.Company, src => src.Company != null ? new Company { Id = src.Company.Id.Value } : null)
                .Map(dest => dest.Fields, src => src.CustomFields != null ? src.CustomFields : null)
            ;

            config.NewConfig<Contact, ContactDTO>()
                .Map(dest => dest.Leads, src =>  src.Leads)
                .Map(dest => dest.CustomFields, src => src.Fields)
                .IgnoreIf((src, dest) => src.Leads == null , dest => dest.Leads);
            ;

            config.NewConfig<List<Lead>, LeadsField>()
                .Map(dest => dest.IDs, src => src.Select(x=>x.Id))
            ;

            config.NewConfig<LeadDTO, Lead>()
                .Map(dest => dest.Company, src => src.Company != null ? new Company { Id = (int)src.Company.Id } : null)
                .Map(dest => dest.Contacts, src => src.Contacts != null ? src.Contacts.IDs.Select(c => new Contact { Id = c }) : null)
                .Map(dest => dest.MainContact, src => src.MainContact != null ? new Contact { Id = (int)src.MainContact.Id } : null)
                .Map(dest => dest.Fields, src => src.CustomFields ?? null)
            ;

            config.NewConfig<Lead, LeadDTO>()
                .Map(dest => dest.Company, src => src.Company != null ? new CompanyField { Id = (int)src.Company.Id } : null)
                .Map(dest => dest.Contacts, src => src.Contacts)
                .Map(dest => dest.MainContact, src => src.MainContact != null ? new MainContactField { Id = (int)src.MainContact.Id } : null)
                .Map(dest => dest.CustomFields, src => src.Fields ?? null)
            ;
            config.NewConfig<List<Contact>, ContactsField>()
                .Map(dest => dest.IDs, src => src.Select(x => x.Id))
            ;
        }
    }
}
