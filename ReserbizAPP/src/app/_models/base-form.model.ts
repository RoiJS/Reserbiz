export abstract class BaseForm<T> {

  isSame(otherObject: T) {
    return JSON.stringify(this) === JSON.stringify(otherObject);
  }

  abstract clone();
}
