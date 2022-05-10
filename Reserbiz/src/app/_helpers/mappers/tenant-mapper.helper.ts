import { IBaseEntityMapper } from '../../_interfaces/mappers/ibase-entity-mapper.interface';
import { ContactPerson } from '../../_models/contact-person.model';
import { Tenant } from '../../_models/tenant.model';
import { TenantDetailsFormSource } from '../../_models/form/tenant-details-form.model';
import { IBaseDtoEntityMapper } from '../../_interfaces/mappers/ibase-dto-entity-mapper.interface';
import { TenantDto } from '../../_dtos/tenant-create.dto';
import { GenderEnum } from '../../_enum/gender.enum';

export class TenantMapper
  implements
    IBaseEntityMapper<Tenant>,
    IBaseDtoEntityMapper<Tenant, TenantDetailsFormSource, TenantDto> {
  mapEntity(t: Tenant): Tenant {
    const tenant = new Tenant();

    tenant.id = t.id;
    tenant.firstName = t.firstName;
    tenant.middleName = t.middleName;
    tenant.lastName = t.lastName;
    tenant.gender = t.gender;
    tenant.address = t.address;
    tenant.contactNumber = t.contactNumber;
    tenant.emailAddress = t.emailAddress;
    tenant.photoUrl = t.photoUrl;
    tenant.isActive = t.isActive;
    tenant.isDeletable = t.isDeletable;

    if (t.contactPersons && t.contactPersons.length > 0) {
      tenant.contactPersons = t.contactPersons.map((c: ContactPerson) => {
        const contactPerson = new ContactPerson();

        contactPerson.id = c.id;
        contactPerson.firstName = c.firstName;
        contactPerson.middleName = c.middleName;
        contactPerson.lastName = c.lastName;
        contactPerson.gender = c.gender;
        contactPerson.contactNumber = c.contactNumber;
        contactPerson.tenantId = c.tenantId;

        return contactPerson;
      });
    }

    return tenant;
  }

  initFormSource(): TenantDetailsFormSource {
    const tenantDetailsForm = new TenantDetailsFormSource(
      '',
      '',
      '',
      GenderEnum.Male,
      '',
      '',
      ''
    );

    return tenantDetailsForm;
  }

  mapFormSourceToDto(tenantFormSource: TenantDetailsFormSource): TenantDto {
    const tenantForUpdate = new TenantDto(
      tenantFormSource.firstName,
      tenantFormSource.middleName,
      tenantFormSource.lastName,
      tenantFormSource.gender,
      tenantFormSource.address,
      tenantFormSource.contactNumber,
      tenantFormSource.emailAddress
    );

    return tenantForUpdate;
  }

  mapEntityToFormSource(tenant: Tenant): TenantDetailsFormSource {
    const tenantFormSource = new TenantDetailsFormSource(
      tenant.firstName,
      tenant.middleName,
      tenant.lastName,
      tenant.gender,
      tenant.address,
      tenant.contactNumber,
      tenant.emailAddress
    );

    return tenantFormSource;
  }

  mapFormSourceToEntity(formSource: TenantDetailsFormSource): Tenant {
    throw new Error('Not implemented');
  }
}
