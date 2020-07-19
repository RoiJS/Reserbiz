import { Tenant } from '@src/app/_models/tenant.model';
import { GenderEnum } from '@src/app/_enum/gender.enum';

describe('Tenant model Test Suite', () => {
  it('should return fullName when firstName and lastname were provided', () => {
    // Arrange
    const tenantTest = new Tenant();
    tenantTest.firstName = 'Jonathan Gabbriel';
    tenantTest.lastName = 'Sanders';

    // Act
    const actualResult = tenantTest.fullName;

    // Assert
    const expectedResult = 'Jonathan Gabbriel Sanders';
    expect(actualResult).toEqual(expectedResult);
  });

  it('should return fullName when only firstName has been provided', () => {
    // Arrange
    const tenantTest = new Tenant();
    tenantTest.firstName = 'Jonathan Gabbriel';

    // Act
    const actualResult = tenantTest.fullName;

    // Assert
    const expectedResult = 'Jonathan Gabbriel';
    expect(actualResult).toEqual(expectedResult);
  });

  it('should return fullName when only lastName has been provided', () => {
    // Arrange
    const tenantTest = new Tenant();
    tenantTest.lastName = 'Sanders';

    // Act
    const actualResult = tenantTest.fullName;

    // Assert
    const expectedResult = 'Sanders';
    expect(actualResult).toEqual(expectedResult);
  });

  it('should return hasContent() to true when firstName, middleName, lastName, gender, address, contactNumber or emailAddress are provided', () => {
    // Arrange
    const tenantTest = new Tenant();
    tenantTest.firstName = 'Jonathan Gabbriel';
    tenantTest.middleName = 'Jenkins';
    tenantTest.lastName = 'Sanders';
    tenantTest.gender = GenderEnum.Male;
    tenantTest.address = 'Makati City';
    tenantTest.contactNumber = '09979476937';
    tenantTest.emailAddress = 'jonathan.sanders@gmail.com';

    // Act
    const actualResult = tenantTest.hasContent();

    // Assert
    expect(actualResult).toBeTrue();
  });

  it('should return hasContent() to false when no firstName, middleName, lastName, gender, address, contactNumber or emailAddress has been provided', () => {
    // Arrange
    const tenantTest = new Tenant();

    // Act
    const actualResult = tenantTest.hasContent();

    // Assert
    expect(actualResult).toBeFalse();
  });
});
