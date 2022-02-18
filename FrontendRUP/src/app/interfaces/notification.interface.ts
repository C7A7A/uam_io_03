export enum NotificationType {
  FRIEND_INVITATION = 0,
  STATUS_CHANGE = 1,
  LOBBY_INVITATION = 2,
  TEXT = 3,
}

export interface INotification {
  id?: number;
  content: string;
  type: NotificationType;
  from?: string;
  lobbyName?: string;
}
