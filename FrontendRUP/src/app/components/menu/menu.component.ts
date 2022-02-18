import { Component, Input, OnInit } from '@angular/core';
import { faEllipsisV, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { IMenuItem } from '../../interfaces/menu-item.interface';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  @Input() icon = faEllipsisV;

  constructor() { }

  ngOnInit(): void {
  }

}
