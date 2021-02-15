export abstract class BaseForm<T> {
  isSame(otherObject: T): boolean {
    return JSON.stringify(this) === JSON.stringify(otherObject);
  }

  abstract clone(): T;
}
