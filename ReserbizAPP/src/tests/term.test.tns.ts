import { Term } from '@src/app/_models/term.model';

describe('Term model Test Suite', () => {
  it('should return hasContent() true when code, name, spaceTypeId or rate has been provided', () => {
    // Arrange
    const testTerm = new Term();
    testTerm.code = 'C-001';
    testTerm.name = 'Term Test';
    testTerm.spaceTypeId = 1;
    testTerm.rate = 500;

    // Act
    const actualResult = testTerm.hasContent();

    // Assert
    expect(actualResult).toBeTrue();
  });

  it('should return hasContent() false when no code, name, spaceTypeId and rate has been provided', () => {
    // Arrange
    const testTerm = new Term();

    // Act
    const actualResult = testTerm.hasContent();

    // Assert
    expect(actualResult).toBeFalse();
  });
});
