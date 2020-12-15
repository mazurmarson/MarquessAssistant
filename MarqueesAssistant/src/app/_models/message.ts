export interface Message {
    id: number;
    senderId: number;
    senderName: string;
    recipientId: number;
    sendDate: number;
    content: string;
    isRead: boolean;
}