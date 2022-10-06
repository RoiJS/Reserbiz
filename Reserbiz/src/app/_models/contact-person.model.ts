import { GenderEnum } from '../_enum/gender.enum';
import { Entity } from './entity.model';

export class ContactPerson extends Entity {
  public id: number;
  public firstName: string;
  public middleName: string;
  public lastName: string;
  public gender: GenderEnum;
  public contactNumber: string;
  public relation: string;
  public tenantId: number;
  public isSelected: boolean;
  public isActive: boolean;

  constructor() {
    super();
    this.id = 0;
    this.firstName = '';
    this.middleName = '';
    this.lastName = '';
    this.gender = GenderEnum.Male;
    this.contactNumber = '';
    this.relation = '';
    this.tenantId = 0;
    this.isSelected = false;
    this.isActive = false;
  }

  get fullName(): string {
    return `${this.firstName} ${this.lastName}`;
  }

  get nameInitials(): string {
    return `${this.firstName[0]}${this.lastName[0]}`;
  }

  get genderName(): string {
    return GenderEnum[this.gender];
  }

  get photoBackgroundColor(): string {
    const randomIndex = this.getNumberFirstDigit();
    // Get color randomly from the list
    return this.colorList[randomIndex];
  }

  private getNumberFirstDigit(): number {
    const stringId = this.id.toString().split('');
    return +stringId[stringId.length - 1];
  }
}
