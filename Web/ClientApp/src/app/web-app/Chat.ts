import { Messege } from './Messege';
import { UserChat } from './UserChat';
import { User } from "./User";
export interface Chat {
    Id: string;
    CreationDateTime: Date | string;
    DeleteTime: Date | string | null;
    DeleterId: string | null;
    Deleter: User;
    UserChats: UserChat[];
    Users: User[];
    Messeges: Messege[];
}
