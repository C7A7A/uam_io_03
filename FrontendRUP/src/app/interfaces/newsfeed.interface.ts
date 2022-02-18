export interface INewsfeed {
  title: string[];
  link: string[];
  language: string[];
  description: string[];
  item: INewsfeedItem[];
}

export interface INewsfeedItem {
  'dc:creator': string[];
  'dc:date': string[];
  description: string[];
  title: string[];
  link: string[];
  enclosure: {
    $: {
      url: string;
    }
  }[];
}

