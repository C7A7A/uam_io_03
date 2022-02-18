import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-movie-info',
  templateUrl: './movie-info.component.html',
  styleUrls: ['./movie-info.component.scss']
})
export class MovieInfoComponent implements OnInit {

  @Input() genres: string[] = ['Przygodowy', 'Akcja', 'Fantastyka'];
  genreNumber = this.genres.length;
  @Input() duration = 123;

  @Input() description = `Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
  Quisque massa tellus, placerat sit amet vestibulum et, iaculis eu nulla. 
  In non augue mollis, hendrerit elit in, efficitur arcu. Donec in dui augue. 
  Nulla viverra mauris est, ut sodales ante sodales eget. Aliquam euismod ligula vel semper varius. 
  Sed varius mauris eget nibh facilisis sagittis et dapibus quam. Duis eget fringilla nisi. 
  Aliquam vitae magna ornare, pellentesque est non, tristique lacus.`;

  constructor() { }

  ngOnInit(): void {
  }

}
