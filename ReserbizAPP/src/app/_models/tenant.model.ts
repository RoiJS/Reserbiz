import { GenderEnum } from '../_enum/gender.enum';
import { ContactPerson } from './contact-person.model';
import { Entity } from './entity.model';

export class Tenant extends Entity {
  public id: number;
  public firstName: string;
  public middleName: string;
  public lastName: string;
  public gender: GenderEnum;
  public address: string;
  public contactNumber: string;
  public emailAddress: string;
  public photoUrl: string;
  public isActive: boolean;
  public isSelected = false;
  public contactPersons: ContactPerson[] = [];

  constructor() {
    super();
    this.id = 0;
    this.firstName = '';
    this.middleName = '';
    this.lastName = '';
    this.gender = GenderEnum.Male;
    this.address = '';
    this.contactNumber = '';
    this.emailAddress = '';
    this.photoUrl = '';
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
    const colorList = [
      '#f6d186',
      '#a8d3da',
      '#f4eeff',
      '#ffaaa5',
      '#beebe9',
      '#f6eec7',
      '#ffd5e5',
      '#ffffdd',
      '#81f5ff',
      '#ffffc5',
    ];

    const randomIndex = this.getNumberFirstDigit();
    // Get color randomly from the list
    return colorList[randomIndex];
  }

  hasContent(): boolean {
    const hasContent = !!(
      this.firstName ||
      this.middleName ||
      this.lastName ||
      this.gender ||
      this.address ||
      this.contactNumber ||
      this.emailAddress
    );

    return hasContent;
  }

  private getNumberFirstDigit(): number {
    const stringId = this.id.toString().split('');
    return +stringId[stringId.length - 1];
  }
}
