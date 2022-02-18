import { faClock } from '@fortawesome/free-solid-svg-icons';
import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IHours } from '../../interfaces/hours.interface';

@Component({
  selector: 'app-modal-preferred-hours',
  templateUrl: './modal-preferred-hours.component.html',
  styleUrls: ['./modal-preferred-hours.component.scss']
})
export class ModalPreferredHoursComponent implements OnInit {

  open = false;

  @Input() hours: IHours;
  @Input() hideCloseButton = true;
  @Output() hoursChange = new EventEmitter<IHours>();
  @Output() accept = new EventEmitter();

  faClock = faClock;

  mondayStartFormControl = new FormControl('', [  ]);
  mondayEndFormControl = new FormControl('', [  ]);

  tuesdayStartFormControl = new FormControl('', [  ]);
  tuesdayEndFormControl = new FormControl('', [  ]);

  wednesdayStartFormControl = new FormControl('', [  ]);
  wednesdayEndFormControl = new FormControl('', [  ]);

  thursdayStartFormControl = new FormControl('', [  ]);
  thursdayEndFormControl = new FormControl('', [  ]);

  fridayStartFormControl = new FormControl('', [  ]);
  fridayEndFormControl = new FormControl('', [  ]);

  saturdayStartFormControl = new FormControl('', [  ]);
  saturdayEndFormControl = new FormControl('', [  ]);

  sundayStartFormControl = new FormControl('', [  ]);
  sundayEndFormControl = new FormControl('', [  ]);

  hoursGroup: FormGroup = null;

  constructor() { }

  ngOnInit(): void {
    this.mondayStartFormControl = new FormControl(this.hours.mon.start, []);
    this.mondayEndFormControl = new FormControl(this.hours.mon.end, []);

    this.tuesdayStartFormControl = new FormControl(this.hours.tue.start, []);
    this.tuesdayEndFormControl = new FormControl(this.hours.tue.end, []);

    this.wednesdayStartFormControl = new FormControl(this.hours.wed.start, []);
    this.wednesdayEndFormControl = new FormControl(this.hours.wed.end, []);

    this.thursdayStartFormControl = new FormControl(this.hours.thu.start, []);
    this.thursdayEndFormControl = new FormControl(this.hours.thu.end, []);

    this.fridayStartFormControl = new FormControl(this.hours.fri.start, []);
    this.fridayEndFormControl = new FormControl(this.hours.fri.end, []);

    this.saturdayStartFormControl = new FormControl(this.hours.sat.start, []);
    this.saturdayEndFormControl = new FormControl(this.hours.sat.end, []);

    this.sundayStartFormControl = new FormControl(this.hours.sun.start, []);
    this.sundayEndFormControl = new FormControl(this.hours.sun.end, []);

    this.hoursGroup = new FormGroup({
      mon: new FormGroup({
        start: this.mondayStartFormControl,
        end: this.mondayEndFormControl
      }),
      tue: new FormGroup({
        start: this.tuesdayStartFormControl,
        end: this.tuesdayEndFormControl
      }),
      wed: new FormGroup({
        start: this.wednesdayStartFormControl,
        end: this.wednesdayEndFormControl
      }),
      thu: new FormGroup({
        start: this.thursdayStartFormControl,
        end: this.thursdayEndFormControl,
      }),
      fri: new FormGroup({
        start: this.fridayStartFormControl,
        end: this.fridayEndFormControl
      }),
      sat: new FormGroup({
        start: this.saturdayStartFormControl,
        end: this.saturdayEndFormControl
      }),
      sun: new FormGroup({
        start: this.sundayStartFormControl,
        end: this.sundayEndFormControl
      })
    });

    this.hoursGroup.valueChanges.subscribe((values: IHours) => {
      this.hoursChange.emit(values);
    });
  }

  _open(): void {
    this.open = true;
  }

  _close(): void {
    this.open = false;
  }

  onAccept() {
    console.log("Accepted preferred hours");
    this.accept.emit(true)
  }
}
