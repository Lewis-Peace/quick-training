export enum UnitOfMeasure {
  KG = 1,
  LB = 2
}

export function UnitOfMeasureToString(uom: UnitOfMeasure) {
  switch (uom) {
    case 1:
      return 'KG';
    case 2:
      return 'LB';
    default:
      return ''
  }
}