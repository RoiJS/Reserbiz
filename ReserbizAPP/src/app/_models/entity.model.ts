import { IEntity } from '../_interfaces/ientity.interface';

export abstract class Entity implements IEntity {
  public id: number;
  public isActive = true;
  public isDeletable = true;
  public isSelected: boolean;

  public colorList = [
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
}
