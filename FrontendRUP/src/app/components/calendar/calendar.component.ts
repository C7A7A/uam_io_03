import { faAngleRight } from '@fortawesome/free-solid-svg-icons';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit } from '@angular/core';
import { CalendarService } from '../../services/calendar.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {

  today = new Date();

  currentMonth = 0;
  nextMonth = 0;
  previousMonth = 0;

  currentMonthName = '';
  nextMonthName = '';
  previousMonthName = '';

  days: number[][] = [];
  daysOfWeek = [
    'Pon', 'Wto', 'Śro', 'Czw', 'Pią', 'Sob', 'Nie'
  ];

  faAngleLeft = faAngleLeft;
  faAngleRight = faAngleRight;

  constructor(
    private calendarService: CalendarService,
  ) { }

  ngOnInit(): void {
    this.calculateAttributesFromToday();
  }

  calculateAttributesFromToday(): void {
    this.currentMonth = this.today.getMonth();
    this.previousMonth = (this.currentMonth + 11) % 12;
    this.nextMonth = (this.currentMonth + 1) % 12;

    this.currentMonthName = this.monthName(this.currentMonth);
    this.nextMonthName = this.monthName(this.nextMonth);
    this.previousMonthName = this.monthName(this.previousMonth);

    this.days = this.generateDaysArray(this.today);
  }

  monthName(id: number): string {
    switch (id) {
      case 0: return 'Styczeń';
      case 1: return 'Luty';
      case 2: return 'Marzec';
      case 3: return 'Kwiecień';
      case 4: return 'Maj';
      case 5: return 'Czerwiec';
      case 6: return 'Lipiec';
      case 7: return 'Sierpień';
      case 8: return 'Wrzesień';
      case 9: return 'Październik';
      case 10: return 'Listopad';
      case 11: return 'Grudzeń';
      default: return 'NOT_KNOWN_ID';
    }
  }

  generateDaysArray(date: Date): number[][] {
    const outArray: number[][] = [];
    let indicator = new Date(date.getFullYear(), date.getMonth(), 1);
    let i = 0;
    do {
      if (!outArray[i]) {
        outArray.push([]);
      }
      outArray[i].push(indicator.getDate());
      if (indicator.getDay() === 0) {
        i++;
      }
      indicator = this.addDay(indicator);
    } while (indicator.getDate() !== 1);
    return outArray;
  }

  addDay(date: Date): Date {
    const newDate = new Date(date.valueOf());
    newDate.setDate(newDate.getDate() + 1);
    return newDate;
  }

  addMonth(date: Date): Date {
    return new Date(date.getFullYear(), (date.getMonth() + 1) % 12, 1);
  }

  substractMonth(date: Date): Date {
    return new Date(date.getFullYear(), (date.getMonth() + 11) % 12, 1);
  }

  moveToNextMonth(): void {
    this.today = this.addMonth(this.today);
    this.calculateAttributesFromToday();
  }

  moveToPreviousMonth(): void {
    this.today = this.substractMonth(this.today);
    this.calculateAttributesFromToday();
  }

  /* USER INTERACTION */

  openDay(day: number): void {
    this.calendarService.getEvents().subscribe((events) => {
      console.log(events);
    });
  }
}
