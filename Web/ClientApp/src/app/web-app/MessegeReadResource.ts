import { User } from "./User";
import { Chat } from "./Chat";
export interface MessegeReadResource {
    id: string;
    creationDateTime: Date | string;
    deleteTime: Date | string | null;
    deleterId: string | null;
    deleter: User;
    senderId: string;
    sender: User;
    reciverId: string;
    reciver: User;
    context: string;
    chatId: string;
    chat: Chat;
}

