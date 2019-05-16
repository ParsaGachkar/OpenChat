import { User } from "./User";
import { Chat } from "./Chat";
export interface UserChat {
    Id: string;
    UserId: string;
    User: User;
    ChatId: string;
    Chat: Chat;
}
