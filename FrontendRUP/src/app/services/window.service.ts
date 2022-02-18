import { BehaviorSubject, Observable } from 'rxjs';
import { Router, RoutesRecognized } from '@angular/router';
import { Injectable } from '@angular/core';
import { distinctUntilChanged, filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class WindowService {

  location = new BehaviorSubject<string>('');
  title = new BehaviorSubject<string>('');

  constructor(private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof RoutesRecognized),
    ).subscribe((event: RoutesRecognized) => {
      this.location.next(event.url);
      this.title.next(event.state.root.firstChild.data.title);
    });
  }

  public getLocation(): Observable<string> {
    return this.location.pipe(distinctUntilChanged());
  }

  public getTitle(): Observable<string> {
    return this.title.pipe(distinctUntilChanged());
  }

  public blockBodyScroll(): void {
    document.body.style.overflow = 'hidden';
  }

  public unblockBodyScroll(): void {
    document.body.style.overflow = 'auto';
  }

  public scrollToTop(): void {
    window.scrollTo(0, 0);
  }
}
