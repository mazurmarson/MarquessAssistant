export interface Marquee {
    id?: number;
    eventId: number;
    width: number;
    length: number;
    upDate?: Date;
    downDate?: Date;
    isUp?: boolean;
    isDown?: boolean;
    eventName?: string;
    description?:string;
}