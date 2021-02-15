import { DurationRangeValueProvider } from '@src/app/_helpers/value_providers/duration-range-value-provider.helper';
import { DurationEnum } from '@src/app/_enum/duration-unit.enum';

describe('Duration Range Value Provider Test Suite', () => {
  it('should return 0 when for default instantiation', function () {
    // Arrange
    const durationRangeValueProvider = new DurationRangeValueProvider();

    // Act
    const actualResult = durationRangeValueProvider.maximum;

    // Assert
    expect(actualResult).toEqual(0);
  });

  it('should return 365 when current duration unit is DurationEnum.Day', function () {
    // Arrange
    const durationRangeValueProvider = new DurationRangeValueProvider(
      DurationEnum.Day
    );

    // Act
    const actualResult = durationRangeValueProvider.maximum;

    // Assert
    expect(actualResult).toEqual(365);
  });

  it('should return 52 when current duration unit is DurationEnum.Week', function () {
    // Arrange
    const durationRangeValueProvider = new DurationRangeValueProvider(
      DurationEnum.Week
    );

    // Act
    const actualResult = durationRangeValueProvider.maximum;

    // Assert
    expect(actualResult).toEqual(52);
  });

  it('should return 12 when current duration unit is DurationEnum.Month', function () {
    // Arrange
    const durationRangeValueProvider = new DurationRangeValueProvider(
      DurationEnum.Month
    );

    // Act
    const actualResult = durationRangeValueProvider.maximum;

    // Assert
    expect(actualResult).toEqual(12);
  });

  it('should return 4 when current duration unit is DurationEnum.Quarter', function () {
    // Arrange
    const durationRangeValueProvider = new DurationRangeValueProvider(
      DurationEnum.Quarter
    );

    // Act
    const actualResult = durationRangeValueProvider.maximum;

    // Assert
    expect(actualResult).toEqual(4);
  });

  it('should return 4 when current duration unit is DurationEnum.Year', function () {
    // Arrange
    const durationRangeValueProvider = new DurationRangeValueProvider(
      DurationEnum.Year
    );

    // Act
    const actualResult = durationRangeValueProvider.maximum;

    // Assert
    expect(actualResult).toEqual(1);
  });
});
