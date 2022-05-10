export class BaseWidgetComponent {
  protected _isBusy = false;
  protected _entityCount = 0;

  get isBusy(): boolean {
    return this._isBusy;
  }

  get entityCount(): string {
    return this._entityCount > 99 ? '99+' : this._entityCount.toString();
  }

  get actualEntityCount(): number {
    return this._entityCount;
  }
}
