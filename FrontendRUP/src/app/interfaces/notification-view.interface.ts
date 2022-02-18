import { INotification } from './notification.interface';

export interface INotificationView {
  content: string;
  buttons: {
    text: string;
    action: (notification: INotification) => void;
    variants?: string;
  }[];
}
