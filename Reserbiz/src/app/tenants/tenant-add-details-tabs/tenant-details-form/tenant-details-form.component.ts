import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

import { Subscription } from 'rxjs';
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular';

import { AddTenantService } from '../../../_services/add-tenant.service';
import { AddContactPersonsService } from '../../../_services/add-contact-persons.service';
import { TenantDetailsFormSource } from '../../../_models/form/tenant-details-form.model';
import { Tenant } from '../../../_models/tenant.model';
import { GenderEnum } from '../../../_enum/gender.enum';
import { TenantMapper } from '../../../_helpers/mappers/tenant-mapper.helper';
import { IGenderValueProvider } from '../../../_interfaces/value_providers/igender-value-provider.interface';
import { GenderValueProvider } from '../../../_helpers/value_providers/gender-value-provider.helper';

@Component({
  selector: 'ns-tenant-details-form',
  templateUrl: './tenant-details-form.component.html',
  styleUrls: ['./tenant-details-form.component.scss'],
})
export class TenantDetailsFormComponent
  implements IGenderValueProvider, OnInit, OnDestroy {
  @ViewChild(RadDataFormComponent, { static: false })
  tenantForm: RadDataFormComponent;

  @Output() onTenantDetailsSaved = new EventEmitter<{
    newTenant: Tenant;
    isFormValid: boolean;
  }>();
  @Output() onCancelTenantDetailsSaved = new EventEmitter<boolean>();

  private _newTenantDetails: Tenant;
  private _tenantDetailsForm: TenantDetailsFormSource;
  private _tenantSavedDetailsSub: Subscription;
  private _cancelTenantSavedDetailsSub: Subscription;
  private _genderValueProvider: GenderValueProvider;

  constructor(
    private addContanctPersonsService: AddContactPersonsService,
    private addTenantService: AddTenantService,
    private translateService: TranslateService
  ) {
    this._genderValueProvider = new GenderValueProvider(this.translateService);
  }

  ngOnInit() {
    this.initTenantDetails();
    this.initTenantForm();

    this._tenantSavedDetailsSub = this.addTenantService.entitySavedDetails.subscribe(
      () => {
        this.onTenantDetailsSavedEmit();
      }
    );

    this._cancelTenantSavedDetailsSub = this.addTenantService.entityCancelSaveDetails.subscribe(
      () => {
        this.onCancelTenantDetailsSavedEmit();
      }
    );
  }

  ngOnDestroy() {
    this._tenantSavedDetailsSub.unsubscribe();
    this._cancelTenantSavedDetailsSub.unsubscribe();
  }

  initTenantDetails() {
    this._newTenantDetails = new Tenant();
    return this._newTenantDetails;
  }

  initTenantForm() {
    this._tenantDetailsForm = new TenantMapper().initFormSource();
  }

  onTenantDetailsSavedEmit() {
    if (this.tenantForm) {
      const isFormNotValid = this.tenantForm.dataForm.hasValidationErrors();

      this._newTenantDetails.firstName = this._tenantDetailsForm.firstName;
      this._newTenantDetails.middleName = this._tenantDetailsForm.middleName;
      this._newTenantDetails.lastName = this._tenantDetailsForm.lastName;
      this._newTenantDetails.gender = this._tenantDetailsForm.gender;
      this._newTenantDetails.address = this._tenantDetailsForm.address;
      this._newTenantDetails.contactNumber = this._tenantDetailsForm.contactNumber;
      this._newTenantDetails.emailAddress = this._tenantDetailsForm.emailAddress;

      this.onTenantDetailsSaved.emit({
        newTenant: this._newTenantDetails,
        isFormValid: !isFormNotValid,
      });
    }
  }

  onCancelTenantDetailsSavedEmit() {
    if (this.tenantForm) {
      this._newTenantDetails.firstName = this._tenantDetailsForm.firstName;
      this._newTenantDetails.middleName = this._tenantDetailsForm.middleName;
      this._newTenantDetails.lastName = this._tenantDetailsForm.lastName;
      this._newTenantDetails.gender = this._tenantDetailsForm.gender;
      this._newTenantDetails.address = this._tenantDetailsForm.address;
      this._newTenantDetails.contactNumber = this._tenantDetailsForm.contactNumber;
      this._newTenantDetails.emailAddress = this._tenantDetailsForm.emailAddress;

      const hasContent = !!(
        this._newTenantDetails.hasContent() ||
        this.addContanctPersonsService.entityList.value.length > 0
      );

      this.onCancelTenantDetailsSaved.emit(hasContent);
    }
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return this._genderValueProvider.genderOptions;
  }

  get tenantDetailsForm(): TenantDetailsFormSource {
    return this._tenantDetailsForm;
  }
}
