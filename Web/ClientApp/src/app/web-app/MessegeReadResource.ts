import { User } from "./User";
import { Chat } from "./Chat";
export interface MessegeReadResource {
    Id: string;
    CreationDateTime: Date | string;
    DeleteTime: Date | string | null;
    DeleterId: string | null;
    Deleter: User;
    SenderId: string;
    Sender: User;
    ReciverId: string;
    Reciver: User;
    Context: string;
    ChatId: string;
    Chat: Chat;
}
