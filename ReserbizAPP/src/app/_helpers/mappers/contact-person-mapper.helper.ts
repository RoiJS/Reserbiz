import { IBaseEntityMapper } from '../../_interfaces/mappers/ibase-entity-mapper.interface';

import { IBaseDtoEntityMapper } from '../../_interfaces/mappers/ibase-dto-entity-mapper.interface';

import { ContactPerson } from '../../_models/contact-person.model';
import { ContactPersonDetailsFormSource } from '../../_models/form/contact-person-details-form.model';

import { ContactPersonDto } from '../../_dtos/contact-person.dto';
import { GenderEnum } from '../../_enum/gender.enum';

export class ContactPersonMapper
  implements
    IBaseEntityMapper<ContactPerson>,
    IBaseDtoEntityMapper<
      ContactPerson,
      ContactPersonDetailsFormSource,
      ContactPersonDto
    >
{
  mapEntity(cp: ContactPerson): ContactPerson {
    const contactPerson = new ContactPerson();

    contactPerson.id = cp.id;
    contactPerson.firstName = cp.firstName;
    contactPerson.middleName = cp.middleName;
    contactPerson.lastName = cp.lastName;
    contactPerson.gender = cp.gender;
    contactPerson.contactNumber = cp.contactNumber;
    contactPerson.relation = cp.relation;
    contactPerson.tenantId = cp.tenantId;

    return contactPerson;
  }

  initFormSource(): ContactPersonDetailsFormSource {
    const contactPersonFormSource = new ContactPersonDetailsFormSource(
      '',
      '',
      '',
      GenderEnum.Male,
      '',
      ''
    );

    return contactPersonFormSource;
  }

  mapFormSourceToDto(cpf: ContactPersonDetailsFormSource): ContactPersonDto {
    const contactPersonDto = new ContactPersonDto(
      cpf.firstName,
      cpf.middleName,
      cpf.lastName,
      cpf.gender,
      cpf.contactNumber,
      cpf.relation
    );
    return contactPersonDto;
  }

  mapEntityToFormSource(
    contactPerson: ContactPerson
  ): ContactPersonDetailsFormSource {
    const contactPersonFormSource = new ContactPersonDetailsFormSource(
      contactPerson.firstName,
      contactPerson.middleName,
      contactPerson.lastName,
      contactPerson.gender,
      contactPerson.contactNumber,
      contactPerson.relation
    );

    return contactPersonFormSource;
  }

  mapFormSourceToEntity(
    formSource: ContactPersonDetailsFormSource
  ): ContactPerson {
    throw new Error('Not implemented');
  }
}
