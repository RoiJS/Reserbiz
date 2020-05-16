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
import { RadDataFormComponent } from 'nativescript-ui-dataform/angular/dataform-directives';

import { AddTenantService } from '@src/app/_services/add-tenant.service';
import { AddContactPersonsService } from '@src/app/_services/add-contact-persons.service';
import { TenantDetailsFormSource } from '@src/app/_models/tenant-details-form.model';
import { Tenant } from '@src/app/_models/tenant.model';
import { GenderEnum } from '@src/app/_enum/gender.enum';

@Component({
  selector: 'ns-tenant-details-form',
  templateUrl: './tenant-details-form.component.html',
  styleUrls: ['./tenant-details-form.component.scss'],
})
export class TenantDetailsFormComponent implements OnInit, OnDestroy {
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

  constructor(
    private addContanctPersonsService: AddContactPersonsService,
    private addTenantService: AddTenantService,
    private translateService: TranslateService
  ) {}

  ngOnInit() {
    this.initTenantDetails();
    this.initTenantForm();

    this._tenantSavedDetailsSub = this.addTenantService.tenantSavedDetails.subscribe(
      () => {
        this.onTenantDetailsSavedEmit();
      }
    );

    this._cancelTenantSavedDetailsSub = this.addTenantService.cancelTenantSavedDetails.subscribe(
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

    this._newTenantDetails.id = 0;
    this._newTenantDetails.firstName = '';
    this._newTenantDetails.middleName = '';
    this._newTenantDetails.lastName = '';
    this._newTenantDetails.gender = GenderEnum.Male;
    this._newTenantDetails.address = '';
    this._newTenantDetails.contactNumber = '';
    this._newTenantDetails.emailAddress = '';
    this._newTenantDetails.photoUrl = '';

    return this._newTenantDetails;
  }

  initTenantForm() {
    this._tenantDetailsForm = new TenantDetailsFormSource(
      this._newTenantDetails.firstName,
      this._newTenantDetails.middleName,
      this._newTenantDetails.lastName,
      this._newTenantDetails.gender,
      this._newTenantDetails.address,
      this._newTenantDetails.contactNumber,
      this._newTenantDetails.emailAddress
    );
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
        this.addContanctPersonsService.contactList.value.length > 0
      );

      this.onCancelTenantDetailsSaved.emit(hasContent);
    }
  }

  get genderOptions(): Array<{ key: GenderEnum; label: string }> {
    return [
      {
        key: GenderEnum.Male,
        label: this.translateService.instant('GENERAL_TEXTS.GENDER.MALE'),
      },
      {
        key: GenderEnum.Female,
        label: this.translateService.instant('GENERAL_TEXTS.GENDER.FEMALE'),
      },
    ];
  }

  get tenantDetailsForm(): TenantDetailsFormSource {
    return this._tenantDetailsForm;
  }
}
