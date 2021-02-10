import { AuthToken } from '@src/app/_models/auth-token.model';

describe('Auth Token Model Test Suite', () => {
  it('should return isAuth true when refresh token expiration date is greater than current date', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      'XFstsl6e18llVjdFw2bh0cGueBePNX89h2HrHbUI7lI=',
      new Date('2020-07-24T20:17:34.3117755+08:00')
    );

    authToken.setCurrentDate(new Date('2020-07-19'));

    // Act
    const actualResult = authToken.isAuth;

    // Assert
    expect(actualResult).toBeTrue();
  });

  it('should return isAuth false when refresh token expiration date is less than current date', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      'XFstsl6e18llVjdFw2bh0cGueBePNX89h2HrHbUI7lI=',
      new Date('2020-07-24T20:17:34.3117755+08:00')
    );

    authToken.setCurrentDate(new Date('2020-07-25'));

    // Act
    const actualResult = authToken.isAuth;

    // Assert
    expect(actualResult).toBeFalse();
  });

  it('should return isAuth false when refresh token expiration date is equal to current date', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      'XFstsl6e18llVjdFw2bh0cGueBePNX89h2HrHbUI7lI=',
      new Date('2020-07-19')
    );

    authToken.setCurrentDate(new Date('2020-07-19'));

    // Act
    const actualResult = authToken.isAuth;

    // Assert
    expect(actualResult).toBeFalse();
  });

  it('should return true when token and _accessToken are equal', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      'XFstsl6e18llVjdFw2bh0cGueBePNX89h2HrHbUI7lI=',
      new Date('2020-07-19')
    );

    authToken.setCurrentDate(new Date('2020-07-19'));

    // Act
    const actualResult = authToken.token;

    // Assert
    const expectedResult =
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8';
    expect(actualResult).toEqual(expectedResult);
  });

  it('should return null for token property when _accessToken is null', () => {
    // Arrange
    const authToken = new AuthToken(
      null,
      'XFstsl6e18llVjdFw2bh0cGueBePNX89h2HrHbUI7lI=',
      new Date('2020-07-19')
    );

    authToken.setCurrentDate(new Date('2020-07-19'));

    // Act
    const actualResult = authToken.token;

    // Assert
    expect(actualResult).toEqual(null);
  });

  it('should return true when refreshToken and _refreshToken are equal', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      'XFstsl6e18llVjdFw2bh0cGueBePNX89h2HrHbUI7lI=',
      new Date('2020-07-19')
    );

    // Act
    const actualResult = authToken.refreshToken;

    // Assert
    const expectedResult = 'XFstsl6e18llVjdFw2bh0cGueBePNX89h2HrHbUI7lI=';
    expect(actualResult).toEqual(expectedResult);
  });

  it('should return null for refreshToken property when _refreshToken is null', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      null,
      new Date('2020-07-19')
    );

    // Act
    const actualResult = authToken.refreshToken;

    // Assert
    expect(actualResult).toEqual(null);
  });

  it('should calculate correct timeToExpiry when refreshToken is greater than current date', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      null,
      new Date('2020-07-24')
    );

    authToken.setCurrentDate(new Date('2020-07-19'));

    // Act
    const actualResult = authToken.timeToExpiry;

    // Assert
    expect(actualResult).toEqual(432000000);
  });

  it('should calculate correct timeToExpiry when refreshToken is less than current date', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      null,
      new Date('2020-07-18')
    );

    authToken.setCurrentDate(new Date('2020-07-19'));

    // Act
    const actualResult = authToken.timeToExpiry;

    // Assert
    expect(actualResult).toEqual(-86400000);
  });

  it('should calculate correct timeToExpiry when refreshToken equal to current date', () => {
    // Arrange
    const authToken = new AuthToken(
      'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4IiwidW5pcXVlX25hbWUiOiJyb2FtIiwiZmlyc3ROYW1lIjoiUm9pIExhcnJlbmNlIiwibWlkZGxlTmFtZSI6IlJleWVzIiwibGFzdE5hbWUiOiJBbWF0b25nIiwiZ2VuZGVyIjoiMCIsInVzZXJuYW1lIjoicm9hbSIsIm5iZiI6MTU5NTE2MTA1NCwiZXhwIjoxNTk1MTYxMzU0LCJpYXQiOjE1OTUxNjEwNTR9.zmmx4607zHgWDtael26bxW4e_jtubadmjBcBWr1BDe8',
      null,
      new Date('2020-07-19')
    );

    authToken.setCurrentDate(new Date('2020-07-19'));

    // Act
    const actualResult = authToken.timeToExpiry;

    // Assert
    expect(actualResult).toEqual(0);
  });
});
