export interface IBaseFormSource<T> {
  isSame(otherObject: T): boolean;
  clone();
}
