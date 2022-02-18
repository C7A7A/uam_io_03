import { EventEmitter } from '@angular/core';
import { faTimes } from '@fortawesome/free-solid-svg-icons';
import { Component, OnInit, Input, Output } from '@angular/core';
import { WindowService } from '../../services/window.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {

  @Input() modalTitle = '';
  @Input() hideCloseButton = false;

  @Input() open = false;
  @Output() openChange = new EventEmitter<boolean>();

  faTimes = faTimes;

  constructor(private windowService: WindowService) { }

  ngOnInit(): void {
    if (this.open) {
      this._open();
    }
  }

  public _close(): void {
    this.windowService.unblockBodyScroll();
    this.openChange.emit(false);
    this.open = false;
  }

  public _open(): void {
    this.windowService.blockBodyScroll();
    this.openChange.emit(true);
    this.open = true;
  }

}
