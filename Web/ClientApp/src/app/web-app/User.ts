import { UserChat } from './UserChat';
export interface User {
    Id: string;
    CreationDateTime: Date | string;
    DeleteTime: Date | string | null;
    DeleterId: string | null;
    Deleter: User;
    PhoneNumber: string;
    UserChats: UserChat[];
}
