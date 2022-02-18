import { faPen } from '@fortawesome/free-solid-svg-icons';
import { EventEmitter } from '@angular/core';
import { Component, OnInit, Input, Output } from '@angular/core';

@Component({
  selector: 'app-profile-block',
  templateUrl: './profile-block.component.html',
  styleUrls: ['./profile-block.component.scss']
})
export class ProfileBlockComponent implements OnInit {

  @Input() hideEditButton = false;
  @Input() blockTitle = '';
  @Output() edit = new EventEmitter();

  faPen = faPen;

  constructor() { }

  ngOnInit(): void {
  }

}
