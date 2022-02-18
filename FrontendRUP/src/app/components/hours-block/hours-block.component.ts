import { Component, Input, OnInit } from '@angular/core';
import { IHours, DAYS_LABELS_SHORT, DAYS_LABELS_LONG } from '../../interfaces/hours.interface';
import { faClock } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-hours-block',
  templateUrl: './hours-block.component.html',
  styleUrls: ['./hours-block.component.scss']
})
export class HoursBlockComponent implements OnInit {

  @Input() hours: IHours;

  faClock = faClock;

  DAYS_LABELS_SHORT = DAYS_LABELS_SHORT;
  DAYS_LABELS_LONG = DAYS_LABELS_LONG;

  constructor() { }

  ngOnInit(): void {
  }

}
