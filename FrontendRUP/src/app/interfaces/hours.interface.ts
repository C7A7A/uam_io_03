export interface IHoursRange {
  start: string;
  end: string;
}

export interface IHours {
  mon: IHoursRange;
  tue: IHoursRange;
  wed: IHoursRange;
  thu: IHoursRange;
  fri: IHoursRange;
  sat: IHoursRange;
  sun: IHoursRange;
}

export const DAYS_LABELS_SHORT = [
  'mon', 'tue', 'wed', 'thu', 'fri', 'sat', 'sun'
];

export const DAYS_LABELS_LONG = [
  'Poniedziałek',
  'Wtorek',
  'Środa',
  'Czwartek',
  'Piątek',
  'Sobota',
  'Niedziela'
];

export interface IHourDBFormat {
  day: 'mon' | 'tue' | 'wed' | 'thu' | 'fri' | 'sat' | 'sun';
  StartTime: string;
  EndTime: string;
}
