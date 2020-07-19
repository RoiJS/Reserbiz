import { NumberFormatter } from '@src/app/_helpers/number-formatter.helper';

describe('Number formatter Helper Test Suite', () => {
  it('should return 5.00 if test value is 5.00', () => {
    // Arrange
    const testValue = 5;

    // Act
    const actualResult = NumberFormatter.formatCurrency(testValue);
    console.log(actualResult);

    // Assert
    expect(actualResult).toEqual('5.00');
  });

  it('should return 5.35 if test value is 5.3456', () => {
    // Arrange
    const testValue = 5.3456;

    // Act
    const actualResult = NumberFormatter.formatCurrency(testValue);

    // Assert
    expect(actualResult).toEqual('5.35');
  });

  it('5.6 should return 5.60 if test value is 5.6', () => {
    // Arrange
    const testValue = 5.6;

    // Act
    const actualResult = NumberFormatter.formatCurrency(testValue);

    // Assert
    expect(actualResult).toEqual('5.60');
  });
});
