import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NewsfeedService } from '../../services/newsfeed.service';
import { INewsfeed } from '../../interfaces/newsfeed.interface';

import * as moment from 'moment';
import 'moment/locale/pl';
import { faArrowLeft, faArrowRight } from '@fortawesome/free-solid-svg-icons';

moment.locale('pl');

@Component({
  selector: 'app-newslist-slider',
  templateUrl: './newslist-slider.component.html',
  styleUrls: ['./newslist-slider.component.scss']
})
export class NewslistSliderComponent implements OnInit {

  faArrowLeft = faArrowLeft;
  faArrowRight = faArrowRight;

  displayArrowLeft = false;
  displayArrowRight = true;

  scrollAmount = 700;

  newses: INewsfeed;

  @ViewChild('wrapper') wrapper: ElementRef;

  constructor(private newsfeedService: NewsfeedService) { }

  ngOnInit(): void {
    this.newsfeedService.getNewses().subscribe((newses) => {
      this.newses = newses;
      this.newses.item = this.newses.item.map(item => {
        item.description[0] = this.extractContent(item.description[0]);
        item['dc:date'][0] = moment(item['dc:date'][0]).format('LL');
        return item;
      });
      console.log(this.newses);
    });
  }

  extractContent(s): string {
    const span = document.createElement('span');
    span.innerHTML = s;
    return span.textContent || span.innerText;
  }

  checkArrowsDisplay(): void {
    if (this.wrapper.nativeElement.scrollLeft) {
      this.displayArrowLeft = true;
    } else {
      this.displayArrowLeft = false;
    }

    if (this.wrapper.nativeElement.scrollLeft + window.innerWidth >= this.wrapper.nativeElement.scrollWidth) {
      this.displayArrowRight = false;
    } else {
      this.displayArrowRight = true;
    }
  }

  handleScroll(amount: number): void {
    this.wrapper.nativeElement.scrollBy({
      left: amount,
      behavior: 'smooth'
    } as ScrollOptions);
    setTimeout(() => {
      this.checkArrowsDisplay();
    }, 500);
  }

  handleLeftArrowClick(): void {
    this.handleScroll(-this.scrollAmount);
  }

  handleRightArrowClick(): void {
    this.handleScroll(this.scrollAmount);
  }
}
