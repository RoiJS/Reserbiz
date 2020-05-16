export class SpaceTypeDto {
  constructor(
    public name: string,
    public description: string,
    public rate: number,
    public availableSlot: number
  ) {}
}
